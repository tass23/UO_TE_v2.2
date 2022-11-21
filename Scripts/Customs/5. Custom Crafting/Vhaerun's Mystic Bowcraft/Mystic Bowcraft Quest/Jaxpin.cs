using System;
using Server;
using System.Collections; 
using Server.ContextMenus;
using System.Collections.Generic;
using Server.Misc;
using Server.Network;
using Server.Items; 
using Server.Mobiles;
using Server.Gumps;

namespace Server.Mobiles 
{ 
	[CorpseName( "a corpse" )]
	public class Jaxpin : BaseCreature 
	{ 
		[Constructable] 
		public Jaxpin() : base( AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4 ) 
		{
			SpeechHue = Utility.RandomDyedHue(); 

			Hue = Utility.RandomSkinHue(); 
			Body = 0x190; 
			Name = "Jaxpin";

			SetStr( 86, 100 );
			SetDex( 81, 95 );
			SetInt( 61, 75 );

			SetDamage( 10, 23 );

			SetSkill( SkillName.MagicResist, 25.0, 47.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 15.0, 37.5 );

			AddItem( new Cloak( Utility.RandomNeutralHue() ) );
			AddItem( new LongPants( Utility.RandomNeutralHue() ) );
			AddItem( new Boots() );
			AddItem( new Doublet( Utility.RandomNeutralHue() ) );
			AddItem( new Cap( Utility.RandomGreenHue() ) );

		}

		public Jaxpin( Serial serial ) : base( serial ) 
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

            	public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{ 
			base.GetContextMenuEntries( from, list );
			list.Add( new JaxpinEntry( from, this ) ); 
		}

		public class JaxpinEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public JaxpinEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
				

                          if( !( m_Mobile is PlayerMobile ) )
					return;
				
				PlayerMobile mobile = (PlayerMobile) m_Mobile;

				{
					if ( ! mobile.HasGump( typeof( JaxpinGump ) ) )
					{
						mobile.SendGump( new JaxpinGump( mobile ));
						
					} 
				}
			}
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{          		
         		Mobile m = from;
			PlayerMobile mobile = m as PlayerMobile;

			if ( mobile != null)
			{
				if( dropped is ColdIronIngot )
         			{
					if( dropped.Amount == 3 )
					{
						dropped.Delete(); 
						SayTo( from, "Just what I needed! Here, you can have it now that I'm done with it." );
						mobile.AddToBackpack( new MysticFletcherTools() );

						return true;
					}
					else
					{
						SayTo( from, "That's not the amount I need." );
					}
         			}
         			else
         			{
					SayTo( from, "No, I need cold iron ingots." );
     				}
			}

			return false;
		
		}
	}
}