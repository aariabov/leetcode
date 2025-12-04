namespace Tests.LinkedLists.ClassicProblems;

/// <summary>
/// Палиндром https://leetcode.com/explore/learn/card/linked-list/219/classic-problems/1209/
/// </summary>
public class IsPalindromeTests
{
    [Fact]
    public void Test()
    {
        var e0 = new ListNode(1);
        var e1 = new ListNode(2);
        var e2 = new ListNode(2);
        var e3 = new ListNode(1);

        e0.next = e1;
        e1.next = e2;
        e2.next = e3;

        var result = IsPalindrome(e0);
        Assert.True(result);
    }

    [Fact]
    public void Test1()
    {
        var e0 = new ListNode(1);
        var e1 = new ListNode(2);

        e0.next = e1;

        var result = IsPalindrome(e0);
        Assert.False(result);
    }

    [Fact]
    public void Test2()
    {
        var e0 = new ListNode(1);

        var result = IsPalindrome(e0);
        Assert.True(result);
    }

    [Fact]
    public void Test3()
    {
        var e0 = new ListNode(1);
        var e1 = new ListNode(0);
        var e2 = new ListNode(1);

        e0.next = e1;
        e1.next = e2;

        var result = IsPalindrome(e0);
        Assert.True(result);
    }

    public bool IsPalindrome(ListNode head)
    {
        if (head == null || head.next == null)
            return true;

        // 1. Найдём середину (slow окажется в середине)
        ListNode slow = head,
            fast = head;
        while (fast != null && fast.next != null)
        {
            slow = slow.next;
            fast = fast.next.next;
        }

        // 2. Разворачиваем вторую половину
        ListNode prev = null,
            curr = slow;
        while (curr != null)
        {
            var next = curr.next;
            curr.next = prev;
            prev = curr;
            curr = next;
        }

        // 3. Сравниваем первую половину и перевёрнутую вторую
        ListNode left = head,
            right = prev;
        while (right != null)
        {
            if (left.val != right.val)
                return false;
            left = left.next;
            right = right.next;
        }

        return true;
    }

    public bool IsPalindrome1(ListNode head)
    {
        if (head.next == null)
        {
            return true;
        }

        var length = 0;
        var current = head;
        while (current != null)
        {
            length++;
            current = current.next;
        }

        var half = length / 2;
        var i = 0;
        var secondHead = head;
        while (i < half)
        {
            secondHead = secondHead.next;
            i++;
        }

        if (length % 2 == 1)
        {
            secondHead = secondHead.next;
        }

        current = head;
        secondHead = ReverseList(secondHead);
        while (secondHead != null)
        {
            if (secondHead.val != current.val)
            {
                return false;
            }
            secondHead = secondHead.next;
            current = current.next;
        }
        return true;

        ListNode ReverseList(ListNode head)
        {
            if (head == null)
            {
                return null;
            }

            var oldHead = head;
            while (head.next != null)
            {
                var newHead = head.next;
                var headNext = head.next.next;
                newHead.next = oldHead;
                oldHead = newHead;
                head.next = headNext;
            }
            return oldHead;
        }
    }
}
