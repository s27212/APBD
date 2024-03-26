using Tutorial2.Containers;

namespace Tutorial2.UserInterface;

public class AppController(List<Ship> ships, List<Container> containers)
{
    private List<Ship> _ships = ships;
    private List<Container> _containers = containers;
    private List<Container> _unloadedContainers = [];
    public void ShowMenu()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Ships:");
            ListElements(_ships);
            Console.WriteLine("\nContainers:");
            var count = 1;
            foreach (var container in _containers)
            {
                Console.WriteLine($"{count++}. {container}" +
                                  $" ({_ships.Find(s => s.HasContainer(container))?.Name ?? "unassigned"})");
            }
            Console.WriteLine();
            
            var dictionary = new Dictionary<int, (string, Action?)>();
            var optionCount = 1;
            dictionary.Add(optionCount++, ("Add container ship", AddShip));
            dictionary.Add(optionCount++, ("Add container", AddContainer));
            if(_ships.Count > 0) dictionary.Add(optionCount++, ("Remove container ship", RemoveShip));
            if (_containers.Count > 0)
            {
                dictionary.Add(optionCount++, ("Remove container", RemoveContainer));
                dictionary.Add(optionCount++, ("Load container", LoadContainer));
                if (_containers.Find(c => !c.IsEmpty()) != null)
                {
                    dictionary.Add(optionCount++, ("Unload container", UnloadContainer));
                }
                if (_unloadedContainers.Count > 0)
                {
                    dictionary.Add(optionCount++, ("Place container on the ship", PlaceOnShip));
                }
                
                if (_ships.Find(s => s.GetContainersNumber() != 0) != null)
                {
                    dictionary.Add(optionCount++, ("Unload container from the ship", UnloadFromShip));
                }
            }
            dictionary.Add(optionCount, ("Exit", null));
            
            foreach (var keyValuePair in dictionary)
            {
                Console.WriteLine($"{keyValuePair.Key}. {keyValuePair.Value.Item1}");
            }

            var option = GetIntInput(1, dictionary.Count);
            var action = dictionary[option].Item2;
            if (action == null) return;
            action.Invoke();
            
            
        }
    }

    private void UnloadFromShip()
    {
        Console.WriteLine("Choose ship:");
        var ships = ListElements(_ships, s => s.GetContainersNumber() != 0);
        var option1 = GetIntInput(1, ships.Count);
        var containers = ListElements(_containers,
            c => ships.Find(s => s.HasContainer(c)) != null);
        var option2 = GetIntInput(1, containers.Count);
        ships[option1-1].Unload(containers[option2-1]);
        _unloadedContainers.Add(containers[option2-1]);
        Console.WriteLine("Container successfully unloaded.");
    }

    private void PlaceOnShip()
    {
        Console.WriteLine("Choose container:");
        var containers = ListElements(_containers,
            c => _ships.Find(s => s.HasContainer(c)) == null);
        var option1 = GetIntInput(1, containers.Count);
        Console.WriteLine("Choose ship:");
        ListElements(_ships);
        var option2 = GetIntInput(1, _ships.Count);
        var container = containers[option1-1];
        try
        {
            _ships[option2-1].Load(container);
            _unloadedContainers.Remove(container);
            Console.WriteLine("Container successfully loaded.");
        }
        catch (OverflowException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private void RemoveShip()
    {
        ListElements(_ships);
        var option = GetIntInput(1, _ships.Count);
        _ships.Remove(_ships[option-1]);
        Console.WriteLine("Ship successfully deleted.");
    }

    private void UnloadContainer()
    {
        var containers = ListElements(_containers, c => !c.IsEmpty());
        var option = GetIntInput(1, containers.Count);
        var container = _containers[option-1];
        container.Empty();
        Console.WriteLine("Container was successfully emptied.");
    }

    private void RemoveContainer()
    {
        ListElements(_containers);
        var option = GetIntInput(1, _containers.Count);
        _containers.Remove(_containers[option-1]);
        Console.WriteLine("Container successfully deleted.");
    }

    private void LoadContainer()
    {

        ListElements(_containers);
        var option = GetIntInput(1, _containers.Count);
        var container = _containers[option-1];
        Console.WriteLine("Enter cargo weight:");
        var weight = GetDoubleInput(1, double.MaxValue);
        if (container is RefrigeratedContainer)
        {
            Console.WriteLine("Choose product type");
            var productNames = Enum.GetNames(typeof(ProductType));
            ListElements(productNames.ToList());
            option = GetIntInput(1, productNames.Length);
            Enum.TryParse(productNames[option - 1], out ProductType type);
            ((RefrigeratedContainer)container).Load(weight, type);
        }
        else
        {
            container.Load(weight);
        }
    }

    private void AddContainer()
    {
        Console.WriteLine("Enter height:");
        var height = GetIntInput(1, int.MaxValue);
        Console.WriteLine("Enter depth:");
        var depth = GetIntInput(1, int.MaxValue);
        Console.WriteLine("Enter tare weight:");
        var tareWeight = GetDoubleInput(1, double.MaxValue);
        Console.WriteLine("Enter maximum payload:");
        var maxPayload = GetIntInput(1, int.MaxValue);
        Console.WriteLine("1.Refrigerated container\n2.Liquid container\n3.Gas container");
        var option = GetIntInput(1, 3);
        Container? container = null;
        switch (option)
        {
            case 1:
            {
                Console.WriteLine("Enter temperature:");
                var temp = GetDoubleInput(double.MinValue, double.MaxValue);
                container = new RefrigeratedContainer(height, tareWeight, depth, maxPayload, temp);
                break;
            }
            case 2:
            {
                Console.WriteLine("Will container store hazardous cargo?\n1.Yes\n2.No");
                var containsHazardous = GetIntInput(1, 2) == 1;
                container = new LiquidContainer(height,tareWeight, depth, maxPayload, containsHazardous);
                break;
            }
            case 3:
            {
                container = new GasContainer(height, tareWeight, depth, maxPayload);
                break;
            }
        }
        _containers.Add(container);
        _unloadedContainers.Add(container);
    }

    private void AddShip()
    {
        Console.WriteLine("Enter the name of the ship:");
        var name = Console.ReadLine();
        Console.WriteLine("Enter the speed the ship:");
        var speed = GetIntInput(1, double.MaxValue);
        Console.WriteLine("Enter maximum number of containers that the ship can carry:");
        var numOfContainers = GetIntInput(1, int.MaxValue);
        Console.WriteLine("Enter maximum weight that the ship can carry:");
        var maxWeight = GetDoubleInput(1, double.MaxValue);
        _ships.Add(new Ship([],speed, numOfContainers, maxWeight, name));
        Console.WriteLine("Ship successfully added.");
    }
    
    private static void ListElements<T>(List<T> list)
    {
        ListElements(list, c => true);
    }

    private static List<T> ListElements<T>(List<T> list, Predicate<T> pred)
    {
        var meetingCriteria = list.FindAll(pred);
        var count = 1;
        foreach (var container in meetingCriteria)
        {
            Console.WriteLine($"{count++}. {container}");
        }

        return meetingCriteria;
    }


    private static double GetDoubleInput(double leftBound, double rightBound)
    {
        while (true)
        {
            var input = Console.ReadLine();
            var doubleEntered = double.TryParse(input, out var option);
            if (!doubleEntered)
            {
                Console.WriteLine("Please, enter a number.");
                continue;
            }
            if (option >= leftBound && option <= rightBound) return option;
            Console.WriteLine("Please, enter a valid number.");
        }
    }

    private static int GetIntInput(double leftBound, double rightBound)
    {
        while (true)
        {
            var input = GetDoubleInput(leftBound, rightBound);
            if (input % 1 == 0) return (int)input;
            Console.WriteLine("Please enter an integer.");
        }
    }
}