using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public abstract class BaseArtyOre : Item, ICommodity
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}
		
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

		}

		public BaseArtyOre( int amount ) : base( 0x09EA )
		{
			Stackable = true;
			Amount = amount;
			Hue = 1281;
		}

		public BaseArtyOre( Serial serial ) : base( serial )
		{
		}


		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
		}
	}

	public class ArtyOre : BaseArtyOre
	{
		[Constructable]
		public ArtyOre() : this( 1 )
		{
		}

		[Constructable]
		public ArtyOre( int amount ) : base( amount )
		{
			Name = "Artifact Ore";
		}

		public ArtyOre( Serial serial ) : base( serial )
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