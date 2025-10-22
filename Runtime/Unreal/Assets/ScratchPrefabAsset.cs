using UnrealSharp.CoreUObject;
using UnrealSharp.Engine;

namespace LunyScratch
{
	/// <summary>
	/// Unreal prefab asset wrapper. Represents a Blueprint Class (Actor subclass) that can be spawned.
	/// </summary>
	internal sealed class ScratchPrefabAsset : IEnginePrefabAsset
	{
		public UClass BlueprintClass { get; }

		public ScratchPrefabAsset(UClass blueprintClass)
		{
			BlueprintClass = blueprintClass;
		}
	}
}
