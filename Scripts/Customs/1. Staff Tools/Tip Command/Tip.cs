using System.Text.RegularExpressions;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Prompts;
using Server.Targeting;
using Server.Commands;

namespace Server.Commands
{
	/// <summary>
	/// Function .tip, allowing a GM to notify something to a player
	/// </summary>
	public class Tip_Command
	{
		/// <summary>
		/// Handler initialization
		/// </summary>
		public static void Initialize()
		{
			CommandSystem.Register( "Tip", AccessLevel.GameMaster, new CommandEventHandler( Tip_OnCommand ) );
		}

		[Usage( "Tip <msg>" )]
		[Description( "Send the message to the selected player" )]
		private static void Tip_OnCommand(CommandEventArgs e)
		{
			if( e.Arguments.Length > 0)
				e.Mobile.Target = new InternalTarget( new Tip( e.Mobile, e.ArgString, null ));
			else
			{
				e.Mobile.SendMessage("Enter your message, use the ';' character to end the tip");
//				e.Mobile.SendMessage("Entrez votre message, utilisez le point virgule pour indiquer la fin du tip.");	//French version
				e.Mobile.Prompt = new InternalPrompt( "" );
			}
		}

		/// <summary>
		/// Target to select the player to notify
		/// </summary>
		private class InternalTarget : Target
		{
			private Tip objMsg;

			public InternalTarget( Tip msg ) : base( 12, false, TargetFlags.None )
			{
				msg.GM.SendMessage("Click on the character you want to notify");
//				msg.GM.SendMessage("Cliquez sur le personnage à qui envoyer le message");	//French version
				objMsg = msg;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
				{
					objMsg.Player = (Mobile) o;
					objMsg.Display();
				}
			}
		}
		
		/// <summary>
		/// Prompt for the message of the Tip
		/// </summary>
		private class InternalPrompt : Prompt
		{
			string stTxt;

			public InternalPrompt( string txt )
			{
				stTxt = txt;
			}

			public override void OnResponse(Mobile from, string text)
			{
				// Deleting the ';' ending character
				stTxt += " " + Regex.Replace(text, ";", "");

				// If we find a ';', we can send the target to choose the player
				if(Regex.IsMatch(text, ";"))
					from.Target = new InternalTarget( new Tip( from, stTxt, null ));
				else
				{
					// Else, there is a sequel to the message
					from.SendMessage("Enter the sequel of your message, use the ';' character to end the Tip");
//					from.SendMessage("Entrez la suite de votre message, utilisez le point virgule pour indiquer la fin du tip.");	//French version
					from.Prompt = new InternalPrompt( stTxt );
				}
			}
		}
		/// <summary>
		/// Tip to send
		/// </summary>
		private class Tip
		{
			#region VARIABLES
				private Mobile objGM;				// GM who send it
				private string stTxt;				// Message
				private Mobile objPlayer;			// Reciever (Player)
			#endregion

			#region PROPERTIES
				/// <summary>
				/// Sender (GM)
				/// </summary>
				public Mobile GM
				{
					get { return objGM; }
					set { objGM = value; }
				}

				/// <summary>
				/// Message to send
				/// </summary>
				public string Text
				{
					get { return stTxt; }
					set { stTxt = value; }
				}

				/// <summary>
				/// Reciever (Player)
				/// </summary>
				public Mobile Player
				{
					get { return objPlayer; }
					set { objPlayer = value; }
				}
			#endregion

			#region CONSTRUCTOR
				/// <summary>
				/// Message to a player
				/// </summary>
				/// <param name="gm">Sender (GM)</param>
				/// <param name="txt">Text to send</param>
				/// <param name="pl">Reciever (Player)</param>
				public Tip( Mobile gm, string txt, Mobile pl )
				{
					objGM = gm;
					stTxt = txt;
					objPlayer = pl;
				}
			#endregion

			#region METHODES PUBLIQUES
				/// <summary>
				/// Display the Tip icone in the upper left corner
				/// Notify the player he get a Tip
				/// </summary>
				public void Display()
				{
					// Verify that the player is still here
					if(objPlayer == null || objPlayer.Deleted )
						return;

					// Notify the player of the Tip
					objPlayer.SendMessage("You get a tip from a GM!");
//					objPlayer.SendMessage("Vous avez reçu un tip d'un GM!");	//French version
					objPlayer.SendGump( new Tip_Gump( this ) );
				}

				/// <summary>
				/// Open the Tip
				/// </summary>
				public void Open()
				{
					// Verify that the player is still here
					if(objPlayer == null || objPlayer.Deleted )
						return;

					// Display the Tip to the player
					objPlayer.SendGump( new Tip_Detailled_Gump( this ) );
				}

				/// <summary>
				/// Ask the player for the answer he want to send to the sender
				/// </summary>
				public void Reply()
				{
					// Verify that the player is still here
					if(objPlayer == null || objPlayer.Deleted )
						return;

					// Ask the player for the answer
					objPlayer.SendMessage("Enter your answer, it will be send to the GM.");
//					objPlayer.SendMessage("Entrez votre réponse, celle-ci sera envoyée au GM.");	//French version
					objPlayer.Prompt = new Tip_Prompt( this );
				}
				
				/// <summary>
				/// Send the player's reply to the GM
				/// </summary>
				/// <param name="from">Player</param>
				/// <param name="text">Answer</param>
				public void Reply( Mobile from, string text)
				{
					// Verify that the GM is still here
					if(objGM == null || objGM.Deleted )
					{
						from.SendMessage("Sorry, the GM can't be found.");
//						from.SendMessage("Désolé, le GM n'est plus là.");	//French version
						return;
					}

					// Notify the GM of the answer
					objGM.SendMessage(from.Name + " is answering :");
					objGM.SendMessage( text );
				}
			#endregion

			/// <summary>
			/// Tip's notification Gump (small)
			/// </summary>
			private class Tip_Gump : Gump
			{
				private Tip objMsg;

				public Tip_Gump( Tip msg ) : base(0, 20)
				{
					objMsg = msg;

					this.Closable=false;
					this.Disposable=false;
					this.Dragable=false;
					this.Resizable=false;
					this.AddPage(0);
					this.AddButton(0, 0, 2507, 2507, (int)Buttons.Button1, GumpButtonType.Reply, 0);
				}

				public enum Buttons
				{
					Button1,
				}

				public override void OnResponse(NetState sender, RelayInfo info)
				{
					switch( info.ButtonID )
					{
						case (int)Buttons.Button1:
							objMsg.Open();
							break;
					}
				}
			}


			/// <summary>
			/// Tip Gump
			/// </summary>
			private class Tip_Detailled_Gump : Gump
			{
				private Tip objMsg;

				public Tip_Detailled_Gump( Tip msg ) : base(50, 40)
				{
					objMsg = msg;

					this.Closable=true;
					this.Disposable=true;
					this.Dragable=true;
					this.Resizable=false;
					this.AddPage(0);
					this.AddBackground(0, 0, 250, 250, 5170);
					this.AddHtml( 20, 24, 210, 200, "<BASEFONT COLOR=BLACK>" + msg.Text + "</BASEFONT>", false, true);
					this.AddButton(190, 228, 2180, 2180, (int)Buttons.Button3, GumpButtonType.Reply, 0);
					this.AddImage(100, 8, 2506);
				}

				public enum Buttons
				{
					Exit,
					Button3,
				}

				public override void OnResponse(NetState sender, RelayInfo info)
				{
					switch( info.ButtonID )
					{
						case (int)Buttons.Button3:
							objMsg.Reply();
							break;
						case (int)Buttons.Exit : break;
					}
				}
			}

			/// <summary>
			/// Prompt to get the player's answer
			/// </summary>
			private class Tip_Prompt : Prompt
			{
				Tip objMsg;
				public Tip_Prompt( Tip msg )
				{
					objMsg = msg;
				}

				public override void OnResponse(Mobile from, string text)
				{
					objMsg.Reply(from, text);
				}
			}
		}
	}
}
