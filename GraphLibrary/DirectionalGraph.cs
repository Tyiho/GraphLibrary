namespace GraphLibrary
{
    public struct DirectionalGraph<T> : IEquatable<DirectionalGraph<T>> where T : notnull
    {
        public HashSet<T> Vertices { get; private set; }
        public HashSet<DirectionalEdge<T>> Edges { get; private set; }

        public DirectionalGraph()
        {
            this.Vertices = new HashSet<T>();
            this.Edges = new HashSet<DirectionalEdge<T>>();
        }
        public DirectionalGraph(DirectionalGraph<T> graph)
        {
            this.Vertices = graph.Vertices.ToHashSet();
            this.Edges = graph.Edges.ToHashSet();
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


        public void AddEdge(DirectionalEdge<T> edge)
        {
            Edges.Add(edge);
            Vertices.Add(edge.Vertex1);
            Vertices.Add(edge.Vertex2);
        }

        public void AddEdge(T vertex1, T vertex2)
        {
            var edge = new DirectionalEdge<T>(vertex1, vertex2);
            Edges.Add(edge);
            Vertices.Add(vertex1);
            Vertices.Add(vertex2);
        }

        public void AddEdges(IEnumerable<DirectionalEdge<T>> edges)
        {
            foreach (var edge in edges)
            {
                Edges.Add(edge);
                Vertices.Add(edge.Vertex1);
                Vertices.Add(edge.Vertex2);
            }
        }

        public void AddGraph(DirectionalGraph<T> graph)
        {
            this.AddVertices(graph.Vertices);
            this.AddEdges(graph.Edges);
        }

        public void ConnectGraph(DirectionalGraph<T> graph, T vertex1, T vertext2)
        {
            if (!this.ContainsVertex(vertex1) && !this.ContainsVertex(vertext2))
            {
                throw new ArgumentException("One vertex must be present in the existing graph to connect graphs.");
            }
            if (!graph.ContainsVertex(vertex1) && !graph.ContainsVertex(vertext2))
            {
                throw new ArgumentException("One vertex must be present in the new graph to connect graphs.");
            }

            this.AddGraph(graph);
            this.AddEdge(vertex1, vertext2);
        }

        public IEnumerable<DirectionalEdge<T>> GetIncidentEdges(T vertex)
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
        public bool ContainsEdge(DirectionalEdge<T> edge)
        {
            return Edges.Contains(edge);
        }
        public bool Contains(DirectionalEdge<T> edge)
        {
            return ContainsEdge(edge);
        }
        public bool Contains(T vertex)
        {
            return ContainsVertex(vertex);
        }
        public bool Contains(DirectionalGraph<T> graph)
        {
            return ContainsGraph(graph);
        }

        public bool IsSubGraphOf(DirectionalGraph<T> graph)
        {
            return Vertices.IsSubsetOf(graph.Vertices) && Edges.IsSubsetOf(graph.Edges);
        }

        public bool IsSuperGraphOf(DirectionalGraph<T> graph)
        {
            return Vertices.IsSupersetOf(graph.Vertices) && Edges.IsSupersetOf(graph.Edges);
        }

        public bool ContainsGraph(DirectionalGraph<T> graph)
        {
            return IsSuperGraphOf(graph);
        }

        public void RemoveEdge(T vertex1, T vertex2)
        {
            var edge = new DirectionalEdge<T>(vertex1, vertex2);
            Edges.Remove(edge);
        }

        public void RemoveEdge(DirectionalEdge<T> edge)
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
        public void RemoveEdges(IEnumerable<DirectionalEdge<T>> edges)
        {
            foreach (var edge in edges)
            {
                RemoveEdge(edge);
            }
        }

        public void RemoveGraph(DirectionalGraph<T> graph)
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


        public IEnumerable<DirectionalEdge<T>> GetIncidentArcs(T vertex)
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


        /* maybe won't work as intended for directed graphs, 
         * but here is an implementation of Bron-Kerbosch algorithm for finding cliques assuming neighbors must be directional
         */
        private IEnumerable<HashSet<T>> BronKerbosch(HashSet<T> R, HashSet<T> P, HashSet<T> X, ref List<HashSet<T>> cliques)
        {
            if (P.Count == 0 && X.Count == 0)
            {
                cliques.Add(R.ToHashSet());
            }
            foreach (var v in P.ToList())
            {
                var vSet = new HashSet<T> { v };
                var neighbors = GetOutNeighbors(v).ToHashSet();
                BronKerbosch(R.Union(vSet).ToHashSet(), P.Intersect(neighbors).ToHashSet(), X.Intersect(neighbors).ToHashSet(), ref cliques);
                P.Remove(v);
                X.Add(v);
            }
            return cliques;
        }

        public IEnumerable<HashSet<T>> GetCliques()
        {
            var cliques = new List<HashSet<T>>();
            BronKerbosch(new HashSet<T>(), this.Vertices, new HashSet<T>(), ref cliques);
            return cliques;
        }

        public HashSet<T> GetMaximalClique()
        {
            var cliques = GetCliques();
            HashSet<T> maxClique = new HashSet<T>();
            foreach (var clique in cliques)
            {
                if (clique.Count > maxClique.Count)
                {
                    maxClique = clique;
                }
            }
            return maxClique;
        }
    }
}
