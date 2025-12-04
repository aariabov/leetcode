namespace Tests.LinkedLists.Conclusion;

/// <summary>
/// Смержить два отсортированных списка https://leetcode.com/explore/learn/card/linked-list/213/conclusion/1227/
/// </summary>
public class MergeTwoListsTests
{
    [Fact]
    public void Test()
    {
        var e0 = new ListNode(1);
        var e1 = new ListNode(2);
        var e2 = new ListNode(4);

        e0.next = e1;
        e1.next = e2;

        var l0 = new ListNode(1);
        var l1 = new ListNode(3);
        var l2 = new ListNode(4);

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
        Assert.Equal(new[] { 1, 1, 2, 3, 4, 4 }, list.ToArray());
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
        if (list1 == null || list2 == null)
        {
            return list1 ?? list2;
        }

        var cur1 = list1;
        var cur2 = list2;
        var isFirst = false;
        var head = list2;
        if (list1.val < list2.val)
        {
            head = list1;
            isFirst = true;
        }

        while (cur1 != null && cur2 != null)
        {
            if (isFirst)
            {
                if (cur1.next == null || cur2.val < cur1.next.val)
                {
                    var next1 = cur1.next;
                    cur1.next = cur2;
                    cur1 = next1;
                    isFirst = false;
                }
                else
                {
                    cur1 = cur1.next;
                }
            }
            else
            {
                if (cur2.next == null || cur1.val < cur2.next.val)
                {
                    var next2 = cur2.next;
                    cur2.next = cur1;
                    cur2 = next2;
                    isFirst = true;
                }
                else
                {
                    cur2 = cur2.next;
                }
            }
        }
        return head;
    }

    public ListNode MergeTwoLists1(ListNode list1, ListNode list2)
    {
        if (list1 == null)
            return list2;
        if (list2 == null)
            return list1;

        if (list1.val < list2.val)
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
}
