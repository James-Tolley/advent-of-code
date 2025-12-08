
List<Node> nodes = File.ReadAllLines("input.txt")
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
        distances.Add(new NodeDistance(node1, node2, node1.DistanceSquaredTo(node2)));
    }
}

// Part 1
//BuildCircuits(nodes, distances.OrderBy(nd => nd.Distance).Take(1000));

// Part 2
BuildCircuits(nodes, distances.OrderBy(nd => nd.Distance));

void BuildCircuits(List<Node> nodes, IEnumerable<NodeDistance> nodeDistances)
{
    var circuits = nodes.Select(n => n.Circuit).ToList();

    foreach (var item in nodeDistances)
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

        if (circuits.Count == 1)
        {
            // Part 2
            Console.WriteLine($"Last two nodes.X product: {(long)item.Node1.X * item.Node2.X}");
            return;
        }
    }

    // Part 1
    var largestCircuits = circuits.OrderByDescending(c => c.Count).Take(3).Aggregate(1, (x, c) => x *= c.Count);
    Console.WriteLine($"Largest 3 circuits product: {largestCircuits}");
}


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

    public double DistanceSquaredTo(Node other) => 
        Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2) + Math.Pow(Z - other.Z, 2);

    public List<Node> Circuit { get; set; }

}