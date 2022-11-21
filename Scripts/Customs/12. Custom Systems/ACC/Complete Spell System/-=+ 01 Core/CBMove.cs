using System;
using Server;
using Server.Misc;
using Server.Spells;

namespace Server.ACC.CSS
{
	public class CBMove : SpecialMove
	{
		public override SkillName MoveSkill{ get{ return SkillName.Bushido; } }

		public override void CheckGain( Mobile m )
		{
			m.CheckSkill( MoveSkill, RequiredSkill - 12.5, RequiredSkill + 37.5 );	//Per five on friday 02/16/07
		}
	}
}