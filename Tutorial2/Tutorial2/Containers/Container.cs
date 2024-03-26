namespace Tutorial2.Containers;

public abstract class Container(int height, double tareWeight, int depth, int maxPayload)
{
    protected double CargoMass { get; set; }
    public int Height { get; } = height;
    private double TareWeight { get; } = tareWeight;
    public int Depth { get; } = depth;
    public string SerialNumber { get; protected init; }
    protected int MaxPayload { get; } = maxPayload;
    protected char Type { get; init; }
    protected static int IdStatic { get; set; }


    public virtual void Empty()
    {
        CargoMass = 0;
    }

    public virtual void Load(double mass)
    {
        if (CargoMass + mass > MaxPayload) throw new OverflowException("The cargo has exceeded maximum capacity!");
        CargoMass += mass;
    }

    public double GetTotalMass()
    {
        return TareWeight + CargoMass;
    }

    public override string ToString()
    {
        return SerialNumber + $"(Cargo mass: {CargoMass}t, Total mass: {GetTotalMass()}t, " +
               $"Available: {MaxPayload-CargoMass}t)";
    }

    public bool IsEmpty()
    {
        return CargoMass == 0;
    }
}