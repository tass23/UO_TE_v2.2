using System;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server.Engines.Craft;

namespace Server.Items
{
    public class DiscountCoupon : BaseTool
    {
        private int mDiscount;

        [CommandProperty(AccessLevel.GameMaster)]
        public int Discount { get { return mDiscount; } set { mDiscount = value; } }
        
		/*
		[CommandProperty(AccessLevel.GameMaster)]
        public bool IsInfinite
        {
            get { return UsesRemaining > 9999; }
            set
            {
                UsesRemaining = value ? int.MaxValue : 1;
                //ShowUsesRemaining = false;
            }
        }
		*/
		
        public override CraftSystem CraftSystem { get { return DefInscription.CraftSystem; } }
		
		[Constructable]
        public DiscountCoupon(): this(25)
        {
                //mDiscount++;
                //this.Name = "The Expanse Online Store Coupon for (" + UsesRemaining.ToString() + ") % OFF";
                //ShowUsesRemaining = false;
        }

        [Constructable]
        public DiscountCoupon(int uses): base(0x14F0)
        {
            //Name = "The Expanse Online Store Coupon";
            this.Weight = 0.1;
            Hue = 1089;
            Movable = true;
			LootType=LootType.Blessed;
            UsesRemaining = uses;
            //ShowUsesRemaining = false;
            mDiscount++;
            this.Name = "The Expanse Online Store Coupon for (" + UsesRemaining.ToString() + ") % OFF";
        }

        public DiscountCoupon(Serial serial): base(serial)
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