namespace Containers;

public class LiquidContainer : Container, IHazardNotifier
{
    public bool ContainsHazardous { get; set; }
    public LiquidContainer(int height, double tareWeight, int depth, int maxPayload, bool containsHazardous) 
        : base(height, tareWeight, depth, maxPayload)
    {
        Type = 'L';
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