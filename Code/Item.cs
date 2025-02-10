using Chikara;

namespace Sandbox;

public class Item : Component
{
	public ChikaraPlayer player { get; set; }
	public PlayerController controller { get; set; }
	public ItemDefinition itemDefinition { get; set; }
	protected override void OnStart()
	{
		player = GetComponent<ChikaraPlayer>();
		controller = GetComponent<PlayerController>();
	}
}
