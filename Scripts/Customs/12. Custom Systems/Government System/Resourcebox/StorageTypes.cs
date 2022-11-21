/***************************************************************************/
/*			ResourceBox.cs | ResourceBoxGump.cs | StorageTypes.cs					*/
/*							Created by A_Li_N													*/
/*				Credits :																		*/
/*						Original Gump Layout - Lysdexic									*/
/*						Hashtable help - UOT and daat99									*/
/***************************************************************************/
/*	Addition of different Resources :													*/
/*		To add/remove resource types from the box, simply put the Type of		*/
/*		the resource in the catagory you wish it to be in.  Each catagory		*/
/*		can hold up to 32 entries without messing a LOT with the gump.			*/
/*	Removing of Resources :																	*/
/*		Commenting out or deleting the type you wish to remove will remove	*/
/*		the type AND the amount each Resource Box contains.						*/
/***************************************************************************/

using System;

namespace Server.Items
{
	public class StorageTypes
	{
		private static Type[] m_Logs = new Type[]
		{
			typeof( Log ),
//			typeof( PineLog ),
			typeof( AshLog ),
//			typeof( MohoganyLog),
			typeof( YewLog ),
			typeof( OakLog ),
//			typeof( ZircoteLog ),
//			typeof( EbonyLog ),
//			typeof( BambooLog ),
//			typeof( PurpleHeartLog ),
//			typeof( RedwoodLog ),
//			typeof( PetrifiedLog ),
			typeof( HeartwoodLog ),
			typeof( BloodwoodLog ),
			typeof( FrostwoodLog ),
		};
		public static Type[] Logs{ get{ return m_Logs; } }

		private static Type[] m_Boards = new Type[]
		{
			typeof( Board ),
//			typeof( PineBoard ),
			typeof( AshBoard ),
//			typeof( MohoganyBoard),
			typeof( YewBoard ),
			typeof( OakBoard ),
//			typeof( ZircoteBoard ),
//			typeof( EbonyBoard ),
//			typeof( BambooBoard ),
//			typeof( PurpleHeartBoard ),
//			typeof( RedwoodBoard ),
//			typeof( PetrifiedBoard ),
			typeof( HeartwoodBoard ),
			typeof( BloodwoodBoard ),
			typeof( FrostwoodBoard ),
		};
		public static Type[] Boards{ get{ return m_Boards; } }

		private static Type[] m_Ingots = new Type[]
		{
			typeof( IronIngot ),
			typeof( DullCopperIngot ),
			typeof( ShadowIronIngot ),
			typeof( CopperIngot ),
			typeof( BronzeIngot ),
			typeof( GoldIngot ),
			typeof( AgapiteIngot ),
			typeof( VeriteIngot ),
			typeof( ValoriteIngot ),
//			typeof( BlazeIngot ),
//			typeof( IceIngot ),
//			typeof( ToxicIngot ),
//			typeof( ElectrumIngot ),
//			typeof( PlatinumIngot ),
		};
		public static Type[] Ingots{ get{ return m_Ingots; } }

		private static Type[] m_Granites = new Type[]
		{
			typeof( Granite ),
			typeof( DullCopperGranite ),
			typeof( ShadowIronGranite ),
			typeof( CopperGranite ),
			typeof( BronzeGranite ),
			typeof( GoldGranite ),
			typeof( AgapiteGranite ),
			typeof( VeriteGranite ),
			typeof( ValoriteGranite ),
//			typeof( BlazeGranite ),
//			typeof( IceGranite ),
//			typeof( ToxicGranite ),
//			typeof( ElectrumGranite ),
//			typeof( PlatinumGranite ),
		};
		public static Type[] Granites{ get{ return m_Granites; } }

		private static Type[] m_Scales = new Type[]
		{
			typeof( RedScales ),
			typeof( YellowScales ),
			typeof( BlackScales ),
			typeof( GreenScales ),
			typeof( WhiteScales ),
			typeof( BlueScales ),
//			typeof( CopperScales ),
//			typeof( SilverScales ),
//			typeof( GoldScales ),
		};
		public static Type[] Scales{ get{ return m_Scales; } }

		private static Type[] m_Leathers = new Type[]
		{
			typeof( Leather ),
			typeof( SpinedLeather ),
			typeof( HornedLeather ),
			typeof( BarbedLeather ),
//			typeof( PolarLeather ),
//			typeof( SyntheticLeather ),
//			typeof( DaemonicLeather ),
//			typeof( ShadowLeather ),
//			typeof( FrostLeather ),
//			typeof( EtherealLeather ),
		};
		public static Type[] Leathers{ get{ return m_Leathers; } }

		private static Type[] m_Misc = new Type[]
		{
			typeof( Sand ),

			typeof( Cloth ),
			typeof( Cotton ),
			typeof( Flax ),
			typeof( Wool ),
			typeof( Bandage ),

			typeof( Arrow ),
			typeof( Bolt ),
			typeof( Feather ),
			typeof( Shaft ),
			
			
		/*	typeof( FletcherTools ),
			typeof( TinkerTools ),
			typeof( Saw ),
			typeof( Tongs ),
			typeof( MortarPestle ),
			typeof( SewingKit ),
			typeof( Pickaxe ),
			typeof( ScribesPen ), */
			
			
			
		};
		public static Type[] Misc{ get{ return m_Misc; } }

		private static Type[] m_Reagents = new Type[]
		{
			typeof( BlackPearl ),
			typeof( Bloodmoss ),
			typeof( Garlic ),
			typeof( Ginseng ),
			typeof( MandrakeRoot ),
			typeof( Nightshade ),
			typeof( SulfurousAsh ),
			typeof( SpidersSilk ),
			typeof( BatWing ),
			typeof( GraveDust ),
			typeof( DaemonBlood ),
			typeof( NoxCrystal ),
			typeof( PigIron ),
		//	typeof( PetrafiedWood ),
		//	typeof( SpringWater ),
		//	typeof( DestroyingAngel ),
		};
		public static Type[] Reagents{ get{ return m_Reagents; } }
	}
}
