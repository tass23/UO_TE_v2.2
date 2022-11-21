using System;
using Server;

namespace Server.Items
{
	public class WrathOfTheDryadReplica : BaseStaff
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ConcussionBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }

		public override int AosStrengthReq{ get{ return 20; } }
		public override int AosMinDamage{ get{ return 15; } }
		public override int AosMaxDamage{ get{ return 17; } }
		public override int AosSpeed{ get{ return 33; } }
		public override float MlSpeed{ get{ return 3.25f; } }

		public override int OldStrengthReq{ get{ return 20; } }
		public override int OldMinDamage{ get{ return 10; } }
		public override int OldMaxDamage{ get{ return 30; } }
		public override int OldSpeed{ get{ return 33; } }
		
		public override int InitMinHits{ get{ return 0; } }
		public override int InitMaxHits{ get{ return 0; } }

		[Constructable]
		public WrathOfTheDryadReplica() : base( 0x13F8 )
		{
			Name = "Wrath of The Dryad Replica";
			Hue = 0x29C;
			WeaponAttributes.HitLeechMana = 50;
			WeaponAttributes.HitLightning = 33;
			Attributes.AttackChance = 15;
			Attributes.WeaponDamage = 40;
			Weight = 3.0;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			pois = 100;
			cold = fire = phys = nrgy = chaos = direct = 0;
		}

		public WrathOfTheDryadReplica( Serial serial ) : base( serial )
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