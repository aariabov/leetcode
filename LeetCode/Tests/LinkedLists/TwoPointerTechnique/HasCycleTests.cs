namespace Tests.LinkedLists.TwoPointerTechnique;

/// <summary>
/// Определить, если ли цикл https://leetcode.com/explore/learn/card/linked-list/214/two-pointer-technique/1212/
/// </summary>
public class HasCycleTests
{
    [Fact]
    public void Test()
    {
        var zero = new ListNode(3);
        var first = new ListNode(2);
        var second = new ListNode(0);
        var three = new ListNode(4);

        zero.next = first;
        first.next = second;
        second.next = three;
        three.next = first;

        Assert.True(HasCycle(zero));
    }

    [Fact]
    public void Test1()
    {
        var zero = new ListNode(1);
        var first = new ListNode(2);

        zero.next = first;
        first.next = zero;

        Assert.True(HasCycle(zero));
    }

    [Fact]
    public void Test2()
    {
        var zero = new ListNode(1);
        var first = new ListNode(2);
        zero.next = first;
        Assert.False(HasCycle(zero));
    }

    [Fact]
    public void Test3()
    {
        var zero = new ListNode(1);
        Assert.False(HasCycle(zero));
    }

    public bool HasCycle(ListNode head)
    {
        if (head == null || head.next == null)
            return false;

        ListNode slow = head;
        ListNode fast = head.next;

        while (slow != fast)
        {
            if (fast == null || fast.next == null)
                return false;

            slow = slow.next;
            fast = fast.next.next;
        }

        return true;
    }

    public bool HasCycle1(ListNode head)
    {
        if (head?.next == null || head.next?.next == null)
        {
            return false;
        }

        var slow = head;
        var fast = head;
        do
        {
            slow = slow.next;
            fast = fast.next.next;
            if (fast.next == null || fast.next.next == null)
            {
                return false;
            }
        } while (fast != slow);

        return true;
    }
}
