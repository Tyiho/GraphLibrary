using GraphLibrary.Structs;

namespace GraphLibraryUnitTest
{
    [TestClass]
    public sealed class DirectionalEdgeUnitTest
    {
        [TestMethod]
        public void TestEdgeCreation()
        {
            DirectionalEdge<object> edge = new DirectionalEdge<object>(1, 2);

            Assert.AreEqual(1, edge.Vertex1);
            Assert.AreEqual(2, edge.Vertex2);

            DirectionalEdge<object> edgeFromTuple = new DirectionalEdge<object>((3, 4));
            Assert.AreEqual(3, edgeFromTuple.Vertex1);
            Assert.AreEqual(4, edgeFromTuple.Vertex2);
        }


        [TestMethod]
        public void TestEdgeContains()
        {
            DirectionalEdge<object> edge = new DirectionalEdge<object>(1, 2);

            Assert.IsTrue(edge.Contains(1));
            Assert.IsTrue(edge.Contains(2));
            Assert.IsFalse(edge.Contains(3));
        }

        [TestMethod]
        public void TestEdgeEquality()
        {
            DirectionalEdge<object> edge1 = new DirectionalEdge<object>(1, 2);
            DirectionalEdge<object> edge2 = new DirectionalEdge<object>(2, 1);
            DirectionalEdge<object> edge3 = new DirectionalEdge<object>(1, 3);
            DirectionalEdge<object> edge4 = new DirectionalEdge<object>(1, 2);
            Assert.IsFalse(edge1.Equals(edge2));
            Assert.IsFalse(edge1.Equals(edge3));
            Assert.IsTrue(edge1.Equals(edge4));
        }

        [TestMethod]
        public void TestEdgeObjectEquality()
        {
            DirectionalEdge<object> edge1 = new DirectionalEdge<object>(1, 2);
            DirectionalEdge<object> edge2 = new DirectionalEdge<object>(2, 1);
            DirectionalEdge<object> edge3 = new DirectionalEdge<object>(1, 3);
            DirectionalEdge<object> edge4 = new DirectionalEdge<object>(1, 2);
            Assert.IsFalse(edge1.Equals((object)edge2));
            Assert.IsFalse(edge1.Equals((object)edge3));
            Assert.IsTrue(edge1.Equals((object)edge4));
        }

        [TestMethod]
        public void TestEdgeHashCode()
        {
            DirectionalEdge<object> edge1 = new DirectionalEdge<object>(1, 2);
            DirectionalEdge<object> edge2 = new DirectionalEdge<object>(2, 1);
            Assert.AreEqual(edge1.GetHashCode(), edge2.GetHashCode());
        }

        [TestMethod]
        public void TestEdgeToString()
        {
            DirectionalEdge<object> edge = new DirectionalEdge<object>(1, 2);
            Assert.AreEqual("(1 -> 2)", edge.ToString());
        }

        [TestMethod]
        public void TestEdgeDeconstruct()
        {
            DirectionalEdge<object> edge = new DirectionalEdge<object>(1, 2);
            edge.Deconstruct(out var v1, out var v2);
            Assert.AreEqual(1, v1);
            Assert.AreEqual(2, v2);
        }

        [TestMethod]
        public void TestEdgeToTuple()
        {
            DirectionalEdge<object> edge = new DirectionalEdge<object>(1, 2);
            var tuple = edge.ToTuple();
            Assert.AreEqual((1, 2), tuple);
        }

        [TestMethod]
        public void TestAsEnumerable()
        {
            DirectionalEdge<object> edge = new DirectionalEdge<object>(1, 2);
            var values = edge.AsEnumerable().ToHashSet();
            Assert.IsTrue(values.SetEquals(new HashSet<object> { 1, 2 }));
        }

        [TestMethod]
        public void TestGetConnectedVertex()
        {
            DirectionalEdge<object> edge = new DirectionalEdge<object>(1, 2);
            Assert.AreEqual(2, edge.GetConnectedVertex(1));
            Assert.IsNull(edge.GetConnectedVertex(2));
            Assert.IsNull(edge.GetConnectedVertex(3));
        }

        [TestMethod]
        public void TestIsVertexReachable()
        {
            DirectionalEdge<object> edge = new DirectionalEdge<object>(1, 2);
            Assert.IsTrue(edge.IsVertex1ReachableFrom(1));
            Assert.IsTrue(edge.IsVertex2ReachableFrom(1));
            Assert.IsFalse(edge.IsVertex1ReachableFrom(2));
            Assert.IsTrue(edge.IsVertex2ReachableFrom(2));
            Assert.IsFalse(edge.IsVertex1ReachableFrom(3));
            Assert.IsFalse(edge.IsVertex2ReachableFrom(3));
        }
    }
}