using Chikara;
using Sandbox;

[Category( "Chikara" )]
public sealed class ChikaraPlayer : Component
{
	public Dictionary<ItemDefinition, int> Items { get; set; } = new();
	protected override void OnFixedUpdate()
	{
		
	}
	public void OnPickup( ItemDefinition item )
	{
		
	}
}
