using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;

namespace Server.Gumps
{
	public class ElementalLevelGump : Gump
	{
		private Mobile m_Pet;

		public ElementalLevelGump( Mobile pet ) : base( 0, 0 )
		{
			m_Pet = pet;

			BaseCreature bc = (BaseCreature)m_Pet;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;

			AddPage(0);

			AddBackground(12, 9, 394, 278, 5054);
			AddImageTiled(17, 15, 384, 113, 9274);
			AddImageTiled(17, 136, 302, 27, 9274);
			AddImageTiled(17, 171, 302, 110, 9274);
			AddImageTiled(326, 136, 76, 27, 9274);
			AddImageTiled(326, 171, 76, 110, 9274);
			
			AddLabel(22, 20, 1149, @"Elemental Points:");
			AddLabel(22, 40, 1149, @"Pet's Current Level:");
			AddLabel(22, 60, 1149, @"Pet's Maximum Level:");
			AddLabel(22, 80, 1149, @"Pet's Gender:");
			AddLabel(22, 100, 1149, @"Pet's Name:");

			AddLabel(125, 20, 64, bc.AbilityPoints.ToString() );
			AddLabel(149, 40, 64, bc.Level.ToString() );
			AddLabel(150, 60, 64, bc.MaxLevel.ToString() );

			AddImage(336, 20, 5549);
			
			if ( bc.Female == true )
				AddLabel(107, 80, 64, @"Female");
			else
				AddLabel(107, 80, 64, @"Male");
			AddLabel(96, 100, 64, bc.Name.ToString() );

			AddLabel(22, 140, 1149, @"Property Name");
			AddLabel(330, 140, 1149, @"Amount");

			AddPage(1);

			if ( bc.Level >= 35 )
			{
				AddLabel(60, 185, 1149, @"Fire Damage");
				AddLabel(330, 185, 64, bc.FireDamage.ToString() + "/" + FSATS.NormalFDam.ToString() );
				AddButton(24, 185, 4005, 4006, 1, GumpButtonType.Reply, 0);
			}
			else
			{
				AddLabel(60, 185, 38, @"-Locked-");
				AddLabel(330, 185, 38, @"???");
			}

			if ( bc.Level >= 40 )
			{
				AddLabel(60, 205, 1149, @"Cold Damage");
				AddLabel(330, 205, 64, bc.ColdDamage.ToString() + "/" + FSATS.NormalCDam.ToString() );
				AddButton(24, 205, 4005, 4006, 2, GumpButtonType.Reply, 0);
			}
			else
			{
				AddLabel(60, 205, 38, @"-Locked-");
				AddLabel(330, 205, 38, @"???");
			}
			
			if ( bc.Level >= 45 )
			{
				AddLabel(60, 225, 1149, @"Poison Damage");
				AddLabel(330, 225, 64, bc.PoisonDamage.ToString() + "/" + FSATS.NormalPDam.ToString() );
				AddButton(24, 225, 4005, 4006, 3, GumpButtonType.Reply, 0);
			}
			else
			{
				AddLabel(60, 225, 38, @"-Locked-");
				AddLabel(330, 225, 38, @"???");
			}

			if ( bc.Level >= 50 )
			{
				AddLabel(60, 245, 1149, @"Energy Damage");
				AddLabel(330, 245, 64, bc.EnergyDamage.ToString() + "/" + FSATS.NormalEDam.ToString() );
				AddButton(24, 245, 4005, 4006, 4, GumpButtonType.Reply, 0);
			}
			else
			{
				AddLabel(60, 245, 38, @"-Locked-");
				AddLabel(330, 245, 38, @"???");
			}
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile; 

			BaseCreature bc = (BaseCreature)m_Pet;

			if ( from == null )
				return;				

			if ( info.ButtonID == 1 )
			{
				if ( bc.FireDamage >= FSATS.NormalFDam )
				{
					from.SendMessage( "This cannot gain any farther." );

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new ElementalLevelGump( bc ) );
				}
				else if ( bc.AbilityPoints != 0 )
				{
					bc.AbilityPoints -= 1;

					if ( bc.FireDamage != -1 )
						bc.FireDamage += 1;
					else
						bc.FireDamage = bc.FireDamage + 1;

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new ElementalLevelGump( bc ) );
				}
				else
				{
					from.SendMessage( "Your pet lacks the elemental points to do that." );
				}
			}

			if ( info.ButtonID == 2 )
			{
				if ( bc.ColdDamage >= FSATS.NormalCDam )
				{
					from.SendMessage( "This cannot gain any farther." );

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new ElementalLevelGump( bc ) );
				}
				else if ( bc.AbilityPoints != 0 )
				{
					bc.AbilityPoints -= 1;

					if ( bc.ColdDamage != -1 )
						bc.ColdDamage += 1;
					else
						bc.ColdDamage = bc.ColdDamage + 1;

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new ElementalLevelGump( bc ) );
				}
				else
				{
					from.SendMessage( "Your pet lacks the elemental points to do that." );
				}
			}

			if ( info.ButtonID == 3 )
			{
				if ( bc.PoisonDamage >= FSATS.NormalPDam )
				{
					from.SendMessage( "This cannot gain any farther." );

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new ElementalLevelGump( bc ) );
				}
				else if ( bc.AbilityPoints != 0 )
				{
					bc.AbilityPoints -= 1;

					if ( bc.PoisonDamage != -1 )
						bc.PoisonDamage += 1;
					else
						bc.PoisonDamage = bc.PoisonDamage + 1;

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new ElementalLevelGump( bc ) );
				}
				else
				{
					from.SendMessage( "Your pet lacks the elemental points to do that." );
				}
			}

			if ( info.ButtonID == 4 )
			{
				if ( bc.EnergyDamage >= FSATS.NormalEDam )
				{
					from.SendMessage( "This cannot gain any farther." );

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new ElementalLevelGump( bc ) );
				}
				else if ( bc.AbilityPoints != 0 )
				{
					bc.AbilityPoints -= 1;

					if ( bc.EnergyDamage != -1 )
						bc.EnergyDamage += 1;
					else
						bc.EnergyDamage = bc.EnergyDamage + 1;

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new ElementalLevelGump( bc ) );
				}
				else
				{
					from.SendMessage( "Your pet lacks the elemental points to do that." );
				}
			}
		}
	}
}