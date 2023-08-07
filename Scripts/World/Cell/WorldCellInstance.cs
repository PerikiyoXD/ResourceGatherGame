using Godot;

namespace World.Cell
{
    public partial class WorldCellInstance : Node3D
    {
        [Export] public WorldCellType cellType;
        [Export] private MeshInstance3D meshInstance;

        public override void _Ready()
        {
            // GD.Print("WorldCellInstance.type: " + cellType);
            var modelPath = TypeModel.GetModelPath(cellType);
            // GD.Print("WorldCellInstance.modelPath: " + modelPath);
            var model = ResourceLoader.Load(modelPath) as Mesh;
            // GD.Print("WorldCellInstance.model: " + model);
            meshInstance = new MeshInstance3D
            {
                Mesh = model
            };
            AddChild(meshInstance);
            meshInstance.Transform = meshInstance.Transform.Translated(new Vector3(-1.5f, 0, 1.5f));
            // GD.Print("WorldCellInstance.meshInstance.Position: " + meshInstance.Transform.Origin);
        }
    }
}