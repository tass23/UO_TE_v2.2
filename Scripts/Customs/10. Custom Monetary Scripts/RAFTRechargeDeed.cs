using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;
using Server;

namespace Server.Items
{
    public class RAFTRechargeTarget : Target
    {
        private RAFTRechargeDeed m_Deed;

        public RAFTRechargeTarget(RAFTRechargeDeed deed)
            : base(1, false, TargetFlags.None)
        {
            m_Deed = deed;
        }

        protected override void OnTarget(Mobile from, object target)
        {
            if (m_Deed.Deleted || m_Deed.RootParent != from)
            {
                from.SendMessage("You cannot reset the recharges on that.");
                return;
            }
            if (target is RAFTRS)
			{
				RAFTRS item = (RAFTRS)target;
				if (item is RAFTRS)
				{
					if (((RAFTRS)item).MaxRecharges == 0)
					{
						from.SendMessage("That R.A.F.T. has already had the Recharges reset.");
					}
					else
                    {
                        ((RAFTRS)item).Recharges = 0;
                        from.SendMessage("Your R.A.F.T.'s Recharges have been reset to 0.");
                        m_Deed.Delete();
                    }
				}
			}
            else
            {
                from.SendMessage("You cannot reset the recharges on that.");
            }
        }
    }

    public class RAFTRechargeDeed : Item
    {
		public override bool ForceShowProperties { get { return ObjectPropertyList.Enabled; } }
		
        [Constructable]
        public RAFTRechargeDeed()  : base( 18095 )
        {
            Name = "R.A.F.T. Recharges Deed";
            Hue = 85;
			LootType = LootType.Blessed;
        }
		
		public override void AddNameProperties( ObjectPropertyList list )
 		{
			base.AddNameProperties( list );
			list.Add( "R.A.F.T. Recharges 0" );
 		}

        public RAFTRechargeDeed(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!IsChildOf(from.Backpack))
            {
                from.SendMessage("The deed needs to be in your pack.");
            }
            else
            {
                from.SendMessage("Please target your R.A.F.T. in your backpack.");
                from.Target = new RAFTRechargeTarget(this);
            }
        }
    }
}