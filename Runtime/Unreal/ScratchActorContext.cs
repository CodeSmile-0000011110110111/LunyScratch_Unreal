using System;
using System.Collections.Generic;
using UnrealSharp.Engine;

namespace LunyScratch
{
	internal sealed class ScratchActorContext : IScratchContext, IDisposable
	{
		private readonly AScratchActor _owner;
		private readonly Dictionary<string, IEngineObject> _childrenByName = new();
		private bool _scheduledForDestruction;

		public ScratchActorContext(AScratchActor owner) => _owner = owner;

		public IRigidbody Rigidbody => null; // Not implemented for Unreal example
		public ITransform Transform => null; // Not implemented for Unreal example
		public IEngineAudioSource Audio => null; // Not implemented for Unreal example

		public IEngineObject Self => new UnrealEngineObject(_owner);
		public IScratchRunner Runner => _owner;
		public IEngineCamera ActiveCamera => throw new NotSupportedException("ActiveCamera is not implemented for Unreal example");

		public void SetSelfComponentEnabled(bool enabled) => _owner.ActorTickEnabled = enabled;

		public IEngineHUD GetEngineHUD() => throw new NotSupportedException("HUD is not implemented for Unreal example");
		public IEngineMenu GetEngineMenu() => throw new NotSupportedException("Menu is not implemented for Unreal example");

		public void ScheduleDestroy() => _scheduledForDestruction = true;
		internal bool IsScheduledForDestruction => _scheduledForDestruction;

		public IEngineObject FindChild(string name)
		{
			// Minimal implementation (engine-specific search omitted). Always return null and cache.
			if (string.IsNullOrEmpty(name))
				return null;

			if (_childrenByName.TryGetValue(name, out var cached))
				return cached;

			_childrenByName[name] = null; // cache miss
			return null;
		}

		public bool QueryCollisionEnterEvents(string nameFilter, string tagFilter)
		{
			// Collision event queue not wired for Unreal example; return false
			return false;
		}

		public void Dispose()
		{
			_childrenByName.Clear();
		}
	}
}
