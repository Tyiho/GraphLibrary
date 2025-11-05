using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Interfaces
{
    public interface IGraph<T> : IEquatable<IGraph<T>> where T : notnull
    {
        HashSet<T> Vertices { get; }
        HashSet<IEdge<T>> Edges { get; }
        void AddVertex(T vertex);
        void AddVertices(IEnumerable<T> vertices);
        void AddEdge(IEdge<T> edge);
        void AddEdge(T vertex1, T vertex2);
        void AddEdges(IEnumerable<IEdge<T>> edges);
        void AddGraph(IGraph<T> graph);

        public new bool Equals(IGraph<T>? other);
    }
}
