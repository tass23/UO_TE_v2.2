using System;
using Server;
using Server.Items;
using Server.Network;
using System.Text;
using Server.Mobiles;
using Server.SkillHandlers;


namespace Server.Engines.XmlSpawner2
{
	public class XmlStarWars : XmlAttachment
	{	
        private string m_StarWars = null;
        public static bool SkillLock = true;

		[CommandProperty( AccessLevel.GameMaster )]
		public string StarWars { get{ return m_StarWars; } set { m_StarWars = value; } }

		public XmlStarWars(ASerial serial) : base(serial)
		{
		}

		[Attachable]
		public XmlStarWars( string StarWars, string name )
		{   
            StarWars = ("true");
			Name = name;
        }

        public override void OnAttach()
        {
            base.OnAttach();

            if (AttachedTo is Mobile)
            {
                // ((Mobile)AttachedTo).AddStatMod(new StatMod(StatType.Dex, "XmlDex" + Name, m_Value, m_Duration));
                if (SkillLock && ((Mobile)AttachedTo).AccessLevel == AccessLevel.Player)
                {
                    bool skillschanged = false;

                    if (((Mobile)AttachedTo).Skills[SkillName.Magery].Base > 50.0)
                    {
                        ((Mobile)AttachedTo).Skills[SkillName.Magery].Base = 50.0;
                        skillschanged = true;
                    }
                    if (((Mobile)AttachedTo).Skills[SkillName.Chivalry].Base > 50.0)
                    {
                        ((Mobile)AttachedTo).Skills[SkillName.Chivalry].Base = 50.0;
                        skillschanged = true;
                    }
                    if (((Mobile)AttachedTo).Skills[SkillName.Necromancy].Base > 50.0)
                    {
                        ((Mobile)AttachedTo).Skills[SkillName.Necromancy].Base = 50.0;
                        skillschanged = true;
                    }
                    if (((Mobile)AttachedTo).Skills[SkillName.AnimalTaming].Base > 50.0)
                    {
                        ((Mobile)AttachedTo).Skills[SkillName.AnimalTaming].Base = 50.0;
                        skillschanged = true;
                    }
                    if (((Mobile)AttachedTo).Skills[SkillName.Spellweaving].Base > 50.0)
                    {
                        ((Mobile)AttachedTo).Skills[SkillName.Spellweaving].Base = 50.0;
                        skillschanged = true;
                    }
                    if (((Mobile)AttachedTo).Skills[SkillName.Mysticism].Base > 50.0)
                    {
                        ((Mobile)AttachedTo).Skills[SkillName.Mysticism].Base = 50.0;
                        skillschanged = true;
                    }
                    if (skillschanged)
                    {
                        ((Mobile)AttachedTo).SendMessage(1365, "You may not use Star Wars if you have more than 50.0 trained in other spell casting skills or taming.");
                        ((Mobile)AttachedTo).SendMessage(1365, "So your skills have been adjusted.");
                    }
                }
            }
        }
               
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize(writer);

			writer.Write( (int) 0 );
			// version 0
			writer.Write(m_StarWars);

		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
			// version 0
			m_StarWars = reader.ReadString();
		}
	}
}
