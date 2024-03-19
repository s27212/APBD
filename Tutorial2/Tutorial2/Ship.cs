namespace Containers;

public class Ship
{
    public List<Container> containers { get; set; }
    public int maxSpeedKnots { get; set; }
    public int maxContainers { get; set; }
    public double maxWeight { get; set; }

    public void Load(Container container)
    {
        if (containers.Count + 1 > maxContainers)
        {
            Console.WriteLine("The ship cannot take any more containers");
        }
        else if (GetTotalMass() + container.GetTotalMass() > maxWeight)
        {
            Console.WriteLine("The maximum cargo weight is exceeded");
        }
        else
        {
            containers.Add(container);
        }
    }

    public void UnloadLast()
    {
        containers.RemoveAt(containers.Count-1);
    }

    public void Info()
    {
        Console.WriteLine("Maximum speed: {0}\nMaximum containers number: {1}\nMaximum weight: {2}",
            maxSpeedKnots,maxContainers,maxWeight);
        Console.WriteLine("Loaded containers:");
        foreach (var c in containers)
        {
            Console.WriteLine(c);
        }
    }
    
    

    private double GetTotalMass()
    {
        double mass = 0;
        foreach (var c in containers)
        {
            mass += c.GetTotalMass();
        }

        return mass;
    }
}