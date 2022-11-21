using System;
using Server;

namespace Server.Items
{
	public class CBookExpanseCommands : BaseBook
	{
		public static readonly BookContent Content = new BookContent
		(
			"Expanse Commands", "Raist",

			new BookPageInfo
			(
				"Some commands you",
				"should know:",
				
				
				"[BondTime - Allows you",
				"to see how long your",
				"pet has to bond.",
				
				"[Roll - Lets you",
				"'roll' for random",
				"number in party chat."
			),
			new BookPageInfo
			(
				"Reagent Cost (LRC),",
				"Lower Mana Cost (LMC),",
				"Reflect Physical, etc.",
				
				"[Grab - used to",
				"target a corpse to loot",
				"items directly to your",
				"loot bag if you have",
				"purchased a Gold Ledger"
			),
			new BookPageInfo
			(
				"and Loot Bag from the",
				"Reward House.",
				"[Grab -t - Sets the loot",
				"options for the Loot",
				"Bag.",
				
				"Chat Channels:",

				"[PC - World General",
				"Chat (Keep it clean",
				"please)"
			),
			new BookPageInfo
			(			
				"[H - Help",
				"Chat (Monitored by",
				"GMs)",
 
				"[C - Regional",
				"Chat (Helpful when",
				"looking for assistance",
				"in a dungeon)"
			),
			new BookPageInfo
			(
				"To join the additional",
				"chat channels, simply",
				"do the following: Type",
				"[C, press enter key. In",
				"the window click on M",
				"at the top right. Then",
				"put a checkmark for",
				"General chat. Then just"
			),
			new BookPageInfo
			(
				"use the appropriate",
				"command and your",
				"message to speak in",
				"that channel. For",
				"example, typing [PC Hi",
				"guys! will say Hi guys!",
				"in the General Chat",
				"channel."
			)
		);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public CBookExpanseCommands() : base( 0xFF0, false )
		{
			Hue = 201;
		}

		public CBookExpanseCommands( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int)0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
}