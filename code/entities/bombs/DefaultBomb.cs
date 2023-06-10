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

			foreach ( Vector3 direction in BOMB_DIRECTIONS )
			{

				Log.Info( "Current Direction: " + BOMB_DIRECTIONS.Length );
			}


				foreach (Vector3 direction in BOMB_DIRECTIONS)
			{

			
				var traces = Trace.Ray( Position, Position + direction * 400 )

				.Size( 4 )

				.WithAnyTags( "player", "grid", "grid-reinforced" )
				.Ignore( this )

				.RunAll();







				int particleDistanceMultiplier = 5;



				if ( traces != null )
				{



					foreach ( TraceResult trace in traces )
					{

						Entity entity = trace.Entity;
						Log.Info( trace.Entity.Tags.Has( "grid-reinforced" ) );

						if ( trace.Entity.Tags.Has( "grid-reinforced" ) )
						{

							particleDistanceMultiplier = (int)(entity.Position - Position).Length / 80;



							break;
						}



						entity.Delete();





					}


				}


				Log.Info( particleDistanceMultiplier + " PARTICLE" );

				for ( int i = 0; i < particleDistanceMultiplier; i++ )
				{
					Particles MyParticle = Particles.Create( "particles/explosion_fireball.vpcf" );


					MyParticle.SetPosition( 0, Position + direction * (i * 100) );


				}

			}



			//DebugOverlay.Line( Position, Position + left * 400, Color.Red, duration: 10.0f );








		}

	


	}
}
