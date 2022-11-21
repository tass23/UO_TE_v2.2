using System;
using System.IO;
using System.Text;
using System.Collections;

using Server;

namespace Server.SG
{
    public class SGEntry
    {
        private int m_SGX;
        public int SGX
        { get { return m_SGX; } set { m_SGX = value; } }

        private int m_SGY;
        public int SGY
        { get { return m_SGY; } set { m_SGY = value; } }

        private int m_SGZ;
        public int SGZ
        { get { return m_SGZ; } set { m_SGZ = value; } }

        private string m_SGFacing;
        public string SGFacing
        { get { return m_SGFacing; } set { m_SGFacing = value; } }

        private int m_SGStyle;
        public int SGStyle
        { get { return m_SGStyle; } set { m_SGStyle = value; } }

        private bool m_SGCanBeUsed;
        public bool SGCanBeUsed
        { get { return m_SGCanBeUsed; } set { m_SGCanBeUsed = value; } }

        private bool m_SGBeingUsed;
        public bool SGBeingUsed
        { get { return m_SGBeingUsed; } set { m_SGBeingUsed = value; } }

        private bool m_SGEnergy;
        public bool SGEnergy
        { get { return m_SGEnergy; } set { m_SGEnergy = value; } }

        private bool m_SGHidden;
        public bool SGHidden
        { get { return m_SGHidden; } set { m_SGHidden = value; } }

        private string m_SGDiscovered;
        public string SGDiscovered
        { get { return m_SGDiscovered; } set { m_SGDiscovered = value; } }

        private string m_SGLocationName;
        public string SGLocationName
        { get { return m_SGLocationName; } set { m_SGLocationName = value; } }

        private int m_SGFacetCode;
        public int SGFacetCode
        { get { return m_SGFacetCode; } set { m_SGFacetCode = value; } }

        private int m_SGAddressCode1;
        public int SGAddressCode1
        { get { return m_SGAddressCode1; } set { m_SGAddressCode1 = value; } }

        private int m_SGAddressCode2;
        public int SGAddressCode2
        { get { return m_SGAddressCode2; } set { m_SGAddressCode2 = value; } }

        private int m_SGAddressCode3;
        public int SGAddressCode3
        { get { return m_SGAddressCode3; } set { m_SGAddressCode3 = value; } }

        private int m_SGAddressCode4;
        public int SGAddressCode4
        { get { return m_SGAddressCode4; } set { m_SGAddressCode4 = value; } }

        private int m_SGAddressCode5;
        public int SGAddressCode5
        { get { return m_SGAddressCode5; } set { m_SGAddressCode5 = value; } }

        public SGEntry(int sgx, int sgy, int sgz, string sgfacing, int sgstyle, bool sgcanbeused, bool sgbeingused, bool sgenergy, bool sghidden, string sgdiscovered, string sglocationname, int sgfacetcode, int sgaddresscode1, int sgaddresscode2, int sgaddresscode3, int sgaddresscode4, int sgaddresscode5)
        {
            m_SGX = sgx;
            m_SGY = sgy;
            m_SGZ = sgz;
            m_SGFacing = sgfacing;
            m_SGStyle = sgstyle;
            m_SGCanBeUsed = sgcanbeused;
            m_SGBeingUsed = sgbeingused;
            m_SGEnergy = sgenergy;
            m_SGHidden = sghidden;
            m_SGDiscovered = sgdiscovered;
            m_SGLocationName = sglocationname;
            m_SGFacetCode = sgfacetcode;
            m_SGAddressCode1 = sgaddresscode1;
            m_SGAddressCode2 = sgaddresscode2;
            m_SGAddressCode3 = sgaddresscode3;
            m_SGAddressCode4 = sgaddresscode4;
            m_SGAddressCode5 = sgaddresscode5;
        }
    }
}