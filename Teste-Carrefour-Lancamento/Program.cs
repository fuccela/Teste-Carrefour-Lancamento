using Teste.Carrefour.Lancamento.Core.Application.Commands;
using Teste.Carrefour.Lancamento.Infrastructure.DI;
using Teste_Carrefour_Lancamento.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterTransactionHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetDailySummaryHandler).Assembly));
builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapTransactionEndpoints();
app.MapDailySummaryEndpoints();

app.Run();
