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
		protected override int distance { get; set; }


		public DefaultBomb() { }

		public DefaultBomb(BomberPlayer player) : base(player, "models/sbox_props/watermelon/watermelon.vmdl", 3f) {

			cooldown = 2;
			distance = 4;



		}


		protected override void Bomb()
		{

		

			foreach (Vector3 direction in BOMB_DIRECTIONS)
			{

			
				var traces = Trace.Ray( Position, Position + direction * ((distance * 100) - 100) )

				.Size( distance - 1 )

				.WithAnyTags( "player", "grid", "grid-reinforced" )
				.Ignore( this )

				.RunAll();







				int particleDistanceMultiplier = distance;



				if ( traces != null )
				{



					foreach ( TraceResult trace in traces )
					{

						Entity entity = trace.Entity;
					

						if ( entity.Tags.Has( "grid-reinforced" ) )
						{

							particleDistanceMultiplier = (int) (entity.Position - Position).Length / 80;



							break;
						}



						entity.Delete();





					}


				}


			

				for ( int i = 0; i < particleDistanceMultiplier; i++ )
				{
					Particles MyParticle = Particles.Create( "particles/explosion_fireball.vpcf" );


					MyParticle.SetPosition( 0, Position + direction * (i * 100) );


				}



			}

			OnDestroyBomb();
			



			//DebugOverlay.Line( Position, Position + left * 400, Color.Red, duration: 10.0f );








		}

	


	}
}
