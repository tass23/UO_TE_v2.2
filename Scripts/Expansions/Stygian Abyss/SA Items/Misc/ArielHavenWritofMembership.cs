/*                                                             .---.
                                                              /  .  \
                                                             |\_/|   |
                                                             |   |  /|
  .----------------------------------------------------------------' |
 /  .-.                                                              |
|  /   \            Contribute To The Orbsydia SA Project            |
| |\_.  |                                                            |
|\|  | /|                        By Lotar84                          |
| `---' |                                                            |
|       |         (Orbanised by Orb SA Core Development Team)        | 
|       |                                                           /
|       |----------------------------------------------------------'
\       |
 \     /
  `---'

*/
using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
    public class ArielHavenWritofMembership : Item
    {
        public override int LabelNumber { get { return 1094998; } }//Ariel Haven Writ of Membership

        [Constructable]
        public ArielHavenWritofMembership()
            : base(0x2831)
        {
        }

        public ArielHavenWritofMembership(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
}
