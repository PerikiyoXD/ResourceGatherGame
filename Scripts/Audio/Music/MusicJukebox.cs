using Godot;

public partial class MusicJukebox : Node
{
	[Export]
	private AudioStream[] songs;

	private AudioStreamPlayer audioStreamPlayer;

	public override void _Ready()
	{
		audioStreamPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		audioStreamPlayer.Stream = GetRandomSong();
		audioStreamPlayer.Play();
	}

	private AudioStream GetRandomSong()
	{
		return songs[GD.Randi() % songs.Length];
	}

	public override void _Process(double delta)
	{
		base._Process(delta);

		if (!audioStreamPlayer.Playing)
		{
			audioStreamPlayer.Stream = GetRandomSong();
			audioStreamPlayer.Play();
		}
	}
}
