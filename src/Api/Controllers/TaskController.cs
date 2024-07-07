using Api.DTOs;
using Domain;
using Domain.Aggregations.Task.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("task")]
public class TaskController : BaseController
{
    private readonly IRepository _repository;

    public TaskController(IRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var user = _repository.Query<TaskUser>().FirstOrDefault(x => x.Id == id);

        if (user == null)
        {
            return NotFoundWithMessage("Nenhuma ustarefauário encontrad.");
        }

        return Success(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = _repository.Query<TaskUser>().ToList();

        return Success(users);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] UserCreateDto dto)
    {
        var user = _repository.AddAsync(dto);

        return Success(user);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] TaskDto dto)
    {
        var user = await _repository.Query<TaskUser>().FirstOrDefaultAsync(x => x.Id == id);

        if (user is not null)
        {
            user.Update(dto.Title, dto.Description, dto.Status);
            _repository.Update(user);
            await _repository.CommitAsync();
        }

        return NotFoundWithMessage("Tarefa não pode ser atualizada");
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var user = _repository.Query<TaskUser>().FirstOrDefault(u => u.Id == id);
        if (user is not null)
        {
            _repository.Delete(user);
            await _repository.CommitAsync();
        }

        return NotFoundWithMessage("Falha ao deletar tarefa");
    }
}