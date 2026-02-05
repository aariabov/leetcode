using FluentAssertions;

namespace Tests.BinarySearchTree.HeightBalanced;

/// <summary>
/// [Построить бинарное дерево поиска из отсортированного массива](https://leetcode.com/explore/learn/card/introduction-to-data-structure-binary-search-tree/143/appendix-height-balanced-bst/1015/)
/// </summary>
public class SortedArrayToBSTTests
{
    [Fact]
    public void Test()
    {
        var expected = TreeNode.BuildTree([0, -3, 9, -10, null, 5]);
        var result = SortedArrayToBST([-10, -3, 0, 5, 9]);
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Test1()
    {
        var expected = TreeNode.BuildTree([3, 1]);
        var result = SortedArrayToBST([1, 3]);
        result.Should().BeEquivalentTo(expected);
    }

    public TreeNode SortedArrayToBST(int[] nums)
    {
        return Rec(nums);

        TreeNode? Rec(int[] nums)
        {
            if (nums.Length == 0)
            {
                return null;
            }

            if (nums.Length == 1)
            {
                return new TreeNode(nums[0]);
            }

            var center = nums.Length / 2;
            var node = new TreeNode(nums[center]);
            node.left = Rec(nums[0..center]);
            node.right = Rec(nums[(center + 1)..]);
            return node;
        }
    }
}
