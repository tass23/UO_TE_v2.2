using System;
using Server;

namespace Server.Items
{
	[Flipable( 0x1F03, 0x1F04 )]
    public class RobeOfTheEquinox : BaseOuterTorso, ITokunoDyable
	{
		public override int LabelNumber{ get{ return 1075042; } } // Robe of the Equinox

		private bool m_ElvesOnly;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool ElvesOnly
		{
			get { return m_ElvesOnly; }
			set { m_ElvesOnly = value; }
		}

		[Constructable]
		public RobeOfTheEquinox() : base( 0x1F04, 0xD6 )
		{
			m_ElvesOnly = Utility.RandomBool();

			Weight = 3.0;
			Attributes.Luck = 95;
			// TODO: Supports arcane?
		}

		public RobeOfTheEquinox( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_ElvesOnly )
				list.Add( 1075086 ); // Elves Only
		}

		public override bool AllowFemaleWearer
		{
			get
			{
				return base.AllowFemaleWearer;
			}
		}

		public override bool CanEquip( Mobile from )
		{
			if ( m_ElvesOnly && from.Race != Race.Elf )
				from.SendLocalizedMessage( 1072203 ); // Only Elves may use this.
			else
				return true;
			
			return false;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 1 ); // version

			writer.Write( m_ElvesOnly );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();

			switch ( version )
			{
				case 1: 
					m_ElvesOnly = reader.ReadBool(); 
					break;
			}			
		}
	}
}