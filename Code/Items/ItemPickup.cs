using System;

namespace Chikara;
[Category( "Items" )]
public class ItemPickup : Component, Component.ITriggerListener
{
	[Property] public ItemDefinition Definition { get; set; }
	public PlayerController Controller { get; set; }
	public ChikaraPlayer Player { get; set; }
	protected override void OnStart()
	{
		LocalPosition += Vector3.Up * 5f;
		if ( Definition.Tilted ) LocalRotation = LocalRotation.Angles().WithPitch( -15 );
	}
	protected override void OnFixedUpdate()
	{
		LocalRotation = LocalRotation.Angles().WithYaw( LocalRotation.Yaw() + 1 );
	}
	public virtual void OnTriggerEnter( GameObject other )
	{
		Controller = other.GetComponent<PlayerController>();
		Player = other.GetComponent<ChikaraPlayer>();
		if ( !Controller.IsValid() || !Player.IsValid() ) return;

		var type = TypeLibrary.GetType( Definition.ItemComponent );
		var comp = Player.Components.Create( type );
		if ( comp is Item item )
		{
			item.Definition = Definition;
		}
		GameObject.Destroy();
	}
}
