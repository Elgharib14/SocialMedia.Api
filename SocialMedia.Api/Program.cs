
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Api.Hellper;
using SocialMedia.Core.Identity;
using SocialMedia.Core.Reposatory;
using SocialMedia.Core.Services;
using SocialMedia.Reposatory;
using SocialMedia.Reposatory.AppContext;
using SocialMedia.Reposatory.Identitycontext;
using SocialMedia.Servecs;
using System.Text;

namespace SocialMedia.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbcontext>(
             opt =>
             {
                 opt.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"));
             });
            builder.Services.AddDbContext<AppIdentityDbContext>(
             opt =>
             {
                 opt.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnectionString"));
             });


            builder.Services.AddAutoMapper(typeof(MapeProfile));
            builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            builder.Services.AddScoped<ITokenServices, TokenServices>();
            builder.Services.AddScoped<IFrendsRepo, FrendsRepo>();

            builder.Services.AddIdentity<AppUser, IdentityRole>()
               .AddEntityFrameworkStores<AppIdentityDbContext>();

            // this code to comper the token which user inter it with the token is genreated
            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JWT:ValidAudience"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"])),
                    };
                });



            var app = builder.Build();

            var Scope = app.Services.CreateScope();
            var servers = Scope.ServiceProvider;
            var loggerFactor = servers.GetRequiredService<ILoggerFactory>();
            try
            {
                var Identitydbcontext = servers.GetRequiredService<AppIdentityDbContext>();
                await Identitydbcontext.Database.MigrateAsync();

                var dbcontext = servers.GetRequiredService<AppDbcontext>();
                await dbcontext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {

                var logger = loggerFactor.CreateLogger<Program>();
                logger.LogError(ex, ex.Message);
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot")),
                RequestPath = new PathString("/wwwroot")
            });

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}