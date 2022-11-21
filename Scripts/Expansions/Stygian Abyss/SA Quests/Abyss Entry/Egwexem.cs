using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
	public class RumorsAboundQuest : BaseQuest
	{			
		public override TimeSpan RestartDelay{ get{ return TimeSpan.FromHours( 12 ); } }
        public override bool DoneOnce { get { return true; } }
		
		/* Rumors Abound */
		public override object Title{ get{ return 1112514; } }
		
		
		public override object Description{ get{ return 1112515; } }
		
		
		public override object Refuse{ get{ return 1112516; } }
		
		
		public override object Uncomplete{ get{ return "You never spoke to Naxatillor yet! Go to him!"; } }/////
		
		
		public override object Complete{ get{ return 1112518; } }/////
		
		public RumorsAboundQuest() : base()
		{					
			AddObjective( new DeliverObjective( typeof( EgwexemWrit ), "Egwexem's Writ", 1, typeof( Naxatillor ), "Naxatillor" ) );

            AddReward(new BaseReward(1112731)); 
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
	
	public class Egwexem : MondainQuester
	{
		public override Type[] Quests
		{ 
			get{ return new Type[] 
			{ 
				typeof( RumorsAboundQuest )
			};} 
		}	
		
		[Constructable]
		public Egwexem() : base( "Egwexem", "the Noble" )
		{
		}
		
		public override void InitBody()
		{
			InitStats( 100, 100, 25 );
			
			Female = false;
			CantWalk = true;
            Body = 666;
			HairItemID = 16987;
			HairHue = 1801;
		}
		
		public override void InitOutfit()
		{
			AddItem( new Backpack() );
            AddItem(new GargishClothChest());
            AddItem(new GargishClothKilt());
            AddItem(new GargishClothLegs(Utility.RandomNeutralHue()));
		}

        public Egwexem(Serial serial) : base(serial)
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
	
	public class EgwexemWrit : Item
	{
        public override int LabelNumber { get { return 1112520; } } 
		
		[Constructable]
		public EgwexemWrit() : base(0x0E34)
		{
            //Hue = 3;
		}
		
		public EgwexemWrit( Serial serial ) : base( serial )
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