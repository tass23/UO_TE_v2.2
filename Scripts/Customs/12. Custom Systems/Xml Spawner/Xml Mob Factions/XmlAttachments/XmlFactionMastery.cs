using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Spells;
using System.Collections;
using System.Text;

namespace Server.Engines.XmlSpawner2
{
    public class XmlFactionMastery : XmlAttachment
    {
        private int m_Chance = 20;       // 20% hitchance by default
        private double m_FactionScaling = 0.002;    // faction multiplier for damage bonus. By default, 0.002% increase in damage per total faction level
        private int m_PercentCap = 200;    // maximum total percent damage bonus
        private int m_PercentIncrease = 50;         // base percentage damage increase
        private string m_Enemy;
        private XmlMobFactions.GroupTypes m_EnemyType = XmlMobFactions.GroupTypes.End_Unused;

        [CommandProperty( AccessLevel.GameMaster )]
        public int Chance { get{ return m_Chance; } set { m_Chance = value; } }

        [CommandProperty( AccessLevel.GameMaster )]
        public int PercentIncrease { get{ return m_PercentIncrease; } set { m_PercentIncrease = value; } }

        [CommandProperty( AccessLevel.GameMaster )]
        public int PercentCap { get{ return m_PercentCap; } set { m_PercentCap = value; } }

        [CommandProperty( AccessLevel.GameMaster )]
        public double FactionScaling { get { return m_FactionScaling; } set { m_FactionScaling  = value; } }

        [CommandProperty( AccessLevel.GameMaster )]
        public string EnemyFaction {
            get { return m_Enemy; }
            set {

                // look up the group type
                try{
                if(value == "?")
                {
                    // randomly pick one
                    m_EnemyType = (XmlMobFactions.GroupTypes)Utility.Random((int)XmlMobFactions.GroupTypes.End_Unused);
                } else
                    m_EnemyType = (XmlMobFactions.GroupTypes)Enum.Parse(typeof(XmlMobFactions.GroupTypes),value,true);
                } catch{}

                m_Enemy = m_EnemyType.ToString();
            }
        }


        // These are the various ways in which the message attachment can be constructed.
        // These can be called via the [addatt interface, via scripts, via the spawner ATTACH keyword.
        // Other overloads could be defined to handle other types of arguments

        // a serial constructor is REQUIRED
        public XmlFactionMastery(ASerial serial) : base(serial)
        {
        }

        [Attachable]
        public XmlFactionMastery(string enemyfaction)
        {
            EnemyFaction = enemyfaction;
        }

        [Attachable]
        public XmlFactionMastery(string enemyfaction,int basepercentincrease )
        {
            m_PercentIncrease = basepercentincrease;
            EnemyFaction = enemyfaction;
        }

        [Attachable]
        public XmlFactionMastery(string enemyfaction,int hitchance, int basepercentincrease )
        {
            m_Chance = hitchance;
            m_PercentIncrease = basepercentincrease;
            EnemyFaction = enemyfaction;
        }

        [Attachable]
        public XmlFactionMastery(string enemyfaction, int hitchance, int basepercentincrease, double expiresin)
        {
            m_Chance = hitchance;
            m_PercentIncrease = basepercentincrease;
            Expiration = TimeSpan.FromMinutes(expiresin);
            EnemyFaction = enemyfaction;
        }
        
        [Attachable]
        public XmlFactionMastery(string enemyfaction, int hitchance, int basepercentincrease, int percentcap, double expiresin)
        {
            m_Chance = hitchance;
            m_PercentIncrease = basepercentincrease;
            Expiration = TimeSpan.FromMinutes(expiresin);
            EnemyFaction = enemyfaction;
            PercentCap = percentcap;
        }

        public override void OnAttach()
		{
		    base.OnAttach();

		    if(AttachedTo is Mobile)
            {
                Mobile m = AttachedTo as Mobile;
                Effects.PlaySound( m, m.Map, 516 );
                m.SendMessage(String.Format("You gain the power of Faction Mastery over {0}",EnemyFaction));
            }
		}
		
		private int ComputeIncrease(int faclevel)
		{
              // calculate the additional damage
              int val = (int)(PercentIncrease + faclevel*FactionScaling);
              
              if(val > PercentCap) val = PercentCap;
              
              return val;
		}


        // note that this method will be called when attached to either a mobile or a weapon
        // when attached to a weapon, only that weapon will do additional damage
        // when attached to a mobile, any weapon the mobile wields will do additional damage
        public override void OnWeaponHit(Mobile attacker, Mobile defender, BaseWeapon weapon, int damageGiven)
        {

            if(m_Chance <= 0 || Utility.Random(100) > m_Chance)
                return;

            if(defender != null && attacker != null && m_EnemyType != XmlMobFactions.GroupTypes.End_Unused)
            {

                // check to the owner's faction levels
                ArrayList list = XmlAttach.FindAttachments(XmlAttach.MobileAttachments,attacker,typeof(XmlMobFactions),"Standard");

                if(list != null && list.Count > 0)
                {
                    XmlMobFactions x = list[0] as XmlMobFactions;

                    double increase = 0;

                    // go through all of the factions the defender might belong to
                    ArrayList glist = XmlMobFactions.FindGroups(defender);

                    if(glist != null && glist.Count > 0)
                    {
                        foreach( XmlMobFactions.GroupTypes targetgroup in glist)
                        {
                            // found the group that matches the enemy type for this attachment
                            if(targetgroup == m_EnemyType)
                            {
                            	// get the percent damage increase based upon total faction level of opponent groups
                                int totalfac = 0;
                                
                                // get the target enemy group
                                XmlMobFactions.Group g = XmlMobFactions.FindGroup(m_EnemyType);
                
                                if(g.Opponents != null)
                                {
                                    // go through all of the opponents of this group
                                    for(int i=0;i<g.Opponents.Length;i++)
                                    {
                                        // and sum the faction levels
                                        try{
                                        totalfac += (int)(x.GetFactionLevel(g.Opponents[i].GroupType)*g.OpponentGain[i]);
                                        } catch{}
                                    }
                                }
                                
                                // what is the damage increase based upon the total opponent faction level
                                increase = (double)ComputeIncrease(totalfac)/100.0;

                                break;

            			    }
        			    }
    			    }

    			    if(increase > 0)
                    {
                        // apply the additional damage if any
                        defender.Damage( (int) (damageGiven*increase), attacker );
                    }
			    }
            }
        }

        public override void OnDelete()
        {
            base.OnDelete();

            if(AttachedTo is Mobile)
            {
                Mobile m = AttachedTo as Mobile;
                if(!m.Deleted)
                {
                    Effects.PlaySound( m, m.Map, 958 );
                    m.SendMessage(String.Format("Your power of Faction Mastery over {0} fades..",EnemyFaction));
                }
            }
        }

        public override void Serialize( GenericWriter writer )
		{
            base.Serialize(writer);

            writer.Write( (int) 0 );
            // version 0
            writer.Write(m_PercentIncrease);
            writer.Write(m_PercentCap);
            writer.Write(m_Chance);
            writer.Write(m_Enemy);
            writer.Write(m_FactionScaling);
        }

        public override void Deserialize(GenericReader reader)
		{
		    base.Deserialize(reader);

            int version = reader.ReadInt();
            // version 0
            m_PercentIncrease = reader.ReadInt();
            m_PercentCap = reader.ReadInt();
            m_Chance = reader.ReadInt();
            EnemyFaction = reader.ReadString();
            m_FactionScaling = reader.ReadDouble();
		}

		public override string OnIdentify(Mobile from)
		{
		
            if(from == null)
            {
                if(Expiration > TimeSpan.Zero)
                {
                    return String.Format("Faction Mastery : increased damage vs {0}, {1}% hitchance, expires in {2} mins",
                        m_Enemy, Chance, Expiration.TotalMinutes);
                } else
                {
                    return String.Format("Faction Mastery : increased damage vs {0}, {1}% hitchance",m_Enemy, Chance);
                }
            }

            string msg = null;
            string raisestr = "improve ";
            int percentincrease = 0;

            // compute the damage increase based upon the owner's faction level for the specified enemy type
            ArrayList list = XmlAttach.FindAttachments(XmlAttach.MobileAttachments, from, typeof(XmlMobFactions),"Standard");
            if(list != null && list.Count > 0)
            {
                XmlMobFactions x = list[0] as XmlMobFactions;

                // get the percent damage increase based upon total faction level of opponent groups
                int totalfac = 0;
                
                // get the target enemy group
                XmlMobFactions.Group g = XmlMobFactions.FindGroup(m_EnemyType);

                if(g != null && g.Opponents != null)
                {
                    // go through all of the opponents
                    for(int i=0;i<g.Opponents.Length;i++)
                    {
                        try{
                        totalfac += (int)(x.GetFactionLevel(g.Opponents[i].GroupType)*g.OpponentGain[i]);
                        raisestr = String.Format("{0}{1}, ",raisestr, g.Opponents[i].GroupType);
                        } catch {}
                    }
                }

                percentincrease = ComputeIncrease(totalfac);

            }
            


            if(Expiration > TimeSpan.Zero)
            {
                msg = String.Format("Faction Mastery : +{3}% damage vs {0} ({4}% max), {1}% hitchance, expires in {2} mins",
                    m_Enemy, Chance, Expiration.TotalMinutes, percentincrease, PercentCap);
            } else
            {
                msg = String.Format("Faction Mastery : +{2}% damage vs {0} ({3}% max), {1}% hitchance",m_Enemy, Chance, percentincrease, PercentCap);
            }

            msg = String.Format("{0} : {1}faction to enhance damage.",msg,raisestr);

            return msg;
		}
    }
}
