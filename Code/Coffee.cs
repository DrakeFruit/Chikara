namespace Chikara;
public class Coffee : Item
{
	public override void OnTriggerEnter( GameObject other )
	{
		base.OnTriggerEnter(other);
		Controller.RunSpeed *= 1.05f;
	}
}
