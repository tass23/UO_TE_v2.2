using System;
using System.Collections;
using Server.Multis;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.Spells;

namespace Server.Items
{ 
	public class Ark : BaseLight
	{
		private DateTime m_NextUse;
		public override int LitItemID{ get { return 10936; } }
		public override int UnlitItemID{ get { return 3645; } }
		
		[Constructable] 
		public Ark() : base (3645)
		{
			Name = "The Ark of the Covenant";
			Hue = 1591;
			Light = LightType.Circle300;
			Weight = 15.0;
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

				if ( this.ItemID == 3645 && house.IsCoOwner( from ) )
				{
					this.ItemID = 10936;
					this.Hue = 1591;
					from.SendMessage( 0, "You activate the Ark." );
					from.PlaySound( 84 );
				}
				else if ( this.ItemID == 10936 && house.IsCoOwner( from ))
				{
					this.ItemID = 3645;
					this.Hue = 1591;
					from.PlaySound( 958 );
					from.SendMessage( 0, "You deactivate the Ark." );
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
			if ( this.ItemID == 10936 )
				return true;
			return false;
		}

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( IsLockedDown && this.ItemID == 10936 && m_NextUse < DateTime.Now && m.InRange( this.Location, 2 ))
			{
				m.SendMessage( 0, "The Ark of the Covenant lends you Intelligence." );
				m.AddStatMod( new StatMod( StatType.Int, "Ark of the Covenant", 25, TimeSpan.FromMinutes( 10.0 )));
				m_NextUse = DateTime.Now + TimeSpan.FromSeconds( 15.0 );
			}

			base.OnMovement( m, oldLocation );
		}
		public Ark( Serial serial ) : base( serial ) 
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