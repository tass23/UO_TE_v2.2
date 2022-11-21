using System;
using Server;
using Server.Mobiles;

namespace Server.Mobiles
{
    public interface IFreezable
    {
        void OnRequestedAnimation(Mobile from);
    }
}