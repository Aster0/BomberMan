using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.entities.bombs
{
	internal class DefaultBomb : BombEntity
	{


		protected override float cooldown { get; set; }



		public DefaultBomb() { }

		public DefaultBomb(BomberPlayer player) : base(player, "models/sbox_props/watermelon/watermelon.vmdl", 3f) {

			cooldown = 5;


		}


		protected override void Bomb()
		{

			Log.Info( "Default Bomb" );


			var tr = Trace.Ray( Position, new Vector3(Position.x , Position.y, Position.z * 5) )
		
				.Ignore( this )
				.Run();

			// See if any of the parent entities are usable if we ain't.
			var ent = tr.Entity;

			Log.Info( ent );


		}


	}
}
