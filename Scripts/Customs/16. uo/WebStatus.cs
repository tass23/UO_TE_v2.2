using System;
using System.IO;
using System.Text;
using Server;
using Server.Network;
using Server.Guilds;
using System.Collections.Generic;
using Server.Accounting;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;

namespace Server.Misc
{
	public class StatusPage : Timer
	{
		public static bool Enabled = true;
		
		public static void Initialize()
		{
			if ( Enabled )
				new StatusPage().Start();
		}
		
		public StatusPage() : base( TimeSpan.FromSeconds( 5.0 ), TimeSpan.FromSeconds( 60.0 ) )
		{
			Priority = TimerPriority.FiveSeconds;
		}

		
		
		private static string Encode( string input )
		{
			
			StringBuilder sb = new StringBuilder( input );

			sb.Replace( "&", "&amp;" );
			sb.Replace( "<", "&lt;" );
			sb.Replace( ">", "&gt;" );
			sb.Replace( "\"", "&quot;" );
			sb.Replace( "'", "&apos;" );

			return sb.ToString();
		}

		protected override void OnTick()
		{
		//	if ( !Directory.Exists( "web" ) )
		//		Directory.CreateDirectory( "web" );

			using ( StreamWriter op = new StreamWriter( "C:/UO_TE/Freeshard/Scripts/Customs/16. uo/output.txt" ) )
			{
				op.WriteLine("ServerName="+ "The Expanse");
				op.WriteLine("ServerVersion=Ver : "+ "7.0.12.0");
				op.WriteLine("UpdateTime="+DateTime.Now.ToString());
                op.WriteLine("Guilds="+Guilds.BaseGuild.List.Count.ToString());
                op.WriteLine("Items="+World.Items.Count.ToString());
                op.WriteLine("Mobiles=" + World.Mobiles.Count.ToString());
                op.WriteLine("Online=" + NetState.Instances.Count.ToString());
                TimeSpan ts = TimeSpan.FromSeconds( (DateTime.Now - Clock.ServerStart).TotalSeconds);
				string ServerupTime = String.Format( "{0:D2}:{1:D2}:{2:D2}:{3:D2}", ts.Days, ts.Hours % 24, ts.Minutes % 60, ts.Seconds % 60 );
				op.WriteLine("UpTime="+(ServerupTime));
				
	
                 foreach ( NetState state in NetState.Instances )
				{
					Mobile m = state.Mobile;
					if ( m != null )
					{
						op.Write( "Player=" );
						op.Write( Encode( m.Name ) );
						op.Write( "," );
						op.Write( m.X );
						op.Write( "," );
						op.Write( m.Y );
						op.Write( "," );
						op.Write( m.Map );
						op.Write( "," );
						op.Write( m.AccessLevel ); 
						op.Write( "," );
						op.Write( "Skills  :"+m.Skills.Total+"<br>" );
						op.WriteLine( "" );
					}
					/*
					else
					{
						return;
					}
					*/
				}
			}
		}
	}
}