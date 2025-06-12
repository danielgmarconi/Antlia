namespace Antlia.Application.Common;

public sealed class MethodResponse
{
    public bool Success { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public object? Response { get; set; }
    public void Update(bool successm, int statusCode, string message, object response)
    {
        Success = successm;
        StatusCode = statusCode;
        Message = message;
        Response = response;
    }
    public void Update(bool successm, int statusCode, string message)
    {
        Success = successm;
        StatusCode = statusCode;
        Message = message;
    }
    public void Update(int statusCode, string message, object response)
    {
        StatusCode = statusCode;
        Message = message;
        Response = response;
    }
    public void Update(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }
}
