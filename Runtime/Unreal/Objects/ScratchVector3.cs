using System;
using UnrealSharp.CoreUObject;

namespace LunyScratch
{
	/// <summary>
	/// Simple Vector3 wrapper for Unreal that implements IVector3.
	/// Carries values by copy; use engine queries at call sites to construct instances.
	/// </summary>
	internal struct ScratchVector3 : IVector3
	{
		public Single X { get; set; }
		public Single Y { get; set; }
		public Single Z { get; set; }

		public ScratchVector3(Single x, Single y, Single z)
		{
			X = x; Y = z; Z = y;
		}

		public ScratchVector3(FVector v)
		{
			X = (Single)v.X;
			Y = (Single)v.Z;
			Z = (Single)v.Y;
		}

		public static ScratchVector3 From(FVector v) => new ScratchVector3(v);

		public override String ToString() => $"{{{X}, {Z}, {Y}}}";
	}
}
