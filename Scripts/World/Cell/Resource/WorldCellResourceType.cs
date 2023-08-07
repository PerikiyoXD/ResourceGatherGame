namespace World.Cell.Resource
{
    public enum Type
    {
        Undefined,
        BasicTree,
    }

    public static class TypeModel
    {
        public static string GetModelPath(Type type)
        {
            return type switch
            {
                Type.BasicTree => "res://Assets/Models/World/Resource/Tree/BasicTree.obj",
                _ => "",
            };
        }
    }
}
