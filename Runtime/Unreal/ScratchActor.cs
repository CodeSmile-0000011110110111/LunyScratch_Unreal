using UnrealSharp.Attributes;
using UnrealSharp.CoreUObject;
using UnrealSharp.Engine;

namespace LunyScratch
{
	// FIXME: UnrealSharp currently doesn't allow abstract AActor subclassing => https://github.com/UnrealSharp/UnrealSharp/issues/560
	[UClass]
	public /*abstract*/ class AScratchActor : AActor, IScratchRunner
	{
		private readonly Table _variables = new();
		private BlockRunner _runner;
		private ScratchActorContext _context;

		public Table Variables => _variables;
		public Table GlobalVariables => UScratchRuntime.Instance.Variables;

		protected override void BeginPlay()
		{
			base.BeginPlay();
			_context = new ScratchActorContext(this);
			_runner = new BlockRunner(_context);
			OnScratchReady();
		}

		public override void Tick(Single deltaTime)
		{
			base.Tick(deltaTime);
			_runner?.ProcessUpdate(deltaTime);
			_runner?.ProcessPhysicsUpdate(GameEngine.Actions.GetFixedDeltaTimeInSeconds());
			if (_context != null && _context.IsScheduledForDestruction)
			{
				DestroyActor();
				return;
			}
			OnUpdate(deltaTime);
		}

		public override void Dispose()
		{
			base.Dispose();
			_runner?.Dispose();
			_context?.Dispose();
			_runner = null;
			_context = null;
		}

		// IScratchRunner implementation
		public void Run(params IScratchBlock[] blocks) => _runner.AddBlock(Blocks.Sequence(blocks));
		public void RunPhysics(params IScratchBlock[] blocks) => _runner.AddPhysicsBlock(Blocks.Sequence(blocks));
		public void RepeatForever(params IScratchBlock[] blocks) => _runner.AddBlock(Blocks.RepeatForever(blocks));
		public void RepeatForeverPhysics(params IScratchBlock[] blocks) => _runner.AddPhysicsBlock(Blocks.RepeatForever(blocks));
		public void When(EventBlock evt, params IScratchBlock[] blocks) => _runner.AddBlock(Blocks.When(evt, blocks));

		protected virtual void OnScratchReady() {}
		protected virtual void OnUpdate(Single deltaTime) {}
	}
}
