using UnrealSharp.Attributes;
using UnrealSharp.Engine;
using UnrealSharp.UnrealSharpCore;

namespace LunyScratch
{
	[UClass]
	public sealed class UScratchRuntime : UCSGameInstanceSubsystem, IEngineRuntime
	{
		private static UScratchRuntime? s_Instance;

		private readonly Table _variables = new();
		private BlockRunner _runner;
		private IScratchContext _context;

		public static UScratchRuntime Instance => s_Instance ?? throw new Exception("Scratch Runtime: Instance not initialized");

		public Table Variables => _variables;

		protected override void Initialize(FSubsystemCollectionBaseRef collection)
		{
			base.Initialize(collection);

			if (s_Instance != null)
				throw new Exception($"{nameof(UScratchRuntime)} singleton duplication");

			_context = new ScratchRuntimeContext(this);
			_runner = new BlockRunner(_context);
			s_Instance = this;
			GameEngine.Initialize(s_Instance, new UnrealActions(), new ScratchAssetRegistry());

			IsTickable = true;
		}

		public void Run(params IScratchBlock[] blocks) => _runner.AddBlock(Blocks.Sequence(blocks));

		public void RunPhysics(params IScratchBlock[] blocks) => _runner.AddPhysicsBlock(Blocks.Sequence(blocks));

		public void RepeatForever(params IScratchBlock[] blocks) => _runner.AddBlock(Blocks.RepeatForever(blocks));

		public void RepeatForeverPhysics(params IScratchBlock[] blocks) => _runner.AddPhysicsBlock(Blocks.RepeatForever(blocks));

		public void When(EventBlock evt, params IScratchBlock[] blocks) => _runner.AddBlock(Blocks.When(evt, blocks));

		public override void Dispose()
		{
			base.Dispose();
			_runner?.Dispose();
			_context?.Dispose();
			_runner = null;
			_context = null;

			GameEngine.Shutdown();

			s_Instance = null;
		}

		protected override void Tick(Single deltaTime)
		{
			_runner.ProcessUpdate(deltaTime);
			_runner.ProcessPhysicsUpdate(GameEngine.Actions.GetFixedDeltaTimeInSeconds());
		}

		public void RunBlock(IScratchBlock block) => _runner.AddBlock(block);
	}
}
