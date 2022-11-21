using System;
using Server;
using Server.Items;
using Server.Engines.Quests;
using Reward = Server.Engines.Quests.BaseReward;
using Server.Engines.Craft;

namespace Server.Items
{
	public class ScrollBox2 : WoodenBox
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
		public ScrollBox2() : base()
		{
                    Movable = true;
                    Hue = 1266;

                    PlaceItemIn( this, 45, 66, new PowerScroll( SkillName.Imbuing, 120.0 ) ); 
       	
                }
		
		public ScrollBox2( Serial serial ) : base( serial )
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