using System;

namespace Server.Items
{

	public class DragonJadeEarrings : GoldEarrings
	{
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }
		public override bool CanBeWornByGargoyles{ get{ return true; } }
		public override Race RequiredRace { get { return Race.Gargoyle; } }
		[Constructable]
		public DragonJadeEarrings() : base( ) //TODO: GargishEarrings ItemID
	
		{
			Name = ("Dragon Jade Earrings");
		
			Hue = 2129;
			
			Attributes.BonusDex = 5;
			Attributes.BonusStr = 5;
			Attributes.RegenHits = 2;
			Attributes.RegenStam = 3;
			Attributes.LowerManaCost = 5;
			Resistances.Physical = 9;
			Resistances.Fire = 16;
			Resistances.Cold = 5;
			Resistances.Poison = 13;
			Resistances.Energy = 3;
			//AbsorptionAttribute.EaterFire = 10;
			
		}

		public DragonJadeEarrings( Serial serial ) : base( serial )
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