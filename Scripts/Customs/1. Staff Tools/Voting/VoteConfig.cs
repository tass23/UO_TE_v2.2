using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using Server;
using Server.Misc;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Accounting;

namespace Server.Voting
{
	/// <summary>
	/// Provides some standard configuration options.
	/// </summary>
	[PropertyObject]
	public sealed class VoteConfig
	{
		/// <summary>
		/// The default VoteSite Name to use on new VoteSite Profiles
		/// </summary>
		public static string __DefaultName = "The Expanse";

		/// <summary>
		/// The default URL to launch in the request senders' browser.
		/// </summary>
		public static string __DefaultURL = "http://www.uoexpanse.com";

		/// <summary>
		/// The default CoolDown delay between vote requests. 
		/// A new request will be rejected if the request sender has previously made a vote request within this amount of time.
		/// </summary>
		public static TimeSpan __DefaultCoolDown = TimeSpan.FromHours(24.0);

		 /* * * * * * * * * * * * * * *||||* * * * * * * * * * * * * * * *\
		 * ============================================================== *
		 * DO NOT EDIT BELOW HERE - YOU REALLY DON'T NEED TO, TRUST ME :) *
		 * ============================================================== *
		 \* * * * * * * * * * * * * * *||||* * * * * * * * * * * * * * * */

		public static void Initialize()
		{
			EventSink.WorldSave += new WorldSaveEventHandler(EventSink_WorldSave);
			EventSink.ServerStarted += new ServerStartedEventHandler(EventSink_ServerStarted);
		}

		private static void EventSink_ServerStarted()
		{
			Instance.Deserialize();
		}

		private static void EventSink_WorldSave(WorldSaveEventArgs e)
		{
			Instance.Serialize();
		}

		private static VoteConfig _Instance = new VoteConfig();
		public static VoteConfig Instance
		{
			get
			{
				if (_Instance == null)
					_Instance = new VoteConfig();

				return _Instance;
			}
		}

		public VoteConfig()
		{
			_Instance = this;
		}

		private string _DefaultName = __DefaultName;
		/// <summary>
		/// The default VoteSite Name to use on new VoteSite Profiles
		/// </summary>
		[CommandProperty(AccessLevel.GameMaster)]
		public string DefaultName { get { return _DefaultName; } set { _DefaultName = value; } }

		private string _DefaultURL = __DefaultURL;
		/// <summary>
		/// The default URL to launch in the request senders' browser.
		/// </summary>
		[CommandProperty(AccessLevel.GameMaster)]
		public string DefaultURL { get { return _DefaultURL; } set { _DefaultURL = value; } }

		private TimeSpan _DefaultCoolDown = __DefaultCoolDown;
		/// <summary>
		/// The default CoolDown delay between vote requests. 
		/// A new request will be rejected if the request sender has previously made a vote request within this amount of time.
		/// </summary>
		[CommandProperty(AccessLevel.GameMaster)]
		public TimeSpan DefaultCoolDown { get { return _DefaultCoolDown; } set { _DefaultCoolDown = value; } }

		public void Serialize()
		{
			//Console.WriteLine("[Vote System]: Saving Config...");

			FileInfo info = new FileInfo("Data\\VoteSystem.cfg");

			if (!info.Exists)
				info.Create().Close();

			using(FileStream fs = info.Open(FileMode.Truncate, FileAccess.Write))
			{
				BinaryFileWriter bin = new BinaryFileWriter(fs, true);
				
				bin.Write((int)0);

				bin.Write((string)_DefaultName);
				bin.Write((string)_DefaultURL);
				bin.Write((TimeSpan)_DefaultCoolDown);

				bin.Close();
			}

			//Console.WriteLine("[Vote System]: Done.");
		}

		public void Deserialize()
		{
			//Console.WriteLine("[Vote System]: Loading Config...");

			FileInfo info = new FileInfo("Data\\VoteSystem.cfg");

			if (!info.Exists)
				info.Create().Close();

			if (info.Length == 0)
				return;

			using (BinaryReader br = new BinaryReader(info.OpenRead()))
			{
				BinaryFileReader bin = new BinaryFileReader(br);
	
				int version = bin.ReadInt();

				_DefaultName = bin.ReadString();
				_DefaultURL = bin.ReadString();
				_DefaultCoolDown = bin.ReadTimeSpan();

				bin.Close();
			}

			//Console.WriteLine("[Vote System]: Done.");
		}
	}
}
