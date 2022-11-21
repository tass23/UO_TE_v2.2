using System;
using System.Collections.Generic; 
using Server;

namespace Server.Mobiles
{
	public class DeedCrafter : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>(); 
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } } 

		public override NpcGuild NpcGuild{ get{ return NpcGuild.TinkersGuild; } }

		[Constructable]
		public DeedCrafter() : base( "the Deed Crafter" )
		{
			SetSkill( SkillName.Begging, 85.0, 100.0 );
			SetSkill( SkillName.Inscribe, 60.0, 83.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBDeedCrafter() );
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Server.Items.Shirt() );
			AddItem( new Server.Items.LongPants() );
			AddItem( new Server.Items.Boots() );
		}

		public DeedCrafter( Serial serial ) : base( serial )
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