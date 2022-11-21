//define jailDebug
using System;
using Server.Mobiles;
using System.IO;
using System.Collections;
using System.Collections.Generic; 
using Server; 
using Server.Misc; 
using Server.Items; 
using Server.Gumps; 
using Server.Network;
using Server.Accounting;
using Server.Targeting;
using Server.ContextMenus;
using Server.Scripts.Commands;
using Server.Regions;
/* Jail script by Cat 9-8-2003, RunUO-Beta-36
 * started on b30.
 * Thanks to Kennie-Insane, I reused his code for handling turning the sentence 
 * length into a timeSpan. 
 * 
 * added fast movement warnig/jailing
 * added cage command
 * add holding cell region (for the cage)
 * release now verifies the existance of all mobiles on the jailing and removes 
 * any releases for mobiles that do not exisit, this will allow jailings 
 * to remove themselves when there are no more characters on the jailing
 * 
 * 
 * ENABLING PLAYER CONTEXT MENUS
 * the script will work without enabling the context menus.  This is optional.
 * add using Server.Scripts.Commands; to your playermobile file;
 * in your playermobile add "JailSystem.getcontextmenus(from,this,list);" right after 
 * base.GetContextMenuEntries( from, list );
 * should look something like this
   public override void GetContextMenuEntries( Mobile from, ArrayList list )
		{
			base.GetContextMenuEntries( from, list );
			JailSystem.getcontextmenus(from,this,list);	
*/
namespace Server.Commands
{
	public class JailSystem
	{	
		public static bool warnspeedy=false;
		const string scriptVersion="36s8";
		public static bool timersRunning=false;
		public static string statusCommand="jailtime";
		public static string timeCommand="what time is it";
		public static string ooclistCommand="ooclist";
		public static bool SingleFacetOnly=true;
		public static string JSName="JailSystem";
		public static bool useSmokingFootGear=true;
		public static bool useLanguageFilter=true;
		public static string foulJailorName="Language Auto Jailor";
		public static bool allowStaffBadWords=true;
		public static string oocJailorName="OOC Jailor";
		public static bool blockOOCSpeech=true;
		public static bool useOOCFilter=true;
		public static bool AllowStaffOOC=true;
		public static int oocwarns=2;	

		public static ArrayList defaultRelease=new ArrayList();//done
		public static ArrayList badWords=new ArrayList();//done text entry 13
		public static ArrayList FoulMouthJailTimes=new ArrayList();//done
		public static ArrayList oocWords=new ArrayList();//done
		public static ArrayList oocParts=new ArrayList();//done
		public static ArrayList oocJailTimes=new ArrayList();//done
		public static ArrayList cells=new ArrayList();//done

		public static Map jailMap;
		public static Map	defaultReleaseFacet;

		private static Hashtable m_jailings;
		private static Hashtable m_warnings;
		private static Hashtable m_fwalkWarnings;
		private static string jailDirectory="Saves\\Jailings";
		private static string idxPath = Path.Combine( jailDirectory, "Jailings.idx" );
		private static string binPath = Path.Combine( jailDirectory, "Jailings.bin" );
		static int m_NextID=0;
	
		public static void defaultSettings()
		{
			statusCommand="jailtime";
			timeCommand="what time is it";
			ooclistCommand="ooclist";
			SingleFacetOnly=true;
			JSName="JailSystem";
			useSmokingFootGear=true;
			jailMap=Map.Trammel;
			defaultReleaseFacet=Map.Trammel;
			defaultRelease.Clear();
			defaultRelease.Add(new Point3D(2708,693,0)); 
			defaultRelease.Add(new Point3D(4476,1281,0));
			defaultRelease.Add(new Point3D(1344,1994,0));
			defaultRelease.Add(new Point3D(1507,3769,0));
			defaultRelease.Add(new Point3D(780,754,0));
			defaultRelease.Add(new Point3D(1833,2942,-22));
			defaultRelease.Add(new Point3D(651,2066,0));
			defaultRelease.Add(new Point3D(3556,2150,26));
			useLanguageFilter=false;
			foulJailorName="Language Auto Jailor";
			allowStaffBadWords=true;
			badWords.Clear();
			badWords.Add("fuck");
			badWords.Add("cunt");
			FoulMouthJailTimes.Clear();
			FoulMouthJailTimes.Add(new TimeSpan(0,0,30,0));
			FoulMouthJailTimes.Add(new TimeSpan(0,2,0,0));
			FoulMouthJailTimes.Add(new TimeSpan(0,4,0,0));
			FoulMouthJailTimes.Add(new TimeSpan(0,8,0,0));
			FoulMouthJailTimes.Add(new TimeSpan(1,0,0,0));
			//ooc section
			oocJailorName="OOC Jailor";
			blockOOCSpeech=false;
			useOOCFilter=false;
			oocWords.Add("lol");
			oocWords.Add("ty");
			oocWords.Add("yw");
			oocWords.Add("rofl");
			oocWords.Add("roflmao");
			oocWords.Add("lmao");
			oocWords.Add("np");
			oocWords.Add("newb");
			oocWords.Add("brb");
			oocWords.Add("afk");
			oocParts.Add("computer");
			oocParts.Add("phone");
			AllowStaffOOC=true;
			oocJailTimes.Clear();
			oocJailTimes.Add(new TimeSpan(0,0,10,0));
			oocJailTimes.Add(new TimeSpan(0,0,20,0));
			oocJailTimes.Add(new TimeSpan(0,0,30,0));
			oocJailTimes.Add(new TimeSpan(0,0,40,0));
			oocJailTimes.Add(new TimeSpan(0,1,0,0));
			oocwarns=2;
			cells.Clear();
			cells.Add(new Point3D(5276,1164,0));
			cells.Add(new Point3D(5286,1164,0));
			cells.Add(new Point3D(5296,1164,0));
			cells.Add(new Point3D(5306,1164,0));
			cells.Add(new Point3D(5276,1174,0));
			cells.Add(new Point3D(5286,1174,0));
			cells.Add(new Point3D(5296,1174,0));
			cells.Add(new Point3D(5306,1174,0));
			cells.Add(new Point3D(5283,1184,0));
		}
		public static Hashtable fWalkWarns
		{
			get
			{
				if(m_fwalkWarnings==null)
					m_fwalkWarnings=new Hashtable();
				return m_fwalkWarnings;
			}
		}
		public static Hashtable warns
		{
			get
			{
				if(m_warnings==null)
					m_warnings=new Hashtable();
				return m_warnings;
			}
		}
		public static Hashtable list
		{
			get
			{
				return m_jailings;
			}
		}
		public static JailSystem fromMobile(Mobile m)
		{
			return fromAccount((Account)m.Account);
		}
		public static JailSystem fromAccount(Account a)
		{
			string un=a.Username;
			foreach(JailSystem js in list.Values)
			{
				if(js.Name==un)
					return js;
			}
			return null;
		}
		public static JailSystem lockup(Mobile m)
		{
			try
			{
				JailSystem js=JailSystem.fromMobile(m);
				if(js==null)
					js=new JailSystem(m);
#if (jailDebug)
				System.Console.WriteLine ("JailDebug-from lockup");
#endif
				js.lockupMobile(m);	
				return js;
			}
			catch
			{
				System.Console.WriteLine("{0}: Lockup call failed on-{1}",JailSystem.JSName,m.Name);
				return null;
			}
		}
		public static void Jail(Mobile badBoy, bool foul, string reason, bool releasetoJailing, string jailedBy, AccessLevel l)
		{
#if (jailDebug)
			System.Console.WriteLine ("JailDebug-jail with bool foul");
#endif
			Jail(badBoy, getJailTerm(badBoy, foul),reason, releasetoJailing, jailedBy,l);
		}
		public static void Jail(Mobile badBoy, TimeSpan ts, string reason, bool releasetoJailing, string jailedBy, AccessLevel l)
		{
			Jail(badBoy, ts, reason, releasetoJailing, jailedBy,l,true);
		}
		public static void Jail(Mobile badBoy, TimeSpan ts, string reason, bool releasetoJailing, string jailedBy, AccessLevel l,bool useBoot)
		{
#if (jailDebug)
			System.Console.WriteLine ("JailDebug-jail with ts");
#endif
			DateTime dt=DateTime.Now.Add(ts);
			Jail(badBoy, dt, reason, releasetoJailing, jailedBy,l,useBoot);
		}
		public static void Jail(Mobile badBoy, int days, int hours, int minutes, string reason, bool releasetoJailing, string jailedBy, AccessLevel l)
		{
#if (jailDebug)
			System.Console.WriteLine ("JailDebug-jail days, int, hours, minures");
#endif
			DateTime dt=DateTime.Now.AddDays(days).AddHours(hours).AddMinutes(minutes);
			Jail(badBoy, dt, reason, releasetoJailing, jailedBy, l);
		}
		public static void Jail(Mobile badBoy, DateTime dt, string reason, bool releasetoJailing, string jailedBy, AccessLevel l)
		{
			Jail(badBoy, dt, reason, releasetoJailing, jailedBy, l,true);
		}
		public static void Jail(Mobile badBoy, DateTime dt, string reason, bool releasetoJailing, string jailedBy, AccessLevel l, bool useBoot)
		{
#if (jailDebug)
			System.Console.WriteLine ("JailDebug-jail with dt");
#endif
			JailSystem js=JailSystem.fromMobile(badBoy);
			if (js==null)
			{
				js=new JailSystem(badBoy, l);
			}
			else
			{
				if(js.jailed)
				{
#if (jailDebug)
					System.Console.WriteLine ("JailDebug-jail already jailed lockup");
#endif
					js.lockupMobile(badBoy,useBoot);
					return;
				}
			}
			js.fillJailReport(badBoy, dt, reason, releasetoJailing, jailedBy);
#if (jailDebug)
			System.Console.WriteLine ("JailDebug-jail lockup");
#endif
			js.lockupMobile(badBoy,useBoot);
		}

		public static void Jail(Mobile badBoy, bool foul, string reason, bool releasetoJailing, string jailedBy)
		{
#if (jailDebug)
			System.Console.WriteLine ("JailDebug-jail with bool foul");
#endif
			Jail(badBoy, getJailTerm(badBoy, foul),reason, releasetoJailing, jailedBy, AccessLevel.Counselor);
		}
		public static void Jail(Mobile badBoy, TimeSpan ts, string reason, bool releasetoJailing, string jailedBy)
		{
#if (jailDebug)
			System.Console.WriteLine ("JailDebug-jail with ts");
#endif
			DateTime dt=DateTime.Now.Add(ts);
			Jail(badBoy, dt, reason, releasetoJailing, jailedBy, AccessLevel.Counselor);
		}
		public static void Jail(Mobile badBoy, int days, int hours, int minutes, string reason, bool releasetoJailing, string jailedBy)
		{
#if (jailDebug)
			System.Console.WriteLine ("JailDebug-jail days, int, hours, minures");
#endif
			DateTime dt=DateTime.Now.AddDays(days).AddHours(hours).AddMinutes(minutes);
			Jail(badBoy, dt, reason, releasetoJailing, jailedBy, AccessLevel.Counselor);
		}
		public static void Jail(Mobile badBoy, DateTime dt, string reason, bool releasetoJailing, string jailedBy)
		{
#if (jailDebug)
			System.Console.WriteLine ("JailDebug-jail with dt");
#endif
			Jail(badBoy, dt, reason, releasetoJailing, jailedBy, AccessLevel.Counselor);
		}
		public static void Configure()
		{
			m_jailings=new Hashtable();
			EventSink.WorldLoad+= new WorldLoadEventHandler(onLoad);
			EventSink.WorldSave+= new WorldSaveEventHandler(onSave);
			//EventSink.FastWalk += new FastWalkEventHandler( OnFastWalk );
		}
		public static bool MovementThrottle_Callback( NetState ns )
		{
			bool hotSteppen=PlayerMobile.MovementThrottle_Callback(ns);
			if(!hotSteppen)
			{
				//Console.WriteLine( "Client: {0}: Fast movement detected (name={1})", ns, ns.Mobile.Name );
				if (warnspeedy) fWalkWarn(ns.Mobile);
			}
			return hotSteppen;
		}
		public static void OnFastWalk( FastWalkEventArgs e )
		{
			e.Blocked = true;//disallow this fastwalk
			//Console.WriteLine( "Client: {0}: Fast movement detected 2(name={1}) in jail system", e.NetState, e.NetState.Mobile.Name );

			if (warnspeedy) fWalkWarn(e.NetState.Mobile);
		}
		public static void fWalkWarn(Mobile m)
		{
			if(!fWalkWarns.Contains(((Account)m.Account).Username))
			{
				fWalkWarns.Add(((Account)m.Account).Username, new Hashtable());
			}
			Hashtable w=(Hashtable)fWalkWarns[((Account)m.Account).Username];
			if (w==null)
			{
#if (jailDebug)
				System.Console.WriteLine ("JailDebug-jail called from fwalkwarn");
#endif
				JailSystem.Jail(m, false, "Fastwalk Detected, warning system was unable to issue a warning and jailed you.", true, "Fastwalk Jailor", AccessLevel.GameMaster);
			}
			DateTime k=DateTime.Now;
			int i=0;
			while (w.Contains(k))
			{
				k=k.Subtract(new TimeSpan(0,0,1));
				if (i>10) continue;
				i++;
			}
			if (i<=10)
			{
				string s="Fastwalk detection";
				w.Add(k, s);
				new warnRemover(w, k);
			}
			if (w.Count>5)
			{
#if (jailDebug)
				System.Console.WriteLine ("JailDebug-jail called from fastwalk warnings exceeded");
#endif
				JailSystem.Jail(m, false, "Fastwalk detection after repeated warnings.", true, oocJailorName);
				fWalkWarns.Remove(((Account)m.Account).Username);
			}
			else
			{
				m.SendMessage("You have been detected using fastwalk.  If you are using a fastwalk/speed hack, stop now or go to jail.");
			}
		}
		public static void onLoad()
		{
			System.Console.WriteLine("Loading Jailings");
			FileStream idxFileStream;
			FileStream binFileStream;
			//BinaryReader idxReader;
			BinaryFileReader idxReader;
			BinaryFileReader binReader;
			//GenericReader idxReader;
			long tPos;
			int tID;
			int tLength;
			JailSystem tJail;
			int JailCount=0;
			int temp=0;
			if ((File.Exists(idxPath))&&(File.Exists(binPath)))
			{
#if jailDebug
				System.Console.WriteLine ("{0} Debug:{1}-{2}",JailSystem.JSName,"Loading-","Files exisit"); 
#endif
				idxFileStream=new FileStream(idxPath, (FileMode)3,(FileAccess) 1, (FileShare)1);
				binFileStream= new FileStream(binPath,(FileMode)3,(FileAccess) 1, (FileShare)1);
				try
				{
					idxReader = new BinaryFileReader(new BinaryReader(idxFileStream));
					binReader = new BinaryFileReader(new BinaryReader(binFileStream));
					JailCount = idxReader.ReadInt();
#if jailDebug
					System.Console.WriteLine ("{0} Debug:{1}-{2}",JailSystem.JSName,"Loading-","streams ok"); 
#endif
					if (JailCount > 0)
					{
#if jailDebug
						System.Console.WriteLine ("{0} Debug:{1}-{2} {3}",JailSystem.JSName,"Loading-","jailings reported",JailCount.ToString()); 
#endif
						for(int i=0; i<JailCount; i++)
						{
							temp=idxReader.ReadInt();//catch the version number which we wont use
							tID=idxReader.ReadInt();
							tPos=idxReader.ReadLong();
							tLength=idxReader.ReadInt();
							tJail=new JailSystem(tID);
							binReader.Seek(tPos,0);
							try
							{
								tJail.Deserialize((GenericReader)binReader);
								if (binReader.Position!=((long)tPos + tLength))
									throw new Exception(String.Format("***** Bad serialize on {0} *****", tID));
							}
							catch
							{}
						}
#if jailDebug
						System.Console.WriteLine ("{0} Debug:{1}-{2}",JailSystem.JSName,"Loading-","Jailings loaded"); 
#endif
					}
					else
					{
#if jailDebug
						System.Console.WriteLine ("{0} Debug:{1}-{2}",JailSystem.JSName,"Loading-","no jailings reported"); 
#endif
					}
					loadingameeditsettings((GenericReader)binReader);

				}
				finally
				{
					if (idxFileStream != null)
						idxFileStream.Close();
					if (binFileStream != null)
						binFileStream.Close();
				}
			}
			else
			{
				JailSystem.defaultSettings();
				System.Console.WriteLine ("{0}: No prior Jailsystem save, using default settings",JailSystem.JSName); 
			}
			System.Console.WriteLine("{0} Jailings Loaded:{1}", JailCount, list.Count);
		}
		public static void loadingameeditsettings(GenericReader idxReader)
		{
			int version=0;
			int temp=0;
			try
			{
				try
				{
					version=idxReader.ReadInt();
				}
#if (jailDebug)
				catch(Exception err)
#else
				catch
#endif
				{
					JailSystem.defaultSettings();
					System.Console.WriteLine ("{0}: settings not found in save file, using default settings.",JailSystem.JSName); 
#if jailDebug
				System.Console.WriteLine ("{0}",err.ToString()); 
#endif
					return;
				}
				switch(version)
				{
					case 0:
						try
						{
#if jailDebug
							System.Console.WriteLine ("{0} Debug:{1}-{2}",JailSystem.JSName,"Loading-","version 0 found"); 
#endif
							JSName=idxReader.ReadString().Trim();
#if jailDebug
							System.Console.WriteLine ("{0} Debug:{1}-{2}",JailSystem.JSName,"Loading-","Name loaded"); 
#endif
							statusCommand=idxReader.ReadString().Trim();
#if jailDebug
							System.Console.WriteLine ("{0} Debug:{1}-{2}",JailSystem.JSName,"Loading-","status loaded"); 
#endif
							timeCommand=idxReader.ReadString().Trim();
#if jailDebug
							System.Console.WriteLine ("{0} Debug:{1}-{2}",JailSystem.JSName,"Loading-","time loaded"); 
#endif
							ooclistCommand=idxReader.ReadString().Trim();
#if jailDebug
							System.Console.WriteLine ("{0} Debug:{1}-{2}",JailSystem.JSName,"Loading-","ooc list loaded"); 
#endif
							foulJailorName=idxReader.ReadString().Trim();
#if jailDebug
							System.Console.WriteLine ("{0} Debug:{1}-{2}",JailSystem.JSName,"Loading-","foul Name loaded"); 
#endif
							oocJailorName=idxReader.ReadString().Trim();
#if jailDebug
							System.Console.WriteLine ("{0} Debug:{1}-{2}",JailSystem.JSName,"Loading-","ooc Name loaded"); 
#endif
							oocwarns=idxReader.ReadInt();
							jailMap=idxReader.ReadMap();
							defaultReleaseFacet=idxReader.ReadMap();
							SingleFacetOnly=idxReader.ReadBool();
							useSmokingFootGear=idxReader.ReadBool();
							useLanguageFilter=idxReader.ReadBool();
							allowStaffBadWords=idxReader.ReadBool();
							blockOOCSpeech=idxReader.ReadBool();
							useOOCFilter=idxReader.ReadBool();
							AllowStaffOOC=idxReader.ReadBool();
							//load default releases
#if jailDebug
							System.Console.WriteLine ("{0} Debug:{1}-{2}",JailSystem.JSName,"Loading-","bools finished"); 
#endif
							temp=idxReader.ReadInt();
							for(int i=0; i<temp; i++)
							{
								defaultRelease.Add(idxReader.ReadPoint3D());
							}
#if jailDebug
							System.Console.WriteLine ("{0} Debug:{1}-{2}",JailSystem.JSName,"Loading-","default releases finished"); 
#endif
							//load cells
							temp=idxReader.ReadInt();
							for(int i=0; i<temp; i++)
							{
								cells.Add(idxReader.ReadPoint3D());
							}
#if jailDebug
							System.Console.WriteLine ("{0} Debug:{1}-{2}",JailSystem.JSName,"Loading-","cells finished"); 
#endif
							//load bad words
							temp=idxReader.ReadInt();
							for(int i=0; i<temp; i++)
							{
								badWords.Add(idxReader.ReadString().Trim());
							}
#if jailDebug
							System.Console.WriteLine ("{0} Debug:{1}-{2}",JailSystem.JSName,"Loading-","bad words finished"); 
#endif
							//load ooc words
							temp=idxReader.ReadInt();
							for(int i=0; i<temp; i++)
							{
								oocWords.Add(idxReader.ReadString().Trim());
							}
#if jailDebug
							System.Console.WriteLine ("{0} Debug:{1}-{2}",JailSystem.JSName,"Loading-","ooc words finished"); 
#endif
							//load ooc part words
							temp=idxReader.ReadInt();
							for(int i=0; i<temp; i++)
							{
								oocParts.Add(idxReader.ReadString().Trim());
							}
#if jailDebug
							System.Console.WriteLine ("{0} Debug:{1}-{2}",JailSystem.JSName,"Loading-","ooc partwords finished"); 
#endif

							//load oocjail times
							temp=idxReader.ReadInt();
							for(int i=0; i<temp; i++)
							{
								oocJailTimes.Add(idxReader.ReadTimeSpan());
							}
#if jailDebug
							System.Console.WriteLine ("{0} Debug:{1}-{2}",JailSystem.JSName,"Loading-","ooc jail terms finished"); 
#endif
							//load foul mouth jail times
							temp=idxReader.ReadInt();
							for(int i=0; i<temp; i++)
							{
								FoulMouthJailTimes.Add(idxReader.ReadTimeSpan());
							}
#if jailDebug
							System.Console.WriteLine ("{0} Debug:{1}-{2}",JailSystem.JSName,"Loading-","foul language jail terms finsihed"); 
#endif
						}
#if (jailDebug)
				catch(Exception err)
#else
						catch
#endif
						{
							JailSystem.defaultSettings();
							System.Console.WriteLine ("{0}: settings not found in save file, using default settings.",JailSystem.JSName); 
							return;
						}
						break;
					case -1:
						JailSystem.defaultSettings();
						break;
					default:
						System.Console.WriteLine ("{0} warning:{1}-{2}",JailSystem.JSName,"Loading-","Unknown version"); 
						break;
				}
			}
#if (jailDebug)
				catch(Exception err)
#else
			catch
#endif
			{
				JailSystem.defaultSettings();
				System.Console.WriteLine ("{0}: settings not found in save file, using default settings:",JailSystem.JSName); 
#if jailDebug
				System.Console.WriteLine ("{0}",err.ToString()); 
#endif
				return;
			}		
		}
		public static void saveingameeditsettings(GenericWriter idxWriter)
		{
			idxWriter.Write((int)0);//version#
			idxWriter.Write((string)JailSystem.JSName.Trim());
			idxWriter.Write((string)JailSystem.statusCommand.Trim());
			idxWriter.Write((string)JailSystem.timeCommand.Trim());
			idxWriter.Write((string)JailSystem.ooclistCommand.Trim());
			idxWriter.Write((string)JailSystem.foulJailorName.Trim());
			idxWriter.Write((string)JailSystem.oocJailorName.Trim());
			idxWriter.Write((int)JailSystem.oocwarns);
			idxWriter.Write((Map)JailSystem.jailMap);
			idxWriter.Write((Map)JailSystem.defaultReleaseFacet);
			idxWriter.Write((bool)JailSystem.SingleFacetOnly);
			idxWriter.Write((bool)JailSystem.useSmokingFootGear);
			idxWriter.Write((bool)JailSystem.useLanguageFilter);
			idxWriter.Write((bool)JailSystem.allowStaffBadWords);
			idxWriter.Write((bool)JailSystem.blockOOCSpeech);
			idxWriter.Write((bool)JailSystem.useOOCFilter);
			idxWriter.Write((bool)JailSystem.AllowStaffOOC);
			idxWriter.Write((int)JailSystem.defaultRelease.Count);
			foreach(Point3D p in defaultRelease)
			{
				idxWriter.Write((Point3D)p);
			}
			idxWriter.Write((int)cells.Count);
			foreach(Point3D p in cells)
			{
				idxWriter.Write((Point3D)p);
			}

			idxWriter.Write((int)badWords.Count);
			foreach(string s in badWords)
			{
				idxWriter.Write((string)s.Trim());
			}
				
			idxWriter.Write((int)oocWords.Count);
			foreach(string s in oocWords)
			{
				idxWriter.Write((string)s.Trim());
			}
				
			idxWriter.Write((int)oocParts.Count);
			foreach(string s in oocParts)
			{
				idxWriter.Write((string)s.Trim());
			}
			idxWriter.Write((int)oocJailTimes.Count);
			foreach(TimeSpan t in oocJailTimes)
			{
				idxWriter.Write((TimeSpan)t);
			}
			idxWriter.Write((int)FoulMouthJailTimes.Count);
			foreach(TimeSpan t in FoulMouthJailTimes)
			{
				idxWriter.Write((TimeSpan)t);
			}
		}
		public static void onSave(WorldSaveEventArgs e)
		{
			System.Console.WriteLine("Saving Jailings");
			if (!Directory.Exists(jailDirectory))
				Directory.CreateDirectory(jailDirectory);
			GenericWriter idxWriter;
			GenericWriter binWriter;
			long tPos;
			if (StandardSaveStrategy.SaveType == 0) 
			{
				idxWriter = new BinaryFileWriter(idxPath, false);
				binWriter = new BinaryFileWriter(binPath, true);
			}
			else 
			{
				idxWriter = new AsyncWriter(idxPath, false);
				binWriter = new AsyncWriter(binPath, true);
			}
			idxWriter.Write((int)m_jailings.Count);
			try
			{
				foreach(JailSystem tJail in m_jailings.Values)
				{
					tPos=binWriter.Position;
					idxWriter.Write((int)0);
					idxWriter.Write((int)tJail.ID);
					idxWriter.Write((long)tPos);
					try
					{
						tJail.Serialize((GenericWriter)binWriter);
					}
					catch(Exception err)
					{
						System.Console.WriteLine("{0}, {1} serialize", err.Message, err.TargetSite);
					}
					idxWriter.Write((int)(binWriter.Position-tPos));
				}
				saveingameeditsettings((GenericWriter)binWriter);

			}
			catch(Exception er)
			{
				System.Console.WriteLine("{0}, {1}", er.Message, er.TargetSite);
			}
			finally
			{			
			}
			idxWriter.Close();
			binWriter.Close();
			System.Console.WriteLine("Jailings Saved");
		}
		private bool returnToPoint=true;
		private Hashtable	releasePoints=new Hashtable();
		public JailSystem():base()
		{
			buildJail();
		}
		public JailSystem(Mobile m):this(m, AccessLevel.Counselor)
		{
		}
		public JailSystem(Mobile m, AccessLevel l):base()
		{
			buildJail();
			this.Name=((Account)m.Account).Username;
			this.m_jailorAC=l;
		}
		public void buildJail()
		{
			this.m_id = JailSystem.m_NextID;
			m_releaseTime=DateTime.Now.AddDays(1);
			JailSystem.m_NextID+=1;
			m_jailings.Add(this.m_id, this);
			jailor=JailSystem.JSName;
			this.m_jailorAC=AccessLevel.Counselor;
			reason="Please wait while the gm fills in a jailing report";
		}
		public void addNote(string from, string text)
		{
			this.Prisoner.Comments.Add(new AccountComment(JailSystem.JSName + "-note", text + " by: " + from));
		}
		public void fillJailReport(Mobile badBoy, int days, int hours, int minutes, string why, bool mreturn, string Jailor)
		{
			DateTime dt_unJail=DateTime.Now.Add(new TimeSpan(days,hours,minutes,0));
			fillJailReport(badBoy, dt_unJail, why, mreturn, Jailor);
		}
		public void fillJailReport(Mobile badBoy, DateTime dt_unJail, string why, bool mreturn, string Jailor)
		{
			Name=((Account)badBoy.Account).Username;
			m_releaseTime=dt_unJail;
			this.reason=why;
			this.jailor=Jailor;
			((Account)badBoy.Account).Comments.Add(new AccountComment(JailSystem.JSName + "-jailed", "Jailed for \"" + why + "\" By:" + Jailor + " On:" + DateTime.Now + " Until:" + dt_unJail.ToString()));
			this.returnToPoint=mreturn;
			this.StartTimer();
		}
		public JailSystem( Serial serial )
		{
			buildJail();
		}
		public virtual void Serialize( GenericWriter writer )
		{
			writer.Write((int)2);
			//new version here
			//version 2
			writer.Write((int)m_jailorAC);
			//version 1
			writer.Write((string)freedBy);
			//version 0 here
			writer.Write((string)m_name);
			writer.Write((DateTime)m_releaseTime);
			writer.Write((int)releasePoints.Count);
			foreach (releaseLoc rl in releasePoints.Values)
			{
				writer.Write((Map)rl.map);
				writer.Write((Point3D)rl.location);
				writer.Write((int)rl.mobile);
				writer.Write((bool)rl.returnToPoint);
			}		
			writer.Write((string)jailor);
			writer.Write((string)reason);
		}
		public virtual void Deserialize( GenericReader reader )
		{
			int imax=0;
			int version=reader.ReadInt();		
			switch(version)
			{
				case 2:
					m_jailorAC=(AccessLevel)reader.ReadInt();
					goto case 1;
				case 1:
					freedBy=reader.ReadString().Trim();
					goto case 0;
				case 0:
					m_name=reader.ReadString().Trim();
					m_releaseTime=reader.ReadDateTime();
					imax=reader.ReadInt();
					for(int i=0; i<imax; i++)
					{
						releaseLoc rl=new releaseLoc();
						rl.map=reader.ReadMap();
						rl.location=reader.ReadPoint3D();
						rl.mobile=reader.ReadInt();
						rl.returnToPoint=reader.ReadBool();
						releasePoints.Add(rl.mobile,rl);
					}
					jailor=reader.ReadString().Trim();
					reason=reader.ReadString().Trim();
					break;;
				default:
					break;
			}
			System.Console.WriteLine("Loaded Jail object:{0} releases:{1}", m_name, imax);
		}
		private DateTime m_releaseTime;
		public DateTime ReleaseDate
		{
			get{return m_releaseTime;}
		}
		private jailing	autoReleasor;
		private int m_id;
		private AccessLevel m_jailorAC=AccessLevel.Counselor;
		public bool jailed
		{
			get 
			{
				return (ReleaseDate>DateTime.Now);
			}
		}
		public int ID
		{
			get{return m_id;}
		}
		private string m_name;
		public string jailor;
		public string reason;
		public string freedBy=JailSystem.JSName;
		public Account Prisoner
		{
			get { return (Account)Accounts.GetAccount(Name); }
		}
		public string Name
		{
			get
			{
				return m_name;
			}
			set
			{
				m_name=value;
			}
		}
		public override string ToString()
		{
			return Name;
		}
		public void TimerRelease()
		{
			if (m_releaseTime<=System.DateTime.Now)
			{
				release();
			}
			else
				Console.WriteLine("JailSystem: A Jail Timer fired but the timer was incorrect so the release was not honored.");
		}
		public void forceRelease(Mobile releasor)
		{
			try
			{
				if (m_jailorAC > releasor.AccessLevel)
				{
					releasor.SendLocalizedMessage(1061637);
					return;
				}
			}
			catch(Exception err)
			{
				System.Console.WriteLine("{0}: access level error, resume release-{1}",JailSystem.JSName,err.ToString());
			}
			freedBy=releasor.Name + " (At:" + DateTime.Now.ToString()+")";
			try
			{
				if (autoReleasor!=null)
					autoReleasor.Stop();
			}
			catch{}
			m_releaseTime=DateTime.Now.Subtract(new TimeSpan(1,0,0,0,0));
			release();	
		}
		public void release(NetState ns)
		{
			try
			{
				if (autoReleasor!=null)
				{
					if (autoReleasor.Running) autoReleasor.Stop();
					autoReleasor=null;
				}
				try
				{
					if (!(ns.Mobile.Region is Regions.Jail)) return;
					ns.Mobile.SendLocalizedMessage(501659);
				}
				catch(Exception err)
				{
					System.Console.WriteLine("{0}: {1} Mobile not released", JailSystem.JSName, err.ToString());
					return;
				}
				releaseLoc rl;
				try
				{
					rl=(releaseLoc)releasePoints[ns.Mobile.Serial.Value];
				}
				catch
				{
					rl=new releaseLoc();
					rl.mobile=ns.Mobile.Serial.Value;
					releasePoints.Add(ns.Mobile.Serial.Value, rl);
				}
				if (rl.release(this.freedBy))
					releasePoints.Remove(ns.Mobile.Serial.Value);
			}
			catch (Exception err)
			{
				System.Console.WriteLine("{0}: {1}", JailSystem.JSName, err.ToString());
			}
			if(releasePoints.Count==0)
			{
				System.Console.WriteLine("Jailing removed for account {0}",this.Name);
				try
				{
					list.Remove(this.ID); 
				}
				catch
				{
				}
			}
		}
		public void ban(Mobile from)
		{
			try
			{
				this.Prisoner.Comments.Add(new AccountComment(JailSystem.JSName + "-jailed", string.Format("{0} banned this account on {1}.",from.Name,DateTime.Now.ToString() )));
				this.Prisoner.Banned=true;
				CommandLogging.WriteLine( from, "{0} {1} {3} account {2}", from.AccessLevel, CommandLogging.Format( from ), this.Prisoner.Username, this.Prisoner.Banned ? "banning" : "unbanning" );
				list.Remove(this.ID); 
			}
			catch
			{
				from.SendMessage("Banning Failed.  If you are trying to remove the jailing release the person, or use 'killjail {0}'", this.ID);
				from.SendMessage("Using killjail on an unbanned account can cause problems for that account.");
			}	
		}
		static public void  killJailing(int tID)
		{
			list.Remove(tID);
		}
		public void release()
		{
			try
			{
				if (!(autoReleasor==null))
				{
					if (autoReleasor.Running) autoReleasor.Stop();
					//autoReleasor=null;
				}
			}
			catch(Exception err)
			{
				System.Console.WriteLine("{0}: auto releasor not found-{1}",JailSystem.JSName,err.ToString());
			}
			try
			{
				verifyMobs();
			}
			catch(Exception err)
			{
				System.Console.WriteLine("{0}: Verify Mobiles failed-{1}",JailSystem.JSName,err.ToString());
			}
			try
			{
				foreach (NetState ns in NetState.Instances)
				{
					if(((Account)ns.Account).Username==m_name)
					{
						release(ns);
					}
				}
			}
			catch(Exception err)
			{
				System.Console.WriteLine("{0}: Release failed-{1} **The most common occurance of this is when an account has been deleted while in jail ***Use the adminjail command to cycle through the jailings and automaticly remove them.",JailSystem.JSName,err.ToString());
			}
			if(releasePoints.Count==0)
			{
				try
				{
					list.Remove(this.ID); 
					System.Console.WriteLine("Jailing removed for account {0}",this.Name);
				}
				catch
				{
				}
			}
		}
		public void verifyMobs()
		{
			ArrayList	temp=new ArrayList();
			foreach(releaseLoc r in this.releasePoints.Values)
			{
				try
				{
					Mobile m=World.FindMobile(r.mobile);
					if (m==null)
					{
						temp.Add(r);
					}
				}
				catch
				{
					temp.Add(r);
				}
			}
			foreach(releaseLoc r in temp )
			{
				this.releasePoints.Remove(r.mobile);
			}

		}
		public void killJail()
		{
			if (this.Prisoner==null)
				m_jailings.Remove(this.m_id);
		}
		public void lockupMobile(Mobile m)
		{
			lockupMobile(m,true);
		}
		public void lockupMobile(Mobile m, bool useFootWear)
		{
			if (!releasePoints.Contains(m.Serial.Value))
				releasePoints.Add(m.Serial.Value, new releaseLoc(m.Location, m.Map, m.Serial.Value, returnToPoint));
			m.SendMessage("While in jail, you can say \"{0}\" at any time to check your jail status", statusCommand);
			m.SendMessage("You can say \"{0}\" at any time to check the time according to the server", JailSystem.timeCommand);
			if (JailSystem.useOOCFilter)
				m.SendMessage("You can say \"{0}\" at any time to see the list of words marked as being out of character", JailSystem.ooclistCommand );
			if (m.Region is Regions.Jail) return;
			//if they are already in jail there is no need to do this
			Point3D cell;
			cell=(Point3D)cells[((new System.Random()).Next(0,cells.Count-1))];
			if ((useSmokingFootGear)&&(useFootWear))new smokingFootGear((Mobile)m);
			m.Location=cell;
			m.Map=JailSystem.jailMap;
			
		}
		public void StartTimer()
		{
			if (autoReleasor!=null)
			{
				if (autoReleasor.Running) autoReleasor.Stop();
				autoReleasor=null;
			}
			if(!jailed)
			{
				release();
				return;
			}
			autoReleasor=new jailing(this);
			autoReleasor.Start();
		}
		public void resetReleaseDateOneDay()
		{
			m_releaseTime=DateTime.Now.AddDays(1);
			StartTimer();
		}
		public void resetReleaseDateNow()
		{
			m_releaseTime=DateTime.Now;
		}
		public void AddDays(int days)
		{
			m_releaseTime=m_releaseTime.AddDays(days);
			StartTimer();
		}
		public void AddMonths(int months)
		{
			m_releaseTime=m_releaseTime.AddMonths(months);
			StartTimer();
		}
		public void AddHours(int hours)
		{
			m_releaseTime=m_releaseTime.AddHours(hours);
			StartTimer();
		}
		public void AddMinutes(int minutes)
		{
			m_releaseTime=m_releaseTime.AddMinutes(minutes);
			StartTimer();
		}
		public void subtractDays(int days)
		{
			removeTime(days,0,0);
		}
		public void subtractHours(int hours)
		{
			removeTime(0,hours,0);
		}
		public void subtractMinutes(int minutes)
		{
			removeTime(0,0,minutes);
		}
		public void removeTime(int days, int hours, int minutes)
		{
			m_releaseTime=m_releaseTime.Subtract(new TimeSpan(days,hours,minutes,0,0));
			StartTimer();
		}
        public static void EventSink_Speech(SpeechEventArgs args)
        {
            try
            {
                if (args.Mobile is PlayerMobile)
                {
                    if ((useLanguageFilter) && (!(args.Mobile.Region is Regions.Jail)))
                        foreach (string s in badWords)
                        {
                            if (args.Speech.ToLower().Trim().IndexOf(s.ToLower()) >= 0)
                            {
                                if ((((Account)args.Mobile.Account).AccessLevel > AccessLevel.Player) && (allowStaffBadWords))
                                    args.Mobile.SendMessage("If you were not staff you’d be in jail now.  Behave yourself");
                                else
                                {
#if (jaildebug)
									System.Console.WriteLine ("JailDebug-jail called from languagefilter");
#endif									
                                    JailSystem.Jail(args.Mobile, true, string.Format("Foul language, \"{0}\" \"{1}\"", s, args.Speech), true, foulJailorName);
                                    return;
                                }
                            }
                        }
                    if ((args.Speech.ToLower().Trim() == timeCommand) || (args.Speech.ToLower().Trim() == timeCommand + "?"))//time query
                        args.Mobile.SendMessage("It is currently {0} by the servers clock", System.DateTime.Now.ToString());
                    else if (args.Speech.ToLower().Trim() == "jailsystem")//credit speech
                    {
                        args.Mobile.SendMessage("This server is running a Jail System.", scriptVersion);
                        args.Blocked = true;
                        return;
                    }
                    else if (args.Speech.ToLower().Trim() == statusCommand)//status command
                    {
                        if (args.Mobile.Region is Regions.Jail)//ignore it if they rant in jail
                        {
                            JailSystem js = JailSystem.fromMobile(args.Mobile);
                            if (js != null)
                            {
                                args.Mobile.SendMessage("You were jailed by: {0}", js.jailor);
                                args.Mobile.SendMessage("You were jailed for: {0}", js.reason);
                                args.Mobile.SendMessage("You are to be released at: {0}", js.ReleaseDate.ToString());
                            }
                            else
                            {
                                args.Mobile.SendMessage("You are missing a jailing object, page a GM.");
                            }
                        }//end status area
                        args.Blocked = true;
                        return;
                    }
                    else if (args.Speech.ToLower().Trim() == ooclistCommand)//status command
                    {
                        string ooclst = "Here is the list of words that can land you in jail:";
                        foreach (string s in oocWords)
                            ooclst += " " + s;
                        foreach (string s in oocParts)
                            ooclst += " " + s;
                        args.Mobile.SendMessage(ooclst);
                        args.Blocked = true;
                        return;
                    }
#if (jaildebug)
					else if (args.Speech.ToLower().Trim()=="jaildata")//credit speech
					{
						args.Mobile.SendMessage("JailObjects:{0}", list.Count);
						args.Blocked=true;
						return;
					}
#endif
					
                    if ((useOOCFilter) && (!(args.Mobile.Region is Regions.Jail)) && (!(args.Mobile.Region.Name.ToLower().IndexOf("ooc") >= 0)))
                    {
                        foreach (string s in oocParts)
                        {
                            if (args.Speech.ToLower().Trim().IndexOf(s.ToLower()) >= 0)
                            {
                                if ((((Account)args.Mobile.Account).AccessLevel == AccessLevel.Player) || (!AllowStaffOOC))
                                {
                                    JailSystem.oocWarn(args.Mobile, s);
                                    args.Blocked = blockOOCSpeech;
                                    return;
                                }
                                else
                                    args.Mobile.SendMessage("Your staff, but please avoid ooc stuff as much as possible-triggered on '{0}'.  say '{1}' to see the list of ooc words", s, ooclistCommand);
                            }
                        }
                        foreach (string s in oocWords)
                        {
                            if (args.Speech.ToLower().Trim() == s.ToLower())
                            {
                                if ((((Account)args.Mobile.Account).AccessLevel == AccessLevel.Player) || (!AllowStaffOOC))
                                {
                                    JailSystem.oocWarn(args.Mobile, s);
                                    args.Blocked = blockOOCSpeech;
                                    return;
                                }
                                else
                                    args.Mobile.SendMessage("Your staff, but please avoid ooc stuff as much as possible-triggered on '{0}'.  say '{1}' to see the list of ooc words", s, ooclistCommand);
                            }
                        }
                    }
                }
            }
            catch (NullReferenceException ne)
            {
                Console.WriteLine("EX : JailSpeech by " + (args.Mobile != null ? args.Mobile.Name : ""));
            }
        }
		public static void OnLoginJail( LoginEventArgs e )
		{
			if(!timersRunning)//start the timers on the first user to login
			{
				timersRunning=true;//so no-one else causes the process to run
				bool loopdone=false;
				while(!loopdone)
				{
					try
					{
						foreach(JailSystem tjs in JailSystem.list.Values)
							tjs.StartTimer();
						loopdone=true;
					}
					catch(Exception err)
					{
						System.Console.WriteLine("Restarting the Jail timer load process:{0}", err.Message);
					}
				}
				System.Console.WriteLine("The Jail timer load process has finished");
			}
			if (e.Mobile==null) return;
			JailSystem js=JailSystem.fromMobile(e.Mobile);//check to see if they have a jail object
			if (js==null) 
			{
#if (jailDebug)
				System.Console.WriteLine ("No jail object");
#endif
				return;//they don’t so we bail
			}
			if (js.jailed)//are they jailed?
			{
#if (jailDebug)
				System.Console.WriteLine ("JailDebug-login lockup");
#endif
				js.lockupMobile(e.Mobile);//yup so lock them up
			}
			else
				js.release(e.Mobile.NetState);//no so we release them
		}
		public static void oocWarn(Mobile m, string s)
		{
			if(!warns.Contains(((Account)m.Account).Username))
			{
				warns.Add(((Account)m.Account).Username, new Hashtable());
			}
			Hashtable w=(Hashtable)warns[((Account)m.Account).Username];
			if (w==null)
			{
#if (jailDebug)
				System.Console.WriteLine ("JailDebug-jail called from oocwarn");
#endif
				JailSystem.Jail(m, false, "Going OOC, warning system was unable to issue a warning and jailed you.", true, "OOC Jailor");
			}
			DateTime k=DateTime.Now;
			int i=0;
			while (w.Contains(k))
			{
				k=k.Subtract(new TimeSpan(0,0,1));
				if (i>10) continue;
				i++;
			}
			if (i<=10)
			{
				w.Add(k, s);
				new warnRemover(w, k);
			}
			if (w.Count>oocwarns)
			{
#if (jailDebug)
				System.Console.WriteLine ("JailDebug-jail called from oocwarn warnings exceeded");
#endif
				JailSystem.Jail(m, false, "Going OOC after repeated warnings.", true, oocJailorName);
				warns.Remove(((Account)m.Account).Username);
			}
			else
			{
				m.SendMessage(k.ToString());
				m.SendMessage("'{0}' is an out of character term.  Going ooc too much can land you in Jail.  For a list of ooc words say '{1}'", s, ooclistCommand);
			}
		}
		public class warnRemover : Timer
		{
			public DateTime	key;
			public Hashtable issuedWarnings;
			public warnRemover(Hashtable w, DateTime k):base(new TimeSpan(1,0,0,0))
			{
				key=k;
				issuedWarnings=w;
				this.Start();
			}
			protected override void OnTick()
			{
				if(issuedWarnings.Contains(key))
				{
					issuedWarnings.Remove(key);
				}
			}
		}
		public static TimeSpan getJailTerm(Mobile m, bool foul)
		{
			int visits=countJailings(m, foul);
			if (foul)
				return getFoulJailTerm(visits);
			else
				return getOOCJailTerm(visits);
		}
		public static TimeSpan getOOCJailTerm(int visits)
		{
			oocJailTimes.Sort();
			if(visits >=oocJailTimes.Count) visits=oocJailTimes.Count-1;
			if(visits < 0) visits=0;
			return (TimeSpan)oocJailTimes[visits];
		}
		public static TimeSpan getFoulJailTerm(int visits)
		{
			FoulMouthJailTimes.Sort();
			if(visits >=FoulMouthJailTimes.Count) visits=FoulMouthJailTimes.Count-1;
			if(visits < 0) visits=0;
			return (TimeSpan)FoulMouthJailTimes[visits];
		}
		public static int countJailings(Mobile m, bool foul)
		{
			return countJailings(((Account)m.Account),foul);
		}
		public static int countJailings(Account a, bool foul)
		{
			int foulCt=0;
			int oocCt=0;
			foreach (AccountComment note in a.Comments)
			{
				if (note.Content.IndexOf(" By:" + oocJailorName) >=0)
					oocCt++;
				if (note.Content.IndexOf(" By:" + foulJailorName) >=0)
					foulCt++;
			}
			if (foul)
				return foulCt;
			else
				return oocCt;
		}
		public static void Initialize()
		{
            EventSink.Login += new LoginEventHandler(OnLoginJail);
            EventSink.Speech += new SpeechEventHandler(EventSink_Speech);
            CommandSystem.Register("unjail", AccessLevel.GameMaster, new CommandEventHandler(unjail_OnCommand));
            CommandSystem.Register("release", AccessLevel.GameMaster, new CommandEventHandler(unjail_OnCommand));
            CommandSystem.Register("jail", AccessLevel.Counselor, new CommandEventHandler(jail_OnCommand));
            CommandSystem.Register("review", AccessLevel.Counselor, new CommandEventHandler(review_OnCommand));
            CommandSystem.Register("warn", AccessLevel.Counselor, new CommandEventHandler(warn_OnCommand));
            CommandSystem.Register("macroJail", AccessLevel.Counselor, new CommandEventHandler(macroCheck_OnCommand));
            CommandSystem.Register("adminJail", AccessLevel.Administrator, new CommandEventHandler(adminJail_OnCommand));
            CommandSystem.Register("killJail", AccessLevel.Administrator, new CommandEventHandler(KillJail_OnCommand));
            CommandSystem.Register("cage", AccessLevel.Administrator, new CommandEventHandler(cage_OnCommand));
			
			//PacketHandler ph = PacketHandlers.GetHandler( 0x02 );
			//ph.ThrottleCallback = new ThrottlePacketCallback( MovementThrottle_Callback );
		}
		public static void getcontextmenus(Mobile from, Mobile player, ArrayList list)
		{
			if(from.AccessLevel>=AccessLevel.Counselor)
				list.Add(new JailEntry(from, player));
			if(from.AccessLevel>=AccessLevel.GameMaster)
				list.Add(new unJailEntry(from, player));
			if(from.AccessLevel>=AccessLevel.Counselor)
				list.Add(new ReviewEntry(from, player));
			if(from.AccessLevel>=AccessLevel.Counselor)
				list.Add(new macroerEntry(from, player));
		}
		[Usage( "AdminJail" )]
		[Description( "Displays the jail sentence gump." )]
		public static void adminJail_OnCommand( CommandEventArgs e )
		{
			if ( e.Mobile is PlayerMobile )
			{
				e.Mobile.SendGump(new JailAdminGump());
			}
		}
		[Usage( "killJail <ID>" )]
		[Description( "Deletes the jailing with the specified ID.  Used only to recover from deadly errors" )]
		public static void KillJail_OnCommand( CommandEventArgs e )
		{
			try
			{
				int tID=Convert.ToInt32(e.ArgString.Trim());
				JailSystem.killJailing(tID);
			}
			catch(Exception err)
			{
				e.Mobile.SendMessage("Kill jailing failed: {0}",err.ToString());
			}
		}
		[Usage( "UnJail" )]
		[Description( "Releases the selected player from jail." )]
		public static void unjail_OnCommand( CommandEventArgs e )
		{
			if ( e.Mobile is PlayerMobile )
			{
				e.Mobile.Target = new JailTarget(true);
				e.Mobile.SendLocalizedMessage( 3000218 );
			}
		}
		[Usage( "Cage" )]
		[Description( "places a cage around the target." )]
		public static void cage_OnCommand( CommandEventArgs e )
		{
			if ( e.Mobile is PlayerMobile )
			{
				e.Mobile.Target = new cageTarget();
				e.Mobile.SendMessage("Who would you like to cage?");
			}
		}
		[Usage( "Macro" )]
		[Description( "Issues a macroing check dialog." )]
		public static void macroCheck_OnCommand( CommandEventArgs e )
		{
			if ( e.Mobile is PlayerMobile )
			{
				e.Mobile.Target = new macroTarget();
				e.Mobile.SendLocalizedMessage( 3000218 );
			}
		}
		[Usage( "Jail" )]
		[Description( "Places the selected player in jail." )]
		public static void jail_OnCommand( CommandEventArgs e )
		{
			if ( e.Mobile is PlayerMobile )
			{
				e.Mobile.Target = new JailTarget(false);
				e.Mobile.SendLocalizedMessage( 3000218 );
			}
		}
		[Usage( "Warn" )]
		[Description( "Issues a warning to the player." )]
		public static void warn_OnCommand( CommandEventArgs e )
		{
			if ( e.Mobile is PlayerMobile )
			{
				e.Mobile.Target = new warnTarget();
				e.Mobile.SendLocalizedMessage( 3000218 );
			}
		}
		[Usage( "Review" )]
		[Description( "Reviews the jailings, GM notes and warnings of the selected player." )]
		public static void review_OnCommand( CommandEventArgs e )
		{
			if ( e.Mobile is PlayerMobile )
			{
				e.Mobile.Target = new warnTarget(false);
				e.Mobile.SendLocalizedMessage( 3000218 );
			}
		}
		public static void macroTest(Mobile from, Mobile badBoy)
		{
			if (badBoy.NetState==null)
			{
				from.SendMessage("They are not online.");
				return;
			}
				badBoy.SendGump(new unattendedMacroGump(from, badBoy));
		}
		public class releaseLoc 
		{
			public Point3D	location;
			public Map		map;
			public int		mobile;
			public bool		returnToPoint;
			public releaseLoc()
			{
				returnToPoint=false;
			}
			public releaseLoc(bool rel2JailPoint)
			{
				returnToPoint=rel2JailPoint;
			}
			public releaseLoc(Point3D loc, Map m, int mob, bool rel2JailPoint)
			{
				location=loc;
				map=m;
				mobile=mob;
				returnToPoint=rel2JailPoint;
			}
			public bool release(string releasor)
			{
				Mobile m=World.FindMobile((Serial)mobile);
				if (m==null)
				{
					System.Console.WriteLine("release location error, Mobile not found.");
					return false;
				}
				if(!returnToPoint)
				{//not returning to the jailing point so rewrites this release point info
					if (JailSystem.SingleFacetOnly)
						map=JailSystem.defaultReleaseFacet;
					else
					{
						if (m.Kills >=5) 
							map=Map.Felucca; 
						else 
							map=Map.Trammel; 
					}
					location=(Point3D)JailSystem.defaultRelease[(new System.Random()).Next(0,JailSystem.defaultRelease.Count-1)];
				}
				try
				{
					m.Location=location;
					m.Map=map;
				}
				catch{}
				if (m.Region is Regions.Jail)
				{
					try
					{
						((Account)m.Account).Comments.Add(new AccountComment(JailSystem.JSName, releasor + "'s release Failed for " + m.Name + "(" + ((Account)m.Account).Username + ") at " + DateTime.Now + " to " + location.ToString() + "(" + map +")"));
					}
					catch{}
					return false;
				}
				else
				{
					try
					{
						((Account)m.Account).Comments.Add(new AccountComment(JailSystem.JSName, releasor + " released " + m.Name + "(" + ((Account)m.Account).Username + ") at " + DateTime.Now + " to " + location.ToString() + "(" + map +")"));
					}
					catch{}
					return true;
				}
				
				
			}
		}
		public static void newJailingFromGMandPlayer(Mobile from, Mobile m)
		{
			JailSystem js=JailSystem.fromMobile(m);
			if (js==null)
			{
				js=new JailSystem();
			}
			else
			{
				if(js.jailed)
				{
					from.SendMessage("{0} is already jailed", m.Name );
#if (jailDebug)
				System.Console.WriteLine ("JailDebug-newfromplayer/gm lockup already jailed");
#endif
					js.lockupMobile(m);
					return;
				}
			}
			js.resetReleaseDateOneDay();
#if (jailDebug)
				System.Console.WriteLine ("JailDebug-new from gm/player lockup");
#endif
			js.lockupMobile(m);
			js.jailor=from.Name;
			if(m.Equals(from))
				m.SendLocalizedMessage(503315);
			else
				m.SendLocalizedMessage(503316);
			from.SendGump((new JailGump(js,from,m,0,"", "")));
		}
		public class jailing : Timer
		{
			public JailSystem	Prisoner;
			public jailing(JailSystem js):base(js.ReleaseDate.Subtract(System.DateTime.Now))
			{
				Prisoner=js;
			}
			protected override void OnTick()
			{
				Prisoner.TimerRelease();
			}
		}
	}
	#region jail system target objects (used by commands)
	public class warnTarget : Target
	{
		private bool m_warn=false;
		public warnTarget(bool warn) : base( -1, false, TargetFlags.None )
		{
			m_warn=warn;
		}
		public warnTarget() : base( -1, false, TargetFlags.None )
		{
			m_warn=true;
		}
		protected override void OnTarget( Mobile from, object targeted )
		{
			if ( from is PlayerMobile && targeted is PlayerMobile )
			{
				Mobile m=(Mobile)targeted;
				if(m_warn)
					from.SendGump(new JailWarnGump(from,m,"",0,null));
				else
				{
					from.SendGump(new jailReviewGump(from,m,0,null));
				}
			}
		}
	}

	public class macroTarget : Target
	{
		public macroTarget() : base( -1, false, TargetFlags.None )
		{
		}
		protected override void OnTarget( Mobile from, object targeted )
		{
			if ( from is PlayerMobile && targeted is PlayerMobile )
			{
				Mobile m=(Mobile)targeted;
				JailSystem.macroTest(from,m);
			}
		}
	}
	public class cageTarget : Target
	{
		public cageTarget() : base( -1, false, TargetFlags.None )
		{
		}
		protected override void OnTarget( Mobile from, object targeted )
		{
			if ( from is PlayerMobile && targeted is PlayerMobile )
			{
				if ( from is PlayerMobile && targeted is PlayerMobile )
				{
					Mobile m=(Mobile)targeted;
					new aCage(m);
					Point3D newcell = m.Location;
					m.Location = new Point3D(0,0,0);
					m.Location = newcell;
				}
			}
		}
	}
	public class JailTarget : Target
	{
		bool m_releasing=false;
		public JailTarget(bool releasing) : base( -1, false, TargetFlags.None )
		{
			m_releasing=releasing;
		}
		protected override void OnTarget( Mobile from, object targeted )
		{
			if ( from is PlayerMobile && targeted is PlayerMobile )
			{
				string temp="jail";
				Mobile m=(Mobile)targeted;
				if (from.AccessLevel < m.AccessLevel)
				{
					from.SendMessage("{0} has a higher access level than you and you can not do that.", m.Name);
					if (m_releasing) temp="release";
					CommandLogging.WriteLine(from, from.Name + " tried to " + temp+ " " + m.Name );
					m.SendMessage(from.Name + " tried to " + temp+ " you");
				}
				else
				{//jailor has a higher (or equal) access level than the target				
					if (m_releasing) 
					{
						JailSystem js=JailSystem.fromAccount((Account)m.Account);
						if (js==null)
						{
							from.SendMessage(m.Name + " no jail object");
							return;
						}
						js.forceRelease(from);
						m.SendLocalizedMessage(501659);
					}
					else
					{
						//temp="jailed";
						JailSystem.newJailingFromGMandPlayer(from, m);		
					}
				}
			}
			else
			{
				from.SendLocalizedMessage(503312);
			}
		}
	}
	#endregion
	#region jail System gumps
	public class jailReviewGump : Gump
	{
		private Mobile badBoy;
		private Mobile jailor;
		private string m_reason="Breaking Shard Rules";
		private ArrayList m_warn;
		private int m_id;
		private bool displayReleases=false;
		public jailReviewGump(Mobile from, Mobile m):base( 1, 30 )
		{
			buildit(from, m,0,null,"");
		}
		public jailReviewGump(Mobile from, Mobile m, int id, ArrayList warnings):base( 1, 30 )
		{
			buildit(from, m, id, warnings, "");
		}
		public jailReviewGump(Mobile from, Mobile m, int id, ArrayList warnings, string note):base( 1, 30 )
		{
			buildit(from, m, id, warnings, note);
		}
		public jailReviewGump(Mobile from, Mobile m, int id, ArrayList warnings, bool showRelease):base( 1, 30 )
		{
			buildit(from, m, id, warnings, "", true, showRelease);
		}
		public jailReviewGump(Mobile from, Mobile m, int id, ArrayList warnings, string note, bool showRelease):base( 1, 30 )
		{
			buildit(from, m, id, warnings, note, true, showRelease);
		}
		public void buildit(Mobile from, Mobile m, int id, ArrayList warnings, string aNote)
		{
			buildit(from, m, id, warnings, aNote, true, false);
		}
		public void buildit(Mobile from, Mobile m, int id, ArrayList warnings, string aNote, bool tGo, bool showRelease)
		{
			displayReleases=showRelease;
			from.CloseGump(typeof ( jailReviewGump ));
			jailor=from;
			badBoy=m;
			m_id=id;
			Closable = true;
			Dragable = true;
			AddPage(0);
			AddBackground( 0, 0, 326, 230, 5054);
			AddLabel( 12, 4, 200,"Reviewing: "+ badBoy.Name + " (" + ((Account)badBoy.Account).Username +")");
			if (tGo)
			{
				AddLabel( 300, 17, 200,"GO");
				AddButton( 280, 20, 2223, 2224, 2, GumpButtonType.Reply, 0 );
			}
			AddLabel( 12, 200, 200,"Note");
			AddBackground( 42, 198, 268, 24, 0x2486 );
			AddTextEntry( 46, 200, 250, 20, 200, 0,aNote );
			//add button
			AddButton( 70, 150, 2460, 2461, 1, GumpButtonType.Reply, 0 );
			//previous button
			AddButton( 120, 150, 2466, 2467, 20, GumpButtonType.Reply, 0 );
			//next Button
			AddButton( 200, 150, 2469, 2470, 21, GumpButtonType.Reply, 0 );
			//release toggle
			AddButton(115,167, displayReleases ? 2154 : 2151, displayReleases ? 2151 : 2154,22,GumpButtonType.Reply, 0);
			AddLabel( 147, 171, 200,"Show Releases");
			if (warnings==null)
			{
				m_warn=new ArrayList();
				foreach (AccountComment note in ((Account)m.Account).Comments)
				{
					if((note.AddedBy==JailSystem.JSName + "-warning")||(note.AddedBy==JailSystem.JSName + "-jailed")||(note.AddedBy==JailSystem.JSName + "-note")||((displayReleases)&&((note.AddedBy==JailSystem.JSName))))
					{
						m_warn.Add(note);
					}
				}
				m_id=m_warn.Count-1;
			}
			else
			{
				m_warn=warnings;
			}
			AddImageTiled( 9, 36, 308, 110, 2624 );
			AddAlphaRegion( 9, 36, 308, 110 );
			string temp="No prior warnings.";
			if (m_warn.Count>0)
			{
				if (m_id < 0) m_id= m_warn.Count-1;
				if (m_id >=m_warn.Count) m_id=0;
				temp=((AccountComment)m_warn[m_id]).Content;
				if ( ((AccountComment)m_warn[m_id]).AddedBy==JailSystem.JSName + "-warning")
					AddLabel( 12, 40,53,"Warned");
				else if ( ((AccountComment)m_warn[m_id]).AddedBy==JailSystem.JSName + "-jailed")
					AddLabel( 12, 40, 38,"Jailed");
				else if ( ((AccountComment)m_warn[m_id]).AddedBy==JailSystem.JSName + "-note")
					AddLabel( 12, 40, 2,"Note");	
				else
					AddLabel( 12, 40, 2,"Release");
				AddLabel( 60, 40, 200, "Issued: " + ((AccountComment)m_warn[m_id]).LastModified.ToString() );
			}
			else
			{
				//no prior warning	
				m_id=-1;
			}
			AddLabel( 12, 60, 200, "Event " + (m_id + 1) + " of " + m_warn.Count.ToString());
			//AddLabel( 12, 230, 200, temp );
			AddHtml(12,80,300,62,temp,true,true);
		}
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from=sender.Mobile;
			switch ( info.ButtonID )
			{
				case 22:
					displayReleases=(!displayReleases);
					from.SendGump(new jailReviewGump(from,badBoy,0,null,info.GetTextEntry(0).Text, displayReleases));
					break;
				case 20:
					//previous button
					m_id--;
					if (m_id<0) m_id=m_warn.Count-1;
					from.SendGump(new jailReviewGump(from, badBoy, m_id, m_warn, info.GetTextEntry(0).Text, displayReleases));
					break;
				case 21:
					//next button
					m_id++;
					if (m_id>=m_warn.Count) m_id=0;
					from.SendGump(new jailReviewGump(from, badBoy, m_id, m_warn, info.GetTextEntry(0).Text, displayReleases));
					break;
					//reason buttons
				case 2:
					from.SendGump(new jailReviewGump(from, badBoy, m_id, m_warn, info.GetTextEntry(0).Text, displayReleases));
					from.Hidden=true;
					from.Location=badBoy.Location;
					from.Map=badBoy.Map;
					break;
				case 1:
					if (info.GetTextEntry(0).Text!="note added")
					{
						((Account)badBoy.Account).Comments.Add(new AccountComment(JailSystem.JSName + "-note", info.GetTextEntry(0).Text + " by: " + from.Name));
						from.SendGump(new jailReviewGump(from,badBoy,0,null,"note added", displayReleases));
					}
					else
						from.SendGump(new jailReviewGump(from,badBoy,0,null,"",displayReleases));
					break;
				default:
					break;
			}
		}
	}
	public class JailWarnGump : Gump
	{
		private Mobile badBoy;
		private Mobile jailor;
		private string m_reason="Breaking Shard Rules";
		private ArrayList m_warn;
		private int m_id;
		public JailWarnGump(Mobile from, Mobile m, string why, int id, ArrayList warnings):base( 100, 40 )
		{
			from.CloseGump(typeof ( JailWarnGump ));
			if ((why==null)||(why=="")) why=JailGump.reasons[0];
			jailor=from;
			badBoy=m;
			m_reason=why;
			m_id=id;
			Closable = true;
			Dragable = true;
			AddPage(0);
			AddBackground( 0, 0, 326, 320, 5054);
			AddImageTiled( 9, 6, 308, 140, 2624 );
			AddAlphaRegion( 9, 6, 308, 140 );
			AddLabel( 16, 98, 200, "Reason");
			AddBackground( 14, 114, 290, 24, 0x2486 );
			AddTextEntry( 18, 116, 282, 20, 200, 0,m_reason );
			AddButton( 14, 11, 1209, 1210, 3, GumpButtonType.Reply, 0 );
			AddLabel( 30, 7, 200, JailGump.reasons[0] );
			AddButton( 14, 29, 1209, 1210, 4, GumpButtonType.Reply, 0 );
			AddLabel( 30, 25, 200, JailGump.reasons[1]);
			AddButton( 14, 47, 1209, 1210, 5, GumpButtonType.Reply, 0 );
			AddLabel( 30, 43, 200, JailGump.reasons[2]);
			AddButton( 150, 11, 1209, 1210, 6, GumpButtonType.Reply, 0 );
			AddLabel( 170, 7, 200, JailGump.reasons[3] );
			AddButton( 150, 29, 1209, 1210, 7, GumpButtonType.Reply, 0 );
			AddLabel( 170, 24, 200, JailGump.reasons[4] );
			AddButton( 150, 47, 1209, 1210, 8, GumpButtonType.Reply, 0 );
			AddLabel( 170, 43, 200, JailGump.reasons[5] );
			AddButton( 14, 66, 1209, 1210, 9, GumpButtonType.Reply, 0 );
			AddLabel( 30, 62, 200, JailGump.reasons[6] );
			AddButton( 14, 84, 1209, 1210, 10, GumpButtonType.Reply, 0 );
			AddLabel( 30, 80, 200, JailGump.reasons[7] );
			//warn button
			AddButton( 218, 152, 2472, 2473, 1, GumpButtonType.Reply, 0 );
			AddLabel( 248, 155, 200, "Warn them" );
			//Jail button
			AddButton( 20, 152, 2472, 2473, 2, GumpButtonType.Reply, 0 );
			AddLabel( 50, 155, 200, "Jail them" );
			//previous button
			AddButton( 10, 300, 2466, 2467, 20, GumpButtonType.Reply, 0 );
			//next Button
			AddButton( 90, 300, 2469, 2470, 21, GumpButtonType.Reply, 0 );
			if (warnings==null)
			{
				m_warn=new ArrayList();
				foreach (AccountComment note in ((Account)m.Account).Comments)
				{
					if((note.AddedBy==JailSystem.JSName + "-warning")||(note.AddedBy==JailSystem.JSName + "-jailed"))
					{
						m_warn.Add(note);
					}
				}
				m_id=m_warn.Count-1;
			}
			else
			{
				m_warn=warnings;
			}
			AddImageTiled( 9, 186, 308, 110, 2624 );
			AddAlphaRegion( 9, 186, 308, 110 );
			string temp="No prior warnings.";
			if (m_warn.Count>0)
			{
				if (m_id < 0) m_id= m_warn.Count-1;
				if (m_id >=m_warn.Count) m_id=0;
				temp=((AccountComment)m_warn[m_id]).Content;
				AddLabel( 12, 190, 200, "Issued: " + ((AccountComment)m_warn[m_id]).LastModified.ToString() );
			}
			else
			{
				//no prior warning	
				m_id=-1;
			}
			AddLabel( 12, 210, 200, "Event " + (m_id + 1) + " of " + m_warn.Count.ToString() + " warnings/Jailings");
			//AddLabel( 12, 230, 200, temp );
			AddHtml(12,230,300,62,temp,true,true);	
		}
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from=sender.Mobile;
			switch ( info.ButtonID )
			{
				case 20:
					//previous button
					m_id--;
					if (m_id<0) m_id=m_warn.Count-1;
					from.SendGump(new JailWarnGump(from, badBoy, info.GetTextEntry(0).Text, m_id, m_warn));
					break;
				case 21:
					//next button
					m_id++;
					if (m_id>=m_warn.Count) m_id=0;
					from.SendGump(new JailWarnGump(from, badBoy, info.GetTextEntry(0).Text, m_id, m_warn));
					break;
					//reason buttons
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
				case 9:
				case 10:
					from.SendGump(new JailWarnGump(from, badBoy, JailGump.reasons[info.ButtonID - 3], m_id, m_warn));
					break;
				case 1:
					//warn them
					from.CloseGump(typeof ( JailWarnGump ));
					if (m_reason==JailGump.reasons[0])
					{
						//they are macroing
						JailSystem.macroTest(from,badBoy);
					}
					else
					{
						//not Unattended macroing
						badBoy.SendGump(new JailWarningGump(from, badBoy, m_reason));
					}
					break;
				case 2:
					//jail them
					from.CloseGump(typeof ( JailWarnGump ));
					from.SendGump(new JailGump(JailSystem.lockup(badBoy),from,badBoy,0,"",m_reason,"0","0","1","0","0",true));
					break;
				default:
					break;
			}
		}
	}
	public class unattendedMacroGump : Gump
	{
		private Mobile badBoy;
		private Mobile jailor;
		DateTime issued=DateTime.Now;
		UAResponseTimer myTimer;
		int tbutton=2;
		bool caughtFired=false;
		public unattendedMacroGump(Mobile from, Mobile m) : base( 70, 40 )
		{
			tbutton=(new System.Random()).Next(6);
			if (tbutton <1)tbutton=1;
			if (tbutton >6)tbutton=6;
			((Account)m.Account).Comments.Add(new AccountComment(JailSystem.JSName + "-warning", from.Name + " checked to see if " + m.Name + " was macroing unattended on: " + DateTime.Now ));
			jailor=from;
			badBoy=m;
			Closable = false;
			Dragable = true;
			AddPage(0);
			AddBackground( 0, 0, 326, 320, 5054);
			AddImageTiled( 9, 65, 308, 240, 2624 );
			AddAlphaRegion( 9, 65, 308, 240 );
			//AddLabel( 16, 20, 200, string.Format("{0} is checking to see if you are macroing unattended", jailor.Name));
			this.AddHtml(16,10,250,50,string.Format("{0} is checking to see if you are macroing unattended", jailor.Name), false, false);			
			//let them show that they are there by selecting these buttons
			AddButton( 20, 72, 2472, 2473, 5, GumpButtonType.Reply, 0 );
			AddLabel( 50, 75, 200, tbutton==5 ? "I'm here!" : "I confess I was macroing unattended." );
			AddButton( 20, 112, 2472, 2473, 1, GumpButtonType.Reply, 0 );
			AddLabel( 50, 115, 200, tbutton==1 ? "I'm here!" : "I confess I was macroing unattended." );
			AddButton( 20, 152, 2472, 2473, 2, GumpButtonType.Reply, 0 );
			AddLabel( 50, 155, 200, tbutton==2 ? "I'm here!" : "I confess I was macroing unattended." );
			AddButton( 20, 192, 2472, 2473, 3, GumpButtonType.Reply, 0 );
			AddLabel( 50, 195, 200, tbutton==3 ? "I'm here!" : "I confess I was macroing unattended.");
			AddButton( 20, 232, 2472, 2473, 4, GumpButtonType.Reply, 0 );
			AddLabel( 50, 235, 200, tbutton==4 ? "I'm here!" : "I confess I was macroing unattended.");
			AddButton( 20, 272, 2472, 2473, 6, GumpButtonType.Reply, 0 );
			AddLabel( 50, 275, 200, tbutton==6 ? "I'm here!" : "I confess I was macroing unattended.");
			myTimer=new UAResponseTimer(this);
		}
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from=sender.Mobile;
			if (myTimer!=null)
				myTimer.Stop();
			if(tbutton==info.ButtonID)
			{
				string mtemp=string.Format("{0} responded to the unattended macroing check in {1} seconds.", from.Name,((TimeSpan)(DateTime.Now.Subtract(issued))).Seconds);
				((Account)badBoy.Account).Comments.Add(new AccountComment(JailSystem.JSName + "-warning", mtemp  ));
				jailor.SendMessage(mtemp);
			}
			else
			{
#if (jailDebug)
				System.Console.WriteLine ("JailDebug-caughtintheact called gump response");
#endif
				caughtInTheAct(false);			
			}
			from.CloseGump(typeof ( unattendedMacroGump ));
		}
		public class UAResponseTimer : Timer
		{
			public unattendedMacroGump m_gump;
			int counts=60;
			public UAResponseTimer(unattendedMacroGump myGump) : base(TimeSpan.FromSeconds(10),TimeSpan.FromSeconds(10))
			{
				m_gump=myGump;
				this.Start();
			}
			protected override void OnTick()
			{
				counts-=this.Interval.Seconds;
				switch (counts)
				{
					case 50:
					case 40:
					case 30:
					case 20:
						this.Interval=TimeSpan.FromSeconds(1);
						goto case 10;
					case 10:
					case 9:
					case 8:
					case 7:
					case 6:
					case 5:
					case 4:
					case 3:
					case 2:
					case 1:
						m_gump.badBoy.SendMessage("Warning closing in {0} seconds", counts);
						break;
					case 0:
#if (jailDebug)
						System.Console.WriteLine ("JailDebug-caughtintheact called um gump timer");
#endif
						m_gump.caughtInTheAct(false);
						m_gump.badBoy.CloseGump(typeof ( unattendedMacroGump ));
						this.Stop();
						break;
					default:
						break;
				}
			}
		}
		public void caughtInTheAct(bool confessed)
		{
			if(caughtFired) return;
			caughtFired=true;
			if(!confessed)
			{
#if (jailDebug)
				System.Console.WriteLine ("JailDebug-jail called from caughtInTheAct not confessed");
#endif
				JailSystem.Jail(badBoy, 1, 0, 0, JailGump.reasons[0], true, jailor.Name);
				jailor.SendMessage("{0} has been jailed for {1} from the warning you issued.", badBoy.Name, JailGump.reasons[0]);
			}
			else
			{
#if (jailDebug)
				System.Console.WriteLine ("JailDebug-jail called from caughtintheact confessed");
#endif
				JailSystem.Jail(badBoy,0,5,0,JailGump.reasons[0], true, jailor.Name);
				jailor.SendMessage("{0} was been jailed for {1} when they confessed on the warning you issued.", badBoy.Name, JailGump.reasons[0]);
			}
			if (myTimer!=null)
			{
				myTimer.Stop();
			}
		}
	}
	public class JailWarningGump : Gump
	{
		public JailWarningGump(Mobile from, Mobile m, string why) : base( 70, 40 )
		{
			//from.CloseGump(typeof ( JailWarningGump ));
			((Account)m.Account).Comments.Add(new AccountComment(JailSystem.JSName + "-warning", m.Name + " warned for \"" + why + "\" by:" + from.Name + " on:" + DateTime.Now ));
			Closable = false;
			Dragable = false;
			Resizable=false;
			AddPage(0);
			if ((why==null)||(why=="")) why="Your actions are a violation of the shard rules";
			AddBackground( 0, 0, 500, 400, 5054);
			//ok button
			AddLabel( 26, 30, 200, from.Name + " has issued a you a warning");
			AddLabel( 26, 50, 200, why);
			AddLabel( 26, 100, 200, "Click ok to dismiss this window");
			AddButton( 30 + ((int)(new System.Random()).Next(100)), 142+((int)(new System.Random()).Next(100)), 2128, 2130, 1, GumpButtonType.Reply, 0 );
		}
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from=sender.Mobile;
			switch ( info.ButtonID )
			{
				case 1:
					from.CloseGump(typeof ( JailWarningGump ));
					break;
				default:
					break;
			}
		}
	}
	public class JailGump : Gump
	{
		private Mobile badBoy;
		private Mobile jailor;
		private int m_page;
		private bool m_return;
		private JailSystem js;
		private string m_reason="Breaking Shard Rules";
		public static string[] reasons={"Unattended Macroing", "Disruptive behavior", "Arguing with Staff", "Harassing other players",
										   "Exploiting Bugs", "Scamming", "Breaking out of Character", "Exposing a Staff Members Player Account"};
		public JailGump(JailSystem tjs, Mobile owner, Mobile prisoner, int page, string error, string reason ) : base( 100, 40 )
		{
			buildIt(tjs, owner, prisoner, page, error,reason,"0","0","1","0","0",true);
		}
		public JailGump(JailSystem tjs, Mobile owner, Mobile prisoner, int page, string error, string reason, string month, string week, string day, string hour, string minute, bool fullreturn ) : base( 100, 40 )
		{
			buildIt(tjs, owner, prisoner, page, error, reason, month, week, day, hour, minute, fullreturn );
		}
		public void buildIt(JailSystem tjs, Mobile owner, Mobile prisoner, int page, string error, string reason, string month, string week, string day, string hour, string minute, bool fullreturn )
		{
			js=tjs;
			m_return=fullreturn;
			m_page=page;
			if ((reason !="")&&(reason != null))
				m_reason=reason;
			else
				m_reason=reasons[1];
			jailor=owner;
			badBoy=prisoner;
			m_reason=reason;
			jailor.CloseGump( typeof ( JailGump ) );
			Closable = false;
			Dragable = false;
			AddPage(0);
			AddBackground( 0, 0, 326, 295, 5054);
			AddImageTiled( 9, 6, 308, 140, 2624 );
			AddAlphaRegion( 9, 6, 308, 140 );
			AddLabel( 16, 98, 200, "Reason");
			AddBackground( 14, 114, 290, 24, 0x2486 );
			AddTextEntry( 18, 116, 282, 20, 200, 0,m_reason );
			AddButton( 14, 11, 1209, 1210, 3, GumpButtonType.Reply, 0 );
			AddLabel( 30, 7, 200, reasons[0] );
			AddButton( 14, 29, 1209, 1210, 4, GumpButtonType.Reply, 0 );
			AddLabel( 30, 25, 200, reasons[1]);
			AddButton( 14, 47, 1209, 1210, 5, GumpButtonType.Reply, 0 );
			AddLabel( 30, 43, 200, reasons[2]);
			AddButton( 150, 11, 1209, 1210, 6, GumpButtonType.Reply, 0 );
			AddLabel( 170, 7, 200, reasons[3] );
			AddButton( 150, 29, 1209, 1210, 7, GumpButtonType.Reply, 0 );
			AddLabel( 170, 24, 200, reasons[4] );
			AddButton( 150, 47, 1209, 1210, 8, GumpButtonType.Reply, 0 );
			AddLabel( 170, 43, 200, reasons[5] );
			AddButton( 14, 66, 1209, 1210, 9, GumpButtonType.Reply, 0 );
			AddLabel( 30, 62, 200, reasons[6] );
			AddButton( 14, 84, 1209, 1210, 10, GumpButtonType.Reply, 0 );
			AddLabel( 30, 80, 200, reasons[7] );
			//ok button
			AddButton( 258, 268, 2128, 2130, 1, GumpButtonType.Reply, 0 );
			AddImageTiled( 8, 153, 308, 113, 2624 );
			AddAlphaRegion( 8, 153, 308, 113 );
			if(m_return==true)
				AddButton( 15, 210, 2153,2151,2,GumpButtonType.Reply, 0 );
			else
				AddButton( 15, 210, 2151,2153,2,GumpButtonType.Reply, 0 );
			AddLabel( 50, 212, 200, "Return to where jailed from on release" );
			if ((error!="")&&(error!=null))
			{
				AddLabel( 10, 235, 200, error );
			}
			if(m_page==0)
			{
				//auto
				//auto/manual
				AddButton( 11, 268, 2111, 2114, 25, GumpButtonType.Reply, 0 );
				AddLabel( 16, 160, 200, "Months" );
				AddBackground( 19, 178, 34, 24, 0x2486 );
				AddTextEntry( 21, 180, 30, 20, 0, 7, month );
				AddLabel( 62, 160, 200, "Weeks" );
				AddBackground( 63, 178, 34, 24, 0x2486 );
				AddTextEntry( 65, 180, 30, 20, 0, 6, week );
				AddLabel( 106, 160, 200, "Days" );
				AddBackground( 104, 178, 34, 24, 0x2486 );
				AddTextEntry( 107, 180, 30, 20, 0, 5, day );
				AddLabel( 145, 160, 200, "Hours" );
				AddBackground( 145, 178, 34, 24, 0x2486 );
				AddTextEntry( 147, 180, 30, 20, 0, 9, hour );
				AddLabel( 185, 160, 200, "Minutes" );
				AddBackground(191, 178, 34, 24, 0x2486 );
				AddTextEntry( 194, 180, 30, 20, 0, 8, minute );
			}
			else
			{
				AddButton( 11, 268, 2114, 2111, 27, GumpButtonType.Reply, 0 );
				AddLabel( 14, 160, 200, "Account will be Jailed for one year" );
				AddLabel( 14, 178, 200, "or until released, which comes first" );

			}
		}
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from=sender.Mobile;
			switch ( info.ButtonID )
			{
					//reason buttons
				case 25:
					from.SendGump((new JailGump(js,from,badBoy,1,"", info.TextEntries[0].Text, "0","0", "1", "0","0",m_return)));
					//, info.GetTextEntry(7), info.GetTextEntry(6), info.GetTextEntry(5), info.GetTextEntry(9), info.GetTextEntry(8),m_return
					break;
				case 27:
					from.SendGump((new JailGump(js,from,badBoy,0,"", info.TextEntries[0].Text, "0","0", "1", "0","0",m_return)));
					break;
				case 2:
					m_return=!m_return;
					if(m_page==1)
						from.SendGump((new JailGump(js, from, badBoy, m_page,"", info.TextEntries[0].Text, "0","0", "1", "0","0",m_return)));
					else
						from.SendGump((new JailGump(js, from, badBoy, m_page,"", info.TextEntries[0].Text, info.GetTextEntry(7).Text, info.GetTextEntry(6).Text, info.GetTextEntry(5).Text, info.GetTextEntry(9).Text, info.GetTextEntry(8).Text, m_return)));
					break;
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
				case 9:
				case 10:
					if(m_page==1)
						from.SendGump((new JailGump(js, from, badBoy, m_page,"", reasons[info.ButtonID - 3], "0","0", "1", "0","0",m_return)));
					else
						from.SendGump((new JailGump(js, from, badBoy, m_page,"", reasons[info.ButtonID - 3],info.GetTextEntry(7).Text, info.GetTextEntry(6).Text, info.GetTextEntry(5).Text, info.GetTextEntry(9).Text, info.GetTextEntry(8).Text, m_return)));
					break;
				case 1:
				{
					DateTime dt_unJail=DateTime.Now;
					string m_Error="";
					int i_days=0;
					int i_weeks=0;
					int i_months=0;
					int i_minutes=0;
					int i_hours=0;
					if ( m_page == 0 )
					{
						try
						{
							i_days = Convert.ToInt32( ( info.GetTextEntry( 5 ) ).Text.Trim() );
						}
						catch
						{
							m_Error = "Bad day(s) entry! No negative values or chars.";
						}
						try
						{
							i_weeks = Convert.ToInt32( ( info.GetTextEntry( 6 ) ).Text.Trim() );
						}
						catch
						{
							if ( m_Error == "" )
								m_Error = "Bad week(s) entry! No negative values or chars.";
						}
						try
						{
							i_months = Convert.ToInt32( ( info.GetTextEntry( 7 ) ).Text.Trim() );
						}
						catch
						{
							if ( m_Error == "" )
								m_Error = "Bad month(s) entry! No negative values or chars.";
						}
						try
						{
							i_minutes = Convert.ToInt32( ( info.GetTextEntry( 8 ) ).Text.Trim() );
						}
						catch
						{
							if ( m_Error == "" )
								m_Error = "Bad minute(s) entry! No negative values or chars.";
						}
						try
						{
							i_hours = Convert.ToInt32( ( info.GetTextEntry( 9 ) ).Text.Trim() );
						}
						catch
						{
							if ( m_Error == "" )
								m_Error = "Bad hour(s) entry! No negative values or chars.";
						}
						if ( ( ( i_days > 7 ) || ( i_days < 0 ) ) && ( m_Error == "" ) )
						{
							if ( m_Error == "" )
								m_Error = "Bad day(s) entry! No negative values. 7 days max.";
						}
						if ( ( ( i_weeks > 4 ) || ( i_weeks < 0 ) ) && ( m_Error == "" ) )
						{
							if ( m_Error == "" )
								m_Error = "Bad week(s) entry! No negative values. 4 weeks max.";
						}
						if ( ( ( i_months > 12 ) || ( i_months < 0 ) ) && ( m_Error == "" ) )
						{
							if ( m_Error == "" )
								m_Error = "Bad month(s) entry! No negative values. 1 year max.";
						}
						if ( ( ( i_minutes > 60 ) || ( i_minutes < 0 ) ) && ( m_Error == "" ) )
						{
							if ( m_Error == "" )
								m_Error = "Bad minute(s) entry! No negative values. 1 hour max.";
						}
						if ( ( ( i_hours > 24 ) || ( i_hours < 0 ) ) && ( m_Error == "" ) )
						{
							if ( m_Error == "" )
								m_Error = "Bad hour(s) entry! No negative values. 1 day max.";
						}
						if ( m_Error != "" )
						{
							from.SendGump( new JailGump(js, from, badBoy, m_page, m_Error, info.TextEntries[0].Text ,i_months.ToString() , i_weeks.ToString(), i_days.ToString(),i_hours.ToString(),i_minutes.ToString(), m_return) );
							break;
						}	
						if ( i_days > 0 )
							dt_unJail = dt_unJail.AddDays( i_days );
						if ( i_weeks > 0 )
							dt_unJail = dt_unJail.AddDays( ( i_weeks * 7 ) );
						if ( i_months > 0 )
							dt_unJail = dt_unJail.AddMonths( i_months );
						if ( i_minutes > 0 )
							dt_unJail = dt_unJail.AddMinutes( i_minutes );
						if ( i_hours > 0 )
							dt_unJail = dt_unJail.AddHours( i_hours );
						if ( dt_unJail.Ticks <= DateTime.Now.Ticks )
						{
							m_Error = "Calculated date is in the past. Adjust your entries.";
							from.SendGump( new JailGump(js, from, badBoy, m_page, m_Error, info.TextEntries[0].Text ,i_months.ToString() , i_weeks.ToString(), i_days.ToString(),i_hours.ToString(),i_minutes.ToString(),m_return ));
							break;
						}
					}
					else
					{
						//page isn’t the time span
						dt_unJail=dt_unJail.AddYears(1);
						if ( dt_unJail.Ticks <= DateTime.Now.Ticks )
						{
							m_Error = "Calculated date is in the past. Adjust your entries.";
							from.SendGump( new JailGump(js, from, badBoy, m_page, m_Error, info.TextEntries[0].Text ,"12" , "0", "0","0","0",m_return) );
							break;
						}
					}
					js.fillJailReport(badBoy, dt_unJail, info.TextEntries[0].Text, m_return, from.Name);
				}
					from.CloseGump(typeof ( JailGump ));
					from.SendGump(new jailReviewGump(from,badBoy,0,null));
					break;
				default:
					//they hit an unknown button
					if(m_page==1)
						from.SendGump((new JailGump(js, from, badBoy, m_page,"", info.TextEntries[0].Text, "0","0", "1", "0","0",m_return)));
					else
						from.SendGump((new JailGump(js, from, badBoy, m_page,"", info.TextEntries[0].Text, info.GetTextEntry(7).Text, info.GetTextEntry(6).Text, info.GetTextEntry(5).Text, info.GetTextEntry(9).Text, info.GetTextEntry(8).Text, m_return)));
					//close the Gump, we're done
					break;
			}
		}
	}
	public class JailBanGump : Gump
	{
		private JailSystem m_js=null;
		public JailBanGump(JailSystem js) : base(10,30)
		{
			
			buildit(js);
		}
		private const int LabelColor32 = 0xFFFFFF;
		private const int SelectedColor32 = 0x8080FF;
		private const int DisabledColor32 = 0x808080;
		public void AddBlackAlpha( int x, int y, int width, int height )
		{
			AddImageTiled( x, y, width, height, 2624 );
			AddAlphaRegion( x, y, width, height );
		}
		
		public string Color( string text, int color )
		{
			return String.Format( "<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", color, text );
		}
		public void AddColorLabel(int x, int y, int width, int height, string text)
		{
			AddHtml( x, y, width, height, Color( text, LabelColor32 ), false, false );
		}
		public void buildit(JailSystem js)
		{
			m_js=js;
			AddBackground( 0, 0, 300, 300, 5054);
			AddBlackAlpha( 5, 5, 290, 290 );
			AddColorLabel( 8, 8,288,288,Color("Are you sure you wish to ban this account?",SelectedColor32));
			AddButton(120,200,241,243,10,GumpButtonType.Reply,0);
			AddButton( 50, 200,239,240,6,GumpButtonType.Reply,0);	
		}
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from=sender.Mobile;
			if(info.ButtonID==6)
			{
				m_js.ban(from);
				//ban them here
			}
		}
	}
	public class JailAdminGump : Gump
	{
		public enum AdminJailGumpPage
		{
			General,
			OOC,
			Language,
			Review
		}
		private const int LabelColor = 0x7FFF;
		private const int SelectedColor = 0x421F;
		private const int DisabledColor = 0x4210;

		private const int LabelColor32 = 0xFFFFFF;
		private const int SelectedColor32 = 0x8080FF;
		private const int DisabledColor32 = 0x808080;

		private AdminJailGumpPage m_page=AdminJailGumpPage.General;
		private int m_subpage=0;
		private int m_id=0;
		private const int TitleX=210;
		private const int TitleY=7;
		private const int BodyX=5;
		private const int BodyY=111;
		private const int MessageX=5;
		private const int MessageY=387;
		private const int gutterOffset=3;
		private const int LineStep=25;
		private JailSystem js=null;
		public void AddTextField( int x, int y, int width, int height, int index )
		{
			AddTextField(x,y,width,height,index,"");
		}
		public void AddTextField( int x, int y, int width, int height, int index, string content)
		{
			AddBackground( x - 2, y - 2, width + 4, height + 4, 0x2486 );
			AddTextEntry( x + 2, y + 2, width - 4, height - 4, 0, index, content );
		}
		public string Color( string text, int color )
		{
			return String.Format( "<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", color, text );
		}

		public void AddButtonLabeled( int x, int y, int buttonID, string text )
		{
			AddButton( x, y - 1, 4005, 4007, buttonID, GumpButtonType.Reply, 0 );
			AddHtml( x + 35, y, 240, 20, Color( text, LabelColor32 ), false, false );
		}
		public void AddToggleLabeled( int x, int y, int buttonID, string text, bool selected )
		{
			AddButton( x, y - 1, selected ? 2154 : 2152, selected ? 2152 : 2154, buttonID, GumpButtonType.Reply, 0 );
			AddHtml( x + 35, y, 240, 20, Color( text, LabelColor32 ), false, false );
		}	
		public void AddPageLabeled( int x, int y, int buttonID, string text, AdminJailGumpPage page )
		{
			AddButton( x, y - 1, (m_page==page) ? 4006 : 4005, 4007, buttonID, GumpButtonType.Reply, 0 );
			AddHtml( x + 35, y, 240, 20, (m_page==page) ? Color( text, LabelColor32 ) : Color( text, SelectedColor32 ), false, false );
		}
		public void AddBlackAlpha( int x, int y, int width, int height )
		{
			AddImageTiled( x, y, width, height, 2624 );
			AddAlphaRegion( x, y, width, height );
		}
		public JailAdminGump() : base(10,30)
		{
			buildit(AdminJailGumpPage.Review,0,0);
		}
		public void message(string text)
		{
			AddColorLabel( MessageX + gutterOffset,MessageY + gutterOffset,390, 40, text);
		}
		public void message()
		{
			this.message("Settings changes will be saved during the world save.");
		}
		private int basex(int x)
		{
			return x + gutterOffset;
		}
		private int basey(int y)
		{
			return basey(y,0);
		}
		private int basey(int y, int lines)
		{
			return (y + gutterOffset)+(lines * LineStep);
		}
		public void AddColorLabel(int x, int y, string text)
		{
			AddColorLabel( x, y, 240, 20,text );
		}
		public void AddColorLabel(int x, int y, int width, int height, string text)
		{
			AddHtml( x, y, width, height, Color( text, LabelColor32 ), false, false );
		}
		public void AddColorLabelScroll(int x, int y, int width, int height, string text)
		{
			AddHtml( x, y, width, height, Color( text, LabelColor32 ),true, true );
		}
		public JailAdminGump(AdminJailGumpPage page) : base(10,30)
		{
			buildit(page,0,0);
		}
		public JailAdminGump(AdminJailGumpPage page, int subpage, int id) : base(10,30)
		{
			buildit( page, subpage, id);
		}
		private void buildit( AdminJailGumpPage page, int subpage, int id)
		{
			Closable = true;
			Dragable = true;
			m_id=id;
			m_page=page;
			m_subpage=subpage;
			AddPage(0);

			AddBackground( 0, 0, 412, 439, 5054);
			AddBlackAlpha( 5, 8, 200, 98 );
			AddBlackAlpha( TitleX, TitleY, 190, 98 );
			AddBlackAlpha( BodyX, BodyY, 396, 271 );
			AddBlackAlpha( MessageX, MessageY, 396, 46 );
			AddPageLabeled(7,12,4,"Review Current Jailings",AdminJailGumpPage.Review);
			AddPageLabeled(7,36,3,"Language Settings",AdminJailGumpPage.Language);
			AddPageLabeled(7,59,2,"OOC Settings",AdminJailGumpPage.OOC);
			AddPageLabeled(7,82,1,"General Settings",AdminJailGumpPage.General);

			AddButton(TitleX + 120,TitleY + 75,241,243,5,GumpButtonType.Reply,0);
			switch (m_page)
			{
				case AdminJailGumpPage.Review:
					buildReviews();
					break;
				case AdminJailGumpPage.General:
					buildSettings();
					break;
				case AdminJailGumpPage.Language:
					buildLanguage();
					break;
				case AdminJailGumpPage.OOC:
					buildOOC();
					break;
				default:
					break;
			}		
		}
		private void buildLanguage()
		{
			this.AddToggleLabeled(basex(TitleX),basey(TitleY,0),13,"Use Language",JailSystem.useLanguageFilter);
			message();
			if (!JailSystem.useLanguageFilter) return;
			AddButton(TitleX + 50,TitleY + 75,239,240,15,GumpButtonType.Reply,0);
			AddLabel(basex( BodyX),basey(BodyY,0),200,"Misc.");
			AddLabel(basex( BodyX)+15,basey(BodyY,1),200,"Foul Jailor");
			this.AddTextField(basex( BodyX)+80,basey(BodyY,1),150,20,12,JailSystem.foulJailorName);
			this.AddToggleLabeled(basex(BodyX)+15,basey(BodyY,2),14,"Allow Staff to use bad words",JailSystem.allowStaffBadWords);
			
			AddLabel(basex( BodyX)+ 240,basey(BodyY,0),200,"Bad words");
			string temp="";
			foreach (string p in JailSystem.badWords)
				temp+= string.Format("{0}\n",p.ToString());
			this.AddColorLabelScroll(basex(BodyX)+240,basey(BodyY,1),150,60,temp.Trim());

			this.AddTextField(basex(BodyX)+240,basey(BodyY,1)+65,150,20,13);
			AddButton(BodyX + 240,basey(BodyY,1)+90,2461,2462,26,GumpButtonType.Reply,0);
			AddButton(BodyX + 295,basey(BodyY,1)+90,2464,2465,27,GumpButtonType.Reply,0);

			AddLabel(basex( BodyX)+ 240,basey(BodyY,5)+10,200,"Jail Terms");
			temp="";
			foreach (TimeSpan t in JailSystem.FoulMouthJailTimes)
				temp+= string.Format("d={0} h={1} m={2}\n",t.Days, t.Hours, t.Minutes);
			this.AddColorLabelScroll(basex(BodyX)+240,basey(BodyY,6)+10,150,60,temp.Trim());

			AddLabel(basex(BodyX)+240,basey(BodyY,6)+75,200,"D");
			AddLabel(basex(BodyX)+290,basey(BodyY,6)+75,200,"H");
			AddLabel(basex(BodyX)+340,basey(BodyY,6)+75,200,"M");
			this.AddTextField(basex(BodyX)+255,basey(BodyY,6)+75,30,20,8);
			this.AddTextField(basex(BodyX)+305,basey(BodyY,6)+75,30,20,9);
			this.AddTextField(basex(BodyX)+355,basey(BodyY,6)+75,30,20,10);
			AddButton(BodyX + 240,basey(BodyY,6)+100,2461,2462,28,GumpButtonType.Reply,0);
			AddButton(BodyX + 295,basey(BodyY,6)+100,2464,2465,29,GumpButtonType.Reply,0);

			
		}
		private void buildOOC()
		{
			this.AddToggleLabeled(basex(TitleX),basey(TitleY,0),9,"Use OOC Filter",JailSystem.useOOCFilter);
			message();
			if (!JailSystem.useOOCFilter) return;
			AddButton(TitleX + 50,TitleY + 75,239,240,10,GumpButtonType.Reply,0);
			//AddLabel(basex( BodyX),basey(BodyY,0),200,"Commands");
			AddLabel(basex( BodyX)+15,basey(BodyY,0),200,"OOCList");
			this.AddTextField(basex( BodyX)+90,basey(BodyY,0),130,20,11,JailSystem.ooclistCommand);
			//AddLabel(basex( BodyX),basey(BodyY,2),200,"Misc.");
			AddLabel(basex( BodyX)+15,basey(BodyY,1),200,"OOC Jailor");
			this.AddTextField(basex( BodyX)+90,basey(BodyY,1),130,20,12,JailSystem.oocJailorName);
			this.AddToggleLabeled(basex( BodyX)+15,basey(BodyY,2),11,"Block OOC speech",JailSystem.blockOOCSpeech);
			this.AddToggleLabeled(basex( BodyX)+15,basey(BodyY,3),12,"Allow Staff to go OOC",JailSystem.AllowStaffOOC);
			AddLabel(basex( BodyX)+15,basey(BodyY,4),200,"OOC Warnings");
			this.AddTextField(basex( BodyX)+120,basey(BodyY,4),50,20,13,JailSystem.oocwarns.ToString());
			
			AddLabel(basex( BodyX),basey(BodyY,5),200,"OOC Parts");
			string temp="";
			foreach (string p in JailSystem.oocParts)
				temp+= string.Format("{0}\n",p.ToString());
			this.AddColorLabelScroll(basex(BodyX),basey(BodyY,6),150,60,temp.Trim());

			this.AddTextField(basex(BodyX),basey(BodyY,6)+65,150,20,15);
			AddButton(basex(BodyX),basey(BodyY,6)+90,2461,2462,34,GumpButtonType.Reply,0);
			AddButton(basex(BodyX)+55 ,basey(BodyY,6)+90,2464,2465,35,GumpButtonType.Reply,0);

			AddLabel(basex( BodyX)+ 240,basey(BodyY,0),200,"OOC Words");
			temp="";
			foreach (string p in JailSystem.oocWords)
				temp+= string.Format("{0}\n",p.ToString());
			this.AddColorLabelScroll(basex(BodyX)+240,basey(BodyY,1),150,60,temp.Trim());

			this.AddTextField(basex(BodyX)+240,basey(BodyY,1)+65,150,20,14);
			AddButton(BodyX + 240,basey(BodyY,1)+90,2461,2462,32,GumpButtonType.Reply,0);
			AddButton(BodyX + 295,basey(BodyY,1)+90,2464,2465,33,GumpButtonType.Reply,0);

			AddLabel(basex( BodyX)+ 240,basey(BodyY,5)+10,200,"Jail Terms");
			temp="";
			foreach (TimeSpan t in JailSystem.FoulMouthJailTimes)
				temp+= string.Format("d={0} h={1} m={2}\n",t.Days, t.Hours, t.Minutes);
			this.AddColorLabelScroll(basex(BodyX)+240,basey(BodyY,6)+10,150,60,temp.Trim());

			AddLabel(basex(BodyX)+240,basey(BodyY,6)+75,200,"D");
			AddLabel(basex(BodyX)+290,basey(BodyY,6)+75,200,"H");
			AddLabel(basex(BodyX)+340,basey(BodyY,6)+75,200,"M");
			this.AddTextField(basex(BodyX)+255,basey(BodyY,6)+75,30,20,8);
			this.AddTextField(basex(BodyX)+305,basey(BodyY,6)+75,30,20,9);
			this.AddTextField(basex(BodyX)+355,basey(BodyY,6)+75,30,20,10);
			AddButton(BodyX + 240,basey(BodyY,6)+100,2461,2462,30,GumpButtonType.Reply,0);
			AddButton(BodyX + 295,basey(BodyY,6)+100,2464,2465,31,GumpButtonType.Reply,0);

		}
		private void buildSettings()
		{
			message();
			AddButton(TitleX + 50,TitleY + 75,239,240,6,GumpButtonType.Reply,0);
			
			AddLabel(basex( BodyX),basey(BodyY,0),200,"Commands");
			AddLabel(basex( BodyX)+15,basey(BodyY,1),200,"Status");
			AddLabel(basex( BodyX)+15,basey(BodyY,2),200,"Time");
			this.AddTextField(basex( BodyX)+60,basey(BodyY,1),150,20,1,JailSystem.statusCommand);
			this.AddTextField(basex( BodyX)+60,basey(BodyY,2),150,20,2,JailSystem.timeCommand);

			AddLabel(basex( BodyX),basey(BodyY,3),200,"Misc.");
			AddLabel(basex( BodyX)+15,basey(BodyY,4),200,"Name");
			this.AddTextField(basex( BodyX)+60,basey(BodyY,4),150,20,3,JailSystem.JSName);
			AddLabel(basex( BodyX)+65,basey(BodyY,5),200,string.Format("Jail Facet:{0}",JailSystem.jailMap.Name ));
			AddButton(basex(BodyX)+15,basey(BodyY,5),2471,2470,20,GumpButtonType.Reply,0);
			this.AddToggleLabeled(basex(BodyX)+15,basey(BodyY,6),7,"Use Smoking Shoes",JailSystem.useSmokingFootGear);
			
			AddLabel(basex( BodyX),basey(BodyY,7),200,"Non-Default Release Setting");
			this.AddToggleLabeled(basex(BodyX)+15,basey(BodyY,8),8,"Single Facet release",JailSystem.SingleFacetOnly);
			AddLabel(basex( BodyX)+65,basey(BodyY,9)+10,200,string.Format("Release Facet:{0}",JailSystem.defaultReleaseFacet.Name ));
			AddButton(BodyX + 15,basey(BodyY,9)+10,2471,2470,21,GumpButtonType.Reply,0);
			
			AddLabel(basex( BodyX)+ 240,basey(BodyY,0),200,"Cells");
			string temp="";
			foreach (Point3D p in JailSystem.cells)
				temp+= p.ToString() + "\n";
			this.AddColorLabelScroll(basex(BodyX)+240,basey(BodyY,1),150,60,temp.Trim());

			this.AddTextField(basex(BodyX)+240,basey(BodyY,1)+65,45,20,5);
			this.AddTextField(basex(BodyX)+290,basey(BodyY,1)+65,45,20,6);
			this.AddTextField(basex(BodyX)+340,basey(BodyY,1)+65,45,20,7);
			AddButton(BodyX + 240,basey(BodyY,1)+90,2461,2462,22,GumpButtonType.Reply,0);
			AddButton(BodyX + 295,basey(BodyY,1)+90,2464,2465,23,GumpButtonType.Reply,0);

			AddLabel(basex( BodyX)+ 240,basey(BodyY,5)+10,200,"Default Release Loctions");
			temp="";
			foreach (Point3D p in JailSystem.defaultRelease)
				temp+= p.ToString() + "\n";
			this.AddColorLabelScroll(basex(BodyX)+240,basey(BodyY,6)+10,150,60,temp.Trim());

			this.AddTextField(basex(BodyX)+240,basey(BodyY,6)+75,45,20,8);
			this.AddTextField(basex(BodyX)+290,basey(BodyY,6)+75,45,20,9);
			this.AddTextField(basex(BodyX)+340,basey(BodyY,6)+75,45,20,10);
			AddButton(BodyX + 240,basey(BodyY,6)+100,2461,2462,24,GumpButtonType.Reply,0);
			AddButton(BodyX + 295,basey(BodyY,6)+100,2464,2465,25,GumpButtonType.Reply,0);
		}
		private void buildReviews()
		{			
			if(JailSystem.list.Count < m_id) m_id=0;
			if(m_id<0) m_id=JailSystem.list.Count-1;
			if(JailSystem.list.Count==0) m_id=-1;
			int i=0;
			if (m_id>=0)
				foreach(JailSystem tj in JailSystem.list.Values)
				{
					if ((i==0)||(i==m_id))
						js=tj;
					i++;
				}
			if(m_id==-1)
			{
				AddLabel( BodyX + gutterOffset, BodyY + gutterOffset, 200,"No accounts are currently jailed.");
				return;
			}
			AddLabel( TitleX + gutterOffset, TitleY + gutterOffset, 200,"Reviewing: " + js.Name);
			AddLabel( TitleX + gutterOffset, TitleY + gutterOffset + LineStep, 200,string.Format("Jailed Account {0} of {1}",m_id + 1,JailSystem.list.Count));
			//previous button
			AddButton( TitleX + gutterOffset, TitleY + gutterOffset + LineStep + LineStep +5, 2466, 2467, 44, GumpButtonType.Reply, 0 );
			//next Button
			AddButton( TitleX + gutterOffset + 80, TitleY + gutterOffset + LineStep + LineStep +5, 2469, 2470, 45, GumpButtonType.Reply, 0 );
			string temp="";
			if (js.Prisoner==null)
				js.killJail();
			else
			{
				foreach (AccountComment note in js.Prisoner.Comments)
				{
					if((note.AddedBy==JailSystem.JSName + "-warning")||(note.AddedBy==JailSystem.JSName + "-jailed")||(note.AddedBy==JailSystem.JSName + "-note"))
					{
						temp=temp + note.AddedBy + "\n\r" + note.Content + "\n\r***********\n\r";
					}
				}
				AddLabel( BodyX + 17, BodyY + 8, 200,"History");
				AddHtml(BodyX + 13,141,300,82,temp,true,true);
				//release
				AddButton( BodyX + 13, BodyY + 120, 2472, 2473, 41, GumpButtonType.Reply, 0 );
				AddLabel( BodyX + 43, BodyY + 123, 200, "Release" );
				AddButton( BodyX + 13, BodyY + 150, 2472, 2473, 50, GumpButtonType.Reply, 0 );
				AddLabel( BodyX + 43, BodyY + 153, 200, "Ban" );
				//add day
				AddButton( BodyX + 101, BodyY + 120, 250, 251, 43, GumpButtonType.Reply, 0 );
				AddButton( BodyX+ 116, BodyY + 120, 252, 253, 47, GumpButtonType.Reply, 0 );
				AddLabel( 135 + BodyX, BodyY + 123, 200, "Week" );
				//add week
				AddButton( BodyX+176, BodyY + 120, 250, 251, 42, GumpButtonType.Reply, 0 );
				AddButton( BodyX+ 191, BodyY + 120, 252, 253, 46, GumpButtonType.Reply, 0 );
				AddLabel( BodyX+ 210, BodyY + 123, 200, "Day" );
				//hours
				AddButton( BodyX+251, BodyY + 120, 250, 251, 48, GumpButtonType.Reply, 0 );
				AddButton( BodyX+266, BodyY + 120, 252, 253, 49, GumpButtonType.Reply, 0 );
				AddLabel( BodyX+284, BodyY + 123, 200, "Hour" );

				AddLabel( BodyX+13, BodyY + 170, 200,"Release at: " + js.ReleaseDate.ToString());
				if (!js.jailed)
				{
					message("This account has been released but currently has characters in jail.");
				}
				else
				{
					message("This account is currently jailed.");
				}
				AddHtml(BodyX+13,BodyY + 189,300,74,js.reason,true,true);
			}
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from=sender.Mobile;
			string temp="";
			switch ( info.ButtonID )
			{
				case 50:
					//ban an account;
					from.SendGump(new JailBanGump(js));
					break;
				case 10:
					temp=info.GetTextEntry(11).Text.ToString().Trim().ToLower();
					if (!(temp=="")&&!(temp==null))
						JailSystem.ooclistCommand=temp;
					temp=info.GetTextEntry(12).Text.ToString().Trim();
					if (!(temp=="")&&!(temp==null))
						JailSystem.oocJailorName=temp;

					temp=info.GetTextEntry(13).Text.ToString().Trim().ToLower();
					if (!(temp=="")&&!(temp==null))
						try
						{
							JailSystem.oocwarns=Convert.ToInt32(temp);
						}
						catch
						{
							from.SendMessage("Bad number of OOC Warnings.");
						}
					goto case 2;
				case 11:
					JailSystem.blockOOCSpeech=!JailSystem.blockOOCSpeech;
					goto case 10;
				case 12:
					JailSystem.AllowStaffOOC=!JailSystem.AllowStaffOOC;
					goto case 10;
				case 15:
					//language section
					temp=info.GetTextEntry(12).Text.ToString().Trim();
					if (!(temp=="")&&!(temp==null))
						JailSystem.foulJailorName=temp;
					JailSystem.FoulMouthJailTimes.Sort();
					goto case 3;
				case 13:
					JailSystem.useLanguageFilter=!JailSystem.useLanguageFilter;
					goto case 3;
				case 14:
					JailSystem.allowStaffBadWords=!JailSystem.allowStaffBadWords;
					goto case 15;		
				case 9:
					JailSystem.useOOCFilter= !JailSystem.useOOCFilter;
					goto case 2;
					//generenal section
				case 1:
					from.SendGump(new JailAdminGump(AdminJailGumpPage.General));
					break;
				case 2:
					from.SendGump(new JailAdminGump(AdminJailGumpPage.OOC));
					break;
				case 3:
					from.SendGump(new JailAdminGump(AdminJailGumpPage.Language));
					break;
				case 4:
					from.SendGump(new JailAdminGump(AdminJailGumpPage.Review));
					break;
				case 5:
					from.CloseGump(typeof ( JailAdminGump ));
					break;
				case 6:
					temp=info.GetTextEntry(1).Text.ToString().Trim().ToLower();
					if (!(temp=="")&&!(temp==null))
						JailSystem.statusCommand=temp;

					temp=info.GetTextEntry(2).Text.ToString().Trim().ToLower();
					if (!(temp=="")&&!(temp==null))
						JailSystem.timeCommand=temp;

					temp=info.GetTextEntry(3).Text.ToString().Trim();
					if (!(temp=="")&&!(temp==null))
						JailSystem.JSName=temp;
					goto case 1;
				case 7:
					JailSystem.useSmokingFootGear= !JailSystem.useSmokingFootGear;
					goto case 6;
				case 8:
					JailSystem.SingleFacetOnly= !JailSystem.SingleFacetOnly;
					goto case 6;
				case 20:
					if (JailSystem.jailMap==Map.Felucca)
						JailSystem.jailMap=Map.Trammel;
					else if (JailSystem.jailMap== Map.Trammel)
						JailSystem.jailMap=Map.Ilshenar;
					else if (JailSystem.jailMap==Map.Ilshenar)
						JailSystem.jailMap=Map.Malas;
					else if (JailSystem.jailMap==Map.Malas)
						JailSystem.jailMap=Map.Felucca;
					goto case 6;
				case 21:
					if (JailSystem.defaultReleaseFacet==Map.Felucca)
						JailSystem.defaultReleaseFacet=Map.Trammel;
					else if (JailSystem.defaultReleaseFacet== Map.Trammel)
						JailSystem.defaultReleaseFacet=Map.Ilshenar;
					else if (JailSystem.defaultReleaseFacet== Map.Ilshenar)
						JailSystem.defaultReleaseFacet=Map.Malas;
					else if (JailSystem.defaultReleaseFacet== Map.Malas)
						JailSystem.defaultReleaseFacet=Map.Felucca;
					//change facet
					goto case 6;
				case 22:
					//add cell
					try
					{
						Point3D p=new Point3D(Convert.ToInt32(info.GetTextEntry(5).Text.Trim()),Convert.ToInt32(info.GetTextEntry(6).Text.Trim()),Convert.ToInt32(info.GetTextEntry(7).Text.Trim()));
						if (JailSystem.cells.Contains(p))
							from.SendMessage("Unable to add jail cell. It is already listed.");
						else
							JailSystem.cells.Add(p);
					}
					catch
					{
						from.SendMessage("Unable to add jail cell. Bad x,y,z.");
					}
					goto case 6;
				case 23:
					//remove cell
					try
					{
						Point3D p=new Point3D(Convert.ToInt32(info.GetTextEntry(5).Text.Trim()),Convert.ToInt32(info.GetTextEntry(6).Text.Trim()),Convert.ToInt32(info.GetTextEntry(7).Text.Trim()));
						if (JailSystem.cells.Contains(p))
							JailSystem.cells.Remove(p);
						else
							from.SendMessage("Unable to remove jail cell. Cell not listed.");
					}
					catch
					{
						from.SendMessage("Unable to remove jail cell. Bad x,y,z.");
					}
					goto case 6;
				case 24:
					//add release
					try
					{
						Point3D p=new Point3D(Convert.ToInt32(info.GetTextEntry(8).Text.Trim()),Convert.ToInt32(info.GetTextEntry(9).Text.Trim()),Convert.ToInt32(info.GetTextEntry(10).Text.Trim()));
						if (JailSystem.defaultRelease.Contains(p))
							from.SendMessage("Unable to add default release location. It is already listed.");
						else
							JailSystem.defaultRelease.Add(p);
					}
					catch
					{
						from.SendMessage("Unable to add release location. Bad x,y,z.");
					}
					goto case 6;
				case 25:
					//remove release
					try
					{
						Point3D p=new Point3D(Convert.ToInt32(info.GetTextEntry(8).Text.Trim()),Convert.ToInt32(info.GetTextEntry(9).Text.Trim()),Convert.ToInt32(info.GetTextEntry(10).Text.Trim()));
						if (JailSystem.defaultRelease.Contains(p))
							JailSystem.defaultRelease.Remove(p);
						else
							from.SendMessage("Release location not listed.");
					}
					catch
					{
						from.SendMessage("Unable to remove release location. Bad x,y,z.");
					}
					goto case 6;
				case 26:
					//add foul word
					try
					{
						temp=info.GetTextEntry(13).Text.ToLower().Trim();
						if ((temp=="")||(temp==null))
							from.SendMessage("Unable to add word");
						else if (JailSystem.badWords.Contains(temp))
							from.SendMessage("Word is already in the list.");
						else
							JailSystem.badWords.Add(temp);
					}
					catch
					{
						from.SendMessage("Unable to add word");
					}
					goto case 15;
				case 27:
					//remove foul word
					try
					{
						temp=info.GetTextEntry(13).Text.ToLower().Trim();
						if ((temp=="")||(temp==null))
							from.SendMessage("Unable to remove word");
						else if (JailSystem.badWords.Contains(temp))
							JailSystem.badWords.Remove(temp);
						else
							from.SendMessage("Word is not in the list.");
					}
					catch
					{
						from.SendMessage("Unable to remove word");
					}
					goto case 15;
				case 28:
					//add jail term
					try
					{
						TimeSpan p=new TimeSpan(Convert.ToInt32(info.GetTextEntry(8).Text.Trim()),Convert.ToInt32(info.GetTextEntry(9).Text.Trim()),Convert.ToInt32(info.GetTextEntry(10).Text.Trim()),0,0);
						if (JailSystem.FoulMouthJailTimes.Contains(p))
							from.SendMessage("Unable to add jail term. It is already listed.");
						else
							JailSystem.FoulMouthJailTimes.Add(p);
					}
					catch
					{
						from.SendMessage("Unable to add jail term. Bad D,H,M.");
					}
					goto case 15;
				case 29:
					//remove jail term
					try
					{
						TimeSpan p=new TimeSpan(Convert.ToInt32(info.GetTextEntry(8).Text.Trim()),Convert.ToInt32(info.GetTextEntry(9).Text.Trim()),Convert.ToInt32(info.GetTextEntry(10).Text.Trim()),0,0);
						if (JailSystem.FoulMouthJailTimes.Contains(p))
							JailSystem.FoulMouthJailTimes.Remove(p);
						else
							from.SendMessage("Jail term not listed.");
					}
					catch
					{
						from.SendMessage("Unable to remove Jail term. Bad D,H,M.");
					}
					goto case 15;

				case 30:
					//add jail term
					try
					{
						TimeSpan p=new TimeSpan(Convert.ToInt32(info.GetTextEntry(8).Text.Trim()),Convert.ToInt32(info.GetTextEntry(9).Text.Trim()),Convert.ToInt32(info.GetTextEntry(10).Text.Trim()),0,0);
						if (JailSystem.oocJailTimes.Contains(p))
							from.SendMessage("Unable to add jail term. It is already listed.");
						else
							JailSystem.oocJailTimes.Add(p);
					}
					catch
					{
						from.SendMessage("Unable to add jail term. Bad D,H,M.");
					}
					goto case 10;
				case 31:
					//remove jail term
					try
					{
						TimeSpan p=new TimeSpan(Convert.ToInt32(info.GetTextEntry(8).Text.Trim()),Convert.ToInt32(info.GetTextEntry(9).Text.Trim()),Convert.ToInt32(info.GetTextEntry(10).Text.Trim()),0,0);
						if (JailSystem.oocJailTimes.Contains(p))
							JailSystem.oocJailTimes.Remove(p);
						else
							from.SendMessage("Jail term not listed.");
					}
					catch
					{
						from.SendMessage("Unable to remove Jail term. Bad D,H,M.");
					}
					goto case 10;
				case 32:
					//add ooc word
					try
					{
						temp=info.GetTextEntry(14).Text.ToLower().Trim();
						if ((temp=="")||(temp==null))
							from.SendMessage("Unable to add word");
						else if (JailSystem.oocWords.Contains(temp))
							from.SendMessage("Word is already in the list.");
						else
							JailSystem.oocWords.Add(temp);
					}
					catch
					{
						from.SendMessage("Unable to add word");
					}
					goto case 10;
				case 33:
					//remove ooc word
					try
					{
						temp=info.GetTextEntry(14).Text.ToLower().Trim();
						if ((temp=="")||(temp==null))
							from.SendMessage("Unable to remove word");
						else if (JailSystem.oocWords.Contains(temp))
							JailSystem.oocWords.Remove(temp);
						else
							from.SendMessage("Word is not in the list.");
					}
					catch
					{
						from.SendMessage("Unable to remove word");
					}
					goto case 10;
				case 34:
					//add ooc part
					try
					{
						temp=info.GetTextEntry(15).Text.ToLower().Trim();
						if ((temp=="")||(temp==null))
							from.SendMessage("Unable to add word");
						else if (JailSystem.oocParts.Contains(temp))
							from.SendMessage("Word is already in the list.");
						else
							JailSystem.oocParts.Add(temp);
					}
					catch
					{
						from.SendMessage("Unable to add word");
					}
					goto case 10;
				case 35:
					//remove ooc part
					try
					{
						temp=info.GetTextEntry(15).Text.ToLower().Trim();
						if ((temp=="")||(temp==null))
							from.SendMessage("Unable to remove word");
						else if (JailSystem.oocParts.Contains(temp))
							JailSystem.oocParts.Remove(temp);
						else
							from.SendMessage("Word is not in the list.");
					}
					catch
					{
						from.SendMessage("Unable to remove word");
					}
					goto case 10;
				case 41:
					js.forceRelease(from);
					from.SendGump(new JailAdminGump(m_page, m_subpage, m_id));
					break;
				case 42:
					js.AddDays(1);
					from.SendGump(new JailAdminGump(m_page, m_subpage, m_id));
					break;
				case 46:
					js.subtractDays(1);
					from.SendGump(new JailAdminGump(m_page, m_subpage, m_id));
					break;
				case 47:
					js.subtractDays(7);
					from.SendGump(new JailAdminGump(m_page, m_subpage, m_id));
					break;
				case 48:
					js.AddHours(1);
					from.SendGump(new JailAdminGump(m_page, m_subpage, m_id));
					break;
				case 49:
					js.subtractHours(1);
					from.SendGump(new JailAdminGump(m_page, m_subpage, m_id));
					break;
				case 43:
					js.AddDays(7);
					from.SendGump(new JailAdminGump(m_page, m_subpage, m_id));
					break;
				case 44:
					//previous button
					m_id--;
					if (m_id<0) m_id=JailSystem.list.Count-1;
					from.SendGump(new JailAdminGump(m_page, m_subpage, m_id));
					break;
				case 45:
					//next button
					m_id++;
					if (m_id>=JailSystem.list.Count) m_id=0;
					from.SendGump(new JailAdminGump(m_page, m_subpage, m_id));
					break;
				default:
					break;
			}
			//from.CloseGump(typeof ( JailAdminGump ));
		}
	}
	#endregion
	#region context menu objects
	public class ReviewEntry : ContextMenuEntry
	{
		private Mobile m_gm;
		private Mobile m_player;

		public ReviewEntry( Mobile gm, Mobile player ) : base( 10004, 200 )
		{
			m_gm=gm;
			m_player=player;
		}
		public override void OnClick()
		{
			m_gm.SendGump(new jailReviewGump(m_gm,m_player,0,null));
		}
	}
	public class JailEntry : ContextMenuEntry
	{
		private Mobile m_gm;
		private Mobile m_player;

		public JailEntry( Mobile gm, Mobile player ) : base( 5008, 200 )
		{
			m_gm=gm;
			m_player=player;
		}
		public override void OnClick()
		{
			JailSystem.newJailingFromGMandPlayer(m_gm, m_player);	
			//this is where we jail them
		}
	}
	public class unJailEntry : ContextMenuEntry
	{
		private Mobile m_gm;
		private Mobile m_player;
		private JailSystem js;
		public unJailEntry( Mobile gm, Mobile player ) : base( 5135, 200 )
		{
			m_gm=gm;
			m_player=player;	
			js=JailSystem.fromMobile(m_player);
			if ( js==null )
				Flags |= Network.CMEFlags.Disabled;
			else if (!js.jailed )
				Flags |= Network.CMEFlags.Disabled;
		}
		public override void OnClick()
		{		
			if(js==null)
				m_gm.SendMessage("They are not jailed");
			else if(js.jailed)
				js.forceRelease(m_gm);
		}
	}

	public class macroerEntry : ContextMenuEntry
	{
		private Mobile m_gm;
		private Mobile m_player;
		private JailSystem js;
		public macroerEntry( Mobile gm, Mobile player ) : base( 394, 200 )
		{
			m_gm=gm;
			m_player=player;	
			js=JailSystem.fromMobile(m_player);
			if ( js==null )
			{}
			else if (js.jailed )
				Flags |= Network.CMEFlags.Disabled;
		}
		public override void OnClick()
		{		
			if(js==null)
				JailSystem.macroTest(m_gm,m_player);
			else if(!js.jailed)
				JailSystem.macroTest(m_gm,m_player);
			else
				m_gm.SendMessage("They are already in jail.");
		}
	}
	#endregion
}

namespace Server.Items
{
	#region smoking boots
	public class smokingFootGear : Item
	{
		const int defaultShoes=5899;
		public smokingFootGear():base(defaultShoes)
		{	
			Name="Smoking boots";
		}
		public new TimeSpan DecayTime
		{
			get {return TimeSpan.FromSeconds(5);}
		}
		public new bool Movable 
		{
			get {return false;}
		}
		public new bool Decays
		{
			get{return true;}
		}
		public smokingFootGear(Mobile m) : base(findFootGear(m))
		{
			Name=m.Name + "'s smoking boots";
			MoveToWorld(m.Location, m.Map);
			new sTimer(this);
		}
		static public int findFootGear(Mobile m)
		{
			try
			{
				foreach (Item i in m.Items)
				{
					if (i is Server.Items.BaseShoes)
					{
						if (i.Parent.Equals(m))
							//return i.ItemID;
							return defaultShoes;
					}
				}
				return defaultShoes;
			}
			catch
			{
				m.SendMessage("Flying Monkeys ate your shoes");
				return defaultShoes;
			}
		}
		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
		}
		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 
		}
		public smokingFootGear(Serial s) :base(s)
		{
		}
		private class sTimer : Timer
		{
			smokingFootGear footwear;
			int tI=1;
			public sTimer(smokingFootGear i):base(TimeSpan.FromSeconds(1),TimeSpan.FromSeconds(5))
			{
				footwear=i;
				this.Start();
			}
			protected override void OnTick()
			{
				if(tI==1)
				{
					Point3D p=new Point3D(footwear.Location.X,footwear.Location.Y,footwear.Location.Z+2);
					Effects.SendLocationEffect(p, footwear.Map,0x3735, 30);
					Effects.PlaySound(p,footwear.Map,0x5C);
					tI++;
				}
				else
				{
					Effects.SendLocationEffect(footwear.Location, footwear.Map,0x36BD, 10);
					Effects.PlaySound(footwear.Location,footwear.Map,0x307);
					this.Stop();
					footwear.Delete();
				}
			}
		}
	}
	#endregion
	#region cage
	public class aCage : Item
	{
		private ArrayList m_Components;
		private class cagePart : Item
		{
			private aCage m_parent;
			public override int LabelNumber{ get{ return 1016152; } } // a tree ornament

			public cagePart( int itemID, aCage parent ) : base( itemID )
			{
				m_parent=parent;
				Movable = false;
			}
			public override void OnDelete()
			{
				if(m_parent !=null)
				{
					if (!m_parent.Deleted)
						m_parent.Delete();
				}
				base.OnDelete();
			}
			public cagePart( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( (int) 1 ); // version
				writer.Write((Item)m_parent);
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();
				switch (version)
				{
					case 1:
						m_parent=(aCage)reader.ReadItem();
						break;
					default:
						break;
				}
			}
		}

		private HoldingCell cellblock;

		public aCage( Mobile from ) : base( 1180 )
		{
			this.Movable=false;
			MoveToWorld( from.Location, from.Map );
			m_Components = new ArrayList();
			cellblock=new HoldingCell(from.Location.X, from.Location.Y,from.Map);
			AddItem( from, 1, 0, 0, new cagePart( 1180,this ),true );// right upper
			AddItem( from, 1, 1, 0, new cagePart( 1180,this ),true );// right lower
			AddItem( from, 0, 1, 0, new cagePart( 1180,this ),true );// left lower

			AddItem( from, 1, 1, 0, new cagePart( 2082,this ),true );// right lower
			AddItem( from, 1, 0, 0, new cagePart( 2081,this ),true );//right center
			AddItem( from, 1, -1, 0, new cagePart( 2083,this ),true );//right upper
			AddItem( from, -1, 1, 0, new cagePart( 2081,this ),true );//left lower 
			AddItem( from, -1, 0, 0, new cagePart( 2081,this ),true );//left center
			//AddItem( from, -1, -1, 0, new cagePart( 2083 ),true );//left upper
			AddItem( from, 0, 1, 0, new cagePart( 2083,this ),true );//center lower
			AddItem( from, 0, -1, 0, new cagePart( 2083,this ),true );//center upper
		}
		private void AddItem( Mobile from, int x, int y, int z, Item item, bool vis )
		{
			item.Visible=vis;
			item.MoveToWorld( new Point3D( from.Location.X + x, from.Location.Y + y, from.Location.Z + z ), from.Map );

			m_Components.Add( item );
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (int) m_Components.Count );

			for ( int i = 0; i < m_Components.Count; ++i )
				writer.Write( (Item)m_Components[i] );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				default:
				{
					int count = reader.ReadInt();

					m_Components = new ArrayList( count );

					for ( int i = 0; i < count; ++i )
					{
						Item item = reader.ReadItem();

						if ( item != null )
							m_Components.Add( item );
					}

					break;
				}
			}
			cellblock=new HoldingCell(this.X, this.Y,this.Map);
		}
		public override void OnAfterDelete()
        {
            foreach (Item item in m_Components)
            {
                if ((item != null) && (!item.Deleted))
                    item.Delete();
            }
            cellblock.Unregister();
        }
		public aCage( Serial serial ) : base( serial )
		{
		}
	}
	#endregion
}
namespace Server.Regions
{
	public class HoldingCell : Region
	{
        public HoldingCell(int x, int y, Map map)
            : base("a Holding Cell", map, 100, new Rectangle2D(x - 1, y - 1, 4, 4)) 
        {
            GoLocation = new Point3D(x, y, map.GetAverageZ(x, y));
            this.Register();
        }
		public override bool AllowBeneficial( Mobile from, Mobile target )
		{
				from.SendMessage( "You may not do that in a holding cell." );

			return ( from.AccessLevel > AccessLevel.Player );
		}

		public override bool AllowHarmful( Mobile from, Mobile target )
		{
				from.SendMessage( "You may not do that in a holding cell." );

			return ( from.AccessLevel > AccessLevel.Player );
		}

		public override bool AllowHousing( Mobile from, Point3D p )
		{
			return false;
		}

		public override void AlterLightLevel( Mobile m, ref int global, ref int personal )
		{
			global = LightCycle.JailLevel;
		}

		public override bool OnBeginSpellCast( Mobile from, ISpell s )
		{
				from.SendLocalizedMessage( 502629 ); // You cannot cast spells here.

			return ( from.AccessLevel > AccessLevel.Player );
		}

		public override bool OnSkillUse( Mobile from, int Skill )
		{
			from.SendMessage( "You may not use skills in a holding cell." );

			return ( from.AccessLevel > AccessLevel.Player );
		}

		public override bool OnCombatantChange( Mobile from, Mobile Old, Mobile New )
		{
			return ( from.AccessLevel > AccessLevel.Player );
		}
		public override void OnEnter( Mobile m )
		{
			if ( m.AccessLevel > AccessLevel.Player )
				m.SendMessage( "You have entered a holding cell." );
		}

		public override void OnExit( Mobile m )
		{
			if ( m.AccessLevel > AccessLevel.Player )
				m.SendMessage( "You have left a holding cell." );
		}
	}
}

