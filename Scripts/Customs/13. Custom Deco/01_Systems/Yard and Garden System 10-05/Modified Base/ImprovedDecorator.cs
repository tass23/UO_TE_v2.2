using System;
using Server;
using Server.Network;
using Server.Regions;
using Server.Multis;
using Server.Gumps;
using Server.Targeting;

namespace Server.Items
{
	public enum Decorate2Command
	{
		None,
		North,
		East,
		South,
		West,
		Turn,
		Up,
		Down
	}

	public class ImprovedDecorator : Item
	{
		private Decorate2Command m_Command;

		[CommandProperty( AccessLevel.GameMaster )]
		public Decorate2Command Command{ get{ return m_Command; } set{ m_Command = value; InvalidateProperties(); } }

		[Constructable]
		public ImprovedDecorator() : base( 0xFC1 )
		{
			Weight = 1.0;
			LootType = LootType.Blessed;
			Name = "an Improved Decorator";
		}

		//public override int LabelNumber{ get{ return 1041280; } } // an interior decorator

		public ImprovedDecorator( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
            if ( m_Command != Decorate2Command.None )
                list.Add( 1018322 + (int)m_Command ); // Turn/Up/Down
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

		public override void OnDoubleClick( Mobile from )
		{
			if ( !CheckUse( this, from ) )
				return;

			if ( m_Command == Decorate2Command.None )
				from.SendGump( new InternalGump( this ) );
			else
				from.Target = new InternalTarget( this );
		}

		public static bool InHouse( Mobile from )
		{
			BaseHouse house = BaseHouse.FindHouseAt( from );

			return ( house != null && house.IsCoOwner( from ) );
		}

		public static bool CheckUse( ImprovedDecorator tool, Mobile from )
		{
			/*if ( tool.Deleted || !tool.IsChildOf( from.Backpack ) )
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			else*/
			if ( !InHouse( from ) )
				from.SendLocalizedMessage( 502092 ); // You must be in your house to do this.
			else
				return true;

			return false;
		}

		private class InternalGump : Gump
		{
			private ImprovedDecorator m_Decorator;

			public InternalGump( ImprovedDecorator decorator ) : base( 150, 50 )
			{
				m_Decorator = decorator;

				AddBackground( 0, 0, 250, 275, 5054 );

				AddButton( 20, 62, ( decorator.Command == Decorate2Command.Turn ? 1895 : 1896 ), 1895, 1, GumpButtonType.Reply, 0 );
				AddHtmlLocalized( 50, 67, 70, 40, 1018323, false, false ); // Turn

				AddButton( 20, 111, ( decorator.Command == Decorate2Command.Up ? 1895 : 1896 ), 1895, 2, GumpButtonType.Reply, 0 );
				AddHtmlLocalized( 50, 116, 70, 40, 1018324, false, false ); // Up

				AddButton( 20, 160, ( decorator.Command == Decorate2Command.Down ? 1895 : 1896 ), 1895, 3, GumpButtonType.Reply, 0 );
				AddHtmlLocalized( 50, 165, 70, 40, 1018325, false, false ); // Down

				AddButton( 210, 62, ( decorator.Command == Decorate2Command.North ? 1895 : 1896 ), 1895, 4, GumpButtonType.Reply, 0 );				
				AddHtmlLocalized( 135, 67, 70, 40, 1075389, false, false ); // north

				AddButton( 210, 111, ( decorator.Command == Decorate2Command.East ? 1895 : 1896 ), 1895, 5, GumpButtonType.Reply, 0 );
				AddHtmlLocalized( 135, 116, 70, 40, 1075387, false, false ); // east

				AddButton( 210, 160, ( decorator.Command == Decorate2Command.South ? 1895 : 1896 ), 1895, 6, GumpButtonType.Reply, 0 );
				AddHtmlLocalized( 135, 165, 70, 40, 1075386, false, false ); // south

				AddButton( 210, 209, ( decorator.Command == Decorate2Command.West ? 1895 : 1896 ), 1895, 7, GumpButtonType.Reply, 0 );
				AddHtmlLocalized( 135, 214, 70, 40, 1075390, false, false ); // west

			}

			public override void OnResponse( NetState sender, RelayInfo info )
			{
				Decorate2Command command = Decorate2Command.None;

				switch ( info.ButtonID )
				{
					case 1: command = Decorate2Command.Turn; break;
					case 2: command = Decorate2Command.Up; break;
					case 3: command = Decorate2Command.Down; break;
					case 4: command = Decorate2Command.North; break;
					case 5: command = Decorate2Command.East; break;
					case 6: command = Decorate2Command.South; break;
					case 7: command = Decorate2Command.West; break;
				}

				if ( command != Decorate2Command.None )
				{
					m_Decorator.Command = command;
					sender.Mobile.SendGump( new InternalGump( m_Decorator ) );
					sender.Mobile.Target = new InternalTarget( m_Decorator );
				}
				else
					Target.Cancel( sender.Mobile );
			}
		}

		private class InternalTarget : Target
		{
			private ImprovedDecorator m_Decorator;

			public InternalTarget( ImprovedDecorator decorator ) : base( -1, false, TargetFlags.None )
			{
				CheckLOS = false;

				m_Decorator = decorator;
			}

			protected override void OnTargetNotAccessible( Mobile from, object targeted )
			{
				OnTarget( from, targeted );
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted == m_Decorator )
				{
					m_Decorator.Command = Decorate2Command.None;
					from.SendGump( new InternalGump( m_Decorator ) );
				}
				else if ( targeted is Item && ImprovedDecorator.CheckUse( m_Decorator, from ) )
				{
					BaseHouse house = BaseHouse.FindHouseAt( from );
					Item item = (Item)targeted;
					
					bool isDecorableComponent = false;

					if ( item is AddonComponent )
						if ( ((AddonComponent)item).Addon.Components.Count == 1 && Core.SE )
							isDecorableComponent = true;

					if ( house == null || !house.IsCoOwner( from ) )
					{
						from.SendLocalizedMessage( 502092 ); // You must be in your house to do this.
					}
					else if ( item.Parent != null || !house.IsInside( item ) )
					{
						from.SendLocalizedMessage( 1042270 ); // That is not in your house.
					}
					else if ( !house.IsLockedDown( item ) && !house.IsSecure( item ) && (item.Movable))
					{
						if ( item is AddonComponent && m_Decorator.Command == Decorate2Command.Up )
							from.SendLocalizedMessage( 1042274 ); // You cannot raise it up any higher.
						else if ( item is AddonComponent && m_Decorator.Command == Decorate2Command.Down )
							from.SendLocalizedMessage( 1042275 ); // You cannot lower it down any further.
						else
							from.SendLocalizedMessage( 1042271 ); // That is not locked down.
					}
					else if ( item is VendorRentalContract )
					{
						from.SendLocalizedMessage( 1062491 ); // You cannot use the house decorator on that object.
					}
					else
					{
						switch ( m_Decorator.Command )
						{
							case Decorate2Command.Up:	Up( item, from );	break;
							case Decorate2Command.Down:	Down( item, from );	break;
							case Decorate2Command.Turn:	Turn( item, from );	break;
							case Decorate2Command.North:	North( item, from );	break;
							case Decorate2Command.East:		East( item, from );		break;
							case Decorate2Command.South:	South( item, from );	break;
							case Decorate2Command.West:		West( item, from );		break;
						}
					}
				}
			}
			
			private static void Turn( Item item, Mobile from )
			{
				FlipableAttribute[] attributes = (FlipableAttribute[])item.GetType().GetCustomAttributes( typeof( FlipableAttribute ), false );

				Item addon = null;
				
				if( attributes.Length > 0 )
					attributes[0].Flip( item );
				#region Heritage Items
				else if ( item is AddonComponent )
				{
					addon = ((AddonComponent) item).Addon;
				}
				else if ( item is AddonContainerComponent )
				{
					addon = ((AddonContainerComponent) item).Addon;
				}
				else if ( item is BaseAddonContainer )
				{
					addon = (BaseAddonContainer) item;
				}
				#endregion
				else
					from.SendLocalizedMessage( 1042273 ); // You cannot turn that.
				
				if ( addon != null )
				{
					FlipableAddonAttribute[] fattributes = (FlipableAddonAttribute[]) addon.GetType().GetCustomAttributes( typeof( FlipableAddonAttribute ), false );
					
					if ( fattributes.Length > 0 )
						fattributes[ 0 ].Flip( from, addon );
				}
			}

			private static void Up( Item item, Mobile from )
			{
				int floorZ = GetFloorZ( item );

				if ( floorZ > int.MinValue && item.Z < (floorZ + 15) ) // Confirmed : no height checks here
					item.Location = new Point3D( item.Location, item.Z + 1 );
				else
					from.SendLocalizedMessage( 1042274 ); // You cannot raise it up any higher.
			}

			private static void Down( Item item, Mobile from )
			{
				int floorZ = GetFloorZ( item );

				if ( floorZ > int.MinValue && item.Z > GetFloorZ( item ) )
					item.Location = new Point3D( item.Location, item.Z - 1 );
				else
					from.SendLocalizedMessage( 1042275 ); // You cannot lower it down any further.
			}
			private static void North( Item item, Mobile from )
			{
					item.Y = ( item.Y - 1 );
			}

			private static void East( Item item, Mobile from )
			{
					item.X = ( item.X + 1 );
			}

			private static void South( Item item, Mobile from )
			{
					item.Y = ( item.Y + 1 );
			}

			private static void West( Item item, Mobile from )
			{
					item.X = ( item.X - 1 );
			}

			#region Knives Townhouses
			private static int GetFloorZ( Item item )
			{
				Map map = item.Map;

				if ( map == null )
					return int.MinValue;

				StaticTile[] tiles = map.Tiles.GetStaticTiles( item.X, item.Y, true );

				int z = int.MinValue;

				for ( int i = 0; i < tiles.Length; ++i )
				{
					StaticTile tile = tiles[i];
					ItemData id = TileData.ItemTable[tile.ID & 0x3FFF];

					int top = tile.Z; // Confirmed : no height checks here

					if ( id.Surface && !id.Impassable && top > z && top <= item.Z )
						z = top;
				}

				if ( z == int.MinValue )
					z = map.Tiles.GetLandTile( item.X, item.Y ).Z;

				return z;
			}
			#endregion

			/*private static int GetFloorZ( Item item )
			{
				Map map = item.Map;

				if ( map == null )
					return int.MinValue;

				StaticTile[] tiles = map.Tiles.GetStaticTiles( item.X, item.Y, true );

				int z = int.MinValue;

				for ( int i = 0; i < tiles.Length; ++i )
				{
					StaticTile tile = tiles[i];
					ItemData id = TileData.ItemTable[tile.ID & 0x3FFF];

					int top = tile.Z; // Confirmed : no height checks here

					if ( id.Surface && !id.Impassable && top > z && top <= item.Z )
						z = top;
				}

				return z;
			}*/
		}
	}
}