
using System;
using Server;
using Server.Network;
using Server.Items;
using Server.Gumps;
using Server.Misc;
using Server.Mobiles;

namespace Server.Items
{
	public class GrapevinePlacementTool : Item
	{
		[Constructable]
		public GrapevinePlacementTool() : base( 0xD1A )
		{
			Movable = true;
			Hue = 0x489;
			Name = "Grapevine Placement Tool";
		}

		public GrapevinePlacementTool(Serial serial) : base( serial ){}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendGump( new AddGrapeVineGump( from, null, 0 ) );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch( version )
			{
				case 1:
				{
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