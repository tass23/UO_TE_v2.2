using System;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;
using Server.Gumps;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;
using Server;
using Server.Items;
using Server.Engines.Craft;
using Server.Engines.Plants;

namespace Server.Items
{
	public class BasketweavingPliers : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefBasketweaving.CraftSystem; } }
        public override int LabelNumber{ get{ return 1112245; } } // Basket Weaving Pliers

		[Constructable]
		public BasketweavingPliers() : base( 0x0FBB )
		{
			Hue = 2208;
			Weight = 1.0;
		}

		[Constructable]
		public BasketweavingPliers( int uses ) : base( uses, 0x1028 )
		{
			Weight = 1.0;
			Hue = 2208;
		}

		public BasketweavingPliers( Serial serial ) : base( serial )
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
			else if ( pm.BasketWeaving )
			{
				pm.SendMessage( "Pick a basket to make from the gump." );

			}
			else if (pm.BasketWeaving == false)
			{
                pm.SendMessage("You need to read a book, and learn Basketweaving in order to use this tool.");
                return;
			}

            base.OnDoubleClick(from);
		}
	}
}
	