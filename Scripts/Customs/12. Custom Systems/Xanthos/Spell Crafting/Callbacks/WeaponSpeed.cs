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
	public class WeaponSpeed
	{
		private static readonly int m_Minimum = 5;
		private static readonly int m_Maximum = 30;

		public static void Callback( Mobile from, object target, object craftState )
		{
			string errorMessage = null;
			CraftState cs = craftState as CraftState;

			try
			{
				if ( target is BaseWeapon )
					SpellCraft.ApplyAttribute( from, cs.Book, cs.Id, (BaseWeapon)target, AosAttribute.WeaponSpeed, m_Minimum, m_Maximum, 5 );
				
				else if ( target is BaseShield )
					SpellCraft.ApplyAttribute( from, cs.Book, cs.Id, (BaseShield)target, AosAttribute.WeaponSpeed, m_Minimum, m_Maximum, 5 );
				
				else
					errorMessage = SpellCraft.AssembleMessage( SpellCraft.MsgNums.Weapons );
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