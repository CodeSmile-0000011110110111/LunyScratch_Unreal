using System;

namespace LunyScratch
{
	internal sealed class ScratchAssetRegistry : AssetRegistry.IAssetRegistry
	{
		public T Get<T>(string path) where T : class, IEngineAsset => GetPlaceholder<T>();

		public IEngineAsset Get(string path, Type assetType) => GetPlaceholder(assetType);

		public T GetPlaceholder<T>() where T : class, IEngineAsset
		{
			var placeholder = new PlaceholderAsset();
			if (placeholder is T t)
				return t;
			throw new NotSupportedException($"No placeholder available for asset type {typeof(T).Name}");
		}

		public IEngineAsset GetPlaceholder(Type assetType)
		{
			var placeholder = new PlaceholderAsset();
			if (assetType.IsInstanceOfType(placeholder))
				return placeholder;
			// Support requesting by interface type
			if (assetType.IsAssignableFrom(typeof(PlaceholderAsset)))
				return placeholder;
			throw new NotSupportedException($"No placeholder available for asset type {assetType?.Name}");
		}

		private sealed class PlaceholderAsset : IEnginePrefabAsset, IEngineUIAsset, IEngineAudioAsset { }
	}
}
