namespace Entities.Models;

public class Cart
{
    public List<CartLine> Lines { get; set; }

    public Cart()
    {
        Lines = new List<CartLine>();
    }

    public void AddLine(Product product, int quantity)
    {
        CartLine? line = Lines.Where(l => l.Product.Id.Equals(product.Id)).SingleOrDefault();
        if (line is null)
        {
            Lines.Add(new CartLine()
                    {
                        Product = product,
                        Quantity = quantity
                    }); 
        }
        else
        {
            line.Quantity += quantity;
        }
    }

    public void RemoveLine(Product product) => Lines.RemoveAll(l => l.Product.Id.Equals(product.Id));
    public decimal ComputeTotalValue() => Lines.Sum(l => l.Product.Price * l.Quantity);
    public void Clear() => Lines.Clear();

}