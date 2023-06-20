using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.entities.powerups
{
	internal class ExtraBombPowerUp : PowerUpEntity
	{
		public ExtraBombPowerUp() { }


		public ExtraBombPowerUp(Vector3 position) : base( "models/sbox_props/watermelon/watermelon.vmdl", position)
		{
			Log.Info( "test" );
		}


	}
}
