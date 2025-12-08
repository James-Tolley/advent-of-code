
var nodes = File.ReadAllLines("input.txt")
    .Select(line => line.Split(','))
    .Select(parts => new Node(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2])))
    .ToList();

// determine distances
var distances = new List<NodeDistance>(nodes.Count * (nodes.Count - 1) / 2);

for (var i = 1; i < nodes.Count; i++)
{
    for (var j = 0; j < i; j++)
    {
        var node1 = nodes[i];
        var node2 = nodes[j];
        var distance = Math.Pow(node1.X - node2.X, 2) +
                        Math.Pow(node1.Y - node2.Y, 2) +
                        Math.Pow(node1.Z - node2.Z, 2);
        distances.Add(new NodeDistance(node1, node2, distance));
    }
}

const int number = 1000;

var closest = distances
    .OrderBy(nd => nd.Distance)
    .Take(number)
    .ToList();

foreach (var distance in closest)
{
    Console.WriteLine($"Node1: ({distance.Node1.X}, {distance.Node1.Y}, {distance.Node1.Z}) " +
                      $"Node2: ({distance.Node2.X}, {distance.Node2.Y}, {distance.Node2.Z}) " +
                      $"Distance: {Math.Sqrt(distance.Distance)}");
}


// build circuits

var circuits = nodes.Select(n => n.Circuit).ToList();

foreach (var item in closest)
{
    if (item.Node1.Circuit != item.Node2.Circuit)
    {
        var circuit1 = item.Node1.Circuit;
        var circuit2 = item.Node2.Circuit;

        // merge circuits
        circuit1.AddRange(circuit2);
        foreach (var node in circuit2)
        {
            node.Circuit = circuit1;
        }
        circuits.Remove(circuit2);
    }
}

foreach (var circuit in circuits)
{
    Console.WriteLine($"Circuit size: {circuit.Count}");
}

var largestCircuits = circuits.OrderByDescending(c => c.Count).Take(3).Aggregate(1, (x, c) => x *= c.Count);

Console.WriteLine($"Largest circuits product: {largestCircuits}");

return 0;


record NodeDistance(Node Node1, Node Node2, double Distance);

class Node
{
    public Node(int x, int y, int z) 
    {
        X = x;
        Y = y;
        Z = z;
        Circuit = [this];
    }
    public int X { get; } 
    public int Y { get; } 
    public int Z { get; } 

    public List<Node> Circuit { get; set; }

}