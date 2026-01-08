using Sandbox;
using Sandbox.Movement;

public sealed class Enemy : Component
{
	[RequireComponent] Rigidbody Rb { get; set; }
	[Property] SkinnedModelRenderer Renderer { get; set; }
	[Property] float AttackCooldown { get; set; } = 3f;
	[Property] float AttackRange { get; set; } = 50;
	[Property] float AttackDuration { get; set; } = 1;
	public ChikaraPlayer closestPlayer => Game.ActiveScene.GetAllComponents<ChikaraPlayer>().OrderBy( x => (x.WorldPosition - WorldPosition).Length ).FirstOrDefault();
	public Vector3 distanceToClosest => closestPlayer.IsValid() ? closestPlayer.WorldPosition - WorldPosition : Vector3.Zero;
	RealTimeSince timeSinceLastAttack = 0;
	RealTimeSince timeSinceAttackStarted = 0;
	protected override void OnFixedUpdate()
	{
		if ( distanceToClosest.Length < AttackRange && closestPlayer.IsValid() && timeSinceLastAttack > AttackCooldown )
		{
			Attack();
		} 
		else if ( timeSinceAttackStarted > AttackDuration && distanceToClosest.Length > AttackRange )
		{
			UpdateMove();
		}

		UpdateAnimations();
	}

	public void Attack()
	{
		timeSinceAttackStarted = 0;

		closestPlayer.OnDamage( new DamageInfo()
		{
			Damage = 5,
			Attacker = GameObject,
		} );
		Renderer.Set( "holdtype", 5 );
		Renderer.Set( "b_attack", true );
		closestPlayer.GetComponent<Rigidbody>().ApplyImpulse( distanceToClosest.Normal * 10000 + Vector3.Up * 2000 );

		timeSinceLastAttack = 0;
	}

	public void UpdateMove()
	{
		Renderer.Set( "holdtype", 0 );
		Rb.Velocity = ( distanceToClosest.Normal * 100 ).WithZ( Rb.Velocity.z );
		WorldRotation = Rotation.LookAt( distanceToClosest.Normal.WithZ(0), Vector3.Up );
	}

	public void UpdateAnimations()
	{
		var relativeX = Vector3.Dot( Rb.Velocity, WorldRotation.Forward );
  		var relativeY = Vector3.Dot( Rb.Velocity, WorldRotation.Right );
  		Renderer.Set( "move_x", relativeX );
  		Renderer.Set( "move_y", relativeY );
	}
}
