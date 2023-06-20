using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.entities.powerups
{
	abstract class PowerUpEntity : AnimatedEntity
	{




		private float yPosToGo;

		public override void Spawn()
		{
			base.Spawn();

			// Always network this entity to all clients
			Transmit = TransmitType.Always;

	



		}

		public PowerUpEntity() { }
		public PowerUpEntity( string modelName, Vector3 position )
		{

			SetModel( modelName );

			Scale = 1.5f;
			Position = new Vector3(position.x, position.y, 30);

			yPosToGo = 30;

			Log.Info( position.z + " Z" );


		}

		bool goUp = true;

		[GameEvent.Tick.Server]
		private void Tick()
		{

			float tempY = Position.z;

			

		
			

			if ( tempY < 0 || tempY >= yPosToGo)
			{
				

				goUp = !goUp;
			
			}

			if(goUp)
			{
				tempY += 1f;
				
			}
			else
			{
				tempY -= 1f;
			
			}


			Log.Info( tempY );
			



			Position = new Vector3( Position.x, Position.y,  tempY );

			Particles particle = Particles.Create( "particles/impact.glass.vpcf" );
		

			particle.SetPosition( 0, Position );
		



		}
	}
}
