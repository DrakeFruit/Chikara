namespace Chikara;
public class Item : Component, Component.ITriggerListener
{
	[Property] public ItemDefinition ItemDefinition { get; set; }
	public PlayerController Controller { get; set; }
	public ChikaraPlayer Player { get; set; }
	protected override void OnStart()
	{
		LocalPosition += Vector3.Up * 5f;
		LocalRotation = LocalRotation.Angles().WithPitch( -15 );
	}
	protected override void OnFixedUpdate()
	{
		LocalRotation = LocalRotation.Angles().WithYaw( LocalRotation.Yaw() + 1 );
	}
	public virtual void OnTriggerEnter( GameObject other )
	{
		Controller = other.GetComponent<PlayerController>();
		Player = other.GetComponent<ChikaraPlayer>();
		Player.Items.Add( ItemDefinition );
	}
}
