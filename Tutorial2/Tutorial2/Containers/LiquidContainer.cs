namespace Tutorial2.Containers;

public class LiquidContainer : Container, IHazardNotifier
{
    private bool ContainsHazardous { get; }
    public LiquidContainer(int height, double tareWeight, int depth, int maxPayload, bool containsHazardous) 
        : base(height, tareWeight, depth, maxPayload)
    {
        Type = 'L';
        SerialNumber = "KON-" + Type + "-" + IdStatic++;
        ContainsHazardous = containsHazardous;
    }

    public override void Load(double mass)
    {
        var cargoMass = CargoMass + mass;
        if (ContainsHazardous ? cargoMass > MaxPayload * 0.5 : cargoMass > MaxPayload * 0.9)
        {
            ((IHazardNotifier)this).Notify();
            return;
        }

        base.Load(mass);
    }
}