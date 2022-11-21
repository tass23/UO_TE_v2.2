using System;
using Server;

namespace Server.Items
{
	public class BountyHead : Item
	{
		private Mobile m_Owner;
		private Mobile m_Killer;
		private DateTime m_TimeOfDeath;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get{ return m_Owner; }
			set{ m_Owner = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Killer
		{
			get{ return m_Killer; }
			set{ m_Killer = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime TimeOfDeath
		{
			get{ return m_TimeOfDeath; }
			set{ m_TimeOfDeath = value; }
		}

		[Constructable]
		public BountyHead() : this( null, null, null, DateTime.MinValue )
		{
		}

		[Constructable]
		public BountyHead( string name, Mobile owner, Mobile killer, DateTime tod ) : base( 0x1DA0 )
		{
			Name = name;
			Weight = 1.0;

			m_Owner = owner;
			m_Killer = killer;
			m_TimeOfDeath = tod;
		}
		
		public BountyHead( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version

			writer.Write( (Mobile) m_Owner );
			writer.Write( (Mobile) m_Killer );
			writer.WriteDeltaTime( m_TimeOfDeath );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch( version )
			{
				case 0:
				{
					m_Owner = reader.ReadMobile();
					m_Killer = reader.ReadMobile();
					m_TimeOfDeath = reader.ReadDeltaTime();
					break;
				}
			}
		}
	}
}