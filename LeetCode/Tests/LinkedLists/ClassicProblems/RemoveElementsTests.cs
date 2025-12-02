namespace Tests.LinkedLists.ClassicProblems;

/// <summary>
/// Удалить элементы из связанного списка https://leetcode.com/explore/learn/card/linked-list/219/classic-problems/1207/
/// </summary>
public class RemoveElementsTests
{
    [Fact]
    public void Test()
    {
        var e0 = new ListNode(1);
        var e1 = new ListNode(2);
        var e2 = new ListNode(6);
        var e3 = new ListNode(3);
        var e4 = new ListNode(4);
        var e5 = new ListNode(5);
        var e6 = new ListNode(6);

        e0.next = e1;
        e1.next = e2;
        e2.next = e3;
        e3.next = e4;
        e4.next = e5;
        e5.next = e6;

        var result = RemoveElements(e0, 6);
        var list = new List<int>();
        var head = result;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 1, 2, 3, 4, 5 }, list.ToArray());
    }

    [Fact]
    public void Test1()
    {
        var e0 = new ListNode(7);
        var e1 = new ListNode(7);
        var e2 = new ListNode(7);
        var e3 = new ListNode(7);

        e0.next = e1;
        e1.next = e2;
        e2.next = e3;

        var result = RemoveElements(e0, 7);
        Assert.Null(result);
    }

    [Fact]
    public void Test2()
    {
        var result = RemoveElements(null, 7);
        Assert.Null(result);
    }

    public ListNode RemoveElements(ListNode head, int val)
    {
        // Создаем фиктивный узел, чтобы удобно удалять элементы с головы списка
        ListNode dummy = new ListNode(0);
        dummy.next = head;

        ListNode current = dummy;

        while (current.next != null)
        {
            if (current.next.val == val)
            {
                current.next = current.next.next; // пропускаем узел
            }
            else
            {
                current = current.next;
            }
        }

        return dummy.next;
    }

    public ListNode RemoveElements1(ListNode head, int val)
    {
        if (head == null)
        {
            return null;
        }
        while (head != null && head.val == val)
        {
            head = head.next;
        }
        if (head == null)
        {
            return null;
        }

        var current = head.next;
        var prev = head;
        while (current != null)
        {
            if (current.val == val)
            {
                while (current != null && current.val == val)
                {
                    current = current.next;
                }

                prev.next = current;
            }
            else
            {
                prev = current;
                current = current.next;
            }
        }

        return head;
    }
}
