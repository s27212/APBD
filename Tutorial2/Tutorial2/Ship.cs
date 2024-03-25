namespace Containers;

public class Ship
{
    public Ship(List<Container> containers, int maxSpeedKnots, int maxContainers, double maxWeight)
    {
        this.containers = containers;
        MaxSpeedKnots = maxSpeedKnots;
        MaxContainers = maxContainers;
        MaxWeight = maxWeight;
    }

    public List<Container> containers { get; set; }
    public int MaxSpeedKnots { get; set; }
    public int MaxContainers { get; set; }
    public double MaxWeight { get; set; }

    public void Load(Container container)
    {
        if (containers.Count + 1 > MaxContainers)
        {
            Console.WriteLine("The ship cannot take any more containers");
        }
        else if (GetTotalMass() + container.GetTotalMass() > MaxWeight)
        {
            Console.WriteLine("The maximum cargo weight is exceeded");
        }
        else
        {
            containers.Add(container);
        }
    }

    public void Load(List<Container> containerList)
    {
        foreach (var container in containerList)
        {
            Load(container);
        }
    }

    public void Transfer(Container container, Ship other)
    {
        other.Load(container);
        Unload(container);
    }

    public void Replace(string toReplace, Container replaceWith)
    {
        var container = containers.Find(c => c.SerialNumber == toReplace);
        if (container == null)
        {
            Console.WriteLine("Container with such serial number does not exist");
        }
        else
        {
            containers[containers.IndexOf(container)] = replaceWith;
        }
    }

    private void Unload(Container container)
    {
        containers.Remove(container);
    }

    public void UnloadLast()
    {
        containers.RemoveAt(containers.Count-1);
    }

    public void Info()
    {
        Console.WriteLine("Maximum speed: {0}\nMaximum containers number: {1}\nMaximum weight: {2}",
            MaxSpeedKnots,MaxContainers,MaxWeight);
        Console.WriteLine("Loaded containers:");
        foreach (var c in containers)
        {
            Console.WriteLine(c);
        }
    }
    
    

    private double GetTotalMass()
    {
        return containers.Sum(c => c.GetTotalMass());
    }
}