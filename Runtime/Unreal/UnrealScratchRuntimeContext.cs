using System;
using UnrealSharp.UnrealSharpCore;

namespace LunyScratch
{
	internal sealed class UnrealScratchRuntimeContext : IScratchContext
	{
		private readonly UScratchRuntime _owner;

		public UnrealScratchRuntimeContext(UScratchRuntime owner) => _owner = owner;

		public IRigidbody Rigidbody => throw new Exception("Runtime context does not have Rigidbody");
		public ITransform Transform => throw new Exception("Runtime context does not have Transform");
		public IEngineAudioSource Audio => null; // No audio on runtime
		public IEngineObject Self => new UnrealEngineObject(_owner);
		public IScratchRunner Runner => _owner;
		public IEngineCamera ActiveCamera => throw new NotSupportedException("ActiveCamera is not implemented for Unreal example");

		public void SetSelfComponentEnabled(bool enabled) { /* no-op for runtime */ }
		public IEngineHUD GetEngineHUD() => throw new NotSupportedException("HUD is not implemented for Unreal example");
		public IEngineMenu GetEngineMenu() => throw new NotSupportedException("Menu is not implemented for Unreal example");
		public void ScheduleDestroy() { /* runtime cannot be destroyed via blocks */ }
		public IEngineObject FindChild(string name) => null; // runtime has no children
		public bool QueryCollisionEnterEvents(string nameFilter, string tagFilter) => false;
	}
}
