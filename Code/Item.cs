namespace Chikara;
public sealed class Item : Component, Component.ITriggerListener
{
	[Property] ItemDefinition ItemDefinition { get; set; }
	private PlayerController controller;
	private ChikaraPlayer player;
	protected override void OnStart()
	{
		
	}
	public void OnTriggerEnter( GameObject other )
	{
		controller = other.GetComponent<PlayerController>();
		player = other.GetComponent<ChikaraPlayer>();
		controller.RunSpeed *= 1.05f;
		player.Items.Add( ItemDefinition );
	}
}
