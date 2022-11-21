using System;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.ContextMenus;
using Server.Engines.RewardSystem;

namespace Server.Items
{	
	public class BloodAnkhComponent : AddonComponent
	{		
		public override bool ForceShowProperties{ get{ return ObjectPropertyList.Enabled; } }

		public BloodAnkhComponent( int itemID ) : base( itemID )
		{			
		}

		public BloodAnkhComponent( Serial serial ) : base( serial )
		{
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
						
			if ( from is PlayerMobile )
				list.Add( new LockKarmaEntry( (PlayerMobile)from, Addon as BloodAnkhAddon ) );

			list.Add( new ResurrectEntry( from, Addon as BloodAnkhAddon ) );
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

		public static void Resurrect( PlayerMobile m, BloodAnkhAddon ankh )
		{
			if ( m == null )
			{
			}
			else if ( !m.InRange( ankh.GetWorldLocation(), 2 ) )
			{
				m.SendLocalizedMessage( 500446 ); // That is too far away.
			}
			else if ( m.Alive )
			{
				m.SendLocalizedMessage( 1060197 ); // You are not dead, and thus cannot be resurrected!
			}
			else if ( m.AnkhNextUse > DateTime.Now )
			{			
				TimeSpan delay = m.AnkhNextUse - DateTime.Now;

				if ( delay.TotalMinutes > 0 )
					m.SendLocalizedMessage( 1079265, Math.Round( delay.TotalMinutes ).ToString() ); // You must wait ~1_minutes~ minutes before you can use this item.
				else
					m.SendLocalizedMessage( 1079263, Math.Round( delay.TotalSeconds ).ToString() ); // You must wait ~1_seconds~ seconds before you can use this item.		
			}
			else
			{
				m.CloseGump( typeof( AnkhResurrectGump ) );
				m.SendGump( new AnkhResurrectGump( m, ResurrectMessage.VirtueShrine ) );
			}
		}

		private class ResurrectEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private BloodAnkhAddon m_Ankh;

			public ResurrectEntry( Mobile mobile, BloodAnkhAddon ankh ) : base( 6195, 2 )
			{
				m_Mobile = mobile;
				m_Ankh = ankh;
			}

			public override void OnClick()
			{
				if ( m_Ankh == null || m_Ankh.Deleted )
					return;

				Resurrect( m_Mobile as PlayerMobile, m_Ankh );
			}
		}

		private class LockKarmaEntry : ContextMenuEntry
		{
			private PlayerMobile m_Mobile;
			private BloodAnkhAddon m_Ankh;

			public LockKarmaEntry( PlayerMobile mobile, BloodAnkhAddon ankh ) : base( mobile.KarmaLocked ? 6197 : 6196, 2 )
			{
				m_Mobile = mobile;
				m_Ankh = ankh;
			}

			public override void OnClick()
			{
				if ( !m_Mobile.InRange( m_Ankh.GetWorldLocation(), 2 ) )
					m_Mobile.SendLocalizedMessage( 500446 ); // That is too far away.
				else
				{
					m_Mobile.KarmaLocked = !m_Mobile.KarmaLocked;

					if ( m_Mobile.KarmaLocked )
						m_Mobile.SendLocalizedMessage( 1060192 ); // Your karma has been locked. Your karma can no longer be raised.
					else
						m_Mobile.SendLocalizedMessage( 1060191 ); // Your karma has been unlocked. Your karma can be raised again.
				}
			}
		}

		private class AnkhResurrectGump : ResurrectGump
		{
			public AnkhResurrectGump( Mobile owner, ResurrectMessage msg ) : base( owner, owner, msg, false )
			{
			}

			public override void OnResponse( NetState state, RelayInfo info )
			{
				Mobile from = state.Mobile;

				if( info.ButtonID == 1 || info.ButtonID == 2 )
				{
					if( from.Map == null || !from.Map.CanFit( from.Location, 16, false, false ) )
					{
						from.SendLocalizedMessage( 502391 ); // Thou can not be resurrected there!
						return;
					}

					if ( from is PlayerMobile )
					{
						((PlayerMobile) from).AnkhNextUse = DateTime.Now + TimeSpan.FromHours( 1 );
					}

					base.OnResponse( state, info );
				}
			}
		}
	}
	public class BloodAnkhAddon : BaseAddon
	{
		public override bool HandlesOnMovement{ get{ return true; } }

		public override BaseAddonDeed Deed
		{ 
			get
			{ 
				BloodAnkhDeed deed = new BloodAnkhDeed();

				return deed; 
			} 
		}		
		
		[Constructable]
		public BloodAnkhAddon( bool east ) : base()
		{
			if ( east )
			{
				Name = "Blood Ankh";
				
				AddComponent( new BloodAnkhComponent( 0x0003 ), 0, 0, 0 );
				AddComponent( new BloodAnkhComponent( 0x0002 ), 0, 1, 0 );
				AddComponent( new BloodAnkhComponent( 0x1CD6 ), 1, 0, 0 );
				AddComponent( new BloodAnkhComponent( 0x1CD4 ), 1, 1, 0 );
				AddComponent( new BloodAnkhComponent( 0x1CD0 ), 2, 0, 0 );
				AddComponent( new BloodAnkhComponent( 0x1CCE ), 2, 1, 0 );
			}
			else
			{
				Name = "Blood Ankh";
				
				AddComponent( new BloodAnkhComponent( 0x0004 ), 0, 0, 0 );
				AddComponent( new BloodAnkhComponent( 0x0005 ), 1, 0, 0 );
				AddComponent( new BloodAnkhComponent( 0x1CD2 ), 0, 1, 0 );
				AddComponent( new BloodAnkhComponent( 0x1CD8 ), 1, 1, 0 );
				AddComponent( new BloodAnkhComponent( 0x1CCD ), 0, 2, 0 );
				AddComponent( new BloodAnkhComponent( 0x1CCE ), 1, 2, 0 );
			}
		}

		public BloodAnkhAddon( Serial serial ) : base( serial )
		{
		}

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( !m.Alive && Utility.InRange( Location, m.Location, 1 ) && !Utility.InRange( Location, oldLocation, 1 ) )
				BloodAnkhComponent.Resurrect( m as PlayerMobile, this );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
			
		}
            
        public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
			
		}
	}	
	
	public class BloodAnkhDeed : BaseAddonDeed, IRewardOption
	{
		public override BaseAddon Addon
		{ 
			get
			{ 
				BloodAnkhAddon addon = new BloodAnkhAddon( m_East );

				return addon; 
			} 
		}
		
		private bool m_East;

		[Constructable]
		public BloodAnkhDeed() : base()
		{
		
			Name = "Blood Ankh Deed";
		
			LootType = LootType.Blessed;

		}

		public BloodAnkhDeed( Serial serial ) : base( serial )
		{
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			
			if ( IsChildOf( from.Backpack ) )
			{
				from.CloseGump( typeof( RewardOptionGump ) );
				from.SendGump( new RewardOptionGump( this ) );
			}
			else
				from.SendLocalizedMessage( 1062334 ); // This item must be in your backpack to be used.   
		}
		
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}

		public void GetOptions( RewardOptionList list )
		{
			list.Add( 1, 1122612 ); // Ankh of Sacrifice South
			list.Add( 2, 1122613 ); // Ankh of Sacrifice East
		}

		public void OnOptionSelected( Mobile from, int option )
		{
			switch ( option )
			{
				case 1: m_East = false; break;
				case 2: m_East = true; break;
			}

			if ( !Deleted )
				base.OnDoubleClick( from );
		}
	}
}
