using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
    public class SWAltar : PeerlessAltar
    {
        public override int KeyCount { get { return 3; } }
        public override MasterKey MasterKey { get { return new SWKey(); } }

        public override Type[] Keys
        {
            get
            {
                return new Type[]
				{
					typeof( TatteredSithCloak ), typeof( AntiquatedSaberHilt ), 
					typeof( AncientHolocron ), typeof( CharredRing ), typeof( AncientFocusingCrystal )
				};
            }
        }

        public override BasePeerless Boss { get { return new MarkaRagnos(); } }

        [Constructable]
        public SWAltar(): base(0x207B)
        {
            BossLocation = new Point3D(180, 276, 8);
            TeleportDest = new Point3D(182, 309, 28);
            ExitDest = new Point3D(1763, 393, -50);
        }

        public SWAltar(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}