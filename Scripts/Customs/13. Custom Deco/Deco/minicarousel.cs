using System;
using System.Collections.Generic;
using Server;
using Server.Items;

namespace Server.Items {
	public class MiniCarouselTimer : Timer {
		private MiniCarouselSwitchComponent MCS;

		public MiniCarouselTimer( MiniCarouselSwitchComponent mcs ) : base( TimeSpan.FromSeconds( 0.125 ), TimeSpan.FromSeconds( 0.50 ) ) {
			MCS = mcs;
			Priority = TimerPriority.EveryTick;
		}

		protected override void OnTick() {
			MCS.GoRound();
			if ( MCS._GR == 16 ) {
				MCS._GR = 0;
				Stop();
			}
		}
	}

	public class CarouselDoll : Item {
		protected Point3D _OffSet;
		private MiniCarouselSwitchComponent DollOwner ;
		
		public CarouselDoll(int IID, int chue, Point3D OffSet, string nme, MiniCarouselSwitchComponent d_owner) : base(IID) {
			Movable = false;
			Name = nme;
			Hue = chue;
			_OffSet = OffSet;
			DollOwner = d_owner;
		}

		public void UpdatePosition() {
			Point3D movepoint = new Point3D( DollOwner.X + _OffSet.X, DollOwner.Y + _OffSet.Y, DollOwner.Z + _OffSet.Z );
			this.MoveToWorld( movepoint, DollOwner.Map );
		}
		
		public override void OnAfterDelete() {
			base.OnAfterDelete();
			DollOwner.Delete();
		}
		
		public override void OnLocationChange( Point3D oldLoc ) {
			base.OnLocationChange(oldLoc);
			if ( Deleted ) {
				return;
			}
			if ( DollOwner.X != this.X - _OffSet.X && DollOwner.Y != this.Y - _OffSet.Y && DollOwner.Z != this.Z - _OffSet.Z )
				DollOwner.Location = new Point3D( this.X - _OffSet.X, this.Y - _OffSet.Y, this.Z - _OffSet.Z );
		}

		public override void OnMapChange() {
			base.OnMapChange();
			if ( Deleted ) {
				return;
			}
			if ( DollOwner.Map != Map ) {
				DollOwner.Map = Map;
			}
		}

		public CarouselDoll( Serial serial ) : base( serial ) {
		}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( 0 );
			writer.Write( _OffSet.X );
			writer.Write( _OffSet.Y );
			writer.Write( _OffSet.Z );
			writer.Write( (Item)DollOwner );
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
			_OffSet = new Point3D(reader.ReadInt(),reader.ReadInt(),reader.ReadInt());
			DollOwner = (MiniCarouselSwitchComponent)reader.ReadItem();
		}
	}
	
	public class MiniCarouselSwitchComponent : AddonComponent {
		protected List<CarouselDoll> Dolls;
		protected List<CarouselDoll> Lights;
		
		public int _GR = 0;
		
		protected MiniCarouselTimer MCT;
		
		public MiniCarouselSwitchComponent( int itemID ) : base( itemID ) {
			Name = "MiniCarousel Switch";
			Hue = 0x58;
			MCT = new MiniCarouselTimer(this);
			if ( Dolls == null ) {
				Dolls = new List<CarouselDoll>();
			}
			if ( Lights == null ) {
				Lights = new List<CarouselDoll>();
			}
			AddDolls();
		}
		
		public void AddDolls() {
			for ( int i = 0; i < 4; i++ ) {
				Point3D offset;
				CarouselDoll doll;
				CarouselDoll light;
				switch(i) {
					case 0 : {
						offset = new Point3D(-1,0,8);
						doll = new CarouselDoll(0x259B, 0xE9, offset, "Carousel Doll", this );
						offset = new Point3D(-1,0,17);
						light = new CarouselDoll(3854,  0x16, offset, "Carousel Light", this );
						Dolls.Add(doll);
						Lights.Add(light);
						break;
					}
					case 1 : {
						offset = new Point3D(-1,-1,8);
						doll = new CarouselDoll(0x259A, 0x12, offset, "Carousel Doll", this );
						offset = new Point3D(-1,-1,17);
						light = new CarouselDoll(3854,  0x2, offset, "Carousel Light", this );
						Dolls.Add(doll);
						Lights.Add(light);
						
						break;
					}
					case 2 : {
						offset = new Point3D(-2,-1,7);
						doll = new CarouselDoll(0x259A, 0x3, offset, "Carousel Doll", this );
						Dolls.Add(doll);
						break;
					}
					case 3 : {
						offset = new Point3D(-2,0,8);
						doll = new CarouselDoll(0x259B, 0x16F, offset, "Carousel Doll", this );
						offset = new Point3D(-2,0,17);
						light = new CarouselDoll(3854,  0x43, offset, "Carousel Light", this );
						Dolls.Add(doll);
						Lights.Add(light);
						break;
					}
					default : {
						break;
					}
				}
			}
			UpdatePosition();
		}
		
		public override void OnDoubleClick( Mobile from ) {
			MCT.Start();
		}
		
		public void GoRound() {
			int thue = Dolls[0].Hue;
			for ( int i = 0; i < 4; i++ ) {
				if ( i < 3) {
					Dolls[i].Hue = Dolls[i + 1].Hue;
				}
				else {
					Dolls[i].Hue = thue;
				}
			}
			for ( int i = 0; i < 3; i++ ) {
				if ( _GR % 2 == 0 ) {
					Lights[i].Hue = Lights[i].Hue + 3;
				}
				else {
					Lights[i].Hue = Lights[i].Hue - 3;
				}
			}
			_GR++;
		}
		
		public void UpdatePosition() {
			foreach( CarouselDoll doll in Dolls ) {
				doll.UpdatePosition();
			}
			foreach( CarouselDoll light in Lights ){
				light.UpdatePosition();
			}
		}
		
		public override void OnLocationChange( Point3D oldLoc ) {
			base.OnLocationChange(oldLoc);
			if ( Deleted ) {
				return;
			}
			UpdatePosition();
		}
		
		public override void OnMapChange() {
			base.OnMapChange();
			if ( Deleted ) {
				return;
			}
			UpdatePosition();
		}
		
		public override void OnAfterDelete() {
			base.OnAfterDelete();
			foreach( CarouselDoll doll in Dolls ) {
				if ( doll != null && !doll.Deleted ) {
					doll.Delete();
				}
			}
			foreach( CarouselDoll light in Lights ) {
				if ( light != null && !light.Deleted ) {
					light.Delete();
				}
			}
		}
		
		public MiniCarouselSwitchComponent( Serial serial ) : base( serial ) {
		}
		
		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
			writer.Write( (int)Dolls.Count );
			foreach( CarouselDoll doll in Dolls ) {
				writer.Write( (Item)doll );
			}
			writer.Write( (int)Lights.Count );
			foreach( CarouselDoll light in Lights ) {
				writer.Write( (Item)light );
			}
		}
		
		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
			MCT = new MiniCarouselTimer(this);
			if ( Dolls == null ) {
				Dolls = new List<CarouselDoll>();
			}
			if ( Lights == null ) {
				Lights = new List<CarouselDoll>();
			}
			int counter = reader.ReadInt();
			for( int i = 0; i < counter; i++ ) {
				CarouselDoll doll = (CarouselDoll)reader.ReadItem();
				Dolls.Add(doll);
			}
			counter = reader.ReadInt();
			for( int i = 0; i < counter; i++ ) {
				CarouselDoll light = (CarouselDoll)reader.ReadItem();
				Lights.Add(light);
			}
		}
	}
	
	public class MiniCarouselAddon : BaseAddon {
		public override BaseAddonDeed Deed{ get{ return new MiniCarouselDeed(); } }
		
		[Constructable]
		public MiniCarouselAddon() {
			//Raisers
			AddonComponent ac = new AddonComponent( 0x7BA ); ac.Hue = 0x58; ac.Name = "Mini Carasoul";
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 0x7BA ); ac.Hue = 0x58; ac.Name = "Mini Carasoul";
			AddComponent( ac, 0, 1, 0 );
			ac = new AddonComponent( 0x7BA ); ac.Hue = 0x58; ac.Name = "Mini Carasoul";
			AddComponent( ac, 1, 1, 0 );
			ac = new AddonComponent( 0x7BA ); ac.Hue = 0x58; ac.Name = "Mini Carasoul";
			AddComponent( ac, 1, 0, 0 );
			
			//Deco
			ac = new AddonComponent( 0x2DCD ); ac.Hue = 0x58; ac.Name = "Mini Carasoul";
			AddComponent( ac, 0, 0, 5 );
			ac = new AddonComponent( 0x2DCC ); ac.Hue = 0x58; ac.Name = "Mini Carasoul";
			AddComponent( ac, 1, 0, 5 );
			ac = new AddonComponent( 0x2DCB ); ac.Hue = 0x58; ac.Name = "Mini Carasoul";
			AddComponent( ac, 1, 1, 5 );
			ac = new AddonComponent( 0x2DCE ); ac.Hue = 0x58; ac.Name = "Mini Carasoul";
			AddComponent( ac, 0, 1, 5 );
			
			//Switch
			AddComponent( new MiniCarouselSwitchComponent( 0x108F ), 2, 1, 0 );
			
			//Pole
			ac = new AddonComponent( 0x10D ); ac.Hue = 0x58; ac.Name = "Mini Carasoul";
			AddComponent( ac, 0, 0, 7 );
			
			//Aunning
			ac = new AddonComponent( 0xAFA ); ac.Hue = 0x58; ac.Name = "Mini Carasoul";
			AddComponent( ac, 0, 0, 12 );
			ac = new AddonComponent( 0x8CB ); ac.Hue = 0x58; ac.Name = "Mini Carasoul";
			AddComponent( ac, -1, -1, 12 );
			ac = new AddonComponent( 0x8CC ); ac.Hue = 0x58; ac.Name = "Mini Carasoul";
			AddComponent( ac, -1, 0, 12 );
			ac = new AddonComponent( 0x8CD ); ac.Hue = 0x58; ac.Name = "Mini Carasoul";
			AddComponent( ac, 0, 0, 12 );
			ac = new AddonComponent( 0x8CE ); ac.Hue = 0x58; ac.Name = "Mini Carasoul";
			AddComponent( ac, 0, -1, 12 );
			
			//Ball on top
			ac = new AddonComponent( 0x1869 ); ac.Hue = 0x58; ac.Name = "Mini Carasoul";
			AddComponent( ac, 0, 0, 13 );
		}
		
		public MiniCarouselAddon( Serial serial ) : base( serial ) {
		}
		
		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class MiniCarouselDeed : BaseAddonDeed {
		public override BaseAddon Addon{ get{ return new MiniCarouselAddon(); } }
		
		[Constructable]
		public MiniCarouselDeed() {
			Name = "Mini-Carousel Deed";
		}
		
		public MiniCarouselDeed( Serial serial ) : base( serial ) {
		}
		
		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}
		
		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}