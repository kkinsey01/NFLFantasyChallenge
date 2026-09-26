namespace ScoreUpdater.Models;

public class ApiResponse<T>
{
    public int StatusCode { get; set; }
    public T Body { get; set; }
}
