using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
	public class TimTheEnchanterQuest : BaseQuest
	{
		public override object Title{ get{ return "Tim the Enchanter"; } }

		public override object Description
		{
			get
			{
				return
					"I am Tim the Enchanter and you seek the Holy Grail!<BR><BR>" +
					"To the north there lies a cave -- the cave of Caerbannog --" +
					" wherein, carved in mystic runes upon the very living rock, the last" +
					" words of Ulfin Bedweer of Regett proclaim the last resting" +
					" place of the most Holy Grail.<BR><BR>" +
					"Go There!  But! Follow only if ye be men of valor, for the entrance" +
					" to this cave is guarded by a creature so foul, so cruel that no man" +
					" yet has fought with it and lived!<BR><BR>" +
					"Bones of four fifty men lie strewn" +
					" about its lair.<BR>" +
					"So, brave knights, if you do doubt your courage or" +
					" your strength, come no further, for death awaits you all with nasty" +
					" big pointy teeth!<BR><BR>" +
					"The only way to defeat the beast is with...The Holy Hand Grenade of Antioch!";     				
			}
		}

		public override object Refuse
		{
			get
			{
				return "Look, that rabbit's got a vicious streak a mile wide, it's a killer!";
			}
		}

		public override object Uncomplete
		{
			get
			{
				return "Well,that's no ordinary rabbit. That's the most foul, cruel, and bad-tempered rodent you ever set eyes on!";
			}
		}

		public override object Complete
		{
			get
			{
				return
					"Well that was a fine job!!<BR><BR>" +
					"Ya killed that poor little fluffy rabbit ya sissy!<BR><BR>" +
					"Da poor thing...he had a life of hopping left in him...<I>*sobs*</I><BR><BR>" +
					"HERE! Take this silly map back to the castle." +
					"<I>*continues sobbing uncontrollably*</I><BR><BR>" +
					"....*sniffle*";
			}
		}

		public TimTheEnchanterQuest() : base()
		{
			AddObjective( new SlayObjective( typeof( HGRabbit ), "Rabbit", 1 ) );
			AddReward( new BaseReward( typeof( BridgeOfDeathScroll ), "Bridge of Death Map" ) );
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

	public class TimTheEnchanter : MondainQuester
	{
		public override Type[] Quests
		{
			get{ return new Type[]
			{
				typeof( TimTheEnchanterQuest )
			};}
		}

		[Constructable]
		public TimTheEnchanter() : base( "Tim", "the Enchanter" )
		{
		}

		public TimTheEnchanter( Serial serial ) : base( serial )
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
			HairHue = 0x740;
		}

		public override void InitOutfit()
		{
			AddItem( new Backpack() );
			AddItem( new Shoes( 0x727 ) );
			AddItem( new Robe() );
			AddItem( new Server.Items.NorseHelm() );
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