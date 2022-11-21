using System;

namespace Server.Items
{
	public class YetiHair : Item
	{
		[Constructable]
		public YetiHair() : this( 1 )
		{
		}

		[Constructable]
		public YetiHair( int amount ) : base( 0xE1F )
		{
		      	Weight = 0.1;
                  	Hue = 1153;
			Name = "Yeti Hair";
			Stackable = true;
			Amount = amount;
            	}

		public YetiHair( Serial serial ) : base( serial )
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

	public class SasquatchHair : Item
	{
		[Constructable]
		public SasquatchHair() : this( 1 )
		{
		}

		[Constructable]
		public SasquatchHair( int amount ) : base( 0xE1F )
		{
		      	Weight = 0.1;
                  	Hue = 0x45D;
			Name = "Sasquatch Hair";
			Stackable = true;
			Amount = amount;
            	}

		public SasquatchHair( Serial serial ) : base( serial )
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