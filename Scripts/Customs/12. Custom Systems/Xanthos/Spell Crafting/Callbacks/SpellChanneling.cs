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
	public class SpellChanneling
	{
		private static readonly int m_Minimum = 1;
		private static readonly int m_Maximum = 1;

		public static void Callback( Mobile from, object target, object craftState )
		{
			string errorMessage = null;
			CraftState cs = craftState as CraftState;

			try
			{
				if ( target is BaseShield )
				{
					SpellCraft.ApplyAttribute( from, cs.Book, cs.Id, (BaseShield)target, AosAttribute.SpellChanneling, m_Minimum, m_Maximum );
					SpellCraft.AdjustFastCast( (BaseShield)target );	// Chance for a penalty to fc
				}
				if ( target is BaseWeapon )
				{
					SpellCraft.ApplyAttribute( from, cs.Book, cs.Id, (BaseWeapon)target, AosAttribute.SpellChanneling, m_Minimum, m_Maximum );
					SpellCraft.AdjustFastCast( (BaseWeapon)target );	// Chance for a penalty to fc
				}
				else
					errorMessage = SpellCraft.AssembleMessage( SpellCraft.MsgNums.Shields, SpellCraft.MsgNums.AndWeapons );
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