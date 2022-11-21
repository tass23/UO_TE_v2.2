using System;
using Server;
using System.Collections.Generic;
using Server.Mobiles;

namespace Server
{
	public class PetLeveling
	{
		public static void DoDeathCheck( BaseCreature from )
		{
			Mobile cm = from.ControlMaster;

			if ( cm != null && from.Controlled == true && from.Tamable == true )
			{
				if ( from.IsBonded == true )
				{
					/*
					if ( Utility.Random( 100 ) < 5 )
					{
						int strloss = from.Str / 20;
						int dexloss = from.Dex / 20;
						int intloss = from.Int / 20;
						int hitsloss = from.Hits / 20;
						int stamloss = from.Stam / 20;
						int manaloss = from.Mana / 20;
						int physloss = from.PhysicalResistance / 20;
						int fireloss = from.FireResistance / 20;
						int coldloss = from.ColdResistance / 20;
						int energyloss = from.EnergyResistance / 20;
						int poisonloss = from.PoisonResistance / 20;
						int dminloss = from.DamageMin / 20;
						int dmaxloss = from.DamageMax / 20;

						if ( from.Str > strloss )
							from.Str -= strloss;

						if ( from.Str > dexloss )
							from.Dex -= dexloss;

						if ( from.Str > intloss )
							from.Int -= intloss;
						
						if ( from.HitsMaxSeed > hitsloss )
							from.HitsMaxSeed -= hitsloss;
						if ( from.StamMaxSeed > stamloss )
							from.StamMaxSeed -= stamloss;
						if ( from.ManaMaxSeed > manaloss )
							from.ManaMaxSeed -= manaloss;

						if ( from.PhysicalResistanceSeed > physloss )
							from.PhysicalResistanceSeed -= physloss;
						if ( from.FireResistSeed > fireloss )
							from.FireResistSeed -= fireloss;
						if ( from.ColdResistSeed > coldloss )
							from.ColdResistSeed -= coldloss;
						if ( from.EnergyResistSeed > energyloss )
							from.EnergyResistSeed -= energyloss;
						if ( from.PoisonResistSeed > poisonloss )
							from.PoisonResistSeed -= poisonloss;

						if ( from.DamageMin > dminloss )
							from.DamageMin -= dminloss;

						if ( from.DamageMax > dmaxloss )
							from.DamageMax -= dmaxloss;

						cm.SendMessage( 38, "Your pet has suffered a 5% stat lose due to its untimely death." );
					}
					*/

					cm.SendMessage( 64, "Your pet has been killed!" );
				}
				else
				{
					cm.SendMessage( 64, "Your pet has been killed!" );
				}
			}
		}

		public static void DoBioDeath( BaseCreature from )
		{
			Mobile cm = from.ControlMaster;

			if ( cm != null && from.Controlled == true && from.Tamable == true )
			{
				if ( from.IsBonded == true )
				{
					/*
					if ( Utility.Random( 100 ) < 25 )
					{
						int strloss = from.Str / 20;
						int dexloss = from.Dex / 20;
						int intloss = from.Int / 20;
						int hitsloss = from.Hits / 20;
						int stamloss = from.Stam / 20;
						int manaloss = from.Mana / 20;
						int physloss = from.PhysicalResistance / 20;
						int fireloss = from.FireResistance / 20;
						int coldloss = from.ColdResistance / 20;
						int energyloss = from.EnergyResistance / 20;
						int poisonloss = from.PoisonResistance / 20;
						int dminloss = from.DamageMin / 20;
						int dmaxloss = from.DamageMax / 20;

						if ( from.Str > strloss )
							from.Str -= strloss;

						if ( from.Str > dexloss )
							from.Dex -= dexloss;

						if ( from.Str > intloss )
							from.Int -= intloss;
						
						if ( from.HitsMaxSeed > hitsloss )
							from.HitsMaxSeed -= hitsloss;
						if ( from.StamMaxSeed > stamloss )
							from.StamMaxSeed -= stamloss;
						if ( from.ManaMaxSeed > manaloss )
							from.ManaMaxSeed -= manaloss;

						if ( from.PhysicalResistanceSeed > physloss )
							from.PhysicalResistanceSeed -= physloss;
						if ( from.FireResistSeed > fireloss )
							from.FireResistSeed -= fireloss;
						if ( from.ColdResistSeed > coldloss )
							from.ColdResistSeed -= coldloss;
						if ( from.EnergyResistSeed > energyloss )
							from.EnergyResistSeed -= energyloss;
						if ( from.PoisonResistSeed > poisonloss )
							from.PoisonResistSeed -= poisonloss;

						if ( from.DamageMin > dminloss )
							from.DamageMin -= dminloss;

						if ( from.DamageMax > dmaxloss )
							from.DamageMax -= dmaxloss;

						cm.SendMessage( 38, "Your pet has suffered a 5% stat lose due to its untimely death." );
					}
					*/
					cm.SendMessage( 64, "Your pet has been killed!" );
				}
				else
				{
					cm.SendMessage( 64, "Your pet has been killed!" );
				}
			}
		}

		public static void DoEvoCheck( BaseCreature attacker )
		{
			if ( attacker.Str >= 20 )
				attacker.Str += attacker.Str / 20;
			else
				attacker.Str += 1;

			if ( attacker.Dex >= 20 )
				attacker.Dex += attacker.Dex / 20;
			else
				attacker.Dex += 1;

			if ( attacker.Int >= 20 )
				attacker.Int += attacker.Int / 20;
			else
				attacker.Int += 1;
		}

		public static void DoLevelBonus( BaseCreature attacker )
		{
			int chance = Utility.Random( 100 );
			
			if ( chance < 35 )
			{
				attacker.Str += Utility.RandomMinMax( 1, 3 );
				attacker.Dex += Utility.RandomMinMax( 1, 3 );
				attacker.Int += Utility.RandomMinMax( 1, 3 );
			}
		}

		public static void CheckLevel( Mobile defender, BaseCreature attacker, int count )
		{
			bool nolevel = false;
			Type typ = attacker.GetType();
			string nam = attacker.Name;

			foreach ( string check in FSATS.NoLevelCreatures )
			{
  				if ( check == nam )
    					nolevel = true;
			}

			if ( nolevel != false )
				return;

			int expgainmin, expgainmax;

			if ( attacker is BaseBioCreature || attacker is BioCreature || attacker is BioMount )
			{
			}
			else if ( defender is BaseCreature )
			{
				if ( attacker.Controlled == true && attacker.ControlMaster != null && attacker.Summoned == false  && attacker.Allured == false && attacker.Level <= 34)
				{
					BaseCreature bc = (BaseCreature)defender;

					expgainmin = bc.HitsMax * 25;
					expgainmax = bc.HitsMax * 50;

					int xpgain = Utility.RandomMinMax( expgainmin, expgainmax );
					
					if ( count > 1 )
						xpgain = xpgain / count;

					if ( attacker.Level <= attacker.MaxLevel - 1 )
					{
						attacker.Exp += xpgain;
						attacker.ControlMaster.SendMessage( "Your pet has gained {0} experience points.", xpgain );
					}
			
					int nextLevel = attacker.NextLevel * attacker.Level;
					if ( attacker.Level <= 34 )
					{
						if ( attacker.Exp >= nextLevel && attacker.Level <= attacker.MaxLevel - 1 )
						{
							DoLevelBonus( attacker );

							Mobile cm = attacker.ControlMaster;
							attacker.Level += 1;
							attacker.Exp = 0;
							attacker.FixedParticles( 0x373A, 10, 15, 5012, EffectLayer.Waist );
							attacker.PlaySound( 503 );
							cm.SendMessage( 38, "Your pets level has increased to {0}.", attacker.Level );

							int gain = Utility.RandomMinMax( 10, 50 );

							attacker.AbilityPoints += gain;

							if ( attacker.ControlMaster != null )
							{
								attacker.ControlMaster.SendMessage( 38, "Your pet has gained {0} ability points.", gain );
							}

							if ( attacker.Level == 9 )
							{
								attacker.AllowMating = true;
								cm.SendMessage( 1161, "Your pet is now at the level to mate." );
							}
							if ( attacker.Level == 34 )
							{
								attacker.AllowMating = false;
								attacker.MaxLevel = 55;
								cm.SendMessage( 1161, "Your pet has now moved in to Elemental Levels and cannot mate anymore. This is your last level to assign Ability Points." );
							}
							if ( attacker.Evolves == true )
							{
								if ( attacker.UsesForm1 == true && attacker.F0 == true )
								{
									DoEvoCheck( attacker );

									attacker.BodyValue = attacker.Form1;
									attacker.BaseSoundID = attacker.Sound1;
									attacker.F1 = true;
									attacker.F2 = false;
									attacker.F3 = false;
									attacker.F4 = false;
									attacker.F5 = false;
									attacker.F6 = false;
									attacker.F7 = false;
									attacker.F8 = false;
									attacker.F9 = false;
									attacker.UsesForm1 = false;
									cm.SendMessage( 64, "Your pet has evoloved." );
								}
								else if ( attacker.UsesForm2 == true && attacker.F1 == true )
								{
									DoEvoCheck( attacker );

									attacker.BodyValue = attacker.Form2;
									attacker.BaseSoundID = attacker.Sound2;
									attacker.F1 = false;
									attacker.F2 = true;
									attacker.F3 = false;
									attacker.F4 = false;
									attacker.F5 = false;
									attacker.F6 = false;
									attacker.F7 = false;
									attacker.F8 = false;
									attacker.F9 = false;
									attacker.UsesForm2 = false;
									cm.SendMessage( 64, "Your pet has evoloved." );
								}
								else if ( attacker.UsesForm3 == true && attacker.F2 == true )
								{
									DoEvoCheck( attacker );

									attacker.BodyValue = attacker.Form3;
									attacker.BaseSoundID = attacker.Sound3;
									attacker.F1 = false;
									attacker.F2 = false;
									attacker.F3 = true;
									attacker.F4 = false;
									attacker.F5 = false;
									attacker.F6 = false;
									attacker.F7 = false;
									attacker.F8 = false;
									attacker.F9 = false;
									attacker.UsesForm3 = false;
									cm.SendMessage( 64, "Your pet has evoloved." );
								}
								else if ( attacker.UsesForm4 == true && attacker.F3 == true )
								{
									DoEvoCheck( attacker );

									attacker.BodyValue = attacker.Form4;
									attacker.BaseSoundID = attacker.Sound4;
									attacker.F1 = false;
									attacker.F2 = false;
									attacker.F3 = false;
									attacker.F4 = true;
									attacker.F5 = false;
									attacker.F6 = false;
									attacker.F7 = false;
									attacker.F8 = false;
									attacker.F9 = false;
									attacker.UsesForm4 = false;
									cm.SendMessage( 64, "Your pet has evoloved." );
								}
								else if ( attacker.UsesForm5 == true && attacker.F4 == true )
								{
									DoEvoCheck( attacker );

									attacker.BodyValue = attacker.Form5;
									attacker.BaseSoundID = attacker.Sound5;
									attacker.F1 = false;
									attacker.F2 = false;
									attacker.F3 = false;
									attacker.F4 = false;
									attacker.F5 = true;
									attacker.F6 = false;
									attacker.F7 = false;
									attacker.F8 = false;
									attacker.F9 = false;
									attacker.UsesForm5 = false;
									cm.SendMessage( 64, "Your pet has evoloved." );
								}
								else if ( attacker.UsesForm6 == true && attacker.F5 == true )
								{
									DoEvoCheck( attacker );

									attacker.BodyValue = attacker.Form6;
									attacker.BaseSoundID = attacker.Sound6;
									attacker.F1 = false;
									attacker.F2 = false;
									attacker.F3 = false;
									attacker.F4 = false;
									attacker.F5 = false;
									attacker.F6 = true;
									attacker.F7 = false;
									attacker.F8 = false;
									attacker.F9 = false;
									attacker.UsesForm6 = false;
									cm.SendMessage( 64, "Your pet has evoloved." );
								}
								else if ( attacker.UsesForm7 == true && attacker.F6 == true )
								{
									DoEvoCheck( attacker );

									attacker.BodyValue = attacker.Form7;
									attacker.BaseSoundID = attacker.Sound7;
									attacker.F1 = false;
									attacker.F2 = false;
									attacker.F3 = false;
									attacker.F4 = false;
									attacker.F5 = false;
									attacker.F6 = false;
									attacker.F7 = true;
									attacker.F8 = false;
									attacker.F9 = false;
									attacker.UsesForm7 = false;
									cm.SendMessage( 64, "Your pet has evoloved." );
								}
								else if ( attacker.UsesForm8 == true && attacker.F7 == true )
								{
									DoEvoCheck( attacker );

									attacker.BodyValue = attacker.Form8;
									attacker.BaseSoundID = attacker.Sound8;
									attacker.F1 = false;
									attacker.F2 = false;
									attacker.F3 = false;
									attacker.F4 = false;
									attacker.F5 = false;
									attacker.F6 = false;
									attacker.F7 = false;
									attacker.F8 = true;
									attacker.F9 = false;
									attacker.UsesForm8 = false;
									cm.SendMessage( 64, "Your pet has evoloved." );
								}
								else if ( attacker.UsesForm9 == true && attacker.F8 == true )
								{
									DoEvoCheck( attacker );

									attacker.BodyValue = attacker.Form9;
									attacker.BaseSoundID = attacker.Sound9;
									attacker.F1 = false;
									attacker.F2 = false;
									attacker.F3 = false;
									attacker.F4 = false;
									attacker.F5 = false;
									attacker.F6 = false;
									attacker.F7 = false;
									attacker.F8 = false;
									attacker.F9 = true;
									attacker.UsesForm9 = false;
									cm.SendMessage( 64, "Your pet has evoloved." );
								}	
							}
						}
					}
				}
			}
		}
	}
}