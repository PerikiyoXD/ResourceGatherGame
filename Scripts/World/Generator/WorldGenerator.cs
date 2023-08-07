using Godot;
using System;
using World.Cell;


public partial class WorldGenerator : Node3D
{
	[Export] private int seed;
	[Export] private Vector2 size;
	[Export] private FastNoiseLite fastNoiseLite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Generate();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Generate()
	{
		// FastNoiseLite noise = new()
		// {
		//     NoiseType = FastNoiseLite.NoiseTypeEnum.ValueCubic,
		//     Seed = seed,
		//     Frequency = 0.1f
		// };

		// Each cell is a 3x3x3 cube, so we need to offset the position by half of the size. 
		// Center of the map is 0,0,0, so we need to offset by half of the size.
		for (int x = 0; x < size.X; x++)
		{
			for (int z = 0; z < size.Y; z++)
			{
				var cellInstance = new WorldCellInstance();
				var noiseGeneratedPosition = new Vector3((x - size.X / 2) * 3 + 1.5f, fastNoiseLite.GetNoise2D(x, z) * 3, (z - size.Y / 2) * 3 + 1.5f);

				if (noiseGeneratedPosition.Y < 0)
				{
					cellInstance.cellType = WorldCellType.Water;
					cellInstance.Position = new Vector3(noiseGeneratedPosition.X, 0, noiseGeneratedPosition.Z);
				}
				else
				{
					var random = new Random();
					var randomType = (WorldCellType)random.Next(2, Enum.GetNames(typeof(WorldCellType)).Length);
					cellInstance.cellType = randomType;
					cellInstance.Position = noiseGeneratedPosition;
				}

				// GD.Print("WorldGenerator.cellInstance.Position: " + cellInstance.Position);

				AddChild(cellInstance);
			}
		}
	}
}
