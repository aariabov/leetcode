namespace Tests.LinkedLists.Conclusion;

/// <summary>
/// Flatten a Multilevel Doubly Linked List https://leetcode.com/explore/learn/card/linked-list/213/conclusion/1225/
/// </summary>
public class FlattenTests
{
    [Fact]
    public void Test()
    {
        var e0 = new Node(1);
        var e1 = new Node(2);
        var e2 = new Node(3);
        var e3 = new Node(4);
        var e4 = new Node(5);
        var e5 = new Node(6);

        e0.next = e1;
        e1.next = e2;
        e2.next = e3;
        e3.next = e4;
        e4.next = e5;

        e5.prev = e4;
        e4.prev = e3;
        e3.prev = e2;
        e2.prev = e1;
        e1.prev = e0;

        var l0 = new Node(7);
        var l1 = new Node(8);
        var l2 = new Node(9);
        var l3 = new Node(10);

        l0.next = l1;
        l1.next = l2;
        l2.next = l3;

        l3.prev = l2;
        l2.prev = l1;
        l1.prev = l0;

        var n0 = new Node(11);
        var n1 = new Node(12);

        n0.next = n1;
        n1.prev = n0;

        e2.child = l0;
        l1.child = n0;

        var result = Flatten(e0);
        var list = new List<int>();
        var head = result;
        var last = head;
        do
        {
            list.Add(head.val);
            last = head;
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 1, 2, 3, 7, 8, 11, 12, 9, 10, 4, 5, 6 }, list.ToArray());

        var reversList = new List<int>();
        do
        {
            reversList.Add(last.val);
            last = last.prev;
        } while (last != null);
        Assert.Equal(new[] { 6, 5, 4, 10, 9, 12, 11, 8, 7, 3, 2, 1 }, reversList.ToArray());
    }

    [Fact]
    public void Test1()
    {
        var e0 = new Node(1);
        var e1 = new Node(2);

        e0.next = e1;
        e1.prev = e0;

        var l0 = new Node(3);

        e0.child = l0;

        var result = Flatten(e0);
        var list = new List<int>();
        var head = result;
        var last = head;
        do
        {
            list.Add(head.val);
            last = head;
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 1, 3, 2 }, list.ToArray());

        var reversList = new List<int>();
        do
        {
            reversList.Add(last.val);
            last = last.prev;
        } while (last != null);
        Assert.Equal(new[] { 2, 3, 1 }, reversList.ToArray());
    }

    [Fact]
    public void Test2()
    {
        var result = Flatten(null);
        Assert.Null(result);
    }

    [Fact]
    public void Test3()
    {
        var e0 = new Node(1);
        var l0 = new Node(2);
        var n0 = new Node(3);

        e0.child = l0;
        l0.child = n0;

        var result = Flatten(e0);
        var list = new List<int>();
        var head = result;
        var last = head;
        do
        {
            list.Add(head.val);
            last = head;
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 1, 2, 3 }, list.ToArray());

        var reversList = new List<int>();
        do
        {
            reversList.Add(last.val);
            last = last.prev;
        } while (last != null);
        Assert.Equal(new[] { 3, 2, 1 }, reversList.ToArray());
    }

    public Node Flatten(Node head)
    {
        if (head == null)
            return null;

        Stack<Node> stack = new Stack<Node>();
        Node curr = head;

        while (curr != null)
        {
            // Если есть child
            if (curr.child != null)
            {
                // Если есть next — сохраняем в стек
                if (curr.next != null)
                    stack.Push(curr.next);

                // Подключаем child вместо next
                curr.next = curr.child;
                curr.child.prev = curr;
                curr.child = null;
            }

            // Если дошли до конца уровня — вытаскиваем из стека
            if (curr.next == null && stack.Count > 0)
            {
                Node next = stack.Pop();
                curr.next = next;
                next.prev = curr;
            }

            curr = curr.next;
        }

        return head;
    }

    public Node Flatten3(Node head)
    {
        if (head == null || (head.next == null && head.child == null))
        {
            return head;
        }

        var cur = head;
        var stack = new Stack<Node>();
        while (true)
        {
            if (cur.child != null)
            {
                if (cur.next != null)
                {
                    stack.Push(cur.next);
                }
                cur.next = cur.child;
                cur.child.prev = cur;
                cur.child = null;
            }

            if (cur.next == null)
            {
                break;
            }
            cur = cur.next;
        }

        while (true)
        {
            if (cur.next == null && stack.Count == 0)
            {
                break;
            }

            if (cur.next == null)
            {
                var next = stack.Pop();
                next.prev = cur;
                cur.next = next;
            }

            cur = cur.next;
        }

        return head;
    }

    public Node Flatten2(Node head)
    {
        if (head == null)
            return null;

        FlattenDFS(head);
        return head;
    }

    // Возвращает tail после развёртки
    private Node FlattenDFS(Node node)
    {
        Node curr = node;
        Node last = null;

        while (curr != null)
        {
            Node next = curr.next;

            // Если есть child — раскрываем его
            if (curr.child != null)
            {
                Node childHead = curr.child;
                Node childTail = FlattenDFS(childHead);

                // Вставляем child между curr и next
                curr.next = childHead;
                childHead.prev = curr;
                curr.child = null;

                // Если был next — подключаем его после tail
                if (next != null)
                {
                    childTail.next = next;
                    next.prev = childTail;
                }

                last = childTail; // tail последнего уровня распаковки
            }
            else
            {
                last = curr; // обновляем tail
            }

            curr = next; // продолжаем по уровню
        }

        return last;
    }

    public Node Flatten1(Node head)
    {
        if (head == null)
        {
            return head;
        }

        Flat(head);
        return head;

        Node Flat(Node node)
        {
            var curr = node;
            while (true)
            {
                if (curr.child != null)
                {
                    var last = Flat(curr.child);
                    if (curr.next != null)
                    {
                        curr.next.prev = last;
                    }
                    last.next = curr.next;
                    curr.next = curr.child;
                    curr.child.prev = curr;
                    curr.child = null;
                }

                if (curr.next != null)
                {
                    curr = curr.next;
                }
                else
                {
                    break;
                }
            }

            return curr;
        }
    }
}
