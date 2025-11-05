using GraphLibrary.Interfaces;

namespace GraphLibrary.Structs
{
    public struct DirectionalGraph<T> : IGraph<T>, IEquatable<DirectionalGraph<T>> where T : notnull
    {
        public HashSet<T> Vertices { get; private set; }
        public HashSet<IEdge<T>> Edges { get; private set; }

        public DirectionalGraph()
        {
            Vertices = new HashSet<T>();
            Edges = new HashSet<IEdge<T>>();
        }
        public DirectionalGraph(DirectionalGraph<T> graph)
        {
            Vertices = graph.Vertices.ToHashSet();
            Edges = graph.Edges.ToHashSet();
        }


        public void AddVertex(T vertex)
        {
            Vertices.Add(vertex);
        }

        public void AddVertices(IEnumerable<T> vertices)
        {
            foreach (var vertex in vertices)
            {
                Vertices.Add(vertex);
            }
        }


        public void AddEdge(IEdge<T> edge)
        {
            if (edge is not DirectionalEdge<T> directionalEdge)
            {
                throw new ArgumentException("Edge must be of type DirectionalEdge<T>");
            }
            Edges.Add(edge);
            Vertices.Add(edge.Vertex1);
            Vertices.Add(edge.Vertex2);
        }

        public void AddEdge(T vertex1, T vertex2)
        {
            var edge = new DirectionalEdge<T>(vertex1, vertex2);
            AddEdge(edge);
        }

        public void AddEdges(IEnumerable<IEdge<T>> edges)
        {
            foreach (var edge in edges)
            {
                AddEdge(edge);
            }
        }

        public void AddGraph(IGraph<T> graph)
        {
            AddVertices(graph.Vertices);
            AddEdges(graph.Edges);
        }

        public void ConnectGraph(DirectionalGraph<T> graph, T vertex1, T vertext2)
        {
            if (!ContainsVertex(vertex1) && !ContainsVertex(vertext2))
            {
                throw new ArgumentException("One vertex must be present in the existing graph to connect graphs.");
            }
            if (!graph.ContainsVertex(vertex1) && !graph.ContainsVertex(vertext2))
            {
                throw new ArgumentException("One vertex must be present in the new graph to connect graphs.");
            }

            AddGraph(graph);
            AddEdge(vertex1, vertext2);
        }

        public IEnumerable<IEdge<T>> GetIncidentEdges(T vertex)
        {
            return Edges.Where(e => e.Contains(vertex)).AsEnumerable();
        }

        public IEnumerable<T> GetNeighbors(T vertex)
        {
            var neighbors = new HashSet<T>();
            foreach (var edge in GetIncidentEdges(vertex))
            {
                if (edge.Vertex1.Equals(vertex))
                {
                    neighbors.Add(edge.Vertex2);
                }
                else
                {
                    neighbors.Add(edge.Vertex1);
                }
            }
            return neighbors.AsEnumerable();
        }

        public int Degree(T vertex)
        {
            return GetIncidentEdges(vertex).Count();
        }

        public bool IsEndVertex(T vertex)
        {
            return Degree(vertex) == 1;
        }

        public bool IsIsolatedVertex(T vertex)
        {
            return Degree(vertex) == 0;
        }

        public bool IsUniversalVertex(T vertex)
        {
            return Degree(vertex) == Vertices.Count - 1;
        }

        public bool ContainsVertex(T vertex)
        {
            return Vertices.Contains(vertex);
        }

        public bool ContainsEdge(T vertex1, T vertex2)
        {
            var edge = new DirectionalEdge<T>(vertex1, vertex2);
            return Edges.Contains(edge);
        }
        public bool ContainsEdge(IEdge<T> edge)
        {
            return Edges.Contains(edge);
        }
        public bool Contains(IEdge<T> edge)
        {
            return ContainsEdge(edge);
        }
        public bool Contains(T vertex)
        {
            return ContainsVertex(vertex);
        }
        public bool Contains(IGraph<T> graph)
        {
            return ContainsGraph(graph);
        }

        public bool IsSubGraphOf(IGraph<T> graph)
        {
            return Vertices.IsSubsetOf(graph.Vertices) && Edges.IsSubsetOf(graph.Edges);
        }

        public bool IsSuperGraphOf(IGraph<T> graph)
        {
            return Vertices.IsSupersetOf(graph.Vertices) && Edges.IsSupersetOf(graph.Edges);
        }

        public bool ContainsGraph(IGraph<T> graph)
        {
            return IsSuperGraphOf(graph);
        }

        public void RemoveEdge(T vertex1, T vertex2)
        {
            var edge = new DirectionalEdge<T>(vertex1, vertex2);
            Edges.Remove(edge);
        }

        public void RemoveEdge(IEdge<T> edge)
        {
            Edges.Remove(edge);
        }

        public void RemoveVertex(T vertex)
        {
            Vertices.Remove(vertex);
            Edges.RemoveWhere(e => e.Contains(vertex));
        }

        public void RemoveVertices(IEnumerable<T> vertices)
        {
            foreach (var vertex in vertices)
            {
                RemoveVertex(vertex);
            }
        }
        public void RemoveEdges(IEnumerable<IEdge<T>> edges)
        {
            foreach (var edge in edges)
            {
                RemoveEdge(edge);
            }
        }

        public void RemoveGraph(IGraph<T> graph)
        {
            RemoveVertices(graph.Vertices);
            RemoveEdges(graph.Edges);
        }

        public void Clear()
        {
            Vertices.Clear();
            Edges.Clear();
        }
        public bool isEmpty()
        {
            return Vertices.Count == 0 && Edges.Count == 0;
        }

        public DirectionalGraph<T> Clone()
        {
            return new DirectionalGraph<T>(this);
        }

        public bool Equals(DirectionalGraph<T> other) => Vertices.SetEquals(other.Vertices) && Edges.SetEquals(other.Edges);

        public override bool Equals(object? obj) => obj is Graph<T> other && Equals(other);

        public bool Equals(IGraph<T>? other) => other is DirectionalGraph<T> && Equals((DirectionalGraph<T>)other);

        public override int GetHashCode()
        {
            int hash = 17;
            foreach (var vertex in Vertices.OrderBy(v => v.GetHashCode()))
            {
                hash = hash * 31 + vertex.GetHashCode();
            }
            foreach (var edge in Edges.OrderBy(e => e.GetHashCode()))
            {
                hash = hash * 31 + edge.GetHashCode();
            }
            return hash;
        }


        public IEnumerable<IEdge<T>> GetIncidentArcs(T vertex)
        {
            return Edges.Where(e => e.Vertex1.Equals(vertex)).AsEnumerable();
        }

        public IEnumerable<T> GetOutNeighbors(T vertex)
        {
            var neighbors = new HashSet<T>();
            foreach (var edge in GetIncidentArcs(vertex))
            {
                neighbors.Add(edge.Vertex2);
            }
            return neighbors.AsEnumerable();
        }
    }
}
