using System;
using Server;

namespace Server.Items
{	
	public class BaronLenshiresCloak : Cloak
	{
		public override int LabelNumber{ get{ return 1073252; } } // Baron Lenshire's Cloak - Museum of Vesper Replica
	
		[Constructable]
		public BaronLenshiresCloak() : this( 0 )
		{
		}

		[Constructable]
		public BaronLenshiresCloak( int hue ) : base( hue )
		{
		}

		public BaronLenshiresCloak( Serial serial ) : base( serial )
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
	
	public class OdricsRobe : Robe
	{
		public override int LabelNumber{ get{ return 1073250; } } // Odric's Robe - Museum of Vesper Replica
	
		[Constructable]
		public OdricsRobe() : this( 0 )
		{
		}

		[Constructable]
		public OdricsRobe( int hue ) : base( hue )
		{
		}

		public OdricsRobe( Serial serial ) : base( serial )
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
	
	public class Adranath : BodySash
	{
		public override int LabelNumber{ get{ return 1073253; } } // Adranath - Museum of Vesper Replica
	
		[Constructable]
		public Adranath() : this( 0 )
		{
		}

		[Constructable]
		public Adranath( int hue ) : base( hue )
		{
		}

		public Adranath( Serial serial ) : base( serial )
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
	
	public class MalabellesDress : Skirt
	{
		public override int LabelNumber{ get{ return 1073251; } } // Malabelle's Dress - Museum of Vesper Replica
	
		[Constructable]
		public MalabellesDress() : this( 0 )
		{
		}

		[Constructable]
		public MalabellesDress( int hue ) : base( hue )
		{
		}

		public MalabellesDress( Serial serial ) : base( serial )
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
	
	public class GypsyHeaddress : SkullCap
	{
		public override int LabelNumber{ get{ return 1073254; } } // Gypsy Headdress - Museum of Vesper Replica
		
		public override int BasePhysicalResistance{ get{ return 15; } }
		public override int BaseFireResistance{ get{ return 20; } }
		public override int BaseColdResistance{ get{ return 20; } }
		public override int BasePoisonResistance{ get{ return 15; } }
		public override int BaseEnergyResistance{ get{ return 15; } }
		
		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 100; } }
	
		[Constructable]
		public GypsyHeaddress() : this( 0 )
		{
		}

		[Constructable]
		public GypsyHeaddress( int hue ) : base( hue )
		{
		}

		public GypsyHeaddress( Serial serial ) : base( serial )
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
	
	public class NystulsWizardsHat : WizardsHat
	{		
		public override int LabelNumber{ get{ return 1073255; } } // Nystul's Wizard's Hat - Museum of Vesper Replica
		
		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 10; } }
		public override int BaseColdResistance{ get{ return 10; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 25; } }
		
		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 100; } }
	
		[Constructable]
		public NystulsWizardsHat() : this( 0 )
		{			
		}

		[Constructable]
		public NystulsWizardsHat( int hue ) : base( hue )
		{
			Attributes.LowerManaCost = 15;
		}

		public NystulsWizardsHat( Serial serial ) : base( serial )
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
	
	public class JesterHatOfChuckles : JesterHat
	{
		public override int LabelNumber{ get{ return 1073256; } } // Jester Hat of Chuckles - Museum of Vesper Replica
		
		public override int BasePhysicalResistance{ get{ return 12; } }
		public override int BaseFireResistance{ get{ return 12; } }
		public override int BaseColdResistance{ get{ return 12; } }
		public override int BasePoisonResistance{ get{ return 12; } }
		public override int BaseEnergyResistance{ get{ return 12; } }
		
		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 100; } }
	
		[Constructable]
		public JesterHatOfChuckles() : this( 0 )
		{			
		}

		[Constructable]
		public JesterHatOfChuckles( int hue ) : base( hue )
		{
			Attributes.Luck = 150;
		}

		public JesterHatOfChuckles( Serial serial ) : base( serial )
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