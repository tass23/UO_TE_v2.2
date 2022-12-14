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
using Server.Mobiles;

namespace Server.Engines.Quests
{
    public class EscortToDugan : BaseQuest
    {
        /*The Lost Brightwhistle*/
        public override object Title { get { return 1095003; } }

        /*Escort Neville Brightwhistle to Elder Dugan.  After Neville is safe, speak to Elder Dugan for your reward.
          I was separated from my brothers when the goblins attacked.  I am a member of the Society of Ariel Haven, come to colonize these halls that we had though abandoned.
          I must get out of here and warn Elder Dugan that these creatures live here and are very dangerous!  Will you show me the way out?*/
        public override object Description { get { return 1095005; } }

        /*Oh, have mercy on me!  I will have to make it on my own.*/
        public override object Refuse { get { return 1095006; } }

        /*Is it much farther to the camp?*/
        public override object Uncomplete { get { return 1095007; } }

        public EscortToDugan() : base()
        {
            AddObjective(new EscortObjective("NPC Encampment"));

            AddReward(new BaseReward(typeof(TalismanofGoblinSlaying), "Talisman of Goblin Slaying"));
        }

        public override object Complete { get { return 1095008; } }

		public override void GiveRewards()
		{			
			base.GiveRewards();
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