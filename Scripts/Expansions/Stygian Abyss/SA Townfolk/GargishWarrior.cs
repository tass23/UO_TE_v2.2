using System;
using Server;
using Server.Items;
using Server.Misc;
using Server.Mobiles;

namespace Server.Mobiles
{
	public class GargishWarrior : BaseCreature
	{
        [Constructable]
        public GargishWarrior() : base(AIType.AI_Animal, FightMode.None, 10, 1, 0.2, 0.4)
        {
            Name = "Warrior";
            if (this.Female = Utility.RandomBool())
            {
                this.Body = 667;
                this.HairItemID = 17067;
                this.HairHue = 1762;
                AddItem(new GargishPlateChest());
                AddItem(new GargishPlateKilt());
                AddItem(new GargishPlateLegs());
                AddItem(new GargishPlateArms());
                AddItem(new PlateTalons());
               
                AddItem(new GlassSword());
            }
            else
            {
                this.Body = 666;
                this.HairItemID = 16987;
                this.HairHue = 1801;
                AddItem(new GargishPlateChest());
                AddItem(new GargishPlateKilt());
                AddItem(new GargishPlateLegs());
                AddItem(new GargishPlateArms());
                AddItem(new PlateTalons());
           
                AddItem(new GlassSword());

            }
        }

		public GargishWarrior( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}