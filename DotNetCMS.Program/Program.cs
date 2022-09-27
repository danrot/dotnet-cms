using DotNetCMS.Application.Pages;
using DotNetCMS.Application.Security;
using DotNetCMS.Domain.Pages;
using DotNetCMS.Persistence.EntityFrameworkCore;
using DotNetCMS.Persistence.EntityFrameworkCore.Pages;
using DotNetCMS.Persistence.EntityFrameworkCore.AspNetCore;
using DotNetCMS.Security.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DotNetCMS");

builder.Services.AddControllers(options =>
{
	options.Filters.Add(typeof(SecurityFilter));
	options.Filters.Add(typeof(TransactionFilter));
});

builder.Services.AddDbContext<CmsContext>(
	options => options.UseMySql(
		connectionString,
		ServerVersion.AutoDetect(connectionString),
		sqlOptions => sqlOptions.MigrationsAssembly("DotNetCMS.Program")
	)
);

builder.Services
	.AddAuthentication(authenticationOptions =>
	{
		authenticationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		authenticationOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
		authenticationOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	})
	.AddJwtBearer(jwtBearerOptions =>
	{
		jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
		{
			ValidAudience = builder.Configuration.GetValue<string>("JwtSettings:Audience"),
			ValidIssuer = builder.Configuration.GetValue<string>("JwtSettings:Issuer"),
			IssuerSigningKey = new SymmetricSecurityKey(
				Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JwtSettings:Secret"))
			)
		};
	})
;

builder.Services.AddScoped<ISecurityService, SecurityService>();

builder.Services.AddScoped<IPageRepository, PageRepository>();
builder.Services.AddScoped<PageService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}

app.MapControllers();

app.Run();
