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
using System.Collections;
using Server.Items;
using Server.Mobiles;
using Server.Engines.Craft;
using System.Collections.Generic;
using Server.Engines.Quests;

namespace Server.Engines.Quests
{
    public class BadCompany : BaseQuest
    {
        public override Type NextQuest { get { return typeof(ATangledWeb); } }

        /*Bad Company*/
        public override object Title { get { return 1095022; } }

        /*Travel to the Green Goblin area and kill Green Goblins until they fear you.  
          Return to Jaacar for your reward.Jaacar make friends with your kind.  
          Not want violence... not eat each other!  Jaacar eat rotworms... Rotworm stew good!  
          Make Jaacar smart!<br><br>We can be friends, yes?  Outside kind and inside kind be friends?  This is good, yes?  
          Jaacar knows who hates your kind; Gray Goblins not hate you. We want to be friends!  Jaacar want to warn you!
          Yes, friends give good warnings!<br><br>Green Goblins bad, very bad.  Green Goblins building up piles of weapons!  
          When green goblins get enough weapons, they make war with the outside kind... Your kind!  They come in the night and stab my new friend with own sword!  
          They will!  I swear!<br><br>Gray Goblins know this, that is why we fight them!  We protect our friend!  You, our friend!  Will you help stop Green Goblins?  
          If you help, Jaacar give some of smart knowledge! Help each other, yes?*/
        public override object Description { get { return 1095024; } }

        /*Oh poor friend.  Not believe Jaacar.  You will see.  Maybe too late, but you will see.*/
        public override object Refuse { get { return 1095025; } }

        /*Friend make Green Goblins dead yet?  Make them go squish?  If no green squish, they kill you when you sleep!  They will!*/
        public override object Uncomplete { get { return 1095026; } }

        public BadCompany()
            : base()
        {

            AddObjective(new SlayObjective(typeof(GreenGoblin), "GreenGoblin", 10));

            AddReward(new BaseReward(typeof(JaacarBox), "Bowl of Rotworm Stew Recipe"));
        }
        /*Oh, have mercy on us!  Have you come to kill every one of us?  Take what you will and go!
          Your kind is more terrible than the master!  Woe are we, the green goblins, we serve the master's plan and yet he... *gasp**/
        public override object Complete { get { return 1095030; } }

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

    public class ATangledWeb : BaseQuest
    {
        /*A Tangled Web*/
        public override object Title { get { return 1095032; } }

        /*Kill Bloodworms and Blood Elementals to fill Jaacar's barrel.
          Return to Jaacar with the filled barrel for your reward
          Will friend help Jaacar with small errand for big friend?  
          Jaacar need big barrel full of blood.  Can friend do that?  
          Best place to get blood is blood elementals and bloodworms nearby.  
          If you do, Jaacar give to you special present!  More special than favorite recipe!*/
        public override object Description { get { return 1095034; } }

        /*Filling barrel not gross!  Filling barrel helps friend!  You think and then come back and help.  Yes, friend is big help!*/
        public override object Refuse { get { return 1095035; } }

        /*Jaacar need barrel filled all the way to the top!  Good friend, go fill the barrel for Jaacar.*/
        public override object Uncomplete { get { return 1095036; } }

        public ATangledWeb(): base()
        {
            AddObjective(new BloodCreaturesObjective(typeof(IBloodCreature), "blood creatures", 12));

            AddReward(new BaseReward(typeof(LargeTreasureBag), 1072706));
        }
        public override void OnCompleted()
        {
            Owner.SendLocalizedMessage(1095038, null, 0x23); // Jaacar's barrel is completely full. Return to Jaacar for your reward.							
            Owner.PlaySound(CompleteSound);
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

        private class BloodCreaturesObjective : SlayObjective
        {
            public BloodCreaturesObjective(Type creature, string name, int amount) : base(creature, name, amount)
            {
            }

            public override void OnKill(Mobile killed)
            {
                base.OnKill(killed);

                if (!Completed)
                    Quest.Owner.SendLocalizedMessage(1095037); // Blood from the creature goes into Jaacar’s barrel.
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
}