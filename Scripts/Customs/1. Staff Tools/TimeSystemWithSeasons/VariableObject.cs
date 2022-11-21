using System;
using Server;
using System.Collections;

namespace Server.TimeSystem
{
    public class VariableObject
    {
        #region Private Variables

        private bool m_Success;

        private string m_Message;

        #endregion

        #region Public Variables

        public bool Success
        {
            get
            {
                return m_Success;
            }
            set
            {
                m_Success = value;
            }
        }

        public string Message
        {
            get
            {
                return m_Message;
            }
            set
            {
                m_Message = value;
            }
        }

        #endregion
    }
}
