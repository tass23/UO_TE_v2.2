using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class SlurpyMcTreasureChest : MetalGoldenChest
	{
		private SlurpyMcTreasureChestTimer m_Timer;
	
		private int m_Range;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Range
		{
			get{ return m_Range; }
			set
			{
				if ( value < 1 )
				{
					m_Range = 1;
				}
				else
				{
					m_Range = value;
				}
			}
		}

		private int m_Delay;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Delay
		{
			get{ return m_Delay; }
			set
			{
				if ( value < 1 )
				{
					m_Delay = 1;
				}
				else
				{
					m_Delay = value;
				}
			}
		}

		[Constructable]
		public SlurpyMcTreasureChest( int range, int delay ) : base()
		{
			Name = "Slurpy McTreasureChest";
			Range = range;
			Delay = delay;
			
			LiftOverride = true;
			
			RestartTimer();
		}

		[Constructable]
		public SlurpyMcTreasureChest( int range ) : this( range, 10 )
		{
		}

		[Constructable]
		public SlurpyMcTreasureChest() : this( 10 )
		{
		}

		public SlurpyMcTreasureChest( Serial serial ) : base( serial )
		{
		}

		public void OnTick()
		{
			// scan for corpses and transfer inventory to me, then delete corpse.

			foreach ( Item item in GetItemsInRange( Range ) )
			{
				if ( item != null && item is Corpse )
				{
					Corpse c = (Corpse)item;
					
					if ( c != null && c.Items.Count > 0 )
					{
						// code from Destroy() in Container.cs (core)
						for ( int i = c.Items.Count - 1; i >= 0; --i )
						{
							if ( i < c.Items.Count )
							{
								if ( c.Items[i] != null )
								{
									DropItem( c.Items[i] );
								}
							}
						}

						if ( Utility.RandomDouble() > 0.9 )
						{
							OMGSLURP();
						}
					}
				}
			}
			
			RestartTimer();
		}
		
		public void OMGSLURP()
		{
			string slurp = "";
			
			switch( Utility.RandomMinMax( 1, 9 ) )
			{
				default: case 1: slurp = "Sluuuurp!"; break;
				case 2: slurp = "*burp*"; break;
				case 3: slurp = "Om nom nom!"; break;
				case 4: slurp = "Yummy."; break;
				case 5: slurp = "*munch*"; break;
				case 6: slurp = "*nibble*"; break;
				case 7: slurp = "Slurp!"; break;
				case 8: slurp = "Glomp!"; break;
				case 9: slurp = "Delicious."; break;
			}
		
			PublicOverheadMessage( MessageType.Regular, Utility.RandomDyedHue(), false, slurp );
		}
		
		public void RestartTimer()
		{
			if ( m_Timer != null )
			{
				m_Timer.Stop();
			}
			
			m_Timer = new SlurpyMcTreasureChestTimer( this, TimeSpan.FromSeconds( m_Delay ) );
			m_Timer.Start();
		}
		
		public override void OnAfterDelete()
		{
			if ( m_Timer != null )
			{
				m_Timer.Stop();
			}
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			
			writer.Write( m_Range );
			writer.Write( m_Delay );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_Range = reader.ReadInt();
			m_Delay = reader.ReadInt();
			
			RestartTimer();
		}

	}

	public class SlurpyMcTreasureChestTimer : Timer
	{
		private SlurpyMcTreasureChest m_Slurpy;

		public SlurpyMcTreasureChestTimer( SlurpyMcTreasureChest slurpy, TimeSpan delay ) : base( delay )
		{
			m_Slurpy = slurpy;

			Priority = TimerPriority.FiveSeconds;
		}
		
		protected override void OnTick()
		{
			if ( m_Slurpy != null )
			{
				m_Slurpy.OnTick();
			}
			else
			{
				Stop();
			}
		}
	}
}