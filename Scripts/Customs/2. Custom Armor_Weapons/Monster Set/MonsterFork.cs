//Created with Script Creator By Marak & Rockstar
using System;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class MonsterFork : WarFork
	{
		public override int ArtifactRarity{ get{ return 100; } }
		public override int OldMinDamage{ get{ return 20; } }
		public override int AosMinDamage{ get{ return 20; } }
		public override int OldMaxDamage{ get{ return 20; } }
		public override int AosMaxDamage{ get{ return 20; } }
		public override float MlSpeed{ get{ return 2.50f; } }
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public MonsterFork()
		{
			Weight = 15;
			Name = "Monster Fork";
			Hue = 69;			
			WeaponAttributes.HitLightning = 10;
			WeaponAttributes.MageWeapon = 1;
			Attributes.AttackChance = 5;
			Attributes.SpellChanneling = 1;
			Attributes.WeaponDamage = 5;
			Slayer = SlayerName.Terathan ;
		}

		public MonsterFork( Serial serial ) : base( serial )
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