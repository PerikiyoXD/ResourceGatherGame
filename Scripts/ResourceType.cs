namespace ResourceGatherGame.Scripts
{
    public enum ResourceType
    {
        Unset,
        Wood,
        WoodPlank,
        Stone,
        StoneBrick,
        Iron,
        IronBar,
        GoldCoin,
    }

    public static class ResourceTypeIcon 
    {
        public static string GetIconPath(this ResourceType resourceType)
        {
            return resourceType switch
            {
                ResourceType.Wood => "res://Assets/Icons/wood.png",
                ResourceType.WoodPlank => "res://Assets/Icons/wood_plank.png",
                ResourceType.Stone => "res://Assets/Icons/stone.png",
                ResourceType.StoneBrick => "res://Assets/Icons/stone_brick.png",
                ResourceType.Iron => "res://Assets/Icons/iron.png",
                ResourceType.IronBar => "res://Assets/Icons/iron_bar.png",
                ResourceType.GoldCoin => "res://Assets/Icons/gold_coin.png",
                _ => "res://Assets/Icons/unknown.png",
            };
        }

        public static string GetResourceName(this ResourceType resourceType)
        {
            return resourceType switch
            {
                ResourceType.Wood => "Wood",
                ResourceType.WoodPlank => "Wood Plank",
                ResourceType.Stone => "Stone",
                ResourceType.StoneBrick => "Stone Brick",
                ResourceType.Iron => "Iron",
                ResourceType.IronBar => "Iron Bar",
                ResourceType.GoldCoin => "Coin",
                _ => "Unknown",
            };
        }
    }
}