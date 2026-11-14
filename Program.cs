using System;
using System.IO;

#region get input
string filePath = "./input.txt";
string content = File.ReadAllText(filePath);
Console.WriteLine(content);
string[] lines = content.Split('\n');
int size = int.Parse(lines[0]);
List<List<(int, int)>> graph = new List<List<(int, int)>>(size);
for (int i = 0; i < size; i++)
    graph[i] = new List<(int, int)>(size);
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
