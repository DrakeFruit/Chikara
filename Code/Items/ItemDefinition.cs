using Sandbox;
using Sandbox.UI;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Chikara;
[AssetType( Category = "Chikara", Extension = "item", Name = "Item Definition", Flags = AssetTypeFlags.IncludeThumbnails )]
public class ItemDefinition : GameResource
{
	public string Name { get; set; }
	public string Description { get; set; }
	[TargetType(typeof(Item))] public Type ItemComponent { get; set; }
	public GameObject Prefab { get; set; }
	[Description( "Tilts the item 15 degrees on the ground and in previews" )] public bool Tilted { get; set; } = true;
	[Hide] public Texture Icon => CreateIcon();

	public Texture CreateIcon()
	{
		return Texture.White;
	}
}
