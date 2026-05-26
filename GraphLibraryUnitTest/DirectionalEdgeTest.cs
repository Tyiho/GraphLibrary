using GraphLibrary.Structs;

namespace GraphLibraryUnitTest
{
    [TestClass]
    public sealed class DirectionalEdgeUnitTest
    {
        [TestMethod]
        public void TestEdgeCreation()
        {
            DirectionalEdge<int> edge = new DirectionalEdge<int>(1, 2);

            Assert.AreEqual(1, edge.Vertex1);
            Assert.AreEqual(2, edge.Vertex2);

            DirectionalEdge<int> edgeFromTuple = new DirectionalEdge<int>((3, 4));
            Assert.AreEqual(3, edgeFromTuple.Vertex1);
            Assert.AreEqual(4, edgeFromTuple.Vertex2);
        }


        [TestMethod]
        public void TestEdgeContains()
        {
            DirectionalEdge<int> edge = new DirectionalEdge<int>(1, 2);

            Assert.IsTrue(edge.Contains(1));
            Assert.IsTrue(edge.Contains(2));
            Assert.IsFalse(edge.Contains(3));
        }

        [TestMethod]
        public void TestEdgeEquality()
        {
            DirectionalEdge<int> edge1 = new DirectionalEdge<int>(1, 2);
            DirectionalEdge<int> edge2 = new DirectionalEdge<int>(2, 1);
            DirectionalEdge<int> edge3 = new DirectionalEdge<int>(1, 3);
            DirectionalEdge<int> edge4 = new DirectionalEdge<int>(1, 2);
            Assert.IsFalse(edge1.Equals(edge2));
            Assert.IsFalse(edge1.Equals(edge3));
            Assert.IsTrue(edge1.Equals(edge4));
        }

        [TestMethod]
        public void TestEdgeHashCode()
        {
            DirectionalEdge<int> edge1 = new DirectionalEdge<int>(1, 2);
            DirectionalEdge<int> edge2 = new DirectionalEdge<int>(2, 1);
            Assert.AreEqual(edge1.GetHashCode(), edge2.GetHashCode());
        }

        [TestMethod]
        public void TestEdgeToString()
        {
            DirectionalEdge<int> edge = new DirectionalEdge<int>(1, 2);
            Assert.AreEqual("(1 -> 2)", edge.ToString());
        }

        [TestMethod]
        public void TestEdgeDeconstruct()
        {
            DirectionalEdge<int> edge = new DirectionalEdge<int>(1, 2);
            edge.Deconstruct(out var v1, out var v2);
            Assert.AreEqual(1, v1);
            Assert.AreEqual(2, v2);
        }

        [TestMethod]
        public void TestEdgeToTuple()
        {
            DirectionalEdge<int> edge = new DirectionalEdge<int>(1, 2);
            var tuple = edge.ToTuple();
            Assert.AreEqual((1, 2), tuple);
        }

        [TestMethod]
        public void TestAsEnumerable()
        {
            DirectionalEdge<int> edge = new DirectionalEdge<int>(1, 2);
            var values = edge.AsEnumerable().ToHashSet();
            Assert.IsTrue(values.SetEquals(new HashSet<int> { 1, 2 }));
        }

        [TestMethod]
        public void GetConnectedVertex_Result_AreEqual()
        {
            DirectionalEdge<int> edge = new DirectionalEdge<int>(1, 2);
            Assert.AreEqual<int>(2, edge.GetConnectedVertex(1));
        }

        [TestMethod]
        public void GetConnectedVertex_IsNull_IsFalse()
        {
           DirectionalEdge<int> edge = new DirectionalEdge<int>(1, 2);
           Assert.IsFalse(edge.GetConnectedVertex(1).IsNull);
        }

        [TestMethod]
        public void GetConnectedVertex_IsNull_IsTrue()
        {
            DirectionalEdge<int> edge = new DirectionalEdge<int>(1, 2);
            Assert.IsTrue(edge.GetConnectedVertex(3).IsNull);
        }

        [TestMethod]
        public void TestIsVertexReachable()
        {
            DirectionalEdge<int> edge = new DirectionalEdge<int>(1, 2);
            Assert.IsTrue(edge.IsVertex1ReachableFrom(1));
            Assert.IsTrue(edge.IsVertex2ReachableFrom(1));
            Assert.IsFalse(edge.IsVertex1ReachableFrom(2));
            Assert.IsTrue(edge.IsVertex2ReachableFrom(2));
            Assert.IsFalse(edge.IsVertex1ReachableFrom(3));
            Assert.IsFalse(edge.IsVertex2ReachableFrom(3));
        }
    }
}