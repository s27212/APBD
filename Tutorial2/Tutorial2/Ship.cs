using Tutorial2.Containers;

namespace Tutorial2;

public class Ship(List<Container> containers, int maxSpeedKnots, int maxContainers, double maxWeight, string name)
{
    private List<Container> _containers = containers;
    public string Name { get; } = name;
    private int MaxSpeedKnots { get; } = maxSpeedKnots;
    private int MaxContainers { get; } = maxContainers;
    private double MaxWeight { get; } = maxWeight;

    public void Load(Container container)
    {
        if (_containers.Count + 1 > MaxContainers)
        {
            throw new OverflowException("The ship cannot take any more containers");
        }
        if (GetTotalMass() + container.GetTotalMass() > MaxWeight)
        {
            throw new OverflowException("The maximum cargo weight is exceeded");
        }
        _containers.Add(container);
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
        var container = _containers.Find(c => c.SerialNumber == toReplace);
        if (container == null)
        {
            Console.WriteLine("Container with such serial number does not exist");
        }
        else
        {
            _containers[_containers.IndexOf(container)] = replaceWith;
        }
    }

    public void Unload(Container container)
    {
        _containers.Remove(container);
    }

    public override string ToString()
    {
        return $"{Name} (Maximum speed: {MaxSpeedKnots}; Maximum containers number: {MaxContainers}" +
               $"; Maximum weight: {MaxWeight})";
    }

    public void Info()
    {
        Console.WriteLine(this);
        Console.WriteLine("Loaded containers:");
        foreach (var c in _containers)
        {
            Console.WriteLine(c);
        }
    }

    private double GetTotalMass()
    {
        return _containers.Sum(c => c.GetTotalMass());
    }

    public int GetContainersNumber()
    {
        return _containers.Count;
    }

    public bool HasContainer(Container container)
    {
        return _containers.Contains(container);
    }
}