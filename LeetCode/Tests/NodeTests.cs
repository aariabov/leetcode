using FluentAssertions;

namespace Tests;

public class NodeTests
{
    [Fact]
    public void Test()
    {
        var n1 = new Node(1);
        var n2 = new Node(2);
        var n3 = new Node(3);
        var n4 = new Node(4);
        var n5 = new Node(5);
        var n6 = new Node(6);

        n1.children.AddRange([n3, n2, n4]);
        n3.children.AddRange([n5, n6]);

        var result = Node.BuildTree([1, null, 3, 2, 4, null, 5, 6]);
        result.Should().BeEquivalentTo(n1);
    }

    [Fact]
    public void Test1()
    {
        var n1 = new Node(1);
        var n2 = new Node(2);
        var n3 = new Node(3);
        var n4 = new Node(4);
        var n5 = new Node(5);
        var n6 = new Node(6);
        var n7 = new Node(7);
        var n8 = new Node(8);
        var n9 = new Node(9);
        var n10 = new Node(10);
        var n11 = new Node(11);
        var n12 = new Node(12);
        var n13 = new Node(13);
        var n14 = new Node(14);

        n1.children.AddRange([n2, n3, n4, n5]);
        n3.children.AddRange([n6, n7]);
        n4.children.AddRange([n8]);
        n5.children.AddRange([n9, n10]);
        n7.children.AddRange([n11]);
        n8.children.AddRange([n12]);
        n9.children.AddRange([n13]);
        n11.children.AddRange([n14]);

        var result = Node.BuildTree(
            [
                1,
                null,
                2,
                3,
                4,
                5,
                null,
                null,
                6,
                7,
                null,
                8,
                null,
                9,
                10,
                null,
                null,
                11,
                null,
                12,
                null,
                13,
                null,
                null,
                14,
            ]
        );
        result.Should().BeEquivalentTo(n1);
    }

    [Fact]
    public void Test2()
    {
        var n1 = new Node(1);
        var result = Node.BuildTree([1, null]);
        result.Should().BeEquivalentTo(n1);
    }

    [Fact]
    public void Test3()
    {
        var result = Node.BuildTree([]);
        result.Should().BeNull();
    }

    [Fact]
    public void Test4()
    {
        var result = Node.BuildTree(null);
        result.Should().BeNull();
    }
}
