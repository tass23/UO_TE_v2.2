#region AuthorHeader
//
//	SpellCrafting version 3.0, by Xanthos and TheOutkastDev
//
//  Based on original ideas and code by TheOutkastDev
//
#endregion AuthorHeader
using System;
using Server;
using Server.Mobiles;
using Server.Targeting;

namespace Server.SpellCrafting.Items
{
	public class BaseSpellCraft : Item
	{
		private int m_CraftID;

		[CommandProperty( AccessLevel.GameMaster )]
		public int CraftID
		{
			get { return m_CraftID; } 
			set { m_CraftID = value; InvalidateProperties(); }
		}

		public BaseSpellCraft( int amount, int craft ) : base( 0x1EA7 )
		{
			Name = "Spellcraft Jewel";
			Hue = 1161;
			Stackable = false;
			Weight = 0.1;
			Amount = amount;
			CraftID = craft;
		}

		public BaseSpellCraft( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties(list);
			list.Add( 1050045," \t" + SpellCraft.GetCraftName( m_CraftID ) + "\t " );
		}

		public override void OnSingleClick( Mobile from )
		{
			this.LabelTo( from, "Spellcraft Jewel : {0}", SpellCraft.GetCraftName( m_CraftID ) );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( m_CraftID );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_CraftID = reader.ReadInt();
		}
	}
}