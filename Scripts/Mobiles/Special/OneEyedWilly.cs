using System;
using Server;
using Server.Items;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Spells.Fifth;
using Server.Engines.CannedEvil;
using System.Collections.Generic;

namespace Server.Mobiles
{
	public class OneEyedWilly : BaseChampion
	{
		private bool m_TrueForm;
		public override ChampionSkullType SkullType{ get{ return ChampionSkullType.Pirate; } }
		
        public override Type[] UniqueArtifacts{ get { return new Type[] {
			typeof( FangOfRactus ) }; } }
			
		public override Type[] SharedArtifacts{ get { return new Type[] {
			typeof( RoyalGuardSurvivalKnife ),
			typeof( CaptainJohnsHat ),
			typeof( TheMostKnowledgePerson ) }; } }
			
        public override Type[] DecorationArtifacts{ get { return new Type[] {
            typeof( MonsterStatuette ) }; } }
			
        public override MonsterStatuetteType[] StatueTypes{ get{ return new MonsterStatuetteType[] {
            MonsterStatuetteType.Ratman }; } }
		
		[Constructable]
        public OneEyedWilly() : base(AIType.AI_Melee )
		{
			Body = 0x191;
			Name ="One-Eyed Willy" ;
			Title ="the pirate" ;
			SetStr( 386, 500 );
			SetDex( 481, 595 );
			SetInt( 861, 975 );

			SetDamage( 25, 43 );

			SetSkill( SkillName.Fencing, 86.0, 107.5 );
			SetSkill( SkillName.Macing, 95.0, 115.5 );
			SetSkill( SkillName.MagicResist, 95.0, 117.5 );
			SetSkill( SkillName.Swords, 85.0, 97.5 );
			SetSkill( SkillName.Tactics, 75.0, 87.5 );
			SetSkill( SkillName.Wrestling, 105.0, 127.5 );

            Fame = 2500;
            Karma = -2500;

			AddItem( new Boots( Utility.RandomNeutralHue() ) );
			AddItem( new FancyShirt());
			AddItem( new Bandana());
			AddItem( new ShortPants( Utility.RandomNeutralHue() ) );
			
			switch ( Utility.Random( 7 ))
			{
				case 0: AddItem( new Longsword() ); break;
				case 1: AddItem( new Cutlass() ); break;
				case 2: AddItem( new Broadsword() ); break;
				case 3: AddItem( new Axe() ); break;
				case 4: AddItem( new Club() ); break;
				case 5: AddItem( new Dagger() ); break;
				case 6: AddItem( new Spear() ); break;
			}

			Utility.AssignRandomHair( this );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 5 );
            if (Utility.Random(100) < 98)
            {

                int level;

                double random = Utility.RandomDouble();

                if (0.30 >= random)
                    level = 110;

                else
                    level = 105;

                switch (Utility.Random(24))
                {
                    case 0: AddToBackpack(new PowerScroll(SkillName.Swords, level)); break;
                    case 1: AddToBackpack(new PowerScroll(SkillName.Fencing, level)); break;
                    case 2: AddToBackpack(new PowerScroll(SkillName.Archery, level)); break;
                    case 3: AddToBackpack(new PowerScroll(SkillName.Parry, level)); break;
                    case 4: AddToBackpack(new PowerScroll(SkillName.Tactics, level)); break;
                    case 5: AddToBackpack(new PowerScroll(SkillName.Anatomy, level)); break;
                    case 6: AddToBackpack(new PowerScroll(SkillName.Healing, level)); break;
                    case 7: AddToBackpack(new PowerScroll(SkillName.Magery, level)); break;
                    case 8: AddToBackpack(new PowerScroll(SkillName.Meditation, level)); break;
                    case 9: AddToBackpack(new PowerScroll(SkillName.EvalInt, level)); break;
                    case 10: AddToBackpack(new PowerScroll(SkillName.MagicResist, level)); break;
                    case 11: AddToBackpack(new PowerScroll(SkillName.Musicianship, level)); break;
                    case 12: AddToBackpack(new PowerScroll(SkillName.Provocation, level)); break;
                    case 13: AddToBackpack(new PowerScroll(SkillName.Discordance, level)); break;
                    case 14: AddToBackpack(new PowerScroll(SkillName.Peacemaking, level)); break;
                    case 15: AddToBackpack(new PowerScroll(SkillName.Chivalry, level)); break;
                    case 16: AddToBackpack(new PowerScroll(SkillName.Focus, level)); break;
                    case 17: AddToBackpack(new PowerScroll(SkillName.Necromancy, level)); break;
                    case 18: AddToBackpack(new PowerScroll(SkillName.Stealing, level)); break;
                    case 19: AddToBackpack(new PowerScroll(SkillName.Stealth, level)); break;
                    case 20: AddToBackpack(new PowerScroll(SkillName.Macing, level)); break;
                    case 21: AddToBackpack(new PowerScroll(SkillName.Wrestling, level)); break;
                    case 22: AddToBackpack(new PowerScroll(SkillName.Lockpicking, level)); break;
                    case 23: AddToBackpack(new PowerScroll(SkillName.Fishing, level)); break;
                }
            }

		}
                
        /*
		private static readonly double[] m_Offsets = new double[]
			{
				Math.Cos( 000.0 / 180.0 * Math.PI ), Math.Sin( 000.0 / 180.0 * Math.PI ),
				Math.Cos( 040.0 / 180.0 * Math.PI ), Math.Sin( 040.0 / 180.0 * Math.PI ),
				Math.Cos( 080.0 / 180.0 * Math.PI ), Math.Sin( 080.0 / 180.0 * Math.PI ),
				Math.Cos( 120.0 / 180.0 * Math.PI ), Math.Sin( 120.0 / 180.0 * Math.PI ),
				Math.Cos( 160.0 / 180.0 * Math.PI ), Math.Sin( 160.0 / 180.0 * Math.PI ),
				Math.Cos( 200.0 / 180.0 * Math.PI ), Math.Sin( 200.0 / 180.0 * Math.PI ),
				Math.Cos( 240.0 / 180.0 * Math.PI ), Math.Sin( 240.0 / 180.0 * Math.PI ),
				Math.Cos( 280.0 / 180.0 * Math.PI ), Math.Sin( 280.0 / 180.0 * Math.PI ),
				Math.Cos( 320.0 / 180.0 * Math.PI ), Math.Sin( 320.0 / 180.0 * Math.PI ),
			};
		*/
        public void Morph()
        {
            if (m_TrueForm)
                return;

            m_TrueForm = true;

            Name = "Wolfenstein";
            Body = 23;
            Hue = 2312;
            BaseSoundID = 0xE5;

            SetStr(586, 700);
            Hits = HitsMax;
            Stam = StamMax;
            Mana = ManaMax;

            SetDamage(50, 67);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 20, 25);
            SetResistance(ResistanceType.Fire, 10, 20);
            SetResistance(ResistanceType.Cold, 5, 10);
            SetResistance(ResistanceType.Poison, 5, 10);
            SetResistance(ResistanceType.Energy, 10, 15);

            SetSkill(SkillName.MagicResist, 57.6, 75.0);
            SetSkill(SkillName.Tactics, 50.1, 70.0);
            SetSkill(SkillName.Wrestling, 60.1, 80.0);

            Fame = 2500;
            Karma = -2500;

            VirtualArmor = 45;
            ProcessDelta();

            Say("AaRrrrooooooooo!"); 
        }
		[CommandProperty( AccessLevel.GameMaster )]
		public override int HitsMax{ get{ return m_TrueForm ? 6000 : 12000; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public override int ManaMax{ get{ return 5000; } }

       	public OneEyedWilly( Serial serial ) : base( serial )
		{
		}

		public override bool OnBeforeDeath()
		{
			if ( m_TrueForm )
			{
				return base.OnBeforeDeath();
			}
			else
			{
				Morph();
				return false;
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );

			writer.Write( m_TrueForm );			
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_TrueForm = reader.ReadBool();

					break;
				}
			}

        }
	}
}