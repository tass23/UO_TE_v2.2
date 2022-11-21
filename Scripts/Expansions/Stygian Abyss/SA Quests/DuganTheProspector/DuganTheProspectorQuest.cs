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

namespace Server.Engines.Quests
{
    public class Missing : BaseQuest
    {
        public override Type NextQuest { get { return typeof(EndingtheThreat); } }

        public override object Title { get { return 1094949; } }//Missing

        public override object Description { get { return 1094951; } }

        /*Very well, fare thee well traveler.  I would not press our problems upon you if you are not willing.  
          I pray that my people are simply trying to secure some treasure they found.*/
        public override object Refuse { get { return 1094952; } }

        /*Greetings, have you any news of my people?  I am encouraged to see you well!*/
        public override object Uncomplete { get { return 1094953; } }

        public Missing(): base()
        {
            AddObjective(new ObtainObjective(typeof(ArielHavenWritofMembership), "Ariel Haven Writ of Membership", 4, 0x2831));

            AddReward(new BaseReward(typeof(CandlewoodTorch), "Candlewood Torch"));
        }

        /*Oh, this is indeed sad news.  It seems my worst fears have been realized! 
          These writs were given to Evan and Kevin Brightwhistle, Sergio Taylor, and Sarah Bootwell.
          Based on your description of the ghastly scene, they have met a most untimely end!
          This is a great setback to our society as they were each great friends to me and an asset to the society.
          'Tis strange that there were only four bodies.... 
          There was a fifth member of the party, Neville Brightwhistle, but he was the youngest and least experienced of the party so if his elder brothers are lost, 
          surely young Neville met a similar fate. ‘Tis a tragedy, surely.
          Please take this torch with my thanks.  It may not seem like much, but it is magic and will never burn out.  
          You will find that rotworms fear fire so it will protect you from them as you venture further into these cursed halls.
          Tread carefully traveller, each member of the party had one of these so I suspect that the rotworms are not what ended their lives.*/
        public override object Complete { get { return 1094956; } }

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

    public class EndingtheThreat : BaseQuest
    {

        public override object Title { get { return 1095012; } }//Ending the Threat

        /*So be it, traveler.  I thank you for the courage you have shown in the past.  Speak to me again if you change your mind.*/
        public override object Description { get { return 1095014; } }

        /*So be it, traveler.  I thank you for the courage you have shown in the past.  Speak to me again if you change your mind.*/
        public override object Refuse { get { return 1095015; } }

        /*The goblins must learn again to fear humans and elves.
          They can be cowed into submission, but if it is not done quickly they will become emboldened and start to venture out of the mountain to the villages and other settlements.  
          You must not let that happen.*/
        public override object Uncomplete { get { return 1095016; } }

        public EndingtheThreat(): base()
        {
            AddObjective(new SlayObjective(typeof(GrayGoblin), "Gray Goblin", 10));

            AddReward(new BaseReward(typeof(LargeTreasureBag), 1072706));
        }
        /*Oh, have mercy on us!  Have you come to kill every one of us? 
          Take what you will and go!  Your kind is more terrible than the mistress!  
          Woe are we, the gray goblins, we serve the mistress and yet she leaves us to be...*/
        public override object Complete { get { return 1095017; } }

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
