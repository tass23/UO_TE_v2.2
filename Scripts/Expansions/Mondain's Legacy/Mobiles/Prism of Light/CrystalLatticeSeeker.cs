using System;
using System.Collections;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles 
{ 
	[CorpseName( "a crystal lattice seeker corpse" )] 
	public class CrystalLatticeSeeker : BaseCreature 
	{ 
		[Constructable] 
		public CrystalLatticeSeeker() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{		
			Name = "a crystal lattice seeker";
			Body = 0x7B;
			Hue = 0x47E;
			
			SetStr( 550, 850 );
			SetDex( 190, 250 );
			SetInt( 350, 450 );

			SetHits( 350, 550 );

			SetDamage( 13, 19 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 80, 90 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Anatomy, 50.0, 75.0 );
			SetSkill( SkillName.EvalInt, 90.0, 100.0 );
			SetSkill( SkillName.Magery, 100.0, 100.0 );
			SetSkill( SkillName.Meditation, 90.0, 100.0 );
			SetSkill( SkillName.MagicResist, 90.0, 100.0 );
			SetSkill( SkillName.Tactics, 90.0, 100.0 );
			SetSkill( SkillName.Wrestling, 90.0, 100.0 );
			
			Fame = 18000;
			Karma = -18000;

            PackArcaneScroll(0, 2);
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 4 );
			AddLoot( LootPack.Parrot );
			AddLoot( LootPack.Gems );
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if ( 0.1 >= Utility.RandomDouble() )
				Drain();
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( 0.1 >= Utility.RandomDouble() )
				Drain();
		}
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );		
			
			if ( Utility.RandomDouble() < 0.75 )
				c.DropItem( new CrystallineFragments() );
				
			if ( Utility.RandomDouble() < 0.07 )
				c.DropItem( new PiecesOfCrystal() );
		}
		
		public override int Feathers{ get{ return 100; } }		
		public override int TreasureMapLevel{ get{ return 5; } }

		public override int GetAttackSound() { return 0x2F6; }
		public override int GetDeathSound()	{ return 0x2F7;	}
		public override int GetAngerSound() { return 0x2F8; }
		public override int GetHurtSound() { return 0x2F9; }
		public override int GetIdleSound() { return 0x2FA; }	
				
		public CrystalLatticeSeeker( Serial serial ) : base( serial ) 
		{ 
		} 
		
		public virtual void Drain()
		{
			switch ( Utility.Random( 3 ) )
			{
				case 0: Drain( SkillCheck.Stat.Str ); break;
				case 1: Drain( SkillCheck.Stat.Dex ); break;
				case 2: Drain( SkillCheck.Stat.Int ); break;
			}			
		}

		public virtual void Drain( SkillCheck.Stat stat )
		{
			Mobile m = Combatant;
			
			if ( m != null && CanBeHarmful( m ) )
			{
				int toDrain;
				
				switch ( stat )
				{
					case SkillCheck.Stat.Str: 
						Say( 1042156 ); // I can grant life, and I can sap it as easily.
						PlaySound( 0x1E6 );
						
						toDrain = Utility.RandomMinMax( 3, 8 );
						Hits += toDrain;						
						m.Hits -= toDrain;
						break;
					case SkillCheck.Stat.Dex: 
						Say( 1042157 ); // You'll go nowhere, unless I deem it should be so.
						PlaySound( 0x1DF );
						
						toDrain = Utility.RandomMinMax( 20, 30 );
						Stam += toDrain;					
						m.Stam -= toDrain;
						break;
					case SkillCheck.Stat.Int: 
						Say( 1042155 ); // Your power is mine to use as I will.
						PlaySound( 0x1F8 );
						
						toDrain = Utility.RandomMinMax( 20, 30 );
						Mana += toDrain;					
						m.Mana -= toDrain;
						break;
				}
				
				m.FixedParticles( 0x374A, 10, 15, 5013, 0x496, 0, EffectLayer.Waist );
			}
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