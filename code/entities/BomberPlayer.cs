using Sandbox.entities.bombs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.entities
{
	public partial class BomberPlayer : AnimatedEntity
	{


		private BombEntity bombEntity;

	



		public override void Spawn()
		{
			base.Spawn();

		
			SetModel( "models/citizen/citizen.vmdl" );




			Log.Info( "Spawned " + Tags );


	

			SetupPhysicsFromCapsule( PhysicsMotionType.Keyframed, Capsule.FromHeightAndRadius( 10, 15 ) );


		}


		[ClientInput] public Vector3 InputDirection { get; protected set; }
		[ClientInput] public Angles ViewAngles { get; set; }

		public override void BuildInput()
		{
			InputDirection = Input.AnalogMove;

			var look = Input.AnalogLook;

			var viewAngles = ViewAngles;
			viewAngles += look;
			ViewAngles = viewAngles.Normal;
		}


		public override void FrameSimulate( IClient cl )
		{
			base.FrameSimulate( cl );

			Camera.Rotation = Rotation.LookAt( new Vector3( 0f, 0, -1 ) );
			Camera.Position = new Vector3( -101.3794f, -217.9602f, 3000 );
		}

		public override void Simulate( IClient cl )
		{
			base.Simulate( cl );




			HandleMovement();

			if ( Game.IsServer && Input.Pressed( "attack1" ) )
			{

				
				if(bombEntity == null)
						bombEntity = new DefaultBomb(this);

				bombEntity.UseBomb();

			
			}
			else if(Game.IsServer && Input.Pressed("jump"))
			{

				Entity[] entities = new Entity[1];
				entities[0] = this;

				Game.ResetMap( entities );
			}

		


		}


		private void HandleMovement()
		{
			var movement = InputDirection.Normal;



			Velocity = movement;



			if ( movement.x == 1 || movement.x == -1 )
			{
				// look up smoothly


				float x = movement.x;

				movement = new Vector3( 30f, 0.5f, 0 );

				movement.x *= x;
			}

		



			if ( movement.Length > 0 )
				Rotation = Rotation.Lerp( Rotation, Rotation.LookAt( movement ), Time.Delta * 20f );





			// apply some speed to it
			Velocity *= Input.Down( "run" ) ? 350 : 200;

			// apply it to our position using MoveHelper, which handles collision
			// detection and sliding across surfaces for us
			MoveHelper helper = new MoveHelper( Position, Velocity );

			Trace trace = helper.Trace.Size( 16 );
			trace = trace.Ignore( this );

			helper.Trace = trace;

		

			if ( helper.TryMove( Time.Delta ) > 0 )
			{
				Position = helper.Position;

			}





		}




	}
}
