/*                                                             .---.
                                                              /  .  \
                                                             |\_/|   |
                                                             |   |  /|
  .----------------------------------------------------------------' |
 /  .-.                                                              |
|  /   \         Contribute To The Orbsydia SA Project               |
| |\_.  |                                                            |
|\|  | /|                        By Lotar84                          |
| `---' |                                                            |
|       |       (Orbanised by Orb SA Core Development Team)          | 
|       |                                                           /
|       |----------------------------------------------------------'
\       |
 \     /
  `---'
*/
using System;
using Server;
using Server.Items;
using Server.Engines.Quests;
using Reward = Server.Engines.Quests.BaseReward;
using Server.Engines.Craft;

namespace Server.Items
{
    public class JaacarBox : WoodenBox
    {

        public override string DefaultName
        {
            get { return "Jaacar Reward Box"; }
        }

        [Constructable]
        public JaacarBox()
            : base()
        {
            Movable = true;
            Hue = 1266;

            DropItem(Reward.CookRecipe());

        }


        public JaacarBox(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}