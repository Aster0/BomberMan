using Sandbox.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.entities.bombs
{
	abstract class BombEntity : AnimatedEntity 
	{


		public override void Spawn()
		{
			base.Spawn();
			// Always network this entity to all clients
			Transmit = TransmitType.Always;

		}
		protected abstract float cooldown { get; set; }
		private float currentCooldown;


		private BomberPlayer player;

		private bool canBomb = false;

		public BombEntity() { }
	
		public BombEntity(BomberPlayer player, string modelName, float modelScale) {

			this.player = player;
			SetModel( modelName );
			Scale = modelScale;
			

			Log.Info( "SPAWNED" );

		


		}

		[GameEvent.Tick.Server]
		private void Tick()
		{

			if(currentCooldown == 0)
				currentCooldown = cooldown;

			if ( canBomb )
			{
				currentCooldown -= Time.Delta;

				if( currentCooldown < 0 )
				{
					currentCooldown = cooldown;
					canBomb = false;

					Bomb();
				}
			}
		}




		


	




		public void UseBomb()
		{
			


			this.Position = player.Position;
			Log.Info( player.Position );


			canBomb = true;
			



		}


		protected abstract void Bomb();
	


	}
}
