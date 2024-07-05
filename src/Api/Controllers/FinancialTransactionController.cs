using Api.DTOs;
using Domain;
using Domain.Aggregations.FinancialTransactionAggregate;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("transactions")]
public class FinancialTransactionController : BaseController
{
    private readonly IRepository _repository;

    public FinancialTransactionController(IRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var movement = _repository.Query<FinancialTransaction>().FirstOrDefault(x => x.Id == id);

        if (movement == null)
        {
            return NotFoundWithMessage("Nenhum registro encontrado.");
        }

        return Success(movement);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var movements = _repository.Query<FinancialTransaction>().ToList();

        return Success(movements);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] FinancialTransactionCreateDTO dto)
    {
        var movement = _repository.AddAsync(dto);

        return Success(movement);
    }
}