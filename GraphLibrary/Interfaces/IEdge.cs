using GraphLibrary.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Interfaces
{
    public interface IEdge<T> : IEquatable<IEdge<T>> where T : notnull
    {
        public T Vertex1 { get; }
        public T Vertex2 { get; }

        public IEnumerable<T> AsEnumerable();

        public new bool Equals(IEdge<T>? other);

        public bool Contains(T vertex);
    }
}
