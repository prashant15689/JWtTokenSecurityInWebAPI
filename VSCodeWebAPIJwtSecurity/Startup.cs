using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace VSCodeWebAPIJwtSecurity
{
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
            services.AddControllers();

            services.AddAuthentication(builder =>
            {
                builder.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                builder.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                builder.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                jwt.SaveToken = true;
                jwt.RequireHttpsMetadata = false;
                jwt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "Jwt",
                    ValidAudience = "Jwt",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecudfhsdfhdfhsdfhsdhdfhreKey"))
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
