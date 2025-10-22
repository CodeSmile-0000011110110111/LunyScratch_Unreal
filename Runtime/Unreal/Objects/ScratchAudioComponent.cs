using System;
using UnrealSharp.CoreUObject;
using UnrealSharp.Engine;

namespace LunyScratch
{
	/// <summary>
	/// Unreal adapter that wraps an AudioComponent and exposes a simple Play() call
	/// compatible with IEngineAudioSource.
	/// </summary>
 internal sealed class ScratchAudioComponent : IEngineAudioSource
	{
		private readonly UAudioComponent _audio;
		private readonly AActor _owner;

		public ScratchAudioComponent(AActor owner, UAudioComponent audio)
		{
			_owner = owner ?? throw new ArgumentNullException(nameof(owner));
			_audio = audio ?? throw new ArgumentNullException(nameof(audio));
		}

		public void Play()
		{
			/*
			// Ensure sound plays at the owning actor's current runtime position using bounds origin
			_owner.GetActorBounds(false, out var origin, out _);
			// Move the audio component to the actor's current position (no sweep, no teleport)
			_audio.SetWorldLocation(origin, false, out FHitResult _, false);
			*/
			// Trigger one-shot playback
			_audio.Play();
		}
	}
}
