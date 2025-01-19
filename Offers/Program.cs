using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using Elastic.Transport;
using Offers.Options;
using Offers.Services.Extensions;
using Offers.Services.Services;
using Offers.Services.Services.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

ConfigureLogging(builder.Configuration);
builder.Host.UseSerilog();

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.Configure<ElasticSearchOptions>(builder.Configuration.GetSection(ElasticSearchOptions.ElasticSearch));

builder.Services.AddRepositories();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IProductService, ProductService>();

var app = builder.Build();
await app.Services.InitElasticearchIndexes();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();


void ConfigureLogging(IConfiguration configuration)
{
    Log.Logger = new LoggerConfiguration().Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(new [] { new Uri(configuration["Elasticsearch:Url"] )}, opts =>
        {
            opts.DataStream = new DataStreamName("logs", "offers", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "development");
        }, transport =>
        {
            transport.Authentication(new BasicAuthentication(configuration["Elasticsearch:Username"], configuration["Elasticsearch:Password"])); // Basic Auth
        })
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}