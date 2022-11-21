using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.Spells;

namespace Server.Items
{
	public class UnderworldChest : PlateChest
	{
		public static void Initialize()
		{
			EventSink.PlayerDeath += new PlayerDeathEventHandler(EventSink_Death);
		}

		public override int ArtifactRarity{ get{ return 100; } }
		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 10; } }
		public override int BaseColdResistance{ get{ return 10; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 10; } }
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public UnderworldChest()
		{
			Name = "Breastplate of the Underworld";
			Hue = 1719;
			ItemID = 11016;
			Attributes.DefendChance = 15;
			ArmorAttributes.MageArmor = 1;
			ArmorAttributes.SelfRepair = 3;
			LootType = LootType.Blessed;
		}

		private static void EventSink_Death(PlayerDeathEventArgs e)
		{
			PlayerMobile from = e.Mobile as PlayerMobile;
			if (from != null && !from.Deleted)
			{
				if (from.Alive )
					return;

				UnderworldChest chest = from.Backpack.FindItemByType( typeof( UnderworldChest)) as UnderworldChest;
				UnderworldCloak cloak = from.Backpack.FindItemByType( typeof( UnderworldCloak)) as UnderworldCloak;
				UnderworldShield shield = from.Backpack.FindItemByType( typeof( UnderworldShield)) as UnderworldShield;
				if ( chest != null && shield != null && cloak != null )
				{
					if ( cloak.Name == "Cloak of the Underworld (Activated)" )
					{
						from.Resurrect();
						from.MoveToWorld( cloak.MarkLoc, cloak.MarkMap );

						if ( from.Corpse != null )
							from.Corpse.MoveToWorld( cloak.MarkLoc, cloak.MarkMap );

						ArrayList petlist = new ArrayList();
						foreach ( Mobile m in World.Mobiles.Values )
						{
							if ( m is BaseCreature )
							{
								BaseCreature bc = (BaseCreature)m;

								if ( (bc.Controlled && bc.ControlMaster == from) || (bc.Summoned && bc.SummonMaster == from) )
									petlist.Add( bc );
							}
						}
						if ( petlist != null && petlist.Count > 0 )
						{
							for( int i = 0; i < petlist.Count; i++ )
							{
								Mobile m = (Mobile)petlist[i];
								if ( m is BaseCreature )
								{
									BaseCreature bc = m as BaseCreature;
									if ( bc.IsBonded == true && bc.Hits <= 0 )
										bc.ResurrectPet();
									bc.MoveToWorld( cloak.MarkLoc, cloak.MarkMap );
								} 
							}
						}
					}
				}
			}
		}

		public UnderworldChest( Serial serial ) : base( serial )
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

	public class UnderworldCloak : Cloak
	{
		public override int ArtifactRarity{ get{ return 100; } }
		private Point3D m_MarkLoc;
		private Map m_MarkMap;

		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D MarkLoc
		{
			get{ return m_MarkLoc; }
			set{ m_MarkLoc = value; }
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public Map MarkMap
		{
			get{ return m_MarkMap; }
			set{ m_MarkMap = value; }
		}

		[Constructable]
		public UnderworldCloak()
		{
			Name = "Cloak of the Underworld (Deactivated)";
			Hue = 1765;
			Resistances.Physical = 10;
			Resistances.Fire = 10;
			m_MarkLoc = new Point3D( 1431, 1698, 0 );
			m_MarkMap = Map.Felucca;
			LootType = LootType.Blessed;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( this.Name == "Cloak of the Underworld (Deactivated)" && from.FindItemOnLayer( Layer.InnerTorso ) is UnderworldChest && from.FindItemOnLayer( Layer.TwoHanded ) is UnderworldShield )
			{
				this.Name = "Cloak of the Underworld (Activated)";
				from.SendMessage( 0, "You activate the Cloak's magic. When you die, you, your corpse, and your pets will be moved to the location marked on the Cloak, and you will be ressurrected. To change this location, cast the Mark spell on the cloak." );
			}
			else if ( this.Name == "Cloak of the Underworld (Activated)" )
			{
				this.Name = "Cloak of the Underworld (Deactivated)";
				from.SendMessage( 0, "You deactivate the cloak." );
			}
		}

		public UnderworldCloak( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( (Point3D) m_MarkLoc );
			writer.Write( (Map) m_MarkMap );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_MarkLoc = reader.ReadPoint3D();
			m_MarkMap = reader.ReadMap();
		}
	}

	public class UnderworldShield : OrderShield
	{
		public override int ArtifactRarity{ get{ return 100; } }
		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 10; } }
		public override int BaseColdResistance{ get{ return 10; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 10; } }
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }
		private DateTime m_NextUse;

		[Constructable]
		public UnderworldShield()
		{
			Name = "Shield of the Underworld";
			Hue = 1765;
			Attributes.DefendChance = 15;
			Attributes.SpellChanneling = 1;
			ArmorAttributes.SelfRepair = 2;
			Attributes.CastRecovery = 1;
			Attributes.CastSpeed = 1;
			LootType = LootType.Blessed;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.FindItemOnLayer( Layer.InnerTorso ) is UnderworldChest && from.FindItemOnLayer( Layer.TwoHanded ) is UnderworldShield && from.FindItemOnLayer( Layer.Cloak ) is UnderworldCloak && DateTime.Now > m_NextUse )
			{
				from.SendMessage( 0, "The Shield restores some health and mana." );
				from.Hits += 50;
				from.Mana += 50;
				m_NextUse = DateTime.Now + TimeSpan.FromMinutes( 1.0 );
			}
			else if ( from.FindItemOnLayer( Layer.InnerTorso ) is UnderworldChest && from.FindItemOnLayer( Layer.TwoHanded ) is UnderworldShield && from.FindItemOnLayer( Layer.Cloak ) is UnderworldCloak )
				from.SendMessage( 0, "That is recharging. Time left: {0}", m_NextUse - DateTime.Now );
			else
				from.SendMessage( 0, "You must have the Cloak, Shield, and Breastplate of the Underworld equipped for this to work." );
		}

		public UnderworldShield( Serial serial ) : base( serial )
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