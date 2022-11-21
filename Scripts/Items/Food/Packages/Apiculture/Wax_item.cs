using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	[FlipableAttribute( 0x154B, 0x154C )]
	public class WaxMask : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 1; } }
		public override int BaseFireResistance{ get{ return 0; } }
		public override int BaseColdResistance{ get{ return 6; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 10; } }
		public override int InitMinHits{ get{ return 25; } }
		public override int InitMaxHits{ get{ return 30; } }

		public override int AosStrReq{ get{ return 20; } }

		public override int ArmorBase{ get{ return 30; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public WaxMask() : base( 0x154B )
		{
			Weight = 1;
			StrRequirement = 10;
			Hue = 1153;
			Name = "Wax Mask";
		}

		public WaxMask( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize(GenericReader reader) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class BeeSwarm : Item
	{
		public override bool Decays
		{
			get { return true; }
		}

		[Constructable]
		public BeeSwarm() : base( 0x91b )
		{
			DefaultDecayTime  = TimeSpan.FromMinutes( 10.0 );
			Movable = false;
		}

		public override void OnSingleClick( Mobile from )
		{
			this.LabelTo( from, this.Name );
		}

		public BeeSwarm( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize(GenericReader reader) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	[FlipableAttribute( 0x1422, 0x1423 )]
	public class Beeswax : Item
	{
		[Constructable]
		public Beeswax() : this( 1 ){}

		[Constructable]
		public Beeswax( int amount ) : base( 0x1422 )
		{
			Stackable = true;
			Weight = 0.25;
			Amount = amount;
		}

		public Beeswax( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class BeeHiveHeal : Item
	{
		[Constructable]
		public BeeHiveHeal() : base( 0xF0A )
		{
			Name = "Beehive curing potion";
			Hue = 91;
		}

		public override void OnSingleClick( Mobile from )
		{
			this.LabelTo( from, this.Name );
		}
		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) || Parent == from )
			{
				from.Target = new BeeHiveHealtarget( this );
				from.SendMessage( "What beehive do you want to cure?" );
			}
			else
			{
				from.SendLocalizedMessage( 1042001 );
			}
		}
		public BeeHiveHeal( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize(GenericReader reader) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class BeeHiveHealtarget : Target
	{
		BeeHiveHeal m_BeeHiveHealVar;
		BeeHive m_BeeHiveMalade = null;

		public BeeHiveHealtarget(BeeHiveHeal m_BeeHiveHealTarg) : base( 10, false, TargetFlags.None )
		{
			m_BeeHiveHealVar = m_BeeHiveHealTarg;
		}

		protected override void OnTarget( Mobile from, object o )
		{
			Container pack = from.Backpack;
			if ( o is BeeHive)
			{
				m_BeeHiveMalade = (BeeHive)o;
				if (m_BeeHiveMalade.m_HiveSick == true)
				{
					m_BeeHiveMalade.m_HiveSick = false;
					m_BeeHiveMalade.Name = "A cured Beehive";
					new BeeHiveTimer(m_BeeHiveMalade).Start();
					m_BeeHiveHealVar.Consume();
					from.AddToBackpack( new Bottle() );
				}
				else from.SendMessage(0x33, "This Beehive isn't sick!" );
			}
			else from.SendMessage(0x33, "this is not a Beehive !" );
		}
	}
}