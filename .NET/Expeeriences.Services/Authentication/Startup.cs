using System;
using System.IO;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using AspNetCore.Identity.DynamoDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Authentication
{
    /// <summary>
    /// https://github.com/miltador/AspNetCore.Identity.DynamoDB/blob/master/samples/IdentitySample.Mvc/Startup.cs
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonDynamoDB>();

            services.Configure<DynamoDbSettings>(Configuration.GetSection("DynamoDB"));
            services.Configure<JwtSettings>(Configuration.GetSection("JWTSettings"));

            services.AddDynamoDBIdentity<DynamoIdentityUser, DynamoIdentityRole>()
                .AddUserStore()
                .AddRoleStore()
                .AddRoleUsersStore();

            //services.AddAuthentication(options =>
            //{
            //    // This is the Default value for ExternalCookieAuthenticationScheme
            //    options.DefaultSignInScheme = new IdentityCookieOptions().ExternalCookieAuthenticationScheme;
            //});

            //services.TryAddSingleton<IdentityMarkerService>();
            services.TryAddSingleton<IUserValidator<DynamoIdentityUser>, UserValidator<DynamoIdentityUser>>();
            services.TryAddSingleton<IPasswordValidator<DynamoIdentityUser>, PasswordValidator<DynamoIdentityUser>>();
            services.TryAddSingleton<IPasswordHasher<DynamoIdentityUser>, PasswordHasher<DynamoIdentityUser>>();
            services.TryAddSingleton<ILookupNormalizer, UpperInvariantLookupNormalizer>();
            services.TryAddSingleton<IdentityErrorDescriber>();
            services.TryAddSingleton<ISecurityStampValidator, SecurityStampValidator<DynamoIdentityUser>>();
            services
                .TryAddSingleton<IUserClaimsPrincipalFactory<DynamoIdentityUser>, UserClaimsPrincipalFactory<DynamoIdentityUser>>();
            services.TryAddSingleton<UserManager<DynamoIdentityUser>, UserManager<DynamoIdentityUser>>();
            services.TryAddScoped<SignInManager<DynamoIdentityUser>, SignInManager<DynamoIdentityUser>>();

            AddDefaultTokenProviders(services);
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            var options = app.ApplicationServices.GetService<IOptions<DynamoDbSettings>>();
            var client = env.IsDevelopment()
                ? new AmazonDynamoDBClient(new AmazonDynamoDBConfig
                {
                    ServiceURL = options.Value.ServiceUrl
                })
                : new AmazonDynamoDBClient();
            var context = new DynamoDBContext(client);

            var userStore = app.ApplicationServices.GetService<IUserStore<DynamoIdentityUser>>()
                as DynamoUserStore<DynamoIdentityUser, DynamoIdentityRole>;
            var roleStore = app.ApplicationServices.GetService<IRoleStore<DynamoIdentityRole>>()
                as DynamoRoleStore<DynamoIdentityRole>;
            var roleUserStore = app.ApplicationServices
                .GetService<DynamoRoleUsersStore<DynamoIdentityRole, DynamoIdentityUser>>();

            userStore.EnsureInitializedAsync(client, context, options.Value.UsersTableName).Wait();
            roleStore.EnsureInitializedAsync(client, context, options.Value.RolesTableName).Wait();
            roleUserStore.EnsureInitializedAsync(client, context, options.Value.RoleUsersTableName).Wait();
        }

        private void AddDefaultTokenProviders(IServiceCollection services)
        {
            var dataProtectionProviderType = typeof(DataProtectorTokenProvider<>).MakeGenericType(typeof(DynamoIdentityUser));
            var phoneNumberProviderType = typeof(PhoneNumberTokenProvider<>).MakeGenericType(typeof(DynamoIdentityUser));
            var emailTokenProviderType = typeof(EmailTokenProvider<>).MakeGenericType(typeof(DynamoIdentityUser));
            AddTokenProvider(services, TokenOptions.DefaultProvider, dataProtectionProviderType);
            AddTokenProvider(services, TokenOptions.DefaultEmailProvider, emailTokenProviderType);
            AddTokenProvider(services, TokenOptions.DefaultPhoneProvider, phoneNumberProviderType);
        }

        private void AddTokenProvider(IServiceCollection services, string providerName, Type provider)
        {
            services.Configure<IdentityOptions>(
                options => { options.Tokens.ProviderMap[providerName] = new TokenProviderDescriptor(provider); });

            services.AddSingleton(provider);
        }

        public class DynamoDbSettings
        {
            public string ServiceUrl { get; set; }
            public string UsersTableName { get; set; }
            public string RolesTableName { get; set; }
            public string RoleUsersTableName { get; set; }
        }
    }
}
