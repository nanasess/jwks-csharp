using Microsoft.EntityFrameworkCore;

namespace jwks_csharp.Models
{
    public partial class PkiContext : DbContext
    {
        public PkiContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Pki> Pki { get; set; }
    }
}
