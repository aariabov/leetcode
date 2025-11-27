namespace Tests.LinkedLists.TwoPointerTechnique;

/// <summary>
/// Пересечение двух списков https://leetcode.com/explore/learn/card/linked-list/214/two-pointer-technique/1215/
/// </summary>
public class GetIntersectionNodeTests
{
    [Fact]
    public void Test()
    {
        var a0 = new ListNode(4);
        var a1 = new ListNode(1);

        var b0 = new ListNode(5);
        var b1 = new ListNode(6);
        var b2 = new ListNode(1);

        var c0 = new ListNode(8);
        var c1 = new ListNode(4);
        var c2 = new ListNode(5);

        c0.next = c1;
        c1.next = c2;

        a0.next = a1;
        a1.next = c0;

        b0.next = b1;
        b1.next = b2;
        b2.next = c0;

        var result = GetIntersectionNode(a0, b0);
        Assert.Equal(c0, result);
    }

    [Fact]
    public void Test1()
    {
        var a0 = new ListNode(1);
        var a1 = new ListNode(9);
        var a2 = new ListNode(1);

        var b0 = new ListNode(3);

        var c0 = new ListNode(2);
        var c1 = new ListNode(4);

        c0.next = c1;

        a0.next = a1;
        a1.next = a2;
        a2.next = c0;

        b0.next = c0;

        var result = GetIntersectionNode(a0, b0);
        Assert.Equal(c0, result);
    }

    [Fact]
    public void Test2()
    {
        var a0 = new ListNode(2);
        var a1 = new ListNode(6);
        var a2 = new ListNode(4);

        var b0 = new ListNode(1);
        var b1 = new ListNode(5);

        a0.next = a1;
        a1.next = a2;

        b0.next = b1;

        var result = GetIntersectionNode(a0, b0);
        Assert.Null(result);
    }

    [Fact]
    public void Test3()
    {
        var a0 = new ListNode(2);

        var c0 = new ListNode(2);
        var c1 = new ListNode(4);
        var c2 = new ListNode(5);
        var c3 = new ListNode(4);

        c0.next = c1;
        c1.next = c2;
        c2.next = c3;

        a0.next = c0;

        var result = GetIntersectionNode(a0, c0);
        Assert.Equal(c0, result);
    }

    public ListNode GetIntersectionNode(ListNode headA, ListNode headB)
    {
        if (headA == null || headB == null)
            return null;

        var a = headA;
        var b = headB;
        while (a != b)
        {
            a = a == null ? headB : a.next;
            b = b == null ? headA : b.next;
        }

        return a;
    }

    public ListNode GetIntersectionNode1(ListNode headA, ListNode headB)
    {
        var a = headA;
        var b = headB;
        do
        {
            do
            {
                if (b == a)
                {
                    return a;
                }
                b = b.next;
            } while (b != null);
            a = a.next;
            b = headB;
        } while (a != null);

        return null;
    }
}
