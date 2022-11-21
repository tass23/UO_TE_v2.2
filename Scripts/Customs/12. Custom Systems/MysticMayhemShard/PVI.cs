using System;
using System.IO;
using System.Collections;
using Server;
using Server.Commands;
using Server.Network;
using Server.Guilds;
using Server.Multis;
using Server.Items;
using Server.Mobiles;
using Server.Accounting;

namespace Server.Misc
{
	public class PVI
	{
		public static void Initialize()
		{
			CommandSystem.Register( "PVI", AccessLevel.Administrator, new CommandEventHandler( PVI_OnCommand ) );
		}

		private static void PVI_OnCommand( CommandEventArgs args )
		{
			using ( StreamWriter op = new StreamWriter( "PVI.log" ) )
			{
				op.WriteLine( "Vendor           Owner            Item                         Price  Description" );

				foreach( Item item in World.Items.Values )
				{
					Type type = item.GetType();
					string typ = type.ToString();
					string des = null;
					//string name = null; // vi name

			try{

					string[] words = typ.Split('.');
					
					if ( item.RootParent is PlayerVendor )
					{

						PlayerVendor vendor = (PlayerVendor)item.RootParent;

						VendorItem vi = vendor.GetVendorItem( item );

						if ( vi != null && vi.IsForSale )
						{
						if ( vendor.Owner.Name != "Bob" )
						{
							string ownername = " ";
							string name = null; //item.Name.ToString();
						//	string lname = " ";
							string nam = words[words.Length - 1];
							if ( item.Name == null )
							{
								name = nam;
							}
							else
							{
								name = item.Name.ToString();
							}

							if ( vi.Description != null && vi.Description != "" )
								des = vi.Description;
							else
								des = " ";

							if ( name.Length > 25 )
							{
							//	lname = name;
								name = name.Substring( 0, 25 );
							}
							if ( vendor.Name != vendor.Owner.Name )
								ownername = vendor.Owner.Name;

							op.WriteLine( "{0,-16} {1,-16} {2,-25} {3,7} {4}", vendor.Name, ownername, name, vi.Price.ToString(), des );
						}
						}
					}
			} catch{op.WriteLine("error {0}", typ);}
				}

		args.Mobile.SendMessage( "Report done <runuo root>/PVI.log" );
			}
		}
	}
}

