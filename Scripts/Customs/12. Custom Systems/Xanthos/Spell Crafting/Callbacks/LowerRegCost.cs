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
	public class LowerRegCost
	{
		private static readonly int m_Minimum = 1;
		private static readonly int m_Maximum = 20;

		public static void Callback( Mobile from, object target, object craftState )
		{
			string errorMessage = null;
			CraftState cs = craftState as CraftState;

			try
			{
				if ( target is BaseArmor && !( target is BaseShield ) )
					SpellCraft.ApplyAttribute( from, cs.Book, cs.Id, (BaseArmor)target, AosAttribute.LowerRegCost, m_Minimum, m_Maximum );
				
				else if ( target is BaseHat )
					SpellCraft.ApplyAttribute( from, cs.Book, cs.Id, (BaseHat)target, AosAttribute.LowerManaCost, m_Minimum, m_Maximum );

				else if (target is BaseJewel )
					SpellCraft.ApplyAttribute( from, cs.Book, cs.Id, (BaseJewel)target, AosAttribute.LowerRegCost, m_Minimum, m_Maximum );
				
				else
					errorMessage = SpellCraft.AssembleMessage( SpellCraft.MsgNums.ArmorComma, SpellCraft.MsgNums.HatsComma, SpellCraft.MsgNums.AndJewelry );
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