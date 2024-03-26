namespace Tutorial2.Containers;

public class GasContainer : Container, IHazardNotifier
{
    public GasContainer(int height, double tareWeight, int depth, int maxPayload) : base(height, tareWeight, depth, maxPayload)
    {
        Type = 'G';
        SerialNumber = "KON-" + Type + "-" + IdStatic++;
    }

    public override void Empty()
    {
        CargoMass *= 0.05;
    }

    public override void Load(double mass)
    {
        if (CargoMass + mass > MaxPayload * 0.5)
        {
            ((IHazardNotifier)this).Notify();
        }
        base.Load(mass);
    }
}