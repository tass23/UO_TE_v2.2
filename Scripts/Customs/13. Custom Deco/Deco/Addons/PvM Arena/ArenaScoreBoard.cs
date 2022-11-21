//################################################################
//By Rocko Wang
//www.medievaldream.com
//Thanks to "The world of dreams" and Drocket for the idea of this
//################################################################


using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Server;
using Server.Network;
using Server.Game.Arena;
using Server.Gumps;

namespace Server.Items
{
    [Flipable(0x1E5E, 0x1E5F)]
    public class ArenaScoreBoard : Item
    {
        private ArenaControlStone m_Arena;

        [CommandProperty(AccessLevel.Administrator)]
        public ArenaControlStone Arena
        {
            get { return m_Arena; }
            set { m_Arena = value; }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (m_Arena != null && !m_Arena.Deleted && m_Arena.Running)
                from.SendGump(new ArenaScoreBoardGump(m_Arena));
            else
                from.SendMessage("The Arena is under maintainance; there is no score available at the moment.");
        }

        [Constructable]
        public ArenaScoreBoard()
            : base(0x1E5E)
        {
            Name = "Arena Score Board";
            Hue = 1161;
        }

        public ArenaScoreBoard(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
            writer.Write(m_Arena);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            switch (version)
            {
                case 0:
                    m_Arena = (ArenaControlStone)reader.ReadItem();
                    break;
            }
        }
    }
}