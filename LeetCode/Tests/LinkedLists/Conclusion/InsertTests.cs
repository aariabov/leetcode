namespace Tests.LinkedLists.Conclusion;

// [Вставка в отсортированный циклический список](https://leetcode.com/explore/learn/card/linked-list/213/conclusion/1226/)
public class InsertTests
{
    public class Node
    {
        public int val;
        public Node next;

        public Node() { }

        public Node(int _val)
        {
            val = _val;
            next = null;
        }

        public Node(int _val, Node _next)
        {
            val = _val;
            next = _next;
        }
    }

    [Fact]
    public void Test()
    {
        var e0 = new Node(3);
        var e1 = new Node(4);
        var e2 = new Node(1);

        e0.next = e1;
        e1.next = e2;
        e2.next = e0;

        var res = Insert(e0, 2);
        var list = new List<int>();
        var head = res;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != e0);
        Assert.Equal(new[] { 3, 4, 1, 2 }, list.ToArray());
    }

    [Fact]
    public void Test1()
    {
        var res = Insert(null, 1);
        Assert.Equal(1, res.val);
        Assert.Equal(1, res.next.val);
    }

    [Fact]
    public void Test2()
    {
        var e0 = new Node(1);

        e0.next = e0;

        var res = Insert(e0, 0);
        var list = new List<int>();
        var head = res;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != e0);
        Assert.Equal(new[] { 1, 0 }, list.ToArray());
    }

    [Fact]
    public void Test3()
    {
        var e0 = new Node(3);
        var e1 = new Node(3);
        var e2 = new Node(3);

        e0.next = e1;
        e1.next = e2;
        e2.next = e0;

        var res = Insert(e0, 0);
        var list = new List<int>();
        var head = res;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != e0);
        Assert.Equal(new[] { 3, 3, 3, 0 }, list.ToArray());
    }

    [Fact]
    public void Test4()
    {
        var e0 = new Node(1);
        var e1 = new Node(3);
        var e2 = new Node(5);

        e0.next = e1;
        e1.next = e2;
        e2.next = e0;

        var res = Insert(e0, 0);
        var list = new List<int>();
        var head = res;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != e0);
        Assert.Equal(new[] { 1, 3, 5, 0 }, list.ToArray());
    }

    [Fact]
    public void Test5()
    {
        var e0 = new Node(1);
        var e1 = new Node(3);
        var e2 = new Node(5);

        e0.next = e1;
        e1.next = e2;
        e2.next = e0;

        var res = Insert(e0, 6);
        var list = new List<int>();
        var head = res;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != e0);
        Assert.Equal(new[] { 1, 3, 5, 6 }, list.ToArray());
    }

    [Fact]
    public void Test6()
    {
        var e0 = new Node(1);
        var e1 = new Node(3);
        var e2 = new Node(5);

        e0.next = e1;
        e1.next = e2;
        e2.next = e0;

        var res = Insert(e0, 2);
        var list = new List<int>();
        var head = res;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != e0);
        Assert.Equal(new[] { 1, 2, 3, 5 }, list.ToArray());
    }

    [Fact]
    public void Test7()
    {
        var e0 = new Node(3);
        var e1 = new Node(5);
        var e2 = new Node(1);

        e0.next = e1;
        e1.next = e2;
        e2.next = e0;

        var res = Insert(e0, 0);
        var list = new List<int>();
        var head = res;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != e0);
        Assert.Equal(new[] { 3, 5, 0, 1 }, list.ToArray());
    }

    [Fact]
    public void Test8()
    {
        var e0 = new Node(1);
        var e1 = new Node(3);
        var e2 = new Node(5);

        e0.next = e1;
        e1.next = e2;
        e2.next = e0;

        var res = Insert(e0, 1);
        var list = new List<int>();
        var head = res;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != e0);
        Assert.Equal(new[] { 1, 1, 3, 5 }, list.ToArray());
    }

    [Fact]
    public void Test9()
    {
        var e0 = new Node(3);
        var e1 = new Node(3);
        var e2 = new Node(5);

        e0.next = e1;
        e1.next = e2;
        e2.next = e0;

        var res = Insert(e0, 0);
        var list = new List<int>();
        var head = res;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != e0);
        Assert.Equal(new[] { 3, 3, 5, 0 }, list.ToArray());
    }

    [Fact]
    public void Test10()
    {
        var e0 = new Node(5);
        var e1 = new Node(1);
        var e2 = new Node(3);

        e0.next = e1;
        e1.next = e2;
        e2.next = e0;

        var res = Insert(e0, 0);
        var list = new List<int>();
        var head = res;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != e0);
        Assert.Equal(new[] { 5, 0, 1, 3 }, list.ToArray());
    }

    public Node Insert(Node head, int insertVal)
    {
        // Сценарий 1: Список пуст
        if (head == null)
        {
            Node newNode = new Node(insertVal);
            newNode.next = newNode;
            return newNode;
        }

        Node curr = head;

        while (true)
        {
            Node next = curr.next;

            // Сценарий 2: Обычная вставка между меньшим и большим значением
            if (curr.val <= insertVal && insertVal <= next.val)
            {
                break;
            }

            // Наткнулись на точку перелома (конец и начало цикла, например: 4 -> 1)
            if (curr.val > next.val)
            {
                // Сценарий 3: Значение больше максимального ИЛИ меньше минимального
                if (insertVal >= curr.val || insertVal <= next.val)
                {
                    break;
                }
            }

            // Сценарий 4: Обошли весь круг и вернулись к началу (все элементы списка одинаковы)
            if (next == head)
            {
                break;
            }

            curr = curr.next;
        }

        // Вставка нового узла
        Node insertNode = new Node(insertVal, curr.next);
        curr.next = insertNode;

        return head;
    }

    // работает, норм
    public Node InsertMy(Node head, int insertVal)
    {
        if (head == null)
        {
            var singleNode = new Node(insertVal);
            singleNode.next = singleNode;
            return singleNode;
        }

        if (head.next == head)
        {
            var singleNode = new Node(insertVal);
            singleNode.next = head;
            head.next = singleNode;
            return head;
        }

        var current = head;
        while (true)
        {
            // вставляем в центр
            if (current.val <= insertVal && insertVal <= current.next.val)
            {
                var centerNode = new Node(insertVal);
                centerNode.next = current.next;
                current.next = centerNode;
                return head;
            }

            // вставляем в конец
            if (current.val < insertVal && current.val > current.next.val)
            {
                var endNode = new Node(insertVal);
                endNode.next = current.next;
                current.next = endNode;
                return head;
            }

            // вставляем в начало
            if (
                current.val >= insertVal
                && insertVal < current.next.val
                && current.val > current.next.val
            )
            {
                var startNode = new Node(insertVal);
                startNode.next = current.next;
                current.next = startNode;
                return head;
            }

            if (current.next == head && current.val == head.val)
            {
                var endNode = new Node(insertVal);
                endNode.next = current.next;
                current.next = endNode;
                return head;
            }
            current = current.next;
        }

        return head;
    }
}
