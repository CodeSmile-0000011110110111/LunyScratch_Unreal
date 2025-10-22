using System;
using UnrealSharp.InputCore;

namespace LunyScratch
{
	// Unreal-specific mapping helpers
	internal static class Remap
	{
		// Letters
		internal static readonly FKey A = new() { KeyName = "A" };
		internal static readonly FKey B = new() { KeyName = "B" };
		internal static readonly FKey C = new() { KeyName = "C" };
		internal static readonly FKey D = new() { KeyName = "D" };
		internal static readonly FKey E = new() { KeyName = "E" };
		internal static readonly FKey F = new() { KeyName = "F" };
		internal static readonly FKey G = new() { KeyName = "G" };
		internal static readonly FKey H = new() { KeyName = "H" };
		internal static readonly FKey I = new() { KeyName = "I" };
		internal static readonly FKey J = new() { KeyName = "J" };
		internal static readonly FKey K = new() { KeyName = "K" };
		internal static readonly FKey L = new() { KeyName = "L" };
		internal static readonly FKey M = new() { KeyName = "M" };
		internal static readonly FKey N = new() { KeyName = "N" };
		internal static readonly FKey O = new() { KeyName = "O" };
		internal static readonly FKey P = new() { KeyName = "P" };
		internal static readonly FKey Q = new() { KeyName = "Q" };
		internal static readonly FKey R = new() { KeyName = "R" };
		internal static readonly FKey S = new() { KeyName = "S" };
		internal static readonly FKey T = new() { KeyName = "T" };
		internal static readonly FKey U = new() { KeyName = "U" };
		internal static readonly FKey V = new() { KeyName = "V" };
		internal static readonly FKey W = new() { KeyName = "W" };
		internal static readonly FKey X = new() { KeyName = "X" };
		internal static readonly FKey Y = new() { KeyName = "Y" };
		internal static readonly FKey Z = new() { KeyName = "Z" };

		// Numbers (top row)
		internal static readonly FKey Digit0 = new() { KeyName = "Zero" };
		internal static readonly FKey Digit1 = new() { KeyName = "One" };
		internal static readonly FKey Digit2 = new() { KeyName = "Two" };
		internal static readonly FKey Digit3 = new() { KeyName = "Three" };
		internal static readonly FKey Digit4 = new() { KeyName = "Four" };
		internal static readonly FKey Digit5 = new() { KeyName = "Five" };
		internal static readonly FKey Digit6 = new() { KeyName = "Six" };
		internal static readonly FKey Digit7 = new() { KeyName = "Seven" };
		internal static readonly FKey Digit8 = new() { KeyName = "Eight" };
		internal static readonly FKey Digit9 = new() { KeyName = "Nine" };

		// Function keys
		internal static readonly FKey F1 = new() { KeyName = "F1" };
		internal static readonly FKey F2 = new() { KeyName = "F2" };
		internal static readonly FKey F3 = new() { KeyName = "F3" };
		internal static readonly FKey F4 = new() { KeyName = "F4" };
		internal static readonly FKey F5 = new() { KeyName = "F5" };
		internal static readonly FKey F6 = new() { KeyName = "F6" };
		internal static readonly FKey F7 = new() { KeyName = "F7" };
		internal static readonly FKey F8 = new() { KeyName = "F8" };
		internal static readonly FKey F9 = new() { KeyName = "F9" };
		internal static readonly FKey F10 = new() { KeyName = "F10" };
		internal static readonly FKey F11 = new() { KeyName = "F11" };
		internal static readonly FKey F12 = new() { KeyName = "F12" };

		// Arrow keys
		internal static readonly FKey LeftArrow = new() { KeyName = "Left" };
		internal static readonly FKey RightArrow = new() { KeyName = "Right" };
		internal static readonly FKey UpArrow = new() { KeyName = "Up" };
		internal static readonly FKey DownArrow = new() { KeyName = "Down" };

		// Special keys
		internal static readonly FKey Space = new() { KeyName = "SpaceBar" };
		internal static readonly FKey Enter = new() { KeyName = "Enter" };
		internal static readonly FKey Escape = new() { KeyName = "Escape" };
		internal static readonly FKey Tab = new() { KeyName = "Tab" };
		internal static readonly FKey Backspace = new() { KeyName = "BackSpace" };
		internal static readonly FKey Delete = new() { KeyName = "Delete" };
		internal static readonly FKey LeftShift = new() { KeyName = "LeftShift" };
		internal static readonly FKey RightShift = new() { KeyName = "RightShift" };
		internal static readonly FKey LeftCtrl = new() { KeyName = "LeftControl" };
		internal static readonly FKey RightCtrl = new() { KeyName = "RightControl" };
		internal static readonly FKey LeftAlt = new() { KeyName = "LeftAlt" };
		internal static readonly FKey RightAlt = new() { KeyName = "RightAlt" };

		// Numpad
		internal static readonly FKey Numpad0 = new() { KeyName = "NumPadZero" };
		internal static readonly FKey Numpad1 = new() { KeyName = "NumPadOne" };
		internal static readonly FKey Numpad2 = new() { KeyName = "NumPadTwo" };
		internal static readonly FKey Numpad3 = new() { KeyName = "NumPadThree" };
		internal static readonly FKey Numpad4 = new() { KeyName = "NumPadFour" };
		internal static readonly FKey Numpad5 = new() { KeyName = "NumPadFive" };
		internal static readonly FKey Numpad6 = new() { KeyName = "NumPadSix" };
		internal static readonly FKey Numpad7 = new() { KeyName = "NumPadSeven" };
		internal static readonly FKey Numpad8 = new() { KeyName = "NumPadEight" };
		internal static readonly FKey Numpad9 = new() { KeyName = "NumPadNine" };
		internal static readonly FKey NumpadEnter = new() { KeyName = "NumPadEnter" };
		internal static readonly FKey NumpadPlus = new() { KeyName = "Add" };
		internal static readonly FKey NumpadMinus = new() { KeyName = "Subtract" };
		internal static readonly FKey NumpadMultiply = new() { KeyName = "Multiply" };
		internal static readonly FKey NumpadDivide = new() { KeyName = "Divide" };

		// Mouse buttons
		internal static readonly FKey MouseLeft = new() { KeyName = "LeftMouseButton" };
		internal static readonly FKey MouseRight = new() { KeyName = "RightMouseButton" };
		internal static readonly FKey MouseMiddle = new() { KeyName = "MiddleMouseButton" };
		internal static readonly FKey MouseBack = new() { KeyName = "ThumbMouseButton" }; // XButton1
		internal static readonly FKey MouseForward = new() { KeyName = "ThumbMouseButton2" }; // XButton2

		internal static FKey ToUnrealKey(Key key) => key switch
		{
			// Letters
			Key.A => A,
			Key.B => B,
			Key.C => C,
			Key.D => D,
			Key.E => E,
			Key.F => F,
			Key.G => G,
			Key.H => H,
			Key.I => I,
			Key.J => J,
			Key.K => K,
			Key.L => L,
			Key.M => M,
			Key.N => N,
			Key.O => O,
			Key.P => P,
			Key.Q => Q,
			Key.R => R,
			Key.S => S,
			Key.T => T,
			Key.U => U,
			Key.V => V,
			Key.W => W,
			Key.X => X,
			Key.Y => Y,
			Key.Z => Z,

			// Numbers
			Key.Digit0 => Digit0,
			Key.Digit1 => Digit1,
			Key.Digit2 => Digit2,
			Key.Digit3 => Digit3,
			Key.Digit4 => Digit4,
			Key.Digit5 => Digit5,
			Key.Digit6 => Digit6,
			Key.Digit7 => Digit7,
			Key.Digit8 => Digit8,
			Key.Digit9 => Digit9,

			// Function keys
			Key.F1 => F1,
			Key.F2 => F2,
			Key.F3 => F3,
			Key.F4 => F4,
			Key.F5 => F5,
			Key.F6 => F6,
			Key.F7 => F7,
			Key.F8 => F8,
			Key.F9 => F9,
			Key.F10 => F10,
			Key.F11 => F11,
			Key.F12 => F12,

			// Arrow keys
			Key.LeftArrow => LeftArrow,
			Key.RightArrow => RightArrow,
			Key.UpArrow => UpArrow,
			Key.DownArrow => DownArrow,

			// Special keys
			Key.Space => Space,
			Key.Enter => Enter,
			Key.Escape => Escape,
			Key.Tab => Tab,
			Key.Backspace => Backspace,
			Key.Delete => Delete,
			Key.LeftShift => LeftShift,
			Key.RightShift => RightShift,
			Key.LeftCtrl => LeftCtrl,
			Key.RightCtrl => RightCtrl,
			Key.LeftAlt => LeftAlt,
			Key.RightAlt => RightAlt,

			// Numpad
			Key.Numpad0 => Numpad0,
			Key.Numpad1 => Numpad1,
			Key.Numpad2 => Numpad2,
			Key.Numpad3 => Numpad3,
			Key.Numpad4 => Numpad4,
			Key.Numpad5 => Numpad5,
			Key.Numpad6 => Numpad6,
			Key.Numpad7 => Numpad7,
			Key.Numpad8 => Numpad8,
			Key.Numpad9 => Numpad9,
			Key.NumpadEnter => NumpadEnter,
			Key.NumpadPlus => NumpadPlus,
			Key.NumpadMinus => NumpadMinus,
			Key.NumpadMultiply => NumpadMultiply,
			Key.NumpadDivide => NumpadDivide,

			_ => default,
		};

		internal static FKey ToUnrealMouseButton(MouseButton button) => button switch
		{
			MouseButton.Left => MouseLeft,
			MouseButton.Right => MouseRight,
			MouseButton.Middle => MouseMiddle,
			MouseButton.Forward => MouseForward,
			MouseButton.Back => MouseBack,
			_ => default,
		};
	}
}
