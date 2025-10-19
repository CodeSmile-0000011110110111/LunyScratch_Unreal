using UnrealSharp.Attributes;
using UnrealSharp.UnrealSharpCore;

namespace LunyScratch
{
	[UClass]
	public sealed class UScratchRuntime : UCSGameInstanceSubsystem, IEngineRuntime
	{
		private static UScratchRuntime? s_Instance;

		private BlockRunner _runner;
		private IScratchContext _context;

		public static UScratchRuntime Instance => s_Instance ?? throw new Exception("Scratch Runtime: Instance not initialized");

		public Table Variables => throw new NotImplementedException();

		protected override void Initialize(FSubsystemCollectionBaseRef collection)
		{
			base.Initialize(collection);

			if (s_Instance != null)
				throw new Exception($"{nameof(UScratchRuntime)} singleton duplication");

			_context = null;
			_runner = new BlockRunner(_context);
			s_Instance = this;
			GameEngine.Initialize(s_Instance, new UnrealActions(), null);

			IsTickable = true;
		}

		public void Run(params IScratchBlock[] blocks) => throw new NotImplementedException();

		public void RunPhysics(params IScratchBlock[] blocks) => throw new NotImplementedException();

		public void RepeatForever(params IScratchBlock[] blocks) => throw new NotImplementedException();

		public void RepeatForeverPhysics(params IScratchBlock[] blocks) => throw new NotImplementedException();

		public void When(EventBlock evt, params IScratchBlock[] blocks) => throw new NotImplementedException();

		public override void Dispose()
		{
			base.Dispose();
			GameEngine.Shutdown();
			s_Instance = null;
		}

		protected override void Tick(Single deltaTime) => _runner.ProcessUpdate(deltaTime);

		public void RunBlock(IScratchBlock block) => _runner.AddBlock(block);
	}
}
