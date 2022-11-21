using System;
using Server;
using Server.Network;
using Server.Gumps;
using Server.Items;

namespace Server.Gumps
{
	public class BioExpGump : Gump
	{
		private int m_ExpPoints;
		private Mobile m_From;
		private BioPetItem m_Pet;

		private int m_CSTotal;
		private int m_TNTotal;
		private int m_PTotal;
		private int m_ETotal;
		private int m_MTotal;

		public BioExpGump( int points, Mobile from, BioPetItem pet, int cs, int tn, int p, int e, int m ) : base( 75, 75 )
		{
			m_ExpPoints = points;
			m_From = from;
			m_Pet = pet;

			m_CSTotal = cs;
			m_TNTotal = tn;
			m_PTotal = p;
			m_ETotal = e;
			m_MTotal = m;

			if ( m_ExpPoints < 0 )
			{
				int newPoints;

				if ( m_From.Hunger <= 0 )
					newPoints = 1;
				else
					newPoints = 3;

				if ( m_From.Int >= 100 )
					newPoints += 1;

				if ( m_From.Int >= 105 )
					newPoints += 1;

				if ( m_From.Int >= 110 )
					newPoints += 1;

				if ( m_From.Int >= 115 )
					newPoints += 1;

				if ( m_From.Int >= 120 )
					newPoints += 1;

				if ( m_From.Int >= 125 )
					newPoints += 1;

				if ( m_From.Int >= 130 )
					newPoints += 2;

				if ( m_From.Int >= 140 )
					newPoints += 3;

				if ( m_From.Hunger >= 5 )
					newPoints += 1;

				if ( m_From.Hunger >= 10 )
					newPoints += 2;

				if ( m_From.Hunger >= 15 )
					newPoints += 3;

				if ( m_From.Hunger >= 20 )
					newPoints += 4;

				if ( m_From.Luck > 0 )
				{
					int luckBonus = m_From.Luck / 500;

					if ( luckBonus > 6 )
						newPoints += 6;
					else if ( luckBonus > 0 )
						newPoints += luckBonus;
				}

				m_ExpPoints = newPoints;
			}

			string remain = "<BASEFONT COLOR=WHITE><CENTER>You have " + m_ExpPoints + " experimentation points remaining.</CENTER></BASEFONT>";

			Closable = false;
			Disposable = false;
			Dragable = true;
			Resizable = false;

			AddPage(0);

			int CSLimit = 4;
			int TNLimit = 4;
			int PLimit = 7;
			int ELimit = 7;
			int MLimit = 7;

			int csmath = 100 * m_CSTotal;
			int cspercent = csmath / CSLimit;

			int tnmath = 100 * m_TNTotal;
			int tnpercent = tnmath / TNLimit;

			int pmath = 100 * m_PTotal;
			int ppercent = pmath / PLimit;

			int emath = 100 * m_ETotal;
			int epercent = emath / ELimit;

			int mmath = 100 * m_MTotal;
			int mpercent = mmath / MLimit;

			AddBackground(12, 9, 365, 355, 5054);
			AddImageTiled(16, 14, 355, 18, 10100);
			AddImageTiled(16, 342, 355, 18, 10100);
			AddHtml( 16, 35, 355, 28, @"<BASEFONT COLOR=WHITE><CENTER>Bio-Engineer Experimentation Menu</CENTER></BASEFONT>", (bool)false, (bool)false);
			AddHtml( 16, 70, 355, 28, remain, (bool)false, (bool)false);
			AddButton(20, 150, 4006, 4005, 1, GumpButtonType.Reply, 0);
			AddButton(20, 180, 4006, 4005, 2, GumpButtonType.Reply, 0);
			AddButton(20, 210, 4006, 4005, 3, GumpButtonType.Reply, 0);
			AddButton(20, 240, 4006, 4005, 4, GumpButtonType.Reply, 0);
			AddButton(20, 270, 4006, 4005, 5, GumpButtonType.Reply, 0);
			AddButton(20, 315, 4012, 4011, 6, GumpButtonType.Reply, 0);
			AddLabel(55, 150, 1149, @"Control Slots");
			AddLabel(55, 180, 1149, @"Taming Needed");
			AddLabel(55, 210, 1149, @"Prowess DNA");
			AddLabel(55, 240, 1149, @"Environment DNA");
			AddLabel(55, 270, 1149, @"Mental DNA");
			AddLabel(55, 315, 1149, @"Create Bio Pet Now");
			AddLabel(325, 150, 1149, cspercent.ToString() + "%" );
			AddLabel(325, 180, 1149, tnpercent.ToString() + "%" );
			AddLabel(325, 210, 1149, ppercent.ToString() + "%" );
			AddLabel(325, 240, 1149, epercent.ToString() + "%" );
			AddLabel(325, 270, 1149, mpercent.ToString() + "%" );
			AddHtml( 16, 110, 355, 28, @"<BASEFONT COLOR=WHITE><CENTER>Please choose an Experimentation below or create pet now</CENTER></BASEFONT>", (bool)false, (bool)false);

		}

		private int GetPercentage( int amount )
		{
			if ( amount <= 0 )
				return 0;

			int math = amount * 10;
			int percent = math / 100;

			return percent;
		}

		private Double GetPercentage( Double amount )
		{
			if ( amount <= 0 )
				return 0;

			Double math = amount * 10.0;
			Double percent = math / 100.0;

			return percent;
		}

      		public override void OnResponse( NetState state, RelayInfo info ) 
      		{ 
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

			int CSLimit = 4;
			int TNLimit = 4;
			int PLimit = 7;
			int ELimit = 7;
			int MLimit = 7;

        		if ( info.ButtonID == 1 ) // Control Slots
         		{
				BioPetItem bio = m_Pet;

				if ( m_CSTotal >= CSLimit )
				{
					from.SendMessage( "You cannot experiment any more on this." );
					from.SendGump( new BioExpGump( m_ExpPoints, from, m_Pet, m_CSTotal, m_TNTotal, m_PTotal, m_ETotal, m_MTotal ) );
				}
				else if ( m_ExpPoints > 0 )
				{
					int chance = 90;

					if ( from.Luck > 0 )
						chance += from.Luck / 100;

					if ( chance > 100 )
						chance = 100;

					if ( Utility.Random( 100 ) < chance )
					{
						bio.DNAControlSlots -= 1;
						
						if ( bio.DNAControlSlots < 1 )
							bio.DNAControlSlots = 1;

						from.SendMessage( 52, "The experiment was a great success." );
					}
					else
					{
						bio.DNAControlSlots += 1;
					
						if ( bio.DNAControlSlots > 5 )
							bio.DNAControlSlots = 5;

						from.SendMessage( 38, "The experiment was a massive failure." );
					}

					m_CSTotal += 1;
					m_ExpPoints -= 1;
					from.SendGump( new BioExpGump( m_ExpPoints, from, m_Pet, m_CSTotal, m_TNTotal, m_PTotal, m_ETotal, m_MTotal ) );
				}
				else
				{
					from.SendMessage( "You lack the experimentation points for this." );
					from.SendGump( new BioExpGump( m_ExpPoints, from, m_Pet, m_CSTotal, m_TNTotal, m_PTotal, m_ETotal, m_MTotal ) );
				}
			}

        		if ( info.ButtonID == 2 ) // Taming Needed
         		{
				BioPetItem bio = m_Pet;

				if ( m_TNTotal >= TNLimit )
				{
					from.SendMessage( "You cannot experiment any more on this." );
					from.SendGump( new BioExpGump( m_ExpPoints, from, m_Pet, m_CSTotal, m_TNTotal, m_PTotal, m_ETotal, m_MTotal ) );
				}
				else if ( m_ExpPoints > 0 )
				{
					int chance = 70;

					if ( from.Luck > 0 )
						chance += from.Luck / 100;

					if ( chance > 100 )
						chance = 100;

					if ( Utility.Random( 100 ) < chance )
					{
						bio.DNAMinTame -= 30.0;
						
						if ( bio.DNAMinTame < 0.0 )
							bio.DNAMinTame = 0.0;

						from.SendMessage( 52, "The experiment was a great success." );
					}
					else
					{
						bio.DNAMinTame += 30.0;
					
						if ( bio.DNAMinTame > 120.0 )
							bio.DNAMinTame = 120.0;

						from.SendMessage( 38, "The experiment was a massive failure." );
					}

					m_TNTotal += 1;
					m_ExpPoints -= 1;
					from.SendGump( new BioExpGump( m_ExpPoints, from, m_Pet, m_CSTotal, m_TNTotal, m_PTotal, m_ETotal, m_MTotal ) );
				}
				else
				{
					from.SendMessage( "You lack the experimentation points for this." );
					from.SendGump( new BioExpGump( m_ExpPoints, from, m_Pet, m_CSTotal, m_TNTotal, m_PTotal, m_ETotal, m_MTotal ) );
				}
			}

        		if ( info.ButtonID == 3 ) // Prowess
         		{
				BioPetItem bio = m_Pet;

				if ( m_PTotal >= PLimit )
				{
					from.SendMessage( "You cannot experiment any more on this." );
					from.SendGump( new BioExpGump( m_ExpPoints, from, m_Pet, m_CSTotal, m_TNTotal, m_PTotal, m_ETotal, m_MTotal ) );
				}
				else if ( m_ExpPoints > 0 )
				{
					int chance = 60;

					if ( from.Luck > 0 )
						chance += from.Luck / 200;

					if ( chance > 100 )
						chance = 100;

					if ( Utility.Random( 100 ) < chance )
					{
						bio.DNAStr += GetPercentage( bio.DNAStr );
						bio.DNAHits += GetPercentage( bio.DNAHits );
						bio.DNAPhysicalResist += GetPercentage( bio.DNAPhysicalResist );
						bio.DNADamageMin += GetPercentage( bio.DNADamageMin );
						bio.DNADamageMax += GetPercentage( bio.DNADamageMax );
						bio.DNAArmor += GetPercentage( bio.DNAArmor );
						bio.DNAAnatomy += GetPercentage( bio.DNAAnatomy );
						bio.DNAWrestling += GetPercentage( bio.DNAWrestling );
						bio.DNATactics += GetPercentage( bio.DNATactics );

						from.SendMessage( 52, "The experiment was a great success." );
					}
					else
					{
						bio.DNAStr -= GetPercentage( bio.DNAStr );
						bio.DNAHits -= GetPercentage( bio.DNAHits );
						bio.DNAPhysicalResist -= GetPercentage( bio.DNAPhysicalResist );
						bio.DNADamageMin -= GetPercentage( bio.DNADamageMin );
						bio.DNADamageMax -= GetPercentage( bio.DNADamageMax );
						bio.DNAArmor -= GetPercentage( bio.DNAArmor );
						bio.DNAAnatomy -= GetPercentage( bio.DNAAnatomy );
						bio.DNAWrestling -= GetPercentage( bio.DNAWrestling );
						bio.DNATactics -= GetPercentage( bio.DNATactics );

						from.SendMessage( 38, "The experiment was a massive failure." );
					}

					m_PTotal += 1;
					m_ExpPoints -= 1;
					from.SendGump( new BioExpGump( m_ExpPoints, from, m_Pet, m_CSTotal, m_TNTotal, m_PTotal, m_ETotal, m_MTotal ) );
				}
				else
				{
					from.SendMessage( "You lack the experimentation points for this." );
					from.SendGump( new BioExpGump( m_ExpPoints, from, m_Pet, m_CSTotal, m_TNTotal, m_PTotal, m_ETotal, m_MTotal ) );
				}
			}

        		if ( info.ButtonID == 4 ) // Environment
         		{
				BioPetItem bio = m_Pet;

				if ( m_ETotal >= ELimit )
				{
					from.SendMessage( "You cannot experiment any more on this." );
					from.SendGump( new BioExpGump( m_ExpPoints, from, m_Pet, m_CSTotal, m_TNTotal, m_PTotal, m_ETotal, m_MTotal ) );
				}
				else if ( m_ExpPoints > 0 )
				{
					int chance = 60;

					if ( from.Luck > 0 )
						chance += from.Luck / 200;

					if ( chance > 100 )
						chance = 100;

					if ( Utility.Random( 100 ) < chance )
					{
						bio.DNADex += GetPercentage( bio.DNADex );
						bio.DNAStam += GetPercentage( bio.DNAStam );
						bio.DNAFireResist += GetPercentage( bio.DNAFireResist );
						bio.DNAColdResist += GetPercentage( bio.DNAColdResist );
						bio.DNAMagicResist += GetPercentage( bio.DNAMagicResist );
						bio.DNAPoisoning += GetPercentage( bio.DNAPoisoning );

						from.SendMessage( 52, "The experiment was a great success." );
					}
					else
					{
						bio.DNADex -= GetPercentage( bio.DNADex );
						bio.DNAStam -= GetPercentage( bio.DNAStam );
						bio.DNAFireResist -= GetPercentage( bio.DNAFireResist );
						bio.DNAColdResist -= GetPercentage( bio.DNAColdResist );
						bio.DNAMagicResist -= GetPercentage( bio.DNAMagicResist );
						bio.DNAPoisoning -= GetPercentage( bio.DNAPoisoning );

						from.SendMessage( 38, "The experiment was a massive failure." );
					}

					m_ETotal += 1;
					m_ExpPoints -= 1;
					from.SendGump( new BioExpGump( m_ExpPoints, from, m_Pet, m_CSTotal, m_TNTotal, m_PTotal, m_ETotal, m_MTotal ) );
				}
				else
				{
					from.SendMessage( "You lack the experimentation points for this." );
					from.SendGump( new BioExpGump( m_ExpPoints, from, m_Pet, m_CSTotal, m_TNTotal, m_PTotal, m_ETotal, m_MTotal ) );
				}
			}

        		if ( info.ButtonID == 5 ) // Mental
         		{
				BioPetItem bio = m_Pet;

				if ( m_MTotal >= MLimit )
				{
					from.SendMessage( "You cannot experiment any more on this." );
					from.SendGump( new BioExpGump( m_ExpPoints, from, m_Pet, m_CSTotal, m_TNTotal, m_PTotal, m_ETotal, m_MTotal ) );
				}
				else if ( m_ExpPoints > 0 )
				{
					int chance = 60;

					if ( from.Luck > 0 )
						chance += from.Luck / 200;

					if ( chance > 100 )
						chance = 100;

					if ( Utility.Random( 100 ) < chance )
					{
						bio.DNAInt += GetPercentage( bio.DNAInt );
						bio.DNAMana += GetPercentage( bio.DNAMana );
						bio.DNAEnergyResist += GetPercentage( bio.DNAEnergyResist );
						bio.DNAPoisonResist += GetPercentage( bio.DNAPoisonResist );
						bio.DNAMagery += GetPercentage( bio.DNAMagery );
						bio.DNAEvalInt += GetPercentage( bio.DNAEvalInt );
						bio.DNAMeditation += GetPercentage( bio.DNAMeditation );

						from.SendMessage( 52, "The experiment was a great success." );
					}
					else
					{
						bio.DNAInt -= GetPercentage( bio.DNAInt );
						bio.DNAMana -= GetPercentage( bio.DNAMana );
						bio.DNAEnergyResist -= GetPercentage( bio.DNAEnergyResist );
						bio.DNAPoisonResist -= GetPercentage( bio.DNAPoisonResist );
						bio.DNAMagery -= GetPercentage( bio.DNAMagery );
						bio.DNAEvalInt -= GetPercentage( bio.DNAEvalInt );
						bio.DNAMeditation -= GetPercentage( bio.DNAMeditation );

						from.SendMessage( 38, "The experiment was a massive failure." );
					}

					m_MTotal += 1;
					m_ExpPoints -= 1;
					from.SendGump( new BioExpGump( m_ExpPoints, from, m_Pet, m_CSTotal, m_TNTotal, m_PTotal, m_ETotal, m_MTotal ) );
				}
				else
				{
					from.SendMessage( "You lack the experimentation points for this." );
					from.SendGump( new BioExpGump( m_ExpPoints, from, m_Pet, m_CSTotal, m_TNTotal, m_PTotal, m_ETotal, m_MTotal ) );
				}
			}

        		if ( info.ButtonID == 6 ) // Finish
         		{
				m_Pet.Visible = true;
				from.SendMessage( "The pet control deed has been placed in your backpack." );
			}

		}
	}
}