using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using System.Collections;

namespace Server.Gumps
{
	public class PetStatGump : Gump
	{
		private BaseCreature m_Bc;
		private Mobile m_From;

		public PetStatGump( BaseCreature pet, Mobile from ) : base( 25, 25 )
		{
			m_Bc = pet;
			m_From = from;

			BaseCreature bc = (BaseCreature)pet;
			Mobile cm = bc.ControlMaster;

			int nextLevel = bc.NextLevel * bc.Level;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddBackground(24, 25, 346, 289, 5054);
			AddLabel(33, 27, 1160, bc.Name.ToString() );
			AddImageTiled(27, 47, 342, 10, 5121);
			AddLabel(35, 55, 1149, @"Exp:");
			AddLabel(35, 75, 1149, @"Current Level:");
			AddLabel(35, 95, 1149, @"Maxium Level:");
			AddLabel(35, 115, 1149, @"Exp Till Next Level:");
			AddImageTiled(27, 135, 342, 10, 5121);
			AddLabel(35, 145, 1149, @"Gender:");
			AddLabel(35, 165, 1149, @"Can Mate:");
			AddImageTiled(27, 185, 342, 10, 5121);
			AddImageTiled(27, 275, 342, 10, 5121);
			if ( m_From == cm && bc.AllowMating == true && FSATS.EnablePetBreeding == true )
			{
				AddButton(36, 281, 4008, 4009, 1, GumpButtonType.Reply, 0);
				AddLabel(71, 282, 1160, @"Mate this pet with another.");
			}
			if ( bc.AbilityPoints != 0 && m_From == cm )
			{
				AddButton(250, 281, 2474, 2473, 3, GumpButtonType.Reply, 0);
			}
			AddButton(287, 281, 4017, 4018, 2, GumpButtonType.Reply, 0);
			AddLabel(322, 282, 1160, @"Close");
			AddLabel(35, 195, 1149, @"Ability Points:");
			AddLabel(35, 215, 1149, @"Evolves:");
			AddLabel(35, 235, 1149, @"Owner:");
			AddLabel(35, 255, 1149, @"Generation:");
			AddPage(1);
			AddLabel(65, 55, 64, bc.Exp.ToString() );
			AddLabel(129, 75, 64, bc.Level.ToString() );
			AddLabel(125, 95, 64, bc.MaxLevel.ToString() );
			AddLabel(156, 115, 64, nextLevel.ToString() );
			if ( bc.Female != true )
			{
				AddLabel(87, 145, 64, @"Male");
			}
			else
			{
				AddLabel(87, 145, 64, @"Female");
			}
			if ( bc.AllowMating != true )
			{
				AddLabel(101, 165, 64, @"No");
			}
			else
			{
				AddLabel(101, 165, 64, @"Yes");
			}

			AddLabel(125, 195, 64, bc.AbilityPoints.ToString() );

			if ( bc.Evolves != true )
			{
				AddLabel(87, 215, 64, @"No");
			}
			else
			{
				AddLabel(87, 215, 64, @"Yes");
			}
			if ( cm != null )
			{
				AddLabel(81, 235, 64, cm.Name.ToString() );
			}
			else
			{
				AddLabel(81, 235, 64, @"unowned" );
			}
			AddLabel(112, 255, 64, bc.Generation.ToString() );

		}
		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

			if ( info.ButtonID == 1 )
			{
				Mobile breeder = new Mobile();

				foreach ( Mobile m in from.GetMobilesInRange( 5 ) )
				{
					if ( m is AnimalBreeder )
						breeder = m;
				}

				if ( breeder is AnimalBreeder )
				{
					from.SendMessage( "What creature would you like your pet to breed with?" );
           				from.Target = new BeginMatingTarget( m_Bc );
				}
				else
				{
					from.SendMessage( "You must be near an animal breeder in order to breed your pet." );
				}
			}

			if ( info.ButtonID == 2 )
			{
			}

			if ( info.ButtonID == 3 )
			{
				from.SendGump( new PetLevelGump( m_Bc ) );
			}
		}
	}

  	public class BeginMatingTarget : Target 
      	{
		private BaseCreature m_Pet;

         	public BeginMatingTarget( BaseCreature pet ) : base ( 10, false, TargetFlags.None ) 
         	{ 
            		m_Pet = pet; 
         	} 
          
         	protected override void OnTarget( Mobile from, object target ) 
         	{
			if ( target is PlayerMobile )
			{
				from.SendMessage( "Huh? But the children would be so ugly" );
			}
			else if ( target is BaseCreature )
			{
				BaseCreature bc = (BaseCreature)target;

				Type targettype = bc.GetType();
				Type pettype = m_Pet.GetType();
				Mobile breeder = new Mobile();
				Mobile owner = new Mobile();

				foreach ( Mobile m in bc.GetMobilesInRange( 5 ) )
				{
					if ( m is AnimalBreeder )
						breeder = m;

					if ( m == bc.ControlMaster )
						owner = m;
				}

				if ( bc.Controlled != true )
				{
					from.SendMessage( "That creature is not tamed." );
				}
				else if ( bc.ControlMaster == null )
				{
					from.SendMessage( "That creature has no master." );
				}
				else if ( bc.MatingDelay >= DateTime.Now )
				{
					from.SendMessage( "That creature has mated in that last six days, It cannot mate again so soon." );
				}
				else if ( bc.ControlMaster == from )
				{
					from.SendMessage( "You cannot breed two of your own pets together, You must find another player who has the same type of pet as your in order to breed." );
				}
				else if ( breeder == null )
				{
					from.SendMessage( "You must be near an animal breeder in order to breed your pet." );
				}
				else if ( owner == null )
				{
					from.SendMessage( "The owner of that pet is not near by to confirm mating between the two pets." );
				}
				else if ( targettype != pettype )
				{
					from.SendMessage( "You cannot crossbreed two different species together." );
				}
				else if ( bc.AllowMating != true )
				{
					from.SendMessage( "This creature is not at the correct level to breed yet." );
				}
				else if ( bc.Female == m_Pet.Female )
				{
					from.SendMessage( "You cannot breed two pets of the same gender together." );
				}
				else
				{
					from.SendGump( new AwaitingConfirmationGump( m_Pet, bc ) );
					owner.SendGump( new BreedingAcceptGump( m_Pet, bc ) );	
				}
			}
			else
			{
				from.SendMessage( "Your pet cannot breed with that." );
			}
		}
	}
}