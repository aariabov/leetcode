using Tests.LinkedLists;

namespace Tests.Recursion1.Conclusion;

/// <summary>
/// [Смержить два отсортированных связанных списка](https://leetcode.com/explore/learn/card/recursion-i/253/conclusion/2382/)
/// </summary>
public class MergeTwoListsTests
{
    [Fact]
    public void Test()
    {
        var e0 = new ListNode(0);
        var e1 = new ListNode(2);
        var e2 = new ListNode(4);

        e0.next = e1;
        e1.next = e2;

        var l0 = new ListNode(1);
        var l1 = new ListNode(3);
        var l2 = new ListNode(5);

        l0.next = l1;
        l1.next = l2;

        var result = MergeTwoLists(e0, l0);
        var list = new List<int>();
        var head = result;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 0, 1, 2, 3, 4, 5 }, list.ToArray());
    }

    [Fact]
    public void Test1()
    {
        var result = MergeTwoLists(null, null);
        Assert.Null(result);
    }

    [Fact]
    public void Test2()
    {
        var l0 = new ListNode(0);

        var result = MergeTwoLists(null, l0);
        var list = new List<int>();
        var head = result;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 0 }, list.ToArray());
    }

    public ListNode MergeTwoLists(ListNode list1, ListNode list2)
    {
        // Базовые случаи
        if (list1 == null)
            return list2;
        if (list2 == null)
            return list1;

        // Рекурсивный шаг
        if (list1.val <= list2.val)
        {
            list1.next = MergeTwoLists(list1.next, list2);
            return list1;
        }
        else
        {
            list2.next = MergeTwoLists(list1, list2.next);
            return list2;
        }
    }

    // работает, но как-то тупо
    public ListNode MergeTwoListsMy(ListNode list1, ListNode list2)
    {
        if (list1 == null)
        {
            return list2;
        }

        if (list2 == null)
        {
            return list1;
        }

        return list1.val < list2.val
            ? Rec(list1.next, list2, list1, true)
            : Rec(list1, list2.next, list2, false);

        ListNode Rec(ListNode? node1, ListNode? node2, ListNode prev, bool wasFirst)
        {
            if (node1 == null && node2 == null)
            {
                return prev;
            }

            ListNode? next = null;
            if (node1 == null)
            {
                next = Rec(node1, node2.next, node2, false);
            }
            else if (node2 == null)
            {
                next = Rec(node1.next, node2, node1, true);
            }
            else if (node2.val < node1.val)
            {
                next = Rec(node1, node2.next, node2, false);
            }
            else
            {
                next = Rec(node1.next, node2, node1, true);
            }
            prev.next = next;
            return prev;
        }
    }
}
