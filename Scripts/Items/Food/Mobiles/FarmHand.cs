using System;
using System.Collections;
using System.Collections.Generic;
using Server;

namespace Server.Mobiles
{
	public class FarmHelper : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		[Constructable]
		public FarmHelper() : base( "the Farm Hand" )
		{
			SetSkill( SkillName.Lumberjacking, 80.0, 100.0 );
			SetSkill( SkillName.TasteID, 80.0, 100.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBFarmHand() );
		}

		public override VendorShoeType ShoeType
		{
			get{ return VendorShoeType.Sandals; }
		}

		public override int GetShoeHue()
		{
			return 0;
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Server.Items.WideBrimHat( Utility.RandomNeutralHue() ) );
		}

		public FarmHelper( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
}