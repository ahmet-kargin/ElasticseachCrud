using ElasticseachCrudAPI.Model;
using ElasticseachCrudAPI.Services;
using Microsoft.Extensions.DependencyInjection;
using Nest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("esdemo");
var client = new ElasticClient(settings);

builder.Services.AddSingleton(client);
builder.Services.AddScoped<IElasticsearchService<MyDocument>, ElasticsearchService<MyDocument>>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();
  
app.Run();
