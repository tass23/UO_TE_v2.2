

using System;
using Server;

namespace Server.Items
{
	public class EttinCostume : Item
	{

		[Constructable]
		public EttinCostume() : base( 0x2684 )
		{
                        Name = "Ettin Costume";
			
                        Hue = 43;
                        Layer = Layer.OuterTorso;
                        ItemID = 0x2684;

			Weight = 3.0;
		}

		public EttinCostume( Serial serial ) : base( serial )
		{
		}

     		 public override bool OnEquip( Mobile from )
		{ 
			if ( from.Mounted == true )
			{
				from.SendMessage( "You cannot be mounted while wearing your costume!" );
				return false;
			}
			if ( from.BodyMod != 0 )
			{
				from.SendMessage( "You cannot wear the costume while your body is transformed!" );
				return false;
			}
			//can place more checks for spell transforms, etc also if you want
			if(base.OnEquip( from ))
			{
				from.SendMessage( "You put on the costume." );				
				from.BodyMod = 18;
from.NameHue = 39;
				from.DisplayGuildTitle = false; return true;
			}
			return false;
		}
						
 public override void OnRemoved( object parent )
              {

                if ( parent is Mobile )
			{
			       Mobile from = (Mobile)parent;
			       from.SendMessage( "You take off the costume." );
                               from.BodyMod = 0;
                               from.NameHue = -1;
                               from.HueMod = -1;
                               from.DisplayGuildTitle = true;
			}

	      if( parent is Mobile && ((Mobile)parent).Kills >= 5)
               {
                    ( (Mobile)parent).Criminal = true;
               }
            if( parent is Mobile && ((Mobile)parent).GuildTitle != null )
               {
                     ( (Mobile)parent).DisplayGuildTitle = true;
               }                

                base.OnRemoved( parent );
            }
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
