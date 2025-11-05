using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Interfaces
{
    public interface ITree<T> : IGraph<T>, IEquatable<ITree<T>> where T : notnull
    {
        T Root { get; }

        public new bool Equals(ITree<T>? other);
    }
}
