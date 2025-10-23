using UnrealSharp.Engine;
using UnrealSharp.UMG;

namespace LunyScratch
{
	public sealed class ScratchHUD : ScratchUI, IEngineHUD
	{
		private UUserWidget _hud;

		public ScratchHUD()
		{
			// Load the Widget Blueprint class from Content/UI/HUD
			var hudAsset = AssetRegistry.Get<IEnginePrefabAsset>("UI/HUD");
			if (hudAsset is ScratchPrefabAsset bp)
			{
				var widgetClass = bp.BlueprintClass; // should be a UUserWidget-derived class
				var widget = WidgetLibrary.CreateWidget(widgetClass, UGameplayStatics.GetPlayerController(0));
				widget.AddToViewport();
				_hud = widget;
				RootWidget = widget;
			}
		}
	}
}
