namespace Chikara;

[Category( "Items" )]
public class Coffee : Item
{
	protected override void OnStart()
	{
		base.OnStart();
		controller.RunSpeed *= 1.05f;
	}
}
