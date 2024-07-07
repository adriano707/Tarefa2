using Api.DTOs;
using Domain;
using Domain.Aggregations.User.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("users")]
public class UserController : BaseController
{
    private readonly IRepository _repository;

    public UserController(IRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var user = _repository.Query<User>().FirstOrDefault(x => x.Id == id);

        if (user == null)
        {
            return NotFoundWithMessage("Nenhum usu�rio encontrado.");
        }

        return Success(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = _repository.Query<User>().ToList();

        return Success(users);
    }

    [HttpPost]
    public async Task<IActionResult> Add(UserCreateDto dto)
    {
        var user = new User(dto.UserName, dto.Password);

        await _repository.AddAsync(user);
        await _repository.CommitAsync();

        return Success(user);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UserUpdateDto dto)
    {
        var user = await _repository.Query<User>().FirstOrDefaultAsync(x => x.Id == id);

        if (user is not null)
        {
            user.Update(dto.UserName, dto.Password);
            _repository.Update(user);
            await _repository.CommitAsync();
        }

        return Success(user);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var user = _repository.Query<User>().FirstOrDefault(u => u.Id == id);
        if (user is not null)
        { 
            _repository.Delete(user);
            await _repository.CommitAsync();
        }

        return NotFoundWithMessage("Falha ao deletar usu�rio");
    }
}