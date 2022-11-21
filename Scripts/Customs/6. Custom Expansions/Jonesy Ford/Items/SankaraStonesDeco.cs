using Server;
using Server.Items;
using Server.Multis;
using System.Collections.Generic;
using Server.Network;
using System;

namespace Server.Items
{ 
	public class SankaraStonesDeco : BaseLight 
	{
		private DateTime m_NextUse;
		public override int LitItemID{ get { return 0x573B; } }
		public override int UnlitItemID{ get { return 0x5737; } }
		
		[Constructable] 
		public SankaraStonesDeco() : base (0x5737)
		{
			Name = "The Sacred Sankara Stones";
			Hue = 1261;
			Light = LightType.Circle150;
			Weight = 5.0;
		} 
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( this.GetWorldLocation(), 2 ) )
			{
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			}
			
			else if ( IsLockedDown )
			{
				BaseHouse house = BaseHouse.FindHouseAt( from );
				if ( this.ItemID == 0x5737 && house.IsCoOwner( from ) )
				{
					this.ItemID = 0x573B;
					this.Hue = 1266;
					from.SendMessage( 0, "You have activated the Sankara Stones." );
					from.PlaySound( 0x0FE );
				}
				else if ( this.ItemID == 0x573B && house.IsCoOwner( from ))
				{
					this.ItemID = 0x5737;
					this.Hue = 1261;
					from.PlaySound( 0x1D6 );
					from.SendMessage( 0, "You have deactivated the Sankara Stones." );
				}
				else
					from.SendLocalizedMessage( 502436 ); // That is not accessible.
			}
			else
				from.SendMessage( 0, "You must lock this down in your house to use it." );
		}

		public override bool HandlesOnMovement{ get{ return IsLockedDown && IsOn(); } }
		public bool IsOn()
		{
			if ( this.ItemID == 0x573B )
				return true;
			return false;
		}

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( IsLockedDown && this.ItemID == 0x573B && m_NextUse < DateTime.Now && m.InRange( this.Location, 2 ))
			{
				m.SendMessage( 0, "The Sankara Stones lend you Strength, Intelligence and Dexterity." );
				m.AddStatMod( new StatMod( StatType.All, "Sankara Stones", 25, TimeSpan.FromMinutes( 10.0 )));
				m_NextUse = DateTime.Now + TimeSpan.FromSeconds( 120.0 );
			}
			base.OnMovement( m, oldLocation );
		}

		public SankaraStonesDeco( Serial serial ) : base( serial ) 
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