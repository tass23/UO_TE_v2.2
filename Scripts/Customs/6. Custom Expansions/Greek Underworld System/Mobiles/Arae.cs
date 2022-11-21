using System;
using System.Collections;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a daemonic corpse" )]
	public class Arae : BaseCreature
	{
		private DateTime m_NextAbility;

		[Constructable]
		public Arae() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an arae";
			Body = 173;
			Hue = 1760;
			BaseSoundID = 1170;

			SetStr( 300 );
			SetDex( 60 );
			SetInt( 100 );
			SetHits( 1000 );
			SetDamage( 15, 20 );
			SetDamageType( ResistanceType.Physical, 60 );
			SetResistance( ResistanceType.Physical, 60 );
			SetResistance( ResistanceType.Fire, 60 );
			SetResistance( ResistanceType.Cold, -50 );
			SetResistance( ResistanceType.Poison, 60 );
			SetResistance( ResistanceType.Energy, 60 );
			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 190.0 );
			SetSkill( SkillName.Wrestling, 190.0 );
			SetSkill( SkillName.Anatomy, 190.0 );

			Fame = 5000;
			Karma = -5000;
			VirtualArmor = 40;
		}

		public override bool OnBeforeDeath()
		{
			ArrayList alist = new ArrayList();
			IPooledEnumerable eable = this.Map.GetMobilesInRange( this.Location, 30 );

			foreach( Mobile m in eable )
				alist.Add( m );

			if ( alist != null && alist.Count > 0 )
			{
				for( int i = 0; i < alist.Count; i++ )
				{
					Mobile m = (Mobile)alist[i];
					if ( m is PlayerMobile )
					{
						int chance = Utility.Random( 1, 100 );
						if ( this.IsParagon == true )
							chance = Utility.Random( 1, 24 );
						if ( chance <= 24 )
						{
							m.SendMessage( 1150, "You recieve a rare Artifact!" );
							m.AddToBackpack( new CursedEarrings() );
						}
					}
				}
			}
			return base.OnBeforeDeath();
		}

		public override bool Unprovokable{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public override void OnThink()
		{
			if ( m_NextAbility < DateTime.Now && this.Combatant != null && this.Combatant.InRange( this.Location, 5 ))
			{
				this.PlaySound( 481 );
				this.Combatant.AddStatMod( new StatMod( StatType.Str, "Arae Curse Str", -15, TimeSpan.FromMinutes( 2.5 ) ) );
				this.Combatant.AddStatMod( new StatMod( StatType.Dex, "Arae Curse Dex", -15, TimeSpan.FromMinutes( 2.5 ) ) );
				this.Combatant.AddStatMod( new StatMod( StatType.Dex, "Arae Curse Int", -15, TimeSpan.FromMinutes( 2.5 ) ) );
				this.Combatant.Paralyzed = true;
				this.m_NextAbility = DateTime.Now + TimeSpan.FromSeconds( 30.0 );
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 3 );
		}

		public Arae( Serial serial ) : base( serial )
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