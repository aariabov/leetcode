namespace Tests.LinkedLists.TwoPointerTechnique;

/// <summary>
/// Удалить n-ый узел с конца https://leetcode.com/explore/learn/card/linked-list/214/two-pointer-technique/1296/
/// </summary>
public class RemoveNthFromEndTests
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

        var result = RemoveNthFromEnd(e0, 2);
        var list = new List<int>();
        var head = result;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 1, 2, 3, 5 }, list.ToArray());
    }

    [Fact]
    public void Test1()
    {
        var e0 = new ListNode(1);

        var result = RemoveNthFromEnd(e0, 1);
        Assert.Null(result);
    }

    [Fact]
    public void Test2()
    {
        var e0 = new ListNode(1);
        var e1 = new ListNode(2);

        e0.next = e1;

        var result = RemoveNthFromEnd(e0, 1);
        var list = new List<int>();
        var head = result;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 1 }, list.ToArray());
    }

    public ListNode RemoveNthFromEnd(ListNode head, int n)
    {
        // создаём вспомогательный узел
        ListNode dummy = new ListNode(0);
        dummy.next = head;

        ListNode fast = dummy;
        ListNode slow = dummy;

        // смещаем fast на n+1 шагов вперед
        for (int i = 0; i <= n; i++)
        {
            fast = fast.next;
        }

        // двигаем оба указателя, пока fast не дойдёт до конца
        while (fast != null)
        {
            fast = fast.next;
            slow = slow.next;
        }

        // удаление нужного узла
        slow.next = slow.next.next;

        return dummy.next;
    }

    public ListNode RemoveNthFromEnd1(ListNode head, int n)
    {
        var i = 0;
        var el = head;
        do
        {
            i++;
            el = el.next;
        } while (el != null);

        el = head;
        var j = 0;
        while (j < i - n - 1)
        {
            j++;
            el = el.next;
        }

        // единственный элемент - null
        if (i == 1)
        {
            return null;
        }

        // первый элемент - второй
        if (i == n)
        {
            return el.next;
        }

        // последний - перепривязываем на null, возвращаем голову
        if (n == 1)
        {
            el.next = null;
            return head;
        }

        // средний - перепривязываем, возвращаем голову
        el.next = el.next.next;
        return head;
    }
}
