namespace Containers;

public class RefrigeratedContainer : Container
{
    public RefrigeratedContainer(int height, double tareWeight, int depth, int maxPayload, double temp)
        : base(height, tareWeight, depth, maxPayload)
    {
        Type = 'C';
        Temp = temp;
    }

    public Product? CargoType { get; private set; }
    public double Temp { get; set;}
    public void Load(double mass, Product product)
    {
        if (CargoType != null && CargoType.ProductType != product.ProductType)
        {
            Console.WriteLine("Selected container already stores a different type of product.");
            return;
        }
        if (Temp < product.Temp)
        {
            Console.WriteLine("The temperature is too low for the selected product!");
            return;
        }
        base.Load(mass);
        CargoType = product;
    }
}