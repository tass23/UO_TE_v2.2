using System;
using Server.Engines.VeteranRewards;

namespace Server.Items
{
    [Flipable(0x46B4, 0x46B5)]
    public class GargishSash : BaseClothing
    {
        public override Race RequiredRace { get { return Race.Gargoyle; } }
        public override bool CanBeWornByGargoyles { get { return true; } }

        [Constructable]
        public GargishSash(): this(0)
        {
        }

        [Constructable]
        public GargishSash(int hue): base(0x46B4, Layer.MiddleTorso, hue)
        {
            Weight = 1.0;
        }

        public GargishSash(Serial serial): base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
	public class GargishClothChest : BaseClothing
	{
		public override Race RequiredRace { get { return Race.Gargoyle; } }
		public override bool CanBeWornByGargoyles{ get{ return true; } }

		[Constructable]
		public GargishClothChest() : this( 0 )
		{
		}

		[Constructable]
		public GargishClothChest( int hue ) : base( 0x0406, Layer.InnerTorso, hue )
		{
			Weight = 2.0;
		}

		public override void OnAdded( object parent )
		{
			if ( parent is Mobile )
			{
				if ( ((Mobile)parent).Female )
					ItemID = 0x0405;
				else
					ItemID = 0x0406;
			}
		}

		public GargishClothChest( Serial serial ) : base( serial )
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

	public class GargishClothArms : BaseClothing
	{
		public override Race RequiredRace { get { return Race.Gargoyle; } }
		public override bool CanBeWornByGargoyles{ get{ return true; } }

		[Constructable]
		public GargishClothArms() : this( 0 )
		{
		}

		[Constructable]
		public GargishClothArms( int hue ) : base( 0x0404, Layer.Arms, hue )
		{
			Weight = 2.0;
		}

		public override void OnAdded( object parent )
		{
			if ( parent is Mobile )
			{
				if ( ((Mobile)parent).Female )
					ItemID = 0x0403;
				else
					ItemID = 0x0404;
			}
		}

		public GargishClothArms( Serial serial ) : base( serial )
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

    [Flipable(0x4002, 0x4003)]
    public class GargishFancyRobe : BaseClothing
    {
		public override Race RequiredRace { get { return Race.Gargoyle; } }
        public override bool CanBeWornByGargoyles { get { return true; } }
	
		[Constructable]
		public GargishFancyRobe()
			: this(0)
		{
		}
	
		[Constructable]
		public GargishFancyRobe(int hue) : base(0x4002, Layer.OuterTorso, hue)
		{
			Weight = 1.0;
		}

		public GargishFancyRobe(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}
	
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
    }

    [Flipable(0x4000, 0x4001)]
    public class GargishRobe : BaseClothing
    {
		public override Race RequiredRace { get { return Race.Gargoyle; } }
        public override bool CanBeWornByGargoyles { get { return true; } }
	
		[Constructable]
		public GargishRobe()
			: this(0)
		{
		}
	
		[Constructable]
		public GargishRobe(int hue) : base(0x4000, Layer.OuterTorso, hue)
		{
			Weight = 1.0;
		}

		public GargishRobe(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}
	
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
    }

	public class GargishClothKilt : BaseClothing
	{
		public override Race RequiredRace { get { return Race.Gargoyle; } }
		public override bool CanBeWornByGargoyles{ get{ return true; } }

		[Constructable]
		public GargishClothKilt() : this( 0 )
		{
		}

		[Constructable]
		public GargishClothKilt( int hue ) : base( 0x0408, Layer.OuterLegs, hue )
		{
			Weight = 2.0;
		}

		public override void OnAdded( object parent )
		{
			if ( parent is Mobile )
			{
				if ( ((Mobile)parent).Female )
					ItemID = 0x0407;
				else
					ItemID = 0x0408;
			}
		}

		public GargishClothKilt( Serial serial ) : base( serial )
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

	public class GargishClothLegs : BaseClothing
	{
		public override Race RequiredRace { get { return Race.Gargoyle; } }
		public override bool CanBeWornByGargoyles{ get{ return true; } }

		[Constructable]
		public GargishClothLegs() : this( 0 )
		{
		}

		[Constructable]
		public GargishClothLegs( int hue ) : base( 0x040A, Layer.Pants, hue )
		{
			Weight = 2.0;
		}

		public override void OnAdded( object parent )
		{
			if ( parent is Mobile )
			{
				if ( ((Mobile)parent).Female )
					ItemID = 0x0409;
				else
					ItemID = 0x040A;
			}
		}

		public GargishClothLegs( Serial serial ) : base( serial )
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
	
	[FlipableAttribute( 0x45A4, 0x45A5 )]
	public class GargishClothWingArmor : BaseClothing
	{
		public override Race RequiredRace { get { return Race.Gargoyle; } }
		public override bool CanBeWornByGargoyles{ get{ return true; } }

		[Constructable]
		public GargishClothWingArmor() : this( 0 )
		{
		}

		[Constructable]
		public GargishClothWingArmor( int hue ) : base( 0x45A4, Layer.Cloak, hue )
		{
			Weight = 2.0;
		}

		public GargishClothWingArmor( Serial serial ) : base( serial )
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

    [FlipableAttribute(0x50D8, 0x50D9)]
    public class GargishApron : BaseWaist
    {
        public override Race RequiredRace { get { return Race.Gargoyle; } }
        public override bool CanBeWornByGargoyles { get { return true; } }

        [Constructable]
        public GargishApron(): this(0)
        {
        }

        [Constructable]
        public GargishApron(int hue): base(0x50D8, hue)
        {
            Weight = 2.0;
        }

        public GargishApron(Serial serial): base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}