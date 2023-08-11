namespace Entities.RequestParameters;

public abstract class RequestParameters
{
    public string? SearchTerm { get; set; }
    public int MinPrice { get; set; } = 0;
    public int MaxPrice { get; set; } = int.MaxValue;
    public bool IsValidPrice => MaxPrice > MinPrice;
}