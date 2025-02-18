using Chikara;
using Sandbox;

[Category( "Chikara" )]
public sealed class ChikaraPlayer : Component
{
	[RequireComponent] public PlayerController Controller { get; set; }
	public Dictionary<ItemDefinition, int> Items { get; set; } = new();
	public float GravityMultiplier { get; set; } = 1;
	protected override void OnFixedUpdate()
	{
		if( !Controller.IsOnGround ) Controller.Body.Velocity -= Scene.PhysicsWorld.Gravity / 1000 * GravityMultiplier;
	}
	public void OnPickup( ItemDefinition item )
	{
		
	}
}
