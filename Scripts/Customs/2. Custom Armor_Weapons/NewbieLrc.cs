using System;
using System.Collections.Generic;
using System.Text;
using Server.Mobiles;
using Server;
using Server.Items;

namespace Server.Items
{
    public class itemNewbieLrc : Container
    {
        [Constructable]
        public itemNewbieLrc() : base(0xE76)
        {
            Name = "Cursed LRC Armour";
            LootType = LootType.Cursed;
            LeatherCap newCap = new LeatherCap();
            newCap.Name = "Cursed New Player LRC Gear";
            newCap.LootType = LootType.Cursed;
            newCap.Attributes.LowerRegCost = 15;
            newCap.Hue = 707;
            this.AddItem(newCap);

            LeatherArms newArms = new LeatherArms();
            newArms.Name = "Cursed New Player LRC Gear";
            newArms.LootType = LootType.Cursed;
            newArms.Attributes.LowerRegCost = 15;
            newArms.Hue = 707;
            this.AddItem(newArms);

            LeatherChest newChest = new LeatherChest();
            newChest.Name = "Cursed New Player LRC Gear";
            newChest.LootType = LootType.Cursed;
            newChest.Attributes.LowerRegCost = 15;
            newChest.Hue = 707;
            this.AddItem(newChest);

            LeatherGloves newGloves = new LeatherGloves();
            newGloves.Name = "Cursed New Player LRC Gear";
            newGloves.LootType = LootType.Cursed;
            newGloves.Attributes.LowerRegCost = 15;
            newGloves.Hue = 707;
            this.AddItem(newGloves);

            LeatherGorget newGorget = new LeatherGorget();
            newGorget.Name = "Cursed New Player LRC Gear";
            newGorget.LootType = LootType.Cursed;
            newGorget.Attributes.LowerRegCost = 15;
            newGorget.Hue = 707;
            this.AddItem(newGorget);

            LeatherLegs newLegs = new LeatherLegs();
            newLegs.Name = "Cursed New Player LRC Gear";
            newLegs.LootType = LootType.Cursed;
            newLegs.Attributes.LowerRegCost = 15;
            newLegs.Hue = 707;
            this.AddItem(newLegs);            
        }

        public itemNewbieLrc(Serial serial) : base(serial)
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
    }
    public class GargNewbie : Container
    {
        [Constructable]
        public GargNewbie() : base(0xE76)
        {
            Name = "Cursed LRC Armour";
            LootType = LootType.Cursed;
            GargishLeatherArms newArms = new GargishLeatherArms();
            newArms.Name = "Cursed New Player LRC Gear";
            newArms.LootType = LootType.Cursed;
            newArms.Attributes.LowerRegCost = 15;
            newArms.Hue = 707;
            this.AddItem(newArms);

            GargishLeatherChest newChest = new GargishLeatherChest();
            newChest.Name = "Cursed New Player LRC Gear";
            newChest.LootType = LootType.Cursed;
            newChest.Attributes.LowerRegCost = 15;
            newChest.Hue = 707;
            this.AddItem(newChest);

            GargishLeatherWingArmor newWings = new GargishLeatherWingArmor();
            newWings.Name = "Cursed New Player LRC Gear";
            newWings.LootType = LootType.Cursed;
            newWings.Attributes.LowerRegCost = 15;
            newWings.Hue = 707;
            this.AddItem(newWings);

            GargishLeatherKilt newKilt = new GargishLeatherKilt();
            newKilt.Name = "Cursed New Player LRC Gear";
            newKilt.LootType = LootType.Cursed;
            newKilt.Attributes.LowerRegCost = 15;
            newKilt.Hue = 707;
            this.AddItem(newKilt);

            GargishLeatherLegs newLegs = new GargishLeatherLegs();
            newLegs.Name = "Cursed New Player LRC Gear";
            newLegs.LootType = LootType.Cursed;
            newLegs.Attributes.LowerRegCost = 15;
            newLegs.Hue = 707;
            this.AddItem(newLegs);            
        }

        public GargNewbie(Serial serial) : base(serial)
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
    }
}