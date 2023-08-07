namespace World.Cell
{
    public enum WorldCellType
    {
        Undefined,
        Water,
        GrassPlain,
    }

    public static class TypeModel
    {
        public static string GetModelPath(WorldCellType type)
        {
            return type switch
            {
                WorldCellType.Water => "res://Assets/Models/World/Ground/Water/Water.obj",
                WorldCellType.GrassPlain => "res://Assets/Models/World/Ground/Grass/GrassPlain.obj",
                _ => "",
            };
        }
    }
}