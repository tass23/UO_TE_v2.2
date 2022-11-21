#region AuthorHeader
//
//	SpellCrafting version 3.0, by Xanthos and TheOutkastDev
//
//  Based on original ideas and code by TheOutkastDev
//
#endregion AuthorHeader
using System;
using Server;
using Server.Items;
using Server.SpellCrafting;

namespace Server.SpellCrafting.Crafts
{
	public class Slayer
	{
		public static void Callback( Mobile from, object target, object craftState )
		{
			string errorMessage = null;
			CraftState cs = craftState as CraftState;

			try
			{
				if (target is BaseWeapon )
					SpellCraft.ApplySlayerAttribute( from, cs.Book, cs.Id, (BaseWeapon)target );
				
				else if (target is BaseInstrument )
					SpellCraft.ApplySlayerAttribute( from, cs.Book, cs.Id, (BaseInstrument)target );
				
				else
					errorMessage = SpellCraft.AssembleMessage( SpellCraft.MsgNums.Weapons, SpellCraft.MsgNums.AndInstruments );
			}
			catch ( SpellCraftException e )
			{
				errorMessage = e.ToString();
			}
			finally
			{
				if ( null != errorMessage )
					from.SendMessage( errorMessage );
			}
		}
	}
}