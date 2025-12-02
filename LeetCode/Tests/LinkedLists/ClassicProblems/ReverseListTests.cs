namespace Tests.LinkedLists.ClassicProblems;

/// <summary>
/// Реверс связанного списка https://leetcode.com/explore/learn/card/linked-list/219/classic-problems/1205/
/// </summary>
public class ReverseListTests
{
    [Fact]
    public void Test()
    {
        var e0 = new ListNode(1);
        var e1 = new ListNode(2);
        var e2 = new ListNode(3);
        var e3 = new ListNode(4);
        var e4 = new ListNode(5);

        e0.next = e1;
        e1.next = e2;
        e2.next = e3;
        e3.next = e4;

        var result = ReverseList(e0);
        var list = new List<int>();
        var head = result;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 5, 4, 3, 2, 1 }, list.ToArray());
    }

    [Fact]
    public void Test1()
    {
        var e0 = new ListNode(1);
        var e1 = new ListNode(2);

        e0.next = e1;

        var result = ReverseList(e0);
        var list = new List<int>();
        var head = result;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 2, 1 }, list.ToArray());
    }

    [Fact]
    public void Test2()
    {
        var result = ReverseList(null);
        Assert.Null(result);
    }

    public ListNode ReverseList(ListNode head)
    {
        if (head == null || head.next == null)
            return head;

        var newHead = ReverseList(head.next);
        head.next.next = head;
        head.next = null;
        return newHead;
    }

    public ListNode MyReverseList(ListNode head)
    {
        if (head == null || head.next == null)
        {
            return head;
        }
        var res = MyReverse(head);
        return res.hewHead;

        (ListNode hewHead, ListNode last) MyReverse(ListNode head)
        {
            if (head.next == null)
            {
                return (head, head);
            }
            var tuple = MyReverse(head.next);
            head.next = null;
            tuple.last.next = head;
            return (tuple.hewHead, tuple.last.next);
        }
    }

    public ListNode ReverseList1(ListNode head)
    {
        if (head == null)
        {
            return null;
        }

        var oldHead = head;
        while (head.next != null)
        {
            var newHead = head.next;
            var headNext = head.next.next;
            newHead.next = oldHead;
            oldHead = newHead;
            head.next = headNext;
        }
        return oldHead;
    }
}
