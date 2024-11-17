using MediatR;
using Microsoft.AspNetCore.Mvc;
using Teste.Carrefour.Lancamento.Core.Application.Commands;
using Teste.Carrefour.Lancamento.Core.Domain.Enums;

namespace Teste_Carrefour_Lancamento.Endpoints
{
    public static class TransactionEndpoints
    {
        public static IEndpointRouteBuilder MapTransactionEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("api/v1/transactions", async ([FromServices] IMediator mediator, 
                                                            decimal amount,
                                                            TransactionType transactionType) =>
            {
                var command = new RegisterTransactionCommand(amount, transactionType);
                var result = await mediator.Send(command);

                return result ? Results.Ok("Transaction Registered Succesfully") : Results.StatusCode(500);
            })
            .WithName("RegisterTransaction")
            .WithTags("Transactions");

            return endpoints;
        }
    }
}
