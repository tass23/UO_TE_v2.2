using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Spells;
using Server.Spells.Third;
using Server.Spells.Sixth;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a changeling corpse" )]
	public class Changeling : BaseCreature
	{
		[Constructable]
		public Changeling() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.1, 0.4 )
		{
			Name = "a changeling";
			Body = 264;
            BaseSoundID = 0x470;

			SetStr( 36, 105 );
			SetDex( 212, 262 );
			SetInt( 317, 399 );

			SetHits( 201, 211 );
			SetStam( 212, 262 );
			SetMana( 317, 399 );

			SetDamage( 11, 22 );

			SetDamageType( ResistanceType.Physical, 100 );
			
			SetResistance( ResistanceType.Physical, 81, 90 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 40, 49 );
			SetResistance( ResistanceType.Poison, 41, 50 );
			SetResistance( ResistanceType.Energy, 43, 50 );

			SetSkill( SkillName.Wrestling, 10.4, 12.5 );
			SetSkill( SkillName.Tactics, 12.0, 19.4 );
			SetSkill( SkillName.MagicResist, 121.6, 132.2 );
			SetSkill( SkillName.Magery, 91.6, 99.5 );
			SetSkill( SkillName.EvalInt, 91.5, 98.8 );
			SetSkill( SkillName.Meditation, 91.7, 98.5 );
			
			PackScroll( 1, 7 );
			PackItem( new Arrow( 35 ) );
			PackItem( new Bolt( 25 ) );			
			PackGem( 2 );

            PackArcaneScroll(0, 1);

            Fame = 15000;
            Karma = -15000;
		}

		public Changeling( Serial serial ) : base( serial )
		{
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosRich, 3 );
		}
		
		private Timer m_Timer;
		private string m_Name;
		private int m_Hue;

		public override void OnThink()
		{
			base.OnThink();
			
			if ( Combatant == null )
				return;	
				
			if ( Hits > 0.8 * HitsMax && Utility.RandomDouble() < 0.05 )
				FireRing();				
				
			if ( Combatant.Player && Name != Combatant.Name )
				Morph();	
		}
		
		public virtual void Morph()
		{
			m_Name = Name;
			m_Hue = Hue;
			
			Body = Combatant.Body; 
			Hue = Combatant.Hue; 
			Name = Combatant.Name;
			Female = Combatant.Female;
			Title = Combatant.Title;
  				
			foreach ( Item item in Combatant.Items )
			{
				if ( item.Layer != Layer.Backpack && item.Layer != Layer.Mount && item.Layer != Layer.Bank )
					if ( FindItemOnLayer( item.Layer ) == null )
						AddItem( new ClonedItem( item ) );
			}
			
			PlaySound( 0x511 );
			FixedParticles( 0x376A, 1, 14, 5045, EffectLayer.Waist );
			
			if ( m_Timer != null )
				m_Timer.Stop();
			
			m_Timer = Timer.DelayCall( TimeSpan.FromSeconds( 5 ), TimeSpan.FromSeconds( 5 ), new TimerCallback( EndMorph ) );
		}
		
		public void DeleteItems()
		{		
			for ( int i = Items.Count - 1; i >= 0; i -- )
				if ( Items[ i ] is ClonedItem )
					Items[ i ].Delete();
					
			if ( Backpack != null )
			{
				for ( int i = Backpack.Items.Count - 1; i >= 0; i -- )
					if ( Backpack.Items[ i ] is ClonedItem )
						Backpack.Items[ i ].Delete();
			}
		}
		
		public virtual void EndMorph()
		{
			if ( Combatant != null && Name == Combatant.Name )
				return;
		
			DeleteItems();
			
			if ( m_Timer != null )
			{
				m_Timer.Stop();		
				m_Timer = null;	
			}
				
			if ( Combatant != null )
			{
				Morph();				
				return;
			}
			
			Body = 264;
			Title = null;
			Name = m_Name;
			Hue = m_Hue;
			
			PlaySound( 0x511 );
			FixedParticles( 0x376A, 1, 14, 5045, EffectLayer.Waist );
		}
		
		private static int[] m_North = new int[]
		{
			-1, -1, 
			1, -1,
			-1, 2,
			1, 2
		};
		
		private static int[] m_East = new int[]
		{
			-1, 0,
			2, 0
		};
		
		public virtual void FireRing()
		{
			for ( int i = 0; i < m_North.Length; i += 2 ) 
			{
				Point3D p = Location;
				
				p.X += m_North[ i ];
				p.Y += m_North[ i + 1 ];
				
				IPoint3D po = p as IPoint3D;
				
				SpellHelper.GetSurfaceTop( ref po );
				
				Effects.SendLocationEffect( po, Map, 0x3E27, 50 );
			}
			
			for ( int i = 0; i < m_East.Length; i += 2 ) 
			{
				Point3D p = Location;
				
				p.X += m_East[ i ];
				p.Y += m_East[ i + 1 ];
				
				IPoint3D po = p as IPoint3D;
				
				SpellHelper.GetSurfaceTop( ref po );
				
				Effects.SendLocationEffect( po, Map, 0x3E31, 50 );
			}
		}
		
		public override bool OnBeforeDeath()
		{			
			if ( m_Timer != null )
				m_Timer.Stop();
					
			return base.OnBeforeDeath();
		}
		
		public override void OnAfterDelete()
		{					
			if ( m_Timer != null )
				m_Timer.Stop();
				
			base.OnAfterDelete();	
		}
		
		public override void Damage( int amount, Mobile from )
		{
			base.Damage( amount, from );
						
			if ( Combatant == null || Hits > HitsMax * 0.2 || Utility.RandomBool() )
				return;	
							
			new InvisibilitySpell( this, null ).Cast();
			
			Target target = Target;
			
			if ( target != null )
				target.Invoke( this, this );
				
			Timer.DelayCall( TimeSpan.FromSeconds( 1 ), new TimerCallback( Teleport ) );
		}
		
		public virtual void Teleport()
		{				
			if ( Combatant == null )
				return;
						
			// 20 tries to teleport
			for ( int tries = 0; tries < 20; tries ++ )
			{
				int x = Utility.RandomMinMax( 5, 7 ); 
				int y = Utility.RandomMinMax( 5, 7 );
				
				if ( Utility.RandomBool() )
					x *= -1;
					
				if ( Utility.RandomBool() )
					y *= -1;
				
				Point3D p = new Point3D( X + x, Y + y, 0 );
				IPoint3D po = new LandTarget( p, Map ) as IPoint3D;
				
				if ( po == null )
					continue;
					
				SpellHelper.GetSurfaceTop( ref po );

				if ( InRange( p, 12 ) && InLOS( p ) && Map.CanSpawnMobile( po.X, po.Y, po.Z ) )
				{					
					
					Point3D from = Location;
					Point3D to = new Point3D( po );
	
					Location = to;
					ProcessDelta();
					
					FixedParticles( 0x376A, 9, 32, 0x13AF, EffectLayer.Waist );
					PlaySound( 0x1FE );
										
					return;					
				}
			}		
			
			RevealingAction();
		}
            public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
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

		private class ClonedItem : Item
		{	
			public ClonedItem( Item oItem ) : base( oItem.ItemID )
			{
				Name = oItem.Name;
				Weight = oItem.Weight;
				Hue = oItem.Hue;
				Layer = oItem.Layer;
			}
	
			public override DeathMoveResult OnParentDeath( Mobile parent )
			{
				return DeathMoveResult.RemainEquiped;
			}
			
			public override DeathMoveResult OnInventoryDeath( Mobile parent )
			{
				Delete();
				return base.OnInventoryDeath( parent );
			}
	
			public ClonedItem( Serial serial ) : base( serial )
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
}

