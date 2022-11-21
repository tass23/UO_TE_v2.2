using System;
using System.Collections;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server;

namespace Server.Items
{
	public class GorgonLens : Item
	{
		private int m_Uses;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Uses
		{
			get{ return m_Uses; }
			set{ m_Uses = value; InvalidateProperties(); }
		}
		
		
		[Constructable]
		public GorgonLens() : base( 9908 )
		{
			Name = "Gorgon Lens";
			Weight = 1.0;
			Hue = 1364;
            Movable = true;  
            LootType = LootType.Blessed; 
			
			Uses = 10;
		}

		public GorgonLens( Serial serial ) : base( serial )
		{
		}
		
		public override void AddNameProperties( ObjectPropertyList list )
 		{
 			base.AddNameProperties( list );
 			list.Add( 1060658, "Uses remaining\t{0}", m_Uses.ToString() );
 		}


        public override void OnDoubleClick(Mobile from)
        {
           
                from.SendLocalizedMessage(1112596); //Which item do you wish to enhance with Gorgon Lenses?
  
                from.Target = new InternalTarget(this);
                 
        }

        private class InternalTarget : Target
        {
            private GorgonLens m_Gorg;

            
            public InternalTarget(GorgonLens gorg) : base(10, false, TargetFlags.None)
            {
                m_Gorg = gorg;
            }

           protected override void OnTarget(Mobile from, object targeted)
           {
                PlayerMobile pm = from as PlayerMobile;

                if (m_Gorg.Deleted)
                    return;

                if ( targeted is BaseArmor )
                {
                    m_Gorg.Visible = false;
                    m_Gorg.Weight = 0;

                    from.SendLocalizedMessage(1112595); //You enhance the item with Gorgon Lenses!
  
                }
                else
                {
                    from.SendLocalizedMessage(1112594); //You cannot place gorgon lenses on this.  
                }                      
            }  
              
        }  


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			
			writer.Write( (int) m_Uses );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
			switch ( version )
			{
				case 0:
				{
					m_Uses = (int)reader.ReadInt();

					break;
				}
			}
		}
		
		public bool ConsumeUse( Mobile from )
		{
			--Uses;
			
			if ( Uses == 0 )
			{
				from.SendLocalizedMessage(1112600); //Your lenses crumble. You are no longer protected from Medusa's gaze!
				Delete();

				return false;
			}
			
			return true;
		}
	}
}
