using System;
using Server;
using Server.Items;
using Server.Prompts;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using System.Collections;
using System.Collections.Generic;

namespace Server.Items
{
	public class BeeHive : AddonComponent, IChopable
	{
		public int m_HiveHoney;
		public int m_HiveWaxes;
		public int m_ApiChance;
		public int m_LoreSkill;
		public bool m_HiveFull;
		public bool m_HiveSick;

		[CommandProperty( AccessLevel.GameMaster )]
		public int HiveHoney { get { return m_HiveHoney; } set { m_HiveHoney = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int HiveWaxes { get { return m_HiveWaxes; } set { m_HiveWaxes = value; } }

		[CommandProperty( AccessLevel.GameMaster )] public int ApiChance { get { return m_ApiChance; } set { m_ApiChance = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int LoreSkill { get { return m_LoreSkill; } set { m_LoreSkill = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool HiveFull { get { return m_HiveFull; } set { m_HiveFull = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool HiveSick { get { return m_HiveSick; } set { m_HiveSick = value; } }

		[Constructable]
		public BeeHive() : base( 0x91a )
		{
			Name = "A Beehive";
			Movable = false;
		}

		public override void OnSingleClick( Mobile from ) { this.LabelTo( from, this.Name ); }

		public override void OnDoubleClick (Mobile from )
		{
			if (! from.InRange( this.GetWorldLocation(), 1 )) from.LocalOverheadMessage( MessageType.Regular, 906, 1019045 );
			else
			{
				m_LoreSkill = ((int)(from.Skills[SkillName.AnimalLore].Value));
				m_ApiChance = Utility.Random( 1, 100 );
				Container pack = from.Backpack;

				if ((m_HiveFull == false) && (m_HiveSick == false))
				{
					new BeeHiveTimer(this).Start();
					this.PublicOverheadMessage( MessageType.Regular, 0x35, false, string.Format("The Beehive starts to be colonized and the bees begin working hard." ));
				}
				else if ((m_HiveFull == true) && (m_HiveSick == true))
				{
					this.PublicOverheadMessage( MessageType.Regular, 0x35, false, string.Format("This Beehive is sick, the bees need to be cured." ));
				}
				else if ((m_HiveFull == true) && (m_ApiChance > m_LoreSkill) && (m_HiveSick == false) )
				{
					if ( Utility.RandomDouble() > (from.Skills[SkillName.AnimalLore].Value / 100) )
					{
						from.FixedParticles( 0x91B, 64, 240, 9916, 0, 3, EffectLayer.Head );
						from.Poison = Poison.Lesser;
						from.SendMessage(0x33, "You have angered the bees!" );
						from.PlaySound (0x230);
					}
					else from.SendMessage( "The bees are to agitated to get the honey from right now.");
				}
				else if ((m_HiveFull == true) && (m_HiveSick == false) && (m_HiveHoney >= 1) && (m_HiveWaxes >= 1))
				{
					if ( from.Skills.Cooking.Base >= 100)
					{
						m_HiveHoney += Utility.Random( ((int)(from.Skills.Cooking.Value / 75)) );
						m_HiveWaxes += Utility.Random( ((int)(from.Skills.Cooking.Value / 75)) );
					}
					from.AddToBackpack( new JarHoney(HiveHoney) );
					from.AddToBackpack( new Beeswax(m_HiveWaxes) );

					from.SendMessage( String.Format( "You gather {0} honey and {1} wax.", m_HiveHoney, m_HiveWaxes ) );
					from.PlaySound (0x58);

					m_HiveHoney = 0;
					m_HiveWaxes = 0;
				}
				else this.PublicOverheadMessage( MessageType.Regular, 0x35, false, string.Format("The beehive and the bees are working hard." ));
			}
		}

		public new void OnChop(Mobile from)
		{
			if (!(from.AccessLevel >= AccessLevel.GameMaster)) return;
			ArrayList list = new ArrayList();
			foreach ( Item itembee in this.GetItemsInRange( 0 ) )
			{
				if (itembee is BeeSwarm) list.Add( itembee );
			}
			if (list.Count > 0)
			{
				for ( int j = 0; j < list.Count; ++j )
				{
					Item item2 = (Item)list[j];
					if (item2 != null) item2.Delete();
				}
			}
			base.OnChop(from);
		}

		public BeeHive( Serial serial ) : base( serial ) { }
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( (int) m_HiveHoney );
			writer.Write( (int) m_HiveWaxes );
			writer.Write( (int) m_ApiChance );
			writer.Write( (int) m_LoreSkill);
			writer.Write( (bool) m_HiveFull);
			writer.Write( (bool) m_HiveSick);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_HiveHoney = reader.ReadInt();
			m_HiveWaxes = reader.ReadInt();
			m_ApiChance = reader.ReadInt();
			m_LoreSkill = reader.ReadInt();
			m_HiveFull = reader.ReadBool();
			m_HiveSick = reader.ReadBool();
			if ( m_HiveFull == true ) new BeeHiveTimer(this).Start();
		}
	}

	public class BeeHiveAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new BeeHiveDeed(); } }

		[Constructable]
		public BeeHiveAddon()
		{
			AddComponent( new BeeHive(), 0, 0, 0 );
		}

		public BeeHiveAddon( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class BeeHiveDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new BeeHiveAddon(); } }
		public override int LabelNumber{ get{ return 1022330; } }

		[Constructable]
		public BeeHiveDeed(){}

		public BeeHiveDeed( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}