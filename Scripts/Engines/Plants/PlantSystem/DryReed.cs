using System;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.ContextMenus;
using Server.Engines.Craft;
using Server.Engines.Plants;

namespace Server.Items
{
	public class DryReed : Item//, ICraftable
	{
        private PlantHue m_PlantHue;

        [CommandProperty(AccessLevel.GameMaster)]
        public PlantHue PlantHue
        {
            get { return m_PlantHue; }
            set { m_PlantHue = value; Hue = PlantHueInfo.GetInfo(value).Hue; InvalidateProperties(); }
        }

        public override bool ForceShowProperties { get { return ObjectPropertyList.Enabled; } }
		
        public bool RetainsColorFrom
        {
            get
            {
                return true;
            }
        }
		
		[Constructable]
		public DryReed() : this( 1 )
		{
		}

		[Constructable]
        public DryReed(int amount) : base(0xF42)
		{
            Weight = 1.0;

            //Hue = 0;  
			//Name = "Dry Reed";			
			Stackable = true;
			Amount = amount; 
		}

        public override void AddNameProperty(ObjectPropertyList list)
        {
            PlantHueInfo hueInfo = PlantHueInfo.GetInfo(m_PlantHue);
            list.Add(1112289, "#" + hueInfo.Name);  // ~1_COLOR~ dry reeds
        }
		
		public DryReed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			
			writer.Write((int)m_PlantHue);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
            switch (version)
            {
                case 1:
                    m_PlantHue = (PlantHue)reader.ReadInt();
                    break;
            }
		}
	}
}