namespace Tutorial2;

public static class TemperatureManager
{
    public static double GetTemperatureForProduct(ProductType productType)
    {
        return productType switch
        {
            ProductType.Bananas => 13.3,
            ProductType.Chocolate => 18,
            ProductType.Fish => 2,
            ProductType.Meat => -15,
            ProductType.IceCream => 18,
            ProductType.FrozenPizza => -30,
            ProductType.Cheese => 7.2,
            ProductType.Sausages => 5,
            ProductType.Butter => 20.5,
            ProductType.Eggs => 19,
            _ => throw new ArgumentException("Unknown product type")
        };
    }
}