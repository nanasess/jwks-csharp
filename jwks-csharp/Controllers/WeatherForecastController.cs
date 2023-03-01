using jwks_csharp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using System.Security.Cryptography;

namespace jwks_csharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private PkiContext _pkiContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, PkiContext pkiContext)
        {
            _logger = logger;
            _pkiContext = pkiContext;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("jwks", Name = "GetJwks")]
        public async Task<IActionResult> GetJwks()
        {
            var pki = await _pkiContext.Pki.FirstOrDefaultAsync();
            if (pki == null)
            {
                return NotFound();
            }
            if (string.IsNullOrEmpty(pki.PublicKey))
            {
                return BadRequest();
            }

            using (var textReader = new StringReader(pki.PublicKey))
            {
                var pubkeyReader = new PemReader(textReader);
                RsaKeyParameters keyParameters = (RsaKeyParameters)pubkeyReader.ReadObject();
                var e = Base64UrlEncoder.Encode(keyParameters.Exponent.ToByteArrayUnsigned());
                var n = Base64UrlEncoder.Encode(keyParameters.Modulus.ToByteArrayUnsigned());
                var dict = new Dictionary<string, string>()
                {
                    {"e", e },
                    {"kty", "RSA" },
                    {"n", n }
                };

                var hash = SHA256.Create();
                Byte[] hashBytes = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dict)));

                JsonWebKey jsonWebKey = new JsonWebKey()
                {
                    Kid = Base64UrlEncoder.Encode(hashBytes),
                    Kty = "RSA",
                    E = e,
                    N = n
                };
                JsonWebKeySet jsonWebKeySet = new JsonWebKeySet();
                jsonWebKeySet.Keys.Add(jsonWebKey);
                
                return Ok(jsonWebKeySet);
            }      
        }
    }
}