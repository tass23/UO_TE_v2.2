using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{	
	public class MasteringtheSoulforge : BaseQuest
	{			   
                public override bool DoneOnce{ get{ return true; } }

		public override object Title{ get{ return "Mastering the Soulforge"; } }
		
		public override object Description{ get{ return 1112529; } }
		
		public override object Refuse{ get{ return 1112549; } }
		
		public override object Uncomplete{ get{ return 1112550; } }

		public MasteringtheSoulforge() : base()
		{								
			AddObjective(new ObtainObjective(typeof(RelicFragment), "Relic Fragments", 50, 0x2DB3));
                       						
			AddReward( new BaseReward( typeof( ScrollBox2 ), "Knowledge" ) );
		}		
		
                public override object Complete{ get{ return 1112551; } }
				
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
