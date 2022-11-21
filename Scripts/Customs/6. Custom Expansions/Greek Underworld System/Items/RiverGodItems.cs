using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class AcheronShirt : Shirt
	{
		public override int ArtifactRarity{ get{ return 100; } }

		[Constructable]
		public AcheronShirt()
		{
			Name = "Acheron's Embrace";
			Hue = 1758;
			Attributes.BonusDex = 8;
		}

		public AcheronShirt( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class StyxRing : GoldRing
	{
		public override int ArtifactRarity{ get{ return 100; } }

		[Constructable]
		public StyxRing()
		{
			Name = "Ring of Styx";
			Hue = 1156;
			Attributes.BonusMana = 15;
			Attributes.LowerRegCost = 10;
			Attributes.CastSpeed = 1;
			Attributes.CastRecovery = 1;
		}

		public StyxRing( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class CocytusBracer : GoldBracelet
	{
		public override int ArtifactRarity{ get{ return 100; } }

		[Constructable]
		public CocytusBracer()
		{
			Name = "Bracer of Cocytus";
			Hue = 1765;
			Attributes.AttackChance = 15;
			Resistances.Physical = 5;
			Resistances.Cold = 5;
			Resistances.Energy = 5;
		}

		public CocytusBracer( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class LetheGauntlets : PlateGloves
	{
		public override int ArtifactRarity{ get{ return 100; } }
		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 10; } }
		public override int BaseColdResistance{ get{ return 10; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 10; } }
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public LetheGauntlets()
		{
			Name = "Gauntlets of Lethe";
			ItemID = 11020;
			Hue = 1157;
			Attributes.BonusStr = 8;
			Attributes.WeaponDamage = 15;
			ArmorAttributes.SelfRepair = 1;
		}

		public LetheGauntlets( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}