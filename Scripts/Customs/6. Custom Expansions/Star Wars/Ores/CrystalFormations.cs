using System;
using Server;

namespace Server.Items
{
	public class Blue1Formation : Item
	{
		[Constructable]
		public Blue1Formation() : base( 12262 )
		{
			Name = "Ankarres Sapphire Formation";
			Weight = 255.0;
			Hue = 1095;
		}

		public Blue1Formation( Serial serial ) : base( serial )
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

	public class Blue2Formation : Item 
	{
		[Constructable]
		public Blue2Formation() : base( 12262 )
		{
		
			Hue = 1100;
			Name = "Baas' Wisdom Crystal Formation";
			Weight = 255.0;
					
		}
		
		public Blue2Formation( Serial serial ) : base( serial ) 
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

	public class Blue3Formation : Item 
	{
		[Constructable]
		public Blue3Formation() : base( 12262 )
		{
		
			Hue = 1185;
			Name = "Kenobi's Legacy Crystal Formation";
			Weight = 255.0;
					
		}
		
		public Blue3Formation( Serial serial ) : base( serial ) 
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

	public class Blue4Formation : Item 
	{
		[Constructable]
		public Blue4Formation() : base( 12262 )
		{
		
			Hue = 1366;
			Name = "Krayt Dragon Pearl Formation";
			Weight = 255.0;
					
		}
		
		public Blue4Formation( Serial serial ) : base( serial ) 
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

	public class Blue5Formation : Item 
	{
		[Constructable]
		public Blue5Formation() : base( 12262 )
		{
		
			Hue = 1100;
			Name = "Permafrost Crystal Formation";
			Weight = 255.0;
		}
		
		public Blue5Formation( Serial serial ) : base( serial ) 
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

	public class Blue6Formation : Item 
	{
		[Constructable]
		public Blue6Formation() : base( 12262 )
		{
		
			Hue = 1488;
			Name = "Upari Crystal Formation";
			Weight = 255.0;
		}
		
		public Blue6Formation( Serial serial ) : base( serial ) 
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

// Green Focusing Crystals________________________________________________________________________________

	public class Green1Formation : Item
	{
		[Constructable]
		public Green1Formation() : base( 12262 )
		{
			Name = "Green Adegan Crystal Formation";
			Hue = 1067;
			Weight = 255.0;

		}

		public Green1Formation( Serial serial ) : base( serial ) 
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
	
	public class Green2Formation : Item
	{
		[Constructable]
		public Green2Formation() : base( 12262 )
		{
			Name = "Allya's Redemption Crystal Formation";
			Hue = 1075;
			Weight = 255.0;

		}
		
		public Green2Formation( Serial serial ) : base( serial ) 
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
	
	public class Green3Formation : Item
	{
		[Constructable]
		public Green3Formation() : base( 12262 )
		{
			Name = "Bondara's Folly Crystal Formation";
			Hue = 1162;
			Weight = 255.0;

		}

		public Green3Formation( Serial serial ) : base( serial ) 
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
	
	public class Green4Formation : Item
	{
		[Constructable]
		public Green4Formation() : base( 12262 )
		{
			Name = "Dawn of Dagobah Crystal Formation";
			Hue = 1062;
			Weight = 255.0;

		}

		public Green4Formation( Serial serial ) : base( serial ) 
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
	
	public class Green5Formation : Item
	{
		[Constructable]
		public Green5Formation() : base( 12262 )
		{
			Name = "Sunrider's Destiny Crystal Formation";
			Hue = 1094;
			Weight = 255.0;

		}
		
		public Green5Formation( Serial serial ) : base( serial ) 
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
	
// Yellow Focusing Crystals___________________________________________________________________________________

	public class Yellow1Formation : Item
	{
		[Constructable]
		public Yellow1Formation() : base( 12262 )
		{
			Name = "Yellow Dragite Crystal Formation";
			Hue = 1159;
			Weight = 255.0;

		}
		
		public Yellow1Formation( Serial serial ) : base( serial ) 
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
	
	public class Yellow2Formation : Item
	{
		[Constructable]
		public Yellow2Formation() : base( 12262 )
		{
			Name = "Heart of the Guardian Crystal Formation";
			Hue = 1081;
			Weight = 255.0;

		}
		
		public Yellow2Formation( Serial serial ) : base( serial ) 
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
	
	public class Yellow3Formation : Item
	{
		[Constructable]
		public Yellow3Formation() : base( 12262 )
		{
			Name = "Impact Crystal Formation";
			Hue = 1161;
			Weight = 255.0;

		}
		
		public Yellow3Formation( Serial serial ) : base( serial ) 
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
	
// White Focusing Crystals________________________________________________________________________________

	public class White1Formation : Item
	{
		[Constructable]
		public White1Formation() : base( 12262 )
		{
			Name = "Barab Ore Formation";
			Hue = 2040;
			Weight = 255.0;

		}
		
		public White1Formation( Serial serial ) : base( serial ) 
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
	
	public class White2Formation : Item
	{
		[Constructable]
		public White2Formation() : base( 12262 )
		{
			Name = "Durindfire Crystal Formation";
			Hue = 2962;
			Weight = 255.0;

		}

		public White2Formation( Serial serial ) : base( serial ) 
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
	
	public class White3Formation : Item
	{
		[Constructable]
		public White3Formation() : base( 12262 )
		{
			Name = "Eralam Crystal Formation";
			Hue = 1153;
			Weight = 255.0;

		}
		
		public White3Formation( Serial serial ) : base( serial ) 
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
	
	public class White4Formation : Item
	{
		[Constructable]
		public White4Formation() : base( 12262 )
		{
			Name = "Nextor Crystal Formation";
			Hue = 1361;
			Weight = 255.0;

		}

		public White4Formation( Serial serial ) : base( serial ) 
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
	
	public class White5Formation : Item
	{
		[Constructable]
		public White5Formation() : base( 12262 )
		{
			Name = "Jenruax Crystal Formation";
			Hue = 901;
			Weight = 255.0;

		}

		public White5Formation( Serial serial ) : base( serial ) 
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
	
	public class White6Formation : Item
	{
		[Constructable]
		public White6Formation() : base( 12262 )
		{
			Name = "Rubat Crystal Formation";
			Hue = 781;
			Weight = 255.0;

		}
		
		public White6Formation( Serial serial ) : base( serial ) 
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
	
	public class White7Formation : Item
	{
		[Constructable]
		public White7Formation() : base( 12262 )
		{
			Name = "Sapith Crystal Formation";
			Hue = 2958;
			Weight = 255.0;

		}
		
		public White7Formation( Serial serial ) : base( serial ) 
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
	
	public class White8Formation : Item
	{
		[Constructable]
		public White8Formation() : base( 12262 )
		{
			Name = "Ultima-Pearl Formation";
			Hue = 2958;
			Weight = 255.0;

		}
		
		public White8Formation( Serial serial ) : base( serial ) 
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
	
// Gray Focusing Crystals__________________________________________________________________________________

	public class Gray1Formation : Item
	{
		[Constructable]
		public Gray1Formation() : base( 12262 )
		{
			Name = "Blackwing Crystal Formation";
			Hue = 1175;
			Weight = 255.0;

		}
		
		public Gray1Formation( Serial serial ) : base( serial ) 
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
	
	public class Gray2Formation : Item
	{
		[Constructable]
		public Gray2Formation() : base( 12262 )
		{
			Name = "Lignan Crystal Formation";
			Hue = 840;
			Weight = 255.0;

		}
		
		public Gray2Formation( Serial serial ) : base( serial ) 
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
	
	public class Gray3Formation : Item
	{
		[Constructable]
		public Gray3Formation() : base( 12262 )
		{
			Name = "Stygium Crystal Formation";
			Hue = 2406;
			Weight = 255.0;

		}

		public Gray3Formation( Serial serial ) : base( serial ) 
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
	
// Red Focusing Crystals____________________________________________________________________

	public class Red1Formation : Item
	{
		[Constructable]
		public Red1Formation() : base( 12262 )
		{
			Name = "Bondar Crystal Formation";
			Hue = 2118;
			Weight = 255.0;
		}

		public Red1Formation( Serial serial ) : base( serial ) 
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
	
	public class Red2Formation : Item
	{
		[Constructable]
		public Red2Formation() : base( 12262 )
		{
			Name = "Allya's Exile Crystal Formation";
			Hue = 1172;
			Weight = 255.0;
		}
		
		public Red2Formation( Serial serial ) : base( serial ) 
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
	
	public class Red3Formation : Item
	{
		[Constructable]
		public Red3Formation() : base( 12262 )
		{
			Name = "Cunning of Tyranus Crystal Formation";
			Hue = 1481;
			Weight = 255.0;
		}
		
		public Red3Formation( Serial serial ) : base( serial ) 
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
	
	public class Red4Formation : Item
	{
		[Constructable]
		public Red4Formation() : base( 12262 )
		{
			Name = "Phond Crystal Formation";
			Hue = 233;
			Weight = 255.0;

		}

		public Red4Formation( Serial serial ) : base( serial ) 
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
	
	public class Red5Formation : Item
	{
		[Constructable]
		public Red5Formation() : base( 12262 )
		{
			Name = "Qixoni Crystal Formation";
			Hue = 2118;
			Weight = 255.0;

		}

		public Red5Formation( Serial serial ) : base( serial ) 
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
	
	public class Red6Formation : Item
	{
		[Constructable]
		public Red6Formation() : base( 12262 )
		{
			Name = "Sigil Crystal Formation";
			Hue = 1552;
			Weight = 255.0;

		}

		public Red6Formation( Serial serial ) : base( serial ) 
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
	
	public class Red7Formation : Item
	{
		[Constructable]
		public Red7Formation() : base( 12262 )
		{
			Name = "Synthetic Crystal Formation";
			Hue = 1477;
			Weight = 255.0;

		}

		public Red7Formation( Serial serial ) : base( serial ) 
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
	
// Pink Focusing Crystals_____________________________________________________________________________

	public class Pink1Formation : Item
	{
		[Constructable]
		public Pink1Formation() : base( 12262 )
		{
			Name = "Damind Crystal Formation";
			Hue = 1580;
			Weight = 255.0;

		}
		
		public Pink1Formation( Serial serial ) : base( serial ) 
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
	
	public class Pink2Formation : Item
	{
		[Constructable]
		public Pink2Formation() : base( 12262 )
		{
			Name = "Lorrdian Gemstone Formation";
			Hue = 1168;
            Weight = 255.0;

		}

		public Pink2Formation( Serial serial ) : base( serial ) 
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
	
	public class Pink3Formation : Item
	{
		[Constructable]
		public Pink3Formation() : base( 12262 )
		{
			Name = "Ruusan Crystal Formation";
			Hue = 26;
			Weight = 255.0;

		}

		public Pink3Formation( Serial serial ) : base( serial ) 
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
	
// Purple Focusing Crystals_____________________________________________________________________

	public class Purple1Formation : Item
	{
		[Constructable]
		public Purple1Formation() : base( 12262 )
		{
			Name = "Hurrikaine Crystal Formation";
			Hue = 14;
			Weight = 255.0;

		}
		
		public Purple1Formation( Serial serial ) : base( serial ) 
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
	
	public class Purple2Formation : Item
	{
		[Constructable]
		public Purple2Formation() : base( 12262 )
		{
			Name = "Windu's Guile Crystal Formation";
			Hue = 1277;
			Weight = 255.0;

		}

		public Purple2Formation( Serial serial ) : base( serial ) 
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
	
// Orange Focusing Crystals______________________________________________________________________________

	public class Orange1Formation : Item
	{
		[Constructable]
		public Orange1Formation() : base( 12262 )
		{
			Name = "Lambent Crystal Formation";
			Hue = 2116;
			Weight = 255.0;

		}

		public Orange1Formation( Serial serial ) : base( serial ) 
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

	public class Orange2Formation : Item
	{
		[Constructable]
		public Orange2Formation() : base( 12262 )
		{
			Name = "Lava Crystal Formation";
			Hue = 39;
			Weight = 255.0;

		}
		
		public Orange2Formation( Serial serial ) : base( serial ) 
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
	
	public class Orange3Formation : Item
	{
		[Constructable]
		public Orange3Formation() : base( 12262 )
		{
			Name = "Solari Crystal Formation";
			Hue = 44;
			Weight = 255.0;
		}
		
		public Orange3Formation( Serial serial ) : base( serial ) 
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
	
	public class Orange4Formation : Item
	{
		[Constructable]
		public Orange4Formation() : base( 12262 )
		{
			Name = "Velmorite Crystal Formation";
			Hue = 48;
			Weight = 255.0;
		}
		
		public Orange4Formation( Serial serial ) : base( serial ) 
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
	
// Cyan Focusing Crystals______________________________________________________________________________

	public class Cyan1Formation : Item
	{
		[Constructable]
		public Cyan1Formation() : base( 12262 )
		{
			Name = "Mantle of the Force Crystal Formation";
			Hue = 1366;
			Weight = 255.0;

		}

		public Cyan1Formation( Serial serial ) : base( serial ) 
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
	
	public class Cyan2Formation : Item
	{
		[Constructable]
		public Cyan2Formation() : base( 12262 )
		{
			Name = "Meditation Crystal Formation";
			Hue = 1328;
			Weight = 255.0;

		}

		public Cyan2Formation( Serial serial ) : base( serial ) 
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
	
// Brown Focusing Crystals________________________________________________________________________

	public class Brown1Formation : Item
	{
		[Constructable]
		public Brown1Formation() : base( 12262 )
		{
			Name = "Ulric's Redemption Crystal Formation";
			Hue = 1867;
			Weight = 255.0;
		}
		
		public Brown1Formation( Serial serial ) : base( serial ) 
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
	
	public class Brown2Formation : Item
	{
		[Constructable]
		public Brown2Formation() : base( 12262 )
		{
			Name = "Vexxtal Crystal Formation";
			Hue = 2110;
			Weight = 255.0;
		}
		
		public Brown2Formation( Serial serial ) : base( serial ) 
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