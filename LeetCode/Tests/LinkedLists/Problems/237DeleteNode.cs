namespace Tests.LinkedLists.Problems;

// [Удалить узел в связанном списке](https://leetcode.com/problems/delete-node-in-a-linked-list/description/)
public class DeleteNodeTests
{
    [Fact]
    public void Test1()
    {
        var e0 = new ListNode(4);
        var e1 = new ListNode(5);
        var e2 = new ListNode(1);
        var e3 = new ListNode(9);

        e0.next = e1;
        e1.next = e2;
        e2.next = e3;

        DeleteNode(e1);
        var list = new List<int>();
        var head = e0;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 4, 1, 9 }, list.ToArray());
    }

    [Fact]
    public void Test2()
    {
        var e0 = new ListNode(4);
        var e1 = new ListNode(5);
        var e2 = new ListNode(1);
        var e3 = new ListNode(9);

        e0.next = e1;
        e1.next = e2;
        e2.next = e3;

        DeleteNode(e2);
        var list = new List<int>();
        var head = e0;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 4, 5, 9 }, list.ToArray());
    }

    public void DeleteNode(ListNode node)
    {
        // Копируем значение из следующего узла в текущий
        node.val = node.next.val;

        // Пропускаем следующий узел, связав текущий со следующим через один
        node.next = node.next.next;
    }

    // работает, но нет смысла копировать оставшиеся елементы
    public void MyDeleteNode(ListNode node)
    {
        while (node.next != null)
        {
            node.val = node.next.val;
            if (node.next.next == null)
            {
                node.next = null;
            }
            else
            {
                node = node.next;
            }
        }
    }
}
