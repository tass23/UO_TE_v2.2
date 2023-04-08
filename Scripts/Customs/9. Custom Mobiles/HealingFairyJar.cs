using System;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
	public class HealingFairyJar : Item
	{
		public static void Initialize()
        {
            EventSink.PlayerDeath += new PlayerDeathEventHandler(EventSink_Death);
        }

        private static void EventSink_Death(PlayerDeathEventArgs e)
        {
            CheckRelease(e.Mobile);
        }
		
		public bool m_HasFairy = false;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public bool HasFairy
		{
			get{ return m_HasFairy; }
			set
			{
				SetHasFairy(value);
				InvalidateProperties();
			}
		}
		
		[Constructable]
		public HealingFairyJar() : base( 0x1005 )
		{
			Weight = 1.0;
			Hue = 0x482;
			SetHasFairy(m_HasFairy);
			LootType = LootType.Blessed;
		}

		public HealingFairyJar( Serial serial ) : base( serial )
		{
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
				UseJar(from);
			else
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
		}

		public void UseJar( Mobile from )
		{
			if ( m_HasFairy )
			{
				BaseCreature mob;
				try { mob = (BaseCreature) Activator.CreateInstance( typeof(SummonedHealingFairy) ); }
				catch { mob = null; }
				if ( mob != null )
				{
					BaseCreature.Summon( mob, from, from.Location, mob.BaseSoundID, TimeSpan.FromSeconds( 30 ) );
					SetHasFairy(false);
					from.SendMessage( "You release the fairy." );
				}
			}
			else if ( !m_HasFairy && from.Alive )
			{
				from.SendMessage("Target a healing fairy to capture!");
				from.Target = new CaptureFairyTarget( this );
			}
		}
		
		public void SetHasFairy ( bool HasFairy )
		{
			m_HasFairy = HasFairy;
			if ( m_HasFairy )
			{
				ItemID = 0x1006;
				Name = "A Healing Fairy Jar: Full";
				Hue = 0;
			}
			else
			{
				ItemID = 0x1005;
				Name = "A Healing Fairy Jar: Empty";
				Hue = 0x482;
			}
		}
	
		public static void CheckRelease( Mobile owner )
        {
            if (owner != null && !owner.Deleted)
            {
                if (owner.Alive)
                    return;
                if (owner.Backpack == null || owner.Backpack.Deleted)
                    return;
				
				List<HealingFairyJar> jars = owner.Backpack.FindItemsByType<HealingFairyJar>(true) ;
				
				if(jars != null)
				{
					foreach( HealingFairyJar jar in jars )
					{
						if (jar != null && !jar.Deleted && jar.m_HasFairy)
						{
							jar.UseJar(owner);
							break;
						}
					}
				}
				
            }
        }
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );			
			writer.Write( (int) 0 ); // version
			writer.Write( (bool) m_HasFairy );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );			
			int version = reader.ReadInt();
			m_HasFairy = reader.ReadBool();
			if(LootType != LootType.Blessed)
				LootType = LootType.Blessed;
		}
		
		private class CaptureFairyTarget : Target
		{
			private HealingFairyJar m_Jar;

			public CaptureFairyTarget( HealingFairyJar jar ) : base( -1, false, TargetFlags.None )
			{
				m_Jar = jar;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Jar.Deleted || m_Jar.HasFairy )
					return;

				if ( m_Jar.RootParent != from )
				{
					from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
				}
				else if ( targeted is HealingFairy )
				{
					HealingFairy fairy = (HealingFairy) targeted;

					if ( fairy.Controlled || ( fairy.Summoned && fairy.SummonMaster != from) )
					{
						from.SendMessage("You may only target a healing fairy that does not belong to someone.");
					}
					else
					{
						m_Jar.SetHasFairy(true);
						fairy.Delete();
					}
				}
				else
				{
					from.SendMessage("You may only capture healing fairies with this jar.");
				}
			}
		}
	}	
}