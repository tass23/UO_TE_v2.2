using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2D20, 0x2D2C )]
	public class VampireSacrificialKnife : BaseKnife
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.PsychicAttack; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.BleedAttack; } }
		public override int AosStrengthReq{ get{ return 85; } }
		public override int AosMinDamage{ get{ return 18; } }
		public override int AosMaxDamage{ get{ return 22; } }
		public override int AosSpeed{ get{ return 44; } }
		
		#region Mondain's Legacy
		public override float MlSpeed{ get{ return 2.50f; } }
		#endregion

		public override int OldStrengthReq{ get{ return 55; } }
		public override int OldMinDamage{ get{ return 12; } }
		public override int OldMaxDamage{ get{ return 14; } }
		public override int OldSpeed{ get{ return 44; } }
        public override int DefMissSound { get { return 0x239; } }
        public override SkillName DefSkill { get { return SkillName.Fencing; } }
		public override int InitMinHits{ get{ return 1; } } // TODO
		public override int InitMaxHits{ get{ return 1; } } // TODO

		[Constructable]
		public VampireSacrificialKnife() : base( 0x2D20 )
		{
			Weight = 5.0;
			Hue = 37;
			Name = "a Sacrificial Knife";
			Slayer = SlayerName.Silver;
			Slayer = SlayerName.Repond;
			WeaponAttributes.UseBestSkill = 1;
			WeaponAttributes.HitLeechHits = 100;
			WeaponAttributes.HitLeechMana = 100;
		}

		public VampireSacrificialKnife( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
}