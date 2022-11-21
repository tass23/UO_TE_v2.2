using System;
using Server.Mobiles;
using Server.Items;
using Server.Gumps;

namespace Server.Items
{
   	public class BioPetDeed : Item
   	{
      		[Constructable]
      		public BioPetDeed() : base( 0x14F0 )
      		{
         		Movable = true;
         		Name = "a bio engineered pet deed";
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
				else
				{
					if ( from.Followers == 0 )
					{
        					BaseBioCreature bio = new BaseBioCreature();
        					bio.Controlled = true;
        					bio.ControlMaster = from;
						bio.Crafter = from;
        					bio.Location = from.Location;
        					bio.Map = from.Map;
        					World.AddMobile( bio );
               					from.SendMessage( "You have created a bio clone." );
      						this.Delete();
					}
					else
					{
						from.SendMessage( "Please shrink or stable all pets before using this deed." );
					}
				}
			}
			else
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
      		}

      		public BioPetDeed( Serial serial ) : base( serial )
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
