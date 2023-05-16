using Sandbox;
using Editor;

[Library( "point_bomberman_camera" ), HammerEntity]
[Title( "Player Spawnpoint" ), Category( "Camera" ), Icon( "camera" )]
[EditorModel( "models/editor/camera.vmdl" )]
public partial class BombermanCamera : Entity
{
	[Property] [Net] public float NearZ { get; set; } = 1;
	[Property] [Net] public float FarZ { get; set; } = 5000;
	
	[Property( Title = "Field Of View" )]
	[Net] public float Fov { get; set; } = 75;
	
	[Property( Title = "Use Ortho" )]
	[Net] public bool IsOrtho { get; set; } = false;
	
	[Property( Title = "Ortho size" )]
	[Net] public Vector2 Size { get; set; } = new (100f);

	[GameEvent.Client.Frame]
	public virtual void OnFrame()
	{
		Camera.Rotation = this.Rotation;
		Camera.Position = this.Position;

		Camera.ZNear = NearZ;
		Camera.ZFar = FarZ;
		
		Camera.FieldOfView = Fov;
		
		Camera.Main.Ortho = IsOrtho;
		
		Camera.Main.OrthoWidth = Size.x;
		Camera.Main.OrthoHeight = Size.y;
	}
	
	[Input]
	public void SetPosition(Vector3 newPosition)
	{
		Position = newPosition;
	}
	[Input]
	public void SetOrthoSize(Vector3 newOrtho)
	{
		Size = newOrtho;
	}
	[Input]
	public void LookAt(Vector3 lookTarget)
	{
		Rotation.LookAt(lookTarget, Vector3.Up);
	}
	[Input]
	public void LookAtEntity(string name)
	{
		var entity = Entity.FindByName( name );
		Rotation.LookAt(entity.Position, Vector3.Up);
	}
	[Input]
	public void SetRotation(Angles newRotation)
	{
		Rotation = newRotation.ToRotation();
	}
	[Input]
	public void SetFromTarget(string name)
	{
		if ( Entity.FindByName( name ) is not BombermanCameraTarget target )
		{
			Log.Error($"Entity {name} is not a Camera Target");
			return;
		}

		Rotation = target.Rotation;
		Position = target.Position;

		NearZ = target.NearZ;
		FarZ = target.FarZ;

		Fov = target.Fov;

		IsOrtho = target.IsOrtho;

		Size = target.Size;
	}
	
	public static void DrawGizmos( EditorContext context )
	{
		var isOrtho = context.Target.GetProperty( "IsOrtho" ).GetValue<bool>();
		if (!isOrtho) return;
		var size = context.Target.GetProperty( "Size" ).GetValue<Vector2>();
		var farz = context.Target.GetProperty( "FarZ" ).GetValue<float>();
		var nearz = context.Target.GetProperty( "NearZ" ).GetValue<float>();
		
		// Ghost
		Gizmo.Draw.Color = Color.White;
		var halfSize = (Vector3.Left * size.x + Vector3.Up * size.y)/2;
		Gizmo.Draw.LineBBox( new BBox(Vector3.Forward*nearz-halfSize, Vector3.Forward*farz+halfSize));
	}
}
