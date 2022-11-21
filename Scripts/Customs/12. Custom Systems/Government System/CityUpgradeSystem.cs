using System;
using Server.Items;
using Server.Mobiles;
using System.Xml;
using System.IO;
using Server.Regions;
using System.Collections;
using System.Collections.Generic;
using Server.Commands;

namespace Server
{

public class CityUpgradeSystem
{


	public static void Initialize()
	{
		CommandSystem.Register( "UpgradeCitySystem", AccessLevel.Administrator, new CommandEventHandler( CityUpgrade_OnCommand ) );
	}
	
	public static string[] Versions = new string[] //Add New version numbers to this array
	{
		"2.21",
		"2.3"
	};
	
	[Usage( "UpgradeCitySystem" )]
	[Description( "Upgrades City to newest version" )]  
	private static void CityUpgrade_OnCommand( CommandEventArgs e )
	{
		
		Mobile from = e.Mobile;
		double Fversion = Convert.ToDouble( PlayerGovernmentSystem.FileVersion );
		
		if ( PlayerGovernmentSystem.FileVersion == PlayerGovernmentSystem.SystemVersion )
		{
			from.SendMessage( "The system is current.  No upgrade is necessary." );
		}
		else
		{  
			for ( int q = 0; q < Versions.Length; q++ )
			{
				double current = Convert.ToDouble( Versions[q] );
				
				if ( current <= Fversion )
					continue;
				else
				{
					switch ( q )
					{
						case 0: // 2.21
							{
								ArrayList list = new ArrayList( World.Items.Values );
								List<BaseAddon> addons = new List<BaseAddon>();
								for ( int i = 0; i < list.Count; i++ )
								{
									if ( list[i] is BaseAddon )
									{
										BaseAddon addn = (BaseAddon)list[i];
										Point3D p = new Point3D( addn.Location );
										Map m = addn.Map;
										if ( Region.Find( p, m ) is PlayerCityRegion )
											addons.Add( addn );
									}
								}
								
								
								for ( int i = 0; i < list.Count; i++ )
								{
									if ( list[i] is CityManagementStone )
									{
										CityManagementStone stone = (CityManagementStone)list[i];
										stone.AddOns = new ArrayList();
										Region r = stone.PCRegion;
										
										if ( addons.Count > 0 )
										{
											for ( int j = 0; j < addons.Count; j++ )
											{
												Point3D p = new Point3D( addons[j].Location );
												Map m = addons[j].Map;
												Type type = addons[j].GetType();
												
												if ( Region.Find( p, m ) == r && !Server.CityTypes.IsCityType( type ) )
													stone.AddOns.Add( addons[j] );
												
												
											}
										}
										stone.VerifyAddons();
										stone.CheckVendors( false );
									}
								}
								break;
								
							}
							
						case 1: //Version 2.3
							{
								ArrayList list = new ArrayList( World.Items.Values );
								
								for ( int i = 0; i < list.Count; i++ )
								{
									if ( list[i] is CityManagementStone )
									{
										CityManagementStone stone = (CityManagementStone)list[i];
										ArrayList vends = new ArrayList( stone.Vendors );
										ArrayList todelete = new ArrayList();
										
										for ( int j = 0; j < vends.Count; j++ )
										{
											Mobile vendor = (Mobile)vends[j];
											if ( vendor == null )
											{
												todelete.Add( vendor );
												continue;
											}
											else
											{
												Serial serial = vendor.Serial;
												Mobile areyouthere = World.FindMobile( serial );
												if ( areyouthere == null )
												{
													todelete.Add( vendor );
													continue;
												}
											}
											
										}
											
										if ( todelete == null )
											continue;
										else
										{
											int count = todelete.Count;
											for ( int x = 0; x < todelete.Count; x++ )
											{
												stone.Vendors.Remove( todelete[x] );
											}
											Console.WriteLine( "{0} Vendors Deleted", count );
										}
									}
								}
								break;
							}
						}
					
				}
				
			}
			World.Save();
			
			using (StreamWriter sw = new StreamWriter( Path.Combine( Core.BaseDirectory, "Data/GovernmentVersion.xml" ) ) )
			{
				XmlTextWriter xml = new XmlTextWriter( sw );
				xml.Formatting = Formatting.Indented;
				xml.IndentChar = '\t';
				xml.Indentation = 1;
				xml.WriteStartDocument( true );
				xml.WriteStartElement( "Version" );
				xml.WriteString( PlayerGovernmentSystem.SystemVersion );
				xml.WriteEndElement();
				xml.Close();
				
			}
			
			from.SendMessage( "City System successfully upgraded to {0}", PlayerGovernmentSystem.SystemVersion );
			Console.WriteLine( "Government system successfully upgraded to version {0}", PlayerGovernmentSystem.SystemVersion );
		}
	}
}
}
