using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
	public class GentleBladeQuest : BaseQuest
	{			
		public override TimeSpan RestartDelay{ get{ return TimeSpan.FromMinutes( 3 ); } }
		
		/* Gentle Blade */
		public override object Title{ get{ return 1075361; } }
		
		/* I came to this place looking for a cure for my wife. But I?m getting ahead of myself -- my wife was attacked by a 
		werewolf, and survived. Now she has become a werewolf herself. My research has turned up nothing that would cure her 
		affliction. *Sob* She begged me to end her suffering, but I cannot. She has removed herself to a remote part of Ice 
		Island so that she does not endanger others. If I give you the means, will you go there, find her, and give her the 
		mercy of a clean death?  */	
		public override object Description{ get{ return 1075362; } }
		
		/* I understand. I am no warrior, either. I suppose I shall have to wait here until one comes along. */
		public override object Refuse{ get{ return 1075364; } }
		
		/* My wife is hiding out in a cave on the north end of Ice Island. You will not be able to harm here, even with the 
		weapon I gave you, until night falls and she transforms into a wolf. */
		public override object Uncomplete{ get{ return 1075365; } }
		
		/* Thank you my friend . . . I know she is at peace, now. Here, keep the weapon. Most of its power is expended, but 
		it remains somewhat potent against wolf-kind. */
		public override object Complete{ get{ return 1075366; } }
		
		public GentleBladeQuest() : base()
		{								
			AddObjective( new SlayObjective( typeof( Aminia ), "warewolf", 1, 10800 ) );
						
			AddReward( new BaseReward( 1075363 ) ); // Misericord
		}
		
		Dagger dagger;
		
		public override void OnAccept()
		{
			dagger = new Dagger();			
			dagger.QuestItem = true;
			dagger.WeaponAttributes.UseBestSkill = 1;
			
			if ( Owner.PlaceInBackpack( dagger ) )
				base.OnAccept();
			else
			{
				dagger.Delete();
				Owner.SendLocalizedMessage( 1075574 ); // Could not create all the necessary items. Your quest has not advanced.
			}
		}
		
		public override void GiveRewards()
		{
			base.GiveRewards();
			
			if ( dagger != null && !dagger.Deleted && dagger.RootParent == Owner )
			{
				dagger.Name = "Misericord";
				dagger.WeaponAttributes.UseBestSkill = 0;
				dagger.QuestItem = false;
				dagger.Slayer3 = TalismanSlayerName.Wolf;
			}
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
	
	public class Fabrizio : MondainQuester
	{
		public override Type[] Quests
		{ 
			get{ return new Type[] 
			{ 
				typeof( GentleBladeQuest )
			};} 
		}	
		
		[Constructable]
		public Fabrizio() : base( "Fabrizio", "the master weaponsmith" )
		{			
		}
		
		public Fabrizio( Serial serial ) : base( serial )
		{
		}		
		
		public override void InitBody()
		{
			InitStats( 100, 100, 25 );
			
			Female = false;
			Race = Race.Human;
			
			Hue = 0x840E;
			HairItemID = 0x203D;
			HairHue = 0x1;
			FacialHairItemID = 0x203F;
			FacialHairHue = 0x1;
		}
		
		public override void InitOutfit()
		{
			AddItem( new Backpack() );		
			AddItem( new Shoes( 0x753 ) );
			AddItem( new LongPants( 0x59C ) );
			AddItem( new HalfApron( 0x8FD ) );
			AddItem( new Tunic( 0x58F ) );
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