using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class SailBoatEast1 : BaseAddon
	{
		[ Constructable ]
		public SailBoatEast1()
		{
			//Boat Hull
			AddonComponent ac = null;
			ac = new AddonComponent( 15977 );
			AddComponent( ac, 6, 0, 0 );
			ac = new AddonComponent( 15975 );
			AddComponent( ac, 5, -1, 0 );
			ac = new AddonComponent( 15994 );
			AddComponent( ac, 5, 0, 0 );
			ac = new AddonComponent( 15996 );
			AddComponent( ac, 4, 1, 0 );
			ac = new AddonComponent( 15998 );
			AddComponent( ac, 3, 1, 0 );
			ac = new AddonComponent( 15997 );
			AddComponent( ac, 4, -1, 0 );
			ac = new AddonComponent( 15999 );
			AddComponent( ac, 3, -1, 0 );
			ac = new AddonComponent( 15993 );
			AddComponent( ac, 5, 1, 0 );
			ac = new AddonComponent( 16005 );
			AddComponent( ac, 2, 2, 0 );
			ac = new AddonComponent( 16005 );
			AddComponent( ac, -2, 2, 0 );
			ac = new AddonComponent( 16005 );
			AddComponent( ac, -1, 2, 0 );
			ac = new AddonComponent( 16005 );
			AddComponent( ac, 0, 2, 0 );
			ac = new AddonComponent( 16005 );
			AddComponent( ac, 1, 2, 0 );
			ac = new AddonComponent( 16008 );
			AddComponent( ac, -2, -1, 0 );
			ac = new AddonComponent( 16008 );
			AddComponent( ac, -1, -1, 0 );
			ac = new AddonComponent( 16008 );
			AddComponent( ac, 0, -1, 0 );
			ac = new AddonComponent( 16008 );
			AddComponent( ac, 1, -1, 0 );
			ac = new AddonComponent( 16008 );
			AddComponent( ac, 2, -1, 0 );
			ac = new AddonComponent( 16010 );
			AddComponent( ac, -2, -2, 0 );
			ac = new AddonComponent( 16010 );
			AddComponent( ac, 0, -2, 0 );
			ac = new AddonComponent( 16010 );
			AddComponent( ac, 1, -2, 0 );
			ac = new AddonComponent( 16010 );
			AddComponent( ac, 2, -2, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, -2, 0, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, -1, 0, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 1, 0, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 2, 0, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 3, 0, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 4, 0, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, -2, 1, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, -1, 1, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 0, 1, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 1, 1, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 2, 1, 0 );
			ac = new AddonComponent( 16012 );
			AddComponent( ac, -3, 1, 0 );
			ac = new AddonComponent( 16013 );
			AddComponent( ac, -3, -1, 0 );
			ac = new AddonComponent( 16014 );
			AddComponent( ac, -4, 1, 0 );
			ac = new AddonComponent( 16015 );
			AddComponent( ac, -4, 0, 0 );
			ac = new AddonComponent( 16016 );
			AddComponent( ac, -4, -1, 0 );
			ac = new AddonComponent( 16017 );
			AddComponent( ac, -5, 1, 0 );
			ac = new AddonComponent( 16018 );
			AddComponent( ac, -5, 0, 0 );
			ac = new AddonComponent( 16020 );
			AddComponent( ac, -5, -1, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, -3, 0, 0 );
			ac = new AddonComponent( 15971 );
			AddComponent( ac, -6, 0, 0 );
			ac = new AddonComponent( 15957 );
			AddComponent( ac, -5, 0, 0 );
			ac = new AddonComponent( 15962 );
			AddComponent( ac, 1, 0, 0 );
			ac = new AddonComponent( 15963 );
			AddComponent( ac, 2, -1, 0 );
			ac = new AddonComponent( 15964 );
			AddComponent( ac, 3, -2, 0 );
			ac = new AddonComponent( 15961 );
			AddComponent( ac, 1, 2, 0 );
			ac = new AddonComponent( 16009 );
			AddComponent( ac, -1, -2, 0 );

		}

		public SailBoatEast1( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class SailBoatEast2 : BaseAddon
	{
		[ Constructable ]
		public SailBoatEast2()
		{
			AddonComponent ac = null;
			ac = new AddonComponent( 15977 );
			AddComponent( ac, 6, 0, 0 );
			ac = new AddonComponent( 15975 );
			AddComponent( ac, 5, -1, 0 );
			ac = new AddonComponent( 15994 );
			AddComponent( ac, 5, 0, 0 );
			ac = new AddonComponent( 15996 );
			AddComponent( ac, 4, 1, 0 );
			ac = new AddonComponent( 15998 );
			AddComponent( ac, 3, 1, 0 );
			ac = new AddonComponent( 15997 );
			AddComponent( ac, 4, -1, 0 );
			ac = new AddonComponent( 15999 );
			AddComponent( ac, 3, -1, 0 );
			ac = new AddonComponent( 15993 );
			AddComponent( ac, 5, 1, 0 );
			ac = new AddonComponent( 16005 );
			AddComponent( ac, 2, 2, 0 );
			ac = new AddonComponent( 16005 );
			AddComponent( ac, -2, 2, 0 );
			ac = new AddonComponent( 16005 );
			AddComponent( ac, 0, 2, 0 );
			ac = new AddonComponent( 16005 );
			AddComponent( ac, 1, 2, 0 );
			ac = new AddonComponent( 16008 );
			AddComponent( ac, -2, -1, 0 );
			ac = new AddonComponent( 16008 );
			AddComponent( ac, -1, -1, 0 );
			ac = new AddonComponent( 16008 );
			AddComponent( ac, 0, -1, 0 );
			ac = new AddonComponent( 16008 );
			AddComponent( ac, 1, -1, 0 );
			ac = new AddonComponent( 16008 );
			AddComponent( ac, 2, -1, 0 );
			ac = new AddonComponent( 16010 );
			AddComponent( ac, -2, -2, 0 );
			ac = new AddonComponent( 16010 );
			AddComponent( ac, 0, -2, 0 );
			ac = new AddonComponent( 16010 );
			AddComponent( ac, 1, -2, 0 );
			ac = new AddonComponent( 16010 );
			AddComponent( ac, 2, -2, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, -2, 0, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, -1, 0, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 1, 0, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 2, 0, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 3, 0, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 4, 0, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, -2, 1, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, -1, 1, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 0, 1, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 1, 1, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 2, 1, 0 );
			ac = new AddonComponent( 16012 );
			AddComponent( ac, -3, 1, 0 );
			ac = new AddonComponent( 16013 );
			AddComponent( ac, -3, -1, 0 );
			ac = new AddonComponent( 16014 );
			AddComponent( ac, -4, 1, 0 );
			ac = new AddonComponent( 16015 );
			AddComponent( ac, -4, 0, 0 );
			ac = new AddonComponent( 16016 );
			AddComponent( ac, -4, -1, 0 );
			ac = new AddonComponent( 16017 );
			AddComponent( ac, -5, 1, 0 );
			ac = new AddonComponent( 16018 );
			AddComponent( ac, -5, 0, 0 );
			ac = new AddonComponent( 16020 );
			AddComponent( ac, -5, -1, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, -3, 0, 0 );
			ac = new AddonComponent( 15971 );
			AddComponent( ac, -6, 0, 0 );
			ac = new AddonComponent( 15957 );
			AddComponent( ac, -5, 0, 0 );
			ac = new AddonComponent( 15962 );
			AddComponent( ac, 1, 0, 0 );
			ac = new AddonComponent( 15963 );
			AddComponent( ac, 2, -1, 0 );
			ac = new AddonComponent( 15964 );
			AddComponent( ac, 3, -2, 0 );
			ac = new AddonComponent( 15961 );
			AddComponent( ac, 1, 2, 0 );
			ac = new AddonComponent( 16010 );
			AddComponent( ac, -1, -2, 0 );
			ac = new AddonComponent( 16004 );
			AddComponent( ac, -1, 2, 0 );

		}

		public SailBoatEast2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class SailBoatNorth1 : BaseAddon
	{
		[ Constructable ]
		public SailBoatNorth1()
		{
			AddonComponent ac = null;
			ac = new AddonComponent( 16084 );
			AddComponent( ac, 2, 1, 0 );
			ac = new AddonComponent( 16095 );
			AddComponent( ac, 2, -2, 0 );
			ac = new AddonComponent( 16094 );
			AddComponent( ac, 2, -1, 0 );
			ac = new AddonComponent( 16092 );
			AddComponent( ac, -1, 0, 0 );
			ac = new AddonComponent( 16093 );
			AddComponent( ac, 0, -1, 0 );
			ac = new AddonComponent( 16024 );
			AddComponent( ac, 0, -5, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 0, -3, 0 );
			ac = new AddonComponent( 16045 );
			AddComponent( ac, 0, -4, 0 );
			ac = new AddonComponent( 16026 );
			AddComponent( ac, 0, -6, 0 );
			ac = new AddonComponent( 16027 );
			AddComponent( ac, -1, -5, 0 );
			ac = new AddonComponent( 16028 );
			AddComponent( ac, 1, -5, 0 );
			ac = new AddonComponent( 16029 );
			AddComponent( ac, -1, -4, 0 );
			ac = new AddonComponent( 16030 );
			AddComponent( ac, 1, -4, 0 );
			ac = new AddonComponent( 16031 );
			AddComponent( ac, -1, -3, 0 );
			ac = new AddonComponent( 16032 );
			AddComponent( ac, 1, -3, 0 );
			ac = new AddonComponent( 16033 );
			AddComponent( ac, -1, 1, 0 );
			ac = new AddonComponent( 16033 );
			AddComponent( ac, -1, 0, 0 );
			ac = new AddonComponent( 16033 );
			AddComponent( ac, -1, -1, 0 );
			ac = new AddonComponent( 16033 );
			AddComponent( ac, -1, -2, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 1, 1, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 1, 0, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 1, -1, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 1, -2, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 0, 1, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 0, -1, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 0, -2, 0 );
			ac = new AddonComponent( 16050 );
			AddComponent( ac, 2, -2, 0 );
			ac = new AddonComponent( 16050 );
			AddComponent( ac, 2, -1, 0 );
			ac = new AddonComponent( 16050 );
			AddComponent( ac, 2, 0, 0 );
			ac = new AddonComponent( 16049 );
			AddComponent( ac, -2, -2, 0 );
			ac = new AddonComponent( 16049 );
			AddComponent( ac, -2, -1, 0 );
			ac = new AddonComponent( 16049 );
			AddComponent( ac, -2, 0, 0 );
			ac = new AddonComponent( 16049 );
			AddComponent( ac, -2, 1, 0 );
			ac = new AddonComponent( 16042 );
			AddComponent( ac, 0, 5, 0 );
			ac = new AddonComponent( 15950 );
			AddComponent( ac, 1, 5, 0 );
			ac = new AddonComponent( 16054 );
			AddComponent( ac, -1, 5, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 0, 4, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 0, 3, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 1, 2, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 0, 2, 0 );
			ac = new AddonComponent( 16050 );
			AddComponent( ac, 2, 2, 0 );
			ac = new AddonComponent( 16049 );
			AddComponent( ac, -2, 2, 0 );
			ac = new AddonComponent( 16033 );
			AddComponent( ac, -1, 2, 0 );
			ac = new AddonComponent( 16037 );
			AddComponent( ac, -1, 3, 0 );
			ac = new AddonComponent( 16038 );
			AddComponent( ac, 1, 3, 0 );
			ac = new AddonComponent( 16039 );
			AddComponent( ac, -1, 4, 0 );
			ac = new AddonComponent( 16040 );
			AddComponent( ac, 1, 4, 0 );
			ac = new AddonComponent( 16060 );
			AddComponent( ac, 0, 6, 0 );

		}

		public SailBoatNorth1( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class SailBoatNorth2 : BaseAddon
	{
		[ Constructable ]
		public SailBoatNorth2()
		{
			AddonComponent ac = null;
			ac = new AddonComponent( 16085 );
			AddComponent( ac, -2, 1, 0 );
			ac = new AddonComponent( 16095 );
			AddComponent( ac, 2, -2, 0 );
			ac = new AddonComponent( 16094 );
			AddComponent( ac, 2, -1, 0 );
			ac = new AddonComponent( 16092 );
			AddComponent( ac, -1, 0, 0 );
			ac = new AddonComponent( 16093 );
			AddComponent( ac, 0, -1, 0 );
			ac = new AddonComponent( 16024 );
			AddComponent( ac, 0, -5, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 0, -3, 0 );
			ac = new AddonComponent( 16045 );
			AddComponent( ac, 0, -4, 0 );
			ac = new AddonComponent( 16026 );
			AddComponent( ac, 0, -6, 0 );
			ac = new AddonComponent( 16027 );
			AddComponent( ac, -1, -5, 0 );
			ac = new AddonComponent( 16028 );
			AddComponent( ac, 1, -5, 0 );
			ac = new AddonComponent( 16029 );
			AddComponent( ac, -1, -4, 0 );
			ac = new AddonComponent( 16030 );
			AddComponent( ac, 1, -4, 0 );
			ac = new AddonComponent( 16031 );
			AddComponent( ac, -1, -3, 0 );
			ac = new AddonComponent( 16032 );
			AddComponent( ac, 1, -3, 0 );
			ac = new AddonComponent( 16033 );
			AddComponent( ac, -1, 1, 0 );
			ac = new AddonComponent( 16033 );
			AddComponent( ac, -1, 0, 0 );
			ac = new AddonComponent( 16033 );
			AddComponent( ac, -1, -1, 0 );
			ac = new AddonComponent( 16033 );
			AddComponent( ac, -1, -2, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 1, 1, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 1, 0, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 1, -1, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 1, -2, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 0, 1, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 0, -1, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 0, -2, 0 );
			ac = new AddonComponent( 16050 );
			AddComponent( ac, 2, -2, 0 );
			ac = new AddonComponent( 16050 );
			AddComponent( ac, 2, -1, 0 );
			ac = new AddonComponent( 16050 );
			AddComponent( ac, 2, 0, 0 );
			ac = new AddonComponent( 16050 );
			AddComponent( ac, 2, 1, 0 );
			ac = new AddonComponent( 16049 );
			AddComponent( ac, -2, -2, 0 );
			ac = new AddonComponent( 16049 );
			AddComponent( ac, -2, -1, 0 );
			ac = new AddonComponent( 16049 );
			AddComponent( ac, -2, 0, 0 );
			ac = new AddonComponent( 16042 );
			AddComponent( ac, 0, 5, 0 );
			ac = new AddonComponent( 15950 );
			AddComponent( ac, 1, 5, 0 );
			ac = new AddonComponent( 16054 );
			AddComponent( ac, -1, 5, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 0, 4, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 0, 3, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 1, 2, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 0, 2, 0 );
			ac = new AddonComponent( 16050 );
			AddComponent( ac, 2, 2, 0 );
			ac = new AddonComponent( 16049 );
			AddComponent( ac, -2, 2, 0 );
			ac = new AddonComponent( 16033 );
			AddComponent( ac, -1, 2, 0 );
			ac = new AddonComponent( 16037 );
			AddComponent( ac, -1, 3, 0 );
			ac = new AddonComponent( 16038 );
			AddComponent( ac, 1, 3, 0 );
			ac = new AddonComponent( 16039 );
			AddComponent( ac, -1, 4, 0 );
			ac = new AddonComponent( 16040 );
			AddComponent( ac, 1, 4, 0 );
			ac = new AddonComponent( 16060 );
			AddComponent( ac, 0, 6, 0 );

		}

		public SailBoatNorth2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class SailBoatSouth1 : BaseAddon
	{
		[ Constructable ]
		public SailBoatSouth1()
		{
			AddonComponent ac = null;
			ac = new AddonComponent( 16033 );
			AddComponent( ac, -1, 2, 0 );
			ac = new AddonComponent( 16033 );
			AddComponent( ac, -1, 1, 0 );
			ac = new AddonComponent( 16033 );
			AddComponent( ac, -1, 0, 0 );
			ac = new AddonComponent( 16033 );
			AddComponent( ac, -1, -1, 0 );
			ac = new AddonComponent( 16033 );
			AddComponent( ac, -1, -2, 0 );
			ac = new AddonComponent( 16049 );
			AddComponent( ac, -2, -2, 0 );
			ac = new AddonComponent( 16049 );
			AddComponent( ac, -2, -1, 0 );
			ac = new AddonComponent( 16049 );
			AddComponent( ac, -2, 0, 0 );
			ac = new AddonComponent( 16049 );
			AddComponent( ac, -2, 1, 0 );
			ac = new AddonComponent( 16049 );
			AddComponent( ac, -2, 2, 0 );
			ac = new AddonComponent( 16044 );
			AddComponent( ac, 1, -2, 0 );
			ac = new AddonComponent( 16044 );
			AddComponent( ac, 1, -1, 0 );
			ac = new AddonComponent( 16044 );
			AddComponent( ac, 1, 0, 0 );
			ac = new AddonComponent( 16044 );
			AddComponent( ac, 1, 1, 0 );
			ac = new AddonComponent( 16044 );
			AddComponent( ac, 1, 2, 0 );
			ac = new AddonComponent( 16044 );
			AddComponent( ac, 0, -2, 0 );
			ac = new AddonComponent( 16044 );
			AddComponent( ac, 0, -1, 0 );
			ac = new AddonComponent( 16044 );
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 16044 );
			AddComponent( ac, 0, 1, 0 );
			ac = new AddonComponent( 16044 );
			AddComponent( ac, 0, 2, 0 );
			ac = new AddonComponent( 16050 );
			AddComponent( ac, 2, -2, 0 );
			ac = new AddonComponent( 16050 );
			AddComponent( ac, 2, 0, 0 );
			ac = new AddonComponent( 16050 );
			AddComponent( ac, 2, 1, 0 );
			ac = new AddonComponent( 16032 );
			AddComponent( ac, 1, -3, 0 );
			ac = new AddonComponent( 16031 );
			AddComponent( ac, -1, -3, 0 );
			ac = new AddonComponent( 16030 );
			AddComponent( ac, 1, -4, 0 );
			ac = new AddonComponent( 16029 );
			AddComponent( ac, -1, -4, 0 );
			ac = new AddonComponent( 16028 );
			AddComponent( ac, 1, -5, 0 );
			ac = new AddonComponent( 16027 );
			AddComponent( ac, -1, -5, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 0, -3, 0 );
			ac = new AddonComponent( 16045 );
			AddComponent( ac, 0, -4, 0 );
			ac = new AddonComponent( 16047 );
			AddComponent( ac, 0, -5, 0 );
			ac = new AddonComponent( 16068 );
			AddComponent( ac, 0, -6, 0 );
			ac = new AddonComponent( 15947 );
			AddComponent( ac, 0, -5, 0 );
			ac = new AddonComponent( 16098 );
			AddComponent( ac, 0, 1, 0 );
			ac = new AddonComponent( 16050 );
			AddComponent( ac, 2, 2, 0 );
			ac = new AddonComponent( 16097 );
			AddComponent( ac, -1, 2, 0 );
			ac = new AddonComponent( 16099 );
			AddComponent( ac, 2, 1, 0 );
			ac = new AddonComponent( 16084 );
			AddComponent( ac, 2, -1, 0 );
			ac = new AddonComponent( 16052 );
			AddComponent( ac, 0, 6, 0 );
			ac = new AddonComponent( 16053 );
			AddComponent( ac, 1, 5, 0 );
			ac = new AddonComponent( 16054 );
			AddComponent( ac, -1, 5, 0 );
			ac = new AddonComponent( 16055 );
			AddComponent( ac, 0, 5, 0 );
			ac = new AddonComponent( 16040 );
			AddComponent( ac, 1, 4, 0 );
			ac = new AddonComponent( 16039 );
			AddComponent( ac, -1, 4, 0 );
			ac = new AddonComponent( 16038 );
			AddComponent( ac, 1, 3, 0 );
			ac = new AddonComponent( 16037 );
			AddComponent( ac, -1, 3, 0 );
			ac = new AddonComponent( 16042 );
			AddComponent( ac, 0, 5, 3 );
			ac = new AddonComponent( 16044 );
			AddComponent( ac, 0, 3, 0 );
			ac = new AddonComponent( 16044 );
			AddComponent( ac, 0, 4, 0 );
			ac = new AddonComponent( 16096 );
			AddComponent( ac, -2, 3, 0 );

		}

		public SailBoatSouth1( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class SailBoatSouth2 : BaseAddon
	{
		[ Constructable ]
		public SailBoatSouth2()
		{
			AddonComponent ac = null;
			ac = new AddonComponent( 16033 );
			AddComponent( ac, -1, 2, 0 );
			ac = new AddonComponent( 16033 );
			AddComponent( ac, -1, 1, 0 );
			ac = new AddonComponent( 16033 );
			AddComponent( ac, -1, 0, 0 );
			ac = new AddonComponent( 16033 );
			AddComponent( ac, -1, -1, 0 );
			ac = new AddonComponent( 16033 );
			AddComponent( ac, -1, -2, 0 );
			ac = new AddonComponent( 16049 );
			AddComponent( ac, -2, -2, 0 );
			ac = new AddonComponent( 16049 );
			AddComponent( ac, -2, 0, 0 );
			ac = new AddonComponent( 16049 );
			AddComponent( ac, -2, 1, 0 );
			ac = new AddonComponent( 16049 );
			AddComponent( ac, -2, 2, 0 );
			ac = new AddonComponent( 16044 );
			AddComponent( ac, 1, -2, 0 );
			ac = new AddonComponent( 16044 );
			AddComponent( ac, 1, -1, 0 );
			ac = new AddonComponent( 16044 );
			AddComponent( ac, 1, 0, 0 );
			ac = new AddonComponent( 16044 );
			AddComponent( ac, 1, 1, 0 );
			ac = new AddonComponent( 16044 );
			AddComponent( ac, 1, 2, 0 );
			ac = new AddonComponent( 16044 );
			AddComponent( ac, 0, -2, 0 );
			ac = new AddonComponent( 16044 );
			AddComponent( ac, 0, -1, 0 );
			ac = new AddonComponent( 16044 );
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 16044 );
			AddComponent( ac, 0, 1, 0 );
			ac = new AddonComponent( 16044 );
			AddComponent( ac, 0, 2, 0 );
			ac = new AddonComponent( 16050 );
			AddComponent( ac, 2, -2, 0 );
			ac = new AddonComponent( 16050 );
			AddComponent( ac, 2, -1, 0 );
			ac = new AddonComponent( 16050 );
			AddComponent( ac, 2, 0, 0 );
			ac = new AddonComponent( 16050 );
			AddComponent( ac, 2, 1, 0 );
			ac = new AddonComponent( 16032 );
			AddComponent( ac, 1, -3, 0 );
			ac = new AddonComponent( 16031 );
			AddComponent( ac, -1, -3, 0 );
			ac = new AddonComponent( 16030 );
			AddComponent( ac, 1, -4, 0 );
			ac = new AddonComponent( 16029 );
			AddComponent( ac, -1, -4, 0 );
			ac = new AddonComponent( 16028 );
			AddComponent( ac, 1, -5, 0 );
			ac = new AddonComponent( 16027 );
			AddComponent( ac, -1, -5, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 0, -3, 0 );
			ac = new AddonComponent( 16045 );
			AddComponent( ac, 0, -4, 0 );
			ac = new AddonComponent( 16047 );
			AddComponent( ac, 0, -5, 0 );
			ac = new AddonComponent( 16068 );
			AddComponent( ac, 0, -6, 0 );
			ac = new AddonComponent( 15947 );
			AddComponent( ac, 0, -5, 0 );
			ac = new AddonComponent( 16098 );
			AddComponent( ac, 0, 1, 0 );
			ac = new AddonComponent( 16050 );
			AddComponent( ac, 2, 2, 0 );
			ac = new AddonComponent( 16097 );
			AddComponent( ac, -1, 2, 0 );
			ac = new AddonComponent( 16099 );
			AddComponent( ac, 2, 1, 0 );
			ac = new AddonComponent( 16085 );
			AddComponent( ac, -2, -1, 0 );
			ac = new AddonComponent( 16052 );
			AddComponent( ac, 0, 6, 0 );
			ac = new AddonComponent( 16053 );
			AddComponent( ac, 1, 5, 0 );
			ac = new AddonComponent( 16054 );
			AddComponent( ac, -1, 5, 0 );
			ac = new AddonComponent( 16055 );
			AddComponent( ac, 0, 5, 0 );
			ac = new AddonComponent( 16040 );
			AddComponent( ac, 1, 4, 0 );
			ac = new AddonComponent( 16039 );
			AddComponent( ac, -1, 4, 0 );
			ac = new AddonComponent( 16038 );
			AddComponent( ac, 1, 3, 0 );
			ac = new AddonComponent( 16037 );
			AddComponent( ac, -1, 3, 0 );
			ac = new AddonComponent( 16042 );
			AddComponent( ac, 0, 5, 3 );
			ac = new AddonComponent( 16044 );
			AddComponent( ac, 0, 3, 0 );
			ac = new AddonComponent( 16044 );
			AddComponent( ac, 0, 4, 0 );
			ac = new AddonComponent( 16096 );
			AddComponent( ac, -2, 3, 0 );

		}

		public SailBoatSouth2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class SailBoatWest1 : BaseAddon
	{
		[ Constructable ]
		public SailBoatWest1()
		{
			AddonComponent ac = null;
			ac = new AddonComponent( 16021 );
			AddComponent( ac, -6, 0, 0 );
			ac = new AddonComponent( 16020 );
			AddComponent( ac, -5, -1, 0 );
			ac = new AddonComponent( 16018 );
			AddComponent( ac, -5, 0, 0 );
			ac = new AddonComponent( 16017 );
			AddComponent( ac, -5, 1, 0 );
			ac = new AddonComponent( 16016 );
			AddComponent( ac, -4, -1, 0 );
			ac = new AddonComponent( 16015 );
			AddComponent( ac, -4, 0, 0 );
			ac = new AddonComponent( 16014 );
			AddComponent( ac, -4, 1, 0 );
			ac = new AddonComponent( 16013 );
			AddComponent( ac, -3, -1, 0 );
			ac = new AddonComponent( 16012 );
			AddComponent( ac, -3, 1, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, -3, 0, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, -5, 0, 3 );
			ac = new AddonComponent( 16007 );
			AddComponent( ac, -2, 2, 0 );
			ac = new AddonComponent( 16008 );
			AddComponent( ac, -2, -1, 0 );
			ac = new AddonComponent( 16008 );
			AddComponent( ac, -1, -1, 0 );
			ac = new AddonComponent( 16008 );
			AddComponent( ac, 0, -1, 0 );
			ac = new AddonComponent( 16008 );
			AddComponent( ac, 1, -1, 0 );
			ac = new AddonComponent( 16008 );
			AddComponent( ac, 2, -1, 0 );
			ac = new AddonComponent( 16010 );
			AddComponent( ac, -2, -2, 0 );
			ac = new AddonComponent( 16010 );
			AddComponent( ac, -1, -2, 0 );
			ac = new AddonComponent( 16010 );
			AddComponent( ac, 0, -2, 0 );
			ac = new AddonComponent( 16010 );
			AddComponent( ac, 1, -2, 0 );
			ac = new AddonComponent( 16010 );
			AddComponent( ac, 2, -2, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, -2, 0, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, -2, 1, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, -1, 0, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, -1, 1, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 0, 1, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 1, 0, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 1, 1, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 2, 0, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 2, 1, 0 );
			ac = new AddonComponent( 16005 );
			AddComponent( ac, -1, 2, 0 );
			ac = new AddonComponent( 16005 );
			AddComponent( ac, 0, 2, 0 );
			ac = new AddonComponent( 16005 );
			AddComponent( ac, 2, 2, 0 );
			ac = new AddonComponent( 15999 );
			AddComponent( ac, 3, -1, 0 );
			ac = new AddonComponent( 15998 );
			AddComponent( ac, 3, 1, 0 );
			ac = new AddonComponent( 15997 );
			AddComponent( ac, 4, -1, 0 );
			ac = new AddonComponent( 15996 );
			AddComponent( ac, 4, 1, 0 );
			ac = new AddonComponent( 15995 );
			AddComponent( ac, 5, -1, 0 );
			ac = new AddonComponent( 15993 );
			AddComponent( ac, 5, 1, 0 );
			ac = new AddonComponent( 15990 );
			AddComponent( ac, 6, 0, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 3, 0, 0 );
			ac = new AddonComponent( 15952 );
			AddComponent( ac, 5, 0, 0 );
			ac = new AddonComponent( 15991 );
			AddComponent( ac, 5, 1, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 4, 0, 0 );
			ac = new AddonComponent( 15980 );
			AddComponent( ac, -1, 0, 0 );
			ac = new AddonComponent( 15979 );
			AddComponent( ac, -1, 2, 0 );
			ac = new AddonComponent( 15978 );
			AddComponent( ac, -2, 2, 0 );
			ac = new AddonComponent( 15981 );
			AddComponent( ac, 0, -1, 0 );
			ac = new AddonComponent( 16004 );
			AddComponent( ac, 1, 2, 0 );

		}

		public SailBoatWest1( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class SailBoatWest2 : BaseAddon
	{
		[ Constructable ]
		public SailBoatWest2()
		{
			AddonComponent ac = null;
			ac = new AddonComponent( 16021 );
			AddComponent( ac, -6, 0, 0 );
			ac = new AddonComponent( 16020 );
			AddComponent( ac, -5, -1, 0 );
			ac = new AddonComponent( 16018 );
			AddComponent( ac, -5, 0, 0 );
			ac = new AddonComponent( 16017 );
			AddComponent( ac, -5, 1, 0 );
			ac = new AddonComponent( 16016 );
			AddComponent( ac, -4, -1, 0 );
			ac = new AddonComponent( 16015 );
			AddComponent( ac, -4, 0, 0 );
			ac = new AddonComponent( 16014 );
			AddComponent( ac, -4, 1, 0 );
			ac = new AddonComponent( 16013 );
			AddComponent( ac, -3, -1, 0 );
			ac = new AddonComponent( 16012 );
			AddComponent( ac, -3, 1, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, -3, 0, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, -5, 0, 3 );
			ac = new AddonComponent( 16007 );
			AddComponent( ac, -2, 2, 0 );
			ac = new AddonComponent( 16008 );
			AddComponent( ac, -2, -1, 0 );
			ac = new AddonComponent( 16008 );
			AddComponent( ac, -1, -1, 0 );
			ac = new AddonComponent( 16008 );
			AddComponent( ac, 0, -1, 0 );
			ac = new AddonComponent( 16008 );
			AddComponent( ac, 1, -1, 0 );
			ac = new AddonComponent( 16008 );
			AddComponent( ac, 2, -1, 0 );
			ac = new AddonComponent( 16010 );
			AddComponent( ac, -2, -2, 0 );
			ac = new AddonComponent( 16010 );
			AddComponent( ac, -1, -2, 0 );
			ac = new AddonComponent( 16010 );
			AddComponent( ac, 0, -2, 0 );
			ac = new AddonComponent( 16010 );
			AddComponent( ac, 2, -2, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, -2, 0, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, -2, 1, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, -1, 0, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, -1, 1, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 0, 1, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 1, 0, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 1, 1, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 2, 0, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 2, 1, 0 );
			ac = new AddonComponent( 16005 );
			AddComponent( ac, -1, 2, 0 );
			ac = new AddonComponent( 16005 );
			AddComponent( ac, 0, 2, 0 );
			ac = new AddonComponent( 16005 );
			AddComponent( ac, 1, 2, 0 );
			ac = new AddonComponent( 16005 );
			AddComponent( ac, 2, 2, 0 );
			ac = new AddonComponent( 15999 );
			AddComponent( ac, 3, -1, 0 );
			ac = new AddonComponent( 15998 );
			AddComponent( ac, 3, 1, 0 );
			ac = new AddonComponent( 15997 );
			AddComponent( ac, 4, -1, 0 );
			ac = new AddonComponent( 15996 );
			AddComponent( ac, 4, 1, 0 );
			ac = new AddonComponent( 15995 );
			AddComponent( ac, 5, -1, 0 );
			ac = new AddonComponent( 15993 );
			AddComponent( ac, 5, 1, 0 );
			ac = new AddonComponent( 15990 );
			AddComponent( ac, 6, 0, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 3, 0, 0 );
			ac = new AddonComponent( 15952 );
			AddComponent( ac, 5, 0, 0 );
			ac = new AddonComponent( 15991 );
			AddComponent( ac, 5, 1, 0 );
			ac = new AddonComponent( 16011 );
			AddComponent( ac, 4, 0, 0 );
			ac = new AddonComponent( 15980 );
			AddComponent( ac, -1, 0, 0 );
			ac = new AddonComponent( 15979 );
			AddComponent( ac, -1, 2, 0 );
			ac = new AddonComponent( 15978 );
			AddComponent( ac, -2, 2, 0 );
			ac = new AddonComponent( 15981 );
			AddComponent( ac, 0, -1, 0 );
			ac = new AddonComponent( 16009 );
			AddComponent( ac, 1, -2, 0 );

		}

		public SailBoatWest2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class SailBoatSailing : BaseAddon
	{
		[ Constructable ]
		public SailBoatSailing()
		{
			AddonComponent ac = null;
			ac = new AddonComponent( 16095 );
			AddComponent( ac, 2, -2, 0 );
			ac = new AddonComponent( 16094 );
			AddComponent( ac, 2, -1, 0 );
			ac = new AddonComponent( 16092 );
			AddComponent( ac, -1, 0, 0 );
			ac = new AddonComponent( 16093 );
			AddComponent( ac, 0, -1, 0 );
			ac = new AddonComponent( 16024 );
			AddComponent( ac, 0, -5, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 0, -3, 0 );
			ac = new AddonComponent( 16045 );
			AddComponent( ac, 0, -4, 0 );
			ac = new AddonComponent( 16026 );
			AddComponent( ac, 0, -6, 0 );
			ac = new AddonComponent( 16027 );
			AddComponent( ac, -1, -5, 0 );
			ac = new AddonComponent( 16028 );
			AddComponent( ac, 1, -5, 0 );
			ac = new AddonComponent( 16029 );
			AddComponent( ac, -1, -4, 0 );
			ac = new AddonComponent( 16030 );
			AddComponent( ac, 1, -4, 0 );
			ac = new AddonComponent( 16031 );
			AddComponent( ac, -1, -3, 0 );
			ac = new AddonComponent( 16032 );
			AddComponent( ac, 1, -3, 0 );
			ac = new AddonComponent( 16033 );
			AddComponent( ac, -1, 1, 0 );
			ac = new AddonComponent( 16033 );
			AddComponent( ac, -1, 0, 0 );
			ac = new AddonComponent( 16033 );
			AddComponent( ac, -1, -1, 0 );
			ac = new AddonComponent( 16033 );
			AddComponent( ac, -1, -2, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 1, 1, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 1, 0, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 1, -1, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 1, -2, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 0, 1, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 0, -1, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 0, -2, 0 );
			ac = new AddonComponent( 16050 );
			AddComponent( ac, 2, -2, 0 );
			ac = new AddonComponent( 16050 );
			AddComponent( ac, 2, -1, 0 );
			ac = new AddonComponent( 16050 );
			AddComponent( ac, 2, 0, 0 );
			ac = new AddonComponent( 16050 );
			AddComponent( ac, 2, 1, 0 );
			ac = new AddonComponent( 16049 );
			AddComponent( ac, -2, -2, 0 );
			ac = new AddonComponent( 16049 );
			AddComponent( ac, -2, -1, 0 );
			ac = new AddonComponent( 16049 );
			AddComponent( ac, -2, 0, 0 );
			ac = new AddonComponent( 16049 );
			AddComponent( ac, -2, 1, 0 );
			ac = new AddonComponent( 16042 );
			AddComponent( ac, 0, 5, 0 );
			ac = new AddonComponent( 15950 );
			AddComponent( ac, 1, 5, 0 );
			ac = new AddonComponent( 16054 );
			AddComponent( ac, -1, 5, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 0, 4, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 0, 3, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 1, 2, 0 );
			ac = new AddonComponent( 16048 );
			AddComponent( ac, 0, 2, 0 );
			ac = new AddonComponent( 16050 );
			AddComponent( ac, 2, 2, 0 );
			ac = new AddonComponent( 16049 );
			AddComponent( ac, -2, 2, 0 );
			ac = new AddonComponent( 16033 );
			AddComponent( ac, -1, 2, 0 );
			ac = new AddonComponent( 16037 );
			AddComponent( ac, -1, 3, 0 );
			ac = new AddonComponent( 16038 );
			AddComponent( ac, 1, 3, 0 );
			ac = new AddonComponent( 16039 );
			AddComponent( ac, -1, 4, 0 );
			ac = new AddonComponent( 16040 );
			AddComponent( ac, 1, 4, 0 );
			ac = new AddonComponent( 16060 );
			AddComponent( ac, 0, 6, 0 );

		}

		public SailBoatSailing( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
public class NujelmStairs : BaseAddon
	{
		[ Constructable ]
		public NujelmStairs()
		{
			AddonComponent ac = null;
			ac = new AddonComponent( 1848 );
			AddComponent( ac, 0, -4, 0 );
			ac = new AddonComponent( 1848 );
			AddComponent( ac, 1, -4, 0 );
			ac = new AddonComponent( 1848 );
			AddComponent( ac, 1, 0, 0 );
			ac = new AddonComponent( 1848 );
			AddComponent( ac, 1, -1, 0 );
			ac = new AddonComponent( 1848 );
			AddComponent( ac, 1, -2, 0 );
			ac = new AddonComponent( 1848 );
			AddComponent( ac, 1, -3, 0 );
			ac = new AddonComponent( 1855 );
			AddComponent( ac, 0, -3, 5 );
			ac = new AddonComponent( 1850 );
			AddComponent( ac, 0, 0, 5 );
			ac = new AddonComponent( 1850 );
			AddComponent( ac, 0, -1, 5 );
			ac = new AddonComponent( 1850 );
			AddComponent( ac, 0, -2, 5 );
			ac = new AddonComponent( 1848 );
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 1848 );
			AddComponent( ac, 0, -1, 0 );
			ac = new AddonComponent( 1848 );
			AddComponent( ac, 0, -2, 0 );
			ac = new AddonComponent( 1848 );
			AddComponent( ac, 0, -3, 0 );
			ac = new AddonComponent( 1848 );
			AddComponent( ac, 1, 5, 0 );
			ac = new AddonComponent( 1848 );
			AddComponent( ac, 1, 4, 0 );
			ac = new AddonComponent( 1848 );
			AddComponent( ac, 1, 3, 0 );
			ac = new AddonComponent( 1848 );
			AddComponent( ac, 1, 2, 0 );
			ac = new AddonComponent( 1848 );
			AddComponent( ac, 1, 1, 0 );
			ac = new AddonComponent( 1848 );
			AddComponent( ac, 0, 5, 0 );
			ac = new AddonComponent( 1854 );
			AddComponent( ac, 0, 4, 5 );
			ac = new AddonComponent( 1850 );
			AddComponent( ac, 0, 3, 5 );
			ac = new AddonComponent( 1850 );
			AddComponent( ac, 0, 2, 5 );
			ac = new AddonComponent( 1850 );
			AddComponent( ac, 0, 1, 5 );
			ac = new AddonComponent( 1848 );
			AddComponent( ac, 0, 4, 0 );
			ac = new AddonComponent( 1848 );
			AddComponent( ac, 0, 3, 0 );
			ac = new AddonComponent( 1848 );
			AddComponent( ac, 0, 2, 0 );
			ac = new AddonComponent( 1848 );
			AddComponent( ac, 0, 1, 0 );

		}

		public NujelmStairs( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}