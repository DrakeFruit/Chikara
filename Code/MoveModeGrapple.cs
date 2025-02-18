using Sandbox;
using Sandbox.Movement;
namespace Chikara;

[Title( "MoveMode - Grapple" )]
[Category( "Chikara" )]
[Icon( "directions_run" )]
public sealed class MoveModeGrapple : MoveMode, PlayerController.IEvents
{
	[RequireComponent] private ChikaraPlayer Player { get; set; }
	[Property] GameObject Hand { get; set; }
	private SceneTraceResult GrappleTrace { get; set; }
	private Vector3 HeadPosition { get; set; }
	private LineRenderer Renderer { get; set; }
	private GameObject Point1 { get; set; }
	private GameObject Point2 { get; set; }
	private bool Grappling = false;
	private string KeyBind = "reload";
	private float PrevGravity;
	protected override void OnStart()
	{
		GrappleSettings();
	}
	protected override void OnFixedUpdate()
	{
		HeadPosition = LocalPosition + Vector3.Up * (Controller.BodyHeight - Controller.EyeDistanceFromTop);
		if ( Input.Pressed( KeyBind ) )
		{
			if ( !Grappling ) //Below code runs once when button pressed
			{
				var cam = Scene.Camera;
				GrappleTrace = Scene.Trace.Ray( cam.LocalPosition, cam.LocalPosition + cam.LocalRotation.Forward * 50000 )
					.IgnoreGameObjectHierarchy( GameObject )
					.Run();
				
				if ( !GrappleTrace.Hit ) return; //Below code runs once if grapple hits
				
				if( Controller.IsOnGround ) Controller.Jump( LocalRotation.Up * 500 );
				Controller.PreventGrounding( .5f ); //Required for the player to stop sticking to the ground
				PrevGravity = Player.GravityMultiplier;
				Grappling = true;
			}
		}else if ( Input.Released( KeyBind ) )
		{
			Player.GravityMultiplier = PrevGravity;
			Grappling = false;
		}

		if ( !Grappling ) return; //Below code runs while grappling
		
		Controller.Body.Velocity += Scene.Camera.LocalRotation.Forward * 15;
		Controller.Body.Velocity += Rotation.LookAt( GrappleTrace.HitPosition - HeadPosition ).Forward * 10;
		Controller.Body.Velocity += Vector3.Zero.WithZ( GrappleTrace.HitPosition.z - LocalPosition.z ) / 50;
	}
	protected override void OnUpdate()
	{
		
	}
	protected override void OnPreRender()
	{
		if ( Scene.Camera is null ) return;

		var hud = Scene.Camera.Hud;
		hud.DrawCircle( new Vector2( Screen.Width / 2, Screen.Height / 2 ), 5, Color.Magenta );
		
		//Grapple stuff
		Renderer.Enabled = Grappling;
		Renderer.Points[0].LocalPosition = Hand.WorldPosition;
		if ( GrappleTrace.Hit ) Renderer.Points[1].LocalPosition = GrappleTrace.HitPosition;
	}
	public void GrappleSettings()
	{
		Renderer = Components.Create<LineRenderer>();
		Point1 = new();
		Point2 = new();
		Renderer.Points ??= new(); //IDK why this works tbh, credit to Yart
		Renderer.Points.Add( Point1 );
		Renderer.Points.Add( Point2 );
		Renderer.Enabled = false;
		
		//Settings
		Renderer.Color = Color.Black;
		Renderer.Width = 1.5f;
		Renderer.CastShadows = true;
		Renderer.StartCap = SceneLineObject.CapStyle.Rounded;
		Renderer.EndCap = SceneLineObject.CapStyle.Rounded;
		Renderer.Lighting = true;
	}
}
