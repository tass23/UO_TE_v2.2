
using System;
using Server;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Commands;


namespace Server.Commands 
{
    public class QuestBackpack
	{		
 		public static void Initialize() 
   		 {
             CommandSystem.Register("QuestBackpack", AccessLevel.Player, new CommandEventHandler(QuestBackpack_OnCommand));
         }

        [Usage("QuestBackpack")]
		[Description( "Get a Questbook Backpack" )]
        public static void QuestBackpack_OnCommand(CommandEventArgs e) 
    		{ 
                        e.Mobile.AddToBackpack(new QuestBookBag());
		    }
	}
}