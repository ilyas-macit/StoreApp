namespace Entities.Models;

public class Order
{
    public int Id { get; set; }
    public ICollection<CartLine> Lines { get; set; }
    public string Name { get; set; }

    public string Country { get; set; }
    public string City { get; set; }
    public bool Shipped { get; set; }
    public DateTime OrderedDate { get; set; }
    

}