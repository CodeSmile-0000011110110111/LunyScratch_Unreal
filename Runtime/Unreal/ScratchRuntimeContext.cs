namespace LunyScratch
{
	internal sealed class ScratchRuntimeContext : IScratchContext
	{
		private UScratchRuntime _owner;

		public IRigidbody Rigidbody => throw new Exception("Runtime context does not have Rigidbody");
		public ITransform Transform => throw new Exception("Runtime context does not have Transform");
		public IEngineAudioSource Audio => null; // No audio on runtime
		public IEngineObject Self => new ScratchEngineObject(_owner);
		public IScratchRunner Runner => _owner;
		public IEngineCamera ActiveCamera => throw new NotSupportedException("ActiveCamera is not implemented for Unreal example");

		public ScratchRuntimeContext(UScratchRuntime owner) => _owner = owner;

		public void SetSelfComponentEnabled(Boolean enabled)
		{
			/* no-op for runtime */
		}

		public IEngineHUD GetEngineHUD() => throw new NotSupportedException("HUD is not implemented for Unreal example");
		public IEngineMenu GetEngineMenu() => throw new NotSupportedException("Menu is not implemented for Unreal example");

		public void ScheduleDestroy()
		{
			/* runtime cannot be destroyed via blocks */
		}

		public IEngineObject FindChild(String name) => null; // runtime has no children
		public Boolean QueryCollisionEnterEvents(String nameFilter, String tagFilter) => false;

		public void Dispose() => _owner = null;
	}
}
