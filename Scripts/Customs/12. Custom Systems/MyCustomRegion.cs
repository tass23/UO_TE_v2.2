using System;
using System.Xml;
using Server;
using Server.Mobiles;
using Server.Spells;
using Server.Spells.Third;
using Server.Spells.Fourth;
using Server.Spells.Fifth;
using Server.Spells.Sixth;
using Server.Spells.Seventh;
using Server.Spells.Eighth;
using Server.Spells.Chivalry;
using Server.SkillHandlers;
using Server.ACC.CSS.Systems.DarkForce;
using Server.ACC.CSS.Systems.LightForce;


namespace Server.Regions
{
	public class DeathMaw : BaseRegion
	{
        public DeathMaw(XmlElement xml, Map map, Region parent)
            : base(xml, map, parent)
		{
		}

                                   /*
								   public override void OnEnter(Mobile m)
                                   {
                                               
                                               m.SendMessage("You have entered the Golden Idol Dungeon.");

                                               base.OnEnter(m);
                                     }

                                    public override void OnExit(Mobile m)
                                    {
            
                                              m.SendMessage("You have left the Golden Idol Dungeon.");

                                              base.OnExit(m);

                                   }
								   */
                                  
	                public override bool AllowHousing( Mobile from, Point3D p )
	                {
		             if ( from.AccessLevel == AccessLevel.Player )
		                      return false;
		              else
			                 return base.AllowHousing( from, p );
		 } 
                                                                      
		public override bool OnBeginSpellCast( Mobile m, ISpell s )
		{
            if ((s is RecallSpell || s is MarkSpell || s is GateTravelSpell || s is SacredJourneySpell) && m.AccessLevel == AccessLevel.Player)
			{
				m.SendMessage( "You cannot cast that spell here." );
				return false;
			}
			else
			{
				return base.OnBeginSpellCast( m, s );
			}
		}
	}
	
	public class SithTrainingFacility : BaseRegion
	{
        public SithTrainingFacility(XmlElement xml, Map map, Region parent)
            : base(xml, map, parent)
		{
		}

                                   
								   public override void OnEnter(Mobile m)
                                   {
                                               
                                               m.SendMessage("You have entered the Sith Training Facility.");

                                               base.OnEnter(m);
                                     }

                                    public override void OnExit(Mobile m)
                                    {
            
                                              m.SendMessage("You have left the Sith Training Facility.");

                                              base.OnExit(m);

                                   }
								   
                                  
	                public override bool AllowHousing( Mobile from, Point3D p )
	                {
		             if ( from.AccessLevel == AccessLevel.Player )
		                      return false;
		              else
			                 return base.AllowHousing( from, p );
		 } 
                                                                      
		public override bool OnBeginSpellCast( Mobile m, ISpell s )
		{
            if ((s is MarkSpell || s is DarkRemembranceSpell || s is RemembranceSpell) && m.AccessLevel == AccessLevel.Player)
			{
				m.SendMessage( "You cannot cast that spell here." );
				return false;
			}
			else
			{
				return base.OnBeginSpellCast( m, s );
			}
		}
	}
}