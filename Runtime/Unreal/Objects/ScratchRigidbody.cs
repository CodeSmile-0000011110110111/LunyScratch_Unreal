using UnrealSharp;
using UnrealSharp.CoreUObject;
using UnrealSharp.Engine;
// For FName

namespace LunyScratch
{
	internal sealed class ScratchRigidbody : IRigidbody
	{
		private readonly AActor _owner;

		private UPrimitiveComponent Root => _owner.RootComponent as UPrimitiveComponent;

		public IVector3 LinearVelocity
		{
			get
			{
				var root = Root;
				if (root != null)
				{
					var v = root.GetPhysicsLinearVelocity();
					return new ScratchVector3(v);
				}
				return new ScratchVector3(0, 0, 0);
			}
		}

		public IVector3 AngularVelocity
		{
			get
			{
				var root = Root;
				if (root != null)
				{
					var v = root.GetPhysicsAngularVelocityInDegrees();
					return new ScratchVector3(v);
				}
				return new ScratchVector3(0, 0, 0);
			}
		}

		public IVector3 Position
		{
			get
			{
				var root = Root;
				if (root != null)
				{
					var p = root.GetSocketLocation(FName.None);
					return new ScratchVector3(p);
				}
				return new ScratchVector3(0, 0, 0);
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
				return new ScratchVector3(1, 0, 0);
			}
		}

		public ScratchRigidbody(AActor owner) => _owner = owner ?? throw new ArgumentNullException(nameof(owner));

		public void SetLinearVelocity(Single x, Single y, Single z)
		{
			var root = Root;
			if (root != null)
				root.SetPhysicsLinearVelocity(new FVector(x, z, y)); // Unreal axis quirks
		}

		public void SetAngularVelocity(Single x, Single y, Single z)
		{
			var root = Root;
			if (root != null)
				root.SetPhysicsAngularVelocityInDegrees(new FVector(x, z, y)); // Unreal axis quirks
		}

		public void SetPosition(Single x, Single y, Single z)
		{
			var root = Root;
			if (root != null)
			{
				root.SetWorldLocation(new FVector(x, z, y), false, out var _, false);
				return;
			}
			_owner.SetActorLocation(new FVector(x, z, y)); // Unreal axis quirks
		}
	}
}
