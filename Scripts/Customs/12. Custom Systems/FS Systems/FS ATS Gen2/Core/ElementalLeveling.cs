using System;
using Server;
using Server.Mobiles;

namespace Server
{
	public class ElementalLeveling
	{
		public static void ElementalDeathCheck( BaseCreature from )
		{
			Mobile cm = from.ControlMaster;

			if ( cm != null && from.Controlled == true && from.Tamable == true )
			{
				if ( from.IsBonded == true && from.MaxLevel >= 40 )
				{
					cm.SendMessage( 64, "Your pet has been killed!" );
				}
				else
				{
					cm.SendMessage( 64, "Your pet has been killed!" );
				}
			}
		}

		public static void ElementalLevelBonus( BaseCreature attacker )
		{
			Mobile cm = attacker.ControlMaster;
			
			if ( attacker.Level == 35 )
			{
				attacker.AllowMating = false;
				cm.SendMessage( 1161, "Your pet has now moved in to Elemental Levels and cannot mate anymore." );
			}			
			
			int chance = Utility.Random( 100 );
			
			if ( chance < 35 )
			{
				attacker.Str += Utility.RandomMinMax( 1, 3 );
				attacker.Dex += Utility.RandomMinMax( 1, 3 );
				attacker.Int += Utility.RandomMinMax( 1, 3 );
			}
		}

		public static void ElementalCheckLevel( Mobile defender, BaseCreature attacker, int count )
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
				if ( attacker.Controlled == true && attacker.ControlMaster != null && attacker.Summoned == false  && attacker.Allured == false && attacker.Level >= 35 )
				{
					BaseCreature bc = (BaseCreature)defender;

					expgainmin = bc.HitsMax * 15;
					expgainmax = bc.HitsMax * 35;

					int xpgain = Utility.RandomMinMax( expgainmin, expgainmax );
					
					if ( count > 1 )
						xpgain = xpgain / count;

					if ( attacker.Level <= attacker.MaxLevel - 1 )
					{
						attacker.Exp += xpgain;
						attacker.ControlMaster.SendMessage( "Your pet has gained {0} experience points.", xpgain );
					}
			
					int nextLevel = attacker.NextLevel * attacker.Level;

					if ( attacker.Exp >= nextLevel && attacker.Level <= attacker.MaxLevel - 1 )
					{
						ElementalLevelBonus( attacker );

						Mobile cm = attacker.ControlMaster;
						attacker.Level += 1;
						attacker.Exp = 0;
						attacker.FixedParticles( 0x373A, 10, 15, 5012, EffectLayer.Waist );
						attacker.PlaySound( 503 );
						cm.SendMessage( 38, "Your pets level has increased to {0}.", attacker.Level );

						int gain = Utility.RandomMinMax( 2, 5 );

						attacker.AbilityPoints += gain;

						if ( attacker.ControlMaster != null )
						{
							attacker.ControlMaster.SendMessage( 38, "Your pet has gained {0} elemental points.", gain );
						}
					}
				}
			}
		}
	}
}