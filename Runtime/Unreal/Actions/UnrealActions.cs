using UnrealSharp.CoreUObject;
using UnrealSharp.Engine;
using UnrealSharp.InputCore;

namespace LunyScratch
{
	public sealed class UnrealActions : IEngineActions
	{
		private static APlayerController? PlayerController => UGameplayStatics.GetPlayerController(0);

		public Double GetFixedDeltaTimeInSeconds() => 1.0 / 60.0;
		public Double GetCurrentTimeInSeconds() => UGameplayStatics.TimeSeconds;

		public Boolean IsKeyPressed(Key key)
		{
			var pc = PlayerController;
			if (pc == null)
				return false;

			FKey fkey;
			if (key == Key.Any)
			{
				// Check a broad set of keys
				foreach (var k in Enum.GetValues<Key>())
				{
					fkey = Remap.ToUnrealKey(key);
					if (!fkey.IsValid)
						continue;

					if (pc.IsInputKeyDown(fkey))
						return true;
				}
				return false;
			}

			fkey = Remap.ToUnrealKey(key);
			return fkey.IsValid && pc.IsInputKeyDown(fkey);
		}

		public Boolean IsKeyJustPressed(Key key)
		{
			var pc = PlayerController;
			if (pc == null)
				return false;

			if (key == Key.Any)
			{
				foreach (var k in Enum.GetValues<Key>())
				{
					var fkey = Remap.ToUnrealKey(k);
					if (!IsValid(fkey))
						continue;

					if (pc.WasInputKeyJustPressed(fkey))
						return true;
				}
				return false;
			}

			var keyF = Remap.ToUnrealKey(key);
			return IsValid(keyF) && pc.WasInputKeyJustPressed(keyF);
		}

		public Boolean IsKeyJustReleased(Key key)
		{
			var pc = PlayerController;
			if (pc == null)
				return false;

			if (key == Key.Any)
			{
				foreach (var k in Enum.GetValues<Key>())
				{
					var fkey = Remap.ToUnrealKey(k);
					if (!IsValid(fkey))
						continue;

					if (pc.WasInputKeyJustReleased(fkey))
						return true;
				}
				return false;
			}

			var keyF = Remap.ToUnrealKey(key);
			return IsValid(keyF) && pc.WasInputKeyJustReleased(keyF);
		}

		public Boolean IsMouseButtonPressed(MouseButton button)
		{
			var pc = PlayerController;
			if (pc == null)
				return false;

			var fkey = Remap.ToUnrealMouseButton(button);
			return IsValid(fkey) && pc.IsInputKeyDown(fkey);
		}

		public Boolean IsMouseButtonJustPressed(MouseButton button)
		{
			var pc = PlayerController;
			if (pc == null)
				return false;

			var fkey = Remap.ToUnrealMouseButton(button);
			return IsValid(fkey) && pc.WasInputKeyJustPressed(fkey);
		}

		public Boolean IsMouseButtonJustReleased(MouseButton button)
		{
			var pc = PlayerController;
			if (pc == null)
				return false;

			var fkey = Remap.ToUnrealMouseButton(button);
			return IsValid(fkey) && pc.WasInputKeyJustReleased(fkey);
		}

		private static Boolean IsValid(in FKey key) => key.IsValid;

		public IEngineObject InstantiatePrefab(IEnginePrefabAsset prefab, ITransform transform)
		{
			LogWarn($"Instantiate not implemented: {prefab}, {transform}");
			return null;
		}

		public void ReloadCurrentScene() => throw new NotImplementedException();

		public void QuitApplication() => throw new NotImplementedException();
		public Double GetDeltaTimeInSeconds() => UGameplayStatics.WorldDeltaSeconds;
		public void LogInfo(String message) => AActor.PrintString(message, printToScreen: false);

		public void LogWarn(String message) => AActor.PrintString(message, printToScreen: false, color: new FLinearColor(0f, 1f, 1f));

		public void LogError(String message) => AActor.PrintString(message, printToScreen: false, color: new FLinearColor(1f, 0f, 0f));
		public void ShowMessage(String message, Double duration = 2f) => AActor.PrintString(message, (Single)duration);

		public void Log(String message) => ShowMessage(message);
		public void PlaySound(String soundName, Double volume) => throw new NotImplementedException();
	}
}
