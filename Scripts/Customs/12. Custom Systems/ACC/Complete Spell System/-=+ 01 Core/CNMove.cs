using System;
using Server;
using Server.Misc;
using Server.Spells;

namespace Server.ACC.CSS
{
	public class CNMove : SpecialMove
	{
		public override SkillName MoveSkill{ get{ return SkillName.Ninjitsu; } }

		public override void CheckGain( Mobile m )
		{
			m.CheckSkill( MoveSkill, RequiredSkill - 12.5, RequiredSkill + 37.5 );	//Per five on friday 02/16/07
		}
	}
}