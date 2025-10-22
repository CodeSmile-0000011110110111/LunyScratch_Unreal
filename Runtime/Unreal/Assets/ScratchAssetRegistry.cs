using UnrealSharp.CoreUObject;

namespace LunyScratch
{
	internal sealed class ScratchAssetRegistry : AssetRegistry.IAssetRegistry
	{
		private static UClass LoadBlueprintClass(String path)
		{
			if (String.IsNullOrWhiteSpace(path))
				return null;

			try
			{
				// reference
				// /Script/Engine.Blueprint'/Game/Prefabs/HitEffect.HitEffect'
				// object path
				// /Game/Prefabs/HitEffect.HitEffect
				// package path
				// /Game/Prefabs/HitEffect

				var filename = Path.GetFileName(path);
				path = $"/Game/{path}.{filename}_C";

				var quoted = path.StartsWith("Blueprint'", StringComparison.Ordinal) ? path : $"Blueprint'{path}'";
				GameEngine.Actions.LogInfo("Try Loading: " + quoted);
				var cls = UObject.StaticLoadObject<UClass>(quoted);
				if (cls != null)
					return cls;
			}
			catch (Exception e)
			{
				GameEngine.Actions.LogError($"Failed to load asset via path '{path}': {e}");
			}

			return null;
		}

		public T Get<T>(String path) where T : class, IEngineAsset
		{
			// Prefab (Blueprint Class) lookup
			if (typeof(T) == typeof(IEnginePrefabAsset) || typeof(T) == typeof(ScratchPrefabAsset))
			{
				var prefab = LoadBlueprintClass(path);
				if (prefab != null)
					return new ScratchPrefabAsset(prefab) as T;

				return GetPlaceholder<T>();
			}

			// Unknown asset types not supported yet
			return GetPlaceholder<T>();
		}

		public IEngineAsset Get(String path, Type assetType)
		{
			if (assetType == typeof(IEnginePrefabAsset) || assetType == typeof(ScratchPrefabAsset))
			{
				var prefab = LoadBlueprintClass(path);
				if (prefab != null)
					return new ScratchPrefabAsset(prefab);

				return GetPlaceholder(assetType);
			}

			return GetPlaceholder(assetType);
		}

		public T GetPlaceholder<T>() where T : class, IEngineAsset
		{
			var placeholder = new PlaceholderAsset();
			if (placeholder is T t)
				return t;

			GameEngine.Actions.LogWarn($"No placeholder available for asset type {typeof(T).Name}");

			return null;
		}

		public IEngineAsset GetPlaceholder(Type assetType)
		{
			var placeholder = new PlaceholderAsset();
			if (assetType.IsInstanceOfType(placeholder))
				return placeholder;

			// Support requesting by interface type
			if (assetType.IsAssignableFrom(typeof(PlaceholderAsset)))
				return placeholder;

			GameEngine.Actions.LogWarn($"No placeholder available for asset type {assetType?.Name}");

			return null;
		}

		private sealed class PlaceholderAsset : IEnginePrefabAsset, IEngineUIAsset, IEngineAudioAsset {}
	}
}
