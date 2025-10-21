using System;
using System.Collections.Generic;
using UnrealSharp.Engine;

namespace LunyScratch
{
	internal sealed class ScratchActorContext : IScratchContext, IDisposable
	{
		private readonly AActor _owner;
		private readonly Dictionary<string, IEngineObject> _childrenByName = new();
		private bool _scheduledForDestruction;

		public ScratchActorContext(AActor owner)
		{
			if (owner is not IScratchRunner)
				throw new ArgumentNullException(nameof(owner), $"does not implement {nameof(IScratchRunner)}");
			
			_owner = owner;
		}

		public IRigidbody Rigidbody => null; // Not implemented for Unreal example
		public ITransform Transform => null; // Not implemented for Unreal example
		public IEngineAudioSource Audio => null; // Not implemented for Unreal example

		public IEngineObject Self => new UnrealEngineObject(_owner);
		public IScratchRunner Runner => _owner as IScratchRunner;
		public IEngineCamera ActiveCamera => throw new NotSupportedException("ActiveCamera is not implemented for Unreal example");

		public void SetSelfComponentEnabled(bool enabled) => _owner.ActorTickEnabled = enabled;

		public IEngineHUD GetEngineHUD() => throw new NotSupportedException("HUD is not implemented for Unreal example");
		public IEngineMenu GetEngineMenu() => throw new NotSupportedException("Menu is not implemented for Unreal example");

		public void ScheduleDestroy() => _scheduledForDestruction = true;
		internal bool IsScheduledForDestruction => _scheduledForDestruction;

		public IEngineObject FindChild(string name)
		{
			if (string.IsNullOrEmpty(name))
				return null;

			if (_childrenByName.TryGetValue(name, out var cached))
				return cached;

			// Use the actor's scene component hierarchy as the search space, similar to Unity's Transform hierarchy.
			var root = _owner.GetComponentByClass<USceneComponent>();
			USceneComponent found = null;

			string GetObjectName(UnrealSharp.CoreUObject.UObject obj)
			{
				// UnrealSharp may not expose GetName; ToString typically returns the object name
				return obj?.ToString();
			}

			if (root != null)
			{
				// First, check direct children (non-recursive) to mirror Unity's fast path
				System.Collections.Generic.IList<USceneComponent> directChildren;
				root.GetChildrenComponents(false, out directChildren);
				foreach (var child in directChildren)
				{
					if (child != null && string.Equals(GetObjectName(child), name, System.StringComparison.InvariantCulture))
					{
						found = child;
						break;
					}
				}

				// If not found, recurse through all descendants
				if (found == null)
				{
					System.Collections.Generic.IList<USceneComponent> allDescendants;
					root.GetChildrenComponents(true, out allDescendants);
					foreach (var child in allDescendants)
					{
						if (child != null && string.Equals(GetObjectName(child), name, System.StringComparison.InvariantCulture))
						{
							found = child;
							break;
						}
					}
				}
			}

			if (found != null)
			{
				var engineObject = new UnrealEngineObject(found);
				_childrenByName[name] = engineObject;
				return engineObject;
			}

			// Fallback: scan all scene components on the actor (non-hierarchical)
			var componentsOfActor = _owner.GetComponentsByClass<USceneComponent>();
			if (componentsOfActor != null)
			{
				foreach (var comp in componentsOfActor)
				{
					if (comp != null && string.Equals(GetObjectName(comp), name, System.StringComparison.InvariantCulture))
					{
						var engineObject = new UnrealEngineObject(comp);
						_childrenByName[name] = engineObject;
						return engineObject;
					}
				}
			}

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
