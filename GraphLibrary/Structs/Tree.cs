using GraphLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Structs
{
    public struct Tree<T> : ITree<T>, IEquatable<Tree<T>> where T : notnull
    {
        public T Root { get; }
        public HashSet<T> Vertices { get; private set; }
        public HashSet<IEdge<T>> Edges { get; private set; }
        public Tree(T root)
        {
            Root = root;
            Vertices = new HashSet<T> { root };
            Edges = new HashSet<IEdge<T>>();
        }
        public Tree(T root, IEnumerable<IEdge<T>> edges)
        {
            Root = root;
            Vertices = new HashSet<T> { root };
            Edges = new HashSet<IEdge<T>>();
            foreach (var edge in edges)
            {
                if (edge is DirectionalEdge<T> && Vertices.Contains(edge.Vertex1) && !Vertices.Contains(edge.Vertex2)) { 
                    Edges.Add(edge);
                    Vertices.Add(edge.Vertex2);
                }
            }
        }

        public bool Equals(ITree<T>? other)
        {
            if (other is null) return false;
            if (!Root.Equals(other.Root)) return false;
            if (!Vertices.SetEquals(other.Vertices)) return false;
            if (!Edges.SetEquals(other.Edges)) return false;
            return true;
        }
        public override bool Equals(object? obj) => obj is ITree<T> other && Equals(other);
        public override int GetHashCode()
        {
            int hash = Root.GetHashCode();
            foreach (var vertex in Vertices)
            {
                hash ^= vertex.GetHashCode();
            }
            foreach (var edge in Edges)
            {
                hash ^= edge.GetHashCode();
            }
            return hash;
        }

        public void AddVertex(T vertex)
        {
            if(Vertices.Add(vertex))
            {
                // Ensure the new vertex is connected to the root by some part of the tree
                if (Edges.Where(e => e.Vertex2.Equals(vertex)).Count() < 1) {
                    var edge = new DirectionalEdge<T>(Root, vertex);
                    Edges.Add(edge);
                }
            }
        }

        public void AddVertices(IEnumerable<T> vertices)
        {
            foreach (var vertex in vertices)
            {
                AddVertex(vertex);
            }
        }

        public void AddEdge(IEdge<T> edge)
        {
            if (edge is DirectionalEdge<T> directionalEdge)
            {
                if (Vertices.Contains(directionalEdge.Vertex1) && !Vertices.Contains(directionalEdge.Vertex2))
                {
                    Edges.Add(edge);
                    Vertices.Add(directionalEdge.Vertex2);
                }
            }
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
            if (graph is Tree<T>) {
                AddVertex(((Tree<T>)graph).Root);
            }
            AddEdges(graph.Edges);
            AddVertices(graph.Vertices);
        }

        public bool Equals(Tree<T> other) => Vertices.SetEquals(other.Vertices) && Edges.SetEquals(other.Edges) && Root.Equals(other.Root);
        public bool Equals(IGraph<T>? other) => other is ITree<T> tree && Equals(tree);


        public IEnumerable<IEdge<T>> GetLowerBranches(IEdge<T> edge) { 
            return Edges.Where(e => e.Vertex1.Equals(edge.Vertex2));
        }

        public void RemoveEdges(IEnumerable<IEdge<T>> edges)
        {
            foreach (var edge in edges)
            {
                Edges.Remove(edge);
                RemoveEdges(GetLowerBranches(edge)); // Recursively remove lower branch
                RemoveVertex(edge.Vertex2); //remove the vertex at the end of the edge
            }
        }

        public void RemoveEdge(IEdge<T> edge)
        {
            Edges.Remove(edge);
            RemoveEdges(GetLowerBranches(edge));
            RemoveVertex(edge.Vertex2);
        }

        public void RemoveVertex(T vertex)
        {
            if(!Vertices.Contains(vertex)) return; //added specifically to avoid extra querying on edges
            if (vertex.Equals(Root)) throw new InvalidOperationException("Cannot remove the root vertex of the tree.");
            var edgesToRemove = Edges.Where(e => e.Vertex1.Equals(vertex) || e.Vertex2.Equals(vertex)).ToList();
            foreach (var edge in edgesToRemove)
            {
                RemoveEdge(edge);
            }
            Vertices.Remove(vertex);
        }

    }
}
