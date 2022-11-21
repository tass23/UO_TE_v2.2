using System;
using System.Text;
using Server.Gumps;
using Server.Network;
using Server.Commands;

namespace Server.Misc
{
	public class AutoStaffTeam
	{
		private const bool m_Enabled = true;  //Change to false, to simply go back to the original help system.
		public static bool Enabled { get{ return m_Enabled; } }
	}
}