using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	public class EvilOrcHelm : OrcHelm
	{
		public override int LabelNumber { get { return 1062021; } } // an evil orc helm

		public override bool UseIntOrDexProperty
		{
			get
			{
				if (Core.SA && !(this.Parent is Mobile))
					return true;

				return base.UseIntOrDexProperty;
			}
		}

		public override int IntOrDexPropertyValue { get { return -10; } }
        
		[Constructable]
		public EvilOrcHelm()
			: base()
		{
			Hue = 0x96E;
			Attributes.BonusStr = 10;
			Attributes.BonusInt = IntOrDexPropertyValue;
			Attributes.BonusDex = IntOrDexPropertyValue;
		}

		public override bool OnEquip(Mobile from)
		{
			if (from.RawInt > from.RawDex)
				Attributes.BonusDex = 0;
			else
				Attributes.BonusInt = 0;
			
			Titles.AwardKarma(from, -22, true);

			return base.OnEquip(from);
		}

		public override void OnRemoved(object parent)
		{
			base.OnRemoved(parent);

			if (parent is Mobile)
			{
				Attributes.BonusInt = IntOrDexPropertyValue;
				Attributes.BonusDex = IntOrDexPropertyValue;
			}
		}

		public EvilOrcHelm(Serial serial)
			: base(serial)
		{
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