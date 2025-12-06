using System.Collections.Specialized;

namespace Tests.LinkedLists.Conclusion;

/// <summary>
/// Копия списка с рандомными указателями https://leetcode.com/explore/learn/card/linked-list/213/conclusion/1229/
/// </summary>
public class CopyRandomListTests
{
    public class Node
    {
        public int val;
        public Node next;
        public Node random;

        public Node(int _val)
        {
            val = _val;
            next = null;
            random = null;
        }
    }

    [Fact]
    public void Test()
    {
        var e0 = new Node(7);
        var e1 = new Node(13);
        var e2 = new Node(11);
        var e3 = new Node(10);
        var e4 = new Node(1);

        e0.next = e1;
        e1.next = e2;
        e2.next = e3;
        e3.next = e4;

        e1.random = e0;
        e2.random = e4;
        e3.random = e2;
        e4.random = e0;

        var res = CopyRandomList(e0);
        var list = new List<int>();
        var head = res;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 7, 13, 11, 10, 1 }, list.ToArray());

        Assert.Equal(null, res.random);
        Assert.Equal(7, res.next.random.val);
        Assert.Equal(1, res.next.next.random.val);
        Assert.Equal(11, res.next.next.next.random.val);
        Assert.Equal(7, res.next.next.next.next.random.val);
    }

    [Fact]
    public void Test1()
    {
        var e0 = new Node(1);
        var e1 = new Node(2);

        e0.next = e1;

        e0.random = e1;
        e1.random = e1;

        var res = CopyRandomList(e0);
        var list = new List<int>();
        var head = res;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 1, 2 }, list.ToArray());

        Assert.Equal(2, res.random.val);
        Assert.Equal(2, res.next.random.val);
    }

    [Fact]
    public void Test2()
    {
        var e0 = new Node(3);
        var e1 = new Node(3);
        var e2 = new Node(3);

        e0.next = e1;
        e1.next = e2;

        e1.random = e0;

        var res = CopyRandomList(e0);
        var list = new List<int>();
        var head = res;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 3, 3, 3 }, list.ToArray());

        Assert.Equal(null, res.random);
        Assert.Equal(3, res.next.random.val);
        Assert.Equal(null, res.next.next.random);
    }

    [Fact]
    public void Test3()
    {
        var res = CopyRandomList(null);
        Assert.Null(res);
    }

    public Node CopyRandomList(Node head)
    {
        if (head == null)
            return null;

        // 1. Вставляем копии узлов после каждого оригинального узла
        Node current = head;
        while (current != null)
        {
            Node newNode = new Node(current.val);
            newNode.next = current.next;
            current.next = newNode;
            current = newNode.next;
        }

        // 2. Проставляем random указатели для копий
        current = head;
        while (current != null)
        {
            if (current.random != null)
            {
                current.next.random = current.random.next;
            }
            current = current.next.next;
        }

        // 3. Отсоединяем два списка
        current = head;
        Node copyHead = head.next;
        Node copy = copyHead;

        while (current != null)
        {
            current.next = current.next.next;
            if (copy.next != null)
            {
                copy.next = copy.next.next;
            }
            current = current.next;
            copy = copy.next;
        }

        return copyHead;
    }

    // понятное, но медленное
    public Node CopyRandomList1(Node head)
    {
        if (head == null)
            return null;

        // Словарь: оригинальный узел -> его копия
        Dictionary<Node, Node> map = new Dictionary<Node, Node>();

        Node current = head;

        // 1. Создаем копии всех узлов и кладем в словарь
        while (current != null)
        {
            map[current] = new Node(current.val);
            current = current.next;
        }

        // 2. Настраиваем next и random указатели
        current = head;
        while (current != null)
        {
            map[current].next = current.next != null ? map[current.next] : null;
            map[current].random = current.random != null ? map[current.random] : null;
            current = current.next;
        }

        return map[head];
    }

    public Node CopyRandomList2(Node head)
    {
        if (head == null)
        {
            return head;
        }

        // формируем список для получения элемента по индексу и справочник для получения индекса по элементу
        var list = new List<Node>();
        var dict = new Dictionary<Node, int>();
        var cur = head;
        while (cur != null)
        {
            dict[cur] = list.Count;
            list.Add(cur);
            cur = cur.next;
        }

        // map индекс элемента, и на какой смотрит
        var initArr = new int?[list.Count];
        for (int k = 0; k < list.Count; k++)
        {
            if (list[k].random != null)
            {
                var idx = dict[list[k].random];
                initArr[k] = idx;
            }
        }

        // формируем новый список
        cur = head.next;
        var newHead = new Node(head.val);
        var prev = newHead;
        var arr = new Node[list.Count];
        arr[0] = newHead;
        var i = 1;
        while (cur != null)
        {
            var node = new Node(cur.val);
            prev.next = node;
            prev = node;
            arr[i] = node;
            i++;
            cur = cur.next;
        }

        // заполняем random
        for (int j = 0; j < initArr.Length; j++)
        {
            var initIdx = initArr[j];
            if (initIdx != null)
            {
                arr[j].random = arr[initIdx.Value];
            }
        }

        return newHead;
    }
}
