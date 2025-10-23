using System;
using UnrealSharp.UMG;

namespace LunyScratch
{
	internal sealed class UIVariableBinding : IDisposable
	{
		private readonly UWidget _widget;
		private readonly Variable _variable;

		public UIVariableBinding(UWidget widget, Variable variable)
		{
			_widget = widget;
			_variable = variable;

			OnValueChanged(_variable); // set initial value
			_variable.OnValueChanged += OnValueChanged;
		}

		public void Dispose() => _variable.OnValueChanged -= OnValueChanged;

		private void OnValueChanged(Variable variable)
		{
			if (_widget is UTextBlock text)
			{
				string content = variable.IsNumber ? variable.Number.ToString("N0") : variable.String;
				// UnrealSharp bindings typically allow assigning string to FText implicitly
				text.Text = content;
			}
			else
			{
				GameEngine.Actions.LogWarn($"Unsupported UI widget type for binding: {_widget?.GetType().Name}");
			}
		}
	}
}
