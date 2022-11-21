/*    
-<>>--<<>>--<0>>--<< <<2005>> >>--<<0>--<<>>--<<>-
|        ____________________________            |
|     -=(_)__________________________)=-         |
|          \_    All Crafts 1.0.0    _\          |
|           \_  -------------------   _\         |
|            )       Created By:        )        |
|           /_  Sirsly & Lucid Nagual _/         |
|         _/__________________________/          |
|      -=(_)__________________________)=-        |
|                                                |
-<>>-<< Based off of Daat99's OWLTR system >>-<<>-
*/
using System;
using Server;
using Server.Targeting;
using Server.Engines.Craft;
using Server.Network;


namespace Server.Items
{
	
	public class StainOfDurability : Item, IUsesRemaining
	{
		private int m_UsesRemaining;

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		public bool ShowUsesRemaining{ get{ return true; } set{} }

		[Constructable]
		public StainOfDurability() : this( 20 )
		{
               	}

		[Constructable]
		public StainOfDurability( int charges ) : base( 6215 )
		{
			Weight = 1.0;
			Hue = 1517;
			UsesRemaining = charges;
                        Name = "stain of durability";
		}

		public StainOfDurability( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
			writer.Write( (int) m_UsesRemaining );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_UsesRemaining = reader.ReadInt();
					break;
				}
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060584, m_UsesRemaining.ToString() ); // uses remaining: ~1_val~
		}

		public virtual void DisplayDurabilityTo( Mobile m )
		{
			LabelToAffix( m, 1017323, AffixType.Append, ": " + m_UsesRemaining.ToString() ); // Durability
		}

		public override void OnSingleClick( Mobile from )
		{
			DisplayDurabilityTo( from );

			base.OnSingleClick( from );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
				from.Target = new InternalTarget( this );
			else
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
		}

		private class InternalTarget : Target
		{
			private StainOfDurability m_Powder;

			public InternalTarget( StainOfDurability powder ) : base( 2, false, TargetFlags.None )
			{
				m_Powder = powder;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Powder.Deleted || m_Powder.UsesRemaining <= 0 )
				{
					from.SendMessage( "You have used up your stain of durability." ); // You have used up your powder of temperament.
					return;
				}

				if ( targeted is BaseWeapon && (DefBowFletching.CraftSystem.CraftItems.SearchForSubclass( targeted.GetType() ) != null) )
				{
					BaseWeapon wep = (BaseWeapon)targeted;

					if ( wep.IsChildOf( from.Backpack ) && m_Powder.IsChildOf( from.Backpack ) )
					{
						int origMaxHP = wep.MaxHitPoints;
						int origCurHP = wep.HitPoints;

						wep.UnscaleDurability();

						if ( wep.MaxHitPoints < wep.InitMaxHits )
						{
							int bonus = wep.InitMaxHits - wep.MaxHitPoints;

							if ( bonus > 25 )
								bonus = 25;

							wep.MaxHitPoints += bonus;
							wep.HitPoints += bonus;

							wep.ScaleDurability();

							if ( wep.MaxHitPoints > origMaxHP )
							{
								from.SendMessage( "You have successfully used the stain on that item." ); // You successfully use the powder on the item.

								--m_Powder.UsesRemaining;

								if ( m_Powder.UsesRemaining <= 0 )
								{
									from.SendMessage( "You have used up your stain of durability." ); // You have used up your powder of temperament.
									m_Powder.Delete();
								}
							}
							else
							{
								wep.MaxHitPoints = origMaxHP;
								wep.HitPoints = origCurHP;
								from.SendLocalizedMessage( 1049085 ); // The item cannot be improved any further.
							}
						}
						else
						{
							from.SendLocalizedMessage( 1049085 ); // The item cannot be improved any further.
							wep.ScaleDurability();
						}
					}
					else
					{
						from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
					}
				}
				else
				{
					from.SendMessage( "You cannot use the stain on that item." ); // You cannot use the powder on that item.
				}
				if ( targeted is BaseWeapon && (DefCarpentry.CraftSystem.CraftItems.SearchForSubclass( targeted.GetType() ) != null) )
				{
					BaseWeapon wep = (BaseWeapon)targeted;

					if ( wep.IsChildOf( from.Backpack ) && m_Powder.IsChildOf( from.Backpack ) )
					{
						int origMaxHP = wep.MaxHitPoints;
						int origCurHP = wep.HitPoints;

						wep.UnscaleDurability();

						if ( wep.MaxHitPoints < wep.InitMaxHits )
						{
							int bonus = wep.InitMaxHits - wep.MaxHitPoints;

							if ( bonus > 25 )
								bonus = 25;

							wep.MaxHitPoints += bonus;
							wep.HitPoints += bonus;

							wep.ScaleDurability();

							if ( wep.MaxHitPoints > origMaxHP )
							{
								from.SendMessage( "You have successfully used the stain on that item." ); // You successfully use the powder on the item.

								--m_Powder.UsesRemaining;

								if ( m_Powder.UsesRemaining <= 0 )
								{
									from.SendMessage( "You have used up your stain of durability." ); // You have used up your powder of temperament.
									m_Powder.Delete();
								}
							}
							else
							{
								wep.MaxHitPoints = origMaxHP;
								wep.HitPoints = origCurHP;
								from.SendLocalizedMessage( 1049085 ); // The item cannot be improved any further.
							}
						}
						else
						{
							from.SendLocalizedMessage( 1049085 ); // The item cannot be improved any further.
							wep.ScaleDurability();
						}
					}
					else
					{
						from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
					}
				}
				else
				{
					from.SendMessage( "You cannot use the stain on that item." ); // You cannot use the powder on that item.
				}
			}
		}
	}
}
