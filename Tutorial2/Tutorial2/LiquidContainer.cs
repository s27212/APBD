namespace Containers;

public class LiquidContainer : Container, IHazardNotifier
{
    public LiquidContainer(int height, double tareWeight, int depth, int maxPayload) 
        : base(height, tareWeight, depth, maxPayload)
    {
        type = 'L';
    }
    
}