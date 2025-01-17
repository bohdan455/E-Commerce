using Offers.Options;
using Offers.Services.Extensions;
using Offers.Services.Services;
using Offers.Services.Services.Interfaces;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.Configure<ElasticSearchOptions>(builder.Configuration.GetSection(ElasticSearchOptions.ElasticSearch));

builder.Services.AddRepositories();
builder.Services.AddTransient<IProductService, ProductService>();

var app = builder.Build();
await app.Services.InitElasticearchIndexes();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
