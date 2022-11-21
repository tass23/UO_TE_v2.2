using System;
using System.Collections;
using Server;
using Server.Items;
using System.Collections.Generic;

namespace Server.Mobiles
{
	[CorpseName( "Molar Ham's corpse" )]
	public class MolarHamDecoy : BaseCreature
	{
		
		[Constructable]
		public MolarHamDecoy() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Molar Ham";
			Body = 183;
			BaseSoundID = 0x165;

			SetStr( 500 );
			SetDex( 175 );
			SetInt( 1000 );

			SetHits( 65000 );
			SetMana( 5000 );

			SetDamage( 3, 6 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Fire, 20 );
			SetDamageType( ResistanceType.Cold, 20 );
			SetDamageType( ResistanceType.Poison, 20 );
			SetDamageType( ResistanceType.Energy, 20 );

			SetResistance( ResistanceType.Physical, 50 );
			SetResistance( ResistanceType.Fire, 50 );
			SetResistance( ResistanceType.Cold, 50 );
			SetResistance( ResistanceType.Poison, 50 );
			SetResistance( ResistanceType.Energy, 50 );

			SetSkill( SkillName.DetectHidden, 80.0 );
			SetSkill( SkillName.EvalInt, 50.0 );
			SetSkill( SkillName.Magery, 50.0 );
			SetSkill( SkillName.Necromancy, 50 );
			SetSkill( SkillName.Meditation, 50.0 );
			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 110.0 );
			SetSkill( SkillName.Wrestling, 120.0 );

			Fame = 28000;
			Karma = -28000;
			
			VirtualArmor = 64;
			
			StaffOfTheMagi weapon = new StaffOfTheMagi();
			weapon.Movable = false;
			AddItem( weapon );

			DragonHelm helm = new DragonHelm();
			helm.Movable = false;
			AddItem( helm );

			BoneGloves gloves = new BoneGloves();
			gloves.Movable = false;
			gloves.Hue = 2412;
			AddItem( gloves );
			
			BoneArms arms = new BoneArms();
			arms.Movable = false;
			arms.Hue = 2412;
			AddItem( arms );

			Cloak cloak = new Cloak();
			cloak.Movable = false;
			cloak.Hue = 2412;
			AddItem( cloak );
			
			StuddedLegs legs = new StuddedLegs();
			legs.Movable = false;
			legs.Hue = 2412;
			AddItem( legs );

			ThighBoots boots = new ThighBoots();
			boots.Movable = false;
			boots.Hue = 2412;
			AddItem( boots );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			c.DropItem(new RewardScroll());
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.SuperBoss, 2 );
			AddLoot( LootPack.Parrot, 1 );
		}
		
		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool BardImmune{ get{ return !Core.SE; } }
		public override bool Unprovokable{ get{ return Core.SE; } }
		public override bool AreaPeaceImmune { get { return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override bool ShowFameTitle{ get{ return false; } }

		public override int TreasureMapLevel{ get{ return 6; } }

		public MolarHamDecoy( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}