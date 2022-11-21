using System;
using System.Collections;
using System.Collections.Generic;
using Server;

namespace Server.Mobiles
{
	public class HomeDecoSalesPerson : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		[Constructable]
		public HomeDecoSalesPerson() : base( "Home Deco Salesperson" )
		{
			//SetSkill( SkillName.Camping, 45.0, 68.0 );
			//SetSkill( SkillName.Tactics, 45.0, 68.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBHomeDeco() );

			//if ( IsTokunoVendor )
				//m_SBInfos.Add( new SBSEHats() );
		}

		public HomeDecoSalesPerson( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}