namespace Tests.LinkedLists.Conclusion;

/// <summary>
/// Сложить два связанных списка https://leetcode.com/explore/learn/card/linked-list/213/conclusion/1228/
/// </summary>
public class AddTwoNumbersTests
{
    [Fact]
    public void Test()
    {
        var e0 = new ListNode(2);
        var e1 = new ListNode(4);
        var e2 = new ListNode(3);

        e0.next = e1;
        e1.next = e2;

        var l0 = new ListNode(5);
        var l1 = new ListNode(6);
        var l2 = new ListNode(4);

        l0.next = l1;
        l1.next = l2;

        var result = AddTwoNumbers(e0, l0);
        var list = new List<int>();
        var head = result;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 7, 0, 8 }, list.ToArray());
    }

    [Fact]
    public void Test1()
    {
        var e0 = new ListNode(0);

        var l0 = new ListNode(0);

        var result = AddTwoNumbers(e0, l0);
        var list = new List<int>();
        var head = result;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 0 }, list.ToArray());
    }

    [Fact]
    public void Test2()
    {
        var e0 = new ListNode(9);
        var e1 = new ListNode(9);
        var e2 = new ListNode(9);
        var e3 = new ListNode(9);
        var e4 = new ListNode(9);
        var e5 = new ListNode(9);
        var e6 = new ListNode(9);

        e0.next = e1;
        e1.next = e2;
        e2.next = e3;
        e3.next = e4;
        e4.next = e5;
        e5.next = e6;

        var l0 = new ListNode(9);
        var l1 = new ListNode(9);
        var l2 = new ListNode(9);
        var l3 = new ListNode(9);

        l0.next = l1;
        l1.next = l2;
        l2.next = l3;

        var result = AddTwoNumbers(e0, l0);
        var list = new List<int>();
        var head = result;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 8, 9, 9, 9, 0, 0, 0, 1 }, list.ToArray());
    }

    [Fact]
    public void Test3()
    {
        var e0 = new ListNode(2);
        var e1 = new ListNode(4);
        var e2 = new ListNode(9);

        e0.next = e1;
        e1.next = e2;

        var l0 = new ListNode(5);
        var l1 = new ListNode(6);
        var l2 = new ListNode(4);
        var l3 = new ListNode(9);

        l0.next = l1;
        l1.next = l2;
        l2.next = l3;

        var result = AddTwoNumbers(e0, l0);
        var list = new List<int>();
        var head = result;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 7, 0, 4, 0, 1 }, list.ToArray());
    }

    public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        ListNode dummy = new ListNode(0);
        ListNode current = dummy;

        int carry = 0;

        while (l1 != null || l2 != null || carry != 0)
        {
            int x = (l1 != null) ? l1.val : 0;
            int y = (l2 != null) ? l2.val : 0;

            int sum = x + y + carry;
            carry = sum / 10;

            current.next = new ListNode(sum % 10);
            current = current.next;

            if (l1 != null)
                l1 = l1.next;
            if (l2 != null)
                l2 = l2.next;
        }

        return dummy.next;
    }

    public ListNode AddTwoNumbers1(ListNode l1, ListNode l2)
    {
        var cur1 = l1;
        var cur2 = l2;
        var prevVal = 0;
        ListNode? prevNode = null;
        var isFirst = l1 != null;
        while (cur1 != null || cur2 != null)
        {
            var sum = 0;
            if (cur1 != null)
            {
                sum = cur1.val + (cur2?.val ?? 0) + prevVal;
                cur1.val = sum > 9 ? sum % 10 : sum;
                prevNode = cur1;
            }
            else
            {
                sum = cur2.val + (cur1?.val ?? 0) + prevVal;
                cur2.val = sum > 9 ? sum % 10 : sum;
                prevNode = cur2;
            }

            if (cur1 != null && cur1.next == null && cur2 != null && isFirst)
            {
                cur1.next = cur2.next;
                cur1 = null;
            }

            if (cur1 != null)
            {
                cur1 = cur1.next;
            }

            if (cur2 != null)
            {
                cur2 = cur2.next;
            }

            prevVal = sum / 10;
        }

        if (prevVal == 1)
        {
            var last = new ListNode(1);
            prevNode.next = last;
        }

        return l1;
    }
}
