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
            return NotFoundWithMessage("Nenhum usuário encontrado.");
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
    public async Task<IActionResult> Add([FromBody] UserCreateDto dto)
    {
        var user = _repository.AddAsync(dto);

        return Success(user);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UserUpdateDto dto)
    {
        var user = _repository.Query<User>().FirstOrDefaultAsync(x => x.Id == dto.Id);

        user.

        return Success(user);
    }
}