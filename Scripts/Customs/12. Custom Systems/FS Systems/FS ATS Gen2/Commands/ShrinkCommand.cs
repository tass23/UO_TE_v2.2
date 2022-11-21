using System;
using Server;
using Server.Mobiles; 
using Server.Gumps; 
using Server.Targeting;
using Server.Items;

namespace Server.Commands
{
	public class ShrinkCommand
	{
		public static void Initialize()
		{
			CommandSystem.Register( "Shrink", AccessLevel.GameMaster, new CommandEventHandler( Shrink_OnCommand ) );
		}

		[Usage( "Shrink" )]
		[Description( "Shrinks a creature." )]
		private static void Shrink_OnCommand( CommandEventArgs e )
		{
			if ( e.Mobile is PlayerMobile )
			{
				e.Mobile.Target = new ShrinkTarget();
				e.Mobile.SendMessage("What would you like to Shrink?");
			}
		}
		public class ShrinkTarget : Target
		{

			public ShrinkTarget() : base( -1, true, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Item )
					from.SendMessage( "That cannot be shrunken." );

				else if ( o is PlayerMobile )
					from.SendMessage( "That cannot be shrunken." );

				/*else if ( o is RoninsBaseCreature && ( (RoninsBaseCreature)o).Pregnant == true )
					from.SendMessage( 53, "Warning! Shrinking a pet while pregnant could cause a server crash." );

				else if ( o is RoninsBaseCreature && ( (RoninsBaseCreature)o).IsMating == true )
					from.SendMessage( 53, "Warning! Shrinking a pet while mating could cause a server crash." );*/

				else if ( o is BaseCreature )
				{
          				BaseCreature c = (BaseCreature)o;
					Type type = c.GetType();
        				ShrinkItemX si = new ShrinkItemX();
					si.MobType = type;
					si.Pet = c;
					si.PetOwner = from;


					if ( c is BaseMount )
					{
						BaseMount mount = (BaseMount)c;
						si.MountID = mount.ItemID;
					}
        				from.AddToBackpack( si );

					c.Controlled = true; 
					c.ControlMaster = null;
					c.Internalize();

					c.OwnerAbandonTime = DateTime.MinValue;

					c.IsStabled = true;
				}
				else
					from.SendMessage( "You cannot shrink that, MOBILES ONLY" );
			}
		}
	}
}