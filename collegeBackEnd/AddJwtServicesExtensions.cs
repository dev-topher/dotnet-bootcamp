using collegeBackEnd.Models.DataModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace collegeBackEnd
{
    public static class AddJwtServicesExtensions
    {
        public static void AddJwtTokenServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            var bindJwtSettings = new JwtSettings();

            Configuration.Bind("JsonWebTokenKeys", bindJwtSettings);
            Services.AddSingleton(bindJwtSettings);
            Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = bindJwtSettings.ValidateIssuerSingingKey,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSingingKey)),
                        ValidateIssuer = bindJwtSettings.ValidateIssuer,
                        ValidIssuer = bindJwtSettings.ValidIssuer,
                        ValidateAudience = bindJwtSettings.ValidateAudience,
                        ValidAudience = bindJwtSettings.ValidAudience,
                        RequireExpirationTime = bindJwtSettings.RequireExpirationTime,
                        ValidateLifetime = bindJwtSettings.ValidateLifetime,
                        ClockSkew = TimeSpan.FromDays(1)
                    };
                });
        }
    }
}
