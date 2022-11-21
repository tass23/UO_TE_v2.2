using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
    public class TripartiteKey  : Item
	{
        public override int LabelNumber { get { return 1111649; } } //Tripartite Key

		[Constructable]
        	public TripartiteKey(): base(0x1012)
		{
			Weight = 1.0;
            		Hue = 1123;
            		Movable = false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			PlayerMobile pm = from as PlayerMobile;

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			
			else if ( pm.AbyssEntry )
			{
                pm.SendMessage("You have completed a Sacred quest already!");
				Delete();
			}
			else
			{
				pm.AbyssEntry = true;
                pm.SendMessage("You have completed the Sacred quest and now have entry to the Abyss!");
               	Delete();
			}
		}

        	public TripartiteKey (Serial serial) : base(serial)
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