using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Spells;
using System.Collections.Generic;

namespace Server.Mobiles
{
	[CorpseName( "a shadowlord corpse" )]
	public class shadowlord3 : BaseCreature
	{
		public static double ChestChance = .30;
		
		[Constructable]
		public shadowlord3 () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Astaroth The Shadowlord of Hatred";
			Body = 704;
			BaseSoundID = 0x47D;
			Team = 1;
			SetStr( 190, 210 );
			SetDex( 450, 550 );
			SetInt( 350, 450 );
			NameHue = 38;
			SetHits( 90000, 120000 );

			SetDamage( 10, 30 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 90, 95 );
			SetResistance( ResistanceType.Fire, 90, 95 );
			SetResistance( ResistanceType.Cold, 90, 95 );
			SetResistance( ResistanceType.Poison, 90, 95 );
			SetResistance( ResistanceType.Energy, 90, 95 );

			SetSkill( SkillName.EvalInt, 100.0, 120.0 );
			SetSkill( SkillName.Magery, 190.0, 200.0 );
			SetSkill( SkillName.Meditation, 90.0, 120.0 );
			SetSkill( SkillName.MagicResist, 100.0, 150.0 );
			SetSkill( SkillName.Tactics, 80.0, 120.0 );
			SetSkill( SkillName.Wrestling, 190.0, 200.0 );
			
			Fame = 24000;
			Karma = -24000;

			VirtualArmor = 80;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			//AddLoot( LootPack.MedScrolls, 2 );
		}
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			
			c.DropItem( new RewardScroll(15) );
			if ( Utility.RandomDouble() < 0.25 )
			c.DropItem( new RewardScroll(2) );
			if ( Utility.RandomDouble() < 0.15 )
			c.DropItem( new RewardScroll(5) );
			if ( Utility.RandomDouble() < 0.10 )
			c.DropItem( new RewardScroll(10) );
			if ( Utility.RandomDouble() < 0.05 )
			c.DropItem( new RewardScroll(15) );
		}
		
		bool tick = false;
		
		public override void OnThink()
		{
			tick = !tick;
		
			if ( tick )
				return;
		
			List<Mobile> targets = new List<Mobile>();

			if ( Map != null )
				foreach ( Mobile m in GetMobilesInRange( 2 ) )
					if ( this != m && SpellHelper.ValidIndirectTarget( this, m ) && CanBeHarmful( m, false ) && ( !Core.AOS || InLOS( m ) ) )
					{
						if ( m is BaseCreature && ((BaseCreature) m).Controlled )
							targets.Add( m );
						else if ( m.Player )
							targets.Add( m );
					}
					
			for ( int i = 0; i < targets.Count; ++i )
			{
				Mobile m = targets[ i ];
				
				AOS.Damage( m, this, 5, 0, 100, 0, 0, 0 );
				
				if ( m.Player )
					m.SendMessage( "The intense Hate is damaging you!", Name ); // : The intense heat is damaging you!
					m.FixedParticles( 6571, 1, 15, 0x00,  0, 1, EffectLayer.LeftFoot );
					m.PlaySound( 0x346 );
			}			
		}
		
		public override int TreasureMapLevel{ get{ return 6; } }
		public override bool AlwaysMurderer{ get{ return true; } }

		private static bool m_InHere;

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

                switch (Utility.Random(4))
                {
                    case 0: defender.FixedParticles( 14089, 1, 15, 0x00,  38, 1, EffectLayer.LeftFoot ); defender.PlaySound( 0x225 ); break;
                    case 1: defender.FixedParticles( 14013, 1, 15, 0x00,  38, 1, EffectLayer.LeftFoot ); defender.PlaySound( 0x11D ); break;
                    case 2: defender.FixedParticles( 0x3818, 1, 15, 0x00,  38, 1, EffectLayer.LeftFoot ); defender.PlaySound( 0x51E ); break;
                    case 3: defender.FixedParticles( 14217, 1, 15, 0x00,  38, 1, EffectLayer.LeftFoot ); defender.PlaySound( 0x029 ); break;
                }
		}

        public override void OnGotMeleeAttack(Mobile attacker)
        {
            if (this.Map != null && attacker != this && 0.1 > Utility.RandomDouble())
            {
                if (attacker is BaseCreature)
                {
                    BaseCreature pet = (BaseCreature)attacker;
                    if (pet.ControlMaster != null && (attacker is Dragon || attacker is GreaterDragon || attacker is SkeletalDragon || attacker is WhiteWyrm || attacker is Drake))
                    {
                        Combatant = null;
                        pet.Combatant = null;
                        Combatant = null;
                        pet.ControlMaster = null;
                        pet.Controlled = false;
                        attacker.Emote(String.Format("* {0} decided to go wild *", attacker.Name));
                    }

                    if (pet.ControlMaster != null && 0.1 > Utility.RandomDouble())
                    {
                        Combatant = null;
                        pet.Combatant = pet.ControlMaster;
                        Combatant = null;
                        attacker.Emote(String.Format("* {0} is being angered *", attacker.Name));
                    }
                }
            }

            base.OnGotMeleeAttack(attacker);
        }
	
		public shadowlord3( Serial serial ) : base( serial )
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