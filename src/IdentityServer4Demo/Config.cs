using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer4Demo
{
    public class Config
    {
        internal static X509Certificate2 GetSigningCertificate(string rootPath)
        {
            var fileName = Path.Combine(rootPath, "cert.pfx");

            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("Signing Certificate is missing!");
            }

            var cert = new X509Certificate2(fileName, "12345678");
            return cert;
        }
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "100000B",
                    Username = "alice",
                    Password = "password",

                    Claims = new []
                    {
                        new Claim("name", "Alice"),
                        new Claim("family_name", "Buyer"),
                        new Claim("email", "alice@buyer.com"),
                        new Claim("company", "BuyerCompany"),
                        new Claim("phone_number", "+14155559999"),
                        new Claim("zoneinfo", "Asia/Almaty")
                    }
                },
                new TestUser
                {
                    SubjectId = "100001B",
                    Username = "alice2",
                    Password = "password",

                    Claims = new []
                    {
                        new Claim("name", "Alice2"),
                        new Claim("family_name", "Buyer2"),
                        new Claim("email", "alice2@buyer.com"),
                        new Claim("company", "BuyerCompany"),
                        new Claim("phone_number", "+14155559998"),
                        new Claim("zoneinfo", "Asia/Almaty")
                    }
                },
                new TestUser
                {
                    SubjectId = "200000S",
                    Username = "bob",
                    Password = "password",

                    Claims = new []
                    {
                        new Claim("name", "Bob"),
                        new Claim("family_name", "Smart Supplier"),
                        new Claim("email", "bob@supplier.com"),
                        new Claim("company", "SupplierCompany"),
                        new Claim("phone_number", "+14155559912"),
                        new Claim("zoneinfo", "Asia/Almaty")
                    }
                },
                new TestUser
                {
                    SubjectId = "200001S",
                    Username = "bob2",
                    Password = "password",

                    Claims = new []
                    {
                        new Claim("name", "Bob2"),
                        new Claim("family_name", "Smart Supplier2"),
                        new Claim("email", "bob2@supplier.com"),
                        new Claim("company", "SupplierCompany"),
                        new Claim("phone_number", "+14155559911"),
                        new Claim("zoneinfo", "Asia/Almaty")
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("api", "Demo API")
                {
                    ApiSecrets = { new Secret("secret".Sha256()) }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "tradefin_api",
                    ClientName = "TradeFin API Client",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,

                    RedirectUris = { "http://localhost:8080/oauth/callback/oidc" },
                    PostLogoutRedirectUris = { "http://localhost:8080/auth/success" },
                    FrontChannelLogoutUri = "http://localhost:5000/signout-idsrv",

                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = { "openid", "profile", "email", "api", "company", "phoneNumber", "timeZone" },
                    AllowOfflineAccess = true,
                    BackChannelLogoutSessionRequired = true,
                    BackChannelLogoutUri = "http://localhost:8080/logout"
                }
            };
        }
    }
}
