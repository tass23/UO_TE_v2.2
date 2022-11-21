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
	public class TrickColumn : BaseLight 
	{
		private DateTime m_NextUse;
		public override int LitItemID{ get { return 0x45AC; } }
		public override int UnlitItemID{ get { return 0x45AB; } }
		
		[Constructable] 
		public TrickColumn() : base (0x45AB)
		{
			Name = "The Trick Column of Dexterity";
			Hue = 1788;
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
				if ( this.ItemID == 0x45AB && house.IsCoOwner( from ) )
				{
					this.ItemID = 0x45AC;
					this.Hue = 1787;
					from.SendMessage( 0, "You activate the Trick Column of Dexterity." );
					from.PlaySound( 84 );
				}
				else if ( this.ItemID == 0x45AC && house.IsCoOwner( from ))
				{
					this.ItemID = 0x45AB;
					this.Hue = 1788;
					from.PlaySound( 958 );
					from.SendMessage( 0, "You deactivate the Trick Column of Dexterity." );
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
			if ( this.ItemID == 0x45AC )
				return true;
			return false;
		}

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( IsLockedDown && this.ItemID == 0x45AC && m_NextUse < DateTime.Now && m.InRange( this.Location, 2 ))
			{
				m.SendMessage( 0, "The Trick Column of Dexterity." );
				m.AddStatMod( new StatMod( StatType.Dex, "Trick Column of Dexterity", 25, TimeSpan.FromMinutes( 10.0 )));
				m_NextUse = DateTime.Now + TimeSpan.FromSeconds( 15.0 );
			}

			base.OnMovement( m, oldLocation );
		}
		public TrickColumn( Serial serial ) : base( serial ) 
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