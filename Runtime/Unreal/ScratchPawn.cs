using UnrealSharp.Attributes;
using UnrealSharp.CoreUObject;
using UnrealSharp.Engine;

namespace LunyScratch
{
	[UClass]
	public /*abstract*/ class AScratchPawn : APawn, IScratchRunner
	{
		private readonly Table _variables = new();
		private BlockRunner _runner;
		private ScratchActorContext _context;

		public Table Variables => _variables;
		public Table GlobalVariables => UScratchRuntime.Instance.Variables;

		// IScratchRunner implementation
		public void Run(params IScratchBlock[] blocks) => _runner.AddBlock(Blocks.Sequence(blocks));
		public void RunPhysics(params IScratchBlock[] blocks) => _runner.AddPhysicsBlock(Blocks.Sequence(blocks));
		public void RepeatForever(params IScratchBlock[] blocks) => _runner.AddBlock(Blocks.RepeatForever(blocks));
		public void RepeatForeverPhysics(params IScratchBlock[] blocks) => _runner.AddPhysicsBlock(Blocks.RepeatForever(blocks));
		public void When(EventBlock evt, params IScratchBlock[] blocks) => _runner.AddBlock(Blocks.When(evt, blocks));

		protected override void BeginPlay()
		{
			base.BeginPlay();
			_context = new ScratchActorContext(this);
			_runner = new BlockRunner(_context);
			// Subscribe to collision-related events on this pawn
			OnActorBeginOverlap += HandleActorBeginOverlap;
			OnActorHit += HandleActorHit;
			OnScratchReady();
		}

		protected override void EndPlay(EEndPlayReason endPlayReason)
		{
			base.EndPlay(endPlayReason);

			OnActorBeginOverlap -= HandleActorBeginOverlap;
			OnActorHit -= HandleActorHit;
		}

		public override void Tick(Single deltaTime)
		{
			base.Tick(deltaTime);
			_runner?.ProcessUpdate(deltaTime);
			_runner?.ProcessPhysicsUpdate(GameEngine.Actions.GetFixedDeltaTimeInSeconds());
			OnUpdate(deltaTime);

			_context?.ClearCollisionEventQueues();

			if (_context != null && _context.IsScheduledForDestruction)
				DestroyActor();
		}

		public override void Dispose()
		{
			base.Dispose();
			_runner?.Dispose();
			_context?.Dispose();
			_runner = null;
			_context = null;
		}

		[UFunction]
		private void HandleActorBeginOverlap(AActor overlappedActor, AActor otherActor)
		{
			//PrintString($"ScratchPawn.HandleActorBeginOverlap({overlappedActor}, {otherActor})");
			_context?.EnqueueCollisionEnter(otherActor);
		}

		[UFunction]
		private void HandleActorHit(AActor selfActor, AActor otherActor, FVector normalImpulse, FHitResult hit)
		{
			//PrintString($"ScratchPawn.HandleActorBeginOverlap({selfActor}, {otherActor})");
			_context?.EnqueueCollisionEnter(otherActor);
		}

		protected virtual void OnScratchReady() {}
		protected virtual void OnUpdate(Single deltaTime) {}
	}
}
