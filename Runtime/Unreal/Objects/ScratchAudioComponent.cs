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

		public void Play() => _audio.Play();
	}
}
