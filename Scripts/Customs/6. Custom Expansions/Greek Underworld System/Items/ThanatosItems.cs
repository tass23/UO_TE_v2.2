using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class BladeOfThanatos : VikingSword
	{
		public override int ArtifactRarity{ get{ return 100; } }
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public BladeOfThanatos()
		{
			Name = "Consecrated Blade of Thanatos";
			Attributes.WeaponSpeed = 15;
			Attributes.SpellChanneling = 1;
			WeaponAttributes.SelfRepair = 2;
			Hue = 2958;
		}

		public override void OnHit( Mobile attacker, Mobile defender, double damagebonus )
		{
			damagebonus += 0.5;
			int chance = Utility.Random( 1, 100 );

			if ( chance <= 15 )
			{
				int damage = Utility.Random( 20, 30 );
				damage += ((100 -(defender.Hits * 100) / defender.HitsMax) * damage) / 100;
				AOS.Damage( defender, attacker, damage, true, 100, 0, 0, 0, 0 );
				attacker.PlaySound( 6667 ); 
			}

			base.OnHit( attacker, defender, damagebonus );
		}

		public BladeOfThanatos( Serial serial ) : base( serial )
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

	public class ThanatosRobe : HoodedShroudOfShadows
	{
		public override int ArtifactRarity{ get{ return 100; } }

		[Constructable]
		public ThanatosRobe()
		{
			Name = "Shroud of Death";
			Hue = 1779;
			Attributes.RegenHits = 10;
			Resistances.Physical = 20;
			Resistances.Fire = 10;
			Resistances.Cold = 10;
			Resistances.Poison = 10;
			Resistances.Energy = 10;
		}

		public ThanatosRobe( Serial serial ) : base( serial )
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

	public class TalismanOfShadows : GoldBracelet
	{

		public override int ArtifactRarity{ get{ return 100; } }
		private int m_Charges;
		private DateTime m_NextRecharge;
		private DateTime m_NextUse;
		private DateTime m_ActiveUntil;

		[Constructable]
		public TalismanOfShadows() : base()
		{
			Name = "Talisman of Shadows";
			ItemID = 12120;
			Layer = Layer.Talisman;
			Hue = 1175;
			Resistances.Physical = 7;
			Resistances.Cold = 7;
			Resistances.Energy = 7;
			m_NextRecharge = DateTime.Now + TimeSpan.FromHours( 16 );
			m_Charges = 5;
			m_NextUse = DateTime.Now;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( m_NextRecharge < DateTime.Now )
			{
				this.m_Charges = 5;
				m_NextRecharge = DateTime.Now + TimeSpan.FromHours( 16 );
			}

			if ( (this.IsChildOf( from.Backpack ) || from.FindItemOnLayer( Layer.Talisman ) is TalismanOfShadows) && m_Charges > 0 && m_NextUse < DateTime.Now )
			{
				from.SendMessage( 0, "You activate the Talisman's magic. Charges left: {0}", m_Charges - 1 );
				from.SendMessage( 0, "You fade into the shadows." );
				from.Hidden = true;
				from.AllowedStealthSteps = 75;
				from.PlaySound( 515 );
				m_NextUse = DateTime.Now + TimeSpan.FromMinutes( 5.0 );
				m_Charges -= 1;
			}
			else if ( (this.IsChildOf( from.Backpack ) || from.FindItemOnLayer( Layer.Talisman ) is TalismanOfShadows) && this.m_Charges > 0 )
				from.SendMessage( 0, "You must wait for that to cool down. Time left: {0}", m_NextUse - DateTime.Now );
			else if ( this.IsChildOf( from.Backpack ) || from.FindItemOnLayer( Layer.Talisman ) is TalismanOfShadows )
				from.SendMessage( 0, "That is out of charges. Time left to recharge: {0}", m_NextRecharge - DateTime.Now );
			else
				from.SendMessage( 0, "That must be either in your backpack or equipped to use." );
		}

		public TalismanOfShadows( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );

			writer.Write( (int) m_Charges );
			writer.Write( (DateTime) m_NextUse );
			writer.Write( (DateTime) m_NextRecharge );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_Charges = reader.ReadInt();
			m_NextUse = reader.ReadDateTime();
			m_NextRecharge = reader.ReadDateTime();
		}
	}
}