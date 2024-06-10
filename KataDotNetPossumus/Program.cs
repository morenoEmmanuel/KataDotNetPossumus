using System.Reflection;
using KataDotNetPossumus.Api.Swagger;
using KataDotNetPossumus.ApiManager.Implementations;
using KataDotNetPossumus.ApiManager.Interfaces;
using KataDotNetPossumus.Business.Implementations;
using KataDotNetPossumus.Business.Interfaces;
using KataDotNetPossumus.Cryptography.Implementations;
using KataDotNetPossumus.Cryptography.Interfaces;
using KataDotNetPossumus.CurrentContext;
using KataDotNetPossumus.Enumerations;
using KataDotNetPossumus.Model.SqlContext;
using KataDotNetPossumus.Repository.Sql.Implementations;
using KataDotNetPossumus.Repository.Sql.Interfaces;
using KataDotNetPossumus.Resources;
using KataDotNetPossumus.SettingHelper;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

#region Dependencies

// App Settings
builder.Services.AddOptions<AppSettingOptions>()
	.Configure<IConfiguration>((settings, configuration) =>
	{
		SetAppSettings(configuration, settings);
	});

builder.Services.AddScoped<AppSettingHelper>();

// DB Context
builder.Services.AddDbContext<KataDotNetPossumusDbContext>();

// Encryption
builder.Services.AddScoped<ILocalEncryptionHelper, LocalEncryptionHelper>();

// Context Data
builder.Services.AddScoped<ContextData>();

// SQL Repositories
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountHistoryRepository, AccountHistoryRepository>();
builder.Services.AddScoped<IAuthenticationBusiness, AuthenticationBusiness>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IWalletRepository, WalletRepository>();

// Businesses
builder.Services.AddScoped<IAccountBusiness, AccountBusiness>();
builder.Services.AddScoped<IAccountHistoryBusiness, AccountHistoryBusiness>();
builder.Services.AddScoped<ICurrencyBusiness, CurrencyBusiness>();
builder.Services.AddScoped<IUserBusiness, UserBusiness>();
builder.Services.AddScoped<IWalletBusiness, WalletBusiness>();

// Api Managers
builder.Services.AddScoped<ICurrencyApiManager, CurrencyApiManager>();

#endregion
// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc(AppConsts.DefaultApiVersion, new OpenApiInfo
	{
		Title = Labels.KataDotNetPossumusApi,
		Version = AppConsts.DefaultApiVersion
	});

	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
	c.IncludeXmlComments(xmlPath);

	c.AddSecurityDefinition(AuthenticationSchemes.Bearer, new OpenApiSecurityScheme
	{
		Type = SecuritySchemeType.Http,
		BearerFormat = Labels.JWT,
		In = ParameterLocation.Header,
		Scheme = AuthenticationSchemes.Bearer
	});

	c.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint($"/swagger/{AppConsts.DefaultApiVersion}/swagger.json", AppConsts.DefaultApiVersion);
	});
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();

static void SetAppSettings(IConfiguration configuration, AppSettingOptions settings)
{
	// Gets the section "AppSettings" from the config file
	var appSettingsSection = configuration.GetSection("AppSettings");

	// Iterates for each setting key
	foreach (var generalSetting in appSettingsSection.GetChildren())
	{
		if (generalSetting.Value == null || settings.Keys.ContainsKey(generalSetting.Key)) continue;

		settings.Keys.Add(generalSetting.Key, generalSetting.Value);
	}

	// Gets all remaining settings
	foreach (var configurationSection in configuration.GetChildren())
	{
		if (configurationSection.Value == null || settings.Keys.ContainsKey(configurationSection.Key)) continue;

		settings.Keys.Add(configurationSection.Key, configurationSection.Value);
	}
}