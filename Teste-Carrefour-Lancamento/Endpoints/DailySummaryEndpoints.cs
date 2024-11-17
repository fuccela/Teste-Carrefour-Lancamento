using MediatR;
using Microsoft.AspNetCore.Mvc;
using Teste.Carrefour.Lancamento.Core.Application.Commands;

namespace Teste_Carrefour_Lancamento.Endpoints
{
    public static class DailySummaryEndpoints
    {
        public static IEndpointRouteBuilder MapDailySummaryEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("api/v1/daily-summary/{date}", async ([FromServices] IMediator mediator, DateTime date) =>
            {
                var query = new GetDailySummaryCommand(date);
                var result = await mediator.Send(query);

                return Results.Ok(new { Date = date, TotalBalance = result });
            })
            .WithName("GetDailySummary")
            .WithTags("Daily Summary");

            return endpoints;
        }
    }
}
