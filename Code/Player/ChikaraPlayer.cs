using Chikara;
using Sandbox;

[Category( "Chikara" )]
public partial class ChikaraPlayer : Component, Component.IDamageable
{
	public static ChikaraPlayer Local => Game.ActiveScene.GetAll<ChikaraPlayer>().FirstOrDefault( x => !x.IsProxy );
	public static PlayerController LocalController => Local.GetComponent<PlayerController>();
	[Sync] public NetDictionary<ItemDefinition, int> Items { get; set; } = new();
	[Sync] public float Health { get; set; } = 100f;

	public void OnDamage( in DamageInfo damage )
	{
		Health -= damage.Damage;
	}

	protected override void OnUpdate()
	{
		if ( IsProxy ) return;
		Scene.Camera.FieldOfView = Scene.Camera.FieldOfView
		.LerpTo( Preferences.FieldOfView * LocalController.Velocity.WithZ(0).Length
		.Remap(LocalController.WalkSpeed, 2000, 1, 2), 0.05f );
	}

	protected override void OnFixedUpdate()
	{
		if ( IsProxy ) return;

		if ( Health <= 0f )
		{
			Health = 100f;
			WorldPosition = Vector3.Zero;
		}
	}
}
