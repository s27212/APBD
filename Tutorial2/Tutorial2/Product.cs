namespace Containers;

public class Product
{
    public Product(ProductType productType, double temp)
    {
        ProductType = productType;
        Temp = temp;
    }

    public ProductType ProductType { get; }
    public double Temp { get; }
}