using GraphLibrary.Structs;

namespace GraphLibraryUnitTest
{
    [TestClass]
    public sealed class EdgeUnitTest
    {
        [TestMethod]
        public void TestEdgeCreation()
        {
            Edge<object> edge = new Edge<object>(1, 2);

            Assert.AreEqual(1, edge.Vertex1);
            Assert.AreEqual(2, edge.Vertex2);

            Edge<object> edgeFromTuple = new Edge<object>((3, 4));
            Assert.AreEqual(3, edgeFromTuple.Vertex1);
            Assert.AreEqual(4, edgeFromTuple.Vertex2);
        }


        [TestMethod]
        public void TestEdgeContains()
        {
            Edge<object> edge = new Edge<object>(1, 2);

            Assert.IsTrue(edge.Contains(1));
            Assert.IsTrue(edge.Contains(2));
            Assert.IsFalse(edge.Contains(3));
        }

        [TestMethod]
        public void TestEdgeEquality()
        {
            Edge<object> edge1 = new Edge<object>(1, 2);
            Edge<object> edge2 = new Edge<object>(2, 1);
            Edge<object> edge3 = new Edge<object>(1, 3);
            Assert.IsTrue(edge1.Equals(edge2));
            Assert.IsFalse(edge1.Equals(edge3));
        }

        [TestMethod]
        public void TestEdgeObjectEquality()
        {
            Edge<object> edge1 = new Edge<object>(1, 2);
            Edge<object> edge2 = new Edge<object>(2, 1);
            Edge<object> edge3 = new Edge<object>(1, 3);
            Assert.IsTrue(edge1.Equals((object)edge2));
            Assert.IsFalse(edge1.Equals((object)edge3));
        }

        [TestMethod]
        public void TestEdgeHashCode()
        {
            Edge<object> edge1 = new Edge<object>(1, 2);
            Edge<object> edge2 = new Edge<object>(2, 1);
            Assert.AreEqual(edge1.GetHashCode(), edge2.GetHashCode());
        }

        [TestMethod]
        public void TestEdgeToString()
        {
            Edge<object> edge = new Edge<object>(1, 2);
            Assert.AreEqual("(1 <-> 2)", edge.ToString());
        }

        [TestMethod]
        public void TestEdgeDeconstruct()
        {
            Edge<object> edge = new Edge<object>(1, 2);
            edge.Deconstruct(out var v1, out var v2);
            Assert.AreEqual(1, v1);
            Assert.AreEqual(2, v2);
        }

        [TestMethod]
        public void TestEdgeToTuple()
        {
            Edge<object> edge = new Edge<object>(1, 2);
            var tuple = edge.ToTuple();
            Assert.AreEqual((1, 2), tuple);
        }

        [TestMethod]
        public void TestAsEnumerable()
        {
            Edge<object> edge = new Edge<object>(1, 2);
            var values = edge.AsEnumerable().ToHashSet();
            Assert.IsTrue(values.SetEquals(new HashSet<object> { 1, 2 }));
        }

        [TestMethod]
        public void TestGetConnectedVertex()
        {
            Edge<object> edge = new Edge<object>(1, 2);
            Assert.AreEqual(2, edge.GetConnectedVertex(1));
            Assert.AreEqual(1, edge.GetConnectedVertex(2));
            Assert.IsNull(edge.GetConnectedVertex(3));
        }

        [TestMethod]
        public void TestIsVertexReachable()
        {
            Edge<object> edge = new Edge<object>(1, 2);
            Assert.IsTrue(edge.IsVertex1ReachableFrom(1));
            Assert.IsTrue(edge.IsVertex2ReachableFrom(1));
            Assert.IsTrue(edge.IsVertex1ReachableFrom(2));
            Assert.IsTrue(edge.IsVertex2ReachableFrom(2));
            Assert.IsFalse(edge.IsVertex1ReachableFrom(3));
            Assert.IsFalse(edge.IsVertex2ReachableFrom(3));
        }
    }
}