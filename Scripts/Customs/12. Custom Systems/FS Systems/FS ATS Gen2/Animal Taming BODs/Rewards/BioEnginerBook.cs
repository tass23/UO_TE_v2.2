using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public class BioEnginerBook : Item
	{
		[Constructable]
		public BioEnginerBook() : base( 0xFF4 )
		{
			Name = "crafting Bio-engineering pets";
			Weight = 1.0;
		}

		public BioEnginerBook( Serial serial ) : base( serial )
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

		public override void OnDoubleClick( Mobile from )
		{
			PlayerMobile pm = from as PlayerMobile;

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else if ( FSATS.EnableBioEngineer == false )
			{
				from.SendMessage( "Bio-Engineering has been disabled on this server, Please contact your server administrator for more information." );
			}
			else if ( pm == null || from.Skills[SkillName.AnimalTaming].Base < 100.0 && from.Skills[SkillName.Tinkering].Base < 100.0 )
			{
				pm.SendMessage( "You must be a grandmaster tamer and tinker to learn bio-enginering." );
			}
			else if ( pm.Bioenginer )
			{
				pm.SendMessage( "You have already learned this information." );
			}
			else
			{
				pm.Bioenginer = true;
				pm.SendMessage( "You have learned bio-enginering." );
				Delete();
			}
		}
	}
}