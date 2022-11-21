using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Accounting;
using Server.Gumps;


namespace Server.Items
{
	
	public class EB2 : Item
	{
		public static int MaxDonations = 21; // maximum number of donations

		public override bool Decays{ get{ return false; } } 
		

		[Constructable]
		public EB2() : base( 9434 )
		{
			Hue = 1356;//golden
			Movable = false;
			Name = "Easter Basket #2";
		}

		public EB2( Serial serial ) : base( serial )
		{
		}
		
		
		public override void OnDoubleClick(Mobile from )
		{
		
			//make sure they are a "newbie" or a GM
			if ( from.AccessLevel >= AccessLevel.Counselor )
				this.SendLocalizedMessageTo(from,1042971,"You have the authority to access the Easter Basket.");
			else if( !(CheckTag(from)) )
			{
				//Greedy!
				this.SendLocalizedMessageTo(from,1042971,"You have taken more than ONE egg per Basket! You cannot complete the Quest if you have done so!");
				return;
			}
			else if( CanGet(from) )
				this.SendLocalizedMessageTo(from,1042971,"You have found an Easter Basket! *NOTE* Only access each Basket ONCE or you will NOT be able to complete the Quest.");
			else
			{				
				this.SendLocalizedMessageTo(from,1042971,"You were supposed to access each Basket once. If you have accessed this Basket more than once, you will not be able to complete the Quest.");
				return;
			}
			

			base.OnDoubleClick (from);

			//increase tag
			if( from.AccessLevel == AccessLevel.Player )
				IncreaseTag( from, 1);

			//show gump
			from.AddToBackpack( new Eggs2() );
			from.CloseGump( typeof(EB2Gump) );
			from.SendGump( new EB2Gump( from, MaxDonations ) );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public void IncreaseTag( Mobile player, int amount )
		{
			Account acct = player.Account as Account;

			if ( acct == null )
				return;
			
			
			string tag = acct.GetTag( "numEQ1" );

			int cur;

			if ( tag == null || tag == "" )
				cur = 0;
			else
				cur = Utility.ToInt32( tag );

			acct.SetTag( "numEQ1", (cur + 1).ToString() );

		}

		public bool CheckTag( Mobile player )
		{

			Account acct = player.Account as Account;

			if ( acct == null )
				return false;
			
			//acct.SetTag( "numRewardsChosen", (cur + 1).ToString() );
			string tag = acct.GetTag( "numEQ1" );

			int cur;

			if ( tag == null || tag == "" )
				cur = 0;
			else
				cur = Utility.ToInt32( tag );

			if( cur < MaxDonations )
				return true;
			return false;
		}

		public bool CanGet( Mobile player )
		{
			

			PlayerMobile pm = (PlayerMobile)player;

			return true;
		}
	}

	public class EB2Gump : Gump 
	{
		private Mobile m_from;
		private int m_max;

		public EB2Gump( Mobile from, int max ) : base(50, 50) 
		{
			m_from = from;
			m_max = max;
			this.InitializeGump();
		}
		public override void OnResponse(NetState sender, RelayInfo info) 
		{
		}
		public virtual void InitializeGump() 
		{
			this.Closable = true;
			this.Disposable = true;
			this.Dragable = true;
			// Initializing Page
			this.AddPage(0);
			// Initializing Background
			this.AddBackground(10, 10, 270, 216, 5054);
			// Initializing Label
			this.AddLabel(90, 19, 255, "Easter Basket #2");
			// Initializing Background
			this.AddBackground(16, 39, 255, 183, 83);
			// Initializing Image
			this.AddImage(250, 20, 216);
			// Initializing Image
			this.AddImage(25, 20, 216);
			// Initializing AlphaRegion
			this.AddAlphaRegion(25, 50, 240, 162);
	
			// Initializing Html
			string temp = "";
			
			if( m_from.AccessLevel == AccessLevel.Player )
				temp = "You have opened the Easter Baskets " + GetNumberofDonations( m_from )  + " of " + m_max.ToString() + " times.<br><br>";
			else
				temp = "Due to your enlightened state, you can open the basket without limit.<br><br>";

			temp += "You have located one of the Quest Easter Baskets! A Quest Egg has been placed in your backpack.<br><br>";
			temp += "The Egg will give you a Clue to the next location.<Br><Br>";
			temp += "Remember, this is a Limited Time Quest for ALL the players!<br><br>";
			temp += "You need to KEEP all your Quest Eggs to recieve a Prize! Remember to only OPEN each Basket once.";

			this.AddHtml(28, 53, 230, 155, temp, false, true);
		}

		public int GetNumberofDonations( Mobile player )
		{
			Account acct = player.Account as Account;

			if ( acct == null )
				return 0;
			
			//acct.SetTag( "numRewardsChosen", (cur + 1).ToString() );
			string tag = acct.GetTag( "numEQ1" );

			int cur;

			if ( tag == null || tag == "" )
				cur = 0;
			else
				cur = Utility.ToInt32( tag );

			return cur;
		}
	}
}
	


