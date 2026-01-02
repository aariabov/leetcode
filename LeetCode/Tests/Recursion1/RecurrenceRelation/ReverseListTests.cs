using Tests.LinkedLists;

namespace Tests.Recursion1.RecurrenceRelation;

/// <summary>
/// [Реверс связанного списка](https://leetcode.com/explore/learn/card/recursion-i/251/scenario-i-recurrence-relation/2378/)
/// </summary>
public class ReverseListTests
{
    [Fact]
    public void Test()
    {
        var e1 = new ListNode(1);
        var e2 = new ListNode(2);
        var e3 = new ListNode(3);
        var e4 = new ListNode(4);
        var e5 = new ListNode(5);

        e1.next = e2;
        e2.next = e3;
        e3.next = e4;
        e4.next = e5;

        var result = ReverseList(e1);
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
        var e1 = new ListNode(1);
        var e2 = new ListNode(2);

        e1.next = e2;

        var result = ReverseList(e1);
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

        ListNode newHead = ReverseList(head.next); // один раз вернем и больше не изменяем
        head.next.next = head;
        head.next = null;

        return newHead;
    }

    // работает хорошо и быстро
    public ListNode ReverseListЬн(ListNode head)
    {
        if (head == null)
        {
            return head;
        }

        var newHead = head;
        Rec(head);
        return newHead;

        ListNode Rec(ListNode? node)
        {
            if (node.next == null)
            {
                newHead = node;
                return node;
            }

            var lastNode = Rec(node.next);
            lastNode.next = node;
            node.next = null;
            return node;
        }
    }
}
