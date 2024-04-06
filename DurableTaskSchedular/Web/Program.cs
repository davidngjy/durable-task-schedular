using DurableTaskSchedular.Web;
using DurableTaskSchedular.Web.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebServices();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapUserEndpoints();

app.Run();
