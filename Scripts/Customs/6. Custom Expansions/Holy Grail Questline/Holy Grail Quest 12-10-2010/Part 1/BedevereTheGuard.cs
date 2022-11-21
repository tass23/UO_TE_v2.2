using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{	
	public class BedevereTheGuard : BaseQuest
	{
		public override object Title{ get{ return "Bedevere the 'Guard'"; } }
		
		public override object Description
		{ 
			get
			{ 
				return 
					"A king you say? Arthur you say, eh? Well if you're such a king then answer this riddle:<BR><BR>" +
					"You say: 'I don't have time for games. I am your king and I order you to do as I say!'<BR><BR>" +
					"What do we do with witches?<BR><BR>" +
					"You say: 'Fine, fine, I'll answer your stupid riddle. We burn witches.'<BR><BR>" +
					"Ah ha! And what else do we burn, besides more witches?<BR><BR>" +
					"You say: 'Wood, of course.'<BR>" +
					"Very logical, nicely done. Now why do witches burn?<BR><BR>" +
					"You say: 'Because they are made out of wood.'<BR><BR>" +
					"Oh truly maginificent m'lord! Well answered! So how do we tell if a witch is made out of wood?<BR><BR>" +
      				"You say: 'You can throw her in the water and see if she floats.'<BR><BR>" +
      				"Marvelous, marvelous. You're doing quite well. You're very bright, and you have such a nice shiny crown. We're almost done.<BR>" +
					"What else floats on water?<BR><BR>" +
					"You say: 'A duck.'<BR><BR>" +
					"Ahh, yes indeed. So logically....<BR><BR>" +
					"You say: 'If she weighs the same as a duck, she's a witch.'<BR><BR>" +
					"Outstanding m'lord! Spectacular! You are indeed the king! Now, what do you want with me?<BR><BR>" +
					"You say: 'I'm here for a mighty steed.'<BR><BR>" +
					"Um, beggin your pardon m'lord, but do you see any steeds in 'ere?<BR><BR>" +
					"You say: 'No. But I still need one, you'll have to come with me back to the castle to speak with the k..err, cook.'<BR><BR>" +
					"The cook sire? But I've already eaten.<BR><BR>" +
					"You say: 'It doesn't matter. You're coming with me anyway.'";
			} 
		}
		
		public override object Refuse{ get{ return "I really should stay here m'lord and watch for witches. The countryside is crawling with them."; } }
		public override object Uncomplete{ get{ return "Perhaps you are a witch pretending to BE a king. Good thing I have a duck here with which to test you."; } }

		public BedevereTheGuard() : base()
		{								
			AddObjective( new EscortObjective( "Throne Room" ) );
			AddReward( new BaseReward( typeof( FineSteed ), "A 'Mighty Steed'" ) );
		}
		
		public override void GiveRewards()
		{			
			base.GiveRewards();
			Owner.SendMessage( "Well I'm here. Witches running rampant across the realm. Fake king kidnaps me and drags me here. You dosy bastard. I'll get you.'", null, 0xEF3 ); // You have demonstrated your compassion!  Your kind actions have been noted.
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
	
	public class BedevereEscort : BaseEscort
	{
		public override Type[] Quests
		{ 
			get{ return new Type[] 
			{ 
				typeof( BedevereTheGuard )
			};} 
		}
		
		[Constructable]
		public BedevereEscort() : base()
		{
			Name = "Bedevere";
			Title = "the Guard";
			NameHue = 68;
		}
		
		public BedevereEscort( Serial serial ) : base( serial )
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
			AddItem( new PlateChest() );
			AddItem( new PlateArms() );
			AddItem( new PlateLegs() );
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