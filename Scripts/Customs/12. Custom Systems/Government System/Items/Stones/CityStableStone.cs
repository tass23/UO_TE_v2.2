using System; 
using System.Collections; 
using Server.Items; 
using Server.Misc; 
using Server.Network; 
using Server.Mobiles; 
using Server.Multis; 
using Server.Gumps;
using Server.Targeting;
using Server.ContextMenus;
using System.Collections.Generic;

namespace Server.Items 
{ 
	public class CityStableStone : Item
	{
		private CivicSign m_Sign;

		public CivicSign Sign
		{
			get{ return m_Sign; }
			set{ m_Sign = value; }
		}

		[Constructable] 
		public CityStableStone() : base( 0xED4 ) 
		{ 
			Name = "a city stable stone"; 
			Movable = false;
		} 

		public CityStableStone( Serial serial ) : base( serial ) 
		{ 
		} 

		private class StableEntry : ContextMenuEntry
		{
			private CityStableStone m_Trainer;
			private Mobile m_From;

			public StableEntry( CityStableStone trainer, Mobile from ) : base( 6126, 12 )
			{
				m_Trainer = trainer;
				m_From = from;
			}

			public override void OnClick()
			{
				m_Trainer.BeginStable( m_From );
			}
		}

		private class ClaimAllEntry : ContextMenuEntry
		{
			private CityStableStone m_Trainer;
			private Mobile m_From;

			public ClaimAllEntry( CityStableStone trainer, Mobile from ) : base( 6127, 12 )
			{
				m_Trainer = trainer;
				m_From = from;
			}

			public override void OnClick()
			{
				m_Trainer.Claim( m_From );
			}
		}
		
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );

			if ( from.Alive )
			{
				list.Add( new StableEntry( this, from ) );

				if ( from.Stabled.Count > 0 )
					list.Add( new ClaimAllEntry( this, from ) );
			}
		}

		public static int GetMaxStabled( Mobile from )
		{
			double taming = from.Skills[SkillName.AnimalTaming].Value;
			double anlore = from.Skills[SkillName.AnimalLore].Value;
			double vetern = from.Skills[SkillName.Veterinary].Value;
			double sklsum = taming + anlore + vetern;

			int max;

			if ( sklsum >= 240.0 )
				max = 5;
			else if ( sklsum >= 200.0 )
				max = 4;
			else if ( sklsum >= 160.0 )
				max = 3;
			else
				max = 2;

			if ( taming >= 100.0 )
				max += (int)((taming - 90.0) / 10);

			if ( anlore >= 100.0 )
				max += (int)((anlore - 90.0) / 10);

			if ( vetern >= 100.0 )
				max += (int)((vetern - 90.0) / 10);

			return max;
		}

		private class StableTarget : Target
		{
			private CityStableStone m_Trainer;

			public StableTarget( CityStableStone trainer ) : base( 12, false, TargetFlags.None )
			{
				m_Trainer = trainer;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is BaseCreature )
					m_Trainer.EndStable( from, (BaseCreature)targeted );
				else if ( targeted == from )
					m_Trainer.Say( 502672 );
				else
					m_Trainer.Say( 1048053 );
			}
		}

		public void BeginStable( Mobile from )
		{
			if ( Deleted || !from.CheckAlive() )
				return;

			if ( from.Stabled.Count >= GetMaxStabled( from ) )
			{
				Say( 1042565 ); // You have too many pets in the stables!
			}
			else
			{
				Say( 1042558 ); /* I charge 30 gold per pet for a real week's stable time.
										 * I will withdraw it from thy bank account.
										 * Which animal wouldst thou like to stable here?
										 */

				from.Target = new StableTarget( this );
			}
		}

		public void EndStable( Mobile from, BaseCreature pet )
		{
			if ( Deleted || !from.CheckAlive() )
				return;

			if ( !pet.Controlled || pet.ControlMaster != from )
			{
				Say( 1042562 ); // You do not own that pet!
			}
			else if ( pet.IsDeadPet )
			{
				Say( 1049668 ); // Living pets only, please.
			}
			else if ( pet.Summoned )
			{
				Say( 502673 ); // I can not stable summoned creatures.
			}
			else if ( pet.Body.IsHuman )
			{
				Say( 502672 ); // HA HA HA! Sorry, I am not an inn.
			}
			else if ( (pet is PackLlama || pet is PackHorse || pet is Beetle) && (pet.Backpack != null && pet.Backpack.Items.Count > 0) )
			{
				Say( 1042563 ); // You need to unload your pet.
			}
			else if ( pet.Combatant != null && pet.InRange( pet.Combatant, 12 ) && pet.Map == pet.Combatant.Map )
			{
				Say( 1042564 ); // I'm sorry.  Your pet seems to be busy.
			}
			else if ( from.Stabled.Count >= GetMaxStabled( from ) )
			{
				Say( 1042565 ); // You have too many pets in the stables!
			}
			else
			{
				if ( Banker.Withdraw( from, 30 ) )
				{
					if ( m_Sign != null )
						m_Sign.Stone.CityTreasury += 30;

					pet.ControlTarget = null;
					pet.ControlOrder = OrderType.Stay;
					pet.Internalize();

					pet.SetControlMaster( null );
					pet.SummonMaster = null;

					pet.IsStabled = true;
					from.Stabled.Add( pet );

					Say( 502679 ); // Very well, thy pet is stabled. Thou mayst recover it by saying 'claim' to me. In one real world week, I shall sell it off if it is not claimed!
				}
				else
				{
					Say( 502677 ); // But thou hast not the funds in thy bank account!
				}
			}
		}

		private class ClaimListGump : Gump
		{
			private CityStableStone m_Trainer;
			private Mobile m_From;
			private ArrayList m_List;

			public ClaimListGump( CityStableStone trainer, Mobile from, ArrayList list ) : base( 50, 50 )
			{
				m_Trainer = trainer;
				m_From = from;
				m_List = list;

				from.CloseGump( typeof( ClaimListGump ) );

				AddPage( 0 );

				AddBackground( 0, 0, 325, 50 + (list.Count * 20), 9250 );
				AddAlphaRegion( 5, 5, 315, 40 + (list.Count * 20) );

				AddHtml( 15, 15, 275, 20, "<BASEFONT COLOR=#FFFFFF>Select a pet to retrieve from the stables:</BASEFONT>", false, false );

				for ( int i = 0; i < list.Count; ++i )
				{
					BaseCreature pet = list[i] as BaseCreature;

					if ( pet == null || pet.Deleted )
						continue;

					AddButton( 15, 39 + (i * 20), 10006, 10006, i + 1, GumpButtonType.Reply, 0 );
					AddHtml( 32, 35 + (i * 20), 275, 18, String.Format( "<BASEFONT COLOR=#C0C0EE>{0}</BASEFONT>", pet.Name ), false, false );
				}
			}

			public override void OnResponse( NetState sender, RelayInfo info )
			{
				int index = info.ButtonID - 1;

				if ( index >= 0 && index < m_List.Count )
					m_Trainer.EndClaimList( m_From, m_List[index] as BaseCreature );
			}
		}
		
		public void BeginClaimList( Mobile from )
		{
			if ( Deleted || !from.CheckAlive() )
				return;

			ArrayList list = new ArrayList();

			for ( int i = 0; i < from.Stabled.Count; ++i )
			{
				BaseCreature pet = from.Stabled[i] as BaseCreature;

				if ( pet == null || pet.Deleted )
				{
					pet.IsStabled = false;
					from.Stabled.RemoveAt( i );
					--i;
					continue;
				}

				list.Add( pet );
			}

			if ( list.Count > 0 )
				from.SendGump( new ClaimListGump( this, from, list ) );
			else
				Say( 502671 ); // But I have no animals stabled with me at the moment!
		}

		public void EndClaimList( Mobile from, BaseCreature pet )
		{
			if ( pet == null || pet.Deleted || from.Map != this.Map || !from.InRange( this, 14 ) || !from.Stabled.Contains( pet ) || !from.CheckAlive() )
				return;

			if ( (from.Followers + pet.ControlSlots) <= from.FollowersMax )
			{
				pet.SetControlMaster( from );

				if ( pet.Summoned )
					pet.SummonMaster = from;

				pet.ControlTarget = from;
				pet.ControlOrder = OrderType.Follow;

				pet.MoveToWorld( from.Location, from.Map );

				pet.IsStabled = false;
				from.Stabled.Remove( pet );

				Say( 1042559 ); // Here you go... and good day to you!
			}
			else
			{
				string petname = pet.Name;
				Say( petname + " remained in the stables because you have too many followers." );
			}
		}
		
		public void Claim( Mobile from )
		{
			if ( Deleted || !from.CheckAlive() )
				return;

			bool claimed = false;
			int stabled = 0;

			for ( int i = 0; i < from.Stabled.Count; ++i )
			{
				BaseCreature pet = from.Stabled[i] as BaseCreature;

				if ( pet == null || pet.Deleted )
				{
					pet.IsStabled = false;
					from.Stabled.RemoveAt( i );
					--i;
					continue;
				}

				++stabled;

				if ( (from.Followers + pet.ControlSlots) <= from.FollowersMax )
				{
					pet.SetControlMaster( from );

					if ( pet.Summoned )
						pet.SummonMaster = from;

					pet.ControlTarget = from;
					pet.ControlOrder = OrderType.Follow;

					pet.MoveToWorld( from.Location, from.Map );

					pet.IsStabled = false;
					from.Stabled.RemoveAt( i );
					--i;

					claimed = true;
				}
				else
				{
					string petname = pet.Name;
					Say( petname + " remained in the stables because you have too many followers." );
				}
			}

			if ( claimed )
				Say( 1042559 ); // Here you go... and good day to you!
			else if ( stabled == 0 )
				Say( 502671 ); // But I have no animals stabled with me at the moment!
		}


		public override bool HandlesOnSpeech{ get{ return true; } } 

		public override void OnSpeech( SpeechEventArgs e )
		{
			if ( !e.Handled && e.HasKeyword( 0x0008 ) )
			{
				e.Handled = true;
				BeginStable( e.Mobile );
			}
			else if ( !e.Handled && e.HasKeyword( 0x0009 ) )
			{
				e.Handled = true;

				if ( !Insensitive.Equals( e.Speech, "claim" ) )
					BeginClaimList( e.Mobile );
				else
					Claim( e.Mobile );
			}
			else
			{
				base.OnSpeech( e );
			}
		}

		public void Say( int number ) 
		{ 
			PublicOverheadMessage( MessageType.Regular, 0x3B2, number ); 
		} 

		public void Say( string args ) 
		{ 
			PublicOverheadMessage( MessageType.Regular, 0x3B2, false, args ); 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 ); // version 

			writer.Write( m_Sign );
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 

			switch ( version )
			{
				case 0:
				{
					m_Sign = (CivicSign)reader.ReadItem();
					break;
				}
			}
		} 
	} 
} 
