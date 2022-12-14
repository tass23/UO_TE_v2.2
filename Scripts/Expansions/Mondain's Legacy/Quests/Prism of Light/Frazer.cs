using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{	
	public class InTheBellyOfTheBeastQuest : BaseQuest
	{				
		/* In the Belly of the Beast */
		public override object Title{ get{ return 1073049; } }
		
		/* Oh, the trauma!  *weeps loudly*  My lucky dagger has been lost.  It was given to me by my father, as a 
		final gift before he died.  That blade has been an heirloom of my family for generations.  I must have it 
		back.  *sniffles pathetically*  Please, find my lucky dagger. */	
		public override object Description{ get{ return 1074658; } }
		
		/* *wailing cries* Then begone if you will not help a poor man in need. */
		public override object Refuse{ get{ return 1074659; } }
		
		/* *sniffles*  The dagger was stolen by some dishonest man.  Or perhaps I dropped it.  That doesn't matter 
		though.  All that matters is that you find my dagger and return it. */
		public override object Uncomplete{ get{ return 1074660; } }
		
		/* You've found it?  My lucky dagger! */
		public override object Complete{ get{ return 1074661; } }
	
		public InTheBellyOfTheBeastQuest() : base()
		{			
			AddObjective( new ObtainObjective( typeof( LuckyDagger ), "lucky dagger", 1 ) );
			
			AddReward( new BaseReward( typeof( SmithsCraftsmanSatchel ), 1074282 ) );
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

	public class Frazer : MondainQuester
	{		
		public override Type[] Quests
		{ 
			get{ return new Type[] 
			{ 
				typeof( InTheBellyOfTheBeastQuest ),
			};} 
		}

        private List<SBInfo> m_SBInfos = new List<SBInfo>();
        protected override List<SBInfo> SBInfos { get { return m_SBInfos; } }
		
		[Constructable]
		public Frazer() : base( "Frazer", "the vagabond" )
		{			
			SetSkill( SkillName.ItemID, 64.0, 100.0 );
		}
		
		public Frazer( Serial serial ) : base( serial )
		{
		}		
		
		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBJewel() );
			m_SBInfos.Add( new SBTinker() );
		}
		
		public override void InitBody()
		{
			InitStats( 100, 100, 25 );
			
			Female = false;
			Race = Race.Human;
			
			Hue = 0x840F;
			HairItemID = 0x204A;
			HairHue = 0x45A;
			FacialHairItemID = 0x204D;
			FacialHairHue = 0x45A;
		}
		
		public override void InitOutfit()
		{
			AddItem( new Shoes( 0x735 ) );
			AddItem( new LongPants( 0x4C0 ) );
			AddItem( new FancyShirt( 0x3 ) );
			AddItem( new JesterHat( 0x74A ) );
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