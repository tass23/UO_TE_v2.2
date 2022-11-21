using System;
using Server;

namespace Server.Items
{
	public class MaceAndShieldGlasses : Glasses
	{
		public override int LabelNumber{ get{ return 1073381; } } // Mace and Shield Reading Glasses
	
		public override int BasePhysicalResistance{ get{ return 25; } }
		public override int BaseFireResistance{ get{ return 10; } }
		public override int BaseColdResistance{ get{ return 10; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 10; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public MaceAndShieldGlasses() : base()
		{
			Hue = 0x1DD;
		
			Attributes.BonusStr = 10;
			Attributes.BonusDex = 5;
			
			WeaponAttributes.HitLowerDefend = 30;
		}

		public MaceAndShieldGlasses( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}	
	
	public class GlassesOfTheArts : Glasses
	{
		public override int LabelNumber{ get{ return 1073363; } } // Reading Glasses of the Arts
	
		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 8; } }
		public override int BaseColdResistance{ get{ return 8; } }
		public override int BasePoisonResistance{ get{ return 4; } }
		public override int BaseEnergyResistance{ get{ return 10; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public GlassesOfTheArts() : base()
		{
			Hue = 0x73;
		
			Attributes.BonusInt = 5;
			Attributes.BonusStr = 5;
			Attributes.BonusHits = 15;
		}

		public GlassesOfTheArts( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}	
	
	public class FoldedSteelGlasses : Glasses
	{
		public override int LabelNumber{ get{ return 1073380; } } // Folded Steel Reading Glasses
	
		public override int BasePhysicalResistance{ get{ return 20; } }
		public override int BaseFireResistance{ get{ return 10; } }
		public override int BaseColdResistance{ get{ return 10; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 10; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public FoldedSteelGlasses() : base()
		{
			Hue = 0x47E;
		
			Attributes.BonusStr = 8;
			Attributes.NightSight = 1;
			Attributes.DefendChance = 15;
		}

		public FoldedSteelGlasses( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}	
	
	public class TradesGlasses : Glasses
	{
		public override int LabelNumber{ get{ return 1073362; } } // Reading Glasses of the Trades
	
		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 10; } }
		public override int BaseColdResistance{ get{ return 10; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 10; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public TradesGlasses() : base()
		{
			Attributes.BonusStr = 10;
			Attributes.BonusInt = 10;
		}

		public TradesGlasses( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}	
	
	public class LyricalGlasses : Glasses
	{
		public override int LabelNumber{ get{ return 1073382; } } // Lyrical Reading Glasses
	
		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 10; } }
		public override int BaseColdResistance{ get{ return 10; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 10; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public LyricalGlasses() : base()
		{
			Hue = 0x47F;
		
			Attributes.ReflectPhysical = 15;
			Attributes.NightSight = 1;
			
			WeaponAttributes.HitLowerDefend = 20;
		}

		public LyricalGlasses( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}	
	
	public class AnthropomorphistGlasses : Glasses
	{
		public override int LabelNumber{ get{ return 1073379; } } // Anthropomorphist Reading Glasses
	
		public override int BasePhysicalResistance{ get{ return 5; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 10; } }
		public override int BasePoisonResistance{ get{ return 20; } }
		public override int BaseEnergyResistance{ get{ return 20; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public AnthropomorphistGlasses() : base()
		{
			Hue = 0x80;
		
			Attributes.BonusHits = 5;
			Attributes.RegenMana = 3;
		}

		public AnthropomorphistGlasses( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}	
	
	public class LightOfWayGlasses : Glasses
	{
		public override int LabelNumber{ get{ return 1073378; } } // Light of Way Reading Glasses
	
		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 10; } }
		public override int BaseColdResistance{ get{ return 10; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 10; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public LightOfWayGlasses() : base()
		{
			Hue = 0x256;
		
			Attributes.BonusStr = 7;
			Attributes.BonusInt = 5;
			Attributes.WeaponDamage = 30;
		}

		public LightOfWayGlasses( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}	
	
	public class NecromanticGlasses : Glasses
	{
		public override int LabelNumber{ get{ return 1073377; } } // Necromantic Reading Glasses
	
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 0; } }
		public override int BaseColdResistance{ get{ return 0; } }
		public override int BasePoisonResistance{ get{ return 0; } }
		public override int BaseEnergyResistance{ get{ return 0; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public NecromanticGlasses() : base()
		{
			Hue = 0x22D;
		
			Attributes.LowerRegCost = 30;
			Attributes.LowerManaCost = 12;
		}

		public NecromanticGlasses( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}	
	
	public class WizardsCrystalGlasses : Glasses
	{
		public override int LabelNumber{ get{ return 1073374; } } // Wizard's Crystal Reading Glasses
	
		public override int BasePhysicalResistance{ get{ return 5; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 5; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public WizardsCrystalGlasses() : base()
		{
			Hue = 0x2B0;
		
			Attributes.BonusMana = 10;
			Attributes.RegenMana = 3;
			Attributes.SpellDamage = 15;
		}

		public WizardsCrystalGlasses( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}	
	
	public class MaritimeGlasses : Glasses
	{
		public override int LabelNumber{ get{ return 1073364; } } // Maritime Reading Glasses
	
		public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 4; } }
		public override int BaseColdResistance{ get{ return 30; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 3; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public MaritimeGlasses() : base()
		{
			Hue = 0x581;
		
			Attributes.NightSight = 1;
			Attributes.Luck = 100;
			Attributes.ReflectPhysical = 20;
		}

		public MaritimeGlasses( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}
	
	public class TreasuresAndTrinketsGlasses : Glasses
	{
		public override int LabelNumber{ get{ return 1073373; } } // Treasures and Trinkets Reading Glasses
	
		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 10; } }
		public override int BaseColdResistance{ get{ return 10; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 10; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public TreasuresAndTrinketsGlasses() : base()
		{
			Hue = 0x5A6; // TODO check
		
			Attributes.BonusInt = 10;
			Attributes.BonusHits = 5;
			Attributes.SpellDamage = 10;
		}

		public TreasuresAndTrinketsGlasses( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}
	
	public class PoisonedGlasses : Glasses
	{
		public override int LabelNumber{ get{ return 1073376; } } // Poisoned Reading Glasses
	
		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 10; } }
		public override int BaseColdResistance{ get{ return 10; } }
		public override int BasePoisonResistance{ get{ return 30; } }
		public override int BaseEnergyResistance{ get{ return 10; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public PoisonedGlasses() : base()
		{
			Hue = 0x55C; // TODO check
		
			Attributes.BonusStam = 3;
			Attributes.RegenStam = 4;
		}

		public PoisonedGlasses( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}
}