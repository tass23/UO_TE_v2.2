using System;

namespace Server.Items
{

	public class ObsidianEarrings : GoldEarrings
	{
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }
		public override bool CanBeWornByGargoyles{ get{ return true; } }
		public override Race RequiredRace { get { return Race.Gargoyle; } }
		[Constructable]
		public ObsidianEarrings() : base( )
		{
			Name = ("Obsidian Earrings");
		
			Hue = 1;
			
			Attributes.BonusMana = 8;
			Attributes.RegenMana = 2;
			Attributes.RegenStam = 2;
			Attributes.SpellDamage = 8;
			Resistances.Physical = 4;
			Resistances.Fire = 10;
			Resistances.Cold = 10;
			Resistances.Poison = 3;
			Resistances.Energy = 13;
			//AbsorptionAttribute.CastingFocus = 4; TODO: how this shit works?
			
		}

		public ObsidianEarrings( Serial serial ) : base( serial )
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