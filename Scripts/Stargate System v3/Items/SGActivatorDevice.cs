using System;
using System.Text;
using System.Collections;

using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;

namespace Server.SG
{
    public class SGActivatorDevice : Item
    {
        public int m_SGAXloc = 0;
        public int m_SGAYloc = 0;
        public int m_SGAZloc = 0;
        public string m_SGAFacing = "S";
        public int m_SGAStyle = 0;

        public bool m_SGACanBeUsed = false;
        public bool m_SGABeingUsed = false;
        public bool m_SGAEnergyField = false;
        public bool m_SGAHiddenLoc = true;

        public string m_SGADiscovererName = "UnSet";
        public string m_SGALocationName = "UnSet";

        public int m_SGAFacetNumber = 1;
        public int m_SGAAddressCode1 = 1;
        public int m_SGAAddressCode2 = 2;
        public int m_SGAAddressCode3 = 3;
        public int m_SGAAddressCode4 = 4;
        public int m_SGAAddressCode5 = 5;

        public string m_WhoClickedIt = "....";
        public int m_TimerCounter = 0;

        #region Gets/Sets
        [CommandProperty(AccessLevel.GameMaster)]
        public int SGAXloc
        { get { return m_SGAXloc; } set { m_SGAXloc = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int SGAYloc
        { get { return m_SGAYloc; } set { m_SGAYloc = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int SGAZloc
        { get { return m_SGAZloc; } set { m_SGAZloc = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public string SGAFacing
        { get { return m_SGAFacing; } set { m_SGAFacing = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int SGAStyle
        { get { return m_SGAStyle; } set { m_SGAStyle = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool SGACanBeUsed
        { get { return m_SGACanBeUsed; } set { m_SGACanBeUsed = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool SGABeingUsed
        { get { return m_SGABeingUsed; } set { m_SGABeingUsed = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool SGAEnergyField
        { get { return m_SGAEnergyField; } set { m_SGAEnergyField = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool SGAHiddenLoc
        { get { return m_SGAHiddenLoc; } set { m_SGAHiddenLoc = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public string SGADiscovererName
        { get { return m_SGADiscovererName; } set { m_SGADiscovererName = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public string SGALocationName
        { get { return m_SGALocationName; } set { m_SGALocationName = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int SGAFacetNumber
        { get { return m_SGAFacetNumber; } set { m_SGAFacetNumber = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int SGAAddressCode1
        { get { return m_SGAAddressCode1; } set { m_SGAAddressCode1 = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int SGAAddressCode2
        { get { return m_SGAAddressCode2; } set { m_SGAAddressCode2 = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int SGAAddressCode3
        { get { return m_SGAAddressCode3; } set { m_SGAAddressCode3 = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int SGAAddressCode4
        { get { return m_SGAAddressCode4; } set { m_SGAAddressCode4 = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int SGAAddressCode5
        { get { return m_SGAAddressCode5; } set { m_SGAAddressCode5 = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public string WhoClickedIt
        { get { return m_WhoClickedIt; } set { m_WhoClickedIt = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int TimerCounter
        { get { return m_TimerCounter; } set { m_TimerCounter = value; } }

        #endregion

        [Constructable]
        public SGActivatorDevice(int sgaxloc, int sgayloc, int sgazloc, string sgafacing, int sgastyle, bool canbeused, bool beingused, bool energy, bool hidden, string discover, string locname, int facetID, int sgac1, int sgac2, int sgac3, int sgac4, int sgac5) : base(13803)
        {
            m_SGAXloc = sgaxloc;
            m_SGAYloc = sgayloc;
            m_SGAZloc = sgazloc;
            m_SGAFacing = sgafacing;
            m_SGAStyle = sgastyle;

            m_SGACanBeUsed = canbeused;
            m_SGABeingUsed = beingused;
            m_SGAEnergyField = energy;
            m_SGAHiddenLoc = hidden;

            m_SGADiscovererName = discover;
            m_SGALocationName = locname;

            m_SGAFacetNumber = facetID;
            m_SGAAddressCode1 = sgac1;
            m_SGAAddressCode2 = sgac2;
            m_SGAAddressCode3 = sgac3;
            m_SGAAddressCode4 = sgac4;
            m_SGAAddressCode5 = sgac5;

            m_WhoClickedIt = "Blank";
            m_TimerCounter = 0;

            Movable = false;
            Weight = 10;
            Name = "Crystal Control Device";
            Hue = 2963;

        }

        public override void OnDoubleClick(Mobile from)
        {
            from.CloseGump(typeof(SGGumpActivatorDevice));

            if (!from.InRange(this.GetWorldLocation(), 1))
            {
                from.SendMessage(77, "Step Closer, You Are Too Far Away");
                from.PlaySound(0x1F0);
            }
            else
            {
                for (int i = 0; i < SGCore.SGList.Count; i++)
                {
                    SGEntry sge = (SGEntry)SGCore.SGList[i];
                    if (sge.SGFacetCode == m_SGAFacetNumber && sge.SGAddressCode1 == m_SGAAddressCode1 && sge.SGAddressCode2 == m_SGAAddressCode2 && sge.SGAddressCode3 == m_SGAAddressCode3 && sge.SGAddressCode4 == m_SGAAddressCode4 && sge.SGAddressCode5 == m_SGAAddressCode5)
                    {
                        m_SGACanBeUsed = sge.SGCanBeUsed;
                        m_SGAEnergyField = sge.SGEnergy;
                    }
                }

                if (!m_SGACanBeUsed && from.AccessLevel < AccessLevel.GameMaster)
                {
                    from.SendMessage(77, "This Device Is Disabled, Contact Shard Admin.");
                }
                else
                {
                    if (m_SGAHiddenLoc)
                    {
                        from.SendMessage(77, "Congratulations, Your first to discover this stargate, click again to start using it.");
                        m_SGAHiddenLoc = false;
                        m_SGADiscovererName = from.Name;

                        for (int i = 0; i < SGCore.SGList.Count; i++)
                        {
                            SGEntry sge = (SGEntry)SGCore.SGList[i];
                            if (sge.SGFacetCode == m_SGAFacetNumber && sge.SGAddressCode1 == m_SGAAddressCode1 && sge.SGAddressCode2 == m_SGAAddressCode2 && sge.SGAddressCode3 == m_SGAAddressCode3 && sge.SGAddressCode4 == m_SGAAddressCode4 && sge.SGAddressCode5 == m_SGAAddressCode5)
                            {
                                sge.SGHidden = false;
                                sge.SGDiscovered = from.Name;
                            }
                        }
                    }
                    else
                    {
                        if (m_SGABeingUsed && m_WhoClickedIt != from.Name)
                        {
                            from.SendMessage(77, "Someone else is using this control device, please wait until there finished.");
                        }
                        else
                        {
                            if (m_SGAEnergyField)
                            {
                                from.SendMessage(77, "This stargate is active, please wait until it becomes free again.");
                                m_WhoClickedIt = "Blank";
                                m_TimerCounter = 0;
                            }
                            else
                            {
                                m_SGABeingUsed = true;
                                m_WhoClickedIt = from.Name;
                                m_TimerCounter++;

                                int sc = 1; // Setup value for selection counter
                                int myfacet = 0;
                                int filleraddress = 1;
                                string whichmap = Convert.ToString(from.Map);
                                if (whichmap == "Felucca")
                                {
                                    myfacet = 1;
                                }
                                if (whichmap == "Trammel")
                                {
                                    myfacet = 2;
                                }
                                if (whichmap == "Ilshenar")
                                {
                                    myfacet = 3;
                                }
                                if (whichmap == "Malas")
                                {
                                    myfacet = 4;
                                }
                                if (whichmap == "Tokuno")
                                {
                                    myfacet = 5;
                                }

                                if (m_TimerCounter == 1)
                                {
                                    from.SendMessage(77, "You access the device controls...");
                                    this.Hue = 2964;
                                    Effects.PlaySound(this.Location, this.Map, SGCore.SGSystemSoundGumpOpen);
                                    new SGActivatorDeviceTimer(this).Start(); // Starts Colour changes
                                }

                                from.SendGump(new SGGumpActivatorDevice(from, this, sc, myfacet, filleraddress, filleraddress, filleraddress, filleraddress, filleraddress));
                            }
                        }
                    }
                }
            }
        }

        #region TimerCode
        private class SGActivatorDeviceTimer : Timer
        {
            public int PulseTimer = 0;

            private SGActivatorDevice m_sgactivatordevice;

            public SGActivatorDeviceTimer(SGActivatorDevice m) : base(TimeSpan.FromSeconds(SGCore.SGSystemBlink), TimeSpan.FromSeconds(SGCore.SGSystemBlink))
            {
                m_sgactivatordevice = m;
            }
            protected override void OnTick()
            {
                if (m_sgactivatordevice.m_SGABeingUsed)
                {
                    Effects.PlaySound(m_sgactivatordevice.Location, m_sgactivatordevice.Map, 0xED);
                    PulseTimer++;

                    if (PulseTimer >= 60)
                    {
                        foreach (NetState state in NetState.Instances)
                        {
                            Mobile mob = state.Mobile;
							if (mob == null)
							{
								return;
							}
                            if (mob.Name == m_sgactivatordevice.m_WhoClickedIt)
                            {
                                mob.CloseGump(typeof(SGGumpActivatorDevice));
                                if ((SGCore.SGSystemBlink * PulseTimer) <= 60)
                                {
                                    mob.SendMessage(77, "Crystal control device's auto close after {0} seconds if there not being used.", (SGCore.SGSystemBlink * PulseTimer).ToString());
                                }
                                else
                                {
                                    mob.SendMessage(77, "Crystal control device's auto close after {0} minutes if there not being used.", ( (SGCore.SGSystemBlink * PulseTimer) / 60).ToString());
                                }
                            }
                        }
                        m_sgactivatordevice.m_SGABeingUsed = false; // release dialer
                        m_sgactivatordevice.m_WhoClickedIt = "Blank"; // Reset Who clicked it value
                        m_sgactivatordevice.m_TimerCounter = 0; // Reset Timer Counter
                    }

                    if (m_sgactivatordevice.Hue == 2964)
                    {
                        m_sgactivatordevice.Hue = 2965;
                    }
                    else if (m_sgactivatordevice.Hue == 2965)
                    {
                        m_sgactivatordevice.Hue = 2966;
                    }
                    else if (m_sgactivatordevice.Hue == 2966)
                    {
                        m_sgactivatordevice.Hue = 2963;
                    }
                    else if (m_sgactivatordevice.Hue == 2963)
                    {
                        m_sgactivatordevice.Hue = 2964;
                    }
                }
                else
                {
                    this.Stop();

                    if (!SGCore.SGSystemEnabled)
                    {
                        m_sgactivatordevice.Hue = 39;
                        m_sgactivatordevice.m_SGACanBeUsed = false;
                        m_sgactivatordevice.m_SGABeingUsed = false;
                        m_sgactivatordevice.m_SGAEnergyField = false;
                    }
                    else
                    {
                        m_sgactivatordevice.Hue = 2963;
                        m_sgactivatordevice.m_SGACanBeUsed = true;
                        m_sgactivatordevice.m_SGABeingUsed = false;
                        m_sgactivatordevice.m_SGAEnergyField = false;
                    }
                }
            }
        }
        #endregion TimerCode

        public SGActivatorDevice(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version

            writer.Write((int)m_SGAXloc);
            writer.Write((int)m_SGAYloc);
            writer.Write((int)m_SGAZloc);
            writer.Write((string)m_SGAFacing);
            writer.Write((int)m_SGAStyle);

            writer.Write((bool)m_SGACanBeUsed);
            writer.Write((bool)m_SGABeingUsed);
            writer.Write((bool)m_SGAEnergyField);
            writer.Write((bool)m_SGAHiddenLoc);

            writer.Write((string)m_SGADiscovererName);
            writer.Write((string)m_SGALocationName);

            writer.Write((int)m_SGAFacetNumber);
            writer.Write((int)m_SGAAddressCode1);
            writer.Write((int)m_SGAAddressCode2);
            writer.Write((int)m_SGAAddressCode3);
            writer.Write((int)m_SGAAddressCode4);
            writer.Write((int)m_SGAAddressCode5);

            writer.Write((string)m_WhoClickedIt);
            writer.Write((int)m_TimerCounter);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            m_SGAXloc = reader.ReadInt();
            m_SGAYloc = reader.ReadInt();
            m_SGAZloc = reader.ReadInt();
            m_SGAFacing = reader.ReadString();
            m_SGAStyle = reader.ReadInt();

            m_SGACanBeUsed = reader.ReadBool();
            m_SGABeingUsed = reader.ReadBool();
            m_SGAEnergyField = reader.ReadBool();
            m_SGAHiddenLoc = reader.ReadBool();

            m_SGADiscovererName = reader.ReadString();
            m_SGALocationName = reader.ReadString();

            m_SGAFacetNumber = reader.ReadInt();
            m_SGAAddressCode1 = reader.ReadInt();
            m_SGAAddressCode2 = reader.ReadInt();
            m_SGAAddressCode3 = reader.ReadInt();
            m_SGAAddressCode4 = reader.ReadInt();
            m_SGAAddressCode5 = reader.ReadInt();

            m_WhoClickedIt = reader.ReadString();
            m_TimerCounter = reader.ReadInt();
        }
    }
}