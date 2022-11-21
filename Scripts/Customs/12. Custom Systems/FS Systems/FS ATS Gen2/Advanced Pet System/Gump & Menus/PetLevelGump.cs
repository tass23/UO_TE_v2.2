using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;

namespace Server.Gumps
{
	public class PetLevelGump : Gump
	{
		private Mobile m_Pet;

		public PetLevelGump( Mobile pet ) : base( 0, 0 )
		{
			m_Pet = pet;

			BaseCreature bc = (BaseCreature)m_Pet;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;

			AddPage(0);

			AddBackground(12, 9, 394, 600, 5054);
			AddImageTiled(17, 15, 384, 113, 9274);
			AddImageTiled(17, 136, 302, 27, 9274);
			AddImageTiled(17, 171, 302, 430, 9274);
			AddImageTiled(326, 136, 76, 27, 9274);
			AddImageTiled(326, 171, 76, 430, 9274);

			AddLabel(22, 20, 1149, @"Ability Points:");
			AddLabel(22, 40, 1149, @"Pet's Current Level:");
			AddLabel(22, 60, 1149, @"Pet's Maximum Level:");
			AddLabel(22, 80, 1149, @"Pet's Gender:");
			AddLabel(22, 100, 1149, @"Pet's Name:");

			AddLabel(116, 20, 64, bc.AbilityPoints.ToString() );
			AddLabel(149, 40, 64, bc.Level.ToString() );
			AddLabel(153, 60, 64, bc.MaxLevel.ToString() );

			AddImage(336, 20, 5549);
			
			if ( bc.Female == true )
				AddLabel(107, 80, 64, @"Female");
			else
				AddLabel(107, 80, 64, @"Male");
			AddLabel(96, 100, 64, bc.Name.ToString() );

			AddLabel(22, 140, 1149, @"Property Name");
			AddLabel(330, 140, 1149, @"Amount");

			AddPage(1);

			if ( bc.Level != 0 )
			{
				AddLabel(60, 175, 1149, @"Hit Points");
				AddLabel(60, 200, 1149, @"Stamina");
				AddLabel(60, 225, 1149, @"Mana");

				AddLabel(330, 175, 64, bc.HitsMax.ToString() + "/" + FSATS.NormalHITS.ToString() );
				AddLabel(330, 200, 64, bc.StamMax.ToString() + "/" + FSATS.NormalSTAM.ToString() );
				AddLabel(330, 225, 64, bc.ManaMax.ToString() + "/" + FSATS.NormalMANA.ToString() );

				AddButton(24, 175, 4005, 4006, 1, GumpButtonType.Reply, 0);
				AddButton(24, 200, 4005, 4006, 2, GumpButtonType.Reply, 0);
				AddButton(24, 225, 4005, 4006, 3, GumpButtonType.Reply, 0);
			}
			else
			{
				AddLabel(60, 175, 38, @"-Locked-");
				AddLabel(60, 200, 38, @"-Locked-");
				AddLabel(60, 225, 38, @"-Locked-");

				AddLabel(330, 175, 38, @"???");
				AddLabel(330, 200, 38, @"???");
				AddLabel(330, 225, 38, @"???");
			}

			if ( bc.Level >= 10 )
			{
				AddLabel(60, 250, 1149, @"Physical Resistance");
				AddLabel(60, 275, 1149, @"Fire Resistance");
				AddLabel(60, 300, 1149, @"Cold Resistance");
				AddLabel(60, 325, 1149, @"Energy Resistance");
				AddLabel(60, 350, 1149, @"Poison Resistance");

				AddLabel(330, 250, 64, bc.PhysicalResistance.ToString() + "/" + FSATS.NormalPhys.ToString() );
				AddLabel(330, 275, 64, bc.FireResistance.ToString() + "/" + FSATS.NormalFire.ToString() );
				AddLabel(330, 300, 64, bc.ColdResistance.ToString() + "/" + FSATS.NormalCold.ToString() );
				AddLabel(330, 325, 64, bc.EnergyResistance.ToString() + "/" + FSATS.NormalEnergy.ToString() );
				AddLabel(330, 350, 64, bc.PoisonResistance.ToString() + "/" + FSATS.NormalPoison.ToString() );

				AddButton(24, 250, 4005, 4006, 4, GumpButtonType.Reply, 0);
				AddButton(24, 275, 4005, 4006, 5, GumpButtonType.Reply, 0);
				AddButton(24, 300, 4005, 4006, 6, GumpButtonType.Reply, 0);
				AddButton(24, 325, 4005, 4006, 7, GumpButtonType.Reply, 0);
				AddButton(24, 350, 4005, 4006, 8, GumpButtonType.Reply, 0);
			}
			else
			{
				AddLabel(60, 250, 38, @"-Locked-");
				AddLabel(60, 275, 38, @"-Locked-");
				AddLabel(60, 300, 38, @"-Locked-");
				AddLabel(60, 325, 38, @"-Locked-");
				AddLabel(60, 350, 38, @"-Locked-");

				AddLabel(330, 250, 38, @"???");
				AddLabel(330, 275, 38, @"???");
				AddLabel(330, 300, 38, @"???");
				AddLabel(330, 325, 38, @"???");
				AddLabel(330, 350, 38, @"???");
			}

			if ( bc.Level >= 20 )
			{
				//AddLabel(60, 450, 1149, @"Strength");
				//AddLabel(60, 475, 1149, @"Dexterity");
				//AddLabel(60, 500, 1149, @"Intelligence");
				//AddLabel(60, 375, 1149, @"Armor Rating (Add only if available)");

				//AddLabel(330, 450, 1149, bc.RawStr.ToString() + "/" + FSATS.NormalSTR.ToString() );
				//AddLabel(330, 475, 1149, bc.RawDex.ToString() + "/" + FSATS.NormalDEX.ToString() );
				//AddLabel(330, 500, 1149, bc.RawInt.ToString() + "/" + FSATS.NormalINT.ToString() );
				//AddLabel(330, 375, 64, bc.VirtualArmor.ToString() + "/" + FSATS.NormalVArmor.ToString() );

				//AddButton(24, 450, 4005, 4006, 12, GumpButtonType.Reply, 0);
				//AddButton(24, 475, 4005, 4006, 13, GumpButtonType.Reply, 0);
				//AddButton(24, 500, 4005, 4006, 14, GumpButtonType.Reply, 0);
				//AddButton(24, 375, 4005, 4006, 14, GumpButtonType.Reply, 0);
			}
			else
			{
				//AddLabel(60, 450, 38, @"-Locked-");
				//AddLabel(60, 475, 38, @"-Locked-");
				AddLabel(60, 375, 38, @"-Locked-");

				//AddLabel(330, 450, 38, @"???");
				//AddLabel(330, 475, 38, @"???");
				AddLabel(330, 375, 38, @"???");
			}
			
			if ( bc.Level > 25 )
			{
				AddLabel(60, 400, 1149, @"Roar Attack");
				AddLabel(60, 425, 1149, @"Poison Attack");
				AddLabel(60, 450, 1149, @"Fire Breath Attack");

				AddLabel(330, 400, 64, bc.RoarAttack.ToString() + "/" + "100" );
				AddLabel(330, 425, 64, bc.PetPoisonAttack.ToString() + "/" + "100" );
				AddLabel(330, 450, 64, bc.FireBreathAttack.ToString() + "/" + "100" );

				AddButton(24, 400, 4005, 4006, 15, GumpButtonType.Reply, 0);
				AddButton(24, 425, 4005, 4006, 16, GumpButtonType.Reply, 0);
				AddButton(24, 450, 4005, 4006, 17, GumpButtonType.Reply, 0);
			}
			else
			{
				AddLabel(60, 400, 38, @"-Locked-");
				AddLabel(60, 425, 38, @"-Locked-");
				AddLabel(60, 450, 38, @"-Locked-");

				AddLabel(330, 400, 38, @"???");
				AddLabel(330, 425, 38, @"???");
				AddLabel(330, 450, 38, @"???");
			}
			
			if ( bc.Level >= 29 )
			{
				AddLabel(60, 475, 1149, @"Min Damage");
				AddLabel(60, 500, 1149, @"Max Damage");
				//AddLabel(60, 425, 1149, @"Armor Rating");

				AddLabel(330, 475, 64, bc.DamageMin.ToString() + "/"  + FSATS.NormalMinDam.ToString() );
				AddLabel(330, 500, 64, bc.DamageMax.ToString() + "/"  + FSATS.NormalMaxDam.ToString() );
				//AddLabel(330, 425, 1149, bc.VirtualArmor.ToString() + "/" + FSATS.NormalVArmor.ToString() );

				AddButton(24, 475, 4005, 4006, 9, GumpButtonType.Reply, 0);
				AddButton(24, 500, 4005, 4006, 10, GumpButtonType.Reply, 0);
				//AddButton(24, 425, 4005, 4006, 11, GumpButtonType.Reply, 0);
			}
			else
			{
				AddLabel(60, 475, 38, @"-Locked-");
				AddLabel(60, 500, 38, @"-Locked-");
				//AddLabel(60, 400, 38, @"-Locked-");

				AddLabel(330, 475, 38, @"???");
				AddLabel(330, 500, 38, @"???");
				//AddLabel(330, 400, 38, @"???");
			}
			
			if ( bc.Level >= 33 )
			{
				AddLabel(60, 525, 1149, @"Strength");
				AddLabel(60, 550, 1149, @"Dexterity");
				//AddLabel(60, 500, 1149, @"Intelligence");

				AddLabel(330, 525, 64, bc.RawStr.ToString() + "/" + FSATS.NormalSTR.ToString() );
				AddLabel(330, 550, 64, bc.RawDex.ToString() + "/" + FSATS.NormalDEX.ToString() );
				//AddLabel(330, 500, 1149, bc.RawInt.ToString() + "/" + FSATS.NormalINT.ToString() );

				AddButton(24, 525, 4005, 4006, 12, GumpButtonType.Reply, 0);
				AddButton(24, 550, 4005, 4006, 13, GumpButtonType.Reply, 0);
				//AddButton(24, 500, 4005, 4006, 14, GumpButtonType.Reply, 0);
			}
			else
			{
				AddLabel(60, 525, 38, @"-Locked-");
				AddLabel(60, 550, 38, @"-Locked-");
				//AddLabel(60, 500, 38, @"-Locked-");

				AddLabel(330, 525, 38, @"???");
				AddLabel(330, 550, 38, @"???");
				//AddLabel(330, 500, 38, @"???");
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
				if ( bc.HitsMax >= FSATS.NormalHITS )
				{
					from.SendMessage( "This cannot gain any farther." );

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else if ( bc.AbilityPoints != 0 )
				{
					bc.AbilityPoints -= 1;

					if ( bc.HitsMaxSeed != -1 )
						bc.HitsMaxSeed += 1;
					else
						bc.HitsMaxSeed = bc.HitsMax + 1;

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else
				{
					from.SendMessage( "Your pet lacks the ability points to do that." );
				}
			}

			if ( info.ButtonID == 2 )
			{
				if ( bc.StamMax >= FSATS.NormalSTAM )
				{
					from.SendMessage( "This cannot gain any farther." );

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else if ( bc.AbilityPoints != 0 )
				{
					bc.AbilityPoints -= 1;

					if ( bc.StamMaxSeed != -1 )
						bc.StamMaxSeed += 1;
					else
						bc.StamMaxSeed = bc.StamMax + 1;

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else
				{
					from.SendMessage( "Your pet lacks the ability points to do that." );
				}
			}

			if ( info.ButtonID == 3 )
			{
				if ( bc.ManaMax >= FSATS.NormalMANA )
				{
					from.SendMessage( "This cannot gain any farther." );

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else if ( bc.AbilityPoints != 0 )
				{
					bc.AbilityPoints -= 1;
				
					if ( bc.ManaMaxSeed != -1 )
						bc.ManaMaxSeed += 1;
					else
						bc.ManaMaxSeed = bc.ManaMax + 1;

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else
				{
					from.SendMessage( "Your pet lacks the ability points to do that." );
				}
			}

			if ( info.ButtonID == 4 )
			{
				if ( bc.PhysicalResistance >= FSATS.NormalPhys )
				{
					from.SendMessage( "This cannot gain any farther." );

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else if ( bc.AbilityPoints != 0 )
				{
					bc.AbilityPoints -= 1;
					bc.PhysicalResistanceSeed += 1;

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else
				{
					from.SendMessage( "Your pet lacks the ability points to do that." );
				}
			}

			if ( info.ButtonID == 5 )
			{
				if ( bc.FireResistance >= FSATS.NormalFire )
				{
					from.SendMessage( "This cannot gain any farther." );

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else if ( bc.AbilityPoints != 0 )
				{
					bc.AbilityPoints -= 1;
					bc.FireResistSeed += 1;

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else
				{
					from.SendMessage( "Your pet lacks the ability points to do that." );
				}
			}

			if ( info.ButtonID == 6 )
			{
				if ( bc.ColdResistance >= FSATS.NormalCold )
				{
					from.SendMessage( "This cannot gain any farther." );

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else if ( bc.AbilityPoints != 0 )
				{
					bc.AbilityPoints -= 1;
					bc.ColdResistSeed += 1;

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else
				{
					from.SendMessage( "Your pet lacks the ability points to do that." );
				}
			}

			if ( info.ButtonID == 7 )
			{
				if ( bc.EnergyResistance >= FSATS.NormalEnergy )
				{
					from.SendMessage( "This cannot gain any farther." );

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else if ( bc.AbilityPoints != 0 )
				{
					bc.AbilityPoints -= 1;
					bc.EnergyResistSeed += 1;

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else
				{
					from.SendMessage( "Your pet lacks the ability points to do that." );
				}
			}

			if ( info.ButtonID == 8 )
			{
				if ( bc.PoisonResistance >= FSATS.NormalPoison )
				{
					from.SendMessage( "This cannot gain any farther." );

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else if ( bc.AbilityPoints != 0 )
				{
					bc.AbilityPoints -= 1;
					bc.PoisonResistSeed += 1;

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else
				{
					from.SendMessage( "Your pet lacks the ability points to do that." );
				}
			}

			if ( info.ButtonID == 9 )
			{
				if ( bc.DamageMin >= FSATS.NormalMinDam )
				{
					from.SendMessage( "This cannot gain any farther." );

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else if ( bc.AbilityPoints != 0 )
				{
					bc.AbilityPoints -= 1;
					bc.DamageMin += 1;

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else
				{
					from.SendMessage( "Your pet lacks the ability points to do that." );
				}
			}

			if ( info.ButtonID == 10 )
			{
				if ( bc.DamageMax >= FSATS.NormalMaxDam )
				{
					from.SendMessage( "This cannot gain any farther." );

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else if ( bc.AbilityPoints != 0 )
				{
					bc.AbilityPoints -= 1;
					bc.DamageMax += 1;

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else
				{
					from.SendMessage( "Your pet lacks the ability points to do that." );
				}
			}

			if ( info.ButtonID == 11 )
			{
				if ( bc.VirtualArmor >= FSATS.NormalVArmor )
				{
					from.SendMessage( "This cannot gain any farther." );

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else if ( bc.AbilityPoints != 0 )
				{
					bc.AbilityPoints -= 1;
					bc.VirtualArmor += 1;

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else
				{
					from.SendMessage( "Your pet lacks the ability points to do that." );
				}
			}

			if ( info.ButtonID == 12 )
			{
				if ( bc.RawStr >= FSATS.NormalSTR )
				{
					from.SendMessage( "This cannot gain any farther." );

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else if ( bc.AbilityPoints != 0 )
				{
					bc.AbilityPoints -= 1;
					bc.Str += 1;

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else
				{
					from.SendMessage( "Your pet lacks the ability points to do that." );
				}
			}

			if ( info.ButtonID == 13 )
			{
				if ( bc.RawDex >= FSATS.NormalDEX )
				{
					from.SendMessage( "This cannot gain any farther." );

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else if ( bc.AbilityPoints != 0 )
				{
					bc.AbilityPoints -= 1;
					bc.Dex += 1;

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else
				{
					from.SendMessage( "Your pet lacks the ability points to do that." );
				}
			}

			if ( info.ButtonID == 14 )
			{
				if ( bc.RawInt >= FSATS.NormalINT )
				{
					from.SendMessage( "This cannot gain any farther." );

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else if ( bc.AbilityPoints != 0 )
				{
					bc.AbilityPoints -= 1;
					bc.Int += 1;

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else
				{
					from.SendMessage( "Your pet lacks the ability points to do that." );
				}
			}

			if ( info.ButtonID == 15 )
			{
				if ( bc.RoarAttack >= 100 )
				{
					from.SendMessage( "This cannot gain any farther." );

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else if ( bc.AbilityPoints != 0 )
				{
					bc.AbilityPoints -= 1;
					bc.RoarAttack += 1;

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else
				{
					from.SendMessage( "Your pet lacks the ability points to do that." );
				}
			}

			if ( info.ButtonID == 16 )
			{
				if ( bc.PetPoisonAttack >= 100 )
				{
					from.SendMessage( "This cannot gain any farther." );

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else if ( bc.AbilityPoints != 0 )
				{
					bc.AbilityPoints -= 1;
					bc.PetPoisonAttack  += 1;

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else
				{
					from.SendMessage( "Your pet lacks the ability points to do that." );
				}
			}

			if ( info.ButtonID == 17 )
			{
				if ( bc.FireBreathAttack >= 100 )
				{
					from.SendMessage( "This cannot gain any farther." );

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else if ( bc.AbilityPoints != 0 )
				{
					bc.AbilityPoints -= 1;
					bc.FireBreathAttack += 1;

					if ( bc.AbilityPoints != 0 )
						from.SendGump( new PetLevelGump( bc ) );
				}
				else
				{
					from.SendMessage( "Your pet lacks the ability points to do that." );
				}
			}
		}
	}
}