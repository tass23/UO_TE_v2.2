using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{				
	public class PerfectTimingQuest : BaseQuest
	{							
		/* Perfect Timing */
		public override object Title{ get{ return 1112870; } }
		
        /* Presumptuous, are we? You think i will just let you get your grubby hands on my clever inventions!
           I think not! If you want to learn how to create these wonders of mechanical life, you will have
           to prove yourself. Correctly combine the required ingredients to build one of my inventions in
           a timely manner and I might share my secrets with you. */
		public override object Description{ get{ return 1112873; } }
		
        /* I'm not surprised. *disdainful snort*  People with both manual and mental dexterity come
           in short supply. Move along then. Science does not wait for anyone. */
		public override object Refuse{ get{ return 1112875; } }
		
        /* Give your assembly the material it requests. You'll find everything lying around here.
           Just use it. But be quick! */
		public override object Uncomplete{ get{ return 1112877; } }
		
		/* There's more to you than meets the eye after all! Well done! You should enjoy this copy of my manual. */
		public override object Complete{ get{ return 1112878; } }
	
		public PerfectTimingQuest() : base()
		{			
			AddObjective( new ObtainObjective( typeof( CompletedClockworkAssembly ), "completed clockwork assembly", 1, 0x1727 ) );
			
			AddReward( new BaseReward( typeof( MechanicalLifeManual ), 1072706 ) );
		}
		
		public override void OnAccept()
		{
            base.OnAccept();

			Owner.AddToBackpack( new ClockworkMechanism() );
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
	
	public class Sutek : MondainQuester
	{
		public override Type[] Quests{ get{ return new Type[] 
		{ 
			typeof( PerfectTimingQuest )
		}; } }
		
		[Constructable]
		public Sutek() : base( "Sutek", "the Mage" )
		{			
		}
		
		public Sutek( Serial serial ) : base( serial )
		{
		}
		
		public override void InitBody()
		{
			InitStats( 100, 100, 25 );
			
			Female = true;
			Race = Race.Human;
			
			Hue = 0x8418;
			HairItemID = 0x2046;
			HairHue = 0x466;
		}
		
		public override void InitOutfit()
		{
			AddItem( new Backpack() );
			AddItem( new Shoes( 0x743 ) );
			AddItem( new Robe( 0x485 ) );
		}
		
		public override void Advertise()
		{
			Say( Utility.RandomBool() ? 1113228 : 1113236 );
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