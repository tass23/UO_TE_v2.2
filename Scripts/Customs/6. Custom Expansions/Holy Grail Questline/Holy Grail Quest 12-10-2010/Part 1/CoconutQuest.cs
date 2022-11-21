using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
	public class CoconutQuest : BaseQuest
	{
		public override object Title{ get{ return "My Kingdom for a Mighty Steed!"; } }

		public override object Description
		{
			get
			{
				return
					"<I>*your coconuts clapping in unison with your gait, you approach the guards at the stable*</I><BR><BR>" +
					"<B>Halt! Who goes there?</B><BR><BR>" +
					"You say: 'It is I, Arthur, son of Uther Pendragon, from the castle of Camelot. King of the Britons, defeator of the Saxons, sovereign of all England!'<BR><BR>" +
					"Pull the other one!<BR><BR>" +
					"You say: 'I have ridden the length and breadth of the land in search of knights who will join me in my court of Camelot...'<BR><BR>" +
					"You're using coconuts! You don't even have a horse! You've got two empty halves of coconut and you're bangin' 'em together!<BR><BR>" +
					"You say: 'So?  We will be riding when the snows of winter covered this land, through the kingdom of Mercea, through--'<BR><BR>" +
					"Where'd you get the coconut??<BR><BR>" +
					"You say: 'We found them.'<BR><BR>" +
					"Found them? The coconut's tropical! This is a temperate zone!<BR><BR>" +
					"You say: 'The swallow may fly south with the sun or the house martin or the plumber may seek warmer climes in winter yet these are not strangers to our land.'<BR><BR>" +
					"Are you suggesting coconuts migrate?<BR><BR>" +
      				"You say: 'Not at all, they could be carried.'<BR><BR>" +
      				"What -- a swallow carrying a coconut?<BR><BR>" +
					"You say: 'It could grip it by the husk!'<BR><BR>" +
					"It's not a question of where he grips it!  It's a simple question of weight ratios!  A five ounce bird could not carry a 1 pound coconut.<BR><BR>" +
					"You say: 'Well, it doesn't matter. I am in great haste and in need of a Mighty Steed! Tell your stable boy to...'<BR><BR>" +
					"Listen, in order to maintain air-speed velocity, a swallow needs to beat its wings 43 times every second, right?<BR><BR>" +
					"You say: 'Please! I'm not interested!'<BR><BR>" +
					"It could be carried by an African swallow! Yeah, an African swallow maybe, but not a European swallow, but then of course African swallows are not migratory.<BR><BR>" +
					"You say: 'PLEASE! Will you get your stable boy fetch me your Finest Steed!?!'<BR><BR>" +
					"So they couldn't bring a coconut back anyway...Wait a minute -- supposing two swallows carried it together?<BR>" +
					"No, they'd have to have it on a line.<BR>" +
					"Oh wait, it's simple!  They'd just use a standard creeper, held under the dorsal guiding feathers! Yes that would work.<BR>" +
					"I should go speak with the Stable Boy about this. Bring me to the King's court and then I will give you your 'mighty' steed.<BR><BR>" +
					"*Whispers to the other guards: <I>'King of the who? I never heard of that guy. What a Patsy!'*</I>";
			}
		}

		public override object Refuse{ get{ return "Stop makin' all that racket, ya silly coconut clacker!"; } }
		public override object Uncomplete{ get{ return "You want to travel 'the length and breadth of the land' but you cannot find the King's court?"; } }
				
		public CoconutQuest() : base()
		{								
			AddObjective( new EscortObjective( "Throne Room" ) );
			AddReward( new BaseReward( typeof( FineSteed ), "A 'Mighty Steed'" ) );
		}
		
		public override void GiveRewards()
		{			
			base.GiveRewards();
			Owner.SendMessage( "BEDEMIR: 'King??? Who the 'ell does he think he is kidding?!?'", null, 0xEF3 ); // You have demonstrated your compassion!  Your kind actions have been noted.
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
	
	public class CoconutEscort : BaseEscort
	{
		public override Type[] Quests
		{ 
			get{ return new Type[] 
			{ 
				typeof( CoconutQuest )
			};} 
		}
		
		[Constructable]
		public CoconutEscort() : base()
		{
			Name = "Bedemir";
			Title = "the Guard";
			NameHue = 68;
		}
		
		public CoconutEscort( Serial serial ) : base( serial )
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
			
			Item helm = new ChainCoif();
			helm.Movable = false;
			helm.Hue = 1646;
			AddItem( helm );
			
			Item leg = new ChainLegs();
			leg.Movable = false;
			leg.Hue = 1646;
			AddItem( leg );
			
			Item chest = new ChainChest();
			chest.Movable = false;
			chest.Hue = 1646;
			AddItem( chest );
			
			Item glove = new PlateGloves();
			glove.Movable = false;
			glove.Hue = 1646;
			AddItem( glove );
			
			Item neck = new PlateGorget();
			neck.Movable = false;
			neck.Hue = 1646;
			AddItem( neck );
			
			Item sword = new Pike();
			sword.Movable = false;
			AddItem( sword );
			
			AddItem( new Boots(1646) );	
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