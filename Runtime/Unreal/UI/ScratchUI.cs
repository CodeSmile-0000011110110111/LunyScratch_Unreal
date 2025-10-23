using UnrealSharp.Attributes;
using UnrealSharp.UMG;

namespace LunyScratch
{
	public abstract class ScratchUI : IEngineUI, IDisposable
	{
		private readonly Dictionary<String, UIVariableBinding> _boundVariables = new();
		private Boolean _disposed;

		protected UUserWidget RootWidget { get; set; }

		protected static UWidget FindWidgetByNameFromNamedSlot(UNamedSlot rootSlot, String name)
		{
			if (rootSlot == null)
				return null;

			var content = rootSlot?.Content;
			return FindWidgetByNameRecursive(content, name);
		}

		protected static UWidget FindWidgetByNameRecursive(UWidget widget, String name)
		{
			if (widget == null)
				return null;

			// Compare the widget's name. ToString() typically yields the UObject name.
			var widgetName = widget.ToString();
			if (!String.IsNullOrEmpty(widgetName) && String.Equals(widgetName, name, StringComparison.InvariantCulture))
				return widget;

			// If this is a content widget, recurse into its single content child first
			if (widget is UContentWidget contentWidget)
			{
				var content = contentWidget.Content;
				var found = FindWidgetByNameRecursive(content, name);
				if (found != null)
					return found;
			}

			// If this is a panel, iterate all children
			if (widget is UPanelWidget panel)
			{
				// You can choose either ChildrenCount/GetChildAt or AllChildren; both are available.
				var count = panel.ChildrenCount;
				for (var i = 0; i < count; i++)
				{
					var child = panel.GetChildAt(i);
					var found = FindWidgetByNameRecursive(child, name);
					if (found != null)
						return found;
				}
			}

			return null;
		}

		public void Dispose()
		{
			if (_disposed)
				return;

			foreach (var kv in _boundVariables)
				kv.Value?.Dispose();
			_boundVariables.Clear();
			_disposed = true;
		}

		public void Show()
		{
			var widget = RootWidget;
			if (widget == null)
			{
				GameEngine.Actions.LogWarn("Show(): No root widget available.");
				return;
			}
			widget.Visibility = ESlateVisibility.Visible;
		}

		public void Hide()
		{
			var widget = RootWidget;
			if (widget == null)
			{
				GameEngine.Actions.LogWarn("Hide(): No root widget available.");
				return;
			}
			widget.Visibility = ESlateVisibility.Collapsed;
		}

		public void BindVariable(Variable variable)
		{
			if (variable == null)
			{
				GameEngine.Actions.LogError("BindVariable: variable is null");
				return;
			}

			var varName = variable.Name ?? String.Empty;
			if (_boundVariables.ContainsKey(varName))
			{
				GameEngine.Actions.LogWarn($"UI widget '{varName}' already has variable binding.");
				return;
			}

			var root = RootWidget;
			if (root == null)
			{
				GameEngine.Actions.LogWarn("BindVariable: No root widget available.");
				return;
			}

			// Expect the root to be a UNamedSlot so we can traverse its content
			var rootSlot = root.WidgetTree?.RootWidget as UNamedSlot;
			if (rootSlot == null)
			{
				GameEngine.Actions.LogWarn("BindVariable: No root slot available.");
				return;
			}

			var target = FindWidgetByNameFromNamedSlot(rootSlot, varName);
			if (target == null)
			{
				GameEngine.Actions.LogWarn($"BindVariable: UI widget named '{varName}' not found under root slot.");
				return;
			}

			var binding = new UIVariableBinding(target, variable);
			_boundVariables[varName] = binding;
		}

		protected UWidget FindWidgetByName(String name)
		{
			var root = RootWidget;
			if (root == null)
				return null;

			var rootSlot = root.WidgetTree?.RootWidget as UNamedSlot;
			return FindWidgetByNameFromNamedSlot(rootSlot, name);
		}
	}
}
