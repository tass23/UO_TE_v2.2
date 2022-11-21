using System;
using System.Net;
using System.Net.Sockets;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Spells;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using Server.Accounting;
using Server.Targeting;
using Server.Commands;

namespace Server.Scripts.Commands
{
    public class BondTimeCommand //: BaseCommand
    {
        public static void Initialize()
        {
            CommandSystem.Register("BondTime", AccessLevel.Player, new CommandEventHandler(BondTime));

        }
        public static void BondTime (CommandEventArgs e)
        {
            e.Mobile.SendMessage("Which pet do you wish to check?");
            e.Mobile.Target = new BondTimeTarget();
        }
        public class BondTimeTarget : Target
        {
            public BondTimeTarget()
                : base(15, false, TargetFlags.None)
            {

            }

            protected override void OnTarget(Mobile from, object obj)
            {
                if (!(obj is Mobile) || !(obj is BaseCreature))
                {
                    from.SendMessage("Error! Must target pets.");
                    return;
                }
                else
                {
					BaseCreature m = (BaseCreature)obj;             
					if (m.Controlled == false)
					{
						from.SendMessage("Error! This pet is not tame.");
						return;

					}
					else
					{
						if (m.IsBonded == true)
						{
							from.SendMessage("Error! This pet has already been bonded.");
							return;
						}
						else 
						{                    
							if (!(m.ControlMaster == from) && from.AccessLevel < AccessLevel.GameMaster)
							{
								from.SendMessage("Error! This pet doesn't belong to you.");
								return;
							}

							else
							{
                                if (m.IsBonded == true)
                                {
                                    from.SendMessage("Error! This pet has already been bonded");
                                    return;
                                }
                                else
                                {
                                    if (DateTime.Now - m.BondingBegin > TimeSpan.FromDays (7))
                                    {
                                        from.SendMessage("Bonding of this pet did not start yet. Feed it again having required Animal Taming skill level");
                                        return;
                                    }
                                    else
                                    {
                                        TimeSpan timeleft = m.BondingBegin + TimeSpan.FromDays(7) - DateTime.Now;
                                        from.SendMessage("{0} days, {1} hours, {2} minutes and {3} seconds ramaining until this pet bonds with you. Pet will bond with you on {4}", timeleft.Days, timeleft.Hours, timeleft.Minutes, timeleft.Seconds, m.BondingBegin + TimeSpan.FromDays (7));
                                    }
                                }
							}
						}
					}
				}
            }
        }
    }
}