using System;
using System.Collections;
using System.IO;

#region get input
string filePath = "./input.txt";
string content = File.ReadAllText(filePath);
string[] lines = content.Split('\n');
int size = int.Parse(lines[0]);
List<List<(int, int)>> graph = new List<List<(int, int)>>(size);
for (int i = 0; i < size; i++)
    graph.Add(new List<(int, int)>(size));
foreach (var line in lines.Skip(1))
{
    var parts = line.Split(' ');
    int u = int.Parse(parts[0]);
    int v = int.Parse(parts[1]);
    int w = int.Parse(parts[2]);
    graph[u].Add((v, w));
    graph[v].Add((u, w));
}
for (int i = 0; i < size; i++)
    graph[i].TrimExcess();
#endregion
Console.Write("Start:");
int start = -1;
while (size <= start || start < 0)
    int.TryParse(Console.ReadLine(), out start);
#region find costs
int[] costs = new int[size];
for (int i = 0; i < size; i++)
    costs[i] = int.MaxValue;
{
    BitArray visited = new BitArray(size);
    costs[start] = 0;
    int current = start;
    while (current != -1)
    {
        visited[current] = true;
        int min = int.MaxValue;
        int minIndex = -1;
        foreach (var (v, w) in graph[current])
            costs[v] = Math.Min(costs[v], costs[current] + w);
        for (int i = 0; i < size; i++)
            if (costs[i] < min && !visited[i])
            {
                min = costs[i];
                minIndex = i;
            }
        current = minIndex;
    }
}
#endregion
while (true)
{
    Console.Write("End:");
    int end = -1;
    while (size <= end || end <= 0)
        int.TryParse(Console.ReadLine(), out end);
    #region find path
    List<int> path = new List<int>();
    int current = end;
    {
        int min = int.MaxValue;
        path.Add(current);
        while (current != start && current != -1)
        {
            int minIndex = -1;
            foreach (var (v, _) in graph[current])
                if (costs[v] <= min)
                {
                    min = costs[v];
                    minIndex = v;
                }
            current = minIndex;
            path.Add(current);
        }
    }
    if (current == -1)
    {
        Console.WriteLine("No path");
        continue;
    }
        
    path.Reverse();
    Console.WriteLine($"Path: {String.Join(",", path)} distance: {costs[end]}");
}
#endregion