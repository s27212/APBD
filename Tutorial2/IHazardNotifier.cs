namespace Containers;

public interface IHazardNotifier
{
    public void Notify()
    {
        Console.WriteLine("Hazardous situation has occured!");
    }
}