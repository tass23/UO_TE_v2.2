using System;
using Server.Mobiles;
using Server.Items;
using Server.Targeting;
using Server;

namespace Server.Items
{
    public class VialofArmorEssence : Item
    {
        public override int LabelNumber{ get{ return 1113018; } }        

        private bool m_Used; 

        [CommandProperty( AccessLevel.GameMaster )]
	public bool Used
	{
		get{ return m_Used; }
		set{ m_Used = value; }
	}
         
        [Constructable]
        public VialofArmorEssence() : base(0x5722)
        {       
            m_Used = false;
        }
   
        public override void AddNameProperties( ObjectPropertyList list )
        {
              base.AddNameProperties( list );
              
              list.Add(1113213);
              list.Add(1113219);
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
               from.SendMessage("You must wait until the Effect of Vial of Armor Essence wears off !");                  
            }        
        }

        private class InternalTarget : Target
        {
            private VialofArmorEssence m_Tasty;

        
            public InternalTarget(VialofArmorEssence tasty) : base(10, false, TargetFlags.None)
            {
                m_Tasty = tasty;
            }

           protected override void OnTarget(Mobile from, object targeted)
           {
                PlayerMobile pm = from as PlayerMobile;

                if (m_Tasty.Deleted)
                    return;

                if ( targeted is BaseCreature )
                {
                    BaseCreature creature = (BaseCreature)targeted;

                    if ( (creature.Controlled || creature.Summoned ) && (from == creature.ControlMaster) && !(creature.Sleep) )
                    {
                        creature.FixedParticles( 0x373A, 10, 15, 5018, EffectLayer.Waist );
	                creature.PlaySound( 0x1EA );

                        ResistanceMod mod = new ResistanceMod( ResistanceType.Physical, +15 );
                        ResistanceMod mod1 = new ResistanceMod( ResistanceType.Fire, +10 );
                        ResistanceMod mod2 = new ResistanceMod( ResistanceType.Cold, +10 );
                        ResistanceMod mod3 = new ResistanceMod( ResistanceType.Poison, +10 );
                        ResistanceMod mod4 = new ResistanceMod( ResistanceType.Energy, +10 );
			creature.AddResistanceMod( mod );
                        creature.AddResistanceMod( mod1 ); 
                        creature.AddResistanceMod( mod2 );
                        creature.AddResistanceMod( mod3 );
                        creature.AddResistanceMod( mod4 ); 

                        from.SendMessage("You have increased the Damage Absorption of your pet by 10% for 10 Minutes !!");  
                        m_Tasty.m_Used = true;
                        creature.Sleep = true; 

                       
                        Timer.DelayCall(TimeSpan.FromMinutes(10.0), delegate()
                        {
                        
                        ResistanceMod mod5 = new ResistanceMod( ResistanceType.Physical, -15 );
                        ResistanceMod mod6 = new ResistanceMod( ResistanceType.Fire, -10 );
                        ResistanceMod mod7 = new ResistanceMod( ResistanceType.Cold, -10 );
                        ResistanceMod mod8 = new ResistanceMod( ResistanceType.Poison, -10 );
                        ResistanceMod mod9 = new ResistanceMod( ResistanceType.Energy, -10 );
			creature.AddResistanceMod( mod5 );
                        creature.AddResistanceMod( mod6 ); 
                        creature.AddResistanceMod( mod7 );
                        creature.AddResistanceMod( mod8 );
                        creature.AddResistanceMod( mod9 ); 
                        creature.PlaySound( 0x1EB );
                       
                        m_Tasty.m_Used = true; 
                        creature.Sleep = false;
                        from.SendMessage("The effect of Vial of Armor Essence is finish !");   

                           Timer.DelayCall(TimeSpan.FromMinutes(120.0), delegate()
                           {
                           m_Tasty.m_Used = false;                                                
                           });
                                               
                        });
                                             
                    }
                    else if ( (creature.Controlled || creature.Summoned ) && (from == creature.ControlMaster) && (creature.Sleep) )
                    {
                         from.SendMessage("Pet already under the influence of Vial of Armor Essence !");     
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

        public VialofArmorEssence(Serial serial)
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