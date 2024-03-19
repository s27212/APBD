namespace Containers;

public abstract class Container
{
    public double cargoMass { get; set; }
    public int height { get; set; }
    public double tareWeight { get; set; }
    public int depth { get; set; }
    public string serialNumber { get; set; }
    public int maxPayload { get; set; }
    protected char type { get; set; }
    private static int idStatic;
    

    public Container(int height, double tareWeight, int depth, int maxPayload)
    {
        this.height = height;
        this.tareWeight = tareWeight;
        this.depth = depth;
        this.maxPayload = maxPayload;
        cargoMass = 0;
        serialNumber = "KON-" + type + "-" + idStatic++;
    }

    public void Empty()
    {
        cargoMass = 0;
    }

    public void Load(double mass)
    {
        if (cargoMass + mass > maxPayload) throw new OverflowException("The cargo has exceeded maximum capacity");
        cargoMass += mass;
    }

    public double GetTotalMass()
    {
        return tareWeight + cargoMass;
    }

    public override string ToString()
    {
        return serialNumber;
    }
}