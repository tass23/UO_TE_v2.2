using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
	public class TorsoQuest : BaseQuest
	{
        public override object Title { get { return "Innocent's Torso Quest"; } }
        public override object Description { get { return "Hail! It is nice to meet you. I will permit you to join the ranks of the werewolves, if you can bring to me the torso of an innocent victim."; } }//and slay XX XXXXXX."; } }
		
		public override object Refuse{ get{ return "I cannot say I am not disappointed. Very well. Off with you."; } }
		public override object Uncomplete{ get{ return "Are you done yet? No!! Get back to it."; } }
        public override object Complete { get { return "I was skeptical when I asked. You may yet be worthy enough to become a werewolf."; } }

        public TorsoQuest(): base()
		{
            AddObjective(new ObtainObjective(typeof(Torso), "Torso", 1));
            AddReward(new BaseReward(typeof(LycanthropePotion), 1, "a Lycanthrope Potion"));
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
	
	public class Savian : MondainQuester
	{
		public override Type[] Quests
		{ 
			get{ return new Type[] 
			{ 
				typeof( TorsoQuest )
			};} 
		}
		
		[Constructable]
        public Savian(): base("Savian", "The Werewolf Clan Leader")
		{			
			Body = 400;
			Hue = 0x847E;
            Blessed = true;
            CantWalk = true;
            Direction = Direction.East;
            Frozen = true;
			Utility.AssignRandomHair( this, 1109 );
		}

        public Savian(Serial serial) : base(serial)
		{
		}		
		
		public override void InitBody()
		{
			InitStats( 100, 100, 25 );
		}
		
		public override void InitOutfit()
		{
            AddItem(new Server.Items.Robe(0x7E3));
            AddItem(new Server.Items.Boots(2119));
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