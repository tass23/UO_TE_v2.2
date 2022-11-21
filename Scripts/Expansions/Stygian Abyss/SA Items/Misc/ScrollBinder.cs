using System;
using Server;
using Server.Targeting;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using Server.Engines.Quests;
using System.Collections;
using Server.Items;
using Server.Regions;
using System.Collections.Generic;

namespace Server.Items
{
	public enum ScrollTypes
	{
		None							= 0,
		PowerScroll					= 1,
		StatCapScroll				= 2,
		ScrollofTranscendence	= 3
	}

	public class ScrollBinder : Item
	{
		public override int LabelNumber{ get{ return 1113135; } }

		private SkillName m_Skill;
		[CommandProperty( AccessLevel.Administrator )]
		public SkillName Skill { get { return m_Skill; } set { m_Skill = value; InvalidateProperties(); } }

		private double m_Value;
		[CommandProperty( AccessLevel.Administrator )]
		public double Value { get { return m_Value; } set { m_Value = value; InvalidateProperties(); } }

		private double m_AmountGiven;
		[CommandProperty( AccessLevel.Administrator )]
		public double AmountGiven { get { return m_AmountGiven; } set { m_AmountGiven = value; InvalidateProperties(); } }

		private int m_AmountNeeded;
		[CommandProperty( AccessLevel.Administrator )]
		public int AmountNeeded { get { return m_AmountNeeded; } set { m_AmountNeeded = value; InvalidateProperties(); } }

		private ScrollTypes m_ScrollType;
		[CommandProperty( AccessLevel.Administrator )]
		public ScrollTypes ScrollType { get { return m_ScrollType; } set { m_ScrollType = value; InvalidateProperties(); } }

		[Constructable]
		public ScrollBinder() : base( 0x14EF )
		{
			Hue = 437;
			LootType = LootType.Cursed;
			ScrollType = ScrollTypes.None;
			
		}

		public ScrollBinder( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( ScrollType == ScrollTypes.PowerScroll )
				list.Add( 1113149, string.Concat( "", Value.ToString(), "\t", SkillInfo.Table[(int)Skill].Name, "\t", "", AmountGiven.ToString(), "\t", AmountNeeded.ToString()  ) ); // ~1_bonus~ ~2_type~ : ~3_given~/~4_needed~
			else if ( ScrollType == ScrollTypes.StatCapScroll )
				list.Add( 1113292, string.Concat( "", (Value - 225).ToString(), "\t", "", "\t", AmountGiven.ToString(), "\t", AmountNeeded.ToString()  ) ); // +~1_bonus~ Stats : ~3_given~/~4_needed~
			else if ( ScrollType == ScrollTypes.ScrollofTranscendence )
				list.Add( 1113620, string.Concat( "", SkillInfo.Table[(int)Skill].Name, "\t", "", AmountGiven.ToString() ) ); // ~1_type~ transcendence : ~2_given~/5.0
		}


		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042664 ); // You must have the object in your backpack to use it.
				return;
			}

			switch ( ScrollType )
			{
				case ScrollTypes.PowerScroll:
				{
					from.SendLocalizedMessage( 1113138 ); // Target the powerscroll you wish to bind.
					break;
				}
				case ScrollTypes.StatCapScroll: 
				{
					from.SendLocalizedMessage( 1113140 ); // Target the stats scroll you wish to bind.
					break;
				}
				case ScrollTypes.ScrollofTranscendence: 
				{
					from.SendLocalizedMessage( 1113139 ); // Target the scroll of transcendence you wish to bind.
					break;
				}
				default:
				{
					from.SendLocalizedMessage( 1113141 ); // Target the scroll you wish to bind.
					break;
				}
			}
			from.Target = new InternalTarget( this );
		}

		private class InternalTarget : Target
		{
			private ScrollBinder m_ScrollBinder;

			public InternalTarget( ScrollBinder scrollBinder ) : base( 3, true, TargetFlags.None )
			{
				m_ScrollBinder = scrollBinder;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				Container pack = from.Backpack;

				if ( !( targeted is Item ) )
				{
					from.SendLocalizedMessage( 1113142 ); // You may only bind powerscrolls, stats scrolls or scrolls of transcendence.
					return;
				}
				else if ( ( pack == null ) || ( pack.Deleted ) )
				{
					from.SendLocalizedMessage( 1053057 ); // You do not have a backpack to place items in!
					return;
				}
				else if ( !m_ScrollBinder.IsChildOf( from.Backpack ) )
				{
					from.SendLocalizedMessage( 1042664 ); // You must have the object in your backpack to use it.
					return;
				}
				//--Mystara
				else if ( ( targeted is Item ) && ( (Item)targeted != null ) && !((Item)targeted).IsChildOf( from.Backpack ) )
				{
					from.SendLocalizedMessage( 1060640 ); // The item must be in your backpack to use it.
					return;
				}
				else if( targeted is PowerScroll )
				{
					if ( m_ScrollBinder.Deleted )
						return;

					PowerScroll ps = (PowerScroll)targeted;

					if ( m_ScrollBinder.ScrollType != ScrollTypes.None )
					{
						if ( ( m_ScrollBinder.ScrollType != ScrollTypes.PowerScroll ) || ( ps.Value != m_ScrollBinder.Value ) || ( ps.Skill != m_ScrollBinder.Skill ) )
						{
							from.SendLocalizedMessage( 1113143 ); // This scroll does not match the type currently being bound.
							return;
						}
					}

					if ( ps.Value == 120 )
					{
						from.SendLocalizedMessage( 1113144 ); // This scroll is already the highest of its type and cannot be bound.
						return;
					}

					if ( ps.Value == 105 )
						m_ScrollBinder.AmountNeeded = 8;
					else if ( ps.Value == 110 )
						m_ScrollBinder.AmountNeeded = 12;
					else if ( ps.Value == 115 )
						m_ScrollBinder.AmountNeeded = 10;

					m_ScrollBinder.AmountGiven++;
					m_ScrollBinder.ScrollType = ScrollTypes.PowerScroll;
					m_ScrollBinder.Skill = ps.Skill;
					m_ScrollBinder.Value = ps.Value;
					ps.Delete();

					if ( m_ScrollBinder.AmountGiven >= m_ScrollBinder.AmountNeeded )
					{
						int psValue = 110;

						if ( m_ScrollBinder.Value == 105 )
							psValue = 110;
						else if ( m_ScrollBinder.Value == 110 )
							psValue = 115;
						else if ( m_ScrollBinder.Value == 115 )
							psValue = 120;

						from.SendLocalizedMessage( 1113145 ); // You've completed your binding and received an upgraded version of your scroll!

						pack.DropItem( new PowerScroll( m_ScrollBinder.Skill, psValue ) );
						m_ScrollBinder.Delete();
					}
				}
				else if( targeted is ScrollofTranscendence )
				{
					if ( m_ScrollBinder.Deleted )
						return;

					ScrollofTranscendence sot = (ScrollofTranscendence)targeted;

					if ( m_ScrollBinder.ScrollType != ScrollTypes.None )
					{
						if ( ( m_ScrollBinder.ScrollType != ScrollTypes.ScrollofTranscendence ) || ( sot.Skill != m_ScrollBinder.Skill ) )
						{
							from.SendLocalizedMessage( 1113143 ); // This scroll does not match the type currently being bound.
							return;
						}
					}

					if ( sot.Value >= 5 )
					{
						from.SendLocalizedMessage( 1113144 ); // This scroll is already the highest of its type and cannot be bound.
						return;
					}

					if ( ( m_ScrollBinder.AmountGiven + sot.Value ) > 5.0 )
					{
						from.SendGump( new WarningGump( 1060637, 30720, 1113147, 30720, 320, 180, new WarningGumpCallback( OnConfirmCallback ), new object[]{ m_ScrollBinder, sot, pack } ) );
					}
					else
					{
						OnConfirmCallback( from, true, new object[]{ m_ScrollBinder, sot, pack } );
					}
				}
				else if( targeted is StatCapScroll )
				{
					if ( m_ScrollBinder.Deleted )
						return;

					StatCapScroll scs = (StatCapScroll)targeted;

					if ( m_ScrollBinder.ScrollType != ScrollTypes.None )
					{
						if ( ( m_ScrollBinder.ScrollType != ScrollTypes.StatCapScroll ) || ( scs.Value != m_ScrollBinder.Value ) )
						{
							from.SendLocalizedMessage( 1113143 ); // This scroll does not match the type currently being bound.
							return;
						}
					}

					if ( scs.Value == 250 )
					{
						from.SendLocalizedMessage( 1113144 ); // This scroll is already the highest of its type and cannot be bound.
						return;
					}

					if ( scs.Value == 230 )
						m_ScrollBinder.AmountNeeded = 6;
					else if ( scs.Value == 235 )
						m_ScrollBinder.AmountNeeded = 8;
					else if ( scs.Value == 240 )
						m_ScrollBinder.AmountNeeded = 8;
					else if ( scs.Value == 245 )
						m_ScrollBinder.AmountNeeded = 5;

					m_ScrollBinder.AmountGiven++;
					m_ScrollBinder.ScrollType = ScrollTypes.StatCapScroll;
					m_ScrollBinder.Value = scs.Value;
					scs.Delete();

					if ( m_ScrollBinder.AmountGiven >= m_ScrollBinder.AmountNeeded )
					{
						int scsValue = 230;

						if ( m_ScrollBinder.Value == 230 )
							scsValue = 235;
						else if ( m_ScrollBinder.Value == 235 )
							scsValue = 240;
						else if ( m_ScrollBinder.Value == 240 )
							scsValue = 245;
						else if ( m_ScrollBinder.Value == 245 )
							scsValue = 250;

						from.SendLocalizedMessage( 1113145 ); // You've completed your binding and received an upgraded version of your scroll!

						pack.DropItem( new StatCapScroll( scsValue ) );
						m_ScrollBinder.Delete();
					}
				}
				else
				{
					from.SendLocalizedMessage( 1113142 ); // You may only bind powerscrolls, stats scrolls or scrolls of transcendence.
				}
			}

			private void OnConfirmCallback( Mobile from, bool okay, object state )
			{
				if ( okay )
				{
					object[] states = (object[])state;
					ScrollBinder sb = (ScrollBinder)states[0];
					ScrollofTranscendence sot = (ScrollofTranscendence)states[1];
					Container pack = (Container)states[2];

					if ( ( pack == null ) || ( pack.Deleted ) )
					{
						from.SendLocalizedMessage( 1053057 ); // You do not have a backpack to place items in!
						return;
					}
					else if ( !sb.IsChildOf( from.Backpack ) || !sot.IsChildOf( from.Backpack ) )
					{
						from.SendLocalizedMessage( 1042664 ); // You must have the object in your backpack to use it.
						return;
					}

					sb.AmountNeeded = 5;
					sb.AmountGiven += sot.Value;
					sb.ScrollType = ScrollTypes.ScrollofTranscendence;
					sb.Skill = sot.Skill;
					sot.Delete();
	
					if ( sb.AmountGiven >= sb.AmountNeeded )
					{
						from.SendLocalizedMessage( 1113145 ); // You've completed your binding and received an upgraded version of your scroll!
	
						pack.DropItem( new ScrollofTranscendence( sb.Skill, 5 ) );
						sb.Delete();
					}
				}
			}

			protected override void OnTargetOutOfRange( Mobile from, object targeted )
			{
				from.SendLocalizedMessage( 502825 ); // That location is too far away
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 );

			writer.Write( (int) m_Skill );
			writer.Write( (double) m_Value );
			writer.Write( (double) m_AmountGiven );
			writer.Write( (int) AmountNeeded );
			writer.Write( (int) m_ScrollType );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadDouble();
					m_AmountGiven = reader.ReadDouble();
					AmountNeeded = reader.ReadInt();
					m_ScrollType = (ScrollTypes)reader.ReadInt();

					break;
				}
			}
		}
	}
}