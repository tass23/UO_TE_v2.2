/* Created and gifted by Hammerhand */
/* Additional edits by Raist for UO-The Expanse */

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
    [CorpseName("the remains of a Hutt")]
    public class TassarineTheHutt : BaseCreature
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
                    switch (Utility.Random(2))
                    {
                        case 0: Say("Greetings " + m_name + ", have you brought the items requested?"); break;
                        case 1: Say("If you do not have my items, leave..."); break;
                    };
                    m_NextTalk = (DateTime.Now + TimeSpan.FromSeconds(2));
                }
            }
        }

        public virtual bool IsInvulnerable { get { return true; } }

        [Constructable]
        public TassarineTheHutt(): base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "Tassarine";
			Title = "the Hutt";
            SpeechHue = Utility.RandomDyedHue();
            Hue = 1437;
            Body = 0x100;
            Frozen = true;
            NameHue = 0x34;
        }

        public TassarineTheHutt(Serial serial): base(serial)
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
				if (dropped is TornScroll)
                {
                    if (dropped.Amount != 1)
                    {
                        this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "I have nothing to say to someone like you.", mobile.NetState);
                        return false;
                    }
                    dropped.Delete();
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Ah, you found my missing shipment manifest. Since you found it, you can fill it, now go bring me 1 Dagger.", mobile.NetState);
                    return true;
                }
                if (dropped is Dagger)
                {
                    if (dropped.Amount != 1)
                    {
                        this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "I need 1 Dagger.", mobile.NetState);
                        return false;
                    }
                    dropped.Delete();
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Now you must bring me 1 Longsword", mobile.NetState);
                    return true;
                }
                else if (dropped is Longsword)
                {
                    if (dropped.Amount != 1)
                    {
                        this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "I need 1 Longsword.", mobile.NetState);
                        return false;
                    }
                    dropped.Delete();
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "1 Kryss is what I require next.", mobile.NetState);
                    return true;
                }
                else if (dropped is Kryss)
                {
                    if (dropped.Amount != 1)
                    {
                        this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "I need 1 Kryss.", mobile.NetState);
                        return false;
                    }
                    dropped.Delete();
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Go get me 1 NoDachi now.", mobile.NetState);
                    return true;
                }
				else if (dropped is NoDachi)
                {
                    if (dropped.Amount != 1)
                    {
                        this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "I need 1 NoDachi.", mobile.NetState);
                        return false;
                    }
                    dropped.Delete();
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "The final item is 1 Arctic Death Dealer.", mobile.NetState);
                    return true;
                }
                else if (dropped is ArcticDeathDealer)
                {
                    if (dropped.Amount != 1)
                    {
                        this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "I need 1 Arctic Death Dealer.", mobile.NetState);
                        return false;
                    }
                    dropped.Delete();
					// Edits by Raist
					switch (Utility.Random(10))
					{
						case 0: mobile.AddToBackpack(new BankCheck(100000));
							break;
						case 1: mobile.AddToBackpack(new GumballTicket());			//Custom item
								mobile.AddToBackpack(new BankCheck(1000));
							break;
						case 2: mobile.AddToBackpack(new YahtzeeDice());			//Custom item
								mobile.AddToBackpack(new BankCheck(10000));
							break;
						case 3: mobile.AddToBackpack(new UmbrellaTableDeed());		//Custom item
								mobile.AddToBackpack(new BankCheck(2500));
							break;
						case 4: mobile.AddToBackpack(new HangingBasketAddonDeed());	//Custom item
								mobile.AddToBackpack(new BankCheck(15000));
							break;
						case 5: mobile.AddToBackpack(new RoseBush1AddonDeed());		//Custom item
								mobile.AddToBackpack(new BankCheck(1900));
							break;
						case 6: mobile.AddToBackpack(new FireRockMiningBook());		//Custom item
								mobile.AddToBackpack(new BankCheck(100));
							break;
						case 7: mobile.AddToBackpack(new morticiansScalpel());		//Custom item
								mobile.AddToBackpack(new BankCheck(6000));
							break;
						case 8: mobile.AddToBackpack(new RandomSnowGlobeDeed());	//Custom item
								mobile.AddToBackpack(new BankCheck(4000));
							break;
						case 9: mobile.AddToBackpack(new GoldPan());				//Custom item
								mobile.AddToBackpack(new BankCheck(500));
							break;
					}
					//mobile.AddToBackpack(item);
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Here is your reward.", mobile.NetState);
                    return true;
                }
                else
                {
                    from.PrivateOverheadMessage(MessageType.Regular, 1153, false, "This had better be important...", from.NetState);
                    return false;
                }
            }
            else
            {
                from.PrivateOverheadMessage(MessageType.Regular, 1153, false, "What am I to do with this?", from.NetState);
                return false;
            }
        }
    }
}