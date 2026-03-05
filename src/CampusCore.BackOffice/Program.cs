using System.IO.Compression;
using CampusCore.Services;
using CampusCore.Tools.Utils.Json;
using Microsoft.AspNetCore.ResponseCompression;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureServices(
	(context, serviceCollection) =>
	{
		serviceCollection
			.AddServices()
			.AddControllersWithViews()
			.AddJsonOptions(options => TextJsonSerializer.Configure(options.JsonSerializerOptions));

		serviceCollection.AddSingleton<IJsonSerializer>(new TextJsonSerializer());

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

app.UseResponseCompression()
	.UseHttpsRedirection()
	.UseStaticFiles()
	.UseRouting()
	.UseEndpoints(endpoints => endpoints.MapControllers());

app.MapFallbackToController("{*path:nonfile}", "Index", "Home");

app.Run();
