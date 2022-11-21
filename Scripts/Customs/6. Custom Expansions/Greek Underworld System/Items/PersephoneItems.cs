using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class PersephoneCrown : Helmet
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
		public PersephoneCrown()
		{
			Name = "Persephone's Crown";
			ItemID = 11118;
			Hue = 1719;
			Attributes.BonusInt = 5;
			Attributes.SpellDamage = 10;
			ArmorAttributes.MageArmor = 1;
			ArmorAttributes.SelfRepair = 2;
		}

		public PersephoneCrown( Serial serial ) : base( serial )
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

	public class PersephoneThorn : AssassinSpike
	{
		private DateTime m_NextVanish;
		public override int ArtifactRarity{ get{ return 400; } }
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public PersephoneThorn()
		{
			Name = "Persephone's Thorn";
			Attributes.CastRecovery = 1;
			Attributes.CastSpeed = 1;
			Attributes.SpellChanneling = 1;
			Attributes.SpellDamage = 10;
			WeaponAttributes.HitLeechHits = 50;
			WeaponAttributes.HitLeechStam = 10;
			WeaponAttributes.HitLeechMana = 50;
			WeaponAttributes.SelfRepair = 2;
			Hue = 1175;
		}

		public override void OnHit( Mobile attacker, Mobile defender, double damagebonus )
		{
			if ( attacker.FindItemOnLayer( Layer.Helm ) is PersephoneCrown )
			{
				damagebonus += 1;
				attacker.Hits += 5;
				attacker.Mana += 5;
				attacker.Stam += 5;
				int chance = Utility.Random( 1, 100 );

				if ( chance <= 15 )
				{
					this.OnHit( attacker, defender, damagebonus );
				}
				else if ( chance <= 35 )
					defender.ApplyPoison( attacker, Poison.GetPoison( 4 ));
			}

			base.OnHit( attacker, defender, damagebonus );
		}

		public PersephoneThorn( Serial serial ) : base( serial )
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