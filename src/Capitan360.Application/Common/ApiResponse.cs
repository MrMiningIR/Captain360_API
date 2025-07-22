namespace Capitan360.Application.Common;

public class ApiResponse<T>(int statusCode, string message, T? data, bool logout = false)
{
    public int StatusCode { get; set; } = statusCode;
    public string? Message { get; set; } = message;

    public bool ForceLogout { get; set; } = logout;
    public T? Data { get; set; } = data;

    public bool Success => StatusCode is >= 200 and < 300;

    public static ApiResponse<T> Ok(T data, string message = "Success")
        => new ApiResponse<T>(200, message, data);

    public static ApiResponse<T> Created(T data, string message = "Created successfully")
        => new ApiResponse<T>(201, message, data);

    public static ApiResponse<T> Deleted(string message = "Deleted successfully")
        => new ApiResponse<T>(200, message, default);

    public static ApiResponse<T> Updated(T data, string message = "Updated successfully")
        => new ApiResponse<T>(200, message, data);

    public static ApiResponse<T> Error(int statusCode, string message)
        => new ApiResponse<T>(statusCode, message, default);


}

