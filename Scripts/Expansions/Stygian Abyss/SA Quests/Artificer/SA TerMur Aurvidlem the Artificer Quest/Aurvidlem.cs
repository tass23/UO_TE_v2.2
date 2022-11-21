using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Engines.Craft;
using System.Collections.Generic;
using Server.Network;
using Server.ContextMenus;

namespace Server.Engines.Quests
{	
       public class Aurvidlem : MondainQuester
       {
                public override Type[] Quests{ get{ return new Type[] 
		{ 
			typeof( KnowledgeoftheSoulforge )
		}; } } 
 
	[Constructable]
	public Aurvidlem() : base("Aurvidlem", "the Artificer")
	{	
		SetSkill( SkillName.Imbuing, 60.0, 80.0 );
	}
		
        public override void InitBody()
	{		
	  	HairItemID = 0x2044;//
                HairHue = 1153;
                FacialHairItemID = 0x204B;
                FacialHairHue = 1153;
                Body = 666;            
                Blessed = true;
	}
		
	public override void InitOutfit()
	{	
		AddItem( new Backpack() );		
		AddItem( new Boots() );
		AddItem( new LongPants( 0x6C7 ) );
		AddItem( new FancyShirt( 0x6BB ) );
		AddItem( new Cloak( 0x59 ) );		
	}

        public override void Advertise()
	{
		Say( 1112525 );  // Come to be Artificer. I have a task for you. 
	}

	public Aurvidlem( Serial serial ) : base( serial )
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