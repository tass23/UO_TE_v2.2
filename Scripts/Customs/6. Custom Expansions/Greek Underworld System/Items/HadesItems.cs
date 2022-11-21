using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class HadesCrown : Helmet
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
		public HadesCrown()
		{
			Name = "Crown of Hades";
			ItemID = 9865;
			Hue = 1719;
			Attributes.BonusInt = 8;
			Attributes.BonusHits = 15;
			ArmorAttributes.MageArmor = 1;
			ArmorAttributes.SelfRepair = 2;
		}

		public HadesCrown( Serial serial ) : base( serial )
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

	public class HadesScepter : Scepter
	{
		public override int LabelNumber{ get{ return 1061106; } } // Axe of the Heavens
		public override int ArtifactRarity{ get{ return 500; } }
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public HadesScepter()
		{
			Name = "Scepter of Hades";
			Attributes.CastRecovery = 1;
			Attributes.CastSpeed = 1;
			Attributes.SpellChanneling = 1;
			Attributes.SpellDamage = 20;
			Attributes.AttackChance = 25;
			Attributes.DefendChance = 25;
			Attributes.WeaponSpeed = 25;
			WeaponAttributes.SelfRepair = 2;
			Hue = 1719;
		}

		public override void OnHit( Mobile attacker, Mobile defender, double damagebonus )
		{
			if ( attacker.FindItemOnLayer( Layer.Helm ) is HadesCrown )
			{
				damagebonus += 1.5;
				attacker.Hits += 10;
				attacker.Mana += 10;
				attacker.Stam += 10;
				int chance = Utility.Random( 1, 100 );

				if ( chance <= 25 )
				{
					this.OnHit( attacker, defender, damagebonus );
				}
			}

			base.OnHit( attacker, defender, damagebonus );
		}

		public HadesScepter( Serial serial ) : base( serial )
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