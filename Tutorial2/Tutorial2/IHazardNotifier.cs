namespace Containers;

public interface IHazardNotifier
{
    void Notify()
    {
        Console.WriteLine("Hazardous situation has occured!");
    }
}