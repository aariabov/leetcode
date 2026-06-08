namespace Tests.LinkedLists.Problems;

// [Удаление дубликатов в отсортированном списке](https://leetcode.com/problems/remove-duplicates-from-sorted-list/description/)
public class DeleteDuplicatesTests
{
    [Fact]
    public void Test()
    {
        var e0 = new ListNode(1);
        var e1 = new ListNode(1);
        var e2 = new ListNode(2);

        e0.next = e1;
        e1.next = e2;

        var result = DeleteDuplicates(e0);
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
    public void Test1()
    {
        var e0 = new ListNode(1);
        var e1 = new ListNode(1);
        var e2 = new ListNode(2);
        var e3 = new ListNode(3);
        var e4 = new ListNode(3);

        e0.next = e1;
        e1.next = e2;
        e2.next = e3;
        e3.next = e4;

        var result = DeleteDuplicates(e0);
        var list = new List<int>();
        var head = result;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 1, 2, 3 }, list.ToArray());
    }

    public ListNode DeleteDuplicates(ListNode head)
    {
        if (head == null)
        {
            return head;
        }

        var current = head;
        var prev = head;
        while (current.next != null)
        {
            if (current.val != current.next.val)
            {
                prev.next = current.next;
                prev = current.next;
            }

            current = current.next;
        }

        if (current.val == prev.val)
        {
            prev.next = null;
        }

        return head;
    }
}
