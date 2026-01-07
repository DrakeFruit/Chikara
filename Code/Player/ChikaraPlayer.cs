using Chikara;
using Sandbox;

[Category( "Chikara" )]
public partial class ChikaraPlayer : Component
{
	public static ChikaraPlayer Local => Game.ActiveScene.GetAll<ChikaraPlayer>().FirstOrDefault( x => !x.IsProxy );
	public static PlayerController LocalController => Local.GetComponent<PlayerController>();
	[Sync] public NetDictionary<ItemDefinition, int> Items { get; set; } = new();
	protected override void OnUpdate()
	{
		Scene.Camera.FieldOfView = Scene.Camera.FieldOfView
		.LerpTo( Preferences.FieldOfView * LocalController.Velocity.WithZ(0).Length
		.Remap(LocalController.WalkSpeed, 2000, 1, 2), 0.05f );
	}
}
