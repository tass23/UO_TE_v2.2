using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Multis
{
    public class GypsyCamp : BaseCamp
    {
        public virtual Mobile Gypsies { get { return new Gypsy(); } }

        private int m_SpawnRange;

        [Constructable]
        public GypsyCamp()
            : base(0x10EE) // dummy garbage at center
        {
        }

        public override void AddComponents()
        {

            Visible = false;
            DecayDelay = TimeSpan.FromMinutes(5.0);
            AddItem(new Static(0x10ee), 0, 0, 0);
            AddItem(new Static(0xfac), 0, 7, 0);

            switch (Utility.Random(3))
            {
                case 0:
                    {
                        AddItem(new Item(0xDE3), 0, 7, 0); // Campfire
                        AddItem(new Item(0x974), 0, 7, 1); // Cauldron
                        break;
                    }
                case 1:
                    {
                        AddItem(new Item(0x1E95), 0, 7, 1); // Rabbit on a spit
                        break;
                    }
                default:
                    {
                        AddItem(new Item(0x1E94), 0, 7, 1); // Chicken on a spit
                        break;
                    }
            }

            AddCampChests();

            for (int i = 0; i < 4; i++)
            {
                AddMobile(Gypsies, 6, Utility.Random(-7, 7), Utility.Random(-7, 7), 0);
            }
        }

        private void AddCampChests()
        {
            LockableContainer chest = null;

            switch (Utility.Random(3))
            {
                case 0: chest = new MetalChest(); break;
                case 1: chest = new MetalGoldenChest(); break;
                default: chest = new WoodenChest(); break;
            }

            chest.LiftOverride = true;

            TreasureMapChest.Fill(chest, 1);

            AddItem(chest, -2, -2, 0);

            LockableContainer crates = null;

            switch (Utility.Random(4))
            {
                case 0: crates = new SmallCrate(); break;
                case 1: crates = new MediumCrate(); break;
                case 2: crates = new LargeCrate(); break;
                default: crates = new LockableBarrel(); break;
            }

            crates.TrapType = TrapType.ExplosionTrap;
            crates.TrapPower = Utility.RandomMinMax(30, 40);
            crates.TrapLevel = 2;

            crates.RequiredSkill = 76;
            crates.LockLevel = 66;
            crates.MaxLockLevel = 116;
            crates.Locked = true;

            crates.DropItem(new Gold(Utility.RandomMinMax(100, 400)));
            crates.DropItem(new Arrow(10));
            crates.DropItem(new Bolt(10));

            crates.LiftOverride = true;

            if (Utility.RandomDouble() < 0.8)
            {
                switch (Utility.Random(4))
                {
                    case 0: crates.DropItem(new LesserCurePotion()); break;
                    case 1: crates.DropItem(new LesserExplosionPotion()); break;
                    case 2: crates.DropItem(new LesserHealPotion()); break;
                    default: crates.DropItem(new LesserPoisonPotion()); break;
                }
            }

            AddItem(crates, 2, 2, 0);
        }

        // Don't refresh decay timer
        public override void OnEnter(Mobile m)
        {
        }

        // Don't refresh decay timer
        public override void OnExit(Mobile m)
        {
        }

        public GypsyCamp(Serial serial)
            : base(serial)
        {
        }

        public override void AddItem(Item item, int xOffset, int yOffset, int zOffset)
        {
            if (item != null)
                item.Movable = false;

            base.AddItem(item, xOffset, yOffset, zOffset);
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