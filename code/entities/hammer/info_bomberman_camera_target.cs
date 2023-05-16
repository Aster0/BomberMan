using Sandbox;
using Editor;

[Library( "info_bomberman_camera_target" ), HammerEntity]
[Title( "Player Spawnpoint" ), Category( "Camera" ), Icon( "camera" )]
[EditorModel( "models/editor/camera.vmdl" )]
public class BombermanCameraTarget : BombermanCamera
{
	public override void OnFrame()
	{
	}
	
	public new static void DrawGizmos( EditorContext context )
	{
		BombermanCamera.DrawGizmos(context);
	}
}
