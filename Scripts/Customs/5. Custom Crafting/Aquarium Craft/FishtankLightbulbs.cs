using System;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class FishtankLightbulb150 : Item
	{
		[Constructable]
		public FishtankLightbulb150() : base( 0x1956 )
		{
			Name = "a 150 watt fishtank lightbulb";
			Hue = 1153;
			Weight = 1.0;
		}

		public FishtankLightbulb150( Serial serial ) : base( serial )
		{
		}
		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendMessage( "You need to have the lightbulb in your backpack to install it!" );
			}
			else
			{
				from.SendMessage( "Target the sand in your fishtank for correct lighting." );
				from.Target = new LightbulbTarget( this );
			}
		}
		private class LightbulbTarget : Target
		{
			private Item m_Item;

			private int m_idID;
			Item id = null;

			public LightbulbTarget( Item item ) : base( 3, false, TargetFlags.None )
			{
				m_Item = item;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Item.Deleted )
					return;

				else if ( targeted is AddonComponent )
				{
					m_idID = 4846;
					id = ( AddonComponent )targeted;

					if ( id.ItemID >= m_idID && id.ItemID <= m_idID )
					{
						bool IsSand = ( "sand" == id.Name );
						if ( IsSand )
						{
							id.Light = LightType.Circle150;
							m_Item.Delete();
							from.SendMessage( "You have changed the lightbulb in your fishtank." ); 
						}
						else
						{
							from.SendMessage( "You really do have to target the sand" );
						} 
					}
					else
					{
						from.SendMessage( "Please select the sand for proper lighting." );
					}					
				}
				else
				{
					from.SendMessage( "Please select the sand in your fishtank." );
				}
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
	public class FishtankLightbulb225 : Item
	{
		[Constructable]
		public FishtankLightbulb225() : base( 0x1956 )
		{
			Name = "a 225 watt fishtank lightbulb";
			Hue = 1153;
			Weight = 1.0;
		}

		public FishtankLightbulb225( Serial serial ) : base( serial )
		{
		}
		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendMessage( "You need to have the lightbulb in your backpack to install it!" );
			}
			else
			{
				from.SendMessage( "Target the sand in your fishtank for correct lighting." );
				from.Target = new LightbulbTarget( this );
			}
		}
		private class LightbulbTarget : Target
		{
			private Item m_Item;

			private int m_idID;
			Item id = null;

			public LightbulbTarget( Item item ) : base( 3, false, TargetFlags.None )
			{
				m_Item = item;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Item.Deleted )
					return;

				else if ( targeted is AddonComponent )
				{
					m_idID = 4846;
					id = ( AddonComponent )targeted;

					if ( id.ItemID >= m_idID && id.ItemID <= m_idID )
					{
						bool IsSand = ( "sand" == id.Name );
						if ( IsSand )
						{
							id.Light = LightType.Circle225;
							m_Item.Delete();
							from.SendMessage( "You have changed the lightbulb in your fishtank." ); 
						}
						else
						{
							from.SendMessage( "You really do have to target the sand" );
						}
					}
					else
					{
						from.SendMessage( "Please select the sand for proper lighting." );
					}					
				}
				else
				{
					from.SendMessage( "Please select the sand in your fishtank." );
				}
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
	public class FishtankLightbulb300 : Item
	{
		[Constructable]
		public FishtankLightbulb300() : base( 0x1956 )
		{
			Name = "a 300 watt fishtank lightbulb";
			Hue = 1153;
			Weight = 1.0;
		}

		public FishtankLightbulb300( Serial serial ) : base( serial )
		{
		}
		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendMessage( "You need to have the lightbulb in your backpack to install it!" );
			}
			else
			{
				from.SendMessage( "Target the sand in your fishtank for correct lighting." );
				from.Target = new LightbulbTarget( this );
			}
		}
		private class LightbulbTarget : Target
		{
			private Item m_Item;

			private int m_idID;
			Item id = null;

			public LightbulbTarget( Item item ) : base( 3, false, TargetFlags.None )
			{
				m_Item = item;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Item.Deleted )
					return;

				else if ( targeted is AddonComponent )
				{
					m_idID = 4846;
					id = ( AddonComponent )targeted;

					if ( id.ItemID >= m_idID && id.ItemID <= m_idID )
					{
						bool IsSand = ( "sand" == id.Name );
						if ( IsSand )
						{
							id.Light = LightType.Circle300;
							m_Item.Delete();
							from.SendMessage( "You have changed the lightbulb in your fishtank." ); 
						}
						else
						{
							from.SendMessage( "You really do have to target the sand" );
						}
					}
					else
					{
						from.SendMessage( "Please select the sand for proper lighting." );
					}					
				}
				else
				{
					from.SendMessage( "Please select the sand in your fishtank." );
				}
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
	public class FishtankLightbulb400 : Item
	{
		[Constructable]
		public FishtankLightbulb400() : base( 0x1956 )
		{
			Name = "a dim 400 watt fishtank lightbulb?";
			Hue = 1153;
			Weight = 1.0;
		}

		public FishtankLightbulb400( Serial serial ) : base( serial )
		{
		}
		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendMessage( "You need to have the lightbulb in your backpack to install it!" );
			}
			else
			{
				from.SendMessage( "Target the sand in your fishtank for correct lighting." );
				from.Target = new LightbulbTarget( this );
			}
		}
		private class LightbulbTarget : Target
		{
			private Item m_Item;

			private int m_idID;
			Item id = null;

			public LightbulbTarget( Item item ) : base( 3, false, TargetFlags.None )
			{
				m_Item = item;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Item.Deleted )
					return;

				else if ( targeted is AddonComponent )
				{
					m_idID = 4846;
					id = ( AddonComponent )targeted;

					if ( id.ItemID >= m_idID && id.ItemID <= m_idID )
					{
						bool IsSand = ( "sand" == id.Name );
						if ( IsSand )
						{
							id.Light = LightType.DarkCircle300;
							m_Item.Delete();
							from.SendMessage( "You have changed the lightbulb in your fishtank." ); 
						}
						else
						{
							from.SendMessage( "You really do have to target the sand" );
						}
					}
					else
					{
						from.SendMessage( "Please select the sand for proper lighting." );
					}					
				}
				else
				{
					from.SendMessage( "Please select the sand in your fishtank." );
				}
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