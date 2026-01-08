using FluentAssertions;

namespace Tests;

public class TreeNodeTests
{
    [Fact]
    public void Test()
    {
        var e1 = new TreeNode(1);
        var l2 = new TreeNode(2);
        var l3 = new TreeNode(3);
        var l4 = new TreeNode(4);
        var r2 = new TreeNode(2);
        var r3 = new TreeNode(3);
        var r4 = new TreeNode(4);

        e1.left = l2;
        e1.right = r2;
        l2.left = l3;
        l2.right = l4;
        r2.left = r4;
        r2.right = r3;

        var result = TreeNode.BuildTree([1, 2, 2, 3, 4, 4, 3]);
        result.Should().BeEquivalentTo(e1);
    }

    [Fact]
    public void Test1()
    {
        var e1 = new TreeNode(1);
        var l2 = new TreeNode(2);
        var l3 = new TreeNode(3);
        var r2 = new TreeNode(2);
        var r3 = new TreeNode(3);

        e1.left = l2;
        e1.right = r2;
        l2.right = l3;
        r2.right = r3;

        var result = TreeNode.BuildTree([1, 2, 2, null, 3, null, 3]);
        result.Should().BeEquivalentTo(e1);
    }
}
