namespace Containers;

public abstract class Container
{
    public double CargoMass { get; set; }
    public int Height { get; set; }
    public double TareWeight { get; set; }
    public int Depth { get; set; }
    public string SerialNumber { get; set; }
    public int MaxPayload { get; set; }
    protected char Type { get; set; }
    private static int _idStatic;
    

    public Container(int height, double tareWeight, int depth, int maxPayload)
    {
        Height = height;
        TareWeight = tareWeight;
        Depth = depth;
        MaxPayload = maxPayload;
        CargoMass = 0;
        SerialNumber = "KON-" + Type + "-" + _idStatic++;
    }

    public void Empty()
    {
        CargoMass = 0;
    }

    public virtual void Load(double mass)
    {
        if (CargoMass + mass > MaxPayload) throw new OverflowException("The cargo has exceeded maximum capacity");
        CargoMass += mass;
    }

    public double GetTotalMass()
    {
        return TareWeight + CargoMass;
    }

    public override string ToString()
    {
        return SerialNumber;
    }
}