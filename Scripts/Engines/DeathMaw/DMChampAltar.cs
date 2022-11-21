using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Engines.DMChamps;

namespace Server.Engines.DMChamps
{
	public class DMChampAltar : PentagramAddon
	{
		private DMChampSpawn m_DMSpawn;

		public DMChampAltar( DMChampSpawn spawn )
		{
			m_DMSpawn = spawn;
		}

		public override void OnAfterDelete()
		{
			base.OnAfterDelete();

			if ( m_DMSpawn != null )
				m_DMSpawn.Delete();
		}

		public DMChampAltar( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_DMSpawn );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_DMSpawn = reader.ReadItem() as DMChampSpawn;

					if ( m_DMSpawn == null )
						Delete();

					break;
				}
			}
		}
	}
}