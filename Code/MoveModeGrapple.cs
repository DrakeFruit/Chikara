using Sandbox;
using Sandbox.Movement;
namespace Chikara;

[Title( "MoveMode - Grapple" )]
[Category( "Chikara" )]
[Icon( "directions_run" )]
public sealed class MoveModeGrapple : MoveMode, PlayerController.IEvents
{
	public SceneTraceResult GrappleTrace { get; set; }
	public Vector3 HeadPosition { get; set; }
	public bool Grappling = false;
	protected override void OnStart()
	{
		
	}
	protected override void OnFixedUpdate()
	{
		HeadPosition = LocalPosition + Vector3.Up * (Controller.BodyHeight - Controller.EyeDistanceFromTop);
		if ( Input.Down( "reload" ) )
		{
			if ( !Grappling )
			{
				GrappleTrace = Scene.Trace.Ray( Scene.Camera.LocalPosition, Scene.Camera.LocalRotation.Forward * 1000 )
					.IgnoreGameObjectHierarchy( GameObject )
					.Run();
			}
			Grappling = true;
		}else Grappling = false;
	}
	protected override void OnUpdate()
	{
		if ( Grappling && GrappleTrace.Hit )
		{
			Gizmo.Draw.LineThickness = 10;
			Gizmo.Draw.Line( HeadPosition, GrappleTrace.HitPosition );
		}
	}
}
