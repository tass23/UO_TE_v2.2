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
	public class Durability
	{
		private static readonly int m_Minimum = 5;
		private static readonly int m_Maximum = 250;

		public static void Callback( Mobile from, object target, object craftState )
		{
			string errorMessage = null;
			CraftState cs = craftState as CraftState;

			try
			{
				if ( target is BaseArmor )
					SpellCraft.ApplyAttribute( from, cs.Book, cs.Id, (BaseArmor)target, AosArmorAttribute.DurabilityBonus, m_Minimum, m_Maximum, 10 );

				else if ( target is BaseWeapon )
					SpellCraft.ApplyAttribute( from, cs.Book, cs.Id, (BaseWeapon)target, AosWeaponAttribute.DurabilityBonus, m_Minimum, m_Maximum, 10 );
								
				else
					errorMessage = SpellCraft.AssembleMessage( SpellCraft.MsgNums.ArmorComma, SpellCraft.MsgNums.ShieldsComma, SpellCraft.MsgNums.AndWeapons );
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