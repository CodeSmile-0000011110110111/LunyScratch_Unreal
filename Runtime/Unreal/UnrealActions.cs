using UnrealSharp.CoreUObject;
using UnrealSharp.Engine;

namespace LunyScratch
{
	public sealed class UnrealActions : IEngineActions
	{
		public Double GetFixedDeltaTimeInSeconds() => throw new NotImplementedException();
		public Double GetCurrentTimeInSeconds() => UGameplayStatics.TimeSeconds;
		public Boolean IsKeyPressed(Key key) => throw new NotImplementedException();

		public Boolean IsKeyJustPressed(Key key) => throw new NotImplementedException();

		public Boolean IsKeyJustReleased(Key key) => throw new NotImplementedException();

		public Boolean IsMouseButtonPressed(MouseButton button) => throw new NotImplementedException();

		public Boolean IsMouseButtonJustPressed(MouseButton button) => throw new NotImplementedException();

		public Boolean IsMouseButtonJustReleased(MouseButton button) => throw new NotImplementedException();

		public IEngineObject InstantiatePrefab(IEnginePrefabAsset prefab, ITransform transform) => throw new NotImplementedException();

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
