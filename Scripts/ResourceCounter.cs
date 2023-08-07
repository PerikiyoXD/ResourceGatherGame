
namespace ResourceGatherGame.Scripts
{
	using Godot;
	using System;

	public partial class ResourceCounter : Panel
	{
		[Export] private ResourceType resourceType;
		[Export] private int count;

		private Label label;
		private TextureRect icon;

		public override void _Ready()
		{
			label = GetNode<Label>("HBoxContainer/Label");
			label.TooltipText = resourceType.GetResourceName();
			label.Text = count.ToString();

			icon = GetNode<TextureRect>("HBoxContainer/TextureRect");
			icon.Texture = ResourceLoader.Load(resourceType.GetIconPath()) as Texture2D;
			icon.TooltipText = resourceType.GetResourceName();

			TooltipText = resourceType.GetResourceName();
		}

		public override void _Process(double delta)
		{
		}
	}
}
