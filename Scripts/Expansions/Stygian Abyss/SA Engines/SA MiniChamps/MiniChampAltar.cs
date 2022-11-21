using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Engines.SAMiniChamps
{
	public class MiniChampAltar : PentagramAddon
	{
		private MiniChampSpawn m_MiniSpawn;

		public MiniChampAltar( MiniChampSpawn spawn )
		{
			m_MiniSpawn = spawn;
		}

		public override void OnAfterDelete()
		{
			base.OnAfterDelete();

			if ( m_MiniSpawn != null )
				m_MiniSpawn.Delete();
		}

		public MiniChampAltar( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_MiniSpawn );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_MiniSpawn = reader.ReadItem() as MiniChampSpawn;

					if ( m_MiniSpawn == null )
						Delete();

					break;
				}
			}
		}
	}
}