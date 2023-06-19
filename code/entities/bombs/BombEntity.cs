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
		protected abstract int distance { get; set; }
		private float currentCooldown;


		private BomberPlayer player;

		private bool canBomb = false;

		protected readonly Vector3[] BOMB_DIRECTIONS = {new Vector3( 1, 0 ), new Vector3(0, 1), new Vector3(0, -1), new Vector3(-1, 0)};

		public BombEntity() { }
	
		public BombEntity(BomberPlayer player, string modelName, float modelScale) {

			this.player = player;
			SetModel( modelName );
			Scale = modelScale;


		
		

			if ( !(player.NumberOfBombsSpawned < player.NumberOfBombs) )
			{
				Log.Info( "test" + player.NumberOfBombsSpawned );
				Delete();

				return;
			}

			player.NumberOfBombsSpawned++;








		}

		[GameEvent.Tick.Server]
		private void Tick()
		{




			if (currentCooldown == 0)
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
		


			canBomb = true;
			



		}

		public void OnDestroyBomb()
		{
			Delete();
			player.NumberOfBombsSpawned--;
		}

		protected abstract void Bomb();
	


	}
}
