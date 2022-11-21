
using System;
using Server.Misc;

namespace Server.Items
{
	public class DoctorsLabCoat : BaseOuterTorso 
	{
        private SkillMod m_SkillMod0;
     		
		[Constructable] 
		public DoctorsLabCoat() : base( 0x2799 ) 
		{ 
			Name = "a Doctor's Lab Coat";
			Hue = 1153;

			DefineMods();
		} 

		private void DefineMods()
		{
            m_SkillMod0 = new DefaultSkillMod(SkillName.Healing, true, 10);
           
        }

		private void SetMods( Mobile wearer )
		{
            wearer.AddSkillMod(m_SkillMod0);

		}

		public override bool OnEquip( Mobile from ) 
		{ 
			SetMods( from );
			return true;  
		} 

		public override void OnRemoved( object parent ) 
		{ 
			if ( parent is Mobile ) 
			{ 
				Mobile m = (Mobile)parent;
				m.RemoveStatMod( "Doctor's Lab Coat" );

                if (m_SkillMod0 != null)
                    m_SkillMod0.Remove();
			} 
		} 

		public override void OnSingleClick( Mobile from ) 
		{ 
			this.LabelTo( from, Name ); 
		}

        public DoctorsLabCoat(Serial serial): base(serial) 
		{ 
			DefineMods();
			
			if ( Parent != null && this.Parent is Mobile ) 
				SetMods( (Mobile)Parent );
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 0 ); 
		} 

		public override void Deserialize(GenericReader reader) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 
		} 
	} 
} 
