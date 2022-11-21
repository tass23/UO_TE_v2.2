using System;
using System.Collections;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class BaconAndEgg : ContainerFood
	{
		public override int MinSkill		{get { return 0; } }
		public override int MaxSkill		{get { return 100; } }
		public override bool NeedSilverware	{get { return false; } }
		public override string CookedMessage	{get { return "You make a plate of bacon and eggs."; } }
		public override Item FoodContainer	{get { return new DirtyPlate(); } }

		[Constructable]
		public BaconAndEgg() : base( 0x9DB )
		{
			Name = "Bacon and Eggs";
			Uses = 3;
			FillFactor = 5;
		}

		public BaconAndEgg( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}