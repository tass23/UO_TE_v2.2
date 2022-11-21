using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;
using Server;

namespace Server.Items
{
    public class EmpoweringDeed : Item
    {
		public override bool ForceShowProperties { get { return ObjectPropertyList.Enabled; } }
		
        [Constructable]
        public EmpoweringDeed() : base( 0x0F53 ) //3923
        {
			Weight = 5.0;
			Light = LightType.Circle150;
			LootType = LootType.Blessed;
        }
		
		public override void AddNameProperty( ObjectPropertyList list )
		{
			if (this.Name == null )
			{
				list.Add( "Unknown Empowering Crystal" );
			}
			else
			{
				list.Add (this.Name);
			}
		}

        public EmpoweringDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}