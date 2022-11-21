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
	public class FireResist
	{
		private static readonly int m_Minimum = 1;
		private static readonly int m_Maximum = 15;

		public static void Callback( Mobile from, object target, object craftState )
		{
			string errorMessage = null;
			CraftState cs = craftState as CraftState;

			try
			{
				if ( target is BaseArmor )
					SpellCraft.ApplyResistance( from, cs.Book, cs.Id, (BaseArmor)target, ResistanceType.Fire, m_Minimum, m_Maximum );
				
				else if (target is BaseWeapon )
					SpellCraft.ApplyAttribute( from, cs.Book, cs.Id, (BaseWeapon)target, AosWeaponAttribute.ResistFireBonus, m_Minimum, m_Maximum );
				
				else if ( target is BaseHat )
					SpellCraft.ApplyAttribute( from, cs.Book, cs.Id, (BaseHat)target, AosElementAttribute.Fire, m_Minimum, m_Maximum );

				else if (target is BaseJewel )
					SpellCraft.ApplyAttribute( from, cs.Book, cs.Id, (BaseJewel)target, AosElementAttribute.Fire, m_Minimum, m_Maximum );
				
				else
					errorMessage = SpellCraft.AssembleMessage( SpellCraft.MsgNums.ArmorComma, SpellCraft.MsgNums.ShieldsComma, SpellCraft.MsgNums.WeaponsComma, SpellCraft.MsgNums.HatsComma, SpellCraft.MsgNums.AndJewelry );
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