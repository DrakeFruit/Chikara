using Sandbox;
using Sandbox.Movement;

[Title( "MoveMode - Slide" )]
[Category( "Chikara" )]
[Icon( "directions_run" )]
public sealed class MoveModeSlide : MoveMode, PlayerController.IEvents
{
	[RequireComponent] public ChikaraPlayer Player { get; set; }
	public bool Sliding = false;
	protected override void OnFixedUpdate()
	{
		if( Input.Down( "Duck" ) && Controller.Velocity.Length >= 10 ) Sliding = true;
		if( Input.Released( "Duck" ) ) Sliding = false;
	}
	public override void AddVelocity()
	{
		//if( Sliding ) 
		
	}
}
