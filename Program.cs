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
int[] distances = new int[size];
int[] distanceParents = new int[size];
for (int i = 0; i < size; i++)
{
    distances[i] = int.MaxValue;
    distanceParents[i] = -1;
}
{
    BitArray visited = new BitArray(size);
    distances[start] = 0;
    int current = start;
    while (current != -1)
    {
        visited[current] = true;
        int min = int.MaxValue;
        int minIndex = -1;
        foreach (var (v, w, _) in graph[current])
        {
            if (distances[current] + w < distances[v])
            {
                distances[v] = distances[current] + w;
                distanceParents[v] = current;
            }
        }
        for (int i = 0; i < size; i++)
            if (distances[i] < min && !visited[i])
            {
                min = distances[i];
                minIndex = i;
            }
        current = minIndex;
    }
}
#endregion
#region time  
float[] times = new float[size];
int[] timeParents = new int[size];
for (int i = 0; i < size; i++)
{
    times[i] = float.PositiveInfinity;
    timeParents[i] = -1;
}
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
            if (times[current] + (float)w / s < times[v])
            {
                times[v] = times[current] + (float)w / s;
                timeParents[v] = current;
            }
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
        pathDistacne.Add(current);
        while (current != start)
        {
            current = distanceParents[current];
            pathDistacne.Add(current);
        }
    }
    #endregion
    #region time
    List<int> pathTime = new List<int>();
    {
        int current = end;
        pathTime.Add(current);
        while (current != start)
        {
            current = timeParents[current];
            pathTime.Add(current);
        }
    }
    #endregion
    pathDistacne.Reverse();
    pathTime.Reverse();
    Console.WriteLine($"Shortest Path: {String.Join(",", pathDistacne)} distance: {distances[end]}\nQuickest path: {String.Join(",", pathTime)} time :{times[end]}");
    #endregion
}
