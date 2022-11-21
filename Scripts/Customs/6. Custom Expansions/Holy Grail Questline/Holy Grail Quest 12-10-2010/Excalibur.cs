using System;
using Server;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x26CE, 0x26CF )]
	public class Excalibur : BaseSword
	{
		public override int ArtifactRarity{ get{ return 100; } }
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.Disarm; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.WhirlwindAttack; } }
		public override int AosStrengthReq{ get{ return 40; } }
		public override int AosMinDamage{ get{ return 12; } }
		public override int AosMaxDamage{ get{ return 14; } }
		public override int AosSpeed{ get{ return 40; } }

		#region Mondain's Legacy
		public override float MlSpeed{ get{ return 2.00f; } }
		#endregion

		public override int OldStrengthReq{ get{ return 40; } }
		public override int OldMinDamage{ get{ return 6; } }
		public override int OldMaxDamage{ get{ return 34; } }
		public override int OldSpeed{ get{ return 30; } }
		public override int DefHitSound{ get{ return 0x237; } }
		public override int DefMissSound{ get{ return 0x23A; } }
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public Excalibur() : base( 0x26CE )
		{
			Weight = 6.0;
            Name = "Excalibur";
            Hue = 1154;

            Attributes.CastSpeed = Utility.RandomMinMax ( 1,3 );
			Attributes.CastRecovery = Utility.RandomMinMax ( 1,3 );
			WeaponAttributes.HitLightning = Utility.RandomMinMax ( 10,30 );
			WeaponAttributes.HitFireball = Utility.RandomMinMax ( 10,30 );
			Attributes.AttackChance = 10;
			Slayer = SlayerName.Repond;
			WeaponAttributes.UseBestSkill = 1;
			Attributes.SpellChanneling = 1;
		}

		#region Mondain's Legacy
		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			pois = fire = cold = chaos = direct = 0;
			nrgy = phys = 50;
		}
		#endregion

		public override bool OnEquip( Mobile from )
		{
			return Validate( from ) && base.OnEquip( from );
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( Validate( Parent as Mobile ) )
				base.OnSingleClick( from );
		}

		public bool Validate( Mobile m )
		{
			if ( m == null || !m.Player )
				return true;
			{
				m.FixedParticles( 0x375A, 10, 30, 5052, EffectLayer.LeftFoot );
				m.PlaySound( 543 );
				m.SendMessage( "You feel the power of Excalibur rush through you as you grasp it within your hand!" );
			}
			return true;
		}

		public Excalibur( Serial serial ) : base( serial )
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