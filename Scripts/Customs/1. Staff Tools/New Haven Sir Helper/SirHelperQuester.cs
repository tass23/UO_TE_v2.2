using System; 
using Server; 
using System.Collections.Generic;
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.ContextMenus;

namespace Server.Mobiles
{

	public class SirHelper : BaseVendor
	{
        private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }
		public override bool IsActiveVendor{ get{ return false; } }
		public override bool DisallowAllMoves{ get{ return true; } }
		public override bool ClickTitle{ get { return false; } }
		public override bool CanTeach{ get{ return false; } }
		
		public override void InitSBInfo()
		{
		}
		[Constructable]
		public SirHelper(): base( "the Profession Guide" )
		{	
		}
		public override void InitBody()
		{
			InitStats( 100, 100, 25 );
			
			Female = false;
			CantWalk = false;
			Race = Race.Human;
			
			Hue = 0x8407;			
			HairItemID = 0x203C;
			HairHue = 250;

			Name = "Sir Helper";
			NameHue = 68;
			
			this.Blessed = true;
		}
		
		public override void InitOutfit()
		{
			AddItem( new Backpack() );			
			AddItem( new Sandals() );
						
			Item item;
			
			item = new Robe();
			item.Hue = 3;
			AddItem( item );
			
			item = new Cloak();
			item.Hue = 1175;
			AddItem( item );
			
		}
		
		public override void OnDoubleClick(Mobile from)
        {
            from.CloseGump(typeof(ProfessionGump));
            from.SendGump(new ProfessionGump(this) );

        }
		
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
	    { 
	        base.GetContextMenuEntries( from, list ); 
        	list.Add( new AmserEntry( from, this ) ); 
	    }
		
		public class AmserEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public AmserEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
                if( !( m_Mobile is PlayerMobile ) )
					return;
				
				PlayerMobile mobile = (PlayerMobile) m_Mobile;

				{
					if ( ! mobile.HasGump( typeof( ProfessionGump ) ) )
					{
						mobile.SendGump( new ProfessionGump( mobile ));
					} 
				}
			}
		}
			
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060668 ); // INFORMATION
		}
		
		public SirHelper( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		} 

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

	}
}
