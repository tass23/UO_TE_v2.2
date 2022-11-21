using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public class NewbieMoongate : Moongate
	{
		[Constructable]
		public NewbieMoongate()
		{
			Name = "Newbie Moongate";
		}

		[Constructable]
		public NewbieMoongate(Serial serial) : base(serial)
		{
		}

        public override bool ValidateUse(Mobile from, bool message)
        {
            if( from is PlayerMobile )
            {
                if ((from.SkillsTotal > 15000))
                {
                    from.SendMessage("You are to skilled to enter the Newbie Dungeon, look for other places to hunt.");
                    return false;
                }
            }
            return base.ValidateUse(from, message);
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}

/*public static bool HasAccess( Mobile mob, RewardList list, out TimeSpan ts )
                {
                        if ( list == null )
                        {
                                ts = TimeSpan.Zero;
                                return false;
                        }
 
                        Account acct = mob.Account as Account;
 
                        if ( acct == null )
                        {
                                ts = TimeSpan.Zero;
                                return false;
                        }
 
                        TimeSpan totalTime = (DateTime.Now - acct.Created);
 
                        ts = ( list.Age - totalTime );
 
                        if ( ts <= TimeSpan.Zero )
                                return true;
 
                        return false;
                }*/
