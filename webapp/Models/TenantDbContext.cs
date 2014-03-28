using System;
using System.Data.Entity;

namespace webapp.Models
{
    public class TenantDbContext : DbContext
    {
        public TenantDbContext()
            : base("TenantDbContext")
        {

        }

        public DbSet<Tenant> Tenants { get; set; }

        public DbSet<IssuingAuthorityKey> IssuingAuthorityKeys { get; set; }

        public DbSet<SignupToken> SignupTokens { get; set; }
    }
}