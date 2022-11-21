using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{	
	public class DabblingontheDarkSide : BaseQuest
	{			   
                public override Type NextQuest{ get{ return typeof( TheBrainyAlchemist ); } }

                public override TimeSpan RestartDelay { get { return TimeSpan.FromMinutes(30); } }

		public override object Title{ get{ return "Dabbling on the Dark Side"; } }
		
		public override object Description{ get{ return 1112963; } }
		
		public override object Refuse{ get{ return "You are Scared from this Task !! Muahahah"; } }
		
		public override object Uncomplete{ get{ return "I am sorry that you have not accepted!"; } }

		public DabblingontheDarkSide() : base()
		{								
			AddObjective(new ObtainObjective(typeof(BouraSkin), "BouraSkin", 5, 0x11f4));
                        AddObjective(new ObtainObjective(typeof(FairyDragonWing), "Fairy Dragon Wings", 10, 0x1084));
                        AddObjective(new ObtainObjective(typeof(Dough), "Dough", 1, 0x103D));
						
			AddReward( new BaseReward( typeof( DeliciouslyTastyTreat ), 2, "Deliciously Tasty Treat" ) );
                    
		}		
		
                public override object Complete{ get{ return 1112966; } }
				
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

    public class TheBrainyAlchemist : BaseQuest
    {
        public override Type NextQuest { get { return typeof(ArmorUp); } }

        public override TimeSpan RestartDelay { get { return TimeSpan.FromMinutes(30); } }

        public override object Title { get { return "The Brainy Alchemist"; } }

        public override object Description { get { return 1112967; } }

        public override object Refuse { get { return "You are Scared from this Task !! Muahahah"; } }

        public override object Uncomplete { get { return "I am sorry that you have not accepted!"; } }

        public TheBrainyAlchemist(): base()
        {
            AddObjective(new ObtainObjective(typeof(ArcaneGem), "Arcane Gem", 1, 0x1ea7));
            AddObjective(new ObtainObjective(typeof(UndeadGargHorn), "Undamaged Undead Gargoyle Horns", 10, 0x2F5F));
            AddObjective(new ObtainObjective(typeof(InspectedKegofTotalRefreshment), "Inspected Keg of Total Refreshment", 1, 0x1940));
            AddObjective(new ObtainObjective(typeof(InspectedKegofGreaterPoison), "Inspected Keg of Greater Poison", 1, 0x1940));

            AddReward(new BaseReward(typeof(InfusedAlchemistsGem), "Infused Alchemist's Gem"));
        }

        public override object Complete { get { return 1112970; } }

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

    public class ArmorUp : BaseQuest
    {
        public override Type NextQuest { get { return typeof(ToTurnBaseMetalIntoVerite); } }

        public override TimeSpan RestartDelay { get { return TimeSpan.FromHours(2); } }

        public override object Title { get { return "Armor Up"; } }

        public override object Description { get { return 1112971; } }

        public override object Refuse { get { return "You are Scared from this Task !! Muahahah"; } }

        public override object Uncomplete { get { return "I am sorry that you have not accepted!"; } }

        public ArmorUp(): base()
        {
            AddObjective(new ObtainObjective(typeof(BouraSkin), "BouraSkin", 5, 0x11f4));
            AddObjective(new ObtainObjective(typeof(LeatherWolfSkin), "Leather Wolf Skin", 10, 0x3189));
            AddObjective(new ObtainObjective(typeof(UndamagedIronBeetleScale), "Undamaged IronBeetle Scale", 10, 0x5742));

            AddReward(new BaseReward(typeof(VialofArmorEssence), 1, "Vial Of Armor Essence"));

        }

        public override object Complete { get { return 1112974; } }

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

    public class ToTurnBaseMetalIntoVerite : BaseQuest
    {
        public override Type NextQuest { get { return typeof(PureValorite); } }

        public override TimeSpan RestartDelay { get { return TimeSpan.FromHours(2); } }

        public override object Title { get { return "To Turn Base Metal Into Verite"; } }

        public override object Description { get { return 1112975; } }

        public override object Refuse { get { return "You are Scared from this Task !! Muahahah"; } }

        public override object Uncomplete { get { return "I am sorry that you have not accepted!"; } }

        public ToTurnBaseMetalIntoVerite(): base()
        {
            AddObjective(new ObtainObjective(typeof(UndeadGargoyleMedallions), "Undead Gargoyle Medallions", 5, 0x2AAA));
            AddObjective(new ObtainObjective(typeof(PileofInspectedVeriteIngots), "Pile of Inspected Verite Ingots", 1, 0x1BEA));

            AddReward(new BaseReward(typeof(ElixirofVeriteConversion), 1, "Elixir of Verite Conversion"));

        }

        public override object Complete { get { return 1112978; } }

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

    public class PureValorite : BaseQuest
    {
        public override Type NextQuest { get { return typeof(TheForbiddenFruit); } }

        public override TimeSpan RestartDelay { get { return TimeSpan.FromDays(1); } }

        public override object Title { get { return "Pure Valorite"; } }

        public override object Description { get { return 1112983; } }

        public override object Refuse { get { return "You are Scared from this Task !! Muahahah"; } }

        public override object Uncomplete { get { return "I am sorry that you have not accepted!"; } }

        public PureValorite(): base()
        {
            AddObjective(new ObtainObjective(typeof(InfusedGlassStave), "Infused Glass Stave", 5, 0x2AAA));
            AddObjective(new ObtainObjective(typeof(PileofInspectedValoriteIngots), "Pile of Inspected Valorite Ingots", 1, 0x1BEA));

            AddReward(new BaseReward(typeof(ElixirofValoriteConversion), 1, "Elixir of Valorite Conversion"));

        }

        public override object Complete { get { return 1112986; } }

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

    public class TheForbiddenFruit : BaseQuest
    {
        public override object Title { get { return "The Forbidden Fruit"; } }

        public override TimeSpan RestartDelay { get { return TimeSpan.FromDays(1); } }

        public override object Description { get { return 1112979; } }

        public override object Refuse { get { return "You are Scared from this Task !! Muahahah"; } }

        public override object Uncomplete { get { return "I am sorry that you have not accepted!"; } }

        public TheForbiddenFruit(): base()
        {
            AddObjective(new ObtainObjective(typeof(BouraSkin), "BouraSkin", 5, 0x11f4));
            AddObjective(new ObtainObjective(typeof(TreefellowWood), "TreefellowWood", 10, 0x1BDD));
            AddObjective(new ObtainObjective(typeof(Dough), "Dough", 1, 0x103D));

            AddReward(new BaseReward(typeof(IrresistiblyTastyTreat), "Irresistibly Tasty Treat"));
        }

        public override object Complete { get { return 1112982; } }

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
