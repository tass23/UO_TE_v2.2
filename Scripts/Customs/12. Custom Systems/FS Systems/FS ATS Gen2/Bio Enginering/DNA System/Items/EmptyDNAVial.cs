using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Items;

namespace Server.Items
{
	public class EmptyDNAVial : Item
	{
		[Constructable]
		public EmptyDNAVial() : this( 1 )
		{
		}

		[Constructable]
		public EmptyDNAVial( int amount ) : base( 3620 )
		{
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
			Name = "empty DNA vial";
		}

        	public override void OnDoubleClick( Mobile from )
        	{
			if ( IsChildOf( from.Backpack ) )
			{

				PlayerMobile pm = (PlayerMobile)from;
				if ( from.Skills[SkillName.AnimalTaming].Base < 100.0 && from.Skills[SkillName.Tinkering].Base < 100.0 )
				{
					from.SendMessage( "You have no clue how to use this." );
				}
				else if ( FSATS.EnableBioEngineer == false )
				{
					from.SendMessage( "Bio-Engineering has been disabled on this server, Please contact your server administrator for more information." );
				}
				else if ( pm.Bioenginer == false )
				{
					from.SendMessage( "You have no clue how to use this." );
				}
				else
				{
					from.CloseGump( typeof( SampleDNAGump ) );
					from.SendGump( new SampleDNAGump( this ) );
				}
			}
			else
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
		}

		public EmptyDNAVial( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version 
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
