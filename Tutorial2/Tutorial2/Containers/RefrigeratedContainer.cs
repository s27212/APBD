namespace Tutorial2.Containers;

public class RefrigeratedContainer : Container
{
    public RefrigeratedContainer(int height, double tareWeight, int depth, int maxPayload, double temp)
        : base(height, tareWeight, depth, maxPayload)
    {
        Type = 'C';
        SerialNumber = "KON-" + Type + "-" + IdStatic++;
        Temp = temp;
    }

    private ProductType? CargoType { get; set; }
    private double Temp { get; set;}
    public void Load(double mass, ProductType productType)
    {
        if (CargoType != null && CargoType != productType)
        {
            Console.WriteLine("Selected container already stores a different type of product.");
            return;
        }
        if (Temp < TemperatureManager.GetTemperatureForProduct(productType))
        {
            Console.WriteLine("The temperature is too low for the selected product!");
            return;
        }
        base.Load(mass);
        CargoType = productType;
    }

    public override string ToString()
    {
        return SerialNumber + $"({CargoType.ToString()}, Cargo mass: {CargoMass}t, Total mass: {GetTotalMass()}t, " +
               $"Available: {MaxPayload-CargoMass}t)";
    }
}