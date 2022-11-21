using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{

	public class LegacyOfDespair : BaseSword
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.CrushingBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ConcussionBlow; } }

		public override int AosStrengthReq{ get{ return 35; } }
		public override int AosMinDamage{ get{ return 15; } }
		public override int AosMaxDamage{ get{ return 16; } }
		public override int AosSpeed{ get{ return 30; } }
		public override float MlSpeed{ get{ return 3.50f; } }

		public override int OldStrengthReq{ get{ return 25; } }
		public override int OldMinDamage{ get{ return 5; } }
		public override int OldMaxDamage{ get{ return 33; } }
		public override int OldSpeed{ get{ return 35; } }

		public override int DefHitSound{ get{ return 0x237; } }
		public override int DefMissSound{ get{ return 0x23A; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		public override bool CanBeWornByGargoyles{ get{ return true; } }
		public override Race RequiredRace { get { return Race.Gargoyle; } }
		
		[Constructable]
		public LegacyOfDespair() : base( 0x90B )
		{
			Name = ("Legacy Of Despair");
		
			Hue = 5141;
		
			Attributes.WeaponSpeed = 30;
			Attributes.WeaponDamage = 60;
			WeaponAttributes.HitLowerDefend = 50;
			WeaponAttributes.HitLowerAttack = 50;
			WeaponAttributes.HitCurse = 10;		
            AosElementDamages.Cold = 75;
            AosElementDamages.Poison = 25;			
		}

		public LegacyOfDespair( Serial serial ) : base( serial )
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