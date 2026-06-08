namespace Tests.LinkedLists.Problems;

// [Оставить только уникальные элементы в отсортированном списке](https://leetcode.com/problems/remove-duplicates-from-sorted-list-ii/description/)
public class DeleteDuplicates2Tests
{
    [Fact]
    public void Test1()
    {
        var e0 = new ListNode(1);
        var e1 = new ListNode(2);
        var e2 = new ListNode(3);
        var e3 = new ListNode(3);
        var e4 = new ListNode(4);
        var e5 = new ListNode(4);
        var e6 = new ListNode(5);

        e0.next = e1;
        e1.next = e2;
        e2.next = e3;
        e3.next = e4;
        e4.next = e5;
        e5.next = e6;

        var result = DeleteDuplicates(e0);
        var list = new List<int>();
        var head = result;
        do
        {
            list.Add(head.val);
            head = head.next;
        } while (head != null);
        Assert.Equal(new[] { 1, 2, 5 }, list.ToArray());
    }

    [Fact]
    public void Test2()
    {
        var e0 = new ListNode(1);
        var e1 = new ListNode(1);
        var e2 = new ListNode(1);
        var e3 = new ListNode(2);
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
        Assert.Equal(new[] { 2, 3 }, list.ToArray());
    }

    [Fact]
    public void Test3()
    {
        var e0 = new ListNode(1);
        var e1 = new ListNode(1);

        e0.next = e1;

        var result = DeleteDuplicates(e0);
        Assert.Null(result);
    }

    public ListNode DeleteDuplicates(ListNode head)
    {
        // Проверка на пустой список или список из одного элемента
        if (head == null || head.next == null)
        {
            return head;
        }

        // Фиктивный узел перед началом списка для обработки удаления головы
        ListNode fake = new ListNode(0);
        fake.next = head;
        ListNode prev = fake; // Указатель на последний проверенный уникальный узел

        while (head != null)
        {
            // Если обнаружен дубликат
            if (head.next != null && head.val == head.next.val)
            {
                // Пропускаем все узлы с таким же значением
                while (head.next != null && head.val == head.next.val)
                {
                    head = head.next;
                }
                // Перемещаем указатель prev.next мимо всех дубликатов
                prev.next = head.next;
            }
            else
            {
                // Если дубликатов нет, просто сдвигаем prev вперед
                prev = prev.next;
            }
            // Переходим к следующему элементу
            head = head.next;
        }

        return fake.next;
    }

    // работает и быстро
    public ListNode MyDeleteDuplicates(ListNode head)
    {
        var current = head;
        var wasDupl = false;
        ListNode? resHead = null;
        ListNode? resLast = null;
        while (current != null)
        {
            var next = current.next;
            if (next == null || current.next.val != current.val)
            {
                if (!wasDupl)
                {
                    if (resHead == null)
                    {
                        resHead = current;
                        resLast = current;
                    }
                    else
                    {
                        resLast.next = current;
                        resLast = current;
                    }
                }
                wasDupl = false;
            }
            else
            {
                wasDupl = true;
            }

            current.next = null;
            current = next;
        }

        return resHead;
    }
}
