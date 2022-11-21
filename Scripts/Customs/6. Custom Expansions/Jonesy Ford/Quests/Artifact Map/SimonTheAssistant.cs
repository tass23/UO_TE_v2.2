using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Targeting;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Network;
using Server.Spells;
using Capt.MiniGames;

namespace Server.Mobiles
{
    [CorpseName("the corpse of Simon")]
    public class SimonTheAssistant : BaseCreature
    {
        private DateTime m_NextTalk;
        private string m_name;
        public DateTime NextTalk { get { return m_NextTalk; } set { m_NextTalk = value; } }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if (!m.Hidden && m is PlayerMobile)
            {
                if (DateTime.Now >= m_NextTalk && InRange(m, 4) && InLOS(m))
                {
                    m_name = m.Name;
                    switch (Utility.Random(3))
                    {
                        case 0: Say("Greetings " + m_name + ", how are you?"); break;
                        case 1: Say("I was just heading over to the museum for a lecture..."); break;
						case 2: Say("Have you heard about the Crimson Raiders?"); break;
                    };
                    m_NextTalk = (DateTime.Now + TimeSpan.FromSeconds(2));
                }
            }
        }
        public virtual bool IsInvulnerable { get { return true; } }
        [Constructable]
        public SimonTheAssistant()
            : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "Simon";
			Title = "the Assistant";
            SpeechHue = Utility.RandomDyedHue();
            Body = 400;
            Frozen = true;

            NameHue = 0x34;
			
			FancyShirt tunic = new FancyShirt();
			tunic.Movable = false;
			tunic.Hue = 51;
			AddItem( tunic );

			LeatherLegs legs = new LeatherLegs();
			legs.Movable = false;
			legs.Hue = 51;
			AddItem( legs );

			Boots boots = new Boots();
			boots.Movable = false;
			boots.Hue = 2051;
			AddItem( boots );
        }
        public override bool ClickTitle { get { return false; } }

        public SimonTheAssistant(Serial serial)
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
        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            Mobile m = from;
            PlayerMobile mobile = m as PlayerMobile;

            if (mobile != null)
            {
				if (dropped is AncientStatue)
                {
                    if (dropped.Amount != 1)
                    {
                        this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "I don't know what to do about our stolen items.", mobile.NetState);
                        return false;
                    }
                    dropped.Delete();
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Ah, you retrieved one of the stolen statues! Thank you! Here, this is the least I can do.", mobile.NetState);
					mobile.AddToBackpack(new ArtifactMap( 1, ( Map.Tokuno ) ));
                    return true;
                }
                if (dropped is AncientBrush)
                {
                    if (dropped.Amount != 1)
                    {
                        this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "It's a shame we had things stolen from the museum.", mobile.NetState);
                        return false;
                    }
                    dropped.Delete();
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Oh, you got one of the old hair brushes back! Thank you! I found this in an old chest yesterday.", mobile.NetState);
					mobile.AddToBackpack(new ArtifactMap( 2, ( Map.Tokuno ) ));
                    return true;
                }
                else if (dropped is AncientLetter)
                {
                    if (dropped.Amount != 1)
                    {
                        this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "The letters to Moses were stolen!", mobile.NetState);
                        return false;
                    }
                    dropped.Delete();
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Oh thank you for finding this! Now we can rebuild the collection.", mobile.NetState);
					mobile.AddToBackpack(new ArtifactMap( 3, ( Map.Tokuno ) ));
                    return true;
                }
                else if (dropped is AncientSalt)
                {
                    if (dropped.Amount != 1)
                    {
                        this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Queen Nefertiti's salt shaker was stolen!", mobile.NetState);
                        return false;
                    }
                    dropped.Delete();
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Oh goodness, you found it! I found this map that might be useful to you.", mobile.NetState);
					mobile.AddToBackpack(new ArtifactMap( 4, ( Map.Tokuno ) ));
                    return true;
                }
				else if (dropped is AncientPepper)
                {
                    if (dropped.Amount != 1)
                    {
                        this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "King Edward's pepper shaker has been pilfered!", mobile.NetState);
                        return false;
                    }
                    dropped.Delete();
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "I am so relieved you found this! You might find this more useful than I did.", mobile.NetState);
					mobile.AddToBackpack(new ArtifactMap( 5, ( Map.Tokuno ) ));
                    return true;
                }
                else if (dropped is AncientJewels)
                {
                    if (dropped.Amount != 1)
                    {
                        this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "My mummy's necklace was taken!", mobile.NetState);
                        return false;
                    }
                    dropped.Delete();
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Thank you for finding my mummy's necklace. Allow me to reward you with this map.", mobile.NetState);
					mobile.AddToBackpack(new ArtifactMap( 6, ( Map.Tokuno ) ));
                    return true;
                }

                else
                {
                    from.PrivateOverheadMessage(MessageType.Regular, 1153, false, "I should have never left that back door unlocked...", from.NetState);
                    return false;
                }
            }
            else
            {
                from.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Sorry, this isn't one of the items that was stolen.", from.NetState);
                return false;
            }
        }
    }
}
