using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class PowerBag : Bag
	{ 
		[Constructable] 
		public PowerBag() : this( 1 ) 
		{ 
		} 

		[Constructable] 
		public PowerBag( int amount )
		
		{ 
			Name = "Bag of Power";
			Hue = 1288;
			ItemID = 2482;
		    			
			DropItem ( new PowerScroll (SkillName.Alchemy, 120 ) );
			DropItem ( new PowerScroll (SkillName.Anatomy, 120 ) );
			DropItem ( new PowerScroll (SkillName.AnimalLore, 120 ) );
			DropItem ( new PowerScroll (SkillName.AnimalTaming, 120 ) );
			DropItem ( new PowerScroll (SkillName.Archery, 120 ) );
            DropItem ( new PowerScroll (SkillName.ArmsLore, 120 ) );
            DropItem ( new PowerScroll (SkillName.Begging, 120 ) );
			DropItem ( new PowerScroll (SkillName.Blacksmith, 120 ) );
			DropItem ( new PowerScroll (SkillName.Bushido, 120 ) );
			DropItem ( new PowerScroll (SkillName.Camping, 120 ) );
			DropItem ( new PowerScroll (SkillName.Carpentry, 120 ) );
			DropItem ( new PowerScroll (SkillName.Cartography, 120 ) );
			DropItem ( new PowerScroll (SkillName.Chivalry, 120 ) );
			DropItem ( new PowerScroll (SkillName.Cooking, 120 ) );
			DropItem ( new PowerScroll (SkillName.DetectHidden, 120 ) );
			DropItem ( new PowerScroll (SkillName.Discordance, 120 ) );
			DropItem ( new PowerScroll (SkillName.EvalInt, 120 ) );
			DropItem ( new PowerScroll (SkillName.Fishing, 120 ) );
			DropItem ( new PowerScroll (SkillName.Fencing, 120 ) );
			DropItem ( new PowerScroll (SkillName.Fletching, 120 ) );
			DropItem ( new PowerScroll (SkillName.Focus, 120 ) );
			DropItem ( new PowerScroll (SkillName.Forensics, 120 ) );
			DropItem ( new PowerScroll (SkillName.Healing, 120 ) );
			DropItem ( new PowerScroll (SkillName.Herding, 120 ) );
			DropItem ( new PowerScroll (SkillName.Hiding, 120 ) );
			DropItem ( new PowerScroll (SkillName.Inscribe, 120 ) );
			DropItem ( new PowerScroll (SkillName.ItemID, 120 ) );
			DropItem ( new PowerScroll (SkillName.Lockpicking, 120 ) );
			DropItem ( new PowerScroll (SkillName.Lumberjacking, 120 ) );
			DropItem ( new PowerScroll (SkillName.Macing, 120 ) );
			DropItem ( new PowerScroll (SkillName.Magery, 120 ) );
			DropItem ( new PowerScroll (SkillName.MagicResist, 120 ) );
			DropItem ( new PowerScroll (SkillName.Meditation, 120 ) );
			DropItem ( new PowerScroll (SkillName.Mining, 120 ) );
			DropItem ( new PowerScroll (SkillName.Musicianship, 120 ) );
			DropItem ( new PowerScroll (SkillName.Necromancy, 120 ) );
			DropItem ( new PowerScroll (SkillName.Ninjitsu, 120 ) );
			DropItem ( new PowerScroll (SkillName.Parry, 120 ) );
			DropItem ( new PowerScroll (SkillName.Peacemaking, 120 ) );
			DropItem ( new PowerScroll (SkillName.Poisoning, 120 ) );
			DropItem ( new PowerScroll (SkillName.Provocation, 120 ) );
			DropItem ( new PowerScroll (SkillName.RemoveTrap, 120 ) );
			DropItem ( new PowerScroll (SkillName.Snooping, 120 ) );
			DropItem ( new PowerScroll (SkillName.SpiritSpeak, 120 ) );
			DropItem ( new PowerScroll (SkillName.Stealing, 120 ) );
			DropItem ( new PowerScroll (SkillName.Stealth, 120 ) );
			DropItem ( new PowerScroll (SkillName.Spellweaving, 120 ) );
			DropItem ( new PowerScroll (SkillName.Swords, 120 ) );
			DropItem ( new PowerScroll (SkillName.Tactics, 120 ) );
			DropItem ( new PowerScroll (SkillName.Tailoring, 120 ) );
			DropItem ( new PowerScroll (SkillName.TasteID, 120 ) );
			DropItem ( new PowerScroll (SkillName.Tinkering, 120 ) );
			DropItem ( new PowerScroll (SkillName.Tracking, 120 ) );
			DropItem ( new PowerScroll (SkillName.Veterinary, 120 ) );
			DropItem ( new PowerScroll (SkillName.Wrestling, 120 ) );       
		} 

		public PowerBag ( Serial serial ) : base( serial ) 
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
