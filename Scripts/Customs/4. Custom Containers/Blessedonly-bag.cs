//Blessed Bag Scripted by Datguy with help from vermillion2083 & Lokai
//2007

using System;
using Server;
using Server.Mobiles;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Engines.Craft;

namespace Server.Items
{
    public class BlessedonlyBag : Container
    {
        [Constructable]
        public BlessedonlyBag() : base( 0x9b2 )
        {
                
        	Name = "A Blessed item Bag";
           	Weight = 0.0;
            Hue = 1170;
            LootType = LootType.Blessed;
            
        }
		
		public override bool OnDragDrop( Mobile from, Item dropped )
		{

 		if ( dropped.LootType == LootType.Blessed ) 
		  return base.OnDragDrop(from, dropped);
  
		else if ( dropped.LootType == LootType.Newbied )		 
			 return base.OnDragDrop(from, dropped);

		else
			{
			from.SendMessage( "That item is not blessed!" );
			return false;	
			}
		}

		public override bool OnDragDropInto( Mobile from, Item item, Point3D p )
		{

 		if ( item.LootType == LootType.Blessed )
		 return base.OnDragDropInto(from, item, p);
	  
		else 
		if ( item.LootType == LootType.Newbied )
		return base.OnDragDropInto(from, item, p);
		
		else
			{
			from.SendMessage( "That item is not blessed!" );
			return false;	
			}
		}

     		public BlessedonlyBag(Serial serial) : base(serial)
   		{
   		}

       public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

     
    }
}