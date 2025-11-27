namespace Tests.LinkedLists.TwoPointerTechnique;

/// <summary>
/// Определить, если ли цикл, и элемент, с которого цикл начинается https://leetcode.com/explore/learn/card/linked-list/214/two-pointer-technique/1214/
/// </summary>
public class DetectCycleTests
{
    [Fact]
    public void Test()
    {
        var zero = new ListNode(3);
        var first = new ListNode(2);
        var second = new ListNode(0);
        var three = new ListNode(-4);

        zero.next = first;
        first.next = second;
        second.next = three;
        three.next = first;

        var result = DetectCycle(zero);
        Assert.Equal(first, result);
    }

    [Fact]
    public void Test1()
    {
        var zero = new ListNode(1);
        var first = new ListNode(2);

        zero.next = first;
        first.next = zero;

        var result = DetectCycle(zero);
        Assert.Equal(zero, result);
    }

    [Fact]
    public void Test3()
    {
        var zero = new ListNode(1);
        var result = DetectCycle(zero);
        Assert.Null(result);
    }

    [Fact]
    public void Test4()
    {
        var e0 = new ListNode(-1);
        var e1 = new ListNode(-7);
        var e2 = new ListNode(7);
        var e3 = new ListNode(-4);
        var e4 = new ListNode(19);
        var e5 = new ListNode(6);
        var e6 = new ListNode(-9);
        var e7 = new ListNode(-5);
        var e8 = new ListNode(-2);
        var e9 = new ListNode(-5);

        e0.next = e1;
        e1.next = e2;
        e2.next = e3;
        e3.next = e4;
        e4.next = e5;
        e5.next = e6;
        e6.next = e7;
        e7.next = e8;
        e8.next = e9;
        e9.next = e6;

        var result = DetectCycle(e0);
        Assert.Equal(e6, result);
    }

    public class ListNode
    {
        public int val;
        public ListNode next;

        public ListNode(int x)
        {
            val = x;
            next = null;
        }
    }

    public ListNode DetectCycle(ListNode head)
    {
        if (head == null || head.next == null)
            return null;

        ListNode slow = head;
        ListNode fast = head;

        // 1. Проверяем, есть ли цикл, используя две скорости.
        while (fast != null && fast.next != null)
        {
            slow = slow.next;
            fast = fast.next.next;

            if (slow == fast)
            {
                // 2. Когда встретились — находим начало цикла.
                ListNode ptr = head;
                while (ptr != slow)
                {
                    ptr = ptr.next;
                    slow = slow.next;
                }
                return ptr;
            }
        }

        return null; // нет цикла
    }
}
