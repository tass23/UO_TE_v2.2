using System;
using System.Collections.Generic;

namespace Server.Commands
{
    public class HearAll
    {
        private static List<Mobile> m_HearAll = new List<Mobile>();

        public static void Initialize()
        {
            CommandSystem.Register("hearall", AccessLevel.Counselor, new CommandEventHandler(HearAll_OnCommand));
            EventSink.Speech += new SpeechEventHandler(HearAllOnSpeech);
        }

        public static void HearAllOnSpeech(SpeechEventArgs e)
        {
            if (m_HearAll.Count > 0)
            {
                string msg = String.Format("({0}): {1}", e.Mobile.RawName, e.Speech);

                for (int i = 0; i < m_HearAll.Count; ++i)
                {
                    m_HearAll[i].SendMessage(msg);
                }
            }
        }
        [Usage("HearAll")]
        [Description("Enable or Disable hearing everything in the world.")]
        public static void HearAll_OnCommand(CommandEventArgs e)
        {
            if (m_HearAll.Contains(e.Mobile))
            {
                m_HearAll.Remove(e.Mobile);
                e.Mobile.SendMessage("HearAll deactivated.");
            }
            else
            {
                m_HearAll.Add(e.Mobile);
                e.Mobile.SendMessage("HearAll enabled.");
            }
        }
                
    }
}

