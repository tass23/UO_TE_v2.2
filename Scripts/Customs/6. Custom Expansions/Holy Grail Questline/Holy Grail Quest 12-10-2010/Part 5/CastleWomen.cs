using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Engines.Quests;

namespace Server.Mobiles
{
	[CorpseName( "corpse of an anthrax 'Doctor'" )]
	public class CastleWomen : BaseCreature
	{
		public override bool InitialInnocent{ get{ return true; } }
		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		private DateTime m_NextPickup = DateTime.Now;
		private DateTime m_Delay = DateTime.Now;
		private DateTime m_Rearm = DateTime.Now;

		[Constructable]
		public CastleWomen() : base( AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			SpeechHue = Utility.RandomDyedHue();
			Hue = Utility.RandomSkinHue();
			Name = NameList.RandomName( "female" );
			Title = "Of the Castle Anthrax";
			Body = 0x191;
			Female = true;

			SetStr( 341, 355 );
			SetDex( 101, 120 );
			SetInt( 111, 120 );
			SetHits( 100, 125 );
			SetDamage( 5, 10 );
			SetResistance( ResistanceType.Physical, 10, 60 );
			SetResistance( ResistanceType.Fire, 10, 60 );
			SetResistance( ResistanceType.Cold, 10, 50 );
			SetResistance( ResistanceType.Poison, 10, 40 );
			SetResistance( ResistanceType.Energy, 10, 60 );
			SetSkill( SkillName.MagicResist, 30.0, 50.0 );
			SetSkill( SkillName.Tactics, 30.0, 50.0 );
			SetSkill( SkillName.Anatomy, 30.0, 50.0 );
			SetSkill( SkillName.Wrestling, 30.0, 50.0 );
			SetSkill( SkillName.Parry, 30.0, 50.0 );
			SetSkill( SkillName.Healing, 30.0, 50.0 );
			SetSkill( SkillName.DetectHidden, 10.0, 20.0 );
			SetSkill( SkillName.Swords, 30.0, 50.0 );
			SetSkill( SkillName.Fencing, 30.0, 50.0 );
			SetSkill( SkillName.Macing, 30.0, 50.0 );
			SetSkill( SkillName.Focus, 30.0, 50.0 );

			AddItem( new Sandals() );

			switch ( Utility.Random( 5 ) )
			{
				default: if ( Utility.RandomBool() ) AddItem( new StuddedBustierArms() );
					else AddItem( new LeatherBustierArms() );
					if ( Utility.RandomBool() ) AddItem( new LeatherSkirt() );
					else AddItem( new LeatherShorts() ); break;
				case 0: AddItem( new FemaleStuddedChest() ); break;
				case 1: AddItem( new FemalePlateChest() ); break;
				case 2: AddItem( new FemaleLeatherChest() ); break;
			}

			if ( Utility.RandomBool() )
			{
				switch ( Utility.Random( 14 ) )
				{
					case 0: AddItem( new Axe() ); break;
					case 1: AddItem( new BattleAxe() ); break;
					case 2: AddItem( new DoubleAxe() ); break;
					case 3: AddItem( new ExecutionersAxe() ); break;
					case 4: AddItem( new Hatchet() ); break;
					case 5: AddItem( new LargeBattleAxe() ); break;
					case 6: AddItem( new Bardiche() ); break;
					case 7: AddItem( new BladedStaff() ); break;
					case 8: AddItem( new Halberd() ); break;
					case 9: AddItem( new QuarterStaff() ); break;
					case 10: AddItem( new GnarledStaff() ); break;
					case 11: AddItem( new WarHammer() ); break;
					case 12: AddItem( new Pitchfork() ); break;
					case 13: AddItem( new Spear() ); break;
				}
			}
			else
			{
				switch ( Utility.Random( 10 ) )
				{
					case 0: AddItem( new Broadsword() ); break;
					case 1: AddItem( new Longsword() ); break;
					case 2: AddItem( new VikingSword() ); break;
					case 3: AddItem( new HammerPick() ); break;
					case 4: AddItem( new Mace() ); break;
					case 5: AddItem( new Maul() ); break;
					case 6: AddItem( new Scepter() ); break;
					case 7: AddItem( new WarAxe() ); break;
					case 8: AddItem( new WarMace() ); break;
					case 9: AddItem( new WarFork() ); break;
				}
				switch ( Utility.Random( 4 ) )
				{
					case 0: AddItem( new BronzeShield() ); break;
					case 1: AddItem( new HeaterShield() ); break;
					case 2: AddItem( new MetalShield() ); break;
					case 3: AddItem( new WoodenKiteShield() ); break;
				}
			}

			PackItem( new Bandage( Utility.RandomMinMax( 2, 5 ) ) );
			if ( Utility.RandomDouble() < 0.5 )
				PackItem( new Bola() );

			Utility.AssignRandomHair( this );

			Fame = 9000;
			Karma = 0;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
		}

		public static bool IsMurderer( Mobile from )
		{
			if ( from != null && from is PlayerMobile )
			{
				BaseQuest quest = QuestHelper.GetQuest( (PlayerMobile) from, typeof( CastleAnthraxQuest ) );

				if ( quest != null )
					return !quest.Completed;
			}
			return false;
		}

		public override bool Unprovokable{ get{ return true; } }
		public override double MinHealDelay{ get{ return 3.0; } }
		public override bool CanHeal{ get{ return !MortalStrike.IsWounded( this ); } }

		public override void AlterMeleeDamageTo( Mobile to, ref int damage )
		{
			if ( to is BaseCreature && (((BaseCreature)to).Controlled || ((BaseCreature)to).Summoned) )
				damage *= (Utility.RandomDouble() < 0.6 ? 4 : 2);
			else if ( to.Player && to.Body.IsMale && Utility.RandomDouble() < 0.05 )
			{
				PlaySound( Utility.RandomList( 0x338, 0x31C, 0x328 ) );
				damage *= 2;
			}
		}

		public override void AlterMeleeDamageFrom( Mobile from, ref int damage )
		{
			if ( from is BaseCreature && ((BaseCreature)from).Controlled )
				damage /= 2;
		}

		public override double WeaponAbilityChance{ get{ return 0.3; } }
		public override WeaponAbility GetWeaponAbility()
		{
			BaseWeapon w = (BaseWeapon)Weapon;
			if ( w != null )
				return Utility.RandomDouble() < 0.3 ? w.PrimaryAbility : w.SecondaryAbility;
			return null;
		}

		private void ThrowBola()
		{
			if ( Backpack.GetAmount( typeof( Bola ) ) > 0 )
			{
				Mobile c = Combatant;
				m_Delay = DateTime.Now + TimeSpan.FromSeconds( Utility.RandomMinMax( 5, 10 ) );
				if ( c.Mounted && InRange( c, 8 ) && CanBeHarmful( c ) && InLOS( c ) && CanSee( c ) )
				{
					Use( Backpack.FindItemByType( typeof( Bola ) ) );
					if ( Target != null )
						Target.Invoke( this, c );
				}
			}
		}

		private void ReArm()
		{
			if ( Backpack != null )
			{
				m_Rearm = DateTime.Now + TimeSpan.FromSeconds( 5 );
				if ( (FindItemOnLayer( Layer.TwoHanded ) == null) || FindItemOnLayer( Layer.OneHanded ) == null && FindItemOnLayer( Layer.TwoHanded ) as BaseShield != null )
				{
					Item w = Backpack.FindItemByType( typeof( BaseWeapon ) );
					Item s = Backpack.FindItemByType( typeof( BaseShield ) );

					if ( w != null )
						EquipItem( w );

					if ( s != null )
						EquipItem( s );
				}
			}
		}

		public override void OnThink()
		{
			base.OnThink();
			if ( m_NextPickup < DateTime.Now && Backpack != null )
			{
				m_NextPickup = DateTime.Now + TimeSpan.FromSeconds( 5 );
				IPooledEnumerable eable = Map.GetItemsInRange( Location, 2 );
				foreach ( Item i in eable )
				{
					if ( i is Bola && Backpack.CheckHold( this, i, false, true ) && i.Movable && i.Stackable )
					{
						AddToBackpack( i );
						break;
					}
				}
				eable.Free();
			}

			if ( Combatant == null )
				return;

			if ( m_Delay < DateTime.Now )
				ThrowBola();

			if ( m_Rearm < DateTime.Now )
				ReArm();
		}

		public CastleWomen( Serial serial ) : base( serial )
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