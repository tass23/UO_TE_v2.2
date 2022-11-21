using System;
using Server;
using Server.Gumps;
using Solaris.Addons;

namespace Server.Items
{
	//the "key" device that lets a user control/furl a flying carpet
	public class FlyingCarpetMagicLamp : MovableAddonKey
	{
		protected override string _CantCreateMessage{ get{ return "The magic lamp must be in your backpack to use."; } }
		protected override string _PlaceTargetMessage{ get{ return "Where do you wish to place your magic carpet?"; } }
		protected override string _PlaceConfirmMessage{ get{ return "Your flying carpet has now been placed.  You can use this magic lamp to control, board, or furl your flying carpet.  Do not lose this item or you lose control of your carpet!"; } }

		
		//the custom Magic Thread item is used to cusomize the carpet
		public override Type ModifyResourceType{ get{ return typeof( MagicThread ); } }
				
				
		//carpet generation properties
		protected FlyingCarpetType _CarpetType;
		protected int _CarpetWidth;
		protected int _CarpetLength;
		
		[CommandProperty( AccessLevel.GameMaster)]
		public FlyingCarpetType CarpetType
		{
			get{ return _CarpetType; }
			set
			{
				_CarpetType = value;
				InvalidateProperties();
			}
			
		}
		
		[CommandProperty( AccessLevel.GameMaster)]
		public int CarpetWidth
		{
			get{ return _CarpetWidth; }
			set
			{
				_CarpetWidth = Math.Max( 3, Math.Min( FlyingCarpet.MAX_WIDTH, value ) );
				
				if( _CarpetWidth % 2 == 0 )
				{
					_CarpetWidth += 1;
				}
				
				InvalidateProperties();
			}
		}
			
		[CommandProperty( AccessLevel.GameMaster)]
		public int CarpetLength
		{
			get{ return _CarpetLength; }
			set
			{
				_CarpetLength = Math.Max( 3, Math.Min( FlyingCarpet.MAX_LENGTH, value ) );
				
				if( _CarpetLength % 2 == 0 )
				{
					_CarpetLength += 1;
				}
				
				InvalidateProperties();
			}
		}
		
		
		[Constructable]
		public FlyingCarpetMagicLamp() : this( 7, 5, Utility.RandomMinMax( 0, 9 ) )
		{
		}
		
		[Constructable]
		public FlyingCarpetMagicLamp( int length, int width, int type ) : this( length, width, (FlyingCarpetType)type )
		{
		}
		
		public FlyingCarpetMagicLamp( int length, int width, FlyingCarpetType type )
		{
			CarpetLength = length;
			CarpetWidth = width;
			CarpetType = type;
			
			ItemID = 3840;
			Hue = 249;
			Name = "Flying Carpet Magic Lamp";
		}
		
		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );

			list.Add( 1060663, "Type\t{0}", _CarpetType.ToString() );
			list.Add( 1060662, "Length\t{0}, Width: {1}", _CarpetLength, _CarpetWidth );
			
			if( _Addon != null && _Addon.Key == this )
			{
				list.Add( 1049644, "Deployed" );
			}
		}
		
		public FlyingCarpetMagicLamp( Serial serial ) : base( serial )
		{
		}
		
		public override bool CanCreateAddon( Mobile from )
		{
			return IsChildOf( from.Backpack );
		}
		
		//this performs the creation of the magic carpet, based on the specs in the magic lamp
		public override void FinishCreation( Mobile from, Point3D point )
		{
			_Addon = new FlyingCarpet( _CarpetLength, _CarpetWidth, _CarpetType, AddonHue );
			
			base.FinishCreation( from, point );
		}
		
		public override void SendConfigGump( Mobile from )
		{
			from.SendGump( new FlyingCarpetConfigGump( this, from ) );
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( 0 );
			
			writer.Write( (int)_CarpetType );
			writer.Write( _CarpetWidth );
			writer.Write( _CarpetLength );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			_CarpetType = (FlyingCarpetType)reader.ReadInt();
			_CarpetWidth = reader.ReadInt();
			_CarpetLength = reader.ReadInt();
			
			
		}
		
	}
	
	
}