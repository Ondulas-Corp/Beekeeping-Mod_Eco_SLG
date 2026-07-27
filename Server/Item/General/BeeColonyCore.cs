using Eco.Core.Items;
using Eco.Gameplay.Items;
using Eco.Gameplay.DynamicValues;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using System;

namespace Beekeeping.Server
{
    /// <summary>
    /// <para>Server side item definition for the "Bee Colony Core" item.</para>
    /// <para>This item is obtained by harvesting wild bee swarms and is required to operate bee hives.</para>
    /// </summary>
	[Serialized]
	[LocDisplayName("Bee Colony Core")]
	[LocDescription("A small colony of wild bees harvest from wild swarms. Essential to establish and maintain domesticated bee hives.")]
	[Weight(100)]
	[MaxStackSize(10)]
	[Tag("BeeColonyCore")]
	[Fuel(75000)] // Around 2H
	[Ecopedia("Items", "Products", subPageName: "Bee Colony Core")]
	public class BeeColonyCoreItem : Item
	{
	}
}