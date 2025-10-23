using System;
using UnrealSharp.Attributes;
using UnrealSharp.Engine;
using UnrealSharp.UMG;

namespace LunyScratch
{
	public sealed class ScratchMenu : ScratchUI, IEngineMenu
	{
		public event Action<string> OnButtonClicked;
		public void RegisterEventHandler(String widgetName)
		{
			var w = FindWidgetByName(widgetName);
			if (w is UButton button)
			{
				try
				{
					button.OnClicked += () => RaiseButtonClicked(widgetName);
					AActor.PrintString("Button '" + widgetName + "' event handler registered.");
				}
				catch (Exception e)
				{
					GameEngine.Actions.LogWarn($"Failed to subscribe to OnClicked for button '{widgetName}': {e.Message}");
				}
			}
			else if (w == null)
			{
				GameEngine.Actions.LogWarn($"Menu button '{widgetName}' not found in widget tree.");
			}
			else
			{
				GameEngine.Actions.LogWarn($"Widget named '{widgetName}' is not a Button (type: {w.GetType().Name}).");
			}
		}

		[UFunction()]
		private void ButtonClicked(string widgetName)
		{

		}

		public void UnregisterEventHandler(String widgetName)
		{
			var w = FindWidgetByName(widgetName);
			if (w is UButton button)
			{
				try
				{
					// TODO: remove event handler
					//btn.OnClicked-=() => RaiseButtonClicked(widgetName);
				}
				catch (Exception e)
				{
					GameEngine.Actions.LogWarn($"Failed to unsubscribe to OnClicked for button '{widgetName}': {e.Message}");
				}
			}
		}

		private UUserWidget _menu;

		public ScratchMenu()
		{
			// Load the Widget Blueprint class from Content/UI/Menu
			var menuAsset = AssetRegistry.Get<IEnginePrefabAsset>("UI/Menu");
			if (menuAsset is ScratchPrefabAsset bp)
			{
				var widgetClass = bp.BlueprintClass; // should be a UUserWidget-derived class
				var widget = WidgetLibrary.CreateWidget(widgetClass, UGameplayStatics.GetPlayerController(0));
				widget.AddToViewport();
				_menu = widget;
				RootWidget = widget;
			}
		}


		// Helper to raise button clicked from code
		internal void RaiseButtonClicked(string buttonName)
		{
			AActor.PrintString("Button '" + buttonName + "' clicked.");
			OnButtonClicked?.Invoke(buttonName);
		}
	}
}
