namespace Entities.Models;

public class Cart
{
    public List<CartLine> Lines { get; set; }

    public Cart()
    {
        Lines = new List<CartLine>();
    }

    public virtual void AddLine(Product product, int quantity)
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

    public virtual void RemoveLine(Product product) => Lines.RemoveAll(l => l.Product.Id.Equals(product.Id));
    public virtual decimal ComputeTotalValue() => Lines.Sum(l => l.Product.Price * l.Quantity);
    public virtual void Clear() => Lines.Clear();

}