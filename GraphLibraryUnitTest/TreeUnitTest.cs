using GraphLibrary.Interfaces;
using GraphLibrary.Structs;
using System.Linq;

namespace GraphLibraryUnitTest
{
    [TestClass]
    public sealed class TreeUnitTest
    {
        [TestMethod]
        public void TestTreeCreation() { 
            Tree<int> tree = new Tree<int>(1);
            Assert.AreEqual(1, tree.Root);
            Assert.IsTrue(tree.Vertices.SetEquals(new HashSet<int> { 1 }));
            Assert.IsTrue(tree.Edges.SetEquals(new HashSet<IEdge<int>>()));

            Tree<object> tree1 = new Tree<object>(1, new HashSet<IEdge<object>> (){ new DirectionalEdge<object>(1,2), new DirectionalEdge<object>(1, 3), new DirectionalEdge<object>(2,3), });
            Assert.AreEqual(3, tree1.Vertices.Count);
            Assert.AreEqual(2, tree1.Edges.Count);
            Assert.IsFalse(tree1.Edges.Contains(new DirectionalEdge<object>(2,3)));
        }

        [TestMethod]
        public void TestAddEdge() {
            Tree<int> tree = new Tree<int>(1);
            tree.AddEdge(new DirectionalEdge<int>(1, 2));
            Assert.IsTrue(tree.Vertices.SetEquals(new HashSet<int> { 1, 2 }));
            Assert.IsTrue(tree.Edges.SetEquals(new HashSet<IEdge<int>> { new DirectionalEdge<int>(1, 2) }));

            tree.AddEdge(new DirectionalEdge<int>(3, 4));
            Assert.IsTrue(tree.Vertices.SetEquals(new HashSet<int> { 1, 2 }));
            Assert.IsFalse(tree.Edges.Contains(new DirectionalEdge<int>(3,4)));
        }

        [TestMethod]
        public void TestAddVertex() {
            Tree<int> tree = new Tree<int>(1);
            tree.AddVertex(2);
            Assert.IsTrue(tree.Vertices.SetEquals(new HashSet<int> { 1, 2 }));
            Assert.IsTrue(tree.Edges.SetEquals(new HashSet<IEdge<int>> { new DirectionalEdge<int>(1, 2) }));
            tree.AddVertex(3);
            Assert.IsTrue(tree.Vertices.SetEquals(new HashSet<int> { 1, 2, 3 }));
            Assert.IsTrue(tree.Edges.SetEquals(new HashSet<IEdge<int>> { new DirectionalEdge<int>(1, 2), new DirectionalEdge<int>(1, 3) }));
        }

        [TestMethod]
        public void TestRemoveVertex()
        {
            Tree<int> tree = new Tree<int>(1);
            tree.AddEdge(new DirectionalEdge<int>(1, 2));
            tree.AddEdge(new DirectionalEdge<int>(2, 3));
            tree.RemoveVertex(2);
            Assert.IsFalse(tree.Vertices.SetEquals(new HashSet<int> { 1, 3 }));
            Assert.IsFalse(tree.Edges.Contains(new DirectionalEdge<int>(1, 2)));
            Assert.IsFalse(tree.Edges.Contains(new DirectionalEdge<int>(2, 3)));

            Assert.ThrowsException<InvalidOperationException>(() => tree.RemoveVertex(1));
        }

        [TestMethod]
        public void TestEquality()
        {
            Tree<int> tree1 = new Tree<int>(1);
            tree1.AddEdge(new DirectionalEdge<int>(1, 2));
            Tree<int> tree2 = new Tree<int>(1);
            tree2.AddEdge(new DirectionalEdge<int>(1, 2));
            Assert.IsTrue(tree1.Equals(tree2));
            Assert.IsTrue(tree1.GetHashCode() == tree2.GetHashCode());
            tree2.AddEdge(new DirectionalEdge<int>(2, 3));
            Assert.IsFalse(tree1.Equals(tree2));
        }
    }
}
