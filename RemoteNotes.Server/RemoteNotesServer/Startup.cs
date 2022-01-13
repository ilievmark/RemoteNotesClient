using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RemoteNotes.BL.Authorization;
using RemoteNotes.BL.Contract.Authorization;
using RemoteNotes.BL.Contract.Note;
using RemoteNotes.BL.Contract.Password;
using RemoteNotes.BL.Contract.Token;
using RemoteNotes.BL.Contract.User;
using RemoteNotes.BL.Contract.UserToken;
using RemoteNotes.BL.Note;
using RemoteNotes.BL.Security;
using RemoteNotes.BL.Security.Password;
using RemoteNotes.BL.Security.Token;
using RemoteNotes.BL.Security.UserToken;
using RemoteNotes.BL.User;
using RemoteNotes.Hubs.Hubs;

namespace RemoteNotesServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSignalR();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Automatic APIs", Version = "v1" });
                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer"
                    }
                );
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });

            services.AddAuthorizationCore();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = AuthOptions.Issuer,
                            ValidateAudience = true,
                            ValidAudience = AuthOptions.Audience,
                            ValidateLifetime = true,
                            IssuerSigningKey = AuthOptions.SymmetricSecurityKey,
                            ValidateIssuerSigningKey = true,
                        };
                        options.Events = new JwtBearerEvents 
                        {
                            OnMessageReceived = context => 
                            {
                                var accessToken = context.Request.Query["access_token"];
                                if (!string.IsNullOrEmpty(accessToken))
                                    context.Token = accessToken;
                                return Task.CompletedTask;
                            }
                        };
                    });

            services.AddScoped<ITokenService, JwtTokenService>();
            services.AddScoped<IUserTokenService, JwtUserTokenService>();
            services.AddScoped<IPasswordCryptor, PasswordCryptor>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<IUserService, UserService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
            }

            app.UseStaticFiles();
            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(builder =>
            {
                builder.MapHub<NotesHub>("/notes");
                builder.MapHub<UserHub>("/user");
            });
        }
    }
}