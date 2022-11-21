using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class HalloweenGhoulStatuette : MonsterStatuette
	{
		[Constructable]
		public HalloweenGhoulStatuette() : base( MonsterStatuetteType.Zombie )
		{
            Name = "Halloween Zombie Statuette";
			ItemID = 8457;
			Hue = Utility.RandomList(243, 997);
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public override bool AllowEquipedCast( Mobile from )
		{
			return true;
		}

		public HalloweenGhoulStatuette( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( "Halloween insert_year" );
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