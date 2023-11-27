using adventofcode.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2022
{
    public class DaySixteen : DayBase<int>
    {
        Dictionary<int, Valve> _valves = new Dictionary<int, Valve>();
        LinkedList<int>[] _valveGraph;
        private List<int>[] _valveList;
        private LinkedList<(int, int)>[] _valveWeightGraph;

        public DaySixteen() : base("day_16_ex.txt")
        {

        }

        public DaySixteen(string fileName) : base(fileName)
        {

        }

        private void ParseInput()
        {
            for(int i = 0; i < _input.Count; i++)
            {
                RegexHelper.Match(_input[i], @"^Valve ([A-Z]+) has flow rate=(\d+); tunnel(s*) lead(s*) to valve(s*) ([A-Z,\s]+)*$",
                    out string valveName, out int flowRate, out string _, out string _, out string _, out string connectedValveList, false);

                //_valves.TryAdd(i, new Valve(valveName, flowRate, new List<string>(connectedValveList.Split(", "))));
            }

            _valveGraph = new LinkedList<int>[_valves.Count];
            for (int i = 0; i < _valves.Count; i++)
                _valveGraph[i] = new LinkedList<int>();

            _valveList = new List<int>[_valves.Count];
            for (int i = 0; i < _valves.Count; i++)
                _valveList[i] = new List<int>();

            _valveWeightGraph = new LinkedList<(int, int)>[_valves.Count];
            for (int i = 0; i < _valves.Count; i++)
                _valveWeightGraph[i] = new LinkedList<(int, int)>();


            foreach (var valve in _valves)
            {
                foreach(var nextValve in valve.Value.ConnectedValveNames)
                {
                    var currentValve = _valves.First(x => x.Value.Name == nextValve);

                    _valveGraph[valve.Key].AddLast(currentValve.Key);
                    _valveList[valve.Key].Add(currentValve.Key);
                    _valveWeightGraph[valve.Key].AddLast((currentValve.Key, currentValve.Value.FlowRate));
                }
            }           
        }

        private void PrintGraph()
        {
            for (int i = 0; i < _valveGraph.Length; i++)
            {
                Console.WriteLine("\nAdjacency list of vertex " + i);
                Console.Write("head");

                foreach (var item in _valveGraph[i])
                {
                    Console.Write(" -> " + item);
                }
                Console.WriteLine();
            }
        }

        private void DFSUtil(int v, bool[] visited)
        {
            // Mark the current node as visited
            // and print it
            visited[v] = true;
            Console.Write(v + " ");

            // Recur for all the vertices
            // adjacent to this vertex
            List<int> vList = _valveList[v];
            foreach (var n in vList)
            {
                if (!visited[n])
                    DFSUtil(n, visited);
            }
        }

        // The function to do DFS traversal.
        // It uses recursive DFSUtil()
        private void DFS(int v)
        {
            // Mark all the vertices as not visited
            // (set as false by default in c#)
            bool[] visited = new bool[_valves.Count];

            // Call the recursive helper function
            // to print DFS traversal
            DFSUtil(v, visited);
        }


        private void BFS(int s)
        {
            int minutes = 0;
            int pressure = 0;

            bool[] visited = new bool[_valves.Count];
            for (int i = 0; i < _valves.Count; i++)
            {
                visited[i] = false;
            }

            LinkedList<int> queue = new LinkedList<int>();

            visited[s] = true;            
            
            queue.AddLast(s);
            

            while(queue.Any() && minutes <= 30)
            {
                s = queue.First();
                Console.Write(s + " ");
                queue.RemoveFirst();

                LinkedList<int> list = _valveGraph[s];

                foreach(var val in list)
                {
                    if (!visited[val])
                    {
                        visited[val] = true;
                        queue.AddLast(val);
                    }
                }
            }
        }

        private int minDistance(int[] dist, bool[] sptSet)
        {
            // Initialize min value
            int min = int.MaxValue, min_index = -1;

            for (int v = 0; v < _valves.Count; v++)
                if (sptSet[v] == false && dist[v] <= min)
                {
                    min = dist[v];
                    min_index = v;
                }

            return min_index;
        }

        private void printSolution(int[] dist)
        {
            Console.Write("Vertex \t\t Distance "
                          + "from Source\n");
            for (int i = 0; i < _valves.Count; i++)
                Console.Write(i + " \t\t " + dist[i] + "\n");
        }

        public static int[] dijkstra(int V, List<List<Valve>> graph, int src)
        {
            int[] distance = new int[V];
            for (int i = 0; i < V; i++)
                distance[i] = Int32.MaxValue;
            distance[src] = 0;

            SortedSet<Valve> pq = new SortedSet<Valve>();
            //pq.Add(new Valve(src, "", 0));

            while (pq.Count > 0)
            {
                Valve current = pq.First();
                pq.Remove(current);

                foreach (
                  Valve n in graph[current.Vertex])
                {
                    if ((distance[current.Vertex] + n.FlowRate) < distance[n.Vertex])
                    {
                        distance[n.Vertex] = n.FlowRate + distance[current.Vertex];
                        //pq.Add(new Valve(n.Vertex), distance[n.Vertex]));
                    }
                }
            }
            // If you want to calculate distance from source to
            // a particular target, you can return
            // distance[target]
            return distance;
        }

        public override int Problem1()
        {
            ParseInput();
            //PrintGraph();

            int pressure = 0;

            //foreach(var valve in _valves)
            //{
            //    BFS(valve.Key);
            //}

            //dijkstra(3);

            //BFS(1);
            //DFS(1);
            //Console.WriteLine();
            ////BFS(2);
            //DFS(2);
            //Console.WriteLine();
            ////BFS(3);
            //DFS(3);
            //Console.WriteLine();
            ////BFS(4);
            //DFS(4);
            //Console.WriteLine();

            return pressure;
        }

        public override int Problem2()
        {
            throw new NotImplementedException();
        }
    }

    public class Valve : IComparable<Valve>
    {
        public Valve(int vertex, string name, int flowRate, List<string> connectedValveNames)
        {
            Vertex = vertex;
            Name = name;
            FlowRate = flowRate;
            ConnectedValveNames = connectedValveNames;
        }

        public int Vertex { get; }
        public string Name { get; }
        public int FlowRate { get; }
        public List<string> ConnectedValveNames { get; }

        public int CompareTo(Valve? other)
        {
            return FlowRate - other.FlowRate;
        }
    }
}
