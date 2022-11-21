using System;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
    public class AngelSword : ThinLongsword
	{
        public override int ArtifactRarity{ get{ return 100; } }
		public override int OldMinDamage{ get{ return 20; } }
		public override int AosMinDamage{ get{ return 20; } }
		public override int OldMaxDamage{ get{ return 30; } }
		public override int AosMaxDamage{ get{ return 30; } }
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public AngelSword()
		{
			Weight = 10;
			Name = "Angel's Sword";
			Hue = 1153;
			WeaponAttributes.HitFireball = 10;
			WeaponAttributes.HitLeechStam = 20;
			WeaponAttributes.SelfRepair = 5;
			Attributes.AttackChance = 30;
			Attributes.WeaponDamage = 5;
			Attributes.WeaponSpeed = 5;
			Slayer = SlayerName.BalronDamnation ;
		}

		public AngelSword( Serial serial ) : base( serial )
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