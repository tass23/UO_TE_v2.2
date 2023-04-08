using System;
using Server;
using Server.Items;
using Server.Factions;

namespace Server.Mobiles
{
	[CorpseName( "a balron lord's corpse" )]
	public class BalronLord : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 145.0; } }
		public override double DispelFocus{ get{ return 65.0; } }
		public override Faction FactionAllegiance { get { return Shadowlords.Instance; } }
		public override Ethics.Ethic EthicAllegiance { get { return Ethics.Ethic.Evil; } }

		[Constructable]
		public BalronLord () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Title = "The Balron Lord";
			Name = NameList.RandomName( "daemon" );
			Body = 40;
			BaseSoundID = 357;

			SetStr( 1286, 1485 );
			SetDex( 1277, 1355 );
			SetInt( 1200, 1250 );

			SetHits( 1792, 1911 );

			SetDamage( 13, 20 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 25 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 75, 90 );
			SetResistance( ResistanceType.Fire, 60, 90 );
			SetResistance( ResistanceType.Cold, 50, 70 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 40, 60 );

			SetSkill( SkillName.Anatomy, 35.1, 60.0 );
			SetSkill( SkillName.EvalInt, 91.1, 105.0 );
			SetSkill( SkillName.Magery, 96.5, 105.0 );
			SetSkill( SkillName.Meditation, 35.1, 60.0 );
			SetSkill( SkillName.MagicResist, 100.5, 150.0 );
			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );

			Fame = 6500;
			Karma = -6500;

			VirtualArmor = 95;

			PackItem( new Longsword() );
        }            
        // It only gives the armor upon killing the balron lord - GreyWolf.
        // to remove just remove from the next line til end of random drop  - GreyWolf.
        /*
		public override void OnDeath( Container c )
		{
            // added to drop the armor so it is not too common - GreyWolf.
			base.OnDeath( c );
                // idea is to drop the armor pieces - GreyWolf.
			int drop = Utility.Random(1, 1);
            for (int i = 0; i < drop; i++)
                if (Utility.RandomDouble() < 0.1) // 5% chance of getting this piece - GreyWolf.
                    c.DropItem(new AngelsNeck());
                else if (Utility.RandomDouble() < 0.2) // 10% chance of getting this piece - GreyWolf.
                    c.DropItem(new AngelsCrown());
                else if (Utility.RandomDouble() < 0.3) // 17.5% chance of getting this piece - GreyWolf.
                    c.DropItem(new AngelHands());
                else if (Utility.RandomDouble() < 0.4) // 25% chance of getting this piece - GreyWolf.
                    c.DropItem(new AngelArms());
                else if (Utility.RandomDouble() < 0.5) // 40% chance of getting this piece - GreyWolf.
                    c.DropItem(new AngelLegs());
                else if (Utility.RandomDouble() < 0.6) // 60% chance of getting this piece - GreyWolf.
                    c.DropItem(new AngelTunic());
                else if (Utility.RandomDouble() < 0.75) // 60% chance of getting this piece - GreyWolf.
                    c.DropItem(new AngelSword());
                else if (Utility.RandomDouble() < 1.0) // 60% chance of getting this piece - GreyWolf.
                    c.DropItem(new AngelShield());
            // end of random drop - GreyWolf.
		}
		*/
		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Rich );
            AddLoot( LootPack.Poor);
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 6; } }
		public override int Meat{ get{ return 5; } }

		public BalronLord( Serial serial ) : base( serial )
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