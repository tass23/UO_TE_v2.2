using System;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using System.Collections;

namespace Server.Gumps
{
	public class BreedingAcceptGump : Gump
	{
		private Mobile m_Pet1;
		private Mobile m_Pet2;

		public BreedingAcceptGump( Mobile pet1, Mobile pet2 ) : base( 0, 0 )
		{
			m_Pet1 = pet1;
			m_Pet2 = pet2;

			BaseCreature bc = (BaseCreature)m_Pet1;

			Closable=false;
			Disposable=false;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddBackground(25, 22, 318, 324, 5054);
			AddLabel(52, 27, 1149, @"Breeding Request");
			AddButton(46, 321, 4005, 4006, 1, GumpButtonType.Reply, 0);
			AddLabel(78, 322, 1149, @"Baby Stat Table");
			AddLabel(52, 60, 1149, @"Requested By:");
			AddLabel(52, 80, 1149, @"Requesters Pet:");
			AddLabel(52, 100, 1149, @"Your Pet:");
			AddHtml( 49, 122, 271, 161, @"<CENTER><U>Breeding Request</U><BR><BR>Someone has requested to breed thier pet with one of yours. If you deside to accept this offer both you and the other pet owner will get a baby from this union.<BR><BR>If you accept and breeding is successful both pets (Mother/Father) will be stabled in the animal breeders stable untill the birth of the babies. This takes three days. After that both pets will not be able to breed again for six days. The animal breeder will give you a claim ticket, When the three days our up you can then return to the animal breeder and drop your ticket on them, They will charge a five thousand gold coin fee for thier services. Then you will get back your pet and the baby.<BR><BR>Do you accept?", (bool)false, (bool)true);
			AddButton(50, 290, 4023, 4024, 2, GumpButtonType.Reply, 0);
			AddButton(83, 290, 4017, 4018, 3, GumpButtonType.Reply, 0);

			if ( bc.ControlMaster != null )
				AddLabel(144, 60, 64, bc.ControlMaster.Name.ToString() );

			if ( m_Pet1 != null )
				AddLabel(158, 80, 64, m_Pet1.Name.ToString() );

			if ( m_Pet2 != null )
				AddLabel(117, 100, 64, m_Pet2.Name.ToString() );

		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			BaseCreature bc1 = (BaseCreature)m_Pet1;
			BaseCreature bc2 = (BaseCreature)m_Pet2;

			Mobile cm1 = bc1.ControlMaster;
			Mobile cm2 = bc2.ControlMaster;

			if ( from == null )
				return;

			//Baby Stat Table
			if ( info.ButtonID == 1 )
			{
				from.CloseGump( typeof( BreedingAcceptGump ) );
				//from.CloseGump( typeof( BabyStatTableGump ) );

				from.SendGump( new BreedingAcceptGump( m_Pet1, m_Pet2 ) );
				//from.SendGump( new BabyStatTableGump( m_Pet1, m_Pet2 ) );
			}

			//Accept
			if ( info.ButtonID == 2 )
			{
				Mobile breeder = new Mobile();

				Mobile owner = new Mobile();

				int ai = 0;

				if ( bc1.AI == AIType.AI_Mage && bc2.AI == AIType.AI_Mage )
					ai = 1;
				if ( bc1.AI == AIType.AI_Melee && bc2.AI == AIType.AI_Melee )
					ai = 2;
				
				int xstr = bc1.RawStr + bc2.RawStr;
				int xdex = bc1.RawDex + bc2.RawDex;
				int xint = bc1.RawInt + bc2.RawInt;

				int xhits = bc1.HitsMax + bc2.HitsMax;
				int xstam = bc1.StamMax + bc2.StamMax;
				int xmana = bc1.ManaMax + bc2.ManaMax;

				int xphys = bc1.PhysicalResistance + bc2.PhysicalResistance;
				int xfire = bc1.FireResistance + bc2.FireResistance;
				int xcold = bc1.ColdResistance + bc2.ColdResistance;
				int xnrgy = bc1.EnergyResistance + bc2.EnergyResistance;
				int xpois = bc1.PoisonResistance + bc2.PoisonResistance;

				int xdmin = bc1.DamageMin + bc2.DamageMin;
				int xdmax = bc1.DamageMax + bc2.DamageMax;

				int xmlev = bc1.Level + bc2.Level;
				
				//Variables Added to allow for pet's natural resistances over the Normal Caps, used in conditionals below
				int StrCapExempt = 0;
				int DexCapExempt = 0;
				int IntCapExempt = 0;
				int HitsCapExempt = 0;
				int StamCapExempt = 0;
				int ManaCapExempt = 0;
				int PhysCapExempt = 0;
				int FireCapExempt = 0;
				int ColdCapExempt = 0;
				int EnerCapExempt = 0;
				int PsnCapExempt = 0;
				int DMaxCapExempt = 0;
				int DMinCapExempt = 0;
								
				//If statements check to see if anything is naturally above cap, if so stores it in CapExempt for that stat
				int newStr = xstr / 2;
				if ( newStr >= FSATS.NormalSTR ) 
					StrCapExempt = newStr;
				int newDex = xdex / 2;
				if ( newDex >= FSATS.NormalDEX ) 
					DexCapExempt = newDex;
				int newInt = xint / 2;
				if ( newInt >= FSATS.NormalINT ) 
					IntCapExempt = newInt;
				int newHits = xhits / 2;
				if ( newHits >= FSATS.NormalHITS ) 
					HitsCapExempt = newHits;
				int newStam = xstam / 2;
				if ( newStam >= FSATS.NormalSTAM ) 
					StamCapExempt = newStam;
				int newMana = xmana / 2;
				if ( newMana >= FSATS.NormalMANA ) 
					ManaCapExempt = newMana;
				int newPhys = xphys / 2;
				if ( newPhys >= FSATS.NormalPhys ) 
					PhysCapExempt = newPhys;
				int newFire = xfire / 2;
				if ( newFire >= FSATS.NormalFire ) 
					FireCapExempt = newFire;
				int newCold = xcold / 2;
				if ( newCold >= FSATS.NormalCold ) 
					ColdCapExempt = newCold;
				int newNrgy = xnrgy / 2;
				if ( newNrgy >= FSATS.NormalEnergy ) 
					EnerCapExempt = newNrgy;
				int newPois = xpois / 2;
				if ( newPois >= FSATS.NormalPoison ) 
					PsnCapExempt = newPois;
				int newDmin = xdmin / 2;
				if ( newDmin >= FSATS.NormalMinDam )
					DMinCapExempt = newDmin;
				int newDmax = xdmax / 2;
				if ( newDmax >= FSATS.NormalMaxDam )
					DMaxCapExempt = newDmax;

				int newMlev = xmlev / 2;
				
				//Assigning Variables for Baby creatures' statistics and setting to zero
				//Str, Dex, Int
				int babyStr = 0;
				int babyDex = 0;
				int babyInt = 0;
				//Hit Points, Stamina, Mana
				int babyHits = 0;
				int babyStam = 0;
				int babyMana = 0;
				//Resistances 
				int babyPhys = 0;
				int babyFire = 0;
				int babyCold = 0;
				int babyNrgy = 0;
				int babyPois = 0;
				//Damage Minimum & Maximum
				int babyDmin = newDmin;
				int babyDmax = newDmax;
				
				//Adds minimum 10, maximum 25 Str, Dex, Int | 100 Mana / HP | 25 Stamina per Generation
				babyStr = newStr + Utility.RandomMinMax( 10, 25 );
				babyDex = newDex + Utility.RandomMinMax( 10, 25 );
				babyInt = newInt + Utility.RandomMinMax( 10, 25 );
				babyHits = newHits + 100;
				babyStam = newStam + 25;
				babyMana = newMana + 100;

				//Random Utility removed, replaced with flat +1 resistance gain
				babyPhys = newPhys + 1;
				babyFire = newFire + 1;
				babyCold = newCold + 1;
				babyNrgy = newNrgy + 1;
				babyPois = newPois + 1;

				int babyMlev = newMlev + Utility.RandomMinMax( 1, 3 );

				int stats = babyStr + babyDex + babyInt + babyHits + babyStam + babyMana + babyPhys + babyFire + babyCold + babyNrgy + babyPois + babyDmin + babyDmax + babyMlev;
				int newPrice = stats * 3;
				int babyPrice = newPrice;
				int chance = stats;

				if ( chance <= 1500 )
					chance = 1500;

				if ( babyStr >= FSATS.NormalSTR )
					babyStr = FSATS.NormalSTR;
					if (StrCapExempt >= babyStr)  //Checks to see if Cap Exempt > babyStat, if so sets to Cap Exempt
						babyStr = StrCapExempt;

				if ( babyDex >= FSATS.NormalDEX )
					babyDex = FSATS.NormalDEX;
					if (DexCapExempt >= babyDex)
						babyDex = DexCapExempt;

				if ( babyInt >= FSATS.NormalINT )
					babyInt = FSATS.NormalINT;
					if (IntCapExempt >= babyInt)
						babyInt = IntCapExempt;

				if ( babyPhys >= 85 )
					babyPhys = 85;
					if (PhysCapExempt >= babyPhys)
						babyPhys = PhysCapExempt;

				if ( babyFire >= 85 )
					babyFire = 85;
					if (FireCapExempt >= babyFire)
						babyFire = FireCapExempt;

				if ( babyCold >= 85 )
					babyCold = 85;
					if (ColdCapExempt >= babyCold)
						babyCold = ColdCapExempt;

				if ( babyNrgy >= 85 )
					babyNrgy = 85;
					if (EnerCapExempt >= babyNrgy)
						babyNrgy = EnerCapExempt;

				if ( babyPois >= 85 )
					babyPois = 85;
					if (PsnCapExempt >= babyPois)
						babyPois = PsnCapExempt;

				if ( babyDmin >= FSATS.NormalMinDam )
					babyDmin = FSATS.NormalMinDam;
					if (DMinCapExempt >= babyDmin)
						babyDmin = DMinCapExempt;
		
				if ( babyDmax >= FSATS.NormalMaxDam )
					babyDmax = FSATS.NormalMaxDam;
					if (DMaxCapExempt >= babyDmax)
						babyDmax = DMaxCapExempt;
				
				if ( babyHits >= 2000 )
					babyHits = 2000;
				
				if ( babyStam >= 750 )
					babyStam = 750;
				
				if ( babyMana >= 2000 )
					babyMana = 2000;

				if ( babyMlev >= 60 )
					babyMlev = 60;

				foreach ( Mobile m in from.GetMobilesInRange( 5 ) )
				{
					if ( m is AnimalBreeder )
						breeder = m;

					if ( m == cm1 )
						owner = m;
				}

				if ( breeder == null )
				{
					from.SendMessage( "You must be near an animal breeder in order to breed your pet." );

					if ( cm1 != null )
						cm1.SendMessage( "The owner of the other pet is too far away from the animal breeder." );
				}
				else if ( owner == null )
				{
					from.SendMessage( "The owner of the other pet is not near by." );

					if ( cm1 != null )
						cm1.SendMessage( "You are to far away from the other pet owner." );
				}
				else if ( Utility.Random( chance ) < 1500 )
				{
					if ( cm1 != null ) //Generate Claim Ticket One
					{
						PetClaimTicket pct = new PetClaimTicket();
						pct.AI = ai;
						pct.Owner = cm1;
						pct.Pet = m_Pet1;
						pct.Str = babyStr;
						pct.Dex = babyDex;
						pct.Int = babyInt;
						pct.Hits = babyHits;
						pct.Stam = babyStam;
						pct.Mana = babyMana;
						pct.Phys = babyPhys;
						pct.Fire = babyFire;
						pct.Cold = babyCold;
						pct.Nrgy = babyNrgy;
						pct.Pois = babyPois;
						pct.Dmin = babyDmin;
						pct.Dmax = babyDmax;
						pct.Mlev = babyMlev;
						pct.Gen = bc1.Generation;
						pct.Price = babyPrice;
						cm1.AddToBackpack( pct );

						breeder.SayTo( cm1, "Ill hold onto your pet for you while its mating." );
						breeder.SayTo( cm1, "Return here in three days and the show me that claim ticket i gave to you." );
						cm1.SendMessage( "They have accepted your offer." );

						bc1.ControlTarget = null;
						bc1.ControlOrder = OrderType.Stay;
						bc1.Internalize();

						bc1.SetControlMaster( null );
					}

					if ( cm2 != null ) //Generate Claim Ticket One
					{
						PetClaimTicket pct = new PetClaimTicket();
						pct.AI = ai;
						pct.Owner = cm2;
						pct.Pet = m_Pet2;
						pct.Str = babyStr;
						pct.Dex = babyDex;
						pct.Int = babyInt;
						pct.Hits = babyHits;
						pct.Stam = babyStam;
						pct.Mana = babyMana;
						pct.Phys = babyPhys;
						pct.Fire = babyFire;
						pct.Cold = babyCold;
						pct.Nrgy = babyNrgy;
						pct.Pois = babyPois;
						pct.Dmin = babyDmin;
						pct.Dmax = babyDmax;
						pct.Mlev = babyMlev;
						pct.Gen = bc2.Generation;
						pct.Price = babyPrice;
						cm2.AddToBackpack( pct );

						breeder.SayTo( cm2, "Ill hold onto your pet for you while its mating." );
						breeder.SayTo( cm2, "Return here in three days and the show me that claim ticket i gave to you." );
						cm2.SendMessage( "You accept their offer." );

						bc2.ControlTarget = null;
						bc2.ControlOrder = OrderType.Stay;
						bc2.Internalize();

						bc2.SetControlMaster( null );
					}

					if ( bc1 != null || bc2 != null )
					{
						bc1.MatingDelay = DateTime.Now + TimeSpan.FromMinutes( 144.0 );
						bc2.MatingDelay = DateTime.Now + TimeSpan.FromMinutes( 144.0 );
					}
				}
				else
				{
					if ( cm1 != null && cm2 != null )
					{
						cm1.SendMessage( "Breeding Failed: It is hard to successfully mate two strong pets together, You will have to wait twelve hours to try again." );
						cm2.SendMessage( "Breeding Failed: It is hard to successfully mate two strong pets together, You will have to wait twelve hours to try again." );
						bc1.MatingDelay = DateTime.Now + TimeSpan.FromMinutes( 12.0 );
						bc2.MatingDelay = DateTime.Now + TimeSpan.FromMinutes( 12.0 );
					}
				}
			}

			//Decline
			if ( info.ButtonID == 3 )
			{
				from.SendMessage( "You have declined their offer." );

				if ( cm1 != null )
					cm1.SendMessage( "They have declined your offer" );
			}
		}
	}
}