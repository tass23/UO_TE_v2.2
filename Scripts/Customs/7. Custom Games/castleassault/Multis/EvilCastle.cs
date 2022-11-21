using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Multis.Deeds;
using Server.Regions;
using Server.Network;
using Server.Targeting;
using Server.Accounting;
using Server.ContextMenus;
using Server.Gumps;

namespace Server.Engines.WarEvents
{
	public class EvilCastle : BaseMulti
	{	
		public EvilCastle() : base( 0x7e | 0x4000 )
		{
			Movable = false;
		}

		public EvilCastle( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
