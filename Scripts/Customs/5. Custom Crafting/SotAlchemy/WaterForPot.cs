using System;
using System.Collections.Generic;
using Server;
using Server.Targeting;

namespace Server.Items
{
    public class WaterForPot : Item
    {
        private AlchemyPot m_AlchemyPot;

        private List<AlchemyInfo> m_InfoArray = new List<AlchemyInfo>();
        [CommandProperty(AccessLevel.GameMaster)]
        public List<AlchemyInfo> InfoArray
        {
            get { return m_InfoArray; }
            set { m_InfoArray = value; InvalidateProperties(); }
        }

        [Constructable]
        public WaterForPot()
            : base(0x34A4)
        {
            Movable = false;
        }

        public WaterForPot(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            m_AlchemyPot = World.FindItem(RootParentEntity.Serial) as AlchemyPot;

            if (!m_AlchemyPot.Ignited && InfoArray != null)
            {
                from.SendMessage("What bottle do you want to fill with it?");
                from.BeginTarget(3, false, TargetFlags.None, new TargetCallback(WaterForPot_OnTarget));
            }
            else if (!m_AlchemyPot.Ignited && InfoArray == null)
            {
                from.SendMessage("You have poured it out of the cauldron");
                Delete();
                m_AlchemyPot.Water = false;
            }
            else
                return;
        }

        private void WaterForPot_OnTarget(Mobile from, object obj)
        {
            if (Deleted)
                return;

            Bottle bottle = obj as Bottle;
            double duration = 0;
            int effect = 0;

            if (bottle == null)
                from.SendMessage("You should fill a bottle with this potion");
            else if (!(bottle.IsChildOf(from.Backpack)))
                from.SendMessage("The bottle should be in your backpack to fill it");
            else
            {
                bottle.Consume();
                AlchemyRecipe ar = new AlchemyRecipe(InfoArray);
                Item potion = new Item();
                duration = from.Skills[SkillName.Alchemy].Base / 2;
                effect = (int)(from.Skills[SkillName.Alchemy].Base / 10);
                switch (ar.RecipeType)
                {
                    case AlRecipe.PhysResist: potion = new PhysResistPotion( duration, effect ); break;
                    case AlRecipe.FireResist: potion = new FireResistPotion(duration, effect); break;
                    case AlRecipe.ColdResist: potion = new ColdResistPotion(duration, effect); break;
                    case AlRecipe.PoisonResist: potion = new PoisonResistPotion(duration, effect); break;
                    case AlRecipe.EnergyResist: potion = new EnergyResistPotion(duration, effect); break;

                    case AlRecipe.MagicResist: potion = new MagicResistSkillPotion( duration, effect ); break;

                    case AlRecipe.None: potion = new UnknownPotion(); break;
                }
                from.SendMessage("You have made a " + potion.Name);
                from.AddToBackpack(potion);
                m_AlchemyPot.Water = false;
                Delete();
            }
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