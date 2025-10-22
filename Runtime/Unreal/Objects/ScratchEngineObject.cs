using UnrealSharp.CoreUObject;
using UnrealSharp.Engine;

namespace LunyScratch
{
	public class ScratchEngineObject : IEngineObject
	{
		private readonly UObject _engineObject;

		public static implicit operator ScratchEngineObject(UObject unrealObject) => new(unrealObject);

		public ScratchEngineObject(UObject unrealObject) => _engineObject = unrealObject;

		public void SetEnabled(Boolean enabled)
		{
			switch (_engineObject)
			{
				case AActor actor:
					actor.ActorTickEnabled = enabled;
					var scene = actor.GetComponentByClass<USceneComponent>();
					if (scene != null)
						scene.SetHiddenInGame(!enabled);
					break;

				case USceneComponent sceneComponent:
					sceneComponent.SetVisibility(enabled, true);
					break;

				default:
					throw new ArgumentOutOfRangeException(nameof(_engineObject), _engineObject, null);
			}
		}

		public Object GetEngineObject() => _engineObject;
	}
}
