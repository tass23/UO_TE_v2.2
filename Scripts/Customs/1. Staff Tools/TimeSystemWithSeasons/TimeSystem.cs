/*******************************************
*               Time System
* 
* Version        : 1.1.12
* By             : Morxeton
* 
* Date Created   : February 8, 2006
* Date Modified  : February 18, 2006
* 
*******************************************/

using System;
using System.Collections;
using System.Text;
using System.IO;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Commands; //added for seasons
using System.Net;
using Server.Accounting;
using Server.Menus;
using Server.Menus.Questions;
using Server.Menus.ItemLists;
using Server.Spells;
using Server.Targeting;
using Server.Targets;
using Server.Gumps;

namespace Server.TimeSystem
{
	public class System
	{
		#region Enums

		// enum Variable:  Do not change anything!  This is used for the SetVariable() method.
		public enum Variable
		{
			None,
			Defaults,
			DayLevel,
			NightLevel,
			DarkestHourLevel,
			LightsOnLevel,
			UseTimeZones,
			TimeZoneXDivisor,
			UseRealTime,
			UseAutoLighting,
			RandomLightOutage,
			LightOutageChancePerTick,
			MinTotalLightsTogglePercent,
			MaxTotalLightsTogglePercent,
			TimerSpeed,
			MinutesPerTick,
			MinutesPerHour,
			HoursPerDay,
			DaysPerMonth,
			MonthsPerYear,
			NightStartHour,
			DayStartHour,
			ScaleTimeMinutes,
			DarkestHourEnabled,
			DarkestHourMinutesAfterNight,
			DarkestHourScaleTimeMinutes,
			DarkestHourMinutesLong,
			MoonPhaseTotalDays,
			MoonPhaseDay,
			MoonPhaseLevelAdjust,
			Year,
			Month,
			Day,
			Hour,
			Minute,
			TimeFormat,
			ClockTimeFormat
		};

		// enum MoonPhase:  You can add custom phases if you'd like, but be sure to keep the new and full
		// phases in respect to each other (i.e. currently seven phases lie in between the two).  New must
		// always be the first phase as well.  No other editting required for adding custom phases.
		public enum MoonPhase
		{
			New,
			WaxingCrescent,
			WaxingQuarter,
			WaxingThird,
			WaxingHalf,
			WaxingTwoThirds,
			WaxingThreeQuarters,
			WaxingGibbous,
			Full,
			WaningGibbous,
			WaningThreeQuarters,
			WaningTwoThirds,
			WaningHalf,
			WaningThird,
			WaningQuarter,
			WaningCrescent
		};

		#endregion

		#region Initialize

		public static void Initialize()
		{
			EventSink.Login += new LoginEventHandler(OnLogin);
			EventSink.WorldSave += new WorldSaveEventHandler(OnWorldSave);

			CommandSystem.Register("TS", AccessLevel.GameMaster, new CommandEventHandler(TimeSystem_OnCommand));
			CommandSystem.Register("BaseTime", AccessLevel.GameMaster, new CommandEventHandler(BaseTime_OnCommand));
			CommandSystem.Register("Time", AccessLevel.Player, new CommandEventHandler(Time_OnCommand));

			//Commands.Register("TS", AccessLevel.GameMaster, new CommandEventHandler(TimeSystem_OnCommand));
			//Commands.Register("BaseTime", AccessLevel.GameMaster, new CommandEventHandler(BaseTime_OnCommand));
			//Commands.Register("Time", AccessLevel.Player, new CommandEventHandler(Time_OnCommand));

			PopulateLightsList();

			Load();

			Start();

			m_TimeFormat = m_TimeFormat.Replace('$', Support.CodeChar);
			m_ClockTimeFormat = m_ClockTimeFormat.Replace('$', Support.CodeChar);

			Console.WriteLine(String.Format("Time System: {0}", GetTime(0, false)));
		}

		#endregion

		#region Constant Variables

		public const string Version = "1.1.12";

		public const int BinaryVersion = 2; // The version number used in the data file.

		private static readonly string m_DataDirectory = Path.Combine(Core.BaseDirectory, @"Data\Custom\Time System");
		private const string m_DataFileName = "Time System.dat";

		private const int m_MinLightLevel = 0; // Minimum light level.  Lower = brighter.  UO only supports 0-30.
		private const int m_MaxLightLevel = 100; // Maximum light level.  Higher = darker.

		private const int m_MinLightLevelDifference = 8; // Minimum light level difference allowed between day and night.
		private const int m_MinDarkestHourNightLevelDifference = 4; // Minimum light level different between night and darkest hour.

		private const int m_MinDayNightHoursDifference = 4; // Minimum hours difference allowed between starting day hour and starting night hour.

		private const double m_MinTimerValue = 0.5; // Minimum seconds allowed per timer tick.

		private static readonly Type[] m_ItemLightTypes = // BaseLight item types that will be toggled on/off for day/night.
		{
			typeof(LampPost1), typeof(LampPost2), typeof(LampPost3)
		};

		private const string m_CommandSet = "SET";
		private const string m_CommandGet = "GET";
		private const string m_CommandAppend = "APPEND";
		private const string m_CommandRepopLightsList = "REPOPLIGHTSLIST";
		private const string m_CommandStop = "STOP";
		private const string m_CommandStart = "START";
		private const string m_CommandRestart = "RESTART";
		private const string m_CommandLoad = "LOAD";
		private const string m_CommandSave = "SAVE";
		private const string m_CommandSetTime = "SETTIME";
		private const string m_CommandQuery = "QUERY";
		private const string m_CommandVersion = "VERSION";

		private static readonly string[] m_CommandsList =
		{
			m_CommandSet,
			m_CommandGet,
			m_CommandAppend,
			m_CommandRepopLightsList,
			m_CommandStop,
			m_CommandStart,
			m_CommandRestart,
			m_CommandLoad,
			m_CommandSave,
			m_CommandSetTime,
			m_CommandQuery,
			m_CommandVersion
		};

		private const string m_TimeFormatMoonPhase = "The moon is $mp$.";
		private const string m_TimeFormatPreset1 = "The time is $hr-ap$:$mn-2$ $ap$ on the $da$$nth-d$ of $mo-0$, in the year $yr$.";
		private const string m_TimeFormatPreset2 = "$mo$/$da$/$yr$ $hr-ap$:$mn-2$ $ap$.";
		private const string m_TimeFormatPreset3 = "$da$/$mo$/$yr$ $hr-ap$:$mn-2$ $ap$.";
		private const string m_TimeFormatPreset4 = "$mo-0$ $da$, $yr$ $hr-ap$:$mn-2$ $ap$.";
		private const string m_TimeFormatPreset5 = "$mo-3$ $da$, $yr$ $hr-ap$:$mn-2$ $ap$.";
		private const string m_TimeFormatPreset6 = "It is $hr-ap$:$mn-2$ $ap$ on the $da$$nth-d$ of $mo-0$, $yr$.";

		#endregion

		#region Private Variables

		private static TimeSystemTimer m_TimeSystemTimer;

		private static bool m_Enabled = true;

		private static int m_DayLevel = 0;
		private static int m_NightLevel = 20;
		private static int m_DarkestHourLevel = 30;
		private static int m_LightsOnLevel = 10;

		private static bool m_UseRealTime = false;

		private static bool m_UseTimeZones = true;
		private static int m_TimeZoneXDivisor = 16;

		// bool m_UseAutoLighting:  Specifies whether you would like to have all world items of a certain type of
		// light to be turned on/off depending on the light level.
		private static bool m_UseAutoLighting = true;
		private static bool m_RandomLightOutage = true;
		private static int m_LightOutageChancePerTick = 10;
		private static int m_MinTotalLightsTogglePercent = 10;
		private static int m_MaxTotalLightsTogglePercent = 25;

		private static double m_TimerSpeed = 5.0; // Seconds per UO minute
		private static int m_MinutesPerTick = 1; // Minutes added per tick of timer

		private static int m_MinutesPerHour = 60;
		private static int m_HoursPerDay = 24;
		private static int m_DaysPerMonth = 30;
		private static int m_MonthsPerYear = 12;

		private static int m_NightStartHour = 18;	//6pm
		private static int m_DayStartHour = 6;
		private static int m_ScaleTimeMinutes = 720; // Minutes it takes to scale from day/night or night/day

		private static bool m_DarkestHourEnabled = true;
		private static int m_DarkestHourMinutesAfterNight = 360; // How many minutes after nightfall until the darkest hour
		private static int m_DarkestHourScaleTimeMinutes = 10; // Minutes it takes to scale from night/darkest hour or darkest hour/night
		private static int m_DarkestHourMinutesLong = 60; // How many minutes will darkest hour last

		private static int m_MoonPhaseTotalDays = 16;
		private static int m_MoonPhaseDay = 1;
		private static int m_MoonPhaseLevelAdjust = 6;

		private static int m_Year = 100;
		private static int m_Month = 1;
		private static int m_Day = 1;
		private static int m_Hour = 4;
		private static int m_Minute = 0;

		private static string m_TimeFormat = String.Format("{0} {1}", m_TimeFormatPreset1, m_TimeFormatMoonPhase);
		private static string m_ClockTimeFormat = m_TimeFormatPreset1;

		private static ArrayList m_LightsList;

		private static bool m_LightsOn = false;
		private static bool m_StopRandomLightOutage = false;

		private static int m_CurrentLevel = 0;

		private static int m_LastRandomSeed = DateTime.Now.Millisecond;

		#endregion

		#region Public Variables

		public static bool Enabled { get { return m_Enabled; } }

		public static int DayLevel { get { return m_DayLevel; } }
		public static int NightLevel { get { return m_NightLevel; } }
		public static int DarkestHourLevel { get { return m_DarkestHourLevel; } }
		public static int LightsOnLevel { get { return m_LightsOnLevel; } }

		public static bool UseTimeZones { get { return m_UseTimeZones; } }
		public static int TimeZoneXDivisor { get { return m_TimeZoneXDivisor; } }

		public static int LightOutageChancePerTick { get { return m_LightOutageChancePerTick; } }
		public static int MinTotalLightsTogglePercent { get { return m_MinTotalLightsTogglePercent; } }
		public static int MaxTotalLightsTogglePercent { get { return m_MaxTotalLightsTogglePercent; } }

		public static double TimerSpeed { get { return m_TimerSpeed; } }
		public static int MinutesPerTick { get { return m_MinutesPerTick; } }

		public static int MinutesPerHour { get { return m_MinutesPerHour; } }
		public static int HoursPerDay { get { return m_HoursPerDay; } }
		public static int DaysPerMonth { get { return m_DaysPerMonth; } }
		public static int MonthsPerYear { get { return m_MonthsPerYear; } }

		public static int NightStartHour { get { return m_NightStartHour; } }
		public static int DayStartHour { get { return m_DayStartHour; } }
		public static int ScaleTimeMinutes { get { return m_ScaleTimeMinutes; } }

		public static bool DarkestHourEnabled { get { return m_DarkestHourEnabled; } }
		public static int DarkestHourMinutesAfterNight { get { return m_DarkestHourMinutesAfterNight; } }
		public static int DarkestHourScaleTimeMinutes { get { return m_DarkestHourScaleTimeMinutes; } }
		public static int DarkestHourMinutesLong { get { return m_DarkestHourMinutesLong; } }

		public static int MoonPhaseTotalDays { get { return m_MoonPhaseTotalDays; } }
		public static int MoonPhaseDay { get { return m_MoonPhaseDay; } }
		public static int MoonPhaseLevelAdjust { get { return m_MoonPhaseLevelAdjust; } }

		public static int Year { get { return m_Year; } }
		public static int Month { get { return m_Month; } }
		public static int Day { get { return m_Day; } }
		public static int Hour { get { return m_Hour; } }
		public static int Minute { get { return m_Minute; } }

		public static string TimeFormat { get { return m_TimeFormat; } }
		public static string ClockTimeFormat { get { return m_ClockTimeFormat; } }

		public static int CurrentLevel { get { return m_CurrentLevel; } }

		public static bool UseRealTime
		{
			get
			{
				return m_UseRealTime;
			}
			set
			{
				if (value && !m_UseRealTime)
				{
					m_TimerSpeed = 60.0;

					m_MoonPhaseTotalDays = 28;
				}
				else if (!value && m_UseRealTime)
				{
					m_TimerSpeed = 5.0;

					m_MoonPhaseTotalDays = 16;
				}

				m_UseRealTime = value;
			}
		}

		public static bool UseAutoLighting
		{
			get
			{
				return m_UseAutoLighting;
			}
			set
			{
				if (value && !m_UseAutoLighting)
				{
					PopulateLightsList();
				}
				else if (!value && m_UseAutoLighting)
				{
					m_LightsList = null;

					TurnOnAllWorldLights();
				}

				m_UseAutoLighting = value;
			}
		}

		public static bool RandomLightOutage
		{
			get
			{
				return m_RandomLightOutage;
			}
			set
			{
				if (!value && m_RandomLightOutage)
				{
					UpdateAllManagedLights();
				}

				m_RandomLightOutage = value;
			}
		}

		#endregion

		#region Calculated Variables

		private static string m_DataLocation
		{
			get
			{
				return Path.Combine(m_DataDirectory, m_DataFileName);
			}
		}

		private static int m_NightHours
		{
			get
			{
				if (m_NightStartHour > m_DayStartHour)
				{
					return (m_HoursPerDay - m_NightStartHour) + m_DayStartHour;
				}
				else
				{
					return m_DayStartHour - m_NightStartHour;
				}
			}
		}

		private static int m_DayHours
		{
			get
			{
				if (m_DayStartHour > m_NightStartHour)
				{
					return (m_HoursPerDay - m_DayStartHour) + m_NightStartHour;
				}
				else
				{
					return m_NightStartHour - m_DayStartHour;
				}
			}
		}

		private static int m_DarkestHourStartMinutes
		{
			get
			{
				return m_ScaleTimeMinutes + m_DarkestHourMinutesAfterNight;
			}
		}

		private static int m_DarkestHourTotalMinutes
		{
			get
			{
				return (m_DarkestHourMinutesLong + (m_DarkestHourScaleTimeMinutes * 2));
			}
		}

		private static int m_RandomSeed
		{
			get
			{
				int randomSeed = new Random(m_LastRandomSeed).Next();
				m_LastRandomSeed = new Random(randomSeed).Next();

				return randomSeed;
			}
		}

		#endregion

		#region EventSinks

		public static void OnLogin(LoginEventArgs args)
		{
			Mobile m = args.Mobile;

			m.CheckLightLevels(true);
		}

		public static void OnWorldSave(WorldSaveEventArgs e)
		{
			Save();
		}

		#endregion

		#region System Control

		public static void Stop()
		{
			m_Enabled = false;

			if (m_TimeSystemTimer != null)
			{
				m_TimeSystemTimer.Stop();
			}
		}

		public static void Start()
		{
			m_Enabled = true;

			m_TimeSystemTimer = new TimeSystemTimer();
			m_TimeSystemTimer.Start();
		}

		public static void Restart()
		{
			if (m_Enabled)
			{
				Stop();
				Start();
			}
		}

		#endregion

		#region Loading and Saving

		private static bool Load()
		{
			if (!CheckPaths())
			{
				Console.WriteLine(String.Format("Time System: \"{0}\" not found!  Creating a new file using the current settings.", m_DataFileName));

				Save();

				return false;
			}
			else
			{
				using (BinaryReader reader = new BinaryReader(File.Open(m_DataLocation, FileMode.Open)))
				{
					try
					{
						int version = reader.ReadInt32();

						if (version >= 1)
						{
							m_DayLevel = reader.ReadInt32();
							m_NightLevel = reader.ReadInt32();
							m_DarkestHourLevel = reader.ReadInt32();
							m_LightsOnLevel = reader.ReadInt32();

							m_UseRealTime = reader.ReadBoolean();

							m_UseAutoLighting = reader.ReadBoolean();
							m_RandomLightOutage = reader.ReadBoolean();
							m_LightOutageChancePerTick = reader.ReadInt32();
							m_MinTotalLightsTogglePercent = reader.ReadInt32();
							m_MaxTotalLightsTogglePercent = reader.ReadInt32();

							m_TimerSpeed = reader.ReadDouble();
							m_MinutesPerTick = reader.ReadInt32();

							m_MinutesPerHour = reader.ReadInt32();
							m_HoursPerDay = reader.ReadInt32();
							m_DaysPerMonth = reader.ReadInt32();
							m_MonthsPerYear = reader.ReadInt32();

							m_NightStartHour = reader.ReadInt32();
							m_DayStartHour = reader.ReadInt32();
							m_ScaleTimeMinutes = reader.ReadInt32();

							m_DarkestHourEnabled = reader.ReadBoolean();
							m_DarkestHourMinutesAfterNight = reader.ReadInt32();
							m_DarkestHourScaleTimeMinutes = reader.ReadInt32();
							m_DarkestHourMinutesLong = reader.ReadInt32();

							m_MoonPhaseTotalDays = reader.ReadInt32();
							m_MoonPhaseDay = reader.ReadInt32();
							m_MoonPhaseLevelAdjust = reader.ReadInt32();

							m_Year = reader.ReadInt32();
							m_Month = reader.ReadInt32();
							m_Day = reader.ReadInt32();
							m_Hour = reader.ReadInt32();
							m_Minute = reader.ReadInt32();
						}

						if (version >= 2)
						{
							m_Enabled = reader.ReadBoolean();

							m_UseTimeZones = reader.ReadBoolean();
							m_TimeZoneXDivisor = reader.ReadInt32();

							m_TimeFormat = reader.ReadString();
							m_ClockTimeFormat = reader.ReadString();
						}

						reader.Close();

						Console.WriteLine("Time System: Loading complete.");

						return true;
					}
					catch (EndOfStreamException)
					{
						reader.Close();

						Console.WriteLine(String.Format("Time System: \"{0}\" is corrupt.  Creating a new file using the current settings.", m_DataFileName));

						SetVariable("defaults", null, true, false);

						Save();

						return false;
					}
					catch (Exception e)
					{
						reader.Close();

						Console.WriteLine(String.Format("Time System: Unable to load data from file \"{0}\"!\r\n{1}", m_DataFileName, e.ToString()));

						SetVariable("defaults", null, true, false);

						Save();

						return false;
					}
				}
			}
		}

		private static bool Save()
		{
			CheckPaths();

			using (BinaryWriter writer = new BinaryWriter(File.Open(m_DataLocation, FileMode.Create)))
			{
				try
				{
					writer.Write(BinaryVersion);

					writer.Write(m_DayLevel);
					writer.Write(m_NightLevel);
					writer.Write(m_DarkestHourLevel);
					writer.Write(m_LightsOnLevel);

					writer.Write(m_UseRealTime);

					writer.Write(m_UseAutoLighting);
					writer.Write(m_RandomLightOutage);
					writer.Write(m_LightOutageChancePerTick);
					writer.Write(m_MinTotalLightsTogglePercent);
					writer.Write(m_MaxTotalLightsTogglePercent);

					writer.Write(m_TimerSpeed);
					writer.Write(m_MinutesPerTick);

					writer.Write(m_MinutesPerHour);
					writer.Write(m_HoursPerDay);
					writer.Write(m_DaysPerMonth);
					writer.Write(m_MonthsPerYear);

					writer.Write(m_NightStartHour);
					writer.Write(m_DayStartHour);
					writer.Write(m_ScaleTimeMinutes);

					writer.Write(m_DarkestHourEnabled);
					writer.Write(m_DarkestHourMinutesAfterNight);
					writer.Write(m_DarkestHourScaleTimeMinutes);
					writer.Write(m_DarkestHourMinutesLong);

					writer.Write(m_MoonPhaseTotalDays);
					writer.Write(m_MoonPhaseDay);
					writer.Write(m_MoonPhaseLevelAdjust);

					writer.Write(m_Year);
					writer.Write(m_Month);
					writer.Write(m_Day);
					writer.Write(m_Hour);
					writer.Write(m_Minute);

					writer.Write(m_Enabled);

					writer.Write(m_UseTimeZones);
					writer.Write(m_TimeZoneXDivisor);

					writer.Write(m_TimeFormat);
					writer.Write(m_ClockTimeFormat);

					writer.Close();

					Console.WriteLine("Time System: Saving complete.");

					return true;
				}
				catch (Exception e)
				{
					writer.Close();

					Console.WriteLine(String.Format("Time System: Unable to save data to file \"{0}\"!\r\n{1}", m_DataFileName, e.ToString()));

					return false;
				}
			}
		}

		private static bool CheckPaths()
		{
			if (!Directory.Exists(m_DataDirectory))
			{
				Directory.CreateDirectory(m_DataDirectory);
				File.Create(m_DataLocation).Close();

				return false;
			}
			else if (!File.Exists(m_DataLocation))
			{
				File.Create(m_DataLocation).Close();

				return false;
			}

			return true;
		}

		#endregion

		#region Game Commands

		[Usage("TS <command> <parameters>")]
		[Description("Issues commands into the Time System.")]
		private static void TimeSystem_OnCommand(CommandEventArgs e)
		{
			Mobile mobile = e.Mobile;

			if (e.Length >= 1)
			{
				string command = e.GetString(0).ToUpper();

				switch (command)
				{
				case m_CommandSet:
					{
						SetVariable(mobile, e, false);

						break;
					}
				case m_CommandGet:
					{
						if (e.Length == 1)
						{
							StringBuilder sb = GetVariableNames();

							mobile.SendMessage(sb.ToString());
						}
						else
						{
							string variableName = e.GetString(1).ToUpper();

							VariableObject variableObject = GetVariable(variableName);

							mobile.SendMessage(variableObject.Message);
						}

						break;
					}
				case m_CommandAppend:
					{
						SetVariable(mobile, e, true);

						break;
					}
				case m_CommandRepopLightsList:
					{
						PopulateLightsList();

						mobile.SendMessage("The managed lights list has been repopulated.");

						break;
					}
				case m_CommandStop:
					{
						Stop();

						mobile.SendMessage("The time system has been stopped.");

						break;
					}
				case m_CommandStart:
					{
						Start();

						mobile.SendMessage("The time system has been started.");

						break;
					}
				case m_CommandRestart:
					{
						if (m_Enabled)
						{
							Restart();

							mobile.SendMessage("The time system has been restarted.");
						}
						else
						{
							mobile.SendMessage(String.Format("The time system has been stopped.  To start it again, please type {0}TS START"));
						}

						break;
					}
				case m_CommandLoad:
					{
						if (Load())
						{
							mobile.SendMessage("The time system has been successfully loaded from file.");

							Restart();
						}
						else
						{
							mobile.SendMessage("The time system has failed to load from file!");
						}

						break;
					}
				case m_CommandSave:
					{
						if (Save())
						{
							mobile.SendMessage("The time system has been successfully saved to file.");
						}
						else
						{
							mobile.SendMessage("The time system has failed to save to file!");
						}

						break;
					}
				case m_CommandSetTime:
					{
						bool success = false;

						if (e.Length == 2)
						{
							string value = e.GetString(1);

							string[] timeSplit = value.Split(':');

							if (timeSplit.Length == 2)
							{
								int hour = -1;
								int minute = -1;

								VariableObject hourObject = new VariableObject();
								VariableObject minuteObject = new VariableObject();

								try
								{
									hour = Convert.ToInt32(timeSplit[0]);
									minute = Convert.ToInt32(timeSplit[1]);

									hourObject = SetVariable("hour", hour.ToString(), false, false);
									minuteObject = SetVariable("minute", minute.ToString(), false, false);

									success = true;
								}
								catch (Exception ex)
								{
									mobile.SendMessage(ex.ToString());
								}

								mobile.SendMessage(hourObject.Message);
								mobile.SendMessage(minuteObject.Message);

								if (hourObject.Success || minuteObject.Success)
								{
									Restart();
								}
							}
						}

						if (!success)
						{
							mobile.SendMessage("You must set the time using hh:mm format!");
						}

						break;
					}
				case m_CommandQuery:
					{
						if (m_Enabled)
						{
							int gameMinutes = m_MinutesPerTick;
							double perSeconds = m_TimerSpeed;

							mobile.SendMessage(String.Format("The time system is running at {0} game minute{1} every {2} real second{3}.", gameMinutes, gameMinutes == 1 ? "" : "s", perSeconds, perSeconds == 1.0 ? "" : "s"));
						}
						else
						{
							mobile.SendMessage("The time system is not running.");
						}

						break;
					}
				case m_CommandVersion:
					{
						mobile.SendMessage(String.Format("The time system version is {0}.", Version));

						break;
					}
				default:
					{
						mobile.SendMessage("That command does not exist!");

						break;
					}
				}
			}
			else
			{
				if (e.Length == 0)
				{
					StringBuilder sb = new StringBuilder();

					sb.Append("List of commands:\r\n");

					for (int i = 0; i < m_CommandsList.Length; i++)
					{
						string commandName = m_CommandsList[i];

						if (i == 0)
						{
							sb.Append(commandName);
						}
						else
						{
							sb.Append(String.Format(", {0}", commandName));
						}
					}

					mobile.SendMessage(sb.ToString());
				}
				else
				{
					mobile.SendMessage("You must specify a command with parameters!");
				}
			}
		}

		[Usage("BaseTime")]
		[Description("Gets the current base date and time.")]
		private static void BaseTime_OnCommand(CommandEventArgs e)
		{
			e.Mobile.SendMessage(GetTime(0, false));
		}

		[Usage("Time")]
		[Description("Gets the current date and time.")]
		private static void Time_OnCommand(CommandEventArgs e)
		{
			e.Mobile.SendMessage(GetTime(e.Mobile.X, false));
		}

		#endregion

		#region Computing and Calculations

		public static int ComputeLevelFor(BaseLight baseLight)
		{
			if (LightCycle.LevelOverride > int.MinValue)
			{
				return LightCycle.LevelOverride;
			}
			else if (!m_Enabled)
			{
				return m_DayLevel;
			}

			bool darkestHour = false;

			int currentLevel = m_CurrentLevel;

			int minute = m_Minute + TimeZoneAdjustment(baseLight.X);
			int hour = m_Hour;
			int day = m_Day;
			int month = m_Month;
			int year = m_Year;
			int moonPhaseDay = m_MoonPhaseDay;

			CheckTime(ref minute, ref hour, ref day, ref month, ref year, ref moonPhaseDay);

			if (IsNightTime(hour)) // Night time.
			{
				if (m_ScaleTimeMinutes - MinutesAfterNight(minute, hour) >= 0)
				{
					currentLevel = Convert.ToInt32(NightLevelAdjust(moonPhaseDay) * ((double)MinutesAfterNight(minute, hour) / (double)m_ScaleTimeMinutes));
				}
				else if (m_DarkestHourEnabled && IsDarkestHour(minute, hour))
				{
					darkestHour = true;

					if (m_UseAutoLighting && m_RandomLightOutage)
					{
						PerformRandomLightOutage( baseLight );
					}

					int minutesAfter = MinutesAfterNight(minute, hour) - m_DarkestHourStartMinutes;

					if (minutesAfter <= m_DarkestHourScaleTimeMinutes)
					{
						currentLevel = Convert.ToInt32(NightLevelAdjust(moonPhaseDay) + ((m_DarkestHourLevel - NightLevelAdjust(moonPhaseDay)) * ((double)minutesAfter / (double)m_DarkestHourScaleTimeMinutes)));
					}
					else if (minutesAfter > m_DarkestHourScaleTimeMinutes && minutesAfter <= (m_DarkestHourMinutesLong + m_DarkestHourScaleTimeMinutes))
					{
						currentLevel = m_DarkestHourLevel;
					}
					else if (minutesAfter > (m_DarkestHourMinutesLong + m_DarkestHourScaleTimeMinutes) && minutesAfter <= m_DarkestHourTotalMinutes)
					{
						currentLevel = Convert.ToInt32(NightLevelAdjust(moonPhaseDay) + ((m_DarkestHourLevel - NightLevelAdjust(moonPhaseDay)) * ((double)(m_DarkestHourTotalMinutes - minutesAfter) / (double)m_DarkestHourScaleTimeMinutes)));
					}
				}
				else
				{
					currentLevel = NightLevelAdjust(moonPhaseDay);
				}

				if (!darkestHour && m_UseAutoLighting && !baseLight.Burning)
				{
					UpdateManagedLight(baseLight, currentLevel);
				}
			}
			else // Day time.
			{
				if (m_ScaleTimeMinutes - MinutesAfterDay(minute, hour) >= 0)
				{
					currentLevel = Convert.ToInt32((NightLevelAdjust(moonPhaseDay) - m_DayLevel) - ((NightLevelAdjust(moonPhaseDay) - m_DayLevel) * ((double)MinutesAfterDay(minute, hour) / (double)m_ScaleTimeMinutes)));
				}
				else
				{
					currentLevel = m_DayLevel;
				}

				if (m_UseAutoLighting)
				{
					UpdateManagedLight(baseLight, currentLevel);
				}
			}

			return currentLevel;
		}

		public static int ComputeLevelFor(Mobile from)
		{
			if (LightCycle.LevelOverride > int.MinValue)
			{
				return LightCycle.LevelOverride;
			}
			else if (!m_Enabled)
			{
				return m_DayLevel;
			}

			int currentLevel = m_CurrentLevel;

			int minute = m_Minute + TimeZoneAdjustment(from.X);
			int hour = m_Hour;
			int day = m_Day;
			int month = m_Month;
			int year = m_Year;
			int moonPhaseDay = m_MoonPhaseDay;

			CheckTime(ref minute, ref hour, ref day, ref month, ref year, ref moonPhaseDay);

			if (IsNightTime( hour )) // Night time.
			{
				if (m_ScaleTimeMinutes - MinutesAfterNight( minute, hour ) >= 0)
				{
					currentLevel = Convert.ToInt32(NightLevelAdjust(moonPhaseDay) * ((double)MinutesAfterNight(minute, hour) / (double)m_ScaleTimeMinutes));
				}
				else if (m_DarkestHourEnabled && IsDarkestHour( minute, hour ))
				{
					int minutesAfter = MinutesAfterNight(minute, hour) - m_DarkestHourStartMinutes;

					if (minutesAfter <= m_DarkestHourScaleTimeMinutes)
					{
						currentLevel = Convert.ToInt32(NightLevelAdjust(moonPhaseDay) + ((m_DarkestHourLevel - NightLevelAdjust(moonPhaseDay)) * ((double)minutesAfter / (double)m_DarkestHourScaleTimeMinutes)));
					}
					else if (minutesAfter > m_DarkestHourScaleTimeMinutes && minutesAfter <= (m_DarkestHourMinutesLong + m_DarkestHourScaleTimeMinutes))
					{
						currentLevel = m_DarkestHourLevel;
					}
					else if (minutesAfter > (m_DarkestHourMinutesLong + m_DarkestHourScaleTimeMinutes) && minutesAfter <= m_DarkestHourTotalMinutes)
					{
						currentLevel = Convert.ToInt32(NightLevelAdjust(moonPhaseDay) + ((m_DarkestHourLevel - NightLevelAdjust(moonPhaseDay)) * ((double)(m_DarkestHourTotalMinutes - minutesAfter) / (double)m_DarkestHourScaleTimeMinutes)));
					}
				}
				else
				{
					currentLevel = NightLevelAdjust(moonPhaseDay);
				}
			}
			else // Day time.
			{
				if (m_ScaleTimeMinutes - MinutesAfterDay(minute, hour) >= 0)
				{
					currentLevel = Convert.ToInt32((NightLevelAdjust(moonPhaseDay) - m_DayLevel) - ((NightLevelAdjust(moonPhaseDay) - m_DayLevel) * ((double)MinutesAfterDay(minute, hour) / (double)m_ScaleTimeMinutes)));
				}
				else
				{
					currentLevel = m_DayLevel;
				}
			}

			return currentLevel;
		}

		public static void ComputeLightLevel()
		{
			if (LightCycle.LevelOverride > int.MinValue)
			{
				m_CurrentLevel = LightCycle.LevelOverride;

				return;
			}
			else if (!m_Enabled)
			{
				m_CurrentLevel = m_DayLevel;

				return;
			}

			if (IsNightTime(m_Hour)) // Night time.
			{
				if (m_ScaleTimeMinutes - MinutesAfterNight(m_Minute, m_Hour) >= 0)
				{
					if (!m_StopRandomLightOutage)
					{
						m_StopRandomLightOutage = true;
					}

					m_CurrentLevel = Convert.ToInt32(NightLevelAdjust(m_MoonPhaseDay) * ((double)MinutesAfterNight(m_Minute, m_Hour) / (double)m_ScaleTimeMinutes));
				}
				else if (m_DarkestHourEnabled && IsDarkestHour(m_Minute, m_Hour))
				{
					if (m_StopRandomLightOutage)
					{
						m_StopRandomLightOutage = false;
					}

					if (m_UseAutoLighting && m_RandomLightOutage)
					{
						PerformRandomLightOutage();
					}

					int minutesAfter = MinutesAfterNight(m_Minute, m_Hour) - m_DarkestHourStartMinutes;

					if (minutesAfter <= m_DarkestHourScaleTimeMinutes)
					{
						m_CurrentLevel = Convert.ToInt32(NightLevelAdjust(m_MoonPhaseDay) + ((m_DarkestHourLevel - NightLevelAdjust(m_MoonPhaseDay)) * ((double)minutesAfter / (double)m_DarkestHourScaleTimeMinutes)));
					}
					else if (minutesAfter > m_DarkestHourScaleTimeMinutes && minutesAfter <= (m_DarkestHourMinutesLong + m_DarkestHourScaleTimeMinutes))
					{
						m_CurrentLevel = m_DarkestHourLevel;
					}
					else if (minutesAfter > (m_DarkestHourMinutesLong + m_DarkestHourScaleTimeMinutes) && minutesAfter <= m_DarkestHourTotalMinutes)
					{
						m_CurrentLevel = Convert.ToInt32(NightLevelAdjust(m_MoonPhaseDay) + ((m_DarkestHourLevel - NightLevelAdjust(m_MoonPhaseDay)) * ((double)(m_DarkestHourTotalMinutes - minutesAfter) / (double)m_DarkestHourScaleTimeMinutes)));
					}
				}
				else
				{
					if (!m_StopRandomLightOutage)
					{
						m_StopRandomLightOutage = true;

						if (m_UseAutoLighting && m_RandomLightOutage)
						{
							UpdateAllManagedLights();
						}
					}

					m_CurrentLevel = NightLevelAdjust(m_MoonPhaseDay);
				}

				if (m_UseAutoLighting && m_LightsList != null && !m_LightsOn && m_CurrentLevel >= m_LightsOnLevel)
				{
					m_LightsOn = true;

					UpdateAllManagedLights();
				}
			}
			else // Day time.
			{
				if (m_ScaleTimeMinutes - MinutesAfterDay(m_Minute, m_Hour) >= 0)
				{
					m_CurrentLevel = Convert.ToInt32((NightLevelAdjust(m_MoonPhaseDay) - m_DayLevel) - ((NightLevelAdjust(m_MoonPhaseDay) - m_DayLevel) * ((double)MinutesAfterDay(m_Minute, m_Hour) / (double)m_ScaleTimeMinutes)));
				}
				else
				{
					m_CurrentLevel = m_DayLevel;
				}

				if (m_UseAutoLighting && m_LightsOn && m_CurrentLevel < m_LightsOnLevel)
				{
					m_LightsOn = false;

					UpdateAllManagedLights();
				}
			}
		}

		private static int TimeZoneAdjustment(int x)
		{
			if (m_UseTimeZones)
			{
				return Convert.ToInt32(x / m_TimeZoneXDivisor);
			}
			else
			{
				return 0;
			}
		}

		private static int MinutesAfterNight(int minute, int hour)
		{
			if (hour >= m_NightStartHour)
			{
				return (hour - m_NightStartHour) * m_MinutesPerHour + minute;
			}
			else
			{
				return ((m_HoursPerDay - m_NightStartHour) + hour) * m_MinutesPerHour + minute;
			}
		}

		private static int MinutesAfterDay( int minute, int hour )
		{
			if (hour >= m_DayStartHour)
			{
				return (hour - m_DayStartHour) * m_MinutesPerHour + minute;
			}
			else
			{
				return ((m_HoursPerDay - m_DayStartHour) + hour) * m_MinutesPerHour + minute;
			}
		}

		private static bool IsDarkestHour( int minute, int hour )
		{
			int minutesAfterNight = MinutesAfterNight(minute, hour);

			if (minutesAfterNight - m_DarkestHourStartMinutes >= 0)
			{
				return ((minutesAfterNight - m_DarkestHourStartMinutes) <= m_DarkestHourTotalMinutes);
			}
			else
			{
				return false;
			}
		}

		private static bool IsNightTime(int hour)
		{
			if (hour >= m_NightStartHour || hour < m_DayStartHour)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private static int NightLevelAdjust(int moonPhaseDay)
		{
			double moonPhaseMultiplier = ((double)moonPhaseDay - 1.0) / ((double)m_MoonPhaseTotalDays / 2.0);

			if (moonPhaseMultiplier > 1.0)
			{
				moonPhaseMultiplier = 2.0 - moonPhaseMultiplier;
			}

			return m_NightLevel - Convert.ToInt32((double)m_MoonPhaseLevelAdjust * moonPhaseMultiplier);
		}

		private static MoonPhase CurrentMoonPhase( int moonPhaseDay )
		{
			int totalMoonPhases = Enum.GetValues(typeof(MoonPhase)).Length;

			double moonPhaseFraction = 100.0 / (double)totalMoonPhases;
			double moonPhaseDaysFraction = ((double)moonPhaseDay / (double)m_MoonPhaseTotalDays) * 100.0;

			int moonPhaseIndex = Convert.ToInt32((moonPhaseDaysFraction / moonPhaseFraction) - 1.0);

			if (moonPhaseIndex < 0)
			{
				moonPhaseIndex = 0;
			}
			else if (moonPhaseIndex > totalMoonPhases - 1)
			{
				moonPhaseIndex = totalMoonPhases - 1;
			}

			return (MoonPhase)moonPhaseIndex;
		}

		private static void ComputeTime()
		{
			if (m_UseRealTime)
			{
				m_Minute = DateTime.Now.Minute;
				m_Hour = DateTime.Now.Hour;
				m_Day = DateTime.Now.Day;
				m_Month = DateTime.Now.Month;
				m_Year = DateTime.Now.Year;
			}
			else
			{
				m_Minute += m_MinutesPerTick;
			}

			CheckTime(ref m_Minute, ref m_Hour, ref m_Day, ref m_Month, ref m_Year, ref m_MoonPhaseDay);

			if (m_UseTimeZones)
			{
				if (m_UseAutoLighting)
				{
					CheckLights();
				}
			}
			else
			{
				ComputeLightLevel();
			}

			
		}
		
		public static int DetermineSeason()
		{
			int minute = m_Minute;
			int hour = m_Hour;
			int day = m_Day;
			int month = m_Month;
			int year = m_Year;
			int moonPhaseDay = m_MoonPhaseDay;
			CheckTime(ref minute, ref hour, ref day, ref month, ref year, ref moonPhaseDay);
			
			if ((month == 1 && day >= 21) || month == 2 || (month == 3 && day < 21)) return 0;
			if ((month == 3 && day >= 21) || month >= 4 || month <= 8 || (month == 9 && day < 21)) return 1;
			if ((month == 9 && day >= 21) || (month == 10 && day < 31) || month == 11 || (month == 12 && day < 21)) return 2;
			if ((month == 12 && day >= 21) || (month == 1 && day < 21)) return 3;
			if (month == 10 && day == 31) return 4;
			return 0;
		}
		
		public static void CheckSeason()
		{
			int minute = m_Minute;
			int hour = m_Hour;
			int day = m_Day;
			int month = m_Month;
			int year = m_Year;
			int moonPhaseDay = m_MoonPhaseDay;
			CheckTime(ref minute, ref hour, ref day, ref month, ref year, ref moonPhaseDay);
			
			#region SEASONS
			// added for seasons by LIACS
			// 0=Spring, 1=Summer, 2=Fall, 3=Winter
			if ((month == 1 && day >= 21) || month == 2 || (month == 3 && day < 21)) 
			//if (month == 3 && day == 21 && hour == 0 && minute == 0) 
			{
				Map map;
				for (int i = 1; i < 5; i++)
				{
					map = Map.AllMaps[i];
					map.Season = 0; //spring
					
					foreach (NetState state in NetState.Instances)
					{
						Mobile m = state.Mobile;
						if (m != null) 
						{
							state.Send(SeasonChange.Instantiate(m.GetSeason(), true));
							m.SendEverything();
						}
					}
				}
			}
			if ((month == 3 && day >= 21) || month >= 4 || month <= 8 || (month == 9 && day < 21)) 
			//if(month == 6 && day == 21 && hour == 0 && minute == 0)
			{
				Map map;

				for (int i = 1; i < 5; i++)
				{
					map = Map.AllMaps[i];
					map.Season = 1; //summer

					foreach (NetState state in NetState.Instances)
					{
						Mobile m = state.Mobile;
						if (m != null) 
						{
							state.Send(SeasonChange.Instantiate(m.GetSeason(), true));
							m.SendEverything();
						}
					}
				}
			}
			if ((month == 9 && day >= 21) || (month == 10 && day < 31) || month == 11 || (month == 12 && day < 21)) 
			//if (month == 9 && day == 21 && hour == 0 && minute == 0)
			{
				Map map;
				for (int i = 1; i < 5; i++)
				{

					map = Map.AllMaps[i];
					map.Season = 2; //fall

					foreach (NetState state in NetState.Instances)
					{
						Mobile m = state.Mobile;
						if (m != null)
						{
							state.Send(SeasonChange.Instantiate(m.GetSeason(), true));
							m.SendEverything();
						}
					}
				}
			}
			if ((month == 12 && day >= 21) || (month == 1 && day < 21)) 
			//if (month == 12 && day == 21 && hour == 0 && minute == 0)
			{
				Map map;
				for (int i = 1; i < 5; i++)
				{

					map = Map.AllMaps[i];
					map.Season = 3; //winter

					foreach (NetState state in NetState.Instances)
					{
						Mobile m = state.Mobile;
						if (m != null)
						{
							state.Send(SeasonChange.Instantiate(m.GetSeason(), true));
							m.SendEverything();
						}
					}
				}
			}
			if (month == 10 && day == 31) 
			{
				Map map;
				for (int i = 1; i < 5; i++)
				{

					map = Map.AllMaps[i];
					map.Season = 4; //cursed

					foreach (NetState state in NetState.Instances)
					{
						Mobile m = state.Mobile;
						if (m != null)
						{
							state.Send(SeasonChange.Instantiate(m.GetSeason(), true));
							m.SendEverything();
						}
					}
				}
			}
			// end added for seasons by LIACS
			#endregion SEASONS
		}

		private static void CheckTime( ref int minute, ref int hour, ref int day, ref int month, ref int year, ref int moonPhaseDay )
		{
			while( minute >= m_MinutesPerHour )
			{
				minute -= m_MinutesPerHour;

				hour++;
			}

			if (hour >= m_HoursPerDay)
			{
				hour -= m_HoursPerDay;

				day++;
				//CheckSeason(day, month);
			}

			if (day > m_DaysPerMonth)
			{
				day -= m_DaysPerMonth;

				month++;
			}

			if (month > m_MonthsPerYear)
			{
				month -= m_MonthsPerYear;

				year++;
			}

			if (hour == m_NightStartHour && minute == 0)
			{
				moonPhaseDay++;

				if (moonPhaseDay > m_MoonPhaseTotalDays)
				{
					moonPhaseDay -= m_MoonPhaseTotalDays;
				}
			}
		}

		#endregion

		#region Managing Lights

		private static void PopulateLightsList()
		{
			m_LightsList = new ArrayList();

			foreach (Item item in World.Items.Values)
			{
				if (!item.Deleted && item is BaseLight)
				{
					for (int i = 0; i < m_ItemLightTypes.Length; i++)
					{
						if (item.GetType() == m_ItemLightTypes[i])
						{
							m_LightsList.Add(item);

							ComputeLevelFor((BaseLight)item);
						}
					}
				}
			}

			Console.WriteLine(String.Format("Time System: Calculated managed lights list and found {0} light{1}.", m_LightsList.Count, m_LightsList.Count == 1 ? "" : "s"));
		}

		private static void CheckLights()
		{
			if (m_LightsList == null)
			{
				return;
			}

			for (int i = 0; i < m_LightsList.Count; i++)
			{
				BaseLight baseLight = (BaseLight)m_LightsList[i];

				if (!baseLight.Deleted)
				{
					ComputeLevelFor(baseLight);
				}
				else
				{
					m_LightsList.RemoveAt(i);

					i--;
				}
			}
		}

		private static void PerformRandomLightOutage()
		{
			if (m_LightsList == null)
			{
				return;
			}

			int lowNumber = new Random(m_RandomSeed).Next(1, (100 - m_LightOutageChancePerTick) + 1);
			int highNumber = lowNumber + m_LightOutageChancePerTick;

			int randomChance = new Random(m_RandomSeed).Next(1, 101);
			int randomPercent = new Random(m_RandomSeed).Next(m_MinTotalLightsTogglePercent, m_MaxTotalLightsTogglePercent + 1);
			int randomLights = Convert.ToInt32(((double)(m_MinTotalLightsTogglePercent + randomPercent) / 100.0) * m_LightsList.Count);

			Random random = new Random(m_RandomSeed);

			if (randomChance >= lowNumber && randomChance <= highNumber)
			{
				for (int i = 0; i < randomLights; i++)
				{
					int randomIndex = random.Next(m_LightsList.Count);

					BaseLight randomLight = (BaseLight)m_LightsList[randomIndex];

					if (randomLight.Deleted)
					{
						m_LightsList.RemoveAt(i);

						randomIndex--;
					}
					else
					{
						if (randomLight.Burning)
						{
							randomLight.Douse();
						}
						else
						{
							randomLight.Ignite();
						}
					}
				}
			}
		}

		private static void PerformRandomLightOutage( BaseLight baseLight )
		{
			int lowNumber = new Random(m_RandomSeed).Next(1, (100 - m_LightOutageChancePerTick) + 1);
			int highNumber = lowNumber + m_LightOutageChancePerTick;

			int randomChance = new Random(m_RandomSeed).Next(1, 101);

			if (randomChance >= lowNumber && randomChance <= highNumber)
			{
				if (baseLight.Burning)
				{
					baseLight.Douse();
				}
				else
				{
					baseLight.Ignite();
				}
			}
		}

		private static void UpdateAllManagedLights()
		{
			if (m_LightsList == null)
			{
				return;
			}

			if (m_CurrentLevel >= m_LightsOnLevel)
			{
				for (int i = 0; i < m_LightsList.Count; i++)
				{
					BaseLight baseLight = (BaseLight)m_LightsList[i];

					if (!baseLight.Deleted)
					{
						if (!baseLight.Burning)
						{
							baseLight.Ignite();
						}
					}
					else
					{
						m_LightsList.RemoveAt(i);

						i--;
					}
				}
			}
			else
			{
				for (int i = 0; i < m_LightsList.Count; i++)
				{
					BaseLight baseLight = (BaseLight)m_LightsList[i];

					if (!baseLight.Deleted)
					{
						if (baseLight.Burning)
						{
							baseLight.Douse();
						}
					}
					else
					{
						m_LightsList.RemoveAt(i);

						i--;
					}
				}
			}
		}

		private static void UpdateManagedLight(BaseLight baseLight, int currentLevel)
		{
			if (currentLevel >= m_LightsOnLevel && !baseLight.Burning)
			{
				baseLight.Ignite();
			}
			else if (currentLevel < m_LightsOnLevel && baseLight.Burning)
			{
				baseLight.Douse();
			}
		}

		private static void TurnOnAllWorldLights()
		{
			foreach (Item item in World.Items.Values)
			{
				if (item is BaseLight)
				{
					for (int i = 0; i < m_ItemLightTypes.Length; i++)
					{
						if (item.GetType() == m_ItemLightTypes[i])
						{
							((BaseLight)item).Ignite();
						}
					}
				}
			}
		}

		#endregion

		#region Get Information

		public static string GetTime(int x, bool fromClock)
		{
			int minute = m_Minute + TimeZoneAdjustment(x);
			int hour = m_Hour;
			int day = m_Day;
			int month = m_Month;
			int year = m_Year;
			int moonPhaseDay = m_MoonPhaseDay;

			CheckTime(ref minute, ref hour, ref day, ref month, ref year, ref moonPhaseDay);

			if (fromClock)
			{
				return Support.GetTimeFormat(m_ClockTimeFormat, minute, hour, day, month, year, GetMoonPhaseName(x));
			}
			else
			{
				return Support.GetTimeFormat(m_TimeFormat, minute, hour, day, month, year, GetMoonPhaseName(x));
			}
		}

		public static void GetTime(int x, out int hours, out int minutes)
		{
			hours = m_Hour;
			minutes = m_Minute + TimeZoneAdjustment(x);
			int day = m_Day;
			int month = m_Month;
			int year = m_Year;
			int moonPhaseDay = m_MoonPhaseDay;

			CheckTime(ref minutes, ref hours, ref day, ref month, ref year, ref moonPhaseDay);
		}

		public static string GetMoonPhaseName( int x )
		{
			int minute = m_Minute + TimeZoneAdjustment(x);
			int hour = m_Hour;
			int day = m_Day;
			int month = m_Month;
			int year = m_Year;
			int moonPhaseDay = m_MoonPhaseDay;

			CheckTime(ref minute, ref hour, ref day, ref month, ref year, ref moonPhaseDay);

			string moonPhase = CurrentMoonPhase(moonPhaseDay).ToString();

			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < moonPhase.Length; i++)
			{
				char c = moonPhase[i];

				if (Char.IsUpper(c))
				{
					if (i == 0)
					{
						sb.Append(Char.ToLower(c));
					}
					else
					{
						sb.Append(String.Format(" {0}", Char.ToLower(c)));
					}
				}
				else
				{
					sb.Append(c);
				}
			}

			return sb.ToString();
		}

		private static StringBuilder GetVariableNames()
		{
			string[] nameVariables = Enum.GetNames(typeof(Variable));

			StringBuilder sb = new StringBuilder();

			sb.Append("List of variables:\r\n");

			for (int i = 0; i < nameVariables.Length; i++)
			{
				string name = nameVariables[i];

				if (i == 0)
				{
					sb.Append(name);
				}
				else
				{
					sb.Append(String.Format(", {0}", name));
				}
			}

			return sb;
		}

		private static Variable GetVariableFromName(string variableName)
		{
			Variable variable = Variable.None;

			variableName = variableName.ToUpper();

			int totalVariables = Enum.GetValues(typeof(Variable)).Length;

			for (int i = 0; i < totalVariables; i++)
			{
				string name = ((Variable)i).ToString().ToUpper();

				if (variableName == name)
				{
					variable = (Variable)i;

					break;
				}
			}

			return variable;
		}

		#endregion

		#region Set Variables

		private static void SetVariable(Mobile mobile, CommandEventArgs e, bool append)
		{
			if (e.Length == 1)
			{
				StringBuilder sb = GetVariableNames();

				mobile.SendMessage(sb.ToString());
			}
			else
			{
				string variableName = e.GetString(1).ToLower();
				string value = null;

				if (e.Length > 2)
				{
					StringBuilder sb = new StringBuilder();

					for (int i = 2; i < e.Length; i++)
					{
						if (i == 2)
						{
							sb.Append(e.GetString(i));
						}
						else
						{
							sb.Append(String.Format(" {0}", e.GetString(i)));
						}
					}

					value = sb.ToString();
				}

				VariableObject variableObject = SetVariable(variableName, value, true, append);

				mobile.SendMessage(variableObject.Message);
			}
		}

		public static VariableObject SetVariable(string variableName, string variableValue, bool restart, bool append)
		{
			Variable variable = GetVariableFromName( variableName );

			object o = Support.GetValue(null, variableValue);

			VariableObject variableObject = new VariableObject();

			bool success = false;
			string minValue = null;
			string maxValue = null;
			string message = null;

			switch (variable)
			{
				case Variable.Defaults:
				{
					m_DayLevel = 0;
					m_NightLevel = 18;
					m_DarkestHourLevel = 28;
					m_LightsOnLevel = 9;

					m_UseTimeZones = true;
					m_TimeZoneXDivisor = 16;

					UseRealTime = false;

					UseAutoLighting = true;
					RandomLightOutage = true;
					m_LightOutageChancePerTick = 10;
					m_MinTotalLightsTogglePercent = 10;
					m_MaxTotalLightsTogglePercent = 25;

					m_TimerSpeed = 5.0;
					m_MinutesPerTick = 1;

					m_MinutesPerHour = 60;
					m_HoursPerDay = 24;
					m_DaysPerMonth = 30;
					m_MonthsPerYear = 12;

					m_NightStartHour = 20;
					m_DayStartHour = 6;
					m_ScaleTimeMinutes = 60;

					m_DarkestHourEnabled = true;
					m_DarkestHourMinutesAfterNight = 150;
					m_DarkestHourScaleTimeMinutes = 30;
					m_DarkestHourMinutesLong = 120;

					m_MoonPhaseTotalDays = 16;
					m_MoonPhaseDay = 1;
					m_MoonPhaseLevelAdjust = 6;

					m_Year = 100;
					m_Month = 1;
					m_Day = 1;
					m_Hour = 4;
					m_Minute = 0;

					m_TimeFormat = String.Format("{0} {1}", m_TimeFormatPreset1, m_TimeFormatMoonPhase);
					m_ClockTimeFormat = m_TimeFormatPreset1;

					variableObject.Success = true;
					variableObject.Message = "The time system has been reset to its default configuration.";

					return variableObject;
				}
			case Variable.DayLevel:
				{
					Type typeExpected = typeof(int);

					int lowValue = m_MinLightLevel;
					int highValue = m_NightLevel - m_MinLightLevelDifference;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is int)
					{
						int value = (int)o;

						if (value >= lowValue && value <= highValue)
						{
							success = true;

							m_DayLevel = value;
						}
						else
						{
							message = Support.ErrorMessageFormatter(variable, value, minValue, maxValue);
						}
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.NightLevel:
				{
					Type typeExpected = typeof(int);

					int lowValue = m_DayLevel + m_MinLightLevelDifference;
					int highValue = m_MaxLightLevel;

					if (m_DarkestHourEnabled)
					{
						highValue = m_DarkestHourLevel - m_MinDarkestHourNightLevelDifference;
					}

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is int)
					{
						int value = (int)o;

						if (value >= lowValue && value <= highValue)
						{
							success = true;

							m_NightLevel = value;
						}
						else
						{
							message = Support.ErrorMessageFormatter(variable, value, minValue, maxValue);

							if (m_DarkestHourEnabled && value >= (m_DarkestHourLevel - m_MinDarkestHourNightLevelDifference))
							{
								message = String.Format("{0}\r\nDisabling the Darkest Hour will allow you to set a value of up to {1}.", message, m_MaxLightLevel);
							}
						}
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.DarkestHourLevel:
				{
					Type typeExpected = typeof(int);

					int lowValue = m_NightLevel + m_MinDarkestHourNightLevelDifference;
					int highValue = m_MaxLightLevel;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is int)
					{
						int value = (int)o;

						if (value >= lowValue && value <= highValue)
						{
							success = true;

							m_DarkestHourLevel = value;
						}
						else
						{
							message = Support.ErrorMessageFormatter(variable, value, minValue, maxValue);
						}
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.LightsOnLevel:
				{
					Type typeExpected = typeof(int);

					int lowValue = m_MinLightLevel;
					int highValue = m_MaxLightLevel;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is int)
					{
						int value = (int)o;

						if (value >= lowValue && value <= highValue)
						{
							success = true;

							m_LightsOnLevel = value;
						}
						else
						{
							message = Support.ErrorMessageFormatter(variable, value, minValue, maxValue);
						}
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.UseTimeZones:
				{
					Type typeExpected = typeof(bool);

					bool lowValue = false;
					bool highValue = true;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is bool)
					{
						bool value = (bool)o;

						success = true;

						m_UseTimeZones = value;
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.TimeZoneXDivisor:
				{
					Type typeExpected = typeof(int);

					int lowValue = 16;
					int highValue = 512;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is int)
					{
						int value = (int)o;

						if (value >= lowValue && value <= highValue)
						{
							success = true;

							m_TimeZoneXDivisor = value;
						}
						else
						{
							message = Support.ErrorMessageFormatter(variable, value, minValue, maxValue);
						}
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.UseRealTime:
				{
					Type typeExpected = typeof(bool);

					bool lowValue = false;
					bool highValue = true;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is bool)
					{
						bool value = (bool)o;

						success = true;

						UseRealTime = value;
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.UseAutoLighting:
				{
					Type typeExpected = typeof(bool);

					bool lowValue = false;
					bool highValue = true;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is bool)
					{
						bool value = (bool)o;

						success = true;

						UseAutoLighting = value;
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.RandomLightOutage:
				{
					Type typeExpected = typeof(bool);

					bool lowValue = false;
					bool highValue = true;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is bool)
					{
						bool value = (bool)o;

						success = true;

						RandomLightOutage = value;
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.LightOutageChancePerTick:
				{
					Type typeExpected = typeof(int);

					int lowValue = 1;
					int highValue = 100;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is int)
					{
						int value = (int)o;

						if (value >= lowValue && value <= highValue)
						{
							success = true;

							m_LightOutageChancePerTick = value;
						}
						else
						{
							message = Support.ErrorMessageFormatter(variable, value, minValue, maxValue);
						}
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.MinTotalLightsTogglePercent:
				{
					Type typeExpected = typeof(int);

					int lowValue = 1;
					int highValue = m_MaxTotalLightsTogglePercent;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is int)
					{
						int value = (int)o;

						if (value >= lowValue && value <= highValue)
						{
							success = true;

							m_MinTotalLightsTogglePercent = value;
						}
						else
						{
							message = Support.ErrorMessageFormatter(variable, value, minValue, maxValue);
						}
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.MaxTotalLightsTogglePercent:
				{
					Type typeExpected = typeof(int);

					int lowValue = m_MinTotalLightsTogglePercent;
					int highValue = 100;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is int)
					{
						int value = (int)o;

						if (value >= lowValue && value <= highValue)
						{
							success = true;

							m_MaxTotalLightsTogglePercent = value;
						}
						else
						{
							message = Support.ErrorMessageFormatter(variable, value, minValue, maxValue);
						}
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.TimerSpeed:
				{
					Type typeExpected = typeof(double);

					if (o is int)
					{
						o = Convert.ToDouble(o);
					}

					double lowValue = m_MinTimerValue;
					double highValue = m_MinutesPerHour;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is double)
					{
						double value = (double)o;

						if (value >= lowValue && value <= highValue)
						{
							success = true;

							m_TimerSpeed = value;

							Restart();
						}
						else
						{
							message = Support.ErrorMessageFormatter(variable, value, minValue, maxValue);
						}
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.MinutesPerTick:
				{
					Type typeExpected = typeof(int);

					int lowValue = 1;
					int highValue = m_MinutesPerHour;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is int)
					{
						int value = (int)o;

						if (value >= lowValue && value <= highValue)
						{
							success = true;

							m_MinutesPerTick = value;
						}
						else
						{
							message = Support.ErrorMessageFormatter(variable, value, minValue, maxValue);
						}
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.MinutesPerHour:
				{
					Type typeExpected = typeof(int);

					int lowValue = 10;
					int highValue = 99;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is int)
					{
						int value = (int)o;

						if (value >= lowValue && value <= highValue)
						{
							success = true;

							m_MinutesPerHour = value;
						}
						else
						{
							message = Support.ErrorMessageFormatter(variable, value, minValue, maxValue);
						}
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.HoursPerDay:
				{
					Type typeExpected = typeof(int);

					int lowValue = 10;
					int highValue = 99;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is int)
					{
						int value = (int)o;

						if (value >= lowValue && value <= highValue)
						{
							success = true;

							m_HoursPerDay = value;

							if (m_NightStartHour > m_HoursPerDay)
							{
								m_NightStartHour = 0;
							}

							if (m_DayStartHour > m_HoursPerDay)
							{
								m_DayStartHour = 4;
							}
						}
						else
						{
							message = Support.ErrorMessageFormatter(variable, value, minValue, maxValue);
						}
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.DaysPerMonth:
				{
					Type typeExpected = typeof(int);

					int lowValue = 10;
					int highValue = 99;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is int)
					{
						int value = (int)o;

						if (value >= lowValue && value <= highValue)
						{
							success = true;

							m_DaysPerMonth = value;
						}
						else
						{
							message = Support.ErrorMessageFormatter(variable, value, minValue, maxValue);
						}
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.MonthsPerYear:
				{
					Type typeExpected = typeof(int);

					int lowValue = 10;
					int highValue = 99;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is int)
					{
						int value = (int)o;

						if (value >= lowValue && value <= highValue)
						{
							success = true;

							m_MonthsPerYear = value;
						}
						else
						{
							message = Support.ErrorMessageFormatter(variable, value, minValue, maxValue);
						}
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.NightStartHour:
				{
					Type typeExpected = typeof(int);

					int lowValue = 0;
					int highValue = m_HoursPerDay;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is int)
					{
						int value = (int)o;

						if (value >= lowValue && value <= highValue)
						{
							bool overNight = false;

							int beforeDayStartHour = m_DayStartHour - m_MinDarkestHourNightLevelDifference;
							int afterDayStartHour = m_DayStartHour + m_MinDarkestHourNightLevelDifference;

							if (beforeDayStartHour < 0)
							{
								overNight = true;

								beforeDayStartHour += m_HoursPerDay;
							}

							if (afterDayStartHour > m_HoursPerDay)
							{
								overNight = true;

								afterDayStartHour -= m_HoursPerDay;
							}

							if (overNight)
							{
								if (beforeDayStartHour > afterDayStartHour)
								{
									lowValue = afterDayStartHour;
									highValue = beforeDayStartHour;
								}
								else
								{
									lowValue = beforeDayStartHour;
									highValue = afterDayStartHour;
								}

								if (value >= lowValue && value <= highValue)
								{
									success = true;

									m_NightStartHour = value;
								}
								else
								{
									message = Support.ErrorMessageFormatter(variable, value, lowValue.ToString(), highValue.ToString());
								}
							}
							else
							{
								if (value <= beforeDayStartHour || value >= afterDayStartHour)
								{
									success = true;

									m_NightStartHour = value;
								}
								else
								{
									message = Support.ErrorMessageFormatter(variable, value, "0", beforeDayStartHour.ToString(), afterDayStartHour.ToString(), m_HoursPerDay.ToString());
								}
							}
						}
						else
						{
							message = Support.ErrorMessageFormatter(variable, value, minValue, maxValue);
						}
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.DayStartHour:
				{
					Type typeExpected = typeof(int);

					int lowValue = 0;
					int highValue = m_HoursPerDay;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is int)
					{
						int value = (int)o;

						if (value >= lowValue && value <= highValue)
						{
							bool overNight = false;

							int beforeNightStartHour = m_NightStartHour - m_MinDarkestHourNightLevelDifference;
							int afterNightStartHour = m_NightStartHour + m_MinDarkestHourNightLevelDifference;

							if (beforeNightStartHour < 0)
							{
								overNight = true;

								beforeNightStartHour += m_HoursPerDay;
							}

							if (afterNightStartHour > m_HoursPerDay)
							{
								overNight = true;

								afterNightStartHour -= m_HoursPerDay;
							}

							if (overNight)
							{
								if (beforeNightStartHour > afterNightStartHour)
								{
									lowValue = afterNightStartHour;
									highValue = beforeNightStartHour;
								}
								else
								{
									lowValue = beforeNightStartHour;
									highValue = afterNightStartHour;
								}

								if (value >= lowValue && value <= highValue)
								{
									success = true;

									m_DayStartHour = value;
								}
								else
								{
									message = Support.ErrorMessageFormatter(variable, value, lowValue.ToString(), highValue.ToString());
								}
							}
							else
							{
								if (value <= beforeNightStartHour || value >= afterNightStartHour)
								{
									success = true;

									m_DayStartHour = value;
								}
								else
								{
									message = Support.ErrorMessageFormatter(variable, value, "0", beforeNightStartHour.ToString(), afterNightStartHour.ToString(), m_HoursPerDay.ToString());
								}
							}
						}
						else
						{
							message = Support.ErrorMessageFormatter(variable, value, minValue, maxValue);
						}
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.ScaleTimeMinutes:
				{
					Type typeExpected = typeof(int);

					int lowValue = 0;
					int highValue = m_NightHours + m_DayHours;

					if (m_NightHours <= m_DayHours)
					{
						highValue = Convert.ToInt32(((double)m_NightHours / 2.0) * m_MinutesPerHour);
					}
					else
					{
						highValue = Convert.ToInt32(((double)m_DayHours / 2.0) * m_MinutesPerHour);
					}

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is int)
					{
						int value = (int)o;

						if (value >= lowValue && value <= highValue)
						{
							success = true;

							m_ScaleTimeMinutes = value;
						}
						else
						{
							message = Support.ErrorMessageFormatter(variable, value, minValue, maxValue);
						}
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.DarkestHourEnabled:
				{
					Type typeExpected = typeof(bool);

					bool lowValue = false;
					bool highValue = true;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is bool)
					{
						bool value = (bool)o;

						success = true;

						m_DarkestHourEnabled = value;
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.DarkestHourMinutesAfterNight:
				{
					Type typeExpected = typeof(int);

					int lowValue = 0;
					int highValue = (m_NightHours * m_MinutesPerHour) - m_DarkestHourTotalMinutes;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is int)
					{
						int value = (int)o;

						if (value >= lowValue && value <= highValue)
						{
							success = true;

							m_DarkestHourMinutesAfterNight = value;
						}
						else
						{
							message = Support.ErrorMessageFormatter(variable, value, minValue, maxValue);
						}
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.DarkestHourScaleTimeMinutes:
				{
					Type typeExpected = typeof(int);

					int lowValue = 0;
					int highValue = Convert.ToInt32((((double)(m_NightHours * m_MinutesPerHour) - m_DarkestHourMinutesLong) / 2.0) - m_DarkestHourMinutesAfterNight);

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is int)
					{
						int value = (int)o;

						if (value >= lowValue && value <= highValue)
						{
							success = true;

							m_DarkestHourScaleTimeMinutes = value;
						}
						else
						{
							message = Support.ErrorMessageFormatter(variable, value, minValue, maxValue);
						}
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.DarkestHourMinutesLong:
				{
					Type typeExpected = typeof(int);

					int lowValue = 0;
					int highValue = (m_NightHours * m_MinutesPerHour) - (m_ScaleTimeMinutes * 2) - m_DarkestHourMinutesAfterNight;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is int)
					{
						int value = (int)o;

						if (value >= lowValue && value <= highValue)
						{
							success = true;

							m_DarkestHourMinutesLong = value;
						}
						else
						{
							message = Support.ErrorMessageFormatter(variable, value, minValue, maxValue);
						}
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.MoonPhaseTotalDays:
				{
					Type typeExpected = typeof(int);

					int lowValue = Enum.GetValues(typeof(MoonPhase)).Length;
					int highValue = int.MaxValue;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is int)
					{
						int value = (int)o;

						if (value >= lowValue && value <= highValue)
						{
							success = true;

							m_MoonPhaseTotalDays = value;
						}
						else
						{
							message = Support.ErrorMessageFormatter(variable, value, minValue, maxValue);
						}
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.MoonPhaseDay:
				{
					Type typeExpected = typeof(int);

					int lowValue = 1;
					int highValue = m_MoonPhaseTotalDays;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is int)
					{
						int value = (int)o;

						if (value >= lowValue && value <= highValue)
						{
							success = true;

							m_MoonPhaseDay = value;
						}
						else
						{
							message = Support.ErrorMessageFormatter(variable, value, minValue, maxValue);
						}
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.MoonPhaseLevelAdjust:
				{
					Type typeExpected = typeof(int);

					int lowValue = m_MinLightLevel;
					int highValue = m_NightLevel;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is int)
					{
						int value = (int)o;

						if (value >= lowValue && value <= highValue)
						{
							success = true;

							m_MoonPhaseLevelAdjust = value;
						}
						else
						{
							message = Support.ErrorMessageFormatter(variable, value, minValue, maxValue);
						}
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.Year:
				{
					Type typeExpected = typeof(int);

					int lowValue = 1;
					int highValue = int.MaxValue;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is int)
					{
						int value = (int)o;

						if (value >= lowValue && value <= highValue)
						{
							success = true;

							m_Year = value;
						}
						else
						{
							message = Support.ErrorMessageFormatter(variable, value, minValue, maxValue);
						}
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.Month:
				{
					Type typeExpected = typeof(int);

					int lowValue = 1;
					int highValue = m_MonthsPerYear;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is int)
					{
						int value = (int)o;

						if (value >= lowValue && value <= highValue)
						{
							success = true;

							m_Month = value;
						}
						else
						{
							message = Support.ErrorMessageFormatter(variable, value, minValue, maxValue);
						}
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.Day:
				{
					Type typeExpected = typeof(int);

					int lowValue = 1;
					int highValue = m_DaysPerMonth;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is int)
					{
						int value = (int)o;

						if (value >= lowValue && value <= highValue)
						{
							success = true;

							m_Day = value;
						}
						else
						{
							message = Support.ErrorMessageFormatter(variable, value, minValue, maxValue);
						}
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.Hour:
				{
					Type typeExpected = typeof(int);

					int lowValue = 0;
					int highValue = m_HoursPerDay - 1;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is int)
					{
						int value = (int)o;

						if (value >= lowValue && value <= highValue)
						{
							success = true;

							m_Hour = value;
						}
						else
						{
							message = Support.ErrorMessageFormatter(variable, value, minValue, maxValue);
						}
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.Minute:
				{
					Type typeExpected = typeof(int);

					int lowValue = 0;
					int highValue = m_MinutesPerHour - 1;

					minValue = Convert.ToString(lowValue);
					maxValue = Convert.ToString(highValue);

					if (o is int)
					{
						int value = (int)o;

						if (value >= lowValue && value <= highValue)
						{
							success = true;

							m_Minute = value;
						}
						else
						{
							message = Support.ErrorMessageFormatter(variable, value, minValue, maxValue);
						}
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, minValue, maxValue, typeExpected);
					}

					break;
				}
			case Variable.TimeFormat:
				{
					Type typeExpected = typeof(string);

					if (variableValue != null)
					{
						switch (variableValue.ToLower())
						{
						case "preset1":
							{
								variableValue = String.Format("{0} {1}", m_TimeFormatPreset1, m_TimeFormatMoonPhase);

								break;
							}
						case "preset2":
							{
								variableValue = String.Format("{0} {1}", m_TimeFormatPreset2, m_TimeFormatMoonPhase);

								break;
							}
						case "preset3":
							{
								variableValue = String.Format("{0} {1}", m_TimeFormatPreset3, m_TimeFormatMoonPhase);

								break;
							}
						case "preset4":
							{
								variableValue = String.Format("{0} {1}", m_TimeFormatPreset4, m_TimeFormatMoonPhase);

								break;
							}
						case "preset5":
							{
								variableValue = String.Format("{0} {1}", m_TimeFormatPreset5, m_TimeFormatMoonPhase);

								break;
							}
						case "preset6":
							{
								variableValue = String.Format("{0} {1}", m_TimeFormatPreset6, m_TimeFormatMoonPhase);

								break;
							}
						}

						variableValue = variableValue.Replace('$', Support.CodeChar);

						if (append)
						{
							m_TimeFormat = String.Format("{0}{1}", m_TimeFormat, variableValue);
						}
						else
						{
							m_TimeFormat = variableValue;
						}

						success = true;
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, typeExpected);
					}

					break;
				}
			case Variable.ClockTimeFormat:
				{
					Type typeExpected = typeof(string);

					if (variableValue != null)
					{
						switch (variableValue.ToLower())
						{
						case "preset1":
							{
								variableValue = m_TimeFormatPreset1;

								break;
							}
						case "preset2":
							{
								variableValue = m_TimeFormatPreset2;

								break;
							}
						case "preset3":
							{
								variableValue = m_TimeFormatPreset3;

								break;
							}
						case "preset4":
							{
								variableValue = m_TimeFormatPreset4;

								break;
							}
						case "preset5":
							{
								variableValue = m_TimeFormatPreset5;

								break;
							}
						case "preset6":
							{
								variableValue = m_TimeFormatPreset6;

								break;
							}
						}

						variableValue = variableValue.Replace('$', Support.CodeChar);

						if (append)
						{
							m_ClockTimeFormat = String.Format("{0}{1}", m_ClockTimeFormat, variableValue);
						}
						else
						{
							m_ClockTimeFormat = variableValue;
						}

						success = true;
					}
					else
					{
						message = Support.ErrorMessageFormatter(variable, o, typeExpected);
					}

					break;
				}
			default:
				{
					message = "That variable type does not exist!";

					break;
				}
			}

			if (success)
			{
				message = Support.VariableMessageFormatter(GetVariableFromName(variableName).ToString(), variableValue, append);

				if (restart)
				{
					Restart();
				}
			}

			variableObject.Success = success;
			variableObject.Message = message;

			return variableObject;
		}

		#endregion

		#region Get Variables

		private static VariableObject GetVariable(string variableName)
		{
			Variable variable = GetVariableFromName(variableName);

			VariableObject variableObject = new VariableObject();

			bool success = false;
			string message = null;

			switch (variable)
			{
			case Variable.None:
				{
					message = "That variable type does not exist!";

					break;
				}
			case Variable.Defaults:
				{
					message = "That variable type does not exist!";

					break;
				}
			case Variable.DayLevel:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_DayLevel.ToString());

					break;
				}
			case Variable.NightLevel:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_NightLevel.ToString());

					success = true;

					break;
				}
			case Variable.DarkestHourLevel:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_DarkestHourLevel.ToString());

					success = true;

					break;
				}
			case Variable.LightsOnLevel:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_LightsOnLevel.ToString());

					success = true;

					break;
				}
			case Variable.UseTimeZones:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_UseTimeZones.ToString());

					success = true;

					break;
				}
			case Variable.TimeZoneXDivisor:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_TimeZoneXDivisor.ToString());

					success = true;

					break;
				}
			case Variable.UseRealTime:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_UseRealTime.ToString());

					success = true;

					break;
				}
			case Variable.UseAutoLighting:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_UseAutoLighting.ToString());

					success = true;

					break;
				}
			case Variable.RandomLightOutage:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_RandomLightOutage.ToString());

					success = true;

					break;
				}
			case Variable.LightOutageChancePerTick:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_LightOutageChancePerTick.ToString());

					success = true;

					break;
				}
			case Variable.TimerSpeed:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_TimerSpeed.ToString());

					success = true;

					break;
				}
			case Variable.MinutesPerTick:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_MinutesPerTick.ToString());

					success = true;

					break;
				}
			case Variable.MinutesPerHour:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_MinutesPerHour.ToString());

					success = true;

					break;
				}
			case Variable.HoursPerDay:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_HoursPerDay.ToString());

					success = true;

					break;
				}
			case Variable.DaysPerMonth:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_DaysPerMonth.ToString());

					success = true;

					break;
				}
			case Variable.MonthsPerYear:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_MonthsPerYear.ToString());

					success = true;

					break;
				}
			case Variable.NightStartHour:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_NightStartHour.ToString());

					success = true;

					break;
				}
			case Variable.DayStartHour:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_DayStartHour.ToString());

					success = true;

					break;
				}
			case Variable.ScaleTimeMinutes:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_ScaleTimeMinutes.ToString());

					success = true;

					break;
				}
			case Variable.DarkestHourEnabled:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_DarkestHourEnabled.ToString());

					success = true;

					break;
				}
			case Variable.DarkestHourMinutesAfterNight:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_DarkestHourMinutesAfterNight.ToString());

					success = true;

					break;
				}
			case Variable.DarkestHourScaleTimeMinutes:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_DarkestHourScaleTimeMinutes.ToString());

					success = true;

					break;
				}
			case Variable.DarkestHourMinutesLong:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_DarkestHourMinutesLong.ToString());

					success = true;

					break;
				}
			case Variable.MoonPhaseTotalDays:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_MoonPhaseTotalDays.ToString());

					success = true;

					break;
				}
			case Variable.MoonPhaseDay:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_MoonPhaseDay.ToString());

					success = true;

					break;
				}
			case Variable.MoonPhaseLevelAdjust:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_MoonPhaseLevelAdjust.ToString());

					success = true;

					break;
				}
			case Variable.Year:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_Year.ToString());

					success = true;

					break;
				}
			case Variable.Month:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_Month.ToString());

					success = true;

					break;
				}
			case Variable.Day:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_Day.ToString());

					success = true;

					break;
				}
			case Variable.Hour:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_Hour.ToString());

					success = true;

					break;
				}
			case Variable.Minute:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_Minute.ToString());

					success = true;

					break;
				}
			case Variable.TimeFormat:
				{
					message = Support.VariableMessageFormatter(variable.ToString(), m_TimeFormat);

					success = true;

					break;
				}
			}

			variableObject.Success = success;
			variableObject.Message = message;

			return variableObject;
		}

		#endregion

		#region Time System Timer

		private class TimeSystemTimer : Timer
		{
			public TimeSystemTimer()
			: base(TimeSpan.Zero, TimeSpan.FromSeconds(m_TimerSpeed))
			{
			}

			protected override void OnTick()
			{
				ComputeTime();

				for (int i = 0; i < NetState.Instances.Count; ++i)
				{
					NetState ns = (NetState)NetState.Instances[i];
					Mobile m = ns.Mobile;

					if (m != null)
					m.CheckLightLevels(false);
				}
			}
		}

		#endregion
	}
}
