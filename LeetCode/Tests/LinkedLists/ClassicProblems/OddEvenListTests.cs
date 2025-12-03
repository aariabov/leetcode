namespace Tests.LinkedLists.ClassicProblems;

/// <summary>
/// Сначала элементы с нечетными индексами, а потом с четными https://leetcode.com/explore/learn/card/linked-list/219/classic-problems/1208/
/// </summary>
public class OddEvenListTests
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

        var result = OddEvenList(e0);
        var list = new List<int>();
        var head = result;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 1, 3, 5, 2, 4 }, list.ToArray());
    }

    [Fact]
    public void Test1()
    {
        var e0 = new ListNode(2);
        var e1 = new ListNode(1);
        var e2 = new ListNode(3);
        var e3 = new ListNode(5);
        var e4 = new ListNode(6);
        var e5 = new ListNode(4);
        var e6 = new ListNode(7);

        e0.next = e1;
        e1.next = e2;
        e2.next = e3;
        e3.next = e4;
        e4.next = e5;
        e5.next = e6;

        var result = OddEvenList(e0);
        var list = new List<int>();
        var head = result;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 2, 3, 6, 7, 1, 5, 4 }, list.ToArray());
    }

    [Fact]
    public void Test2()
    {
        var result = OddEvenList(null);
        Assert.Null(result);
    }

    public ListNode OddEvenList(ListNode head)
    {
        if (head == null || head.next == null)
            return head;

        ListNode odd = head;
        ListNode even = head.next;
        ListNode evenHead = even;

        while (even != null && even.next != null)
        {
            odd.next = even.next;
            odd = odd.next;

            even.next = odd.next;
            even = even.next;
        }

        odd.next = evenHead;
        return head;
    }

    public ListNode OddEvenList1(ListNode head)
    {
        if (head == null || head.next == null || head.next.next == null)
        {
            return head;
        }

        ListNode? oddPrev = null;
        ListNode? evenPrev = null;
        var evenHead = head.next;
        var current = head;
        var i = 1;
        while (current != null)
        {
            if (i % 2 == 1)
            {
                if (oddPrev != null)
                {
                    oddPrev.next = current;
                }
                oddPrev = current;
            }
            else
            {
                if (evenPrev != null)
                {
                    evenPrev.next = current;
                }

                evenPrev = current;
            }
            current = current.next;
            i++;
        }
        evenPrev.next = null;
        oddPrev.next = evenHead;
        return head;
    }
}
