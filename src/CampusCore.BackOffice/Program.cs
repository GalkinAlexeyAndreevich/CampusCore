using System.IO.Compression;
using CampusCore.Scheduler;
using CampusCore.Services;
using CampusCore.Tools.Utils;
using CampusCore.Tools.Utils.Json;
using Microsoft.AspNetCore.ResponseCompression;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

DatabaseUtils.Configure(
	builder.Configuration.GetConnectionString("Default")
		?? throw new InvalidOperationException("Connection string 'Default' is not configured.")
);

builder.Host.ConfigureServices(
	(context, serviceCollection) =>
	{
		serviceCollection
			.AddServices()
			.AddControllersWithViews()
			.AddJsonOptions(options => TextJsonSerializer.Configure(options.JsonSerializerOptions));

		serviceCollection.AddSingleton<IJsonSerializer>(new TextJsonSerializer());

		serviceCollection.AddScheduler();

		serviceCollection.Configure<GzipCompressionProviderOptions>(options =>
		{
			options.Level = CompressionLevel.Optimal;
		});

		serviceCollection.AddResponseCompression(options =>
		{
			options.EnableForHttps = true;
			options.Providers.Add<BrotliCompressionProvider>();
			options.Providers.Add<GzipCompressionProvider>();
		});
	}
);

WebApplication app = builder.Build();

app.UseResponseCompression();

if (builder.Configuration.GetValue("UseHttpsRedirection", true))
	app.UseHttpsRedirection();

app
	.UseStaticFiles()
	.UseRouting()
	.UseEndpoints(endpoints => endpoints.MapControllers());

app.MapFallbackToController("{*path:nonfile}", "Index", "Home");

app.Run();
