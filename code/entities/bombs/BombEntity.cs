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

			
		}

	

		private BomberPlayer player;

		public BombEntity() { }
	
		public BombEntity(BomberPlayer player, string modelName, float modelScale) {

			this.player = player;
			SetModel( modelName );
			Scale = modelScale;


			Log.Info( "SPAWNED" );
		}

		protected abstract float cooldown { get; set; }






		public void UseBomb()
		{
			Bomb();


			this.Position = player.Position;
			Log.Info( player.Position );
			

		}


		protected abstract void Bomb();
	


	}
}
