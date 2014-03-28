using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Claims;
using System.Web;
using webapp.Models;

namespace webapp.Models
{
    public class DataContext : DbContext
    {
        private const string TenantIdClaimType = "http://schemas.microsoft.com/identity/claims/tenantid";
        private const string LoginUrl = "https://login.windows.net/{0}";
        private const string GraphUrl = "https://graph.windows.net";
        private const string GraphUserUrl = "https://graph.windows.net/{0}/users/{1}?api-version=2013-04-05";
        private static readonly string AppPrincipalId = ConfigurationManager.AppSettings["ida:ClientID"];
        private static readonly string AppKey = ConfigurationManager.AppSettings["ida:Password"];

        public DataContext()
            : base()
        {
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;

            if (ClaimsPrincipal.Current.Identity.IsAuthenticated)
            {
                string tenantId = ClaimsPrincipal.Current.FindFirst(TenantIdClaimType).Value;

                try
                {
                    //var connection = db.Database.Connection;
                    //var command = connection.CreateCommand();
                    //command.CommandText = @"USE FEDERATION TestFederation (FederationKey=0) WITH FILTERING = OFF, RESET";

                    string federationCmdText = string.Format(@"USE FEDERATION {0} ({1}='{2}') WITH FILTERING=ON, RESET",
                 "Goodies_Federation", "FederationKey", tenantId);
                    //   command.CommandText = federationCmdText; 

                    //   if (connection.State != ConnectionState.Open)
                    //       connection.Open();
                    //   command.ExecuteNonQuery();

                    ((IObjectContextAdapter)this).ObjectContext.Connection.Open();
                    this.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, federationCmdText);


                }
                catch (Exception ex)
                {
                    throw new Exception("Erreur SQL Azure Fédération", ex);
                };
            }

            
        }

        public DataContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}