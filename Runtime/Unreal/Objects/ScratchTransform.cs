using System;
using UnrealSharp; // For FName
using UnrealSharp.CoreUObject;
using UnrealSharp.Engine;

namespace LunyScratch
{
	internal sealed class ScratchTransform : ITransform
	{
		private readonly AActor _owner;

		private USceneComponent Root => _owner.RootComponent as USceneComponent;

		public IVector3 Position
		{
			get
			{
				var root = Root;
				if (root != null)
				{
					var pos = root.GetSocketLocation(FName.None);
					return new ScratchVector3(pos);
				}
				// Fallback: use actor bounds origin
				_owner.GetActorBounds(false, out var origin, out _);
				return new ScratchVector3(origin);
			}
		}

		public IVector3 Forward
		{
			get
			{
				var root = Root;
				if (root != null)
				{
					var rot = root.GetSocketRotation(FName.None);
					var fwd = MathLibrary.GetForwardVector(rot);
					return new ScratchVector3(fwd);
				}
				// Fallback if no root: world +X
				return new ScratchVector3(1, 0, 0);
			}
		}

		public ScratchTransform(AActor owner)
		{
			_owner = owner ?? throw new ArgumentNullException(nameof(owner));
		}

		public void SetPosition(Single x, Single y, Single z)
		{
			var root = Root;
			if (root != null)
			{
				root.SetWorldLocation(new FVector(x, y, z), false, out FHitResult _, false);
				return;
			}
			_owner.SetActorLocation(new FVector(x, y, z));
		}
	}
}
