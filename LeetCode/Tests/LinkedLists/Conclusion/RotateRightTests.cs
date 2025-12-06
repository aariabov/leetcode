namespace Tests.LinkedLists.Conclusion;

/// <summary>
/// Повернуть список на количество элементов https://leetcode.com/explore/learn/card/linked-list/213/conclusion/1295/
/// </summary>
public class RotateRightTests
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

        var result = RotateRight(e0, 2);
        var list = new List<int>();
        var head = result;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 4, 5, 1, 2, 3 }, list.ToArray());
    }

    [Fact]
    public void Test1()
    {
        var e0 = new ListNode(0);
        var e1 = new ListNode(1);
        var e2 = new ListNode(2);

        e0.next = e1;
        e1.next = e2;

        var result = RotateRight(e0, 4);
        var list = new List<int>();
        var head = result;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 2, 0, 1 }, list.ToArray());
    }

    [Fact]
    public void Test2()
    {
        var result = RotateRight(null, 0);
        Assert.Equal(null, result);
    }

    [Fact]
    public void Test3()
    {
        var e0 = new ListNode(1);
        var e1 = new ListNode(2);

        e0.next = e1;

        var result = RotateRight(e0, 0);
        var list = new List<int>();
        var head = result;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 1, 2 }, list.ToArray());
    }

    [Fact]
    public void Test4()
    {
        var e0 = new ListNode(1);
        var e1 = new ListNode(2);

        e0.next = e1;

        var result = RotateRight(e0, 2);
        var list = new List<int>();
        var head = result;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 1, 2 }, list.ToArray());
    }

    public ListNode RotateRight(ListNode head, int k)
    {
        if (head == null || head.next == null || k == 0)
            return head;

        // Find length and last node
        ListNode tail = head;
        int length = 1;
        while (tail.next != null)
        {
            tail = tail.next;
            length++;
        }

        // Make it circular
        tail.next = head;

        // Effective rotations
        k = k % length;
        int stepsToNewHead = length - k;

        // Find new head
        ListNode newTail = tail;
        while (stepsToNewHead-- > 0)
        {
            newTail = newTail.next;
        }

        // Break the cycle
        ListNode newHead = newTail.next;
        newTail.next = null;

        return newHead;
    }

    public ListNode RotateRight1(ListNode head, int k)
    {
        if (head == null || head.next == null || k == 0)
        {
            return head;
        }

        var count = 0;
        var cur = head;
        while (cur != null)
        {
            count++;
            cur = cur.next;
        }

        var kk = k % count;
        if (kk == 0)
        {
            return head;
        }

        cur = head;
        var i = count - kk;
        var j = 1;
        while (j < i)
        {
            cur = cur.next;
            j++;
        }

        var newHead = cur.next;
        cur.next = null;

        cur = newHead;
        while (cur.next != null)
        {
            cur = cur.next;
        }
        cur.next = head;

        return newHead;
    }
}
