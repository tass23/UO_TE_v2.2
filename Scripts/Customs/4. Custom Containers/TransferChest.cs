/*
 * Author: mordero
 * Email: mordero@gmail.com
 * Description: Creates a pair of chests that allow items to be transferred between them using a key.
 * Install: Just drag it into your custom script folder
 * TODO:
 * -Create a way to charge the chests (other than by a GM)?
 */

using System;
using Server;
using Server.Items;
using Server.Targeting;

namespace mordero.Items
{
    [FlipableAttribute(0xe80, 0x9a8)]
    class TransferChest : LockableContainer
    {
        public override string DefaultName
        {
            get
            {
                return "A Transfer Chest (" + m_currentCharge + ")";
            }
        }
        #region Properties
        private TransferChest m_mate;
        private int m_maxItems;
        private int m_maxItemWeight;
        private int m_maxCharges;
        private int m_currentCharge;
        private bool m_deleteOnNoCharge;

        /// <summary>
        /// The mate of the chest where items get sent when locked.
        /// </summary>
        [CommandProperty(AccessLevel.GameMaster)]
        public TransferChest Mate
        {
            get { return m_mate; }
            set
            {
                if (value != this)
                    m_mate = value;
                else
                    m_mate = null;

                InvalidateProperties();
            }
        }
        /// <summary>
        /// Maximum weight per item.
        /// </summary>
        [CommandProperty(AccessLevel.GameMaster)]
        public int MaxItemWeight
        {
            get { return m_maxItemWeight; }
            set { m_maxItemWeight = value; InvalidateProperties(); }
        }
        /// <summary>
        /// The maximum number of items that can be sent at a time.
        /// </summary>
        [CommandProperty(AccessLevel.GameMaster)]
        public new int MaxItems
        {
            get { return m_maxItems; }
            set { m_maxItems = value; InvalidateProperties(); }
        }
        /// <summary>
        /// The maximum number of charges the chest can hold.
        /// </summary>
        [CommandProperty(AccessLevel.GameMaster)]
        public int MaxCharges
        {
            get { return m_maxCharges; }
            set { m_maxCharges = value; InvalidateProperties(); }
        }
        /// <summary>
        /// The current number of charges the chest has.
        /// </summary>
        [CommandProperty(AccessLevel.GameMaster)]
        public int CurrentCharge
        {
            get { return m_currentCharge; }
            set
            {
                if (value > m_maxCharges)
                    m_currentCharge = m_maxCharges;
                else if (value < 0)
                    m_currentCharge = 0;
                else
                    m_currentCharge = value;
                InvalidateProperties();
            }
        }
        /// <summary>
        /// Delete the chest if it is out of charges
        /// </summary>
        [CommandProperty(AccessLevel.GameMaster)]
        public bool DeleteOnNoCharge
        {
            get { return m_deleteOnNoCharge; }
            set
            {
                m_deleteOnNoCharge = value;
                m_mate.DeleteOnNoCharge = value;
                InvalidateProperties();
            }
        }
        #endregion
        /// <summary>
        /// Creates the first chest, with a deed to the second inside.
        /// </summary>
        [Constructable]
        public TransferChest()
            : base(0xE80)
        {
            KeyValue = Key.RandomValue();
            AddItem(new TransferKey(KeyType.Gold, this.KeyValue));//add new transfer key
            AddItem(new TransferChestDeed(this, this.KeyValue));//add deed to create the mate chest
            SetUpProperties();
        }

        /// <summary>
        /// Creates the second chest of the pair. Used by the TransferChestDeed
        /// </summary>
        [Constructable]
        public TransferChest(TransferChest original, uint KeyVal)
            : base(0xE80)
        {
            KeyValue = KeyVal;
            m_mate = original;
            AddItem(new TransferKey(KeyType.Gold, this.KeyValue));//add new transfer key
            SetUpProperties();
        }
        /// <summary>
        /// Creates a chest with only the KeyValue set. Used by the TwoTransferChestDeed
        /// </summary>
        [Constructable]
        public TransferChest(uint keyvalue)
            : base(0xE80)
        {
            KeyValue = keyvalue;
            AddItem(new TransferKey(KeyType.Gold, this.KeyValue));
            SetUpProperties();
        }
        public void SetUpProperties()
        {
            m_maxCharges = 15;
            m_maxItems = 10;
            m_maxItemWeight = 25;
            m_deleteOnNoCharge = true;
            m_currentCharge = m_maxCharges;
            Name = "A Transfer Chest";
        }
        public TransferChest(Serial serial)
            : base(serial)
        {
        }
        public override void LockPick(Mobile from)
        {
            Locked = true;
            from.SendMessage("You feel a jolt of energy pass through you.");
            from.Damage(from.Hits / 2);
        }
        public void TransferContents(Mobile from)
        {
            if (m_mate != null && !m_mate.Deleted)
            {
                if (Items.Count <= m_maxItems)
                {
                    if (m_currentCharge > 0)
                    {
                    Transfer:
                        int notTransferable = 0;
                        Items.ForEach(delegate(Item i)
                        {
                            if (i is Container || i is TransferChest)
                            {
                                from.SendMessage("You cannot send a container through the chest.");
                                notTransferable++;
                            }
                            else if (i is TransferKey)
                            {
                                from.SendMessage("You cannot send a transfer chest key through the chest.");
                                notTransferable++;
                            }
                            else if (i is TransferChestDeed)
                            {
                                from.SendMessage("You cannot send a transfer chest deed through the chest.");
                                notTransferable++;
                            }
                            else if (i.Weight > m_maxItemWeight)
                            {
                                string name;
                                if (i.Name == null)
                                    name = i.DefaultName;
                                else
                                    name = i.Name;
                                from.SendMessage("{0} is too heavy to send through this chest.", name);
                                notTransferable++;
                            }
                            else
                            {
                                m_mate.AddItem(i);
                                m_currentCharge -= 1;
                                if (m_currentCharge <= 0)
                                {
                                    if ((this.Mate.CurrentCharge <= 0) && (m_deleteOnNoCharge))
                                    {
                                        from.SendMessage("The chest ran out of charges during the transfer and will delete itself in 5 minutes.");
                                        TransferDeleteTimer timer = new TransferDeleteTimer(this);
                                        timer.Start();
                                        if (this.Mate.Items.Count <= 0)
                                        {
                                            this.Mate.Locked = true;
                                            this.Mate.KeyValue = 0;
                                            TransferDeleteTimer timerMate = new TransferDeleteTimer(this.Mate);
                                            timerMate.Start();
                                        }
                                    }
                                    else
                                    {
                                        from.SendMessage("The chest ran out of charges during the transfer.");
                                        this.Locked = false;
                                        return;
                                    }
                                }
                            }
                        });//end loop

                        if (Items.Count != notTransferable)
                        {
                            goto Transfer;//need this for some weird bug of transferring only half of the total items each iteration :(
                        }
                    }
                    else
                    {
                        if (this.Mate.CurrentCharge <= 0)
                        {
                            if (m_deleteOnNoCharge)
                            {
                                TransferDeleteTimer timer = new TransferDeleteTimer(this);
                                timer.Start();
                                if (this.Mate.Items.Count <= 0)
                                {
                                    this.Mate.Locked = true;
                                    this.Mate.KeyValue = 0;
                                    TransferDeleteTimer timerMate = new TransferDeleteTimer(this.Mate);
                                    timerMate.Start();
                                    from.SendMessage("Both this chest and its mate are out of charges, they will be deleted in 5 minutes.");
                                }
                                else
                                {
                                    from.SendMessage("Both this chest and its mate are out of charges, this one will be deleted in 5 minutes.");

                                }
                            }
                            else
                            {
                                from.SendMessage("Both this chest and its mate are out of charges.");
                            }
                        }
                        else
                        {
                            from.SendMessage("The chest is out of charges.");
                        }
                        this.Locked = false;//unlock it if the transfer didnt work

                    }
                }
                else
                {
                    from.SendMessage("There are too many items in this chest to send at one time.");
                    this.Locked = false;
                }
            }
            else
            {
                from.SendMessage("This chest will be destroyed in 5 minutes since it does not have a mate.");
                TransferDeleteTimer timer = new TransferDeleteTimer(this);
                timer.Start();
                this.Locked = false;
            }
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
            //Version 0
            writer.Write((Item)m_mate);
            writer.Write((int)m_currentCharge);
            writer.Write((int)m_maxCharges);
            writer.Write((int)m_maxItems);
            writer.Write((int)m_maxItemWeight);
            writer.Write((bool)m_deleteOnNoCharge);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            switch (version)
            {
                case 0:
                    m_mate = (TransferChest)reader.ReadItem();
                    m_currentCharge = reader.ReadInt();
                    m_maxCharges = reader.ReadInt();
                    m_maxItems = reader.ReadInt();
                    m_maxItemWeight = reader.ReadInt();
                    m_deleteOnNoCharge = reader.ReadBool();
                    break;
            }
        }
    }
    class TransferKey : Key
    {
        public override string DefaultName
        {
            get
            {
                return "A Transfer Chest Key";
            }
        }
        [Constructable]
        public TransferKey(KeyType type, uint LockVal)
            : base(type, LockVal)
        {
        }
        /// <summary>
        /// Only use this one if you need to replace a key.
        /// </summary>
        [Constructable]
        public TransferKey()
            : base(KeyType.Gold)
        {
        }
        public TransferKey(Serial serial)
            : base(serial)
        {
        }
        //A slightly modified version of the virtual OnDoubleClick
        public override void OnDoubleClick(Mobile from)
        {
            if (!this.IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(501661); // That key is unreachable.
                return;
            }

            Target t;
            int number;

            if (KeyValue != 0)
            {
                number = 501662; // What shall I use this key on?
                t = new SendTarget(this);

                from.SendLocalizedMessage(number);

                from.Target = t;
            }

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
        //Slightly modified version of UnlockTarget, got rid of rename and copy
        private class SendTarget : Target
        {
            private TransferKey m_Key;
            /// <summary>
            /// Used to lock and unlock the chest as well as calling the TransferContents method of the chest.
            /// </summary>
            public SendTarget(TransferKey key)
                : base( key.MaxRange, false, TargetFlags.None)
            {
                m_Key = key;
                CheckLOS = false;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (m_Key.Deleted || !m_Key.IsChildOf(from.Backpack))
                {
                    from.SendLocalizedMessage(501661); // That key is unreachable.
                    return;
                }

                int number;

                if (targeted is ILockable)
                {
                    ILockable t = (ILockable)targeted;
                    bool locked = t.Locked;
                    if (m_Key.UseOn(from, (ILockable)targeted))
                    {
                        number = -1;
                        //it should be the transfer chest since it was unlocked, but check anyways
                        if (targeted is TransferChest)
                        {
                            TransferChest target = (TransferChest)targeted;
                            if (!locked)//if it wasnt locked before the key was used, then send the contents
                            {
                                target.TransferContents(from);
                                target.Delta(ItemDelta.Update);//closes the chest if its still open
                            }
                        }
                    }
                    else
                        number = 501668; // This key doesn't seem to unlock that.
                }
                else
                {
                    number = 501666; // You can't unlock that!
                }

                if (number != -1)
                {
                    from.SendLocalizedMessage(number);
                }
            }
        }
    }
    class TransferChestDeed : Item
    {
        public override string DefaultName
        {
            get
            {
                return "a transfer chest deed";
            }
        }
        private uint m_KeyValue;
        private TransferChest m_original;

        [CommandProperty(AccessLevel.GameMaster)]
        public uint KeyValue
        {
            get { return m_KeyValue; }
            set { m_KeyValue = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public TransferChest Original
        {
            get { return m_original; }
            set
            {
                if (value is TransferChest)
                    m_original = value;
                else
                    m_original = null;

                InvalidateProperties();
            }
        }

        /// <summary>
        /// Deed for creating the second, mated chest to the original.
        /// </summary>
        /// <param name="original">First chest.</param>
        /// <param name="LockVal">Key value for chests.</param>
        [Constructable]
        public TransferChestDeed(TransferChest original, uint LockVal)
            : base(0x14EF)
        {
            Weight = 1.0;
            Name = "Deed for a Transfer Chest";
            m_KeyValue = LockVal;
            m_original = original;
        }
        public TransferChestDeed(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!this.Deleted && (this.IsChildOf(from.Backpack)))
            {
                TransferChest chest = new TransferChest(m_original, m_KeyValue);//create the second chest
                from.Backpack.AddItem(chest);//add it to the backpack
                m_original.Mate = chest;//complete the link for the original
                this.Delete();//delete the deed
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);

            //Version 0
            writer.Write((uint)m_KeyValue);
            writer.Write((Item)m_original);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    m_KeyValue = reader.ReadUInt();
                    m_original = (TransferChest)reader.ReadItem();
                    break;
            }
        }
    }
    class TwoTransferChestDeed : Item
    {
        public override string DefaultName
        {
            get
            {
                return "a deed for two Transfer Chests";
            }
        }

        private uint m_KeyValue;

        [CommandProperty(AccessLevel.GameMaster)]
        public uint KeyValue
        {
            get { return m_KeyValue; }
            set { m_KeyValue = value; InvalidateProperties(); }
        }
        [Constructable]
        public TwoTransferChestDeed()
            : base(0x14EF)
        {
            Weight = 1.0;
            Name = "Deed for two Transfer Chests";
            m_KeyValue = Key.RandomValue();
        }
        public TwoTransferChestDeed(Serial serial)
            : base(serial)
        {
        }
        public override void OnDoubleClick(Mobile from)
        {
            if (!this.Deleted && (this.IsChildOf(from.Backpack)))
            {
                //create chests
                TransferChest chestOne = new TransferChest(m_KeyValue);
                TransferChest chestTwo = new TransferChest(m_KeyValue);
                //link them
                chestOne.Mate = chestTwo;
                chestTwo.Mate = chestOne;
                //add them to the backpack
                from.Backpack.AddItem(chestOne);
                from.Backpack.AddItem(chestTwo);
                this.Delete();//delete the deed
            }
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
            //version 0
            writer.Write((uint)m_KeyValue);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    m_KeyValue = reader.ReadUInt();
                    break;
            }
        }

    }
    class TransferDeleteTimer : Timer
    {
        private TransferChest m_chest;
        public TransferDeleteTimer(TransferChest chest)
            : base(TimeSpan.FromMinutes(5))
        {
            m_chest = chest;
            Priority = TimerPriority.OneMinute;
        }
        protected override void OnTick()
        {
            if (m_chest == null || m_chest.Deleted)
            {
                Stop();
                return;
            }
            if (m_chest.RootParent is Mobile)
            {
                ((Mobile)m_chest.RootParent).Emote("You feel a jolt as the chest disappears.");
            }
            m_chest.Delete();
        }
    }
}
