using System; 
using Server; 
using Server.Items;
using Server.Multis.Deeds;
using Server.Engines.Quests;
using Reward = Server.Engines.Quests.BaseReward;

namespace Server.Items
{
    public class DustyMuseumBag : BaseRewardBag
    {

        public override int LabelNumber { get { return 1112994; } }

        [Constructable]
        public DustyMuseumBag()
        {

            AddItem(new Gold(6000));

            switch (Utility.Random(9))
            {
                case 0: AddItem(new Amber(5)); break;
                case 1: AddItem(new Amethyst(5)); break;
                case 2: AddItem(new Citrine(5)); break;
                case 3: AddItem(new Ruby(5)); break;
                case 4: AddItem(new Emerald(5)); break;
                case 5: AddItem(new Diamond(5)); break;
                case 6: AddItem(new Sapphire(5)); break;
                case 7: AddItem(new StarSapphire(5)); break;
                case 8: AddItem(new Tourmaline(5)); break;
            }

            switch (Utility.Random(4))
            {
                case 0: AddItem(new MagicalResidue(10)); break;
                case 1: AddItem(new RelicFragment(10)); break;
                case 2: AddItem(new DelicateScales(10)); break;
                case 3: AddItem(new ChagaMushroom(10)); break;
            }
        }


        public DustyMuseumBag(Serial serial)
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