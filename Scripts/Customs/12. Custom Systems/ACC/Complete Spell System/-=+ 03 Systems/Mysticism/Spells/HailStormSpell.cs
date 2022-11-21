using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Spells;
using Server.Targeting;

namespace Server.ACC.CSS.Systems.Mysticism
{
	public class HailStormSpell : MysticismSpell
	{
		// A torrent of rain brings down a storm of hailstones upon the caster's enemies in the surrounding area. 

		public override int RequiredMana{ get{ return 40; } }
		public override double RequiredSkill{ get{ return 70; } }

		private static SpellInfo m_Info = new SpellInfo(
				"Hail Storm", "Kal Des Ylem",
				230,
				9022,
				Reagent.BlackPearl,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot,
				Reagent.DragonBlood
			);
			
		public override SpellCircle Circle
    {
      get { return SpellCircle.Fourth; }
    }

		public HailStormSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new MysticismSpellTarget( this, true, TargetFlags.None );
		}

		public override void OnTarget( Object o )
		{
			IPoint3D p = o as IPoint3D;

			if ( p == null )
				return;

			Point3D point = new Point3D( p );

			// Can you stack multiple hail sotrms on OSI?
			if ( !IsHailStorming( point ) )
			{
				HailStormArea.Add( point );
				new HailStormTimer( this, Caster, point ).Start();
				Caster.PlaySound( 0x649 );
			}
			else
				Caster.SendMessage( "It is already hailing there." );

			FinishSequence();
		}

		public static List<Point3D> HailStormArea = new List<Point3D>();

		public static bool IsHailStorming( Point3D point )
		{
			bool hailing = false;

			for ( int i = 0; i < HailStormArea.Count; i++ )
			{
				if ( HailStormArea[i].X >= (point.X - 5) && HailStormArea[i].X <= (point.X + 5) )
					if ( HailStormArea[i].Y >= (point.Y - 5) && HailStormArea[i].Y <= (point.Y + 5) )
					{
						hailing = true;
						break;
					}
			}

			return hailing;
		}

		public static void RemoveHailPoint( Point3D point )
		{
			for ( int i = 0; i < HailStormArea.Count; i++ )
				if ( point.X == HailStormArea[i].X && point.Y == HailStormArea[i].Y )
					HailStormArea.Remove( HailStormArea[i] );
		}

		public class HailStormTimer : Timer
		{
			private MysticismSpell m_Spell;
			public Mobile Caster;
			private Point3D m_StormPoint;
			private Map m_StormMap;
			private int m_Damage;
			private int m_Count;
			private int m_MaxCount;
			private Point3D m_LastTarget = new Point3D();

			public HailStormTimer( MysticismSpell spell, Mobile caster, Point3D point ) : base( TimeSpan.FromMilliseconds( 100.0 ), TimeSpan.FromMilliseconds( 100.0 ) )
			{
				m_Spell = spell;
				Caster = caster;
				m_StormPoint = point;
				m_StormMap = caster.Map;
				m_Count = 0;
				m_MaxCount = 75;
				m_Damage = (int)( (caster.Skills[SkillName.Mysticism].Value + (caster.Skills[SkillName.Focus].Value / 2)) / 4 );
			}

			protected override void OnTick()
			{
				m_Count++;

				//if ( Caster != null )
					//Ability.Aura( m_LastTarget, m_StormMap, Caster, m_Damage, m_Damage, ResistanceType.Cold, 0, null, "" );

				if ( m_Count > m_MaxCount || Caster == null )
				{
					HailStormSpell.RemoveHailPoint( m_StormPoint );
					Stop();
					return;
				}

				if ( (m_Count % 10) == 0 )
					//Ability.Aura( m_StormPoint, m_StormMap, Caster, 15, 15, ResistanceType.Cold, 5, null, "" );

				m_LastTarget.X = m_StormPoint.X += Utility.RandomMinMax( -5, 5 );
				m_LastTarget.Y = m_StormPoint.Y += Utility.RandomMinMax( -5, 5 );
				Effects.SendMovingParticles( 
					new Entity( Serial.Zero, new Point3D( m_LastTarget.X - 10, m_LastTarget.Y - 10, m_LastTarget.Z + 30 ), m_StormMap ),
					new Entity( Serial.Zero, m_LastTarget, m_StormMap ), 
					0x36D4 /*0x1EA7*/, 15, 0, false, false, 1365, 0, 9502, 1, 0, (EffectLayer)255, 0x100 );
			}
		}
	}
}
/*



*/