
using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Items.Crops
{
	public class BaseGrapeVine : Item
	{
		private const int max = 5;
		private DateTime lastpicked;
		private int m_yield;
		private GrapeVariety m_Variety;
		private Mobile m_Placer;

		public Timer regrowTimer;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Yield{ get{ return m_yield; } set{ m_yield = value; } }

		public int Capacity{ get{ return max; } }
		public DateTime LastPick{ get{ return lastpicked; } set{ lastpicked = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public GrapeVariety Variety
		{
			get{ return m_Variety; }
			set{ m_Variety = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Placer
		{
			get{ return m_Placer; }
			set{ m_Placer = value; }
		}

		public virtual GrapeVariety DefaultVariety{ get{ return GrapeVariety.CabernetSauvignon; } }

		[Constructable]
		public BaseGrapeVine( int itemID ) : base( itemID )
		{
			Movable = false;
			m_Variety = DefaultVariety;
			m_Placer = null;

			init( this, false );
		}

		public override void AddNameProperty( ObjectPropertyList list )
 		{
 			list.Add( 1060658, "Variety\t{0}", WinemakingResources.GetName( m_Variety ) );

			base.AddNameProperty( list );
 		}

		public override void OnSingleClick( Mobile from )
		{
			this.LabelTo( from, "Variety : {0} ", WinemakingResources.GetName( m_Variety ) );
		}

		public static void init ( BaseGrapeVine plant, bool full )
		{
			plant.LastPick = DateTime.Now;
			plant.regrowTimer = new FruitTimer( plant );

			if ( full )
			{
				plant.Yield = plant.Capacity;
			}
			else
			{
				plant.Yield = 5;
				plant.regrowTimer.Start();
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.Mounted )
			{
				from.SendMessage( "You cannot pick fruit while mounted." );
				return;
			}

			if ( DateTime.Now > lastpicked.AddSeconds(5) )
			{
				lastpicked = DateTime.Now;

				int lumberValue = (int)from.Skills[SkillName.Cooking].Value / 20;
				if ( from.Mounted )
					++lumberValue;

				if ( lumberValue < 0 )
				{
					from.SendMessage( "You have no idea how to pick this fruit." );
					return;
				}

				if ( from.InRange( this.GetWorldLocation(), 2 ) )
				{
					if( m_Placer == null || from == m_Placer || from.AccessLevel >= AccessLevel.GameMaster )
					{
						if ( m_yield < 1 )
						{
							from.SendMessage( "There is nothing here to harvest." );
						}
						else
						{
							from.Direction = from.GetDirectionTo( this );

							from.Animate( from.Mounted ? 26:17, 7, 1, true, false, 0 );

							if ( lumberValue < m_yield )
								lumberValue = m_yield + 1;

							int pick = Utility.Random( lumberValue );
							if ( pick == 0 )
							{
								from.SendMessage( "You do not manage to gather any fruit." );
								return;
							}

							m_yield -= pick;
							from.SendMessage( "You pick {0} grape bunch{1}!", pick, ( pick == 1 ? "" : "es" ) );

							GiveGrapes(from, pick, m_Variety);

							if ( !regrowTimer.Running )
							{
								regrowTimer.Start();
							}
						}
					}
					else
					{
						from.SendMessage( "You do not own these vines." );
					}
				}
				else
				{
					from.SendLocalizedMessage( 500446 );
				}
			}
			else
			{
				from.SendMessage( "You must wait a few moments before you can pick more fruit." );
			}
		}

		public static void GiveGrapes( Mobile m, int pick, GrapeVariety variety )
		{
			switch ( variety )
			{
				case GrapeVariety.CabernetSauvignon:
				{
					CabernetSauvignonGrapes cscrop = new CabernetSauvignonGrapes( pick );
					m.AddToBackpack( cscrop );
					break;
				}
				case GrapeVariety.Chardonnay:
				{
					ChardonnayGrapes ccrop = new ChardonnayGrapes( pick );
					m.AddToBackpack( ccrop );
					break;
				}
				case GrapeVariety.CheninBlanc:
				{
					CheninBlancGrapes cbcrop = new CheninBlancGrapes( pick );
					m.AddToBackpack( cbcrop );
					break;
				}
				case GrapeVariety.Merlot:
				{
					MerlotGrapes mcrop = new MerlotGrapes( pick );
					m.AddToBackpack( mcrop );
					break;
				}
				case GrapeVariety.PinotNoir:
				{
					PinotNoirGrapes pncrop = new PinotNoirGrapes( pick );
					m.AddToBackpack( pncrop );
					break;
				}
				case GrapeVariety.Riesling:
				{
					RieslingGrapes rcrop = new RieslingGrapes( pick );
					m.AddToBackpack( rcrop );
					break;
				}
				case GrapeVariety.Sangiovese:
				{
					SangioveseGrapes scrop = new SangioveseGrapes( pick );
					m.AddToBackpack( scrop );
					break;
				}
				case GrapeVariety.SauvignonBlanc:
				{
					SauvignonBlancGrapes sbcrop = new SauvignonBlancGrapes( pick );
					m.AddToBackpack( sbcrop );
					break;
				}
				case GrapeVariety.Shiraz:
				{
					ShirazGrapes shcrop = new ShirazGrapes( pick );
					m.AddToBackpack( shcrop );
					break;
				}
				case GrapeVariety.Viognier:
				{
					ViognierGrapes vcrop = new ViognierGrapes( pick );
					m.AddToBackpack( vcrop );
					break;
				}
				case GrapeVariety.Zinfandel:
				{
					ZinfandelGrapes zcrop = new ZinfandelGrapes( pick );
					m.AddToBackpack( zcrop );
					break;
				}
				default:
				{
					Grapes crop = new Grapes( pick );
					m.AddToBackpack( crop );
					break;
				}
			}
		}

		private class FruitTimer : Timer
		{
			private BaseGrapeVine i_plant;

			public FruitTimer( BaseGrapeVine plant ) : base( TimeSpan.FromSeconds( 60 ), TimeSpan.FromSeconds( 30 ) )
			{
				Priority = TimerPriority.OneSecond;
				i_plant = plant;
			}

			protected override void OnTick()
			{
				if ( ( i_plant != null ) && ( !i_plant.Deleted ) )
				{
					int current = i_plant.Yield;

					if ( ++current >= i_plant.Capacity )
					{
						current = i_plant.Capacity;
						Stop();
					}
					else if ( current <= 0 )
						current = 1;

					i_plant.Yield = current;

				}
				else Stop();
			}
		}

		public BaseGrapeVine( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 2 );

			writer.Write( (Mobile)m_Placer );

			writer.Write( (int) m_Variety );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			switch ( version )
			{
				case 2:
				{
					m_Placer = reader.ReadMobile();
					goto case 1;
				}
				case 1:
				{
					m_Variety = (GrapeVariety)reader.ReadInt();
					goto case 0;
				}
				case 0:
				{
					init( this, true );
					break;
				}
			}
		}
	}
}