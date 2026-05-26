using GraphLibrary.Interfaces;
using GraphLibrary.Structs;
using System.Diagnostics;

namespace GraphLibraryUnitTest
{
    [TestClass]
    public sealed class DirectionalGraphUnitTest
    {
        [TestMethod]
        public void TestGraphCreation()
        {
            DirectionalGraph<int> graph = new DirectionalGraph<int>();
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 1);
            Assert.AreEqual(3, graph.Vertices.Count);
            Assert.AreEqual(3, graph.Edges.Count);
        }

        [TestMethod]
        public void TestAddVertex()
        {
            DirectionalGraph<int> graph = new DirectionalGraph<int>();
            Assert.IsTrue(graph.Vertices.Count == 0);
            graph.AddVertex(1);
            Assert.IsTrue(graph.Vertices.Count == 1);
            Assert.IsTrue(graph.Vertices.Contains(1));
            Assert.IsFalse(graph.Vertices.Contains(2));
            graph.AddVertex(2);
            Assert.IsTrue(graph.Vertices.Contains(2));
            Assert.IsTrue(graph.Vertices.Count == 2);
        }

        [TestMethod]
        public void TestAddEdge()
        {
            DirectionalGraph<int> graph = new DirectionalGraph<int>();
            Assert.IsTrue(graph.Edges.Count == 0);
            graph.AddEdge(1, 2);
            Assert.IsTrue(graph.Edges.Count == 1);
            Assert.IsTrue(graph.Edges.Contains(new DirectionalEdge<int>(1, 2)));
            Assert.IsFalse(graph.Edges.Contains(new DirectionalEdge<int>(2, 3)));
            graph.AddEdge(2, 3);
            Assert.IsTrue(graph.Edges.Contains(new DirectionalEdge<int>(2, 3)));
            Assert.IsTrue(graph.Edges.Count == 2);
        }

        [TestMethod]
        public void TestAddVertices()
        {
            DirectionalGraph<int> graph = new DirectionalGraph<int>();
            List<int> verticesToAdd = new List<int>() { 1, 2, 3, 4, 5 };
            graph.AddVertices(verticesToAdd);
            Assert.AreEqual(5, graph.Vertices.Count);
            foreach (var vertex in verticesToAdd)
            {
                Assert.IsTrue(graph.Vertices.Contains(vertex));
            }
        }

        [TestMethod]
        public void TestAddEdges()
        {
            DirectionalGraph<int> graph = new DirectionalGraph<int>();
            List<DirectionalEdge<int>> edgesToAdd = new List<DirectionalEdge<int>>()
            {
                new DirectionalEdge<int>(1, 2),
                new DirectionalEdge<int>(2, 3),
                new DirectionalEdge<int>(3, 1)
            };
            List<IEdge<int>> edgesToAdd1 = edgesToAdd.Cast<IEdge<int>>().ToList();
            graph.AddEdges(edgesToAdd1);
            Assert.AreEqual(3, graph.Edges.Count);
            foreach (var edge in edgesToAdd)
            {
                Assert.IsTrue(graph.Edges.Contains(edge));
            }
        }

        [TestMethod]
        public void TestAddGraph()
        {
            DirectionalGraph<int> graph1 = new DirectionalGraph<int>();
            graph1.AddEdge(1, 2);
            graph1.AddEdge(2, 3);
            DirectionalGraph<int> graph2 = new DirectionalGraph<int>();
            graph2.AddEdge(3, 4);
            graph2.AddEdge(4, 5);
            graph2.AddEdge(1, 2); //duplicate edge to test that it is not added twice

            graph1.AddGraph(graph2);
            Assert.AreEqual(5, graph1.Vertices.Count);
            Assert.AreEqual(4, graph1.Edges.Count);
            for (int i = 1; i <= 5; i++)
            {
                Assert.IsTrue(graph1.Vertices.Contains(i));
            }
            Assert.IsTrue(graph1.Edges.Contains(new DirectionalEdge<int>(1, 2)));
            Assert.IsTrue(graph1.Edges.Contains(new DirectionalEdge<int>(2, 3)));
            Assert.IsTrue(graph1.Edges.Contains(new DirectionalEdge<int>(3, 4)));
            Assert.IsTrue(graph1.Edges.Contains(new DirectionalEdge<int>(4, 5)));
        }

        [TestMethod]
        public void TestConnectGraph()
        {
            DirectionalGraph<int> graph1 = new DirectionalGraph<int>();
            graph1.AddEdge(1, 2);
            graph1.AddEdge(2, 3);
            DirectionalGraph<int> graph2 = new DirectionalGraph<int>();
            graph2.AddEdge(4, 5);
            graph2.AddEdge(5, 6);

            Assert.ThrowsException<ArgumentException>(() => graph1.ConnectGraph(graph2, 1, 67));
            Assert.ThrowsException<ArgumentException>(() => graph1.ConnectGraph(graph2, 7, 1));

            graph1.ConnectGraph(graph2, 3, 4);

            Assert.AreEqual(6, graph1.Vertices.Count);
            Assert.AreEqual(5, graph1.Edges.Count);
            Assert.IsTrue(graph1.Edges.Contains(new DirectionalEdge<int>(3, 4)));
        }


        [TestMethod]
        public void TestContainsVertex()
        {
            DirectionalGraph<int> graph = new DirectionalGraph<int>();
            graph.AddEdge(1, 2);
            Assert.IsTrue(graph.ContainsVertex(1));
            Assert.IsTrue(graph.ContainsVertex(2));
            Assert.IsFalse(graph.ContainsVertex(3));
        }

        [TestMethod]
        public void TestContainsEdge()
        {
            DirectionalGraph<int> graph = new DirectionalGraph<int>();
            graph.AddEdge(1, 2);
            Assert.IsTrue(graph.ContainsEdge(1, 2));
            Assert.IsTrue(graph.ContainsEdge(new DirectionalEdge<int>(1, 2)));
            Assert.IsFalse(graph.ContainsEdge(2, 3));
            Assert.IsFalse(graph.ContainsEdge(new DirectionalEdge<int>(2, 3)));
        }

        [TestMethod]
        public void TestSubGraph()
        {
            DirectionalGraph<int> graph1 = new DirectionalGraph<int>();
            graph1.AddEdge(1, 2);
            graph1.AddEdge(2, 3);
            DirectionalGraph<int> graph2 = new DirectionalGraph<int>();
            graph2.AddEdge(1, 2);
            Assert.IsTrue(graph2.IsSubGraphOf(graph1));
            graph2.AddEdge(3, 4);
            Assert.IsFalse(graph2.IsSubGraphOf(graph1));
        }

        [TestMethod]
        public void TestSuperGraph()
        {
            DirectionalGraph<int> graph1 = new DirectionalGraph<int>();
            graph1.AddEdge(1, 2);

            DirectionalGraph<int> graph2 = new DirectionalGraph<int>();
            graph2.AddEdge(1, 2);
            graph2.AddEdge(2, 3);

            Assert.IsTrue(graph2.IsSuperGraphOf(graph1));
        }

        //same as IsSuperGraphOf
        [TestMethod]
        public void TestContainsGraph()
        {
            DirectionalGraph<int> graph1 = new DirectionalGraph<int>();
            graph1.AddEdge(1, 2);
            graph1.AddEdge(2, 3);
            DirectionalGraph<int> graph2 = new DirectionalGraph<int>();
            graph2.AddEdge(1, 2);
            Assert.IsTrue(graph1.ContainsGraph(graph2));
            graph2.AddEdge(3, 4);
            Assert.IsFalse(graph1.ContainsGraph(graph2));
        }


        //universal contains method, tests for vertices, edges, and subgraphs
        [TestMethod]
        public void TestContains()
        {
            DirectionalGraph<int> graph = new DirectionalGraph<int>();
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 1);

            Assert.IsTrue(graph.Contains(1));
            Assert.IsTrue(graph.Contains(2));
            Assert.IsTrue(graph.Contains(3));
            Assert.IsFalse(graph.Contains(4));
            Assert.IsTrue(graph.Contains(new DirectionalEdge<int>(1, 2)));
            Assert.IsTrue(graph.Contains(new DirectionalEdge<int>(2, 3)));
            Assert.IsTrue(graph.Contains(new DirectionalEdge<int>(3, 1)));
            Assert.IsFalse(graph.Contains(new DirectionalEdge<int>(1, 4)));

            DirectionalGraph<int> subGraph = new DirectionalGraph<int>();
            subGraph.AddEdge(1, 2);
            Assert.IsTrue(graph.Contains(subGraph));
            subGraph.AddEdge(2, 4);
            Assert.IsFalse(graph.Contains(subGraph));
        }

        [TestMethod]
        public void TestDegree()
        {
            DirectionalGraph<int> graph = new DirectionalGraph<int>();
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 1);
            Assert.AreEqual(2, graph.Degree(1));
            Assert.AreEqual(2, graph.Degree(2));
            Assert.AreEqual(2, graph.Degree(3));
        }

        [TestMethod]
        public void TestNeighbors()
        {
            DirectionalGraph<int> graph = new DirectionalGraph<int>();
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 1);
            var neighborsOf1 = graph.GetNeighbors(1).ToHashSet();
            Assert.IsTrue(neighborsOf1.SetEquals(new HashSet<int> { 2, 3 }));
            var neighborsOf2 = graph.GetNeighbors(2).ToHashSet();
            Assert.IsTrue(neighborsOf2.SetEquals(new HashSet<int> { 1, 3 }));
            var neighborsOf3 = graph.GetNeighbors(3).ToHashSet();
            Assert.IsTrue(neighborsOf3.SetEquals(new HashSet<int> { 1, 2 }));
        }

        [TestMethod]
        public void TestRemoveVertex()
        {
            DirectionalGraph<int> graph = new DirectionalGraph<int>();
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            Assert.IsTrue(graph.ContainsVertex(2));
            graph.RemoveVertex(2);
            Assert.IsFalse(graph.ContainsVertex(2));
            Assert.IsFalse(graph.ContainsEdge(1, 2));
            Assert.IsFalse(graph.ContainsEdge(2, 3));
        }

        [TestMethod]
        public void TestRemoveEdge()
        {
            DirectionalGraph<int> graph = new DirectionalGraph<int>();
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            Assert.IsTrue(graph.ContainsEdge(1, 2));
            graph.RemoveEdge(1, 2);
            Assert.IsFalse(graph.ContainsEdge(1, 2));
            Assert.IsTrue(graph.ContainsVertex(1));
            Assert.IsTrue(graph.ContainsVertex(2));


            graph.AddEdge(1, 2);

            //attempt to remove edge in opposite direction: should not remove anything
            graph.RemoveEdge(new DirectionalEdge<int>(2, 1));


            Assert.IsTrue(graph.ContainsEdge(1, 2));
            Assert.IsTrue(graph.ContainsVertex(1));
            Assert.IsTrue(graph.ContainsVertex(2));
        }

        [TestMethod]
        public void TestRemoveVertices()
        {
            DirectionalGraph<int> graph = new DirectionalGraph<int>();
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);
            List<int> verticesToRemove = new List<int>() { 2, 3 };
            graph.RemoveVertices(verticesToRemove);
            Assert.IsFalse(graph.ContainsVertex(2));
            Assert.IsFalse(graph.ContainsVertex(3));
            Assert.IsFalse(graph.ContainsEdge(1, 2));
            Assert.IsFalse(graph.ContainsEdge(2, 3));
            Assert.IsTrue(graph.ContainsVertex(1));
            Assert.IsTrue(graph.ContainsVertex(4));
            Assert.IsFalse(graph.ContainsEdge(3, 4));

        }

        [TestMethod]
        public void TestRemoveEdges()
        {
            DirectionalGraph<int> graph = new DirectionalGraph<int>();
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);
            List<DirectionalEdge<int>> edgesToRemove = new List<DirectionalEdge<int>>()
            {
                new DirectionalEdge<int>(1, 2),
                new DirectionalEdge<int>(2, 3)
            };
            List<IEdge<int>> edgesToRemove1 = edgesToRemove.Cast<IEdge<int>>().ToList();
            graph.RemoveEdges(edgesToRemove1);
            Assert.IsFalse(graph.ContainsEdge(1, 2));
            Assert.IsFalse(graph.ContainsEdge(2, 3));
            Assert.IsTrue(graph.ContainsEdge(3, 4));
            Assert.IsTrue(graph.ContainsVertex(1));
            Assert.IsTrue(graph.ContainsVertex(2));
            Assert.IsTrue(graph.ContainsVertex(3));
            Assert.IsTrue(graph.ContainsVertex(4));
        }

        [TestMethod]
        public void TestRemoveGraph()
        {
            DirectionalGraph<int> graph1 = new DirectionalGraph<int>();
            graph1.AddEdge(1, 2);
            graph1.AddEdge(2, 3);
            graph1.AddEdge(3, 4);
            DirectionalGraph<int> graph2 = new DirectionalGraph<int>();
            graph2.AddEdge(2, 3);
            graph2.AddEdge(3, 4);

            graph1.RemoveGraph(graph2);
            Assert.IsTrue(graph1.ContainsVertex(1));
            Assert.IsFalse(graph1.ContainsVertex(2));
            Assert.IsFalse(graph1.ContainsVertex(3));
            Assert.IsFalse(graph1.ContainsVertex(4));
            Assert.IsFalse(graph1.ContainsEdge(1, 2));
            Assert.IsFalse(graph1.ContainsEdge(2, 3));
            Assert.IsFalse(graph1.ContainsEdge(3, 4));
        }

        [TestMethod]
        public void TestClear()
        {
            DirectionalGraph<int> graph = new DirectionalGraph<int>();
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.Clear();
            Assert.IsTrue(graph.IsEmpty());
            Assert.AreEqual(0, graph.Vertices.Count);
            Assert.AreEqual(0, graph.Edges.Count);
        }


        [TestMethod]
        public void TestEquals()
        {
            DirectionalGraph<int> graph1 = new DirectionalGraph<int>();
            graph1.AddEdge(1, 2);
            graph1.AddEdge(2, 3);
            graph1.AddEdge(3, 1);

            DirectionalGraph<int> graph2 = new DirectionalGraph<int>(graph1);

            Assert.AreEqual(graph1, graph2);

            graph2.AddEdge(4, 5);

            Assert.AreNotEqual(graph1, graph2);
        }

        [TestMethod]
        public void TestClone()
        {
            DirectionalGraph<int> graph1 = new DirectionalGraph<int>();
            graph1.AddEdge(1, 2);
            graph1.AddEdge(2, 3);
            graph1.AddEdge(3, 1);
            DirectionalGraph<int> graph2 = graph1.Clone();
            Assert.AreEqual(graph1, graph2);
            graph2.AddEdge(4, 5);
            Assert.AreNotEqual(graph1, graph2);
        }

        [TestMethod]
        public void TestIsEmpty()
        {
            DirectionalGraph<int> graph = new DirectionalGraph<int>();
            Assert.IsTrue(graph.IsEmpty());
            graph.AddVertex(1);
            Assert.IsFalse(graph.IsEmpty());
            graph.RemoveVertex(1);
            Assert.IsTrue(graph.IsEmpty());
            graph.AddEdge(1, 2);
            Assert.IsFalse(graph.IsEmpty());
        }

        [TestMethod]
        public void TestIsEndVertex()
        {
            DirectionalGraph<int> graph = new DirectionalGraph<int>();
            graph.AddEdge(1, 2);
            Assert.IsTrue(graph.IsEndVertex(1));
            Assert.IsTrue(graph.IsEndVertex(2));
            graph.AddEdge(2, 3);
            Assert.IsFalse(graph.IsEndVertex(2));
        }

        [TestMethod]
        public void TestIsIsolatedVertex()
        {
            DirectionalGraph<int> graph = new DirectionalGraph<int>();
            graph.AddVertex(1);
            Assert.IsTrue(graph.IsIsolatedVertex(1));
            graph.AddEdge(1, 2);
            Assert.IsFalse(graph.IsIsolatedVertex(1));
        }

        [TestMethod]
        public void TestIsUniversalVertex()
        {
            DirectionalGraph<int> graph = new DirectionalGraph<int>();
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            Assert.IsTrue(graph.IsUniversalVertex(1));
            Assert.IsFalse(graph.IsUniversalVertex(2));
            graph.AddEdge(2, 3);
            Assert.IsTrue(graph.IsUniversalVertex(2));
        }

        [TestMethod]
        public void TestGetIncidentEdges()
        {
            DirectionalGraph<int> graph = new DirectionalGraph<int>();
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(2, 3);
            var incidentEdgesOf1 = graph.GetIncidentEdges(1).ToHashSet();
            Assert.IsTrue(incidentEdgesOf1.SetEquals(new HashSet<IEdge<int>> { new DirectionalEdge<int>(1, 2), new DirectionalEdge<int>(1, 3) }));
            var incidentEdgesOf2 = graph.GetIncidentEdges(2).ToHashSet();
            Assert.IsTrue(incidentEdgesOf2.SetEquals(new HashSet<IEdge<int>> { new DirectionalEdge<int>(1, 2), new DirectionalEdge<int>(2, 3) }));
        }


        [TestMethod]
        public void TestGetHashCode()
        {
            DirectionalGraph<int> graph1 = new DirectionalGraph<int>();
            graph1.AddEdge(1, 2);
            graph1.AddEdge(2, 3);
            DirectionalGraph<int> graph2 = new DirectionalGraph<int>();
            graph2.AddEdge(1, 2);
            graph2.AddEdge(2, 3);
            Assert.AreEqual(graph1.GetHashCode(), graph2.GetHashCode());
            graph2.AddEdge(3, 4);
            Assert.AreNotEqual(graph1.GetHashCode(), graph2.GetHashCode());
        }
    }
}
