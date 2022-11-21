/**********************************************************
RunUO 2.0 AoV C# script file
Official Age of Valor Script :: http://www.uovalor.com
Last modified by Death on Jan-25-2011 08:34:26pm
Filepath: scripts\_custom\TileFlipPuzzle.cs
Lines of code: 664
***********************************************************/


using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Network;
using System.Diagnostics; //Needed for stopwatch

namespace Server.Items
{
	public class TileFlipPuzzle : BaseAddon
	{
		[Constructable]
		public TileFlipPuzzle() : this(Utility.RandomMinMax( 3, 6 )) //Utility.RandomMinMax( 3, 6 )
		{
		}

		[Constructable]
		public TileFlipPuzzle( int sideLength )
		{
			AddComponent( new TileFlipPuzzleControlPanel( sideLength ), 1, 0, -2 );
		}

		//public override bool ShareHue{ get{ return false; } }

		public TileFlipPuzzle( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class TileFlipPuzzleControlPanel : AddonComponent
	{
		private static readonly TimeSpan m_UseTimeout = TimeSpan.FromMinutes( 2.0 );

		/*public struct Node
		{
			private int m_X;
			private int m_Y;

			public int X{ get{ return m_X; } set{ m_X = value; } }
			public int Y{ get{ return m_Y; } set{ m_Y = value; } }

			public Node( int x, int y )
			{
				m_X = x;
				m_Y = y;
			}
		}*/

		private int m_SideLength;
		//private Node[] m_Path;

		[CommandProperty( AccessLevel.GameMaster )]
		public int SideLength
		{
			get{ return m_SideLength; }
			set
			{
				if ( value < 3 )
					value = 3;
				else if ( value > 6 )
					value = 6;

				if ( m_SideLength != value )
				{
					m_SideLength = value;
				}
			}
		}
		
		public TileFlipPuzzleControlPanel( int sideLength ) : base( 12289 )
		{
			Hue = 1161;
			Name = "Demon Pillar\n(Double Click)";
			SideLength = sideLength;
		}

		private Mobile m_User;
		private DateTime m_LastUse;
		private bool[] flippedTiles = new bool[37];
		private Stopwatch stopwatch = new Stopwatch();
		private int varient;

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( this, 3 ) )
			{
				from.SendLocalizedMessage( 500446 ); // That is too far away.
				return;
			}

			if ( m_User != null )
			{
				if ( m_User == from )
					return;

				if ( m_User.Deleted || m_User.Map != Map || !m_User.InRange( this, 3 )
					|| m_User.NetState == null || DateTime.Now - m_LastUse >= m_UseTimeout )
				{
					m_User.CloseGump( typeof( TileFlipPuzzleGameGump ) );
				}
				else
				{
					from.SendMessage( "Someone is currently using the control panel." );
					return;
				}
			}

			m_User = from;
			m_LastUse = DateTime.Now;
			
			//Console.WriteLine("Puzzle Size: " + this.SideLength);
			
			if (this.SideLength == 3) {
				flippedTiles[1] = true; flippedTiles[3] = true; flippedTiles[7] = true; flippedTiles[9] = true;
				/*switch (Utility.Random( 5 ))
				{
					case 0:
						flippedTiles[1] = true; flippedTiles[9] = true; varient = 1; break;
					case 1:
						flippedTiles[4] = true; flippedTiles[2] = true; flippedTiles[8] = true; flippedTiles[6] = true; varient = 2; break;
					case 2:
						flippedTiles[1] = true; flippedTiles[4] = true; flippedTiles[7] = true; flippedTiles[2] = true; flippedTiles[8] = true; flippedTiles[3] = true; flippedTiles[6] = true;
						flippedTiles[9] = true; varient = 3; break;
					case 3: 
						flippedTiles[2] = true; flippedTiles[5] = true; flippedTiles[8] = true; varient = 4; break;
					case 4: 
						flippedTiles[1] = true; flippedTiles[7] = true; flippedTiles[5] = true; flippedTiles[3] = true; flippedTiles[9] = true; varient = 5; break;
				}*/
			}
			else if (this.SideLength == 4) {
				flippedTiles[6] = true; flippedTiles[10] = true; flippedTiles[7] = true; flippedTiles[11] = true;
				/*switch (Utility.Random( 5 ))
				{
					case 0: //
						flippedTiles[6] = true; flippedTiles[10] = true; flippedTiles[7] = true; flippedTiles[11] = true; flippedTiles[1] = true; flippedTiles[13] = true; flippedTiles[4] = true; 
						flippedTiles[16] = true; varient = 1; break;
					case 1:
						flippedTiles[1] = true; flippedTiles[5] = true; flippedTiles[2] = true; flippedTiles[6] = true; flippedTiles[11] = true; flippedTiles[15] = true; flippedTiles[12] = true;
						flippedTiles[16] = true; varient = 2; break;
					case 2:
						flippedTiles[1] = true; flippedTiles[6] = true; flippedTiles[11] = true; flippedTiles[16] = true; varient = 3; break;
					case 3: 
						flippedTiles[1] = true; flippedTiles[9] = true; flippedTiles[6] = true; flippedTiles[14] = true; flippedTiles[3] = true; flippedTiles[11] = true; flippedTiles[8] = true;
						flippedTiles[16] = true; varient = 4; break;
					case 4: //Solvable
						flippedTiles[2] = true; flippedTiles[3] = true; flippedTiles[14] = true; flippedTiles[15] = true; varient = 5; break;
				}*/
			}
			else if (this.SideLength == 5) {
				flippedTiles[11] = true; flippedTiles[7] = true; flippedTiles[12] = true; flippedTiles[17] = true; flippedTiles[3] = true; flippedTiles[8] = true; flippedTiles[13] = true; 
				flippedTiles[18] = true; flippedTiles[23] = true; flippedTiles[9] = true; flippedTiles[14] = true; flippedTiles[19] = true; flippedTiles[15] = true;
				/*switch (Utility.Random( 5 ))
				{
					case 0: //Solvable (real easy)
						flippedTiles[11] = true; flippedTiles[7] = true; flippedTiles[17] = true; flippedTiles[3] = true; flippedTiles[23] = true; flippedTiles[9] = true; flippedTiles[19] = true;
						flippedTiles[15] = true; varient = 1; break;
					case 1:
						flippedTiles[1] = true; flippedTiles[2] = true; flippedTiles[6] = true; flippedTiles[16] = true; flippedTiles[21] = true; flippedTiles[22] = true; flippedTiles[24] = true;
						flippedTiles[25] = true; flippedTiles[20] = true; flippedTiles[4] = true; flippedTiles[5] = true; flippedTiles[10] = true; varient = 2; break;
					case 2:
						flippedTiles[7] = true; flippedTiles[17] = true; flippedTiles[9] = true; flippedTiles[19] = true; flippedTiles[1] = true; flippedTiles[5] = true; flippedTiles[21] = true;
						flippedTiles[25] = true; varient = 3; break;
					case 3: //solvable
						flippedTiles[1] = true; flippedTiles[2] = true; flippedTiles[3] = true; flippedTiles[4] = true; flippedTiles[5] = true; flippedTiles[6] = true; flippedTiles[11] = true;
						flippedTiles[16] = true; flippedTiles[21] = true; flippedTiles[22] = true; flippedTiles[23] = true; flippedTiles[24] = true; flippedTiles[25] = true; flippedTiles[10] = true;
						flippedTiles[15] = true; flippedTiles[20] = true; varient = 4; break;
					case 4: //solvable
						flippedTiles[2] = true; flippedTiles[3] = true; flippedTiles[4] = true; flippedTiles[22] = true; flippedTiles[23] = true; flippedTiles[24] = true; varient = 5; break;
				}*/
			}
			else if (this.SideLength == 6) {
				flippedTiles[13] = true; flippedTiles[19] = true; flippedTiles[8] = true; flippedTiles[14] = true; flippedTiles[20] = true; flippedTiles[26] = true; flippedTiles[3] = true; 
				flippedTiles[9] = true; flippedTiles[15] = true; flippedTiles[21] = true; flippedTiles[27] = true; flippedTiles[33] = true; flippedTiles[4] = true; flippedTiles[10] = true;
				flippedTiles[16] = true; flippedTiles[22] = true; flippedTiles[28] = true; flippedTiles[34] = true; flippedTiles[11] = true; flippedTiles[17] = true; flippedTiles[23] = true; 
				flippedTiles[29] = true; flippedTiles[18] = true; flippedTiles[24] = true;
				/*switch (Utility.Random( 5 ))
				{
					case 0: //Solvable (real easy)
						flippedTiles[12] = true; varient = 1; break;
					case 1:
						flippedTiles[12] = true; varient = 1; break;
					case 2:
						flippedTiles[12] = true; varient = 1; break;
					case 3: //solvable
						flippedTiles[12] = true; varient = 1; break;
					case 4: //solvable
						flippedTiles[12] = true; varient = 1; break;
				}*/
			}
			
			stopwatch.Start();
			from.SendGump( new TileFlipPuzzleGameGump( this, from, ref flippedTiles, varient) );
		}

		private class TileFlipPuzzleGameGump : Gump
		{
			private enum NodeHue
			{
				Gray,
				Blue,
				Red
			}

			private TileFlipPuzzleControlPanel m_Panel;
			private Mobile m_From;
			private int m_Varient;
			private bool[] m_Array;
			private string level;
			

			private int buttonCount;
			private int c;


			public TileFlipPuzzleGameGump( TileFlipPuzzleControlPanel panel, Mobile from, ref bool[] arr, int varient) : base( 5, 30 )
			{
				m_Panel = panel;
				m_From = from;
				m_Varient = varient;
				
				m_Panel.DoDamage( m_From );
				//m_Step = step;
				
				m_Array = arr;
				
				int sideLength = panel.SideLength;
				
				switch ( sideLength )
				{
					case 3: 
						level = "Beginner";
						break;
					case 4:
						level = "Intermediate";
						break;
					case 5:
						level = "Expert";
						break;
					case 6:
						level = "Master";
						break;
				}

				AddBackground( 50, 0, 530, 410, 0xA28 );
				AddBackground( 95, 20, 442, 90, 0xA28 );

				/*AddHtml( 143, 35, 300, 45, "TILE FLIP PUZZLE (" + level + ", #" + m_Varient + ")", false, false );

				AddHtml( 143, 60, 300, 70, "Click a tile to flip. All joining tiles will also", false, false );
				AddHtml( 143, 75, 300, 85, "flip. Change all tiles to green gems to win.", false, false );*/
				
				AddHtml( 143, 35, 300, 100, "(" + level + ", #" + m_Varient + ")", false, false );
				AddHtml( 143, 50, 300, 100, "Click a tile to flip. All joining tiles will also flip.", false, false );
				AddHtml( 143, 65, 300, 100, "Destroy the demon pillar when all tiles are green. ", false, false );
				
				//AddBackground( 200, 115, 30 + 40 * sideLength, 30 + 40 * sideLength, 0xA28 );
				//AddBackground( 210, 125, 10 + 40 * sideLength, 10 + 40 * sideLength, 0x1400 );
				
				if (sideLength == 3) {
					AddBackground( 240, 175, 30 + 40 * sideLength, 30 + 40 * sideLength, 0xA28 );
					AddBackground( 250, 185, 10 + 40 * sideLength, 10 + 40 * sideLength, 0x1400 );
				}
				else if (sideLength == 4) {
					AddBackground( 220, 157, 30 + 40 * sideLength, 30 + 40 * sideLength, 0xA28 );
					AddBackground( 230, 167, 10 + 40 * sideLength, 10 + 40 * sideLength, 0x1400 );
				}
				else if (sideLength == 5) {
					AddBackground( 200, 136, 30 + 40 * sideLength, 30 + 40 * sideLength, 0xA28 );
					AddBackground( 210, 146, 10 + 40 * sideLength, 10 + 40 * sideLength, 0x1400 );
				}
				else if (sideLength == 6) {
					AddBackground( 180, 115, 30 + 40 * sideLength, 30 + 40 * sideLength, 0xA28 );
					AddBackground( 190, 125, 10 + 40 * sideLength, 10 + 40 * sideLength, 0x1400 );
				}
				
				
				//Console.WriteLine("BEFORE CRASH2\n");
				//Console.ReadKey();

				for ( int i = 0; i < sideLength; i++ )
				{
					for ( int j = 0; j < sideLength; j++ )
					{
						buttonCount++;
						if(m_Array[buttonCount] == true) {
							if (sideLength == 3) {
								AddButton( 265 + 40 * i, 195 + 40 * j, 0x2C56, 0x2C56, buttonCount, GumpButtonType.Reply, 0); // Press
							}
							else if (sideLength == 4) {
								AddButton( 245 + 40 * i, 177 + 40 * j, 0x2C56, 0x2C56, buttonCount, GumpButtonType.Reply, 0); // Press
							}
							else if (sideLength == 5) {
								AddButton( 225 + 40 * i, 156 + 40 * j, 0x2C56, 0x2C56, buttonCount, GumpButtonType.Reply, 0); // Press
							}
							else if (sideLength == 6) {
								AddButton( 205 + 40 * i, 135 + 40 * j, 0x2C56, 0x2C56, buttonCount, GumpButtonType.Reply, 0); // Press
							}
						}
						else {
							if (sideLength == 3) {
								AddButton( 265 + 40 * i, 195 + 40 * j, 0x2C6E, 0x2C6E, buttonCount, GumpButtonType.Reply, 0); // Press
							}
							else if (sideLength == 4) {
								AddButton( 245 + 40 * i, 177 + 40 * j, 0x2C6E, 0x2C6E, buttonCount, GumpButtonType.Reply, 0); // Press
							}
							else if (sideLength == 5) {
								AddButton( 225 + 40 * i, 156 + 40 * j, 0x2C6E, 0x2C6E, buttonCount, GumpButtonType.Reply, 0); // Press
							}
							else if (sideLength == 6) {
								AddButton( 205 + 40 * i, 135 + 40 * j, 0x2C6E, 0x2C6E, buttonCount, GumpButtonType.Reply, 0); // Press
							}
						}
					}
				}
			}

			public override void OnResponse( NetState sender, RelayInfo info )
			{
				if ( m_Panel.Deleted || !m_From.CheckAlive() || info.ButtonID == 0 )
				{
					m_Panel.m_User = null;
					return;
				}

				if ( m_From.Map != m_Panel.Map || !m_From.InRange( m_Panel, 3 ) )
				{
					m_From.SendLocalizedMessage( 500446 ); // That is too far away.
					m_Panel.m_User = null;
					return;
				}

					switch ( info.ButtonID )
					{
						case 1: 
							if (m_Panel.SideLength == 3) {
								m_Array[1] = !m_Array[1]; m_Array[2] = !m_Array[2]; m_Array[4] = !m_Array[4];
							}
							else if (m_Panel.SideLength == 4) {
								m_Array[1] = !m_Array[1]; m_Array[2] = !m_Array[2]; m_Array[5] = !m_Array[5];
							}
							else if (m_Panel.SideLength == 5) {
								m_Array[1] = !m_Array[1]; m_Array[2] = !m_Array[2]; m_Array[6] = !m_Array[6];
							}
							else if (m_Panel.SideLength == 6) {
								m_Array[1] = !m_Array[1]; m_Array[2] = !m_Array[2]; m_Array[7] = !m_Array[7];
							}
							break;
						case 2: 
							if (m_Panel.SideLength == 3) {
								m_Array[1] = !m_Array[1]; m_Array[2] = !m_Array[2]; m_Array[3] = !m_Array[3]; m_Array[5] = !m_Array[5];
							}
							else if (m_Panel.SideLength == 4) {
								m_Array[1] = !m_Array[1]; m_Array[2] = !m_Array[2]; m_Array[3] = !m_Array[3]; m_Array[6] = !m_Array[6];
							}
							else if (m_Panel.SideLength == 5) {
								m_Array[1] = !m_Array[1]; m_Array[2] = !m_Array[2]; m_Array[3] = !m_Array[3]; m_Array[7] = !m_Array[7];
							}
							else if (m_Panel.SideLength == 6) {
								m_Array[1] = !m_Array[1]; m_Array[2] = !m_Array[2]; m_Array[3] = !m_Array[3]; m_Array[8] = !m_Array[8];
							}
							break;
						case 3:				
							if (m_Panel.SideLength == 3) {
								m_Array[2] = !m_Array[2]; m_Array[3] = !m_Array[3]; m_Array[6] = !m_Array[6];
							}				
							else if (m_Panel.SideLength == 4) {
								m_Array[2] = !m_Array[2]; m_Array[3] = !m_Array[3]; m_Array[4] = !m_Array[4]; m_Array[7] = !m_Array[7];
							}
							else if (m_Panel.SideLength == 5) {
								m_Array[2] = !m_Array[2]; m_Array[3] = !m_Array[3]; m_Array[4] = !m_Array[4]; m_Array[8] = !m_Array[8];
							}
							else if (m_Panel.SideLength == 6) {
								m_Array[2] = !m_Array[2]; m_Array[3] = !m_Array[3]; m_Array[4] = !m_Array[4]; m_Array[9] = !m_Array[9];
							}
							break;
						case 4: 
							if (m_Panel.SideLength == 3) {
								m_Array[1] = !m_Array[1]; m_Array[4] = !m_Array[4]; m_Array[5] = !m_Array[5]; m_Array[7] = !m_Array[7];
							}	
							else if (m_Panel.SideLength == 4) {
								m_Array[3] = !m_Array[3]; m_Array[4] = !m_Array[4]; m_Array[8] = !m_Array[8];
							}
							else if (m_Panel.SideLength == 5) {
								m_Array[3] = !m_Array[3]; m_Array[4] = !m_Array[4]; m_Array[5] = !m_Array[5]; m_Array[9] = !m_Array[9];
							}
							else if (m_Panel.SideLength == 6) {
								m_Array[3] = !m_Array[3]; m_Array[4] = !m_Array[4]; m_Array[5] = !m_Array[5]; m_Array[10] = !m_Array[10];
							}
							break;
						case 5: 
							if (m_Panel.SideLength == 3) {
								m_Array[4] = !m_Array[4]; m_Array[5] = !m_Array[5]; m_Array[6] = !m_Array[6]; m_Array[2] = !m_Array[2]; m_Array[8] = !m_Array[8];
							}	
							else if (m_Panel.SideLength == 4) {
								m_Array[1] = !m_Array[1]; m_Array[5] = !m_Array[5]; m_Array[6] = !m_Array[6]; m_Array[9] = !m_Array[9];
							}
							else if (m_Panel.SideLength == 5) {
								m_Array[4] = !m_Array[4]; m_Array[5] = !m_Array[5]; m_Array[10] = !m_Array[10];
							}
							else if (m_Panel.SideLength == 6) {
								m_Array[4] = !m_Array[4]; m_Array[5] = !m_Array[5]; m_Array[6] = !m_Array[6]; m_Array[11] = !m_Array[11];
							}
							break;
						case 6:
							if (m_Panel.SideLength == 3) {
								m_Array[3] = !m_Array[3]; m_Array[5] = !m_Array[5]; m_Array[6] = !m_Array[6]; m_Array[9] = !m_Array[9];
							}	
							else if (m_Panel.SideLength == 4) {
								m_Array[2] = !m_Array[2]; m_Array[5] = !m_Array[5]; m_Array[6] = !m_Array[6]; m_Array[7] = !m_Array[7]; m_Array[10] = !m_Array[10];
							}
							else if (m_Panel.SideLength == 5) {
								m_Array[1] = !m_Array[1]; m_Array[6] = !m_Array[6]; m_Array[7] = !m_Array[7]; m_Array[11] = !m_Array[11];
							}
							else if (m_Panel.SideLength == 6) {
								m_Array[5] = !m_Array[5]; m_Array[6] = !m_Array[6]; m_Array[12] = !m_Array[12];
							}
							break;
						case 7:  
							if (m_Panel.SideLength == 3) {
								m_Array[4] = !m_Array[4]; m_Array[7] = !m_Array[7]; m_Array[8] = !m_Array[8];
							}	
							else if (m_Panel.SideLength == 4) {
								m_Array[3] = !m_Array[3]; m_Array[6] = !m_Array[6]; m_Array[11] = !m_Array[11]; m_Array[7] = !m_Array[7]; m_Array[8] = !m_Array[8];
							}
							else if (m_Panel.SideLength == 5) {
								m_Array[6] = !m_Array[6]; m_Array[7] = !m_Array[7]; m_Array[8] = !m_Array[8]; m_Array[2] = !m_Array[2]; m_Array[12] = !m_Array[12];
							}
							else if (m_Panel.SideLength == 6) {
								m_Array[1] = !m_Array[1]; m_Array[7] = !m_Array[7]; m_Array[8] = !m_Array[8]; m_Array[13] = !m_Array[13];
							}
							break;
						case 8: 
							if (m_Panel.SideLength == 3) {
								m_Array[5] = !m_Array[5]; m_Array[7] = !m_Array[7]; m_Array[8] = !m_Array[8]; m_Array[9] = !m_Array[9];
							}	
							else if (m_Panel.SideLength == 4) {
								m_Array[4] = !m_Array[4]; m_Array[8] = !m_Array[8]; m_Array[7] = !m_Array[7]; m_Array[12] = !m_Array[12];
							}
							else if (m_Panel.SideLength == 5) {
								m_Array[7] = !m_Array[7]; m_Array[8] = !m_Array[8]; m_Array[9] = !m_Array[9]; m_Array[3] = !m_Array[3]; m_Array[13] = !m_Array[13];
							}
							else if (m_Panel.SideLength == 6) {
								m_Array[7] = !m_Array[7]; m_Array[8] = !m_Array[8]; m_Array[9] = !m_Array[9]; m_Array[2] = !m_Array[2]; m_Array[14] = !m_Array[14];
							}
							break;
						case 9: 
							if (m_Panel.SideLength == 3) {
								m_Array[6] = !m_Array[6]; m_Array[8] = !m_Array[8]; m_Array[9] = !m_Array[9];
							}	
							else if (m_Panel.SideLength == 4) {
								m_Array[5] = !m_Array[5]; m_Array[9] = !m_Array[9]; m_Array[13] = !m_Array[13]; m_Array[10] = !m_Array[10];
							}
							else if (m_Panel.SideLength == 5) {
								m_Array[8] = !m_Array[8]; m_Array[9] = !m_Array[9]; m_Array[10] = !m_Array[10]; m_Array[4] = !m_Array[4]; m_Array[14] = !m_Array[14];
							}
							else if (m_Panel.SideLength == 6) {
								m_Array[8] = !m_Array[8]; m_Array[9] = !m_Array[9]; m_Array[10] = !m_Array[10]; m_Array[3] = !m_Array[3]; m_Array[15] = !m_Array[15];
							}
							break;
						case 10:
							if (m_Panel.SideLength == 4) {
								m_Array[9] = !m_Array[9]; m_Array[6] = !m_Array[6]; m_Array[10] = !m_Array[10]; m_Array[14] = !m_Array[14]; m_Array[11] = !m_Array[11];
							}
							else if (m_Panel.SideLength == 5) {
								m_Array[9] = !m_Array[9]; m_Array[10] = !m_Array[10]; m_Array[5] = !m_Array[5]; m_Array[15] = !m_Array[15];
							}
							else if (m_Panel.SideLength == 6) {
								m_Array[4] = !m_Array[4]; m_Array[9] = !m_Array[9]; m_Array[10] = !m_Array[10]; m_Array[11] = !m_Array[11]; m_Array[16] = !m_Array[16];
							}
							break;
						case 11:
							if (m_Panel.SideLength == 4) {
								m_Array[10] = !m_Array[10]; m_Array[7] = !m_Array[7]; m_Array[11] = !m_Array[11]; m_Array[15] = !m_Array[15]; m_Array[12] = !m_Array[12];
							}
							else if (m_Panel.SideLength == 5) {
								m_Array[11] = !m_Array[11]; m_Array[12] = !m_Array[12]; m_Array[6] = !m_Array[6]; m_Array[16] = !m_Array[16];
							}
							else if (m_Panel.SideLength == 6) {
								m_Array[10] = !m_Array[10]; m_Array[11] = !m_Array[11]; m_Array[12] = !m_Array[12]; m_Array[5] = !m_Array[5]; m_Array[17] = !m_Array[17];
							}
							break;
						case 12: 
							if (m_Panel.SideLength == 4) {
								m_Array[11] = !m_Array[11]; m_Array[8] = !m_Array[8]; m_Array[12] = !m_Array[12]; m_Array[16] = !m_Array[16];
							}
							else if (m_Panel.SideLength == 5) {
								m_Array[11] = !m_Array[11]; m_Array[12] = !m_Array[12]; m_Array[13] = !m_Array[13]; m_Array[7] = !m_Array[7]; m_Array[17] = !m_Array[17];
							}
							else if (m_Panel.SideLength == 6) {
								m_Array[11] = !m_Array[11]; m_Array[12] = !m_Array[12]; m_Array[6] = !m_Array[6]; m_Array[18] = !m_Array[18];
							}
							break;
						case 13: 
							if (m_Panel.SideLength == 4) {
								m_Array[9] = !m_Array[9]; m_Array[13] = !m_Array[13]; m_Array[14] = !m_Array[14];
							}
							else if (m_Panel.SideLength == 5) {
								m_Array[12] = !m_Array[12]; m_Array[13] = !m_Array[13]; m_Array[14] = !m_Array[14]; m_Array[8] = !m_Array[8]; m_Array[18] = !m_Array[18];
							}
							else if (m_Panel.SideLength == 6) {
								m_Array[13] = !m_Array[13]; m_Array[14] = !m_Array[14]; m_Array[7] = !m_Array[7]; m_Array[19] = !m_Array[19];
							}
							break;
						case 14: 
							if (m_Panel.SideLength == 4) {
								m_Array[13] = !m_Array[13]; m_Array[14] = !m_Array[14]; m_Array[15] = !m_Array[15]; m_Array[10] = !m_Array[10];
							}
							else if (m_Panel.SideLength == 5) {
								m_Array[13] = !m_Array[13]; m_Array[14] = !m_Array[14]; m_Array[15] = !m_Array[15]; m_Array[9] = !m_Array[9]; m_Array[19] = !m_Array[19];
							}
							else if (m_Panel.SideLength == 6) {
								m_Array[13] = !m_Array[13]; m_Array[14] = !m_Array[14]; m_Array[15] = !m_Array[15]; m_Array[8] = !m_Array[8]; m_Array[20] = !m_Array[20];
							}
							break;
						case 15: 
							if (m_Panel.SideLength == 4) {
								m_Array[14] = !m_Array[14]; m_Array[15] = !m_Array[15]; m_Array[16] = !m_Array[16]; m_Array[11] = !m_Array[11];
							}
							else if (m_Panel.SideLength == 5) {
								m_Array[14] = !m_Array[14]; m_Array[15] = !m_Array[15]; m_Array[10] = !m_Array[10]; m_Array[20] = !m_Array[20];
							}
							else if (m_Panel.SideLength == 6) {
								m_Array[14] = !m_Array[14]; m_Array[15] = !m_Array[15]; m_Array[16] = !m_Array[16]; m_Array[9] = !m_Array[9]; m_Array[21] = !m_Array[21];
							}
							break;
						case 16: 
							if (m_Panel.SideLength == 4) {
								m_Array[15] = !m_Array[15]; m_Array[16] = !m_Array[16]; m_Array[12] = !m_Array[12];
							}
							else if (m_Panel.SideLength == 5) {
								m_Array[16] = !m_Array[16]; m_Array[17] = !m_Array[17]; m_Array[11] = !m_Array[11]; m_Array[21] = !m_Array[21];
							}
							else if (m_Panel.SideLength == 6) {
								m_Array[15] = !m_Array[15]; m_Array[16] = !m_Array[16]; m_Array[17] = !m_Array[17]; m_Array[10] = !m_Array[10]; m_Array[22] = !m_Array[22];
							}
							break;
						case 17: 
							if (m_Panel.SideLength == 5) {
								m_Array[16] = !m_Array[16]; m_Array[17] = !m_Array[17]; m_Array[18] = !m_Array[18]; m_Array[12] = !m_Array[12]; m_Array[22] = !m_Array[22];
							}
							else if (m_Panel.SideLength == 6) {
								m_Array[16] = !m_Array[16]; m_Array[17] = !m_Array[17]; m_Array[18] = !m_Array[18]; m_Array[11] = !m_Array[11]; m_Array[23] = !m_Array[23];
							}
							break;
						case 18: 
							if (m_Panel.SideLength == 5) {
								m_Array[17] = !m_Array[17]; m_Array[18] = !m_Array[18]; m_Array[19] = !m_Array[19]; m_Array[13] = !m_Array[13]; m_Array[23] = !m_Array[23];
							}
							else if (m_Panel.SideLength == 6) {
								m_Array[17] = !m_Array[17]; m_Array[18] = !m_Array[18]; m_Array[12] = !m_Array[12]; m_Array[24] = !m_Array[24];
							}
							break;
						case 19: 
							if (m_Panel.SideLength == 5) {
								m_Array[18] = !m_Array[18]; m_Array[19] = !m_Array[19]; m_Array[20] = !m_Array[20]; m_Array[14] = !m_Array[14]; m_Array[24] = !m_Array[24];
							}
							else if (m_Panel.SideLength == 6) {
								m_Array[19] = !m_Array[19]; m_Array[20] = !m_Array[20]; m_Array[13] = !m_Array[13]; m_Array[25] = !m_Array[25];
							}
							break;
						case 20: 
							if (m_Panel.SideLength == 5) {
								m_Array[19] = !m_Array[19]; m_Array[20] = !m_Array[20]; m_Array[15] = !m_Array[15]; m_Array[25] = !m_Array[25];
							}
							else if (m_Panel.SideLength == 6) {
								m_Array[19] = !m_Array[19]; m_Array[20] = !m_Array[20]; m_Array[21] = !m_Array[21]; m_Array[14] = !m_Array[14]; m_Array[26] = !m_Array[26];
							}
							break;
						case 21: 
							if (m_Panel.SideLength == 5) {
								m_Array[16] = !m_Array[16]; m_Array[21] = !m_Array[21]; m_Array[22] = !m_Array[22];
							}
							else if (m_Panel.SideLength == 6) {
								m_Array[20] = !m_Array[20]; m_Array[21] = !m_Array[21]; m_Array[22] = !m_Array[22]; m_Array[15] = !m_Array[15]; m_Array[27] = !m_Array[27];
							}
							break;
						case 22:
							if (m_Panel.SideLength == 5) {
								m_Array[21] = !m_Array[21]; m_Array[22] = !m_Array[22]; m_Array[23] = !m_Array[23]; m_Array[17] = !m_Array[17];
							}
							else if (m_Panel.SideLength == 6) {
								m_Array[21] = !m_Array[21]; m_Array[22] = !m_Array[22]; m_Array[23] = !m_Array[23]; m_Array[16] = !m_Array[16]; m_Array[28] = !m_Array[28];
							}
							break;
						case 23: 
							if (m_Panel.SideLength == 5) {
								m_Array[22] = !m_Array[22]; m_Array[23] = !m_Array[23]; m_Array[24] = !m_Array[24]; m_Array[18] = !m_Array[18];
							}
							else if (m_Panel.SideLength == 6) {
								m_Array[22] = !m_Array[22]; m_Array[23] = !m_Array[23]; m_Array[24] = !m_Array[24]; m_Array[17] = !m_Array[17]; m_Array[29] = !m_Array[29];
							}
							break;
						case 24: 
							if (m_Panel.SideLength == 5) {
								m_Array[23] = !m_Array[23]; m_Array[24] = !m_Array[24]; m_Array[25] = !m_Array[25]; m_Array[19] = !m_Array[19];
							}
							else if (m_Panel.SideLength == 6) {
								m_Array[23] = !m_Array[23]; m_Array[24] = !m_Array[24]; m_Array[18] = !m_Array[18]; m_Array[30] = !m_Array[30];
							}
							break;
						case 25: 
							if (m_Panel.SideLength == 5) {
								m_Array[24] = !m_Array[24]; m_Array[25] = !m_Array[25]; m_Array[20] = !m_Array[20];
							}
							else if (m_Panel.SideLength == 6) {
								m_Array[25] = !m_Array[25]; m_Array[26] = !m_Array[26]; m_Array[19] = !m_Array[19]; m_Array[31] = !m_Array[31];
							}
							break;
						case 26: 
							if (m_Panel.SideLength == 6) {
								m_Array[25] = !m_Array[25]; m_Array[26] = !m_Array[26]; m_Array[27] = !m_Array[27]; m_Array[20] = !m_Array[20]; m_Array[32] = !m_Array[32];
							}
							break;
						case 27: 
							if (m_Panel.SideLength == 6) {
								m_Array[26] = !m_Array[26]; m_Array[27] = !m_Array[27]; m_Array[28] = !m_Array[28]; m_Array[21] = !m_Array[21]; m_Array[33] = !m_Array[33];
							}
							break;
						case 28: 
							if (m_Panel.SideLength == 6) {
								m_Array[27] = !m_Array[27]; m_Array[28] = !m_Array[28]; m_Array[29] = !m_Array[29]; m_Array[22] = !m_Array[22]; m_Array[34] = !m_Array[34];
							}
							break;
						case 29: 
							if (m_Panel.SideLength == 6) {
								m_Array[28] = !m_Array[28]; m_Array[29] = !m_Array[29]; m_Array[30] = !m_Array[30]; m_Array[23] = !m_Array[23]; m_Array[35] = !m_Array[35];
							}
							break;
						case 30: 
							if (m_Panel.SideLength == 6) {
								m_Array[29] = !m_Array[29]; m_Array[30] = !m_Array[30]; m_Array[24] = !m_Array[24]; m_Array[36] = !m_Array[36];
							}
							break;
						case 31: 
							if (m_Panel.SideLength == 6) {
								m_Array[25] = !m_Array[25]; m_Array[31] = !m_Array[31]; m_Array[32] = !m_Array[32];
							}
							break;
						case 32: 
							if (m_Panel.SideLength == 6) {
								m_Array[26] = !m_Array[26]; m_Array[31] = !m_Array[31]; m_Array[32] = !m_Array[32]; m_Array[33] = !m_Array[33];
							}
							break;
						case 33: 
							if (m_Panel.SideLength == 6) {
								m_Array[27] = !m_Array[27]; m_Array[32] = !m_Array[32]; m_Array[33] = !m_Array[33]; m_Array[34] = !m_Array[34];
							}
							break;
						case 34: 
							if (m_Panel.SideLength == 6) {
								m_Array[28] = !m_Array[28]; m_Array[33] = !m_Array[33]; m_Array[34] = !m_Array[34]; m_Array[35] = !m_Array[35];
							}
							break;
						case 35: 
							if (m_Panel.SideLength == 6) {
								m_Array[29] = !m_Array[29]; m_Array[34] = !m_Array[34]; m_Array[35] = !m_Array[35]; m_Array[36] = !m_Array[36];
							}
							break;
						case 36: 
							if (m_Panel.SideLength == 6) {
								m_Array[30] = !m_Array[30]; m_Array[35] = !m_Array[35]; m_Array[36] = !m_Array[36];
							}
							break;
							

						default:
							break;
					}
					
					foreach (bool value in m_Array)
					{
						//Console.WriteLine(c + ": " + value + "\n");
						if (value == false && c != 0) {
							break;
						}
						else {
							if (m_Panel.SideLength == 3 && c == 9) {
								m_Panel.TileFlipPuzzleSolve( m_From );
							}
							else if (m_Panel.SideLength == 4 && c == 16) {
								m_Panel.TileFlipPuzzleSolve( m_From );
							}
							else if (m_Panel.SideLength == 5 && c == 25) {
								m_Panel.TileFlipPuzzleSolve( m_From );
							}
							else if (m_Panel.SideLength == 6 && c == 36) {
								m_Panel.TileFlipPuzzleSolve( m_From );
							}
						}
						c++;
					}


							m_From.PlaySound( 0x1F4 );
							m_From.SendGump( new TileFlipPuzzleGameGump( m_Panel, m_From, ref m_Array, m_Varient));
							m_Panel.m_LastUse = DateTime.Now;
				
			}
		}

		private Hashtable m_DamageTable = new Hashtable();

		public void DoDamage( Mobile to )
		{
			if ( !to.Alive )
				return;

			if ( m_DamageTable[to] == null )
			{
				//to.Frozen = true;

				DamageTimer timer = new DamageTimer( this, to );
				m_DamageTable[to] = timer;

				timer.Start();
			}
		}

		private class DamageTimer : Timer
		{
			private TileFlipPuzzleControlPanel m_Panel;
			private Mobile m_To;

			public DamageTimer( TileFlipPuzzleControlPanel panel, Mobile to ) : base( TimeSpan.FromSeconds( 5.0 ), TimeSpan.FromSeconds( 5.0 ) )
			{
				m_Panel = panel;
				m_To = to;

				Priority = TimerPriority.TwoFiftyMS;
			}

			protected override void OnTick()
			{
				if ( m_Panel.Deleted || m_To.Deleted || !m_To.Alive || !m_To.InRange( m_Panel, 3 ) )
				{
					End();
					return;
				}

				m_To.FixedParticles( 0x3709, 1, 30, 9934, 0, 7, EffectLayer.Waist );
				m_To.PlaySound( 0x208 );
				m_To.LocalOverheadMessage( MessageType.Regular, 0xC9, true, "* You are engulfed in flames *" );
				m_To.NonlocalOverheadMessage( MessageType.Regular, 0xC9, true, string.Format( "* {0} is engulfed in flames *", m_To.Name ) );

				AOS.Damage( m_To, m_To, 40, 0, 100, 0, 0, 0 );
			}

			private void End()
			{
				m_Panel.m_DamageTable.Remove( m_To );
				//m_To.Frozen = false;

				Stop();
			}
		}

		public void TileFlipPuzzleSolve( Mobile from )
		{
			stopwatch.Stop();
			
			//Console.WriteLine("Flip Puzzle: ", "Time elapsed: {0}", stopwatch.Elapsed);
		
			if ( from != null )
			{
			Effects.PlaySound( Location, Map, 0x207 );
			Effects.PlaySound( Location, Map, 0x1F3 );

			Effects.SendLocationEffect( Location, Map, 0x36B0, 4, 4 );
			Effects.SendLocationEffect( new Point3D( X - 1, Y - 1, Z + 2 ), Map, 0x36B0, 4, 4 );
			Effects.SendLocationEffect( new Point3D( X - 2, Y - 1, Z + 2 ), Map, 0x36B0, 4, 4 );
			Effects.SendLocationEffect( new Point3D( X - 1, Y - 1, Z + 18 ), Map, 0x36B0, 4, 4 );
			Effects.SendLocationEffect( new Point3D( X - 2, Y - 1, Z + 18 ), Map, 0x36B0, 4, 4 );
			Effects.SendLocationEffect( new Point3D( X - 1, Y - 1, Z + 34 ), Map, 0x36B0, 4, 4 );
			Effects.SendLocationEffect( new Point3D( X - 2, Y - 1, Z + 34 ), Map, 0x36B0, 4, 4 );

			from.SendMessage( "You scrounge some gems from the wreckage." );

			for ( int i = 0; i < SideLength; i++ )
			{
				from.AddToBackpack( new ArcaneGem() );
			}
			switch (Utility.Random(12))  //picks one of the following
            {
				case 0:
					from.AddToBackpack( new Diamond( SideLength ) ); break;
				case 1:
					from.AddToBackpack( new RewardScroll() ); break;
				case 2:
					from.AddToBackpack( new RewardScroll() );
					from.AddToBackpack( new Gold( SideLength ) ); break;
				case 3:
					from.AddToBackpack( new RewardScroll() );
					from.AddToBackpack( new PowderOfTranslocation( 25 ) ); break;
				case 4:
					from.AddToBackpack( new Gold( SideLength ) );
					from.AddToBackpack( new Ruby( SideLength ) ); break;
				case 5:
					from.AddToBackpack( new RewardScroll() );
					from.AddToBackpack( new Amber( SideLength ) ); break;
				case 6:
					from.AddToBackpack( new Gold( SideLength ) );
					from.AddToBackpack( new PowderOfTranslocation( 50 ) ); break;
				case 7:
					from.AddToBackpack( new StarSapphire( SideLength ) );
					from.AddToBackpack( new RewardScroll() ); break;
				case 8:
					from.AddToBackpack( new Emerald( SideLength ) );
					from.AddToBackpack( new Gold( SideLength ) ); break;
				case 9:
					from.AddToBackpack( new Citrine( SideLength ) );
					from.AddToBackpack( new RewardScroll() ); break;
				case 10:
					from.AddToBackpack( new Amethyst( SideLength ) );
					from.AddToBackpack( new Gold( SideLength ) ); break;
				case 11:
					from.AddToBackpack( new Ruby( SideLength ) );
					from.AddToBackpack( new BlueDiamond( SideLength ) ); break;
            }
			//from.AddToBackpack( new Diamond( SideLength ) );

			Item ore = new ShadowIronOre( 9 );
			ore.MoveToWorld( new Point3D( X - 1, Y, Z + 2 ), Map );

			ore = new ShadowIronOre( 14 );
			ore.MoveToWorld( new Point3D( X - 2, Y - 1, Z + 2 ), Map );
			
			from.CloseGump( typeof( TileFlipPuzzleGameGump ) );
			from = null;
			
			Delete();
			}
		}

		public TileFlipPuzzleControlPanel( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int) 0 ); // version

			writer.WriteEncodedInt( (int) m_SideLength );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();

			m_SideLength = reader.ReadEncodedInt();
		}
	}
}