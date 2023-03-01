using System.ComponentModel.DataAnnotations.Schema;

namespace jwks_csharp.Models
{
    [Table("pki")]
    public partial class Pki
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        [Column("private_key", TypeName = "text")]
        public string? PrivateKey { get; set; }
        [Column("public_key", TypeName = "text")]
        public string? PublicKey { get; set; }
    }
}
