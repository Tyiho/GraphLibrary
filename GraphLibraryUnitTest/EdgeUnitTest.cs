using GraphLibrary.Structs;

namespace GraphLibraryUnitTest
{
    [TestClass]
    public sealed class EdgeUnitTest
    {
        [TestMethod]
        public void TestEdgeCreation()
        {
            Edge<int> edge = new Edge<int>(1, 2);

            Assert.AreEqual(1, edge.Vertex1);
            Assert.AreEqual(2, edge.Vertex2);

            Edge<int> edgeFromTuple = new Edge<int>((3, 4));
            Assert.AreEqual(3, edgeFromTuple.Vertex1);
            Assert.AreEqual(4, edgeFromTuple.Vertex2);
        }


        [TestMethod]
        public void TestEdgeContains()
        {
            Edge<int> edge = new Edge<int>(1, 2);

            Assert.IsTrue(edge.Contains(1));
            Assert.IsTrue(edge.Contains(2));
            Assert.IsFalse(edge.Contains(3));
        }

        [TestMethod]
        public void TestEdgeEquality()
        {
            Edge<int> edge1 = new Edge<int>(1, 2);
            Edge<int> edge2 = new Edge<int>(2, 1);
            Edge<int> edge3 = new Edge<int>(1, 3);
            Assert.IsTrue(edge1.Equals(edge2));
            Assert.IsFalse(edge1.Equals(edge3));
        }

        [TestMethod]
        public void TestEdgeObjectEquality()
        {
            Edge<int> edge1 = new Edge<int>(1, 2);
            Edge<int> edge2 = new Edge<int>(2, 1);
            Edge<int> edge3 = new Edge<int>(1, 3);
            Assert.IsTrue(edge1.Equals((object)edge2));
            Assert.IsFalse(edge1.Equals((object)edge3));
        }

        [TestMethod]
        public void TestEdgeHashCode()
        {
            Edge<int> edge1 = new Edge<int>(1, 2);
            Edge<int> edge2 = new Edge<int>(2, 1);
            Assert.AreEqual(edge1.GetHashCode(), edge2.GetHashCode());
        }

        [TestMethod]
        public void TestEdgeToString()
        {
            Edge<int> edge = new Edge<int>(1, 2);
            Assert.AreEqual("(1 <-> 2)", edge.ToString());
        }

        [TestMethod]
        public void TestEdgeDeconstruct()
        {
            Edge<int> edge = new Edge<int>(1, 2);
            edge.Deconstruct(out var v1, out var v2);
            Assert.AreEqual(1, v1);
            Assert.AreEqual(2, v2);
        }

        [TestMethod]
        public void TestEdgeToTuple()
        {
            Edge<int> edge = new Edge<int>(1, 2);
            var tuple = edge.ToTuple();
            Assert.AreEqual((1, 2), tuple);
        }

        [TestMethod]
        public void TestAsEnumerable()
        {
            Edge<int> edge = new Edge<int>(1, 2);
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
        public void GetConnectedVertex_ResultCast_ThrowsNullReferenceException()
        {
            DirectionalEdge<int> edge = new DirectionalEdge<int>(1, 2);

            Assert.ThrowsException<NullReferenceException>(() => (int)edge.GetConnectedVertex(3));
        }

        [TestMethod]
        public void GetConnectedVertex_Result_IsDefault()
        {
            DirectionalEdge<int> edge = new DirectionalEdge<int>(1, 2);

            Assert.AreEqual(default, edge.GetConnectedVertex(3).Value);
        }

        [TestMethod]
        public void GetConnectedVertex_IsNull_IsFalse()
        {
            Edge<int> edge = new Edge<int>(1, 2);
            Assert.IsFalse(edge.GetConnectedVertex(2).IsNull);
        }

        [TestMethod]
        public void GetConnectedVertex_IsNull_IsTrue()
        {
            Edge<int> edge = new Edge<int>(1, 2);
            Assert.IsTrue(edge.GetConnectedVertex(3).IsNull);
        }


        [TestMethod]
        public void TestIsVertexReachable()
        {
            Edge<int> edge = new Edge<int>(1, 2);
            Assert.IsTrue(edge.IsVertex1ReachableFrom(1));
            Assert.IsTrue(edge.IsVertex2ReachableFrom(1));
            Assert.IsTrue(edge.IsVertex1ReachableFrom(2));
            Assert.IsTrue(edge.IsVertex2ReachableFrom(2));
            Assert.IsFalse(edge.IsVertex1ReachableFrom(3));
            Assert.IsFalse(edge.IsVertex2ReachableFrom(3));
        }
    }
}