using System;
using Server.Misc;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x1515, 0x1530 )] 
	public class ObiDiEnse : Obi 
	{
        private SkillMod m_SkillMod0;

        public override int LabelNumber{ get{ return 1112406; } } // Obi Di Ense 
		public override int ArtifactRarity{ get{ return 11; } }

		public override int InitMinHits{ get{ return 150; } }
		public override int InitMaxHits{ get{ return 150; } }
		
		[Constructable] 
		public ObiDiEnse() : base( 0x27A0 ) 
		{
            Hue = 0;
            Attributes.BonusInt = 5;
            Attributes.NightSight = 1;
            SkillBonuses.SetValues(0, SkillName.Focus, 5.0);
		} 

		private void DefineMods()
		{
			m_SkillMod0 = new DefaultSkillMod( SkillName.Stealth, true, 20 ); 
			
		}

		private void SetMods( Mobile wearer )
		{			
			wearer.AddSkillMod( m_SkillMod0 ); 
			
		}

		public override bool OnEquip( Mobile from ) 
		{ 
			SetMods( from );
			return true;  
		} 

		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendLocalizedMessage( 1042083 ); // You can not dye that.
			return false;
		}

		public override void OnRemoved( object parent ) 
		{ 
			if ( parent is Mobile ) 
			{ 
				if ( m_SkillMod0 != null ) 
					m_SkillMod0.Remove(); 		
			} 
		} 

		public override void OnSingleClick( Mobile from ) 
		{ 
			this.LabelTo( from, Name ); 
		} 

		public ObiDiEnse( Serial serial ) : base( serial ) 
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
