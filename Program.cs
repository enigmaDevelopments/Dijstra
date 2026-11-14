using System;
using System.Collections;
using System.IO;

#region get input
string filePath = "./input.txt";
string content = File.ReadAllText(filePath);
string[] lines = content.Split('\n');
int size = int.Parse(lines[0]);
List<List<(int, int, int)>> graph = new List<List<(int, int, int)>>(size);
for (int i = 0; i < size; i++)
    graph.Add(new List<(int, int, int)>(size));
foreach (var line in lines.Skip(1))
{
    var parts = line.Split(' ');
    int u = int.Parse(parts[0]);
    int v = int.Parse(parts[1]);
    int w = int.Parse(parts[2]);
    int s = int.Parse(parts[3]);
    graph[u].Add((v, w, s));
    graph[v].Add((u, w, s));
}
for (int i = 0; i < size; i++)
    graph[i].TrimExcess();
#endregion
Console.Write("Start:");
int start = -1;
while (size <= start || start < 0)
    int.TryParse(Console.ReadLine(), out start);
#region find costs
#region distance
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
        foreach (var (v, w, _) in graph[current])
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
#region time  
float[] times = new float[size];
for (int i = 0; i < size; i++)
    times[i] = float.PositiveInfinity;
{
    BitArray visited = new BitArray(size);
    times[start] = 0;
    int current = start;
    while (current != -1)
    {
        visited[current] = true;
        float min = int.MaxValue;
        int minIndex = -1;
        foreach (var (v, w, s) in graph[current])
            times[v] = Math.Min(times[v], times[current] + (float)w/s);
        for (int i = 0; i < size; i++)
            if (times[i] < min && !visited[i])
            {
                min = times[i];
                minIndex = i;
            }
        current = minIndex;
    }
}
#endregion
#endregion
while (true)
{
    Console.Write("End:");
    int end = -1;
    while (size <= end || end < 0)
        int.TryParse(Console.ReadLine(), out end);
    #region find path
    #region distance
    List<int> pathDistacne = new List<int>();
    {
        int current = end;
        int min = int.MaxValue;
        pathDistacne.Add(current);
        while (current != start)
        {
            int minIndex = -1;
            foreach (var (v, _, _) in graph[current])
                if (costs[v] <= min)
                {
                    min = costs[v];
                    minIndex = v;
                }
            current = minIndex;
            pathDistacne.Add(current);
        }
    }
    #endregion
    #region time
    List<int> pathTime = new List<int>();
    {
        int current = end;
        float min = float.PositiveInfinity;
        pathTime.Add(current);
        while (current != start)
        {
            int minIndex = -1;
            foreach (var (v, _, _) in graph[current])
                if (times[v] <= min)
                {
                    min = times[v];
                    minIndex = v;
                }
            current = minIndex;
            pathTime.Add(current);
        }
    }
    #endregion
    pathDistacne.Reverse();
    pathTime.Reverse();
    Console.WriteLine(String.Join(",", costs));
    Console.WriteLine(String.Join(",", times));
    Console.WriteLine($"Shortest Path: {String.Join(",", pathDistacne)} distance: {costs[end]}\nQuickest path: {String.Join(",", pathTime)} time :{times[end]}");
    #endregion
}
