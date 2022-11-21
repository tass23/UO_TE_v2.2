using System;

namespace Server.Items
{
	[FlipableAttribute( 0x153b, 0x153c )]
	public class BloodyHalfApron : BaseWaist
	{
		[Constructable]
		public BloodyHalfApron() : this( 0 )
		{
		}

		[Constructable]
		public BloodyHalfApron( int hue ) : base( 0x153b, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 2.0;
		}

		public BloodyHalfApron( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[Flipable( 0x27A0, 0x27EB )]
	public class BloodyObi : BaseWaist
	{
		[Constructable]
		public BloodyObi() : this( 0 )
		{
		}

		[Constructable]
		public BloodyObi( int hue ) : base( 0x27A0, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 1.0;
		}

		public BloodyObi( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[FlipableAttribute( 0x2B68, 0x315F )]
	public class BloodyWoodlandBelt : BaseWaist
	{
		public override Race RequiredRace { get { return Race.Elf; } }

		[Constructable]
		public BloodyWoodlandBelt() : this( 0 )
		{
		}

		[Constructable]
		public BloodyWoodlandBelt( int hue ) : base( 0x2B68, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 4.0;
		}

		public BloodyWoodlandBelt( Serial serial ) : base( serial )
		{
		}

		public override bool Scissor( Mobile from, Scissors scissors )
		{
			from.SendLocalizedMessage( 502440 ); // Scissors can not be used on that to produce anything.
			return false;
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	public class BloodyCloak : BaseCloak
	{
		[Constructable]
		public BloodyCloak() : this( 0 )
		{
		}

		[Constructable]
		public BloodyCloak( int hue ) : base( 0x1515, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 5.0;
		}

		public BloodyCloak( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
		}

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
	}
	public class BloodyKasa : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }
		public override int InitMinHits{ get{ return 30; } }
		public override int InitMaxHits{ get{ return 30; } }

		[Constructable]
		public BloodyKasa() : this( 0 )
		{
		}

		[Constructable]
		public BloodyKasa( int hue ) : base( 0x2798, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 3.0;
		}

		public BloodyKasa( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[Flipable( 0x278F, 0x27DA )]
	public class BloodyClothNinjaHood : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 6; } }
		public override int BasePoisonResistance{ get{ return 9; } }
		public override int BaseEnergyResistance{ get{ return 9; } }
		public override int InitMinHits{ get{ return 30; } }
		public override int InitMaxHits{ get{ return 30; } }

		[Constructable]
		public BloodyClothNinjaHood() : this( 0 )
		{
		}

		[Constructable]
		public BloodyClothNinjaHood( int hue ) : base( 0x278F, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 2.0;
		}

		public BloodyClothNinjaHood( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[Flipable( 0x2306, 0x2305 )]
	public class BloodyFlowerGarland : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 6; } }
		public override int BasePoisonResistance{ get{ return 9; } }
		public override int BaseEnergyResistance{ get{ return 9; } }
		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 20; } }

		[Constructable]
		public BloodyFlowerGarland() : this( 0 )
		{
		}

		[Constructable]
		public BloodyFlowerGarland( int hue ) : base( 0x2306, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 1.0;
		}

		public BloodyFlowerGarland( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	public class BloodyFloppyHat : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }
		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 20; } }

		[Constructable]
		public BloodyFloppyHat() : this( 0 )
		{
		}

		[Constructable]
		public BloodyFloppyHat( int hue ) : base( 0x1713, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 1.0;
		}

		public BloodyFloppyHat( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	public class BloodyWideBrimHat : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }
		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 20; } }

		[Constructable]
		public BloodyWideBrimHat() : this( 0 )
		{
		}

		[Constructable]
		public BloodyWideBrimHat( int hue ) : base( 0x1714, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 1.0;
		}

		public BloodyWideBrimHat( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	public class BloodyCap : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }
		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 20; } }

		[Constructable]
		public BloodyCap() : this( 0 )
		{
		}

		[Constructable]
		public BloodyCap( int hue ) : base( 0x1715, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 1.0;
		}

		public BloodyCap( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	public class BloodySkullCap : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 5; } }
		public override int BasePoisonResistance{ get{ return 8; } }
		public override int BaseEnergyResistance{ get{ return 8; } }
		public override int InitMinHits{ get{ return 14; } }
		public override int InitMaxHits{ get{ return 14; } }

		[Constructable]
		public BloodySkullCap() : this( 0 )
		{
		}

		[Constructable]
		public BloodySkullCap( int hue ) : base( 0x1544, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 1.0;
		}

		public BloodySkullCap( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	public class BloodyBandana : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 5; } }
		public override int BasePoisonResistance{ get{ return 8; } }
		public override int BaseEnergyResistance{ get{ return 8; } }
		public override int InitMinHits{ get{ return 12; } }
		public override int InitMaxHits{ get{ return 12; } }

		[Constructable]
		public BloodyBandana() : this( 0 )
		{
		}

		[Constructable]
		public BloodyBandana( int hue ) : base( 0x1540, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 1.0;
		}

		public BloodyBandana( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	public class BloodyBearMask : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 5; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 8; } }
		public override int BasePoisonResistance{ get{ return 4; } }
		public override int BaseEnergyResistance{ get{ return 4; } }
		public override int InitMinHits{ get{ return 27; } }
		public override int InitMaxHits{ get{ return 27; } }

		[Constructable]
		public BloodyBearMask() : this( 0 )
		{
		}

		[Constructable]
		public BloodyBearMask( int hue ) : base( 0x1545, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 5.0;
		}

		public BloodyBearMask( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	public class BloodyDeerMask : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 2; } }
		public override int BaseFireResistance{ get{ return 6; } }
		public override int BaseColdResistance{ get{ return 8; } }
		public override int BasePoisonResistance{ get{ return 1; } }
		public override int BaseEnergyResistance{ get{ return 7; } }
		public override int InitMinHits{ get{ return 26; } }
		public override int InitMaxHits{ get{ return 26; } }

		[Constructable]
		public BloodyDeerMask() : this( 0 )
		{
		}

		[Constructable]
		public BloodyDeerMask( int hue ) : base( 0x1547, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 4.0;
		}

		public BloodyDeerMask( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	public class BloodyHornedTribalMask : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 6; } }
		public override int BaseFireResistance{ get{ return 9; } }
		public override int BaseColdResistance{ get{ return 0; } }
		public override int BasePoisonResistance{ get{ return 4; } }
		public override int BaseEnergyResistance{ get{ return 5; } }
		public override int InitMinHits{ get{ return 28; } }
		public override int InitMaxHits{ get{ return 28; } }

		[Constructable]
		public BloodyHornedTribalMask() : this( 0 )
		{
		}

		[Constructable]
		public BloodyHornedTribalMask( int hue ) : base( 0x1549, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 2.0;
		}

		public BloodyHornedTribalMask( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	public class BloodyTribalMask : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 0; } }
		public override int BaseColdResistance{ get{ return 6; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 5; } }
		public override int InitMinHits{ get{ return 24; } }
		public override int InitMaxHits{ get{ return 24; } }

		[Constructable]
		public BloodyTribalMask() : this( 0 )
		{
		}

		[Constructable]
		public BloodyTribalMask( int hue ) : base( 0x154B, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 2.0;
		}


		public BloodyTribalMask( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	public class BloodyTallStrawHat : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }
		public override int InitMinHits{ get{ return 14; } }
		public override int InitMaxHits{ get{ return 14; } }

		[Constructable]
		public BloodyTallStrawHat() : this( 0 )
		{
		}

		[Constructable]
		public BloodyTallStrawHat( int hue ) : base( 0x1716, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 1.0;
		}

		public BloodyTallStrawHat( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	public class BloodyStrawHat : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }
		public override int InitMinHits{ get{ return 10; } }
		public override int InitMaxHits{ get{ return 10; } }

		[Constructable]
		public BloodyStrawHat() : this( 0 )
		{
		}

		[Constructable]
		public BloodyStrawHat( int hue ) : base( 0x1717, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 1.0;
		}

		public BloodyStrawHat( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	public class BloodyOrcishKinMask : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 1; } }
		public override int BaseFireResistance{ get{ return 1; } }
		public override int BaseColdResistance{ get{ return 7; } }
		public override int BasePoisonResistance{ get{ return 7; } }
		public override int BaseEnergyResistance{ get{ return 8; } }
		public override int InitMinHits{ get{ return 26; } }
		public override int InitMaxHits{ get{ return 26; } }

		public override string DefaultName
		{
			get { return "a bloody mask of orcish kin"; }
		}

		[Constructable]
		public BloodyOrcishKinMask() : this( 0x8A4 )
		{
		}

		[Constructable]
		public BloodyOrcishKinMask( int hue ) : base( 0x141B, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 2.0;
		}

		public override bool CanEquip( Mobile m )
		{
			if ( !base.CanEquip( m ) )
				return false;

			if ( m.BodyMod == 183 || m.BodyMod == 184 )
			{
				m.SendLocalizedMessage( 1061629 ); // You can't do that while wearing savage kin paint.
				return false;
			}

			return true;
		}

		public override void OnAdded( object parent )
		{
			base.OnAdded( parent );

			if ( parent is Mobile )
				Misc.Titles.AwardKarma( (Mobile)parent, -20, true );
		}

		public BloodyOrcishKinMask( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	public class BloodyMagicWizardsHat : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }
		public override int InitMinHits{ get{ return 29; } }
		public override int InitMaxHits{ get{ return 29; } }
		public override int BaseIntBonus{ get{ return +2; } }

		[Constructable]
		public BloodyMagicWizardsHat() : this( 0 )
		{
		}

		[Constructable]
		public BloodyMagicWizardsHat( int hue ) : base( 0x1718, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 1.0;
		}

		public BloodyMagicWizardsHat( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	public class BloodyBonnet : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }
		public override int InitMinHits{ get{ return 10; } }
		public override int InitMaxHits{ get{ return 10; } }

		[Constructable]
		public BloodyBonnet() : this( 0 )
		{
		}

		[Constructable]
		public BloodyBonnet( int hue ) : base( 0x1719, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 1.0;
		}

		public BloodyBonnet( Serial serial ) : base( serial )
		{
		}


		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	public class BloodyFeatheredHat : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }
		public override int InitMinHits{ get{ return 12; } }
		public override int InitMaxHits{ get{ return 12; } }

		[Constructable]
		public BloodyFeatheredHat() : this( 0 )
		{
		}

		[Constructable]
		public BloodyFeatheredHat( int hue ) : base( 0x171A, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 1.0;
		}

		public BloodyFeatheredHat( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	public class BloodyTricorneHat : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }
		public override int InitMinHits{ get{ return 15; } }
		public override int InitMaxHits{ get{ return 15; } }

		[Constructable]
		public BloodyTricorneHat() : this( 0 )
		{
		}

		[Constructable]
		public BloodyTricorneHat( int hue ) : base( 0x171B, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 1.0;
		}

		public BloodyTricorneHat( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	public class BloodyJesterHat : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }
		public override int InitMinHits{ get{ return 16; } }
		public override int InitMaxHits{ get{ return 16; } }

		[Constructable]
		public BloodyJesterHat() : this( 0 )
		{
		}

		[Constructable]
		public BloodyJesterHat( int hue ) : base( 0x171C, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 1.0;
		}

		public BloodyJesterHat( Serial serial ) : base( serial )
		{
		}


		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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
	[Flipable( 0x2307, 0x2308 )]
	public class BloodyFurBoots : BaseShoes
	{
		[Constructable]
		public BloodyFurBoots() : this( 0 )
		{
		}

		[Constructable]
		public BloodyFurBoots( int hue ) : base( 0x2307, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 3.0;
		}

		public BloodyFurBoots( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[FlipableAttribute( 0x170b, 0x170c )]
	public class BloodyBoots : BaseShoes
	{
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		[Constructable]
		public BloodyBoots() : this( 0 )
		{
		}

		[Constructable]
		public BloodyBoots( int hue ) : base( 0x170B, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 3.0;
		}

		public BloodyBoots( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[Flipable]
	public class BloodyThighBoots : BaseShoes, IArcaneEquip
	{
		#region Arcane Impl
		private int m_MaxArcaneCharges, m_CurArcaneCharges;

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxArcaneCharges
		{
			get{ return m_MaxArcaneCharges; }
			set{ m_MaxArcaneCharges = value; InvalidateProperties(); Update(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int CurArcaneCharges
		{
			get{ return m_CurArcaneCharges; }
			set{ m_CurArcaneCharges = value; InvalidateProperties(); Update(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IsArcane
		{
			get{ return ( m_MaxArcaneCharges > 0 && m_CurArcaneCharges >= 0 ); }
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

			if ( IsArcane )
				LabelTo( from, 1061837, String.Format( "{0}\t{1}", m_CurArcaneCharges, m_MaxArcaneCharges ) );
		}

		public void Update()
		{
			if ( IsArcane )
				ItemID = 0x26AF;
			else if ( ItemID == 0x26AF )
				ItemID = 0x1711;

			if ( IsArcane && CurArcaneCharges == 0 )
				Hue = 0;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( IsArcane )
				list.Add( 1061837, "{0}\t{1}", m_CurArcaneCharges, m_MaxArcaneCharges ); // arcane charges: ~1_val~ / ~2_val~
		}

		public void Flip()
		{
			if ( ItemID == 0x1711 )
				ItemID = 0x1712;
			else if ( ItemID == 0x1712 )
				ItemID = 0x1711;
		}
		#endregion

		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		[Constructable]
		public BloodyThighBoots() : this( 0 )
		{
		}

		[Constructable]
		public BloodyThighBoots( int hue ) : base( 0x1711, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 4.0;
		}

		public BloodyThighBoots( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version

			if ( IsArcane )
			{
				writer.Write( true );
				writer.Write( (int) m_CurArcaneCharges );
				writer.Write( (int) m_MaxArcaneCharges );
			}
			else
			{
				writer.Write( false );
			}
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					if ( reader.ReadBool() )
					{
						m_CurArcaneCharges = reader.ReadInt();
						m_MaxArcaneCharges = reader.ReadInt();

						if ( Hue == 2118 )
							Hue = ArcaneGem.DefaultArcaneHue;
					}
					break;
				}
			}
		}
	}

	[FlipableAttribute( 0x170f, 0x1710 )]
	public class BloodyShoes : BaseShoes
	{
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		[Constructable]
		public BloodyShoes() : this( 0 )
		{
		}

		[Constructable]
		public BloodyShoes( int hue ) : base( 0x170F, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 2.0;
		}

		public BloodyShoes( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[FlipableAttribute( 0x170d, 0x170e )]
	public class BloodySandals : BaseShoes
	{
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		[Constructable]
		public BloodySandals() : this( 0 )
		{
		}

		[Constructable]
		public BloodySandals( int hue ) : base( 0x170D, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 1.0;
		}

		public BloodySandals( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[Flipable( 0x2797, 0x27E2 )]
	public class BloodyNinjaTabi : BaseShoes
	{
		[Constructable]
		public BloodyNinjaTabi() : this( 0 )
		{
		}

		[Constructable]
		public BloodyNinjaTabi( int hue ) : base( 0x2797, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 2.0;
		}

		public BloodyNinjaTabi( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[Flipable( 0x2796, 0x27E1 )]
	public class BloodySamuraiTabi : BaseShoes
	{
		[Constructable]
		public BloodySamuraiTabi() : this( 0 )
		{
		}

		[Constructable]
		public BloodySamuraiTabi( int hue ) : base( 0x2796, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 2.0;
		}

		public BloodySamuraiTabi( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[Flipable( 0x2796, 0x27E1 )]
	public class BloodyWaraji : BaseShoes
	{
		[Constructable]
		public BloodyWaraji() : this( 0 )
		{
		}

		[Constructable]
		public BloodyWaraji( int hue ) : base( 0x2796, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 2.0;
		}

		public BloodyWaraji( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[FlipableAttribute( 0x2FC4, 0x317A )]
	public class BloodyElvenBoots : BaseShoes
	{
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }
		public override Race RequiredRace { get { return Race.Elf; } }

		[Constructable]
		public BloodyElvenBoots() : this( 0 )
		{
		}

		[Constructable]
		public BloodyElvenBoots( int hue ) : base( 0x2FC4, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 2.0;
		}

		public BloodyElvenBoots( Serial serial ) : base( serial )
		{
		}


		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}


	[FlipableAttribute( 0x152e, 0x152f )]
	public class BloodyShortPants : BasePants
	{
		[Constructable]
		public BloodyShortPants() : this( 0 )
		{
		}

		[Constructable]
		public BloodyShortPants( int hue ) : base( 0x152E, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 2.0;
		}

		public BloodyShortPants( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[FlipableAttribute( 0x1539, 0x153a )]
	public class BloodyLongPants : BasePants
	{
		[Constructable]
		public BloodyLongPants() : this( 0 )
		{
		}

		[Constructable]
		public BloodyLongPants( int hue ) : base( 0x1539, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 2.0;
		}

		public BloodyLongPants( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[Flipable( 0x279B, 0x27E6 )]
	public class BloodyTattsukeHakama : BasePants
	{
		[Constructable]
		public BloodyTattsukeHakama() : this( 0 )
		{
		}

		[Constructable]
		public BloodyTattsukeHakama( int hue ) : base( 0x279B, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 2.0;
		}

		public BloodyTattsukeHakama( Serial serial ) : base( serial )
		{
		}


		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[FlipableAttribute( 0x2FC3, 0x3179 )]
	public class BloodyElvenPants : BasePants
	{
		public override Race RequiredRace { get { return Race.Elf; } }

		[Constructable]
		public BloodyElvenPants() : this( 0 )
		{
		}

		[Constructable]
		public BloodyElvenPants( int hue ) : base( 0x2FC3, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 2.0;
		}

		public BloodyElvenPants( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	[FlipableAttribute( 0x1efd, 0x1efe )]
	public class BloodyFancyShirt : BaseShirt
	{
		[Constructable]
		public BloodyFancyShirt() : this( 0 )
		{
		}

		[Constructable]
		public BloodyFancyShirt( int hue ) : base( 0x1EFD, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 2.0;
		}

		public BloodyFancyShirt( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[FlipableAttribute( 0x1517, 0x1518 )]
	public class BloodyShirt : BaseShirt
	{
		[Constructable]
		public BloodyShirt() : this( 0 )
		{
		}

		[Constructable]
		public BloodyShirt( int hue ) : base( 0x1517, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 1.0;
		}

		public BloodyShirt( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

			if ( Weight == 2.0 )
				Weight = 1.0;
		}
	}

	[Flipable( 0x2794, 0x27DF )]
	public class BloodyClothNinjaJacket : BaseShirt
	{
		[Constructable]
		public BloodyClothNinjaJacket() : this( 0 )
		{
		}

		[Constructable]
		public BloodyClothNinjaJacket( int hue ) : base( 0x2794, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 5.0;
			Layer = Layer.InnerTorso;
		}

		public BloodyClothNinjaJacket( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	public class BloodyElvenShirt : BaseShirt
	{
		public override Race RequiredRace { get { return Race.Elf; } }

		[Constructable]
		public BloodyElvenShirt() : this( 0 )
		{
		}

		[Constructable]
		public BloodyElvenShirt( int hue ) : base( 0x3175, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 2.0;
		}

		public BloodyElvenShirt(Serial serial) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	public class BloodyElvenDarkShirt : BaseShirt
	{
		public override Race RequiredRace { get { return Race.Elf; } }

		[Constructable]
		public BloodyElvenDarkShirt() : this( 0 )
		{
		}

		[Constructable]
		public BloodyElvenDarkShirt( int hue ) : base( 0x3176, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 2.0;
		}

		public BloodyElvenDarkShirt( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	[Flipable( 0x1541, 0x1542 )]
	public class BloodyBodySash : BaseMiddleTorso
	{
		[Constructable]
		public BloodyBodySash() : this( 0 )
		{
		}

		[Constructable]
		public BloodyBodySash( int hue ) : base( 0x1541, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 1.0;
		}

		public BloodyBodySash( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[Flipable( 0x153D, 0x153E )]
	public class BloodyFullApron : BaseMiddleTorso
	{
		[Constructable]
		public BloodyFullApron() : this( 0 )
		{
		}

		[Constructable]
		public BloodyFullApron( int hue ) : base( 0x153D, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 4.0;
		}

		public BloodyFullApron( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[Flipable( 0x1f7B, 0x1f7C )]
	public class BloodyDoublet : BaseMiddleTorso
	{
		[Constructable]
		public BloodyDoublet() : this( 0 )
		{
		}

		[Constructable]
		public BloodyDoublet( int hue ) : base( 0x1F7B, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 2.0;
		}

		public BloodyDoublet( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[Flipable( 0x1ffd, 0x1ffe )]
	public class BloodySurcoat : BaseMiddleTorso
	{
		[Constructable]
		public BloodySurcoat() : this( 0 )
		{
		}

		[Constructable]
		public BloodySurcoat( int hue ) : base( 0x1FFD, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 6.0;
		}

		public BloodySurcoat( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

			if ( Weight == 3.0 )
				Weight = 6.0;
		}
	}

	[Flipable( 0x1FA1, 0x1FA2 )]
	public class BloodyTunic : BaseMiddleTorso
	{
		[Constructable]
		public BloodyTunic() : this( 0 )
		{
		}

		[Constructable]
		public BloodyTunic( int hue ) : base( 0x1FA1, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 5.0;
		}

		public BloodyTunic( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[Flipable( 0x2310, 0x230F )]
	public class BloodyFormalShirt : BaseMiddleTorso
	{
		[Constructable]
		public BloodyFormalShirt() : this( 0 )
		{
		}

		[Constructable]
		public BloodyFormalShirt( int hue ) : base( 0x2310, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 1.0;
		}

		public BloodyFormalShirt( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version

			if ( Weight == 2.0 )
				Weight = 1.0;
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[Flipable( 0x1F9F, 0x1FA0 )]
	public class BloodyJesterSuit : BaseMiddleTorso
	{
		[Constructable]
		public BloodyJesterSuit() : this( 0 )
		{
		}

		[Constructable]
		public BloodyJesterSuit( int hue ) : base( 0x1F9F, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 4.0;
		}

		public BloodyJesterSuit( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[Flipable( 0x27A1, 0x27EC )]
	public class BloodyJinBaori : BaseMiddleTorso
	{
		[Constructable]
		public BloodyJinBaori() : this( 0 )
		{
		}

		[Constructable]
		public BloodyJinBaori( int hue ) : base( 0x27A1, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 3.0;
		}

		public BloodyJinBaori( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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


	[Flipable( 0x230C, 0x230B )]
	public class BloodyFurSarong : BaseOuterLegs
	{
		[Constructable]
		public BloodyFurSarong() : this( 0 )
		{
		}

		[Constructable]
		public BloodyFurSarong( int hue ) : base( 0x230C, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 3.0;
		}

		public BloodyFurSarong( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

			if (  Weight == 4.0 )
				Weight = 3.0;
		}
	}

	[Flipable( 0x1516, 0x1531 )]
	public class BloodySkirt : BaseOuterLegs
	{
		[Constructable]
		public BloodySkirt() : this( 0 )
		{
		}

		[Constructable]
		public BloodySkirt( int hue ) : base( 0x1516, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 4.0;
		}

		public BloodySkirt( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[Flipable( 0x1537, 0x1538 )]
	public class BloodyKilt : BaseOuterLegs
	{
		[Constructable]
		public BloodyKilt() : this( 0 )
		{
		}

		[Constructable]
		public BloodyKilt( int hue ) : base( 0x1537, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 2.0;
		}

		public BloodyKilt( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[Flipable( 0x279A, 0x27E5 )]
	public class BloodyHakama : BaseOuterLegs
	{
		[Constructable]
		public BloodyHakama() : this( 0 )
		{
		}

		[Constructable]
		public BloodyHakama( int hue ) : base( 0x279A, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 2.0;
		}

		public BloodyHakama( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[Flipable( 0x230E, 0x230D )]
	public class BloodyGildedDress : BaseOuterTorso
	{
		[Constructable]
		public BloodyGildedDress() : this( 0 )
		{
		}

		[Constructable]
		public BloodyGildedDress( int hue ) : base( 0x230E, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 3.0;
		}

		public BloodyGildedDress( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[Flipable( 0x1F00, 0x1EFF )]
	public class BloodyFancyDress : BaseOuterTorso
	{
		[Constructable]
		public BloodyFancyDress() : this( 0 )
		{
		}

		[Constructable]
		public BloodyFancyDress( int hue ) : base( 0x1F00, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 3.0;
		}

		public BloodyFancyDress( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[Flipable]
	public class BloodyRobe : BaseOuterTorso, IArcaneEquip
	{
		#region Arcane Impl
		private int m_MaxArcaneCharges, m_CurArcaneCharges;

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxArcaneCharges
		{
			get{ return m_MaxArcaneCharges; }
			set{ m_MaxArcaneCharges = value; InvalidateProperties(); Update(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int CurArcaneCharges
		{
			get{ return m_CurArcaneCharges; }
			set{ m_CurArcaneCharges = value; InvalidateProperties(); Update(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IsArcane
		{
			get{ return ( m_MaxArcaneCharges > 0 && m_CurArcaneCharges >= 0 ); }
		}

		public void Update()
		{
			if ( IsArcane )
				ItemID = 0x26AE;
			else if ( ItemID == 0x26AE )
				ItemID = 0x1F04;

			if ( IsArcane && CurArcaneCharges == 0 )
				Hue = 0;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( IsArcane )
				list.Add( 1061837, "{0}\t{1}", m_CurArcaneCharges, m_MaxArcaneCharges ); // arcane charges: ~1_val~ / ~2_val~
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

			if ( IsArcane )
				LabelTo( from, 1061837, String.Format( "{0}\t{1}", m_CurArcaneCharges, m_MaxArcaneCharges ) );
		}

		public void Flip()
		{
			if ( ItemID == 0x1F03 )
				ItemID = 0x1F04;
			else if ( ItemID == 0x1F04 )
				ItemID = 0x1F03;
		}
		#endregion

		[Constructable]
		public BloodyRobe() : this( 0 )
		{
		}

		[Constructable]
		public BloodyRobe( int hue ) : base( 0x1F03, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 3.0;
		}

		public BloodyRobe( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version

			if ( IsArcane )
			{
				writer.Write( true );
				writer.Write( (int) m_CurArcaneCharges );
				writer.Write( (int) m_MaxArcaneCharges );
			}
			else
			{
				writer.Write( false );
			}
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					if ( reader.ReadBool() )
					{
						m_CurArcaneCharges = reader.ReadInt();
						m_MaxArcaneCharges = reader.ReadInt();

						if ( Hue == 2118 )
							Hue = ArcaneGem.DefaultArcaneHue;
					}
					break;
				}
			}
		}
	}

	public class BloodyMonkRobe : BaseOuterTorso
	{
		[Constructable]
		public BloodyMonkRobe() : this( 0x21E )
		{
		}
		
		[Constructable]
		public BloodyMonkRobe( int hue ) : base( 0x2687, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37; Weight = 1.0; StrRequirement = 0;
		}
		public override bool CanBeBlessed { get { return false; } }
		public BloodyMonkRobe( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[Flipable( 0x2684, 0x2683 )]
	public class BloodyHoodedShroudOfShadows : BaseOuterTorso
	{
		[Constructable]
		public BloodyHoodedShroudOfShadows() : this( 0x455 )
		{
		}

		[Constructable]
		public BloodyHoodedShroudOfShadows( int hue ) : base( 0x2684, hue )
		{
			LootType = LootType.Blessed;
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37; Weight = 3.0;
		}

		public BloodyHoodedShroudOfShadows( Serial serial ) : base( serial )
		{
		}


		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[Flipable( 0x1f01, 0x1f02 )]
	public class BloodyPlainDress : BaseOuterTorso
	{
		[Constructable]
		public BloodyPlainDress() : this( 0 )
		{
		}

		[Constructable]
		public BloodyPlainDress( int hue ) : base( 0x1F01, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 2.0;
		}

		public BloodyPlainDress( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

			if (  Weight == 3.0 )
				Weight = 2.0;
		}
	}

	[Flipable( 0x2799, 0x27E4 )]
	public class BloodyKamishimo : BaseOuterTorso
	{
		[Constructable]
		public BloodyKamishimo() : this( 0 )
		{
		}

		[Constructable]
		public BloodyKamishimo( int hue ) : base( 0x2799, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 3.0;
		}

		public BloodyKamishimo( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[Flipable( 0x279C, 0x27E7 )]
	public class BloodyHakamaShita : BaseOuterTorso
	{
		[Constructable]
		public BloodyHakamaShita() : this( 0 )
		{
		}

		[Constructable]
		public BloodyHakamaShita( int hue ) : base( 0x279C, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 3.0;
		}

		public BloodyHakamaShita( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[Flipable( 0x2782, 0x27CD )]
	public class BloodyMaleKimono : BaseOuterTorso
	{
		[Constructable]
		public BloodyMaleKimono() : this( 0 )
		{
		}

		[Constructable]
		public BloodyMaleKimono( int hue ) : base( 0x2782, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 3.0;
		}

		public override bool AllowFemaleWearer{ get{ return false; } }

		public BloodyMaleKimono( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[Flipable( 0x2783, 0x27CE )]
	public class BloodyFemaleKimono : BaseOuterTorso
	{
		[Constructable]
		public BloodyFemaleKimono() : this( 0 )
		{
		}

		[Constructable]
		public BloodyFemaleKimono( int hue ) : base( 0x2783, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 3.0;
		}

		public override bool AllowMaleWearer{ get{ return false; } }

		public BloodyFemaleKimono( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
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

	[Flipable( 0x2FB9, 0x3173 )]
	public class BloodyMaleElvenRobe : BaseOuterTorso
	{
		public override Race RequiredRace { get { return Race.Elf; } }

		[Constructable]
		public BloodyMaleElvenRobe() : this( 0 )
		{
		}

		[Constructable]
		public BloodyMaleElvenRobe( int hue ) : base( 0x2FB9, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 2.0;
		}

		public BloodyMaleElvenRobe( Serial serial ) : base( serial )
		{
		}


		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	[Flipable( 0x2FBA, 0x3174 )]
	public class BloodyFemaleElvenRobe : BaseOuterTorso
	{
		public override Race RequiredRace { get { return Race.Elf; } }

		[Constructable]
		public BloodyFemaleElvenRobe() : this( 0 )
		{
		}

		[Constructable]
		public BloodyFemaleElvenRobe( int hue ) : base( 0x2FBA, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Blood Stained Clothing"; Hue = 37;    Weight = 2.0;
		}

		public override bool AllowMaleWearer{ get{ return false; } }

		public BloodyFemaleElvenRobe( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 37; from.SendMessage( "You wash the dye from the fabric, but the blood remains." ); } else { Hue = 1; from.SendMessage("The dye turns the fabric black, but the blood will never wash out." );}

			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
}