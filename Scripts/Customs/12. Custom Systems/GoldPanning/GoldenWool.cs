using System;
using Server.Items;
using Server.Targeting;
using System.Collections.Generic;

namespace Server.Items
{
	public class GoldenWool : Item
	{
		[Constructable]
		public GoldenWool() : this( 1 )
		{
		}

		[Constructable]
		public GoldenWool( int amount ) : base( 0xDF8 )
		{
			Stackable = true;
			Weight = 4.0;
			Amount = amount;
			Hue = 1260;
			Name = "Mysterious piece of artifact";
		}

		public GoldenWool( Serial serial ) : base( serial )
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
	
	public class GoldenFleece : BearMask
	{
		public override int BasePhysicalResistance{ get{ return 15; } }
		public override int BaseFireResistance{ get{ return 13; } }
		public override int BaseColdResistance{ get{ return 18; } }
		public override int BasePoisonResistance{ get{ return 14; } }
		public override int BaseEnergyResistance{ get{ return 14; } }

		public override int InitMinHits{ get{ return 40; } }
		public override int InitMaxHits{ get{ return 60; } }

		[Constructable]
		public GoldenFleece()
		{
			Weight = 5.0;
			Hue = 1260;
			Name = "Jason's Golden Fleece";
			
			Attributes.RegenHits = 5;
			Attributes.BonusStr = 15;
			Attributes.ReflectPhysical = 20;
			Attributes.AttackChance = 18;
		}

		public GoldenFleece( Serial serial ) : base( serial )
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