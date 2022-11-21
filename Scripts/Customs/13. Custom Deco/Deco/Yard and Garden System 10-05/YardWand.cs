using System;
using Server.Network;
using Server.Items;
using Server.Gumps;
using Server.Misc;

namespace Server.Items
{
	public class YardWand : Item
	{
		public int xstart = 15;
		public int ystart = 5;
		public int page = 0;

		[Constructable]
		public YardWand() : base( 9569 )
		{
			Movable = true;
			Name = "Yard Wand";
		}

		public YardWand(Serial serial) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			YardTarget yt = new YardTarget( this, from, 0, 0, page );
			yt.GumpUp();
//			from.SendGump( new YardGump( from, this ) );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
			writer.Write( xstart );
			writer.Write( ystart );
			writer.Write( page );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch( version )
			{
				case 1:
				{
					xstart = reader.ReadInt();
					ystart = reader.ReadInt();
					page = reader.ReadInt();
					goto case 0;
				}
				case 0:
				{
					break;
				}
			}
		}
	}
}