using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class BaseController :  ControllerBase
{ 
    protected IActionResult Success<T>(T entity)
    {
        return Ok(Result<T>.Factory.BuildSuccess(entity));
    }
    
    protected IActionResult Success()
    {
        return Ok(Result.Factory.BuildSuccess());
    }
    
    protected IActionResult Created<T>(T entity)
    {
        return Created(Result<T>.Factory.BuildSuccess(entity));
    }
    
    protected IActionResult ErrorWithMessage(string message)
    {
        return Ok(Result.Factory.BuildError(message));
    }
    
    protected IActionResult ErrorWithException(Exception ex)
    {
        return Ok(Result.Factory.BuildError(ex));
    }
    
    protected IActionResult NotFoundWithMessage(string message)
    {
        return NotFound(Result.Factory.BuildError(message));
    }
    
}