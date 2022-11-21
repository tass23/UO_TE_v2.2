using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class RunicRechargeTarget : Target // Create our targeting class (which we derive from the base target class)
	{
		private RunicRechargeDeed m_Deed;

		public RunicRechargeTarget( RunicRechargeDeed deed ) : base( 1, false, TargetFlags.None )
		{
			m_Deed = deed;
		}

		protected override void OnTarget( Mobile from, object target ) // Override the protected OnTarget() for our feature
		{
			if ( m_Deed.Deleted || m_Deed.RootParent != from )
				return;

			if ( target is BaseRunicTool )
			{
				BaseRunicTool item = (BaseRunicTool)target;

				item.UsesRemaining = 30;
				item.ShowUsesRemaining = true;
				from.SendMessage( "You have added 30 Uses to this runic tool." ); // You bless the item....

				m_Deed.Delete(); // Delete the deed
			}
			else
			{
				from.SendMessage( "You cannot recharge that object." ); // You cannot bless that object
			}
		}
	}
	public class RunicRechargeDeed : Item
	{

		[Constructable]
		public RunicRechargeDeed() : base( 5360 )
		{
			Name = "Runic Tool Recharge Deed";
			base.Weight = 1.0;
			Hue = 1291;
			LootType = LootType.Blessed;
		}

		public RunicRechargeDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			LootType = LootType.Blessed;

			int version = reader.ReadInt();
		}

		public override bool DisplayLootType{ get{ return false; } }
		
		public override void OnDoubleClick( Mobile from ) // Override double click of the deed to call our target
		{
			if ( !IsChildOf( from.Backpack ) ) // Make sure its in their pack
			{
				 from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else
			{
				from.SendMessage( "Which runic tool would you like to add uses to?" ); // What would you like to bless? (Clothes Only)
				from.Target = new RunicRechargeTarget( this ); // Call our target
			 }
		}
	}
}


