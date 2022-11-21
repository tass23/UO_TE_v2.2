using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.CustomizableVendor;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Diagnostics;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Engines.Harvest;

namespace Server.Items
{
	public abstract class RewardItem : Item
	{
		public int m_Rstock;
		public int m_Rcost;
		public string m_Desc;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int Rstock
		{
			get{ return m_Rstock; }
			set{ m_Rstock = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int Rcost
		{
			get{ return m_Rcost; }
			set{ m_Rcost = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public string Desc
		{
			get{ return m_Desc; }
			set{ m_Desc = value;}
		}
		
		[Constructable]
        public RewardItem(): base(3763)
        {
			Hue = 1;
			Name = "a<BASEFONT COLOR=#E3CC34> [Reward Item]</BASEFONT>";
		}
		
		public RewardItem( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 ); // version
			writer.Write( (int) m_Rstock );
			writer.Write( (int) m_Rcost );			
			writer.Write( (string) m_Desc );
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
				{
					m_Rstock = reader.ReadInt();
					m_Rcost = reader.ReadInt();
					m_Desc = reader.ReadString();
					break;
				}
			}
		}
	}
//___________________________________________________________Mounts______________________________________
	public class EtherealPolarBearDisplay : Item
    {
        [Constructable]
        public EtherealPolarBearDisplay(): base(8417)
        {
            Name = "an Ethereal Polar Bear<BASEFONT COLOR=#E3CC34> [Reward Item]</BASEFONT>";
			//e_Stock = 1;
			//e_Cost = 5;
			Weight = 10.0;
			Hue = 0;
			//e_ItemID = 8417;
			//e_Description = "<BASEFONT COLOR=#E3CC34>Does not require - shrinking, storage or Follower slots to use. {0} in stock."+e_Stock.ToString()+"</BASEFONT>";
        }

        public EtherealPolarBearDisplay(Serial serial): base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class EtherealSkeletalSteedDisplay : Item
    {
        [Constructable]
        public EtherealSkeletalSteedDisplay(): base(9751)
        {
            Name = "an Ethereal Skeletal Steed";
			//e_Stock = 11;
			//e_Cost = 15;
			Weight = 10.0;
			//e_ItemID = 9751;
			//e_Description = "Does not require - shrinking, storage or Follower slots to use.";
        }

        public EtherealSkeletalSteedDisplay(Serial serial): base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class EtherealChimeraDisplay : Item
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "1 Reward Scroll" );
		}
        [Constructable]
        public EtherealChimeraDisplay(): base(11669)
        {
            Name = "Ethereal Chimera Display";
			//list.Add( "1 Reward Scroll" );
			//base.GetProperties( list );
            //ItemID = 11669;
            LootType = LootType.Blessed;
        }

        public EtherealChimeraDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (Name != "Ethereal Chimera (Display Only)")
                Name = "Ethereal Chimera (Display Only)";
        }
    }
	
    public class EtherealChargerOfTheFallenDisplay : Item
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "1 Reward Scroll" );
		}
        [Constructable]
        public EtherealChargerOfTheFallenDisplay(): base(11676)
        {
            Name = "Ethereal Charger Of The Fallen Display";
			//list.Add( "1 Reward Scroll" );
			//base.GetProperties( list );
            //ItemID = 11676;
            LootType = LootType.Blessed;
        }

        public EtherealChargerOfTheFallenDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (Name != "Ethereal Charger Of The Fallen (Display Only)")
                Name = "Ethereal Charger Of The Fallen (Display Only)";
        }
    }
	
	
	public class FlyingCarpetMagicLampDisplay : Item
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "150 Reward Scroll" );
		}
		[Constructable]
		public FlyingCarpetMagicLampDisplay(): base (3840)
		{
			Name = "Flying Magic Carpet Lamp (Display Only)";
			Hue = 249;
		}
				
		public FlyingCarpetMagicLampDisplay( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( 0 );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();			
		}
		
	}
//________________________________________________________________End Mounts_______________________________________

//________________________________________________________Armor_______________________________________________

	public class FemalePlateChestOfEvolutionDisplay: FemalePlateChest
	{
		public override int ArtifactRarity{ get{ return 666; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public FemalePlateChestOfEvolutionDisplay()
		{	Name = "Female Plate Chest Of Evolution (Display Only)";
			//r_Cost = 25;
			Hue = 1175;
            ArmorAttributes.SelfRepair = 10;
        }

        public FemalePlateChestOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

		/*public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            if (r_Cost != 0)
                list.Add(1060658, "Cost: {0} Reward Scrolls", r_Cost);
        }*/

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
	
	public class HeaterShieldOfEvolutionDisplay: HeaterShield 
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "35 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 666; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public HeaterShieldOfEvolutionDisplay()
		{	Name = "Heater Shield Of Evolution (Display Only)";
			Hue = 1175;
            ArmorAttributes.SelfRepair = 10;
        }

        public HeaterShieldOfEvolutionDisplay(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class PlateArmsOfEvolutionDisplay: PlateArms
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "30 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 666; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public PlateArmsOfEvolutionDisplay()
		{	Name = "Plate Arms Of Evolution (Display Only)";
			Hue = 1175;
            ArmorAttributes.SelfRepair = 10;
        }

        public PlateArmsOfEvolutionDisplay(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class PlateChestOfEvolutionDisplay: PlateChest
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "50 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 666; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public PlateChestOfEvolutionDisplay()
		{	Name = "Plate Chest Of Evolution (Display Only)";
			Hue = 1175;
            ArmorAttributes.SelfRepair = 10;
        }

        public PlateChestOfEvolutionDisplay(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class PlateGlovesOfEvolutionDisplay: PlateGloves
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "30 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 666; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public PlateGlovesOfEvolutionDisplay()
		{	Name = "Plate Gloves Of Evolution (Display Only)";
			Hue = 1175;
            ArmorAttributes.SelfRepair = 10;
        }

        public PlateGlovesOfEvolutionDisplay(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class PlateGorgetOfEvolutionDisplay: PlateGorget 
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "25 Reward Scrolls" );
		}

		public override int ArtifactRarity{ get{ return 666; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public PlateGorgetOfEvolutionDisplay()
		{	Name = "Plate Gorget Of Evolution (Display Only)";
			Hue = 1175;
            ArmorAttributes.SelfRepair = 10;
        }

        public PlateGorgetOfEvolutionDisplay(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class PlateHelmOfEvolutionDisplay: PlateHelm 
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "25 Reward Scrolls" );
		}

		public override int ArtifactRarity{ get{ return 666; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public PlateHelmOfEvolutionDisplay()
		{	Name = "Plate Helm Of Evolution (Display Only)";
			Hue = 1175;
            ArmorAttributes.SelfRepair = 10;
        }

        public PlateHelmOfEvolutionDisplay(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class PlateLegsOfEvolutionDisplay: PlateLegs
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "40 Reward Scrolls" );
		}

		public override int ArtifactRarity{ get{ return 666; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public PlateLegsOfEvolutionDisplay()
		{	Name = "Plate Legs Of Evolution (Display Only)";
			Hue = 1175;
            ArmorAttributes.SelfRepair = 10;
        }

        public PlateLegsOfEvolutionDisplay(Serial serial): base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class CloakOfCommandDisplay : Item
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "50 Reward Scrolls" );
		}
        [Constructable]
        public CloakOfCommandDisplay(): base (0x1515)
        {
            Weight = 5.0;
            Name = "Cloak of Command (Display Only)";
            Hue = Utility.RandomMinMax(1150, 1175);
            Layer = Layer.Cloak;
        }

        public CloakOfCommandDisplay(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class BodySashOfCommandDisplay : Item
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "50 Reward Scrolls" );
		}
        [Constructable]
        public BodySashOfCommandDisplay(): base (0x1541)
        {
            Weight = 1.0;
            Name = "Body Sash of Command (Display Only)";
            Hue = Utility.RandomMinMax(1150, 1175);
            Layer = Layer.MiddleTorso;
        }

        public BodySashOfCommandDisplay(Serial serial): base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class AngelArmsDisplay : PlateArms
  {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "205 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 519; } }

		public override int InitMinHits{ get{ return 500; } }
		public override int InitMaxHits{ get{ return 500; } }

		public override int BaseColdResistance{ get{ return 50; } } 
		public override int BaseEnergyResistance{ get{ return 50; } } 
		public override int BasePhysicalResistance{ get{ return 50; } } 
		public override int BasePoisonResistance{ get{ return 50; } } 
		public override int BaseFireResistance{ get{ return 50; } } 
      
      [Constructable]
		public AngelArmsDisplay()
		{
			Weight = 10;
			Name = "Angel's Arms (Display Only)";
			Hue = 1153;
			ArmorAttributes.DurabilityBonus = 20;
			ArmorAttributes.MageArmor = 1;
			ArmorAttributes.SelfRepair = 50;
			Attributes.AttackChance = 25;
			Attributes.BonusDex = 25;
			Attributes.BonusHits = 25;
			Attributes.BonusStr = 75;
			Attributes.BonusMana = 100;
			Attributes.CastRecovery = 2;
			Attributes.CastSpeed = 2;
			Attributes.DefendChance = 30;
			Attributes.LowerManaCost = 11;
			Attributes.LowerRegCost = 20;
			Attributes.Luck = 200;
			Attributes.NightSight = 1;
			Attributes.ReflectPhysical = 20;
			Attributes.RegenHits = 20;
			Attributes.RegenMana = 35;
			Attributes.SpellDamage = 25;
			Attributes.WeaponDamage = 20;
			LootType = LootType.Blessed;
		}

		public AngelArmsDisplay( Serial serial ) : base( serial )
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
	
	public class AngelHandsDisplay : PlateGloves
  {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "200 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 654; } }

		public override int InitMinHits{ get{ return 500; } }
		public override int InitMaxHits{ get{ return 500; } }

		public override int BaseColdResistance{ get{ return 50; } } 
		public override int BaseEnergyResistance{ get{ return 50; } } 
		public override int BasePhysicalResistance{ get{ return 50; } } 
		public override int BasePoisonResistance{ get{ return 50; } } 
		public override int BaseFireResistance{ get{ return 50; } } 
      
      [Constructable]
		public AngelHandsDisplay()
		{
			Weight = 10;
			Name = "Angel's Hands (Display Only)";
			Hue = 1153;
			ArmorAttributes.MageArmor = 1;
			ArmorAttributes.SelfRepair = 50;
			Attributes.AttackChance = 15;
			Attributes.BonusDex = 10;
			Attributes.BonusHits = 25;
			Attributes.BonusMana = 30;
			Attributes.CastRecovery = 2;
			Attributes.CastSpeed = 2;
			Attributes.DefendChance = 30;
			Attributes.LowerManaCost = 11;
			Attributes.LowerRegCost = 20;
			Attributes.Luck = 150;
			Attributes.NightSight = 1;
			Attributes.ReflectPhysical = 20;
			Attributes.RegenHits = 10;
			Attributes.SpellDamage = 15;
			Attributes.WeaponDamage = 20;
			LootType = LootType.Blessed;
		}

		public AngelHandsDisplay( Serial serial ) : base( serial )
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
	
	public class AngelLegsDisplay : PlateLegs
  {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "240 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 567; } }

		public override int InitMinHits{ get{ return 1000; } }
		public override int InitMaxHits{ get{ return 1000; } }

		public override int BaseColdResistance{ get{ return 50; } } 
		public override int BaseEnergyResistance{ get{ return 50; } } 
		public override int BasePhysicalResistance{ get{ return 50; } } 
		public override int BasePoisonResistance{ get{ return 50; } } 
		public override int BaseFireResistance{ get{ return 50; } } 
      
      [Constructable]
		public AngelLegsDisplay()
		{
			Weight = 60;
			Name = "Angel's Legs (Display Only)";
			Hue = 1153;
			ArmorAttributes.DurabilityBonus = 20;
			ArmorAttributes.MageArmor = 1;
			ArmorAttributes.SelfRepair = 50;
			Attributes.AttackChance = 10;
			Attributes.BonusDex = 15;
			Attributes.BonusStam = 10;
			Attributes.CastRecovery = 1;
			Attributes.CastSpeed = 2;
			Attributes.DefendChance = 10;
			Attributes.LowerManaCost = 11;
			Attributes.LowerRegCost = 20;
			Attributes.Luck = 125;
			Attributes.NightSight = 1;
			Attributes.ReflectPhysical = 25;
			Attributes.RegenStam = 15;
			Attributes.SpellDamage = 10;
			Attributes.WeaponDamage = 15;
			LootType = LootType.Blessed;
		}

		public AngelLegsDisplay( Serial serial ) : base( serial )
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
	
    public class AngelTunicDisplay : PlateChest
  {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "240 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 567; } }

		public override int InitMinHits{ get{ return 5000; } }
		public override int InitMaxHits{ get{ return 5000; } }

		public override int BaseColdResistance{ get{ return 50; } } 
		public override int BaseEnergyResistance{ get{ return 50; } } 
		public override int BasePhysicalResistance{ get{ return 50; } } 
		public override int BasePoisonResistance{ get{ return 50; } } 
		public override int BaseFireResistance{ get{ return 50; } } 
      
      [Constructable]
		public AngelTunicDisplay()
		{
			Weight = 10;
			Name = "Angel's Tunic (Display Only)";
			Hue = 1153;
			ArmorAttributes.MageArmor = 1;
			ArmorAttributes.SelfRepair = 50;
			Attributes.AttackChance = 10;
			Attributes.BonusDex = 15;
			Attributes.BonusHits = 20;
			Attributes.BonusInt = 10;
			Attributes.BonusMana = 15;
			Attributes.BonusStam = 25;
			Attributes.CastRecovery = 1;
			Attributes.CastSpeed = 1;
			Attributes.DefendChance = 30;
			Attributes.LowerManaCost = 5;
			Attributes.LowerRegCost = 20;
			Attributes.Luck = 250;
			Attributes.NightSight = 1;
			Attributes.ReflectPhysical = 25;
			Attributes.RegenHits = 15;
			Attributes.RegenMana = 10;
			Attributes.RegenStam = 20;
			Attributes.SpellDamage = 5;
			Attributes.WeaponDamage = 10;
			LootType = LootType.Blessed;
		}

		public AngelTunicDisplay( Serial serial ) : base( serial )
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
	
	public class AngelsCrownDisplay : StandardPlateKabuto
  {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "200 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 547; } }

		public override int InitMinHits{ get{ return 1000; } }
		public override int InitMaxHits{ get{ return 1000; } }

		public override int BaseColdResistance{ get{ return 50; } } 
		public override int BaseEnergyResistance{ get{ return 50; } } 
		public override int BasePhysicalResistance{ get{ return 50; } } 
		public override int BasePoisonResistance{ get{ return 50; } } 
		public override int BaseFireResistance{ get{ return 50; } } 
      
      [Constructable]
		public AngelsCrownDisplay()
		{
			Weight = 60;
			Name = "Angel's Crown (Display Only)";
			Hue = 1153;
			ArmorAttributes.DurabilityBonus = 15;
			ArmorAttributes.MageArmor = 1;
			ArmorAttributes.SelfRepair = 30;
			Attributes.AttackChance = 20;
			Attributes.BonusDex = 15;
			Attributes.BonusHits = 20;
			Attributes.BonusInt = 35;
			Attributes.BonusMana = 40;
			Attributes.BonusStam = 20;
			Attributes.CastRecovery = 3;
			Attributes.CastSpeed = 3;
			Attributes.DefendChance = 15;
			Attributes.LowerManaCost = 11;
			Attributes.LowerRegCost = 20;
			Attributes.Luck = 175;
			Attributes.NightSight = 1;
			Attributes.ReflectPhysical = 15;
			Attributes.SpellDamage = 20;
			Attributes.WeaponDamage = 15;
			LootType = LootType.Blessed;
		}

		public AngelsCrownDisplay( Serial serial ) : base( serial )
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
	
	public class AngelsNeckDisplay : PlateGorget
  {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "200 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 543; } }

		public override int InitMinHits{ get{ return 500; } }
		public override int InitMaxHits{ get{ return 500; } }

		public override int BaseColdResistance{ get{ return 50; } } 
		public override int BaseEnergyResistance{ get{ return 50; } } 
		public override int BasePhysicalResistance{ get{ return 50; } } 
		public override int BasePoisonResistance{ get{ return 50; } } 
		public override int BaseFireResistance{ get{ return 50; } } 
      
      [Constructable]
		public AngelsNeckDisplay()
		{
			Weight = 10;
			Name = "Angel's Neck (Display Only)";
			Hue = 1153;
			ArmorAttributes.MageArmor = 1;
			ArmorAttributes.SelfRepair = 25;
			Attributes.AttackChance = 20;
			Attributes.BonusDex = 15;
			Attributes.BonusHits = 10;
			Attributes.CastRecovery = 1;
			Attributes.CastSpeed = 2;
			Attributes.DefendChance = 20;
			Attributes.LowerManaCost = 11;
			Attributes.LowerRegCost = 20;
			Attributes.Luck = 150;
			Attributes.NightSight = 1;
			Attributes.ReflectPhysical = 20;
			Attributes.RegenHits = 10;
			Attributes.SpellDamage = 5;
			Attributes.WeaponDamage = 15;
			LootType = LootType.Blessed;
		}

		public AngelsNeckDisplay( Serial serial ) : base( serial )
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
	
	public class AngelShieldDisplay : BaseShield
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "210 Reward Scrolls" );
		}
        public override int ArtifactRarity{ get{ return 11; } }
		public override int BasePhysicalResistance{ get{ return 20; } }
		public override int BaseFireResistance{ get{ return 50; } }
		public override int BaseColdResistance{ get{ return 10; } }
		public override int BasePoisonResistance{ get{ return 30; } }
		public override int BaseEnergyResistance{ get{ return 25; } }

		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 155; } }

		public override int AosStrReq{ get{ return 0; } }

		public override int ArmorBase{ get{ return 60; } }

		[Constructable]
		public AngelShieldDisplay() : base( 0x1BC4 )
		{
			if ( !Core.AOS )
				LootType = LootType.Blessed;

			Weight = 10.0;
			Name = "Angel's Shield (Display Only)";
			Hue = 1153;
            Attributes.SpellChanneling = 1;
            ArmorAttributes.LowerStatReq = 20;
            ArmorAttributes.SelfRepair = 4;
            Attributes.DefendChance = 20;
            Attributes.Luck = 150;
		}

		public AngelShieldDisplay( Serial serial ) : base(serial)
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( Weight == 6.0 )
				Weight = 0.0;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}
	}
	
    public class AngelSwordDisplay : ThinLongsword
  {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "220 Reward Scrolls" );
		}
        public override int ArtifactRarity{ get{ return 475; } }
		public override int OldMinDamage{ get{ return 20; } }
		public override int AosMinDamage{ get{ return 20; } }
		public override int OldMaxDamage{ get{ return 30; } }
		public override int AosMaxDamage{ get{ return 30; } }

		public override int InitMinHits{ get{ return 55; } }
		public override int InitMaxHits{ get{ return 255; } }

      [Constructable]
		public AngelSwordDisplay()
		{
			Weight = 10;
			Name = "Angel's Sword (Display Only)";
			Hue = 1153;
      
			WeaponAttributes.HitFireball = 20;
			WeaponAttributes.HitLeechMana = 25;
			WeaponAttributes.HitLeechStam = 20;
			WeaponAttributes.HitLowerAttack = 15;
			WeaponAttributes.LowerStatReq = 20;
			WeaponAttributes.SelfRepair = 5;
			WeaponAttributes.UseBestSkill = 1;
			Attributes.AttackChance = 30;
			Attributes.CastRecovery = 1;
			Attributes.CastSpeed = 1;
			Attributes.Luck = 275;
			Attributes.SpellChanneling = 1;
			Attributes.SpellDamage = 20;
			Attributes.WeaponDamage = 25;
			Attributes.WeaponSpeed = 55;
      
			Slayer = SlayerName.BalronDamnation ;
		}

		public AngelSwordDisplay( Serial serial ) : base( serial )
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
	
	public class MonsterArmsDisplay : LeatherArms
  {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "15 Reward Scrolls" );
		}
		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 200; } }

		public override int BaseColdResistance{ get{ return 12; } } 
		public override int BaseEnergyResistance{ get{ return 20; } } 
		public override int BasePhysicalResistance{ get{ return 12; } } 
		public override int BasePoisonResistance{ get{ return 15; } } 
		public override int BaseFireResistance{ get{ return 15; } } 
      
      [Constructable]
		public MonsterArmsDisplay()
		{
			Weight = 15;
			Name = "Monster Arms (Display Only)";
			Hue = 69;
			ArmorAttributes.DurabilityBonus = 100;
			ArmorAttributes.MageArmor = 1;
			Attributes.AttackChance = 15;
			Attributes.BonusDex = 1;
			Attributes.BonusHits = 5;
			Attributes.BonusInt = 1;
			Attributes.DefendChance = 15;
			Attributes.LowerManaCost = 15;
			Attributes.NightSight = 1;
			Attributes.ReflectPhysical = 15;
			Attributes.RegenHits = 5;
			Attributes.RegenMana = 5;
			Attributes.RegenStam = 5;
		}

		public MonsterArmsDisplay( Serial serial ) : base( serial )
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
	
	public class MonsterChestDisplay : LeatherChest
  {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "20 Reward Scrolls" );
		}
		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 200; } }

		public override int BaseColdResistance{ get{ return 12; } } 
		public override int BaseEnergyResistance{ get{ return 20; } } 
		public override int BasePhysicalResistance{ get{ return 12; } } 
		public override int BasePoisonResistance{ get{ return 15; } } 
		public override int BaseFireResistance{ get{ return 19; } } 
      
      [Constructable]
		public MonsterChestDisplay()
		{
			Weight = 15;
			Name = "Monster Chest (Display Only)";
			Hue = 69;
			ArmorAttributes.DurabilityBonus = 50;
			ArmorAttributes.LowerStatReq = 1;
			ArmorAttributes.MageArmor = 1;
			Attributes.AttackChance = 15;
			Attributes.BonusHits = 3;
			Attributes.DefendChance = 15;
			Attributes.LowerManaCost = 15;
			Attributes.NightSight = 1;
			Attributes.ReflectPhysical = 15;
			Attributes.RegenHits = 5;
			Attributes.RegenMana = 5;
			Attributes.RegenStam = 5;
		}

		public MonsterChestDisplay( Serial serial ) : base( serial )
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
	
	public class MonsterForkDisplay : WarFork
  {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "25 Reward Scrolls" );
		}
		public override int OldMinDamage{ get{ return 20; } }
		public override int AosMinDamage{ get{ return 20; } }
		public override int OldMaxDamage{ get{ return 20; } }
		public override int AosMaxDamage{ get{ return 20; } }
		public override float MlSpeed{ get{ return 2.50f; } }

		public override int InitMinHits{ get{ return 15; } }
		public override int InitMaxHits{ get{ return 15; } }

      [Constructable]
		public MonsterForkDisplay()
		{
			Weight = 15;
			Name = "Monster Fork (Display Only)";
			Hue = 69;
			WeaponAttributes.HitLightning = 50;
			WeaponAttributes.HitLowerAttack = 45;
			WeaponAttributes.HitLowerDefend = 40;
			WeaponAttributes.MageWeapon = 1;
			Attributes.AttackChance = 15;
			Attributes.CastRecovery = 1;
			Attributes.CastSpeed = 1;
			Attributes.DefendChance = 15;
			Attributes.NightSight = 1;
			Attributes.SpellChanneling = 1;
			Attributes.WeaponDamage = 45;
			Attributes.WeaponSpeed = 15;
			Slayer = SlayerName.Terathan ;
		}

		public MonsterForkDisplay( Serial serial ) : base( serial )
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
	
	public class MonsterGlovesDisplay : LeatherGloves
  {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "15 Reward Scrolls" );
		}
		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 200; } }

		public override int BaseColdResistance{ get{ return 11; } } 
		public override int BaseEnergyResistance{ get{ return 20; } } 
		public override int BasePhysicalResistance{ get{ return 12; } } 
		public override int BasePoisonResistance{ get{ return 14; } } 
		public override int BaseFireResistance{ get{ return 13; } } 
      
      [Constructable]
		public MonsterGlovesDisplay()
		{
			Weight = 15;
			Name = "Monster Gloves (Display Only)";
			Hue = 69;
			ArmorAttributes.DurabilityBonus = 50;
			ArmorAttributes.MageArmor = 1;
			Attributes.AttackChance = 15;
			Attributes.BonusDex = 5;
			Attributes.BonusHits = 5;
			Attributes.BonusInt = 5;
			Attributes.DefendChance = 15;
			Attributes.LowerManaCost = 15;
			Attributes.NightSight = 1;
			Attributes.ReflectPhysical = 15;
			Attributes.RegenHits = 5;
			Attributes.RegenMana = 5;
			Attributes.RegenStam = 5;
		}

		public MonsterGlovesDisplay( Serial serial ) : base( serial )
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
	
	public class MonsterLegsDisplay : LeatherSuneate
  {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "20 Reward Scrolls" );
		}
		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 200; } }

		public override int BaseColdResistance{ get{ return 14; } } 
		public override int BaseEnergyResistance{ get{ return 20; } } 
		public override int BasePhysicalResistance{ get{ return 12; } } 
		public override int BasePoisonResistance{ get{ return 15; } } 
		public override int BaseFireResistance{ get{ return 13; } } 
      
      [Constructable]
		public MonsterLegsDisplay()
		{
			Weight = 15;
			Name = "Monster Legs (Display Only)";
			Hue = 69;
			ArmorAttributes.DurabilityBonus = 50;
			ArmorAttributes.MageArmor = 1;
			Attributes.AttackChance = 15;
			Attributes.BonusDex = 5;
			Attributes.BonusHits = 5;
			Attributes.BonusInt = 5;
			Attributes.DefendChance = 15;
			Attributes.LowerManaCost = 15;
			Attributes.NightSight = 1;
			Attributes.ReflectPhysical = 15;
			Attributes.RegenHits = 5;
			Attributes.RegenMana = 5;
			Attributes.RegenStam = 5;
		}

		public MonsterLegsDisplay( Serial serial ) : base( serial )
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
	
	public class MonsterNeckDisplay : LeatherMempo
  {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "15 Reward Scrolls" );
		}
		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 200; } }

		public override int BaseColdResistance{ get{ return 11; } } 
		public override int BaseEnergyResistance{ get{ return 20; } } 
		public override int BasePhysicalResistance{ get{ return 12; } } 
		public override int BasePoisonResistance{ get{ return 9; } } 
		public override int BaseFireResistance{ get{ return 14; } } 
      
      [Constructable]
		public MonsterNeckDisplay()
		{
			Weight = 15;
			Name = "Monster Neck (Display Only)";
			Hue = 69;
			ArmorAttributes.DurabilityBonus = 50;
			ArmorAttributes.MageArmor = 1;
			ArmorAttributes.SelfRepair = 1;
			Attributes.AttackChance = 15;
			Attributes.BonusDex = 5;
			Attributes.BonusHits = 5;
			Attributes.BonusInt = 5;
			Attributes.DefendChance = 15;
			Attributes.LowerManaCost = 15;
			Attributes.NightSight = 1;
			Attributes.ReflectPhysical = 15;
			Attributes.RegenHits = 5;
			Attributes.RegenMana = 5;
			Attributes.RegenStam = 5;
		}

		public MonsterNeckDisplay( Serial serial ) : base( serial )
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
//__________________________________________End Armor___________________________
//__________________________________________Misc________________________________

	public class FlyingCarpetDyeTubDisplay : DyeTub
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "45 Reward Scrolls" );
		}
		[Constructable] 
		public FlyingCarpetDyeTubDisplay() : base()
		{
			Name = "Flying Carpet Dye Tub (Display Only";
		}

		public FlyingCarpetDyeTubDisplay( Serial serial ) : base( serial )
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
	
    public class HouseMailBoxDisplay : Item
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "10 Reward Scrolls" );
		}
        [Constructable]
        public HouseMailBoxDisplay() : base(0x2DF2)
        {
            Weight = 1.0;
            Movable = true;
            Name = "Mailbox (Display Only)";
            Hue = 1157;
        }

        public HouseMailBoxDisplay(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
   	public class PetBondingDeedDisplay : Item 
   	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "15 Reward Scrolls" );
		}
    
      	[Constructable] 
      	public PetBondingDeedDisplay() : base( 0x14F0 ) 
      	{ 
         	Weight = 1.0;  
         	Movable = true;
         	Name="Pet Bonding Deed (Display Only)";   
      	} 

      	public PetBondingDeedDisplay( Serial serial ) : base( serial ) 
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
	
	public class AdventurerKeyDisplay : Item
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "100 Reward Scrolls" );
		}
		
		[Constructable]
		public AdventurerKeyDisplay() : base( 1151 )		//hue 1151
		{
			ItemID = 0x170B;				//crate
			Name = "Adventurer's Boots (Display Only)";
		}

		public AdventurerKeyDisplay( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class BODKeyDisplay : Item
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "100 Reward Scrolls" );
		}
		[Constructable]
		public BODKeyDisplay() : base( 1161 )		//hue 1161 - blaze
		{
			ItemID = 8793;
			Name = "Ultimate BOD Book (Display Only)";
		}

		public BODKeyDisplay( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class PSKeyDisplay : Item
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "100 Reward Scrolls" );
		}
		[Constructable]
		public PSKeyDisplay() : base( 1153 )
		{
			ItemID = 8793;
			Name = "Ultimate Power Scroll Book (Display Only)";
		}

		public PSKeyDisplay( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );	
			int version = reader.ReadInt();
		}
	}
	
	public class WoodKeyDisplay : Item
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "100 Reward Scrolls" );
		}
		[Constructable]
		public WoodKeyDisplay() : base( 88 )		//hue 88
		{
			ItemID = 0x1BD9;			//pile of wood
			Name = "Wood Storage (Display Only)";
		}

		public WoodKeyDisplay( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );	
			int version = reader.ReadInt();
		}
	}
	
	public class ChampSkullKeyDisplay : Item
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "100 Reward Scrolls" );
		}
		[Constructable]
		public ChampSkullKeyDisplay() : base( 1547 )
		{
			ItemID = 0x2203;			//big skull
			Name = "Champion Skull Holder (Display Only)";
		}

		public ChampSkullKeyDisplay( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );	
			int version = reader.ReadInt();
		}
	}
	
	public class TailorKeyDisplay : Item
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "100 Reward Scrolls" );
		}
		[Constructable]
		public TailorKeyDisplay() : base( 68 )
		{
			ItemID = 3997;			//sewingkit
			Name = "Tailor Store (Display Only)";
		}

		public TailorKeyDisplay( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );	
			int version = reader.ReadInt();
		}
	}
	
	public class GardenersKeyDisplay : Item
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "100 Reward Scrolls" );
		}
		[Constructable]
		public GardenersKeyDisplay() : base( 62 )		//hue 62
		{
			ItemID = 0xFB7;				//forged metal
			Name = "Gardener's Trowel (Display Only)";
		}
		
		public GardenersKeyDisplay( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class IngotKeyDisplay : Item
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "100 Reward Scrolls" );
		}
		[Constructable]
		public IngotKeyDisplay() : base( 0x14 )		//hue 0x14
		{
			ItemID = 0x1BE8;			//pile of ingots
			Name = "Ingot Keys (Display Only)";
		}
		
		public IngotKeyDisplay( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class GraniteKeyDisplay : Item
	{
		public override bool ForceShowProperties{ get{ return ObjectPropertyList.Enabled; } }
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "100 Reward Scrolls" );
		}
		[Constructable]
		public GraniteKeyDisplay() : base( 1161 )		//hue 1161
		{
			ItemID = 0x177C;				//rocks
			Weight = 1.0;
			Name = "Stone Storage (Display Only)";
		}
		
		public GraniteKeyDisplay( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );	
			int version = reader.ReadInt();
		}
	}
	
	public class ScribesKeyDisplay : Item
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "100 Reward Scrolls" );
		}
		[Constructable]
		public ScribesKeyDisplay() : base( 0 )		//hue 0
		{
			ItemID = 0xFBE;			//open book
			Name = "Scribe's Tome (Display Only)";
		}
		
		public ScribesKeyDisplay( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class OreKeyDisplay : Item
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "100 Reward Scrolls" );
		}
		[Constructable]
		public OreKeyDisplay() : base( 0x14 )		//hue 0x14
		{
			ItemID = 0x19B9;			//pile of ore
			Name = "Ore Keys (Display Only)";
		}
		
		public OreKeyDisplay( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class PetKeyDisplay : Item
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "100 Reward Scrolls" );
		}
		[Constructable]
		public PetKeyDisplay() : base( 0x1375 )
		{
			Hue = 1180;
			Name = "Pet Key (Display Only)";
			LootType = LootType.Blessed;
		}
		
		//serial constructor
		public PetKeyDisplay( Serial serial ) : base( serial )
		{
		}
		
		//events
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( 0 );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}
	
	public class BankChestAddonDisplay : Item
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "150 Reward Scrolls" );
		}
		[Constructable]
		public BankChestAddonDisplay() : base ( 0xE41 )
		{
			Hue = Utility.RandomYellowHue();
			Name = "Bank Chest (Display Only)";
		}
		
		public BankChestAddonDisplay( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 );  // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}
	
	public class TrainingElementalDeedDisplay : Item
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "45 Reward Scrolls" );
		}
		[Constructable]
		public TrainingElementalDeedDisplay() : base( 0x14F0 )
		{
			Weight = 1.0;
			Name = "Training Elemental Deed (Display Only)";
			Hue = 1877;
		}

		public TrainingElementalDeedDisplay( Serial serial ) : base ( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

    public class BlessedonlyBagDisplay : Item
    {
		public override bool ForceShowProperties{ get{ return ObjectPropertyList.Enabled; } }
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "65 Reward Scrolls" );
		}
        [Constructable]
        public BlessedonlyBagDisplay() : base( 0x9b2 )
        {
                
        	Name = "A Blessed Item Bag (Display Only)";
           	Weight = 0.0;
            Hue = 1170;
            LootType = LootType.Blessed;
            
        }
     	public BlessedonlyBagDisplay(Serial serial) : base(serial)
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

	public class BlessedBagDeedDisplay : Item 
	{
		public override bool ForceShowProperties{ get{ return ObjectPropertyList.Enabled; } }
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "65 Reward Scrolls" );
		}
		[Constructable]
		public BlessedBagDeedDisplay() : this( 1 )
		{
			ItemID = 5360;
			Movable = true;
			Hue = 1152;
			Name = "Blessed Bag Deed (Display Only)";
					
		}

		[Constructable]
		public BlessedBagDeedDisplay( int amount ) 
        {
		}		

		public BlessedBagDeedDisplay( Serial serial ) : base( serial ) 
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
	
	public class RewardPickaxeDisplay : BaseRewardAxe
	{
		public override HarvestSystem HarvestSystem{ get{ return Mining.System; } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.DoubleStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Disarm; } }

		public override int AosStrengthReq{ get{ return 50; } }
		public override int AosMinDamage{ get{ return 13; } }
		public override int AosMaxDamage{ get{ return 15; } }
		public override int AosSpeed{ get{ return 35; } }
		public override float MlSpeed{ get{ return 3.00f; } }

		public override int OldStrengthReq{ get{ return 25; } }
		public override int OldMinDamage{ get{ return 1; } }
		public override int OldMaxDamage{ get{ return 15; } }
		public override int OldSpeed{ get{ return 35; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 60; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Slash1H; } }

		public override bool CanBeWornByGargoyles{ get{ return true; } }
		public override bool ForceShowProperties{ get{ return ObjectPropertyList.Enabled; } }
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "100 Reward Scrolls" );
		}

		[Constructable]
		public RewardPickaxeDisplay() : base( 0xE86 )
		{
			WeaponAttributes.HitHarm = 33;
			WeaponAttributes.HitLowerAttack = 40;
			Attributes.WeaponDamage = 40;
			WeaponAttributes.ResistColdBonus = 10;
			Weight = 11.0;
			UsesRemaining = 50;
			ShowUsesRemaining = false;
			Name = "Reward Pickaxe (Display Only)";
			Hue = 1099;
		}

		public RewardPickaxeDisplay( Serial serial ) : base( serial )
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
			ShowUsesRemaining = true;
		}
	}
	
	public class TrashPackDisplay : Item
	{
		public override bool ForceShowProperties{ get{ return ObjectPropertyList.Enabled; } }
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "125 Reward Scrolls" );
		}
		//public override int MaxWeight{ get{ return 0; } } // A value of 0 signals unlimited weight

		[Constructable]
		public TrashPackDisplay() : base( 0x9B2 )
		{
            Name = "Trash Bag (Display Only)";
			Hue = 1166;
			Movable = true;
		}

		public TrashPackDisplay( Serial serial ) : base( serial )
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
	
//___________________________________________________End Misc______________________________
//___________________________________________________Weapons_______________________________
	public class AssassinSpikeOfEvolutionDisplay : AssassinSpike
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "55 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public AssassinSpikeOfEvolutionDisplay()
		{	Name = "Assassin Spike Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public AssassinSpikeOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class AxeOfEvolutionDisplay : Axe
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "50 Reward Scrolls" );
		}
        public override int ArtifactRarity { get { return 500; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        [Constructable]
        public AxeOfEvolutionDisplay()
        {
            Name = "Axe Of Evolution (Display Only)";
            Hue = 0x4F2;
            WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public AxeOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class BardicheOfEvolutionDisplay : Bardiche
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "45 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public BardicheOfEvolutionDisplay()
		{	Name = "Bardiche Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public BardicheOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class BattleAxeOfEvolutionDisplay : BattleAxe
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "50 Reward Scrolls" );
		}
        public override int ArtifactRarity { get { return 500; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        [Constructable]
        public BattleAxeOfEvolutionDisplay()
        {
            Name = "Battle Axe Of Evolution (Display Only)";
            Hue = 0x4F2;
            WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public BattleAxeOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class BladedStaffOfEvolutionDisplay : BladedStaff
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "50 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public BladedStaffOfEvolutionDisplay()
		{	Name = "Bladed Staff Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public BladedStaffOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class BoneHarvesterOfEvolutionDisplay : BoneHarvester
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "45 Reward Scrolls" );
		}
        public override int ArtifactRarity { get { return 500; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        [Constructable]
        public BoneHarvesterOfEvolutionDisplay()
        {
            Name = "Bone Harvester Of Evolution (Display Only)";
            Hue = 0x4F2;
            WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public BoneHarvesterOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class BowOfEvolutionDisplay : Bow
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "55 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public BowOfEvolutionDisplay()
		{	Name = "Bow Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public BowOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class BroadswordOfEvolutionDisplay : Broadsword
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "50 Reward Scrolls" );
		}
        public override int ArtifactRarity { get { return 500; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        [Constructable]
        public BroadswordOfEvolutionDisplay()
        {
            Name = "Broadsword Of Evolution (Display Only)";
            Hue = 0x4F2;
            WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public BroadswordOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class ButcherKnifeOfEvolutionDisplay : ButcherKnife
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "55 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public ButcherKnifeOfEvolutionDisplay()
		{	Name = "Butcher Knife Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public ButcherKnifeOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class CleaverOfEvolutionDisplay : Cleaver
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "55 Reward Scrolls" );
		}
		public override int ArtifactRarity { get { return 500; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        [Constructable]
        public CleaverOfEvolutionDisplay()
        {
            Name = "Cleaver Of Evolution (Display Only)";
            Hue = 0x4F2;
            WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public CleaverOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class CompositeBowOfEvolutionDisplay : CompositeBow
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "50 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public CompositeBowOfEvolutionDisplay()
		{	Name = "Composite Bow Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public CompositeBowOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class CrescentBladeOfEvolutionDisplay : CrescentBlade
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "55 Reward Scrolls" );
		}
		public override int ArtifactRarity { get { return 500; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        [Constructable]
        public CrescentBladeOfEvolutionDisplay()
        {
            Name = "Crescent Blade Of Evolution (Display Only)";
            Hue = 0x4F2;
            WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public CrescentBladeOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class CrossbowOfEvolutionDisplay : Crossbow
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "50 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public CrossbowOfEvolutionDisplay()
		{	Name = "Crossbow Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public CrossbowOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class CutlassOfEvolutionDisplay : Cutlass
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "55 Reward Scrolls" );
		}
        public override int ArtifactRarity { get { return 500; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        [Constructable]
        public CutlassOfEvolutionDisplay()
        {
            Name = "Cutlass Of Evolution (Display Only)";
            Hue = 0x4F2;
            WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public CutlassOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class DaggerOfEvolutionDisplay : Dagger
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "60 Reward Scrolls" );
		}
        public override int ArtifactRarity { get { return 500; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        [Constructable]
        public DaggerOfEvolutionDisplay()
        {
            Name = "Dagger Of Evolution (Display Only)";
            Hue = 0x4F2;
            WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public DaggerOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class DaishoOfEvolutionDisplay : Daisho
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "55 Reward Scrolls" );
		}
        public override int ArtifactRarity { get { return 500; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        [Constructable]
        public DaishoOfEvolutionDisplay()
        {
            Name = "Daisho Of Evolution (Display Only)";
            Hue = 0x4F2;
            WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public DaishoOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class DoubleAxeOfEvolutionDisplay : DoubleAxe
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "50 Reward Scrolls" );
		}
        public override int ArtifactRarity { get { return 500; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        [Constructable]
        public DoubleAxeOfEvolutionDisplay()
        {
            Name = "Double Axe Of Evolution (Display Only)";
            Hue = 0x4F2;
            WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public DoubleAxeOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class DoubleBladedStaffOfEvolutionDisplay : DoubleBladedStaff
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "50 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public DoubleBladedStaffOfEvolutionDisplay()
		{	Name = "Double Bladed Staff Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public DoubleBladedStaffOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class ElvenCompositeLongbowOfEvolutionDisplay : ElvenCompositeLongbow
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "45 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public ElvenCompositeLongbowOfEvolutionDisplay()
		{	Name = "Elven Composite Longbow Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public ElvenCompositeLongbowOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class ElvenSpellbladeOfEvolutionDisplay : ElvenSpellblade
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "55 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public ElvenSpellbladeOfEvolutionDisplay()
		{	Name = "Elven Spellblade Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public ElvenSpellbladeOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class ExecutionersAxeOfEvolutionDisplay : ExecutionersAxe
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "50 Reward Scrolls" );
		}
        public override int ArtifactRarity { get { return 500; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        [Constructable]
        public ExecutionersAxeOfEvolutionDisplay()
        {
            Name = "Executioners Axe Of Evolution (Display Only)";
            Hue = 0x4F2;
            WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public ExecutionersAxeOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class GnarledStaffOfEvolutionDisplay : GnarledStaff
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "50 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public GnarledStaffOfEvolutionDisplay()
		{	Name = "Gnarled Staff Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public GnarledStaffOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class HalberdOfEvolutionDisplay : Halberd
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "55 Reward Scrolls" );
		}
        public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public HalberdOfEvolutionDisplay()
		{	Name = "Halberd Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public HalberdOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class HammerPickOfEvolutionDisplay : HammerPick
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "60 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public HammerPickOfEvolutionDisplay()
		{	Name = "Hammer Pick Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public HammerPickOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class HatchetOfEvolutionDisplay : Hatchet
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "60 Reward Scrolls" );
		}
        public override int ArtifactRarity { get { return 500; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        [Constructable]
        public HatchetOfEvolutionDisplay()
        {
            Name = "Hatchet Of Evolution (Display Only)";
            Hue = 0x4F2;
            WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public HatchetOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class HeavyCrossbowOfEvolutionDisplay : HeavyCrossbow
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "65 Reward Scrolls" );
		}
        public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public HeavyCrossbowOfEvolutionDisplay()
		{	Name = "Heavy Crossbow Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public HeavyCrossbowOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class KamaOfEvolutionDisplay : Kama
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "60 Reward Scrolls" );
		}
        public override int ArtifactRarity { get { return 500; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        [Constructable]
        public KamaOfEvolutionDisplay()
        {
            Name = "Kama Of Evolution (Display Only)";
            Hue = 0x4F2;
            WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public KamaOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class KatanaOfEvolutionDisplay : Katana
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "55 Reward Scrolls" );
		}
        public override int ArtifactRarity { get { return 500; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        [Constructable]
        public KatanaOfEvolutionDisplay()
        {
            Name = "Katana Of Evolution (Display Only)";
            Hue = 0x4F2;
            WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public KatanaOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class KryssOfEvolutionDisplay : Kryss
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "60 Reward Scrolls" );
		}
        public override int ArtifactRarity { get { return 500; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        [Constructable]
        public KryssOfEvolutionDisplay()
        {
            Name = "Kryss Of Evolution (Display Only)";
            Hue = 0x4F2;
            WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public KryssOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class LajatangOfEvolutionDisplay : Lajatang
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "55 Reward Scrolls" );
		}
        public override int ArtifactRarity { get { return 500; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        [Constructable]
        public LajatangOfEvolutionDisplay()
        {
            Name = "Lajatang Of Evolution (Display Only)";
            Hue = 0x4F2;
            WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public LajatangOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class LanceOfEvolutionDisplay : Lance
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "55 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public LanceOfEvolutionDisplay()
		{	Name = "Lance Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public LanceOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class LargeBattleAxeOfEvolutionDisplay : LargeBattleAxe
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "60 Reward Scrolls" );
		}
        public override int ArtifactRarity { get { return 500; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        [Constructable]
        public LargeBattleAxeOfEvolutionDisplay()
        {
            Name = "Large Battle Axe Of Evolution (Display Only)";
            Hue = 0x4F2;
            WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public LargeBattleAxeOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class LeafbladeOfEvolutionDisplay : Leafblade
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "60 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public LeafbladeOfEvolutionDisplay()
		{	Name = "Leafblade Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public LeafbladeOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class MaceOfEvolutionDisplay : Mace
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "55 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public MaceOfEvolutionDisplay()
		{	Name = "Mace Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public MaceOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class MaulOfEvolutionDisplay : Maul
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "55 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public MaulOfEvolutionDisplay()
		{	Name = "Maul Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public MaulOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class NoDachiOfEvolutionDisplay : NoDachi
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "60 Reward Scrolls" );
		}
        public override int ArtifactRarity { get { return 500; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        [Constructable]
        public NoDachiOfEvolutionDisplay()
        {
            Name = "No Dachi Of Evolution (Display Only)";
            Hue = 0x4F2;
            WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public NoDachiOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class OrnateAxeOfEvolutionDisplay : OrnateAxe
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "60 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public OrnateAxeOfEvolutionDisplay()
		{	Name = "Ornate Axe Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public OrnateAxeOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class PikeOfEvolutionDisplay : Pike
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "55 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public PikeOfEvolutionDisplay()
		{	Name = "Pike Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public PikeOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class PitchforkOfEvolutionDisplay : Pitchfork
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "60 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public PitchforkOfEvolutionDisplay()
		{	Name = "Pitchfork Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public PitchforkOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class QuarterStaffOfEvolutionDisplay : QuarterStaff
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "50 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public QuarterStaffOfEvolutionDisplay()
		{	Name = "Quarter Staff Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public QuarterStaffOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class RepeatingCrossbowOfEvolutionDisplay : RepeatingCrossbow
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "50 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public RepeatingCrossbowOfEvolutionDisplay()
		{	Name = "Repeating Crossbow Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public RepeatingCrossbowOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class RuneBladeOfEvolutionDisplay : RuneBlade
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "60 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public RuneBladeOfEvolutionDisplay()
		{	Name = "Rune Blade Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public RuneBladeOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class SaiOfEvolutionDisplay : Sai
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "50 Reward Scrolls" );
		}
        public override int ArtifactRarity { get { return 500; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        [Constructable]
        public SaiOfEvolutionDisplay()
        {
            Name = "Sai Of Evolution (Display Only)";
            Hue = 0x4F2;
            WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public SaiOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class ScepterOfEvolutionDisplay : Scepter
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "55 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public ScepterOfEvolutionDisplay()
		{	Name = "Scepter Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public ScepterOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class ScimitarOfEvolutionDisplay : Scimitar
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "55 Reward Scrolls" );
		}
        public override int ArtifactRarity { get { return 500; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        [Constructable]
        public ScimitarOfEvolutionDisplay()
        {
            Name = "Scimitar Of Evolution (Display Only)";
            Hue = 0x4F2;
            WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public ScimitarOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class ScytheOfEvolutionDisplay : Scythe
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "55 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public ScytheOfEvolutionDisplay()
		{	Name = "Scythe Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public ScytheOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class ShortbowOfEvolutionDisplay : MagicalShortbow
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "60 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public ShortbowOfEvolutionDisplay()
		{	Name = "Shortbow Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public ShortbowOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class ShortSpearOfEvolutionDisplay : ShortSpear
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "60 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public ShortSpearOfEvolutionDisplay()
		{	Name = "Short Spear Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public ShortSpearOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class SkinningKnifeOfEvolutionDisplay : SkinningKnife
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "50 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public SkinningKnifeOfEvolutionDisplay()
		{	Name = "Skinning Knife Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public SkinningKnifeOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class SpearOfEvolutionDisplay : Spear
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "60 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public SpearOfEvolutionDisplay()
		{	Name = "Spear Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public SpearOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class TekagiOfEvolutionDisplay : Tekagi
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "60 Reward Scrolls" );
		}
        public override int ArtifactRarity { get { return 500; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        [Constructable]
        public TekagiOfEvolutionDisplay()
        {
            Name = "Tekagi Of Evolution (Display Only)";
            Hue = 0x4F2;
            WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public TekagiOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class TessenOfEvolutionDisplay : Tessen
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "60 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public TessenOfEvolutionDisplay()
		{	Name = "Tessen Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public TessenOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class ThinLongswordOfEvolutionDisplay : ThinLongsword
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "55 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public ThinLongswordOfEvolutionDisplay()
		{	Name = "Thin Longsword Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public ThinLongswordOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class TwoHandedAxeOfEvolutionDisplay : TwoHandedAxe
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "60 Reward Scrolls" );
		}
        public override int ArtifactRarity { get { return 500; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        [Constructable]
        public TwoHandedAxeOfEvolutionDisplay()
        {
            Name = "Two Handed Axe Of Evolution (Display Only)";
            Hue = 0x4F2;
            WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public TwoHandedAxeOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class VikingSwordOfEvolutionDisplay : VikingSword
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "55 Reward Scrolls" );
		}
        public override int ArtifactRarity { get { return 500; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        [Constructable]
        public VikingSwordOfEvolutionDisplay()
        {
            Name = "Viking Sword Of Evolution (Display Only)";
            Hue = 0x4F2;
            WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public VikingSwordOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class WakizashiOfEvolutionDisplay : Wakizashi
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "60 Reward Scrolls" );
		}
        public override int ArtifactRarity { get { return 500; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        [Constructable]
        public WakizashiOfEvolutionDisplay()
        {
            Name = "Wakizashi Of Evolution (Display Only)";
            Hue = 0x4F2;
            WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public WakizashiOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class WarAxeOfEvolutionDisplay : WarAxe
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "55 Reward Scrolls" );
		}
        public override int ArtifactRarity { get { return 500; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        [Constructable]
        public WarAxeOfEvolutionDisplay()
        {
            Name = "War Axe Of Evolution (Display Only)";
            Hue = 0x4F2;
            WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public WarAxeOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
    public class WarCleaverOfEvolutionDisplay : WarCleaver
    {
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "60 Reward Scrolls" );
		}
        public override int ArtifactRarity { get { return 500; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        [Constructable]
        public WarCleaverOfEvolutionDisplay()
        {
            Name = "War Cleaver Of Evolution (Display Only)";
            Hue = 0x4F2;
            WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public WarCleaverOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class WarForkOfEvolutionDisplay : WarFork
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "55 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public WarForkOfEvolutionDisplay()
		{	Name = "War Fork Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public WarForkOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class WarHammerOfEvolutionDisplay : WarHammer
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "60 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public WarHammerOfEvolutionDisplay()
		{	Name = "War Hammer Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public WarHammerOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class WarMaceOfEvolutionDisplay : WarMace
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "60 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public WarMaceOfEvolutionDisplay()
		{	Name = "War Mace Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public WarMaceOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
	
	public class YumiOfEvolutionDisplay : Yumi
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "55 Reward Scrolls" );
		}
		public override int ArtifactRarity{ get{ return 500; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public YumiOfEvolutionDisplay()
		{	Name = "Yumi Of Evolution (Display Only)";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;

            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public YumiOfEvolutionDisplay(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
//_____________________________________________________________End Weapons_______________________
}