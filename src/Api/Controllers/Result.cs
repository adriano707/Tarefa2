using System.Net;

namespace Api.Controllers;

public class Result
{
    public int Code { get; private set; }
    public bool Success { get; }
    public List<string>? Messages { get; private set; }

    public Result(bool success, List<string>? messages, int code)
    {
        Success = success;
        Messages = messages;
        Code = code;
    }
    
    public Result(bool success, int code)
    {
        Success = success;
        Code = code;
    }

    public static class Factory
    {
        public static Result<T> BuildSuccess<T>(T entity)
        {
            return new Result<T>(true, entity)
            {
                Code = (int)HttpStatusCode.OK
            };
        }
        
        public static Result BuildSuccess()
        {
            return new Result(true, (int)HttpStatusCode.OK);
        }
        
        public static Result BuildError(Exception ex)
        {
            List<string> errors = new List<string>();
            
            errors.Add(ex.Message);
            errors.Add(ex.StackTrace);
            
            return new Result(false, errors, (int) HttpStatusCode.Conflict);
        }
        
        public static Result BuildError(string message)
        {
            List<string> errors = new List<string>() { message };
            
            return new Result(false, errors, (int) HttpStatusCode.Conflict);
        }
        
        public static Result BuildError(List<string> messages)
        {
            List<string> errors = messages;
            
            return new Result(false, errors, (int) HttpStatusCode.Conflict);
        }
    }
}

public class Result<T> : Result
{
    public T Data { get; }

    public Result(bool success, T data, List<string> messages = null, int code = 0): base(success, messages, code)
    {
        Data = data;
    }
}