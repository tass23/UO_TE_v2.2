using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public class VampireShroud : BaseArmor
	{
		public override int ArtifactRarity{ get{ return 100; } }
		
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }
		
		public override int BasePhysicalResistance{ get{ return 24; } }
		public override int BaseFireResistance{ get{ return 16; } }
		public override int BaseColdResistance{ get{ return 22; } }
		public override int BasePoisonResistance{ get{ return 18; } }
		public override int BaseEnergyResistance{ get{ return 17; } }

		public override int ArmorBase{ get{ return 35; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public VampireShroud() : base( 0x2683 )
		{
			Name = "a Vampire Shroud";
			Hue = 0x7E3;
			Movable = true;
			Visible = true;
			Weight = 1.0;
			Identified = true;

			MeditationAllowance = ArmorMeditationAllowance.All;
		}

		public override void OnDoubleClick( Mobile m )
		{
			PlayerMobile from = (PlayerMobile) m;
			if ( !(this.IsChildOf(from.Backpack)) && (Parent == from ))
			{
				if (from.Vampire > 0)
				{
					if (m.Hidden == false ) m.Hidden = true;
						else if (m.Hidden == true) m.Hidden = false;

					Effects.SendLocationEffect( new Point3D( m.X + 1, m.Y, m.Z + 4 ), m.Map, 0x3728, 13 );
					Effects.SendLocationEffect( new Point3D( m.X + 1, m.Y, m.Z ), m.Map, 0x3728, 13 );
					Effects.SendLocationEffect( new Point3D( m.X + 1, m.Y, m.Z - 4 ), m.Map, 0x3728, 13 );
					Effects.SendLocationEffect( new Point3D( m.X, m.Y + 1, m.Z + 4 ), m.Map, 0x3728, 13 );
					Effects.SendLocationEffect( new Point3D( m.X, m.Y + 1, m.Z ), m.Map, 0x3728, 13 );
					Effects.SendLocationEffect( new Point3D( m.X, m.Y + 1, m.Z - 4 ), m.Map, 0x3728, 13 );
					Effects.SendLocationEffect( new Point3D( m.X + 1, m.Y + 1, m.Z + 11 ), m.Map, 0x3728, 13 );
					Effects.SendLocationEffect( new Point3D( m.X + 1, m.Y + 1, m.Z + 7 ), m.Map, 0x3728, 13 );
					Effects.SendLocationEffect( new Point3D( m.X + 1, m.Y + 1, m.Z + 3 ), m.Map, 0x3728, 13 );
					Effects.SendLocationEffect( new Point3D( m.X + 1, m.Y + 1, m.Z - 1 ), m.Map, 0x3728, 13 );

					m.PlaySound( 0x228 );
				}
			}
		}
		
		public override void OnRemoved(object parent)
		{
			if (parent is PlayerMobile)
			{
				PlayerMobile from = (PlayerMobile)parent;
				//from.Title = null;
				//from.HueMod = 0;
			}
		}

		public override bool OnEquip(Mobile m)
		{
			if(m is PlayerMobile)
			{
				PlayerMobile from = (PlayerMobile) m;
				if (from.Vampire > 0)
				{
					//from.HueMod = 0x847E;
					//from.Title = "the Vampire";
				}
				else
				{
					from.SendMessage("Only a vampire may wear this!");
					from.Damage( Utility.Random( 10, 20 ) );
					return false;
					//this.Delete();
				}
			}
			return true;
		}
			
		public VampireShroud( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); 
		}

		public override void Deserialize(GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

		}
	}
}