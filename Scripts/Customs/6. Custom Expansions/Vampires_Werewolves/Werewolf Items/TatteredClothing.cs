using System;

namespace Server.Items
{

	[FlipableAttribute( 0x153b, 0x153c )]
	public class TatteredHalfApron : BaseWaist
	{
		[Constructable]
		public TatteredHalfApron() : this( 0 )
		{
		}

		[Constructable]
		public TatteredHalfApron( int hue ) : base( 0x153b, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 2.0;
		}

		public TatteredHalfApron( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredObi : BaseWaist
	{
		[Constructable]
		public TatteredObi() : this( 0 )
		{
		}

		[Constructable]
		public TatteredObi( int hue ) : base( 0x27A0, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 1.0;
		}

		public TatteredObi( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredWoodlandBelt : BaseWaist
	{
		public override Race RequiredRace { get { return Race.Elf; } }

		[Constructable]
		public TatteredWoodlandBelt() : this( 0 )
		{
		}

		[Constructable]
		public TatteredWoodlandBelt( int hue ) : base( 0x2B68, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 4.0;
		}

		public TatteredWoodlandBelt( Serial serial ) : base( serial )
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

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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

	public class TatteredCloak : BaseCloak
	{
		[Constructable]
		public TatteredCloak() : this( 0 )
		{
		}

		[Constructable]
		public TatteredCloak( int hue ) : base( 0x1515, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 5.0;
		}

		public TatteredCloak( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredKasa : BaseHat
	{
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 30; } }
		public override int InitMaxHits{ get{ return 30; } }

		[Constructable]
		public TatteredKasa() : this( 0 )
		{
		}

		[Constructable]
		public TatteredKasa( int hue ) : base( 0x2798, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 3.0;
		}

		public TatteredKasa( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}
			
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
	public class TatteredClothNinjaHood : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 6; } }
		public override int BasePoisonResistance{ get{ return 9; } }

		public override int InitMinHits{ get{ return 30; } }
		public override int InitMaxHits{ get{ return 30; } }

		[Constructable]
		public TatteredClothNinjaHood() : this( 0 )
		{
		}

		[Constructable]
		public TatteredClothNinjaHood( int hue ) : base( 0x278F, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 2.0;
		}

		public TatteredClothNinjaHood( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredFlowerGarland : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 6; } }
		public override int BaseEnergyResistance{ get{ return 9; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 20; } }

		[Constructable]
		public TatteredFlowerGarland() : this( 0 )
		{
		}

		[Constructable]
		public TatteredFlowerGarland( int hue ) : base( 0x2306, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 1.0;
		}

		public TatteredFlowerGarland( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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

	public class TatteredFloppyHat : BaseHat
	{
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 20; } }

		[Constructable]
		public TatteredFloppyHat() : this( 0 )
		{
		}

		[Constructable]
		public TatteredFloppyHat( int hue ) : base( 0x1713, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 1.0;
		}

		public TatteredFloppyHat( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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

	public class TatteredWideBrimHat : BaseHat
	{
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 20; } }

		[Constructable]
		public TatteredWideBrimHat() : this( 0 )
		{
		}

		[Constructable]
		public TatteredWideBrimHat( int hue ) : base( 0x1714, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 1.0;
		}

		public TatteredWideBrimHat( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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

	public class TatteredCap : BaseHat
	{
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 20; } }

		[Constructable]
		public TatteredCap() : this( 0 )
		{
		}

		[Constructable]
		public TatteredCap( int hue ) : base( 0x1715, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 1.0;
		}

		public TatteredCap( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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

	public class TatteredSkullCap : BaseHat
	{
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 5; } }
		public override int BasePoisonResistance{ get{ return 8; } }

		public override int InitMinHits{ get{ return 14; } }
		public override int InitMaxHits{ get{ return 14; } }

		[Constructable]
		public TatteredSkullCap() : this( 0 )
		{
		}

		[Constructable]
		public TatteredSkullCap( int hue ) : base( 0x1544, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 1.0;
		}

		public TatteredSkullCap( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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

	public class TatteredBandana : BaseHat
	{
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 8; } }

		public override int InitMinHits{ get{ return 12; } }
		public override int InitMaxHits{ get{ return 12; } }

		[Constructable]
		public TatteredBandana() : this( 0 )
		{
		}

		[Constructable]
		public TatteredBandana( int hue ) : base( 0x1540, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 1.0;
		}

		public TatteredBandana( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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

	public class TatteredBearMask : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 5; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 8; } }
		public override int BasePoisonResistance{ get{ return 4; } }

		public override int InitMinHits{ get{ return 27; } }
		public override int InitMaxHits{ get{ return 27; } }

		[Constructable]
		public TatteredBearMask() : this( 0 )
		{
		}

		[Constructable]
		public TatteredBearMask( int hue ) : base( 0x1545, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 5.0;
		}

		public TatteredBearMask( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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

	public class TatteredDeerMask : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 2; } }
		public override int BaseFireResistance{ get{ return 6; } }
		public override int BasePoisonResistance{ get{ return 1; } }
		public override int BaseEnergyResistance{ get{ return 7; } }

		public override int InitMinHits{ get{ return 26; } }
		public override int InitMaxHits{ get{ return 26; } }

		[Constructable]
		public TatteredDeerMask() : this( 0 )
		{
		}

		[Constructable]
		public TatteredDeerMask( int hue ) : base( 0x1547, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 4.0;
		}

		public TatteredDeerMask( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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

	public class TatteredHornedTribalMask : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 6; } }
		public override int BaseFireResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 4; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 28; } }
		public override int InitMaxHits{ get{ return 28; } }

		[Constructable]
		public TatteredHornedTribalMask() : this( 0 )
		{
		}

		[Constructable]
		public TatteredHornedTribalMask( int hue ) : base( 0x1549, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 2.0;
		}

		public TatteredHornedTribalMask( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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

	public class TatteredTribalMask : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 6; } }
		public override int BasePoisonResistance{ get{ return 8; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 24; } }
		public override int InitMaxHits{ get{ return 24; } }

		[Constructable]
		public TatteredTribalMask() : this( 0 )
		{
		}

		[Constructable]
		public TatteredTribalMask( int hue ) : base( 0x154B, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 2.0;
		}


		public TatteredTribalMask( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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

	public class TatteredTallStrawHat : BaseHat
	{
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 14; } }
		public override int InitMaxHits{ get{ return 14; } }

		[Constructable]
		public TatteredTallStrawHat() : this( 0 )
		{
		}

		[Constructable]
		public TatteredTallStrawHat( int hue ) : base( 0x1716, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 1.0;
		}

		public TatteredTallStrawHat( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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

	public class TatteredStrawHat : BaseHat
	{
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 10; } }
		public override int InitMaxHits{ get{ return 10; } }

		[Constructable]
		public TatteredStrawHat() : this( 0 )
		{
		}

		[Constructable]
		public TatteredStrawHat( int hue ) : base( 0x1717, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 1.0;
		}

		public TatteredStrawHat( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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

	public class TatteredOrcishKinMask : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 1; } }
		public override int BaseFireResistance{ get{ return 1; } }
		public override int BaseColdResistance{ get{ return 7; } }
		public override int BaseEnergyResistance{ get{ return 8; } }

		public override int InitMinHits{ get{ return 26; } }
		public override int InitMaxHits{ get{ return 26; } }


		public override string DefaultName
		{
			get { return "a tattered mask of orcish kin"; }
		}

		[Constructable]
		public TatteredOrcishKinMask() : this( 0x8A4 )
		{
		}

		[Constructable]
		public TatteredOrcishKinMask( int hue ) : base( 0x141B, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 2.0;
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

		public TatteredOrcishKinMask( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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

	public class TatteredMagicWizardsHat : BaseHat
	{
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 29; } }
		public override int InitMaxHits{ get{ return 29; } }

		public override int BaseIntBonus{ get{ return +2; } }

		[Constructable]
		public TatteredMagicWizardsHat() : this( 0 )
		{
		}

		[Constructable]
		public TatteredMagicWizardsHat( int hue ) : base( 0x1718, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 1.0;
		}

		public TatteredMagicWizardsHat( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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

	public class TatteredBonnet : BaseHat
	{
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 10; } }
		public override int InitMaxHits{ get{ return 10; } }

		[Constructable]
		public TatteredBonnet() : this( 0 )
		{
		}

		[Constructable]
		public TatteredBonnet( int hue ) : base( 0x1719, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 1.0;
		}

		public TatteredBonnet( Serial serial ) : base( serial )
		{
		}


		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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

	public class TatteredFeatheredHat : BaseHat
	{
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 12; } }
		public override int InitMaxHits{ get{ return 12; } }

		[Constructable]
		public TatteredFeatheredHat() : this( 0 )
		{
		}

		[Constructable]
		public TatteredFeatheredHat( int hue ) : base( 0x171A, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 1.0;
		}

		public TatteredFeatheredHat( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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

	public class TatteredTricorneHat : BaseHat
	{
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 15; } }
		public override int InitMaxHits{ get{ return 15; } }

		[Constructable]
		public TatteredTricorneHat() : this( 0 )
		{
		}

		[Constructable]
		public TatteredTricorneHat( int hue ) : base( 0x171B, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 1.0;
		}

		public TatteredTricorneHat( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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

	public class TatteredJesterHat : BaseHat
	{
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 16; } }
		public override int InitMaxHits{ get{ return 16; } }

		[Constructable]
		public TatteredJesterHat() : this( 0 )
		{
		}

		[Constructable]
		public TatteredJesterHat( int hue ) : base( 0x171C, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 1.0;
		}

		public TatteredJesterHat( Serial serial ) : base( serial )
		{
		}


		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredFurBoots : BaseShoes
	{
		[Constructable]
		public TatteredFurBoots() : this( 0 )
		{
		}

		[Constructable]
		public TatteredFurBoots( int hue ) : base( 0x2307, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 3.0;
		}

		public TatteredFurBoots( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredBoots : BaseShoes
	{
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		[Constructable]
		public TatteredBoots() : this( 0 )
		{
		}

		[Constructable]
		public TatteredBoots( int hue ) : base( 0x170B, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 3.0;
		}

		public TatteredBoots( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredThighBoots : BaseShoes, IArcaneEquip
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
		public TatteredThighBoots() : this( 0 )
		{
		}

		[Constructable]
		public TatteredThighBoots( int hue ) : base( 0x1711, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 4.0;
		}

		public TatteredThighBoots( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredShoes : BaseShoes
	{
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		[Constructable]
		public TatteredShoes() : this( 0 )
		{
		}

		[Constructable]
		public TatteredShoes( int hue ) : base( 0x170F, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 2.0;
		}

		public TatteredShoes( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredSandals : BaseShoes
	{
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		[Constructable]
		public TatteredSandals() : this( 0 )
		{
		}

		[Constructable]
		public TatteredSandals( int hue ) : base( 0x170D, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 1.0;
		}

		public TatteredSandals( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredNinjaTabi : BaseShoes
	{
		[Constructable]
		public TatteredNinjaTabi() : this( 0 )
		{
		}

		[Constructable]
		public TatteredNinjaTabi( int hue ) : base( 0x2797, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 2.0;
		}

		public TatteredNinjaTabi( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredSamuraiTabi : BaseShoes
	{
		[Constructable]
		public TatteredSamuraiTabi() : this( 0 )
		{
		}

		[Constructable]
		public TatteredSamuraiTabi( int hue ) : base( 0x2796, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 2.0;
		}

		public TatteredSamuraiTabi( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredWaraji : BaseShoes
	{
		[Constructable]
		public TatteredWaraji() : this( 0 )
		{
		}

		[Constructable]
		public TatteredWaraji( int hue ) : base( 0x2796, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 2.0;
		}

		public TatteredWaraji( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredElvenBoots : BaseShoes
	{
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override Race RequiredRace { get { return Race.Elf; } }

		[Constructable]
		public TatteredElvenBoots() : this( 0 )
		{
		}

		[Constructable]
		public TatteredElvenBoots( int hue ) : base( 0x2FC4, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 2.0;
		}

		public TatteredElvenBoots( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredShortPants : BasePants
	{
		[Constructable]
		public TatteredShortPants() : this( 0 )
		{
		}

		[Constructable]
		public TatteredShortPants( int hue ) : base( 0x152E, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 2.0;
		}

		public TatteredShortPants( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredLongPants : BasePants
	{
		[Constructable]
		public TatteredLongPants() : this( 0 )
		{
		}

		[Constructable]
		public TatteredLongPants( int hue ) : base( 0x1539, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 2.0;
		}

		public TatteredLongPants( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredTattsukeHakama : BasePants
	{
		[Constructable]
		public TatteredTattsukeHakama() : this( 0 )
		{
		}

		[Constructable]
		public TatteredTattsukeHakama( int hue ) : base( 0x279B, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 2.0;
		}

		public TatteredTattsukeHakama( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredElvenPants : BasePants
	{
		public override Race RequiredRace { get { return Race.Elf; } }

		[Constructable]
		public TatteredElvenPants() : this( 0 )
		{
		}

		[Constructable]
		public TatteredElvenPants( int hue ) : base( 0x2FC3, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 2.0;
		}

		public TatteredElvenPants( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredFancyShirt : BaseShirt
	{
		[Constructable]
		public TatteredFancyShirt() : this( 0 )
		{
		}

		[Constructable]
		public TatteredFancyShirt( int hue ) : base( 0x1EFD, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 2.0;
		}

		public TatteredFancyShirt( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredShirt : BaseShirt
	{
		[Constructable]
		public TatteredShirt() : this( 0 )
		{
		}

		[Constructable]
		public TatteredShirt( int hue ) : base( 0x1517, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 1.0;
		}

		public TatteredShirt( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredClothNinjaJacket : BaseShirt
	{
		[Constructable]
		public TatteredClothNinjaJacket() : this( 0 )
		{
		}

		[Constructable]
		public TatteredClothNinjaJacket( int hue ) : base( 0x2794, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 5.0;
			Layer = Layer.InnerTorso;
		}

		public TatteredClothNinjaJacket( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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

	public class TatteredElvenShirt : BaseShirt
	{
		public override Race RequiredRace { get { return Race.Elf; } }

		[Constructable]
		public TatteredElvenShirt() : this( 0 )
		{
		}

		[Constructable]
		public TatteredElvenShirt( int hue ) : base( 0x3175, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 2.0;
		}

		public TatteredElvenShirt(Serial serial)
			: base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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

	public class TatteredElvenDarkShirt : BaseShirt
	{
		public override Race RequiredRace { get { return Race.Elf; } }
		[Constructable]
		public TatteredElvenDarkShirt() : this( 0 )
		{
		}

		[Constructable]
		public TatteredElvenDarkShirt( int hue ) : base( 0x3176, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 2.0;
		}

		public TatteredElvenDarkShirt( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredBodySash : BaseMiddleTorso
	{
		[Constructable]
		public TatteredBodySash() : this( 0 )
		{
		}

		[Constructable]
		public TatteredBodySash( int hue ) : base( 0x1541, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 1.0;
		}

		public TatteredBodySash( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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

	[Flipable( 0x153d, 0x153e )]
	public class TatteredFullApron : BaseMiddleTorso
	{
		[Constructable]
		public TatteredFullApron() : this( 0 )
		{
		}

		[Constructable]
		public TatteredFullApron( int hue ) : base( 0x153d, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 4.0;
		}

		public TatteredFullApron( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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

	[Flipable( 0x1f7b, 0x1f7c )]
	public class TatteredDoublet : BaseMiddleTorso
	{
		[Constructable]
		public TatteredDoublet() : this( 0 )
		{
		}

		[Constructable]
		public TatteredDoublet( int hue ) : base( 0x1F7B, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 2.0;
		}

		public TatteredDoublet( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredSurcoat : BaseMiddleTorso
	{
		[Constructable]
		public TatteredSurcoat() : this( 0 )
		{
		}

		[Constructable]
		public TatteredSurcoat( int hue ) : base( 0x1FFD, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 6.0;
		}

		public TatteredSurcoat( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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

	[Flipable( 0x1fa1, 0x1fa2 )]
	public class TatteredTunic : BaseMiddleTorso
	{
		[Constructable]
		public TatteredTunic() : this( 0 )
		{
		}

		[Constructable]
		public TatteredTunic( int hue ) : base( 0x1FA1, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 5.0;
		}

		public TatteredTunic( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredFormalShirt : BaseMiddleTorso
	{
		[Constructable]
		public TatteredFormalShirt() : this( 0 )
		{
		}

		[Constructable]
		public TatteredFormalShirt( int hue ) : base( 0x2310, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 1.0;
		}

		public TatteredFormalShirt( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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

	[Flipable( 0x1f9f, 0x1fa0 )]
	public class TatteredJesterSuit : BaseMiddleTorso
	{
		[Constructable]
		public TatteredJesterSuit() : this( 0 )
		{
		}

		[Constructable]
		public TatteredJesterSuit( int hue ) : base( 0x1F9F, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 4.0;
		}

		public TatteredJesterSuit( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredJinBaori : BaseMiddleTorso
	{
		[Constructable]
		public TatteredJinBaori() : this( 0 )
		{
		}

		[Constructable]
		public TatteredJinBaori( int hue ) : base( 0x27A1, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 3.0;
		}

		public TatteredJinBaori( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredFurSarong : BaseOuterLegs
	{
		[Constructable]
		public TatteredFurSarong() : this( 0 )
		{
		}

		[Constructable]
		public TatteredFurSarong( int hue ) : base( 0x230C, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 3.0;
		}

		public TatteredFurSarong( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredSkirt : BaseOuterLegs
	{
		[Constructable]
		public TatteredSkirt() : this( 0 )
		{
		}

		[Constructable]
		public TatteredSkirt( int hue ) : base( 0x1516, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 4.0;
		}

		public TatteredSkirt( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredKilt : BaseOuterLegs
	{
		[Constructable]
		public TatteredKilt() : this( 0 )
		{
		}

		[Constructable]
		public TatteredKilt( int hue ) : base( 0x1537, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 2.0;
		}

		public TatteredKilt( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredHakama : BaseOuterLegs
	{
		[Constructable]
		public TatteredHakama() : this( 0 )
		{
		}

		[Constructable]
		public TatteredHakama( int hue ) : base( 0x279A, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 2.0;
		}

		public TatteredHakama( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredGildedDress : BaseOuterTorso
	{
		[Constructable]
		public TatteredGildedDress() : this( 0 )
		{
		}

		[Constructable]
		public TatteredGildedDress( int hue ) : base( 0x230E, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 3.0;
		}

		public TatteredGildedDress( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredFancyDress : BaseOuterTorso
	{
		[Constructable]
		public TatteredFancyDress() : this( 0 )
		{
		}

		[Constructable]
		public TatteredFancyDress( int hue ) : base( 0x1F00, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 3.0;
		}

		public TatteredFancyDress( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredRobe : BaseOuterTorso, IArcaneEquip
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
		public TatteredRobe() : this( 0 )
		{
		}

		[Constructable]
		public TatteredRobe( int hue ) : base( 0x1F03, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 3.0;
		}

		public TatteredRobe( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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

	public class TatteredMonkRobe : BaseOuterTorso
	{
		[Constructable]
		public TatteredMonkRobe() : this( 0x21E )
		{
		}
		
		[Constructable]
		public TatteredMonkRobe( int hue ) : base( 0x2687, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 1.0;
			StrRequirement = 0;
		}
		public override bool CanBeBlessed { get { return false; } }
		public TatteredMonkRobe( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredHoodedShroudOfShadows : BaseOuterTorso
	{
		[Constructable]
		public TatteredHoodedShroudOfShadows() : this( 0x455 )
		{
		}

		[Constructable]
		public TatteredHoodedShroudOfShadows( int hue ) : base( 0x2684, hue )
		{
			LootType = LootType.Blessed;
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 3.0;
		}

		public TatteredHoodedShroudOfShadows( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredPlainDress : BaseOuterTorso
	{
		[Constructable]
		public TatteredPlainDress() : this( 0 )
		{
		}

		[Constructable]
		public TatteredPlainDress( int hue ) : base( 0x1F01, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 2.0;
		}

		public TatteredPlainDress( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredKamishimo : BaseOuterTorso
	{
		[Constructable]
		public TatteredKamishimo() : this( 0 )
		{
		}

		[Constructable]
		public TatteredKamishimo( int hue ) : base( 0x2799, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 3.0;
		}

		public TatteredKamishimo( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredHakamaShita : BaseOuterTorso
	{
		[Constructable]
		public TatteredHakamaShita() : this( 0 )
		{
		}

		[Constructable]
		public TatteredHakamaShita( int hue ) : base( 0x279C, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 3.0;
		}

		public TatteredHakamaShita( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredMaleKimono : BaseOuterTorso
	{
		[Constructable]
		public TatteredMaleKimono() : this( 0 )
		{
		}

		[Constructable]
		public TatteredMaleKimono( int hue ) : base( 0x2782, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 3.0;
		}

		public override bool AllowFemaleWearer{ get{ return false; } }

		public TatteredMaleKimono( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredFemaleKimono : BaseOuterTorso
	{
		[Constructable]
		public TatteredFemaleKimono() : this( 0 )
		{
		}

		[Constructable]
		public TatteredFemaleKimono( int hue ) : base( 0x2783, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 3.0;
		}

		public override bool AllowMaleWearer{ get{ return false; } }

		public TatteredFemaleKimono( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredMaleElvenRobe : BaseOuterTorso
	{
		public override Race RequiredRace { get { return Race.Elf; } }

		[Constructable]
		public TatteredMaleElvenRobe() : this( 0 )
		{
		}

		[Constructable]
		public TatteredMaleElvenRobe( int hue ) : base( 0x2FB9, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 2.0;
		}

		public TatteredMaleElvenRobe( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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
	public class TatteredFemaleElvenRobe : BaseOuterTorso
	{
		public override Race RequiredRace { get { return Race.Elf; } }
		[Constructable]
		public TatteredFemaleElvenRobe() : this( 0 )
		{
		}

		[Constructable]
		public TatteredFemaleElvenRobe( int hue ) : base( 0x2FBA, hue )
		{
			if (Utility.RandomDouble() < 0.25) { Movable = true; } else { Movable = false; } Name = "Tattered Clothing"; Hue = 1905;    Weight = 2.0;
		}

		public override bool AllowMaleWearer{ get{ return false; } }

		public TatteredFemaleElvenRobe( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			if( sender.DyedHue == 0 ) { Hue = 1905; from.SendMessage( "You attempt to wash the dye from the fabric, but it does no good." ); } else { Hue = 1; from.SendMessage("The dye does nothing to this fabric." );}

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