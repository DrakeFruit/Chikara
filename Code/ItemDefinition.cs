using Sandbox;
using System;

namespace Chikara;
[GameResource("Item Definition", "item", "An item, duh", Category = "Chikara", Icon = "inventory_2")]
public class ItemDefinition : GameResource
{
	public string Name { get; set; }
	public string Description { get; set; }
	[Property, TargetType(typeof(Item))] public Type ItemComponent { get; set; }
	public GameObject Prefab { get; set; }
	public Texture Icon { get; set; }
}
