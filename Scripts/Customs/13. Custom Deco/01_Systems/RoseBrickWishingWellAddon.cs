using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class RoseBrickWishingWellAddon : BaseAddon
	{
		private int m_GoldAmt;
		[CommandProperty( AccessLevel.GameMaster)] public int GoldAmt { get { return m_GoldAmt;} set { m_GoldAmt = value;} }

		private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {3224, -2, -4, 0}, {65, -2, -2, 0}, {3204, 1, 4, 0}
			, {1303, 3, 2, 0}, {3203, -1, 4, 0}, {6041, -1, -1, 0}
			, {6042, 1, 0, 0}, {6039, 2, 1, 0}, {6040, 2, -1, 0}
			, {6041, 2, 2, 0}, {6042, 0, -1, 0}, {6040, 1, 2, 0}
			, {6042, 0, -2, 0}, {67, 2, -1, 0}, {6040, -3, 1, 0}
			, {6039, -1, -3, 0}, {6041, 2, 0, 0}, {6040, 0, 2, 0}
			, {3334, -1, 1, 3}, {63, -4, -1, 0}, {6041, -1, 2, 0}
			, {6039, 1, -3, 0}, {3210, 1, -4, 1}, {6041, 2, -2, 0}
			, {6041, -1, 0, 0}, {3204, 4, 3, 0}, {6039, 1, 1, 0}
			, {67, 2, -3, 0}, {1303, 3, -2, 0}, {66, -3, 1, 0}
			, {3338, 1, -3, 1}, {6041, -1, -2, 0}, {6039, 0, 0, 0}
			, {6040, 1, -2, 0}, {3224, 3, 4, 0}, {3210, -3, 2, 0}
			, {3210, 4, -3, 0}, {3203, 3, 4, 0}, {1303, 3, -1, 0}
			, {1303, 3, 1, 0}, {1303, 4, 1, 0}, {3203, 2, -4, 0}
			, {3203, 0, 4, 0}, {3203, 1, 4, 0}, {3203, 1, -4, 0}
			, {3203, 3, -4, 0}, {3272, -4, -3, 0}, {3203, 3, -3, 0}
			, {3339, -1, 1, 1}, {3203, -3, -2, 0}, {3203, -2, -2, 0}
			, {3203, -2, -3, 0}, {3203, -3, 2, 0}, {3203, -2, 2, 0}
			, {3203, 2, 4, 0}, {3203, 4, -2, 0}, {3203, -2, -4, 0}
			, {1303, 4, -1, 0}, {3336, 0, -3, 1}, {81, -4, 0, 10}
			, {81, -2, -2, 5}, {3203, 4, 2, 0}, {3203, 0, -4, 0}
			, {1303, 4, 0, 0}, {81, -2, 2, 5}, {6040, 0, -3, 0}
			, {1303, 3, 0, 0}, {3272, -4, 2, 0}, {65, 2, 3, 0}
			, {67, 2, 1, 0}, {6040, -1, 1, 0}, {67, 2, 2, 0}
			, {6039, -1, 3, 0}, {66, -3, -2, 0}, {6039, 1, 3, 0}
			, {68, -2, -4, 0}, {66, -2, 1, 0}, {66, 1, 3, 0}
			, {66, 0, 3, 0}, {66, -1, 3, 0}, {6042, 2, -3, 0}
			, {6039, 2, 3, 0}, {3224, -2, 4, 0}, {3203, 3, 3, 0}
			, {3224, 3, -4, 0}, {67, -2, -3, 0}, {66, 2, -4, 0}
			, {67, -2, 2, 0}, {66, -1, -4, 0}, {13427, 0, 1, 0}
			, {63, -4, 0, 0}, {6040, -2, 1, 0}, {66, 1, -4, 0}
			, {3203, -2, 4, 0}, {3203, -1, -4, 0}, {67, 2, -2, 0}
			, {67, -2, 3, 0}, {3203, -2, 3, 0}, {6039, 0, 3, 0}
			, {66, 0, -4, 0}, {67, 2, 0, 0}, {63, -4, 1, 0}
			, {13425, 1, -1, 0}, {64, -4, -2, 0}
		};

 
		private static int[,] m_AddOnComplexComponents = new int[,] {
			  {15890, -2, 0, 0, 2477, -1 }
		};

	
		public override BaseAddonDeed Deed { get { return new RoseBrickWishingWellAddonDeed(); } }

		[ Constructable ]
		public RoseBrickWishingWellAddon()
		{
			for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
				AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );
			for (int i = 0; i < m_AddOnComplexComponents.Length / 6; i++)
				AddComplexComponent( (BaseAddon)this, m_AddOnComplexComponents[i,0], m_AddOnComplexComponents[i,1], m_AddOnComplexComponents[i,2], m_AddOnComplexComponents[i,3], m_AddOnComplexComponents[i,4], m_AddOnComplexComponents[i,5] );
		}

		private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource)
		{
			AddComplexComponent(addon, item, xoffset, yoffset, zoffset, hue, lightsource, null);
		}

		private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource, string name)
		{
			AddonComponent ac;
			ac = new AddonComponent(item);
			if (name != null) ac.Name = name;
			if (hue != 0) ac.Hue = hue;
			if (lightsource != -1) ac.Light = (LightType) lightsource;
			addon.AddComponent(ac, xoffset, yoffset, zoffset);
		}

		public override void OnChop( Mobile from )
		{
			int tempgold = m_GoldAmt;
			base.OnChop(from);
			if (this.Deleted)
			{
				Container pack = from.Backpack;
				if (pack != null && tempgold > 0)
				{
					from.SendMessage("You clean the gold out of the well and place it in your pack");
					if (tempgold > 20000) from.AddToBackpack( new BankCheck(tempgold) );
					else from.AddToBackpack( new Gold(tempgold) );
				}
			}
		}

		public override void OnComponentUsed( AddonComponent c, Mobile from )
		{
			if ( (c.ItemID >= 6039 && c.ItemID <= 6044) || (c.ItemID >= 13521 && c.ItemID <= 13525) || (c.ItemID >= 13422 && c.ItemID <= 13445) || (c.ItemID >= 8099 && c.ItemID <= 8138) )
			{
				Container pack = from.Backpack;
				if (pack != null && pack.ConsumeTotal( typeof( Gold ) , 1 ) )
				{
					Effects.SendLocationEffect( c.Location, c.Map, 0x352D, 16, 4 );
					Effects.PlaySound( c.Location, c.Map, 0x364 );
					m_GoldAmt += 1;
					from.SendMessage("You throw a coin into the fountain and make a wish");
				}
				else from.SendMessage("You quickly count the number of coins in the fountain and estamate there is about {0} coins in there", Utility.RandomMinMax( (int)(m_GoldAmt * .8), (int)(m_GoldAmt * 1.2)));
			}
			else from.SendMessage("You quickly count the number of coins in the fountain and estamate there is about {0} coins in there", Utility.RandomMinMax( (int)(m_GoldAmt * .8), (int)(m_GoldAmt * 1.2)));
		}

		public RoseBrickWishingWellAddon(Serial serial) : base(serial){}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 );
			writer.Write( m_GoldAmt );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_GoldAmt = reader.ReadInt();
		}
	}

	public class RoseBrickWishingWellAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new RoseBrickWishingWellAddon(); } }

		[Constructable]
		public RoseBrickWishingWellAddonDeed()
		{
			Name = "Rose Brick Wishing Well";
		}

		public RoseBrickWishingWellAddonDeed(Serial serial) : base(serial){}
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( 0 ); }
		public override void	Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}