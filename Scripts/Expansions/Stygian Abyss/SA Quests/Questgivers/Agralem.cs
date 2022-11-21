using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Engines.Quests;

namespace Server.Engines.Quests
{
    public class IntoTheVoidQuest : BaseQuest
    {
        public override bool DoneOnce { get { return true; } }

      
        public override object Title { get { return 1112687; } }


        public override object Description { get { return 1112690; } }

        public override object Refuse { get { return 1112691; } }


        public override object Uncomplete { get { return 1112692; } }


        public override object Complete { get { return 1112693; } }

        public IntoTheVoidQuest() : base()
        {
            switch (Utility.Random(4))
            {
                case 0: AddObjective( new SlayObjective( typeof( Anzuanord ),"Anzuanord" , 10 )); break;
                case 1: AddObjective( new SlayObjective( typeof( Vasanord ), "Vasanord", 10)); break;
                case 2: AddObjective( new SlayObjective( typeof( UsagralemBallem ), "Usagralem Ballem ", 10)); break;
                case 3: AddObjective( new SlayObjective( typeof( Anlorzen ), "Anlorzen", 10)); break;
             
            }	                                                         

            AddReward(new BaseReward(typeof(AbyssReaver), 1112694 ));/////Abyss Reaver
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

    public class Agralem : MondainQuester
    {
        public override Type[] Quests
        {
            get
            {
                return new Type[] 
			{ 
				typeof( IntoTheVoidQuest )
			};
            }
        }

        [Constructable]
        public Agralem() : base("Agralem", "the Bladeweaver")
        {
        }

       public override void InitBody()
		{
			InitStats( 100, 100, 25 );
			
			Female = false;
			CantWalk = true;

            Body = 666;
			HairItemID = 16987;
			HairHue = 1801;
        }

        public override void InitOutfit()
        {
            AddItem(new Backpack());

            AddItem(new GargishClothChest(Utility.RandomNeutralHue()));
            AddItem(new GargishClothKilt(Utility.RandomNeutralHue()));
            AddItem(new GargishClothLegs(Utility.RandomNeutralHue()));
        }

        public Agralem(Serial serial) : base(serial)
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
	
	