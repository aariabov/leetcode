using Tests.LinkedLists;

namespace Tests.Recursion1.PrincipleOfRecursion;

/// <summary>
/// [Swap пары в связанном списке](https://leetcode.com/explore/learn/card/recursion-i/250/principle-of-recursion/1681/)
/// </summary>
public class SwapPairsTests
{
    [Fact]
    public void Test()
    {
        var e1 = new ListNode(1);
        var e2 = new ListNode(2);
        var e3 = new ListNode(3);
        var e4 = new ListNode(4);

        e1.next = e2;
        e2.next = e3;
        e3.next = e4;

        var result = SwapPairs(e1);
        var list = new List<int>();
        var head = result;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 2, 1, 4, 3 }, list.ToArray());
    }

    [Fact]
    public void Test1()
    {
        var e1 = new ListNode(1);
        var e2 = new ListNode(2);
        var e3 = new ListNode(3);

        e1.next = e2;
        e2.next = e3;

        var result = SwapPairs(e1);
        var list = new List<int>();
        var head = result;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 2, 1, 3 }, list.ToArray());
    }

    [Fact]
    public void Test2()
    {
        var result = SwapPairs(null);
        Assert.Null(result);
    }

    [Fact]
    public void Test3()
    {
        var e1 = new ListNode(1);

        var result = SwapPairs(e1);
        var list = new List<int>();
        var head = result;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 1 }, list.ToArray());
    }

    public ListNode SwapPairs(ListNode head)
    {
        return Rec(head);

        ListNode Rec(ListNode? root)
        {
            if (root == null || root.next == null)
            {
                return root;
            }

            var swaped = Rec(root.next.next);
            var temp = root.next;
            root.next = swaped;
            temp.next = root;
            return temp;
        }
    }
}
