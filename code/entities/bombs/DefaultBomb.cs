using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.entities.bombs
{
	internal class DefaultBomb : BombEntity
	{


		protected override float cooldown { get; set; }



		public DefaultBomb() { }

		public DefaultBomb(BomberPlayer player) : base(player, "models/sbox_props/watermelon/watermelon.vmdl", 3f) {

			cooldown = 2;



		}


		protected override void Bomb()
		{

			Log.Info( "Default Bomb" );




			Vector3 forward = new Vector3( 1, 0, 0 );

			Vector3 right = new Vector3( 0, 1, 0 );
			Vector3 left = new Vector3( 0, -1, 0 );



			//DebugOverlay.Line( Position, Position + left * 400, Color.Red, duration: 10.0f );

			var traces = Trace.Ray( Position, Position + left * 400 )
			
				.Size(4)
		
				.WithAnyTags("player", "grid")
				.Ignore( this )
			
				.RunAll();

			



			for (int i = 0; i < 5; i++ )
			{
				Particles MyParticle = Particles.Create( "particles/explosion_fireball.vpcf" );


				MyParticle.SetPosition( 0, Position + left * (i * 100) );

				
			}
		


			if (traces != null)
			{


				Log.Info( traces.Length );
				foreach ( TraceResult trace in traces )
				{
					Log.Info( trace.Entity );


					trace.Entity.Delete();





				}
			}







		}

	


	}
}
