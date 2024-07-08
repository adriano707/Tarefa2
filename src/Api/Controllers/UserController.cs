using Api.DTOs;
using Api.Services;
using Domain;
using Domain.Aggregations.User.Entities;
using Microsoft.AspNetCore.Authorization;
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

    [HttpPost]
    [AllowAnonymous]
    [Route("login")]
    public async Task<IActionResult> LogIn([FromBody] UserDto dto)
    {

        var user = _repository.Query<User>().FirstOrDefault(u => u.UserName == dto.UserName);

        if (user == null) return Unauthorized("User not found. ");
        if (user.Password != dto.Password) return Unauthorized("Password does not match. ");

        var token = TokenService.GenerateToken(user);

        ReturnLoginDto returnLoginDto = new ReturnLoginDto()
        {
            Id = user.Id,
            UserName = user.UserName,
            Password = user.Password,
            Token = token
        };
        return Success(returnLoginDto);
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

        return NotFoundWithMessage("Falha ao deletar usuário");
    }
}