using System;
using Server;
using Server.Items;
using Server.Engines.Quests;
using Reward = Server.Engines.Quests.BaseReward;
using Server.Engines.Craft;

namespace Server.Items
{
	public class ScrollBox : WoodenBox
	{

                public override string DefaultName
		{
			get { return "Reward Scroll Box"; }
		}

                private static void PlaceItemIn( Container parent, int x, int y, Item item ) 
                { 
                   parent.AddItem( item ); 
                   item.Location = new Point3D( x, y, 0 ); 
                } 
		
		[Constructable]	
		public ScrollBox() : base()
		{
                    Movable = true;
                    Hue = 1151;

                    PlaceItemIn( this, 45, 66, new PowerScroll( SkillName.Imbuing, 115.0 ) ); 
       	
                }
		
		public ScrollBox( Serial serial ) : base( serial )
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