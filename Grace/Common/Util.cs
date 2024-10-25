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

    public static void AddItemNode(
        TreeNodeCollection parentCollection,
        int itemId,
        ContextMenuStrip? contextMenu = null)
    {
        string itemName = ItemCache.Cache[itemId];
        TreeNode itemNode = new($"{itemId}: {itemName}");
        itemNode.Name = itemId.ToString();

        if (contextMenu != null)
            itemNode.ContextMenuStrip = contextMenu;

        parentCollection.Add(itemNode);
    }

    public static async Task AddDropGroupNode(
        DropRepository dropRepository,
        TreeNodeCollection parentCollection,
        int dropGroupId,
        string dropName,
        ContextMenuStrip? contextMenu = null)
    {
        TreeNode groupNode = new(dropName);
        parentCollection.Add(groupNode);
        groupNode.Tag = dropGroupId;
        groupNode.Name = dropGroupId.ToString();

        if (contextMenu != null)
            groupNode.ContextMenuStrip = contextMenu;

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
                AddItemNode(groupNode.Nodes, dropId, contextMenu);

            else if (dropId < 0)
                await AddDropGroupNode(dropRepository, groupNode.Nodes, dropId, dropGroup.ItemNames[i], contextMenu);

            else
                groupNode.Nodes.Add(new TreeNode("Empty slot"));
        }
    }

    public static TreeNode[] FindByName(
        this TreeNodeCollection treeNodeCollectionToLookIn,
        string key,
        bool searchAllChildren)
    {
        List<TreeNode> foundNodes = FindInternal(key, searchAllChildren, treeNodeCollectionToLookIn, new List<TreeNode>());

        return foundNodes.ToArray();
    }

    private static List<TreeNode> FindInternal(
        string key,
        bool searchAllChildren,
        TreeNodeCollection treeNodeCollectionToLookIn,
        List<TreeNode> foundTreeNodes)
    {
        for (int i = 0; i < treeNodeCollectionToLookIn.Count; i++)
        {
            if (treeNodeCollectionToLookIn[i] is null)
                continue;

            if (treeNodeCollectionToLookIn[i].Name == key)
            {
                foundTreeNodes.Add(treeNodeCollectionToLookIn[i]);
            }
        }

        if (searchAllChildren)
        {
            for (int i = 0; i < treeNodeCollectionToLookIn.Count; i++)
            {
                if (treeNodeCollectionToLookIn[i] is null)
                {
                    continue;
                }

                if ((treeNodeCollectionToLookIn[i].Nodes is not null) && treeNodeCollectionToLookIn[i].Nodes.Count > 0)
                {
                    // If it has a valid child collection, append those results to our collection.
                    foundTreeNodes = FindInternal(key, searchAllChildren, treeNodeCollectionToLookIn[i].Nodes, foundTreeNodes);
                }
            }
        }

        return foundTreeNodes;
    }
}
