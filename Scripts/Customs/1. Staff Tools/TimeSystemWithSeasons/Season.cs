using System;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Commands;

namespace Server.Commands
{
	public class SetSeason
	{
		public static void Initialize()
		{
			CommandSystem.Register("Season", AccessLevel.Seer, new CommandEventHandler(Season_OnCommand));
		}

		public static void Season_OnCommand(CommandEventArgs e)
		{
			Map map;
			Mobile mob = e.Mobile;
			if(e.Length == 1)
			{
				for(int i = 1; i < 5; i++)
				{
					map = Map.AllMaps[i];
					map.Season = (e.GetInt32(0));

					mob.Send(SeasonChange.Instantiate(map.Season, true));

				}
			}
			else
			{
				for(int i = 1; i < 5; i++)
				{
					map = Map.AllMaps[i];
					map.Season = (TimeSystem.System.DetermineSeason());

					foreach(NetState state in NetState.Instances)
					{
						Mobile m = state.Mobile;
						if(m != null)
						{
							state.Send(SeasonChange.Instantiate(m.GetSeason(), true));
							m.SendEverything();
						}
					}
				}
			}
		}
	}
}
