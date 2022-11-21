using System;
using Server;
using Server.Misc;
using Server.Items;
using Server.Targeting;
using Server.Commands;

namespace Server.Items
{
    public class AccessLevelStone : Item
	{
		private Mobile m_Owner;
		private AccessLevel m_StoredAccessLevel;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner { get { return m_Owner; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public AccessLevel NextAccessLevel { get { return m_StoredAccessLevel; } }

		[Constructable]
		public AccessLevelStone() : base( 0x1870 )
		{
			Weight = 1.0;
			Hue = 0x38;
			Name = "Access Level Stone"; //Je.
			LootType = LootType.Blessed;
			m_StoredAccessLevel = AccessLevel.Player;
		}

		public override void OnDoubleClick( Mobile m )
		{
			if( !IsChildOf( m.Backpack ) )
			{
				m.SendLocalizedMessage( 1042001 );
				return;
			}

			if( Owner == null || Owner == m || m.AccessLevel > Owner.AccessLevel )
			{
				if( Owner == null && m.AccessLevel > AccessLevel.Player )
					m_Owner = m;
				else if( Owner != null && m.AccessLevel > Owner.AccessLevel )
					m_Owner = m; //Se la roba.
				else if( m != Owner && m.AccessLevel < AccessLevel.Counselor )
				{
					m.SendMessage( "You are unable to use that!" );
					Delete();
					return;
				}

				if( NextAccessLevel == AccessLevel.Player )
				{
					m_StoredAccessLevel = m_Owner.AccessLevel;
					m_Owner.AccessLevel = AccessLevel.Player;
				}
				else
				{
					m_Owner.AccessLevel = NextAccessLevel;
					m_StoredAccessLevel = AccessLevel.Player;
				}

				m.SendMessage( "AccessLevel changed!" );
			}
			else
			{
				m.SendMessage( "That is not yours!" );
				Delete();
			}
		}

		public AccessLevelStone( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)1 ); // version 

			//Version 1:
			writer.Write( (int)m_StoredAccessLevel );

			writer.Write( m_Owner != null );

			if( m_Owner != null )
				writer.Write( m_Owner );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			switch( version )
			{
				case 1:
				{
					m_StoredAccessLevel = (AccessLevel)reader.ReadInt();

					if( reader.ReadBool() )
						m_Owner = reader.ReadMobile();

					break;
				}
			}
		}
	}
}