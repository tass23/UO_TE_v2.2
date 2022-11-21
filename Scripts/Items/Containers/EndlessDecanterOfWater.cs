/*
 _______        __     __ __        _______ __                         
|       |.----.|  |--.|__|  |_     |     __|  |_.-----..----..--------.
|   -   ||   _||  _  ||  |   _|    |__     |   _|  _  ||   _||        |
|_______||__|  |_____||__|____|    |_______|____|_____||__|  |__|__|__|
  
*/

using System;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Engines.Plants;
using System.Collections;
using Server.Mobiles;
using Server.Engines.Quests;
using Server.Engines.Quests.Hag;
using Server.Engines.Quests.Matriarch;

namespace Server.Items
{
	public class EndlessDecanterOfWater : Pitcher
	{
		private Item m_Link;

		[CommandProperty( AccessLevel.GameMaster )]
		public Item Link
		{
			get{ return m_Link; }
			set{ m_Link = value; }
		}

		public override int MaxQuantity{ get{ return 40; } }

		[Constructable]
		public EndlessDecanterOfWater()
		{
			Name = "an endless decanter of water";
		}

		public EndlessDecanterOfWater( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsEmpty )
			{
				if ( !Fillable || !ValidateUse( from, true ) )
					return;

				from.BeginTarget( -1, true, TargetFlags.None, new TargetCallback( Link_OnTarget ) );
				from.SendLocalizedMessage( 1115892 ); //Target a water trough you wish to link.
			}
			else
			{
				base.OnDoubleClick( from );
			}
		}
		
		public static void Refill( Mobile from, EndlessDecanterOfWater edw )
		{
			if ( edw.Quantity == 0 )
			{
				if ( edw.Link != null )
				{
					if ( from.InRange( edw.Link.GetWorldLocation(), 20 ) )
					{
						edw.Quantity = 40;
						from.SendLocalizedMessage( 1115901 ); //The decanter has automatically been filled from the linked water trough.
					}
					else
					{
						from.SendLocalizedMessage( 1115972 ); //The decanter’s refill attempt failed because the linked water trough is not in the area.
					}
				}
				else
				{
					from.SendLocalizedMessage( 1115898 ); //The link between this decanter and the water trough has been removed.
				}
			}
		}

		public virtual void Link_OnTarget( Mobile from, object targ )
		{
			if ( targ is Item )
			{
				Item wt = targ as Item;

				if ( wt.ItemID == 2881 || wt.ItemID == 2882 || wt.ItemID == 2883 || wt.ItemID == 2884 )
				{
					this.Link = wt;

					this.Content = BeverageType.Water;
					this.Poison = null;
					this.Poisoner = null;
					this.Quantity = 40;

					from.SendLocalizedMessage( 1115899 ); //That water trough has been linked to this decanter.
				}
				else
				{
					from.SendLocalizedMessage( 1115900 ); //Invalid target. Please target a water trough.
				}
			}
			else
			{
				from.SendLocalizedMessage( 1115900 ); //Invalid target. Please target a water trough.
			}
		}

		public override void Pour_OnTarget( Mobile from, object targ )
		{
			base.Pour_OnTarget( from, targ );
			Refill( from, this );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );

			// Version 1
			writer.Write( (Item) m_Link );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch( version )
			{
				case 0:
				{
					m_Link = reader.ReadItem();
					break;
				}
			}
		}
	}
}