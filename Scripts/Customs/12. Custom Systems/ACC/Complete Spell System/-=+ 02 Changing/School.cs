using System;
using System.Collections;

namespace Server.ACC.CSS
{
	[Flags]
	public enum School
	{
		Invalid  		= 0x00000000,
		Magery   		= 0x00000001,
		Necro    		= 0x00000002,
		Chivalry 		= 0x00000004,
		Druid    		= 0x00000020,
		Avatar   		= 0x00000040,
		Bard     		= 0x00000080,
		Cleric   		= 0x00000100,
		Ranger   		= 0x00000200,
		Rogue    		= 0x00000400,
		Undead   		= 0x00000800,
        Ancient  		= 0x00001000,
		Mysticism 		= 0x00001200,
		Spellweaving	= 0x00001400,
		LightForce		= 0x00001600,
		DarkForce		= 0x00001800,
		Vampire			= 0x00002000,
		Werewolf		= 0x00002200
	}
}