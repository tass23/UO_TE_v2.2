using System;
using Server;
using Server.Targeting;
using Server.Mobiles;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
    public class ElixirofVeriteConversion : Item
    {
      
        [Constructable]
        public ElixirofVeriteConversion()
            : base(0x99B)
        {
            Name = "Elixir of Verite Conversion";  

            Hue = 2207;
            Movable = true;
        }

        public override void OnDoubleClick(Mobile from)
        {
                Container backpack = from.Backpack;

                CopperIngot item1 = (CopperIngot)backpack.FindItemByType(typeof(CopperIngot));   
     
             if (item1 != null)                
             {  
          
                BaseIngot m_Ore1 = item1 as BaseIngot;

                int toConsume = m_Ore1.Amount;

                if ( ( m_Ore1.Amount > 499 ) && ( m_Ore1.Amount < 501 ) ) 
                {
                   m_Ore1.Delete();
                   from.SendMessage("You've successfully converted the Metal.");                    
                   from.AddToBackpack(new VeriteIngot(500)); 
                   this.Delete();

                }
                else if ( ( m_Ore1.Amount < 500 ) || ( m_Ore1.Amount > 500 ) )
                {
                      from.SendMessage("You can only convert 500 Copper Ingots at a time.");
                }
              }
             else
             {
                      from.SendMessage("There isn't Copper Ingots in your Backpack.");
             }                 
 
        }

      
        public ElixirofVeriteConversion(Serial serial)
            : base(serial)
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


