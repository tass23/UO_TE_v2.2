using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
	public class GalahadQuest : BaseQuest
	{
		public override object Title{ get{ return "The Tale of Sir Galahad"; } }

		public override object Description
		{
			get
			{
				return
					"You say: 'I was in the nick of time, you were in great peril.'<BR><BR>" +
					"I don't think I was.<BR><BR>" +
					"You say: 'Yes you were, you were in terrible peril..'<BR><BR>" +
					"Look, let me go back in there and face the peril.<BR><BR>" +
					"You say: 'No, it's too perilous.'<BR><BR>" +
					"Look, I'm a knight, I'm supposed to get as much peril as I can.<BR><BR>" +
					"You say: 'No, we've got to find the Holy Grail. Come on!'<BR><BR>" +
					"Well, let me have just a little bit of peril?<BR><BR>" +
					"You say: 'NO! Now come along with me. We must head back to the castle!'";
			}
		}

		public override object Refuse{ get{ return "The peril within the castle is just too tempting?"; } }
		public override object Uncomplete{ get{ return "You must get back to the castle at once!"; } }

		public GalahadQuest() : base()
		{
			AddObjective( new EscortObjective( "Throne Room" ) );
			AddReward( new BaseReward( typeof( GalahadReport ), "Galahads' Report" ) );
		}

		public override void GiveRewards()
		{
			base.GiveRewards();
			Owner.SendMessage( "GALAHAD:'No, really, honestly, I can go back and handle that lot easily!'", null, 0xEF3 ); // You have demonstrated your compassion!  Your kind actions have been noted.
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

	public class GalahadEscort : BaseEscort
	{
		public override Type[] Quests
		{
			get{ return new Type[]
			{
				typeof( GalahadQuest )
			};}
		}

		[Constructable]
		public GalahadEscort() : base()
		{
			Name = "Sir Galahad";
			Title = "the Chaste";
			NameHue = 68;
		}

		public GalahadEscort( Serial serial ) : base( serial )
		{
		}

		public override void InitBody()
		{
			InitStats( 100, 100, 25 );
			Female = false;
			CantWalk = false;
			Race = Race.Human;
			Hue = 0x8400;	
			HairItemID = 0x2045;
			HairHue = 915;
		}

		public override void InitOutfit()
		{
			AddItem( new Backpack() );

			PlateArms arms = new PlateArms();
			arms.Hue = 1072;
			AddItem( arms );

			PlateGloves gloves = new PlateGloves();
			gloves.Hue = 1072;
			AddItem( gloves );

			PlateLegs legs = new PlateLegs();
			legs.Hue = 1072;
			AddItem( legs );

			PlateGorget neck = new PlateGorget();
			neck.Hue = 1072;
			AddItem( neck );

			PlateChest chest = new PlateChest();
			chest.Hue = 1072;
			AddItem( chest );

			Broadsword weapon = new Broadsword();
			weapon.Hue = 1072;
			weapon.Movable = false;
			EquipItem( weapon );
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