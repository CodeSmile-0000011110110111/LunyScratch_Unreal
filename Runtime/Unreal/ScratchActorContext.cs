using System;
using System.Collections.Generic;
using UnrealSharp;
using UnrealSharp.Attributes;
using UnrealSharp.CoreUObject;
using UnrealSharp.Engine;

namespace LunyScratch
{
	internal sealed class ScratchActorContext : IScratchContext, IDisposable
	{
		private readonly Dictionary<String, IEngineObject> _childrenByName = new();
		private readonly HashSet<AActor> _collisionEnterQueue = new();
		private AActor _owner;
		private Boolean _scheduledForDestruction;

		private IRigidbody? _rigidbody;
		private ITransform? _transform;
		private IEngineAudioSource? _audio;
		private Boolean _audioCached;

		public IRigidbody Rigidbody => _rigidbody ??= new ScratchRigidbody(_owner);
		public ITransform Transform => _transform ??= new ScratchTransform(_owner);
		public IEngineAudioSource Audio
		{
			get
			{
				if (!_audioCached)
				{
					UAudioComponent audioComp = null;
					// Search the owner's scene component hierarchy for an AudioComponent
					var root = _owner.GetComponentByClass<USceneComponent>();
					if (root != null)
					{
						IList<USceneComponent> allDescendants;
						root.GetChildrenComponents(true, out allDescendants);
						if (allDescendants != null)
						{
							foreach (var child in allDescendants)
							{
								if (child is UAudioComponent ac)
								{
									audioComp = ac;
									break;
								}
							}
						}
					}
					// If none in children, try any audio component on the actor (fallback)
					if (audioComp == null)
					{
						var actorAudio = _owner.GetComponentsByClass<UAudioComponent>();
						if (actorAudio != null)
						{
							foreach (var ac in actorAudio)
							{
								if (ac != null) { audioComp = ac; break; }
							}
						}
					}
					_audio = audioComp != null ? new ScratchAudioComponent(_owner, audioComp) : null;
					_audioCached = true;
				}
				return _audio;
			}
		}

		public IEngineObject Self => new ScratchEngineObject(_owner);
		public IScratchRunner Runner => _owner as IScratchRunner;
		public IEngineCamera ActiveCamera => throw new NotSupportedException("ActiveCamera is not implemented for Unreal example");
		internal Boolean IsScheduledForDestruction => _scheduledForDestruction;

		public ScratchActorContext(AActor owner)
		{
			if (owner is not IScratchRunner)
				throw new ArgumentNullException(nameof(owner), $"does not implement {nameof(IScratchRunner)}");

			_owner = owner;
		}

		public void Dispose()
		{
			// Clear caches
			_childrenByName.Clear();
			_collisionEnterQueue.Clear();
			_owner = null;
		}

		public void SetSelfComponentEnabled(Boolean enabled) => _owner.ActorTickEnabled = enabled;

		public IEngineHUD GetEngineHUD() => throw new NotSupportedException("HUD is not implemented for Unreal example");
		public IEngineMenu GetEngineMenu() => throw new NotSupportedException("Menu is not implemented for Unreal example");

		public void ScheduleDestroy() => _scheduledForDestruction = true;

		public IEngineObject FindChild(String name)
		{
			if (String.IsNullOrEmpty(name))
				return null;

			if (_childrenByName.TryGetValue(name, out var cached))
				return cached;

			// Use the actor's scene component hierarchy as the search space, similar to Unity's Transform hierarchy.
			var root = _owner.GetComponentByClass<USceneComponent>();
			USceneComponent found = null;

			String GetObjectName(UObject obj) =>
				// UnrealSharp may not expose GetName; ToString typically returns the object name
				obj?.ToString();

			if (root != null)
			{
				// First, check direct children (non-recursive) to mirror Unity's fast path
				IList<USceneComponent> directChildren;
				root.GetChildrenComponents(false, out directChildren);
				foreach (var child in directChildren)
				{
					if (child != null && String.Equals(GetObjectName(child), name, StringComparison.InvariantCulture))
					{
						found = child;
						break;
					}
				}
				
				// If not found, recurse through all descendants
				if (found == null)
				{
					IList<USceneComponent> allDescendants;
					root.GetChildrenComponents(true, out allDescendants);
					foreach (var child in allDescendants)
					{
						if (child != null && String.Equals(GetObjectName(child), name, StringComparison.InvariantCulture))
						{
							found = child;
							break;
						}
					}
				}
			}

			if (found != null)
			{
				var engineObject = new ScratchEngineObject(found);
				_childrenByName[name] = engineObject;
				return engineObject;
			}

			// Fallback: scan all scene components on the actor (non-hierarchical)
			var componentsOfActor = _owner.GetComponentsByClass<USceneComponent>();
			if (componentsOfActor != null)
			{
				foreach (var comp in componentsOfActor)
				{
					if (comp != null && String.Equals(GetObjectName(comp), name, StringComparison.InvariantCulture))
					{
						var engineObject = new ScratchEngineObject(comp);
						_childrenByName[name] = engineObject;
						return engineObject;
					}
				}
			}

			_childrenByName[name] = null; // cache miss
			return null;
		}
		
		public Boolean QueryCollisionEnterEvents(String nameFilter, String tagFilter)
		{
			foreach (var other in _collisionEnterQueue)
			{
				var otherName = other?.ToString();
				var nameOk = nameFilter == null || String.Equals(otherName, nameFilter, StringComparison.InvariantCulture);
				var tagOk = true;
				if (tagFilter != null)
				{
					// Unreal uses FName for tags
					tagOk = other != null && other.ActorHasTag(new FName(tagFilter));
				}
				if (nameOk && tagOk)
					return true;
			}
			return false;
		}

		internal void EnqueueCollisionEnter(AActor other)
		{
			if (other != null)
				_collisionEnterQueue.Add(other);
		}

		public void ClearCollisionEventQueues() => _collisionEnterQueue.Clear();

	}
}
