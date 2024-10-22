using Grace.Cache;
using Grace.Model;
using Grace.Model.Repository;

namespace Grace.Common;

public static class Util
{
    public static T[] GetControlsByName<T>(Control parentControl, string name) where T : Control
    {
        return parentControl.Controls
            .OfType<T>()
            .Where(t => t.Name.Contains(name))
            .Reverse()
            .ToArray();
    }

    public static void AddItemNode(TreeNode parentNode, int itemId)
    {
        string itemName = ItemCache.Cache[itemId];
        TreeNode itemNode = new($"{itemId}: {itemName}");
        itemNode.Name = itemId.ToString();
        parentNode.Nodes.Add(itemNode);
    }

    public static TreeNode CreateItemNode(int itemId)
    {
        string itemName = ItemCache.Cache[itemId];
        TreeNode itemNode = new($"{itemId}: {itemName}");
        itemNode.Name = itemId.ToString();
        return itemNode;
    }

    public static async Task AddDropGroupNode(DropRepository dropRepository, TreeNode parentNode, int dropGroupId, string dropName)
    {
        TreeNode groupNode = new(dropName);
        parentNode.Nodes.Add(groupNode);
        groupNode.Tag = dropGroupId;
        groupNode.Name = dropGroupId.ToString();

        Drop? dropGroup = await dropRepository.GetGroupById(dropGroupId);
        if (dropGroup == null)
        {
            TreeNode noDataNode = new("No drops found");
            groupNode.Nodes.Add(noDataNode);

            return;
        }

        for (int i = 0; i < 10; i++)
        {
            int dropId = dropGroup.DropItemIds[i];

            if (dropId > 0)
                AddItemNode(groupNode, dropId);

            else if (dropId < 0)
                await AddDropGroupNode(dropRepository, groupNode, dropId, dropGroup.ItemNames[i]);

            else
                groupNode.Nodes.Add(new TreeNode("Empty slot"));
        }
    }

    public static async Task<TreeNode> CreateDropGroupNode(DropRepository dropRepository, int dropGroupId, string dropName)
    {
        Drop? dropGroup = await dropRepository.GetGroupById(dropGroupId);
        if (dropGroup == null)
            return new TreeNode("No drops found");

        TreeNode groupNode = new(dropName);
        groupNode.Tag = dropGroupId;
        groupNode.Name = dropGroupId.ToString();

        for (int i = 0; i < 10; i++)
        {
            int dropId = dropGroup.DropItemIds[i];

            if (dropId > 0)
                AddItemNode(groupNode, dropId);

            else if (dropId < 0)
                await AddDropGroupNode(dropRepository, groupNode, dropId, dropGroup.ItemNames[i]);

            else
                groupNode.Nodes.Add(new TreeNode("Empty slot"));
        }

        return groupNode;
    }

    public static TreeNode[] FindByName(this TreeNodeCollection collection, string name)
    {
        List<TreeNode> foundNodes = [];

        for (int i = 0; i < collection.Count; i++)
        {
            if (collection[i].Name == name)
            {
                foundNodes.Add(collection[i]);
            }
        }

        return foundNodes.ToArray();
    }
}
