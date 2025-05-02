using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();

builder.Services.AddScoped<Func<IDbConnection>>(_ => () =>
{
    var connection = new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"));
    connection.Open(); 
    return connection;
});

var app = builder.Build();

app.UseSwaggerGen();

app.UseFastEndpoints();

app.Run();

