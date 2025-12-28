using FluentAssertions;

namespace Tests.QueueStack.StackAndDFS;

/// <summary>
/// [Клонировать граф](https://leetcode.com/explore/learn/card/queue-stack/232/practical-application-stack/1392/)
/// </summary>
public class CloneGraphTests
{
    [Fact]
    public void CloneGraph_NullInput_ReturnsNull()
    {
        Node result = CloneGraph(null);
        Assert.Null(result);
    }

    [Fact]
    public void CloneGraph_SingleNode()
    {
        Node node = new Node(1);

        Node clone = CloneGraph(node);

        Assert.NotNull(clone);
        Assert.NotSame(node, clone);
        Assert.Equal(1, clone.val);
        Assert.Empty(clone.neighbors);
    }

    [Fact]
    public void CloneGraph_ExampleGraph()
    {
        /*
            Graph:
            1 -- 2
            |    |
            4 -- 3
        */

        Node n1 = new Node(1);
        Node n2 = new Node(2);
        Node n3 = new Node(3);
        Node n4 = new Node(4);

        n1.neighbors = new[] { n2, n4 };
        n2.neighbors = new[] { n1, n3 };
        n3.neighbors = new[] { n2, n4 };
        n4.neighbors = new[] { n1, n3 };

        Node clone = CloneGraph(n1);

        // Проверка структуры
        Assert.NotSame(n1, clone);
        Assert.Equal(1, clone.val);
        Assert.Equal(2, clone.neighbors.Count);

        // Проверка, что граф реально склонирован
        HashSet<Node> original = new HashSet<Node>();
        HashSet<Node> copied = new HashSet<Node>();

        Traverse(n1, original);
        Traverse(clone, copied);

        Assert.Equal(original.Count, copied.Count);

        foreach (var node in copied)
            Assert.DoesNotContain(node, original);
    }

    // Вспомогательный метод для обхода графа
    private void Traverse(Node node, HashSet<Node> visited)
    {
        if (visited.Contains(node))
            return;

        visited.Add(node);

        foreach (var neighbor in node.neighbors)
            Traverse(neighbor, visited);
    }
    
    public Node CloneGraphIter(Node node) {
        if (node == null)
            return null;

        // Map original -> clone
        Dictionary<Node, Node> visited = new Dictionary<Node, Node>();

        // Queue for BFS
        Queue<Node> queue = new Queue<Node>();

        // Clone the first node
        visited[node] = new Node(node.val);
        queue.Enqueue(node);

        while (queue.Count > 0) {
            Node current = queue.Dequeue();

            foreach (var neighbor in current.neighbors) {
                // If neighbor not cloned yet
                if (!visited.ContainsKey(neighbor)) {
                    visited[neighbor] = new Node(neighbor.val);
                    queue.Enqueue(neighbor);
                }

                // Add neighbor clone to current clone
                visited[current].neighbors.Add(visited[neighbor]);
            }
        }

        return visited[node];
    }
    
    private Dictionary<Node, Node> visited = new Dictionary<Node, Node>();
    public Node CloneGraphRec(Node node) {
        if (node == null)
            return null;

        // If node already cloned, return it
        if (visited.ContainsKey(node))
            return visited[node];

        // Clone current node
        Node clone = new Node(node.val);
        visited[node] = clone;

        // Clone neighbors
        foreach (var neighbor in node.neighbors) {
            clone.neighbors.Add(CloneGraphRec(neighbor));
        }

        return clone;
    }
    
    // работает
    public Node CloneGraph(Node node)
    {
        if (node is null)
        {
            return null;
        }
        
        var res = new Node(node.val);
        var stack = new Stack<Node>();
        var initDict = new Dictionary<Node, Node>();
        initDict.Add(node, res);
        stack.Push(node);
        while (stack.Count > 0)
        {
            var item = stack.Pop();
            foreach (var neighbor in item.neighbors)
            {
                if (!initDict.ContainsKey(neighbor))
                {
                    stack.Push(neighbor);
                    initDict.Add(neighbor, new Node(neighbor.val));
                }
            }
        }

        foreach (var initItem in initDict)
        {
            var cloneItem = initItem.Value;
            foreach (var initNode in initItem.Key.neighbors)
            {
                cloneItem.neighbors.Add(initDict[initNode]);
            }
        }

        return res;
    }
    
    // работает
    public Node CloneGraphMy(Node node)
    {
        if (node is null)
        {
            return null;
        }
        
        var res = new Node(node.val);
        var stack = new Stack<Node>();
        var initDict = new Dictionary<int, Node>();
        var cloneDict = new Dictionary<int, Node>();
        initDict.Add(node.val, node);
        stack.Push(node);
        cloneDict.Add(res.val, res);
        while (stack.Count > 0)
        {
            var item = stack.Pop();
            foreach (var neighbor in item.neighbors)
            {
                if (initDict.TryAdd(neighbor.val, neighbor))
                {
                    stack.Push(neighbor);
                    cloneDict.Add(neighbor.val, new Node(neighbor.val));
                }
            }
        }

        foreach (var initItem in initDict)
        {
            var cloneItem = cloneDict[initItem.Key];
            foreach (var initNode in initItem.Value.neighbors)
            {
                var cloneNeighbor = cloneDict[initNode.val];
                cloneItem.neighbors.Add(cloneNeighbor);
            }
        }

        return res;
    }
    
    public class Node {
        public int val;
        public IList<Node> neighbors;

        public Node() {
            val = 0;
            neighbors = new List<Node>();
        }

        public Node(int _val) {
            val = _val;
            neighbors = new List<Node>();
        }

        public Node(int _val, List<Node> _neighbors) {
            val = _val;
            neighbors = _neighbors;
        }
    }
}