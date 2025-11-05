using GraphLibrary.Interfaces;

namespace GraphLibrary.Structs
{
    public struct Edge<T> : IEdge<T>, IEquatable<Edge<T>> where T : notnull
    {
        public T Vertex1 { get; }
        public T Vertex2 { get; }

        public Edge(T vertex1, T vertex2)
        {
            if (vertex1.Equals(vertex2)) throw new ArgumentException("An edge cannot connect a vertex to itself.");
            Vertex1 = vertex1;
            Vertex2 = vertex2;
        }
        public Edge((T, T) vertices) : this(vertices.Item1, vertices.Item2) { }



        public IEnumerable<T> AsEnumerable() {
            yield return Vertex1;
            yield return Vertex2;
        }
        public void Deconstruct(out T vertex1, out T vertex2)
        {
            vertex1 = Vertex1;
            vertex2 = Vertex2;
        }
        public (T, T) ToTuple() => (Vertex1, Vertex2);
        public bool Contains(T vertex) => Vertex1.Equals(vertex) || Vertex2.Equals(vertex);

        public bool Equals(Edge<T> other) => Vertex1.Equals(other.Vertex1) && Vertex2.Equals(other.Vertex2) || Vertex1.Equals(other.Vertex2) && Vertex2.Equals(other.Vertex1);
        public bool Equals(IEdge<T>? other) => other is Edge<T> edge && Equals(edge);
        public override bool Equals(object? obj) => obj is Edge<T> other && Equals(other);

        public override int GetHashCode()
        {
            int hash1 = Vertex1.GetHashCode();
            int hash2 = Vertex2.GetHashCode();
            return hash1 ^ hash2;
        }

        public T? GetConnectedVertex(T vertex) {
            if (vertex.Equals(Vertex1)) { 
                return Vertex2;
            }
            if (vertex.Equals(Vertex2)) {
                return Vertex1;
            }
            return default;
        }

        public bool IsVertex1ReachableFrom(T vertex) => Contains(vertex);
        public bool IsVertex2ReachableFrom(T vertex) => Contains(vertex);

        public override string ToString() => $"({Vertex1} <-> {Vertex2})";
    }
}
