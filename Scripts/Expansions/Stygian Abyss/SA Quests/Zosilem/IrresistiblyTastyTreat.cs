using System;
using Server.Mobiles;
using Server.Items;
using Server.Targeting;
using Server;

namespace Server.Items
{
    public class IrresistiblyTastyTreat : Item
    {
        public override int LabelNumber{ get{ return 1113005; } } 

        private bool m_Used;   

        [CommandProperty( AccessLevel.GameMaster )]
	public bool Used
	{
		get{ return m_Used; }
		set{ m_Used = value; }
	}
         
        [Constructable]
        public IrresistiblyTastyTreat() : base(0xF7E)
        {
                     
            m_Used = false;
        }
   
        public override void AddNameProperties( ObjectPropertyList list )
        {
              base.AddNameProperties( list );
              
              list.Add(1113213);
              list.Add(1113216);
              list.Add(1113217); 
              list.Add(1070722, "Duration: 10 Min"); 
              list.Add(1042971, "Cooldown: 2 Hours"); 

        } 


        public override void OnDoubleClick(Mobile from)
        {
            
            if (!m_Used)
            { 
                from.SendMessage("Which animal you want to Targhet ?"); 
  
                from.Target = new InternalTarget(this);
            }
            else
            {
               from.SendLocalizedMessage(1113051);                
            }        
        }

        private class InternalTarget : Target
        {
            private IrresistiblyTastyTreat m_Tasty;
 
            private int Base1;
            private int Base2;

            private int Change1;
            private int Change2;

            private int Change3;
            private int Change4;
            private int Change5;  
        
            private int Change6;
            private int Change7;
            private int Change8;   

            public InternalTarget(IrresistiblyTastyTreat tasty) : base(10, false, TargetFlags.None)
            {
                m_Tasty = tasty;
            }

           protected override void OnTarget(Mobile from, object targeted)
           {
                PlayerMobile pm = from as PlayerMobile;

                if (m_Tasty.Deleted)
                    return;
             if ( targeted is Mobile )
	     {        
                 
                if ( targeted is BaseCreature )
                {   
                    BaseCreature creature = (BaseCreature)targeted;

                    Base1 = (creature.DamageMin); 
                    Base2 = (creature.DamageMax); 

                    Change1 = (int)((creature.DamageMin) * 1.10); 
                    Change2 = (int)((creature.DamageMax) * 1.10);   

                    Change6 = (creature.RawStr); 
                    Change7 = (creature.RawDex);
                    Change8 = (creature.RawInt);
  
                    Change3 = (int)((creature.RawStr) * 1.15); 
                    Change4 = (int)((creature.RawDex) * 1.15);
                    Change5 = (int)((creature.RawInt) * 1.15);
              
 
                    if ( (creature.Controlled || creature.Summoned ) && (from == creature.ControlMaster) && !(creature.Sleep) )
                    {
                        creature.FixedParticles( 0x376A, 9, 32, 5005, EffectLayer.Waist );
	                creature.PlaySound( 0x1E9 );

                        creature.SetDamage( Change1, Change2 );
 
                        creature.RawStr = Change3;
			creature.RawDex = Change4;
                        creature.RawInt = Change5;

                        from.SendMessage("You have increased the Stats of your pet by 15% and the Damage by 10% for 10 Minutes !!");  
                        m_Tasty.m_Used = true;
                        creature.Sleep = true; 

                       
                        Timer.DelayCall(TimeSpan.FromMinutes(10.0), delegate()
                        {
                        creature.SetDamage( Base1, Base2 );                     
                      
                        creature.RawStr = Change6;
			creature.RawDex = Change7;
                        creature.RawInt = Change8;
                       
                        m_Tasty.m_Used = true; 
                        creature.Sleep = false;
                        from.SendMessage("The effect of Irresistibly Tasty Treat is Finish !");   

                           Timer.DelayCall(TimeSpan.FromMinutes(120.0), delegate()
                           {
                           m_Tasty.m_Used = false;                                                
                           });
                                               
                        });
                                             
                    }
                    else if ( (creature.Controlled || creature.Summoned ) && (from == creature.ControlMaster) && (creature.Sleep) )
                    {
                         from.SendLocalizedMessage(502676);
                    }                     
                    else 
                    {
                         from.SendLocalizedMessage(1113049);
                    }
                }
                else
                {
                   from.SendLocalizedMessage(500329);            
                }                      
             }  
          }          
        }

        public IrresistiblyTastyTreat(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write( (bool) m_Used ); 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_Used = reader.ReadBool();
        }
    }
}