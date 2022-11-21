using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.Multis;
using Server.Targeting;
using Server.ContextMenus;

namespace Server.Items
{
	public enum TeleporterQuality
	{
		Normal,
		Exceptional
	}

	public class AddonHouseTeleporter : Item, IChopable, ISecurable
	{
		private Item m_Target;
		private SecureLevel m_Level;
		private TeleporterQuality m_Quality;
		private bool m_AllowDifferentHouses;

		[CommandProperty(AccessLevel.GameMaster)]
		public Item Target
		{
			get { return m_Target; }
			set { m_Target = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public SecureLevel Level
		{
			get { return m_Level; }
			set { m_Level = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool AllowDifferentHouses { get { return m_AllowDifferentHouses; } set { m_AllowDifferentHouses = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public TeleporterQuality Quality { get { return m_Quality; } set { m_Quality = value; } }

		public AddonHouseTeleporter(int itemID) : this(itemID, null)
		{
		}

		public AddonHouseTeleporter(int itemID, Item target) : base( itemID )
		{
			m_Level = SecureLevel.Owner;
			m_Target = target;
		}

		public bool CheckAccess(Mobile m)
		{
			BaseHouse house = BaseHouse.FindHouseAt(this);

			if (house != null && (house.Public ? house.IsBanned(m) : !house.HasAccess(m)))
				return false;

			return (house != null && house.HasSecureAccess(m, m_Level));
		}

		public override bool OnMoveOver(Mobile m)
		{
			BaseHouse house = BaseHouse.FindHouseAt(m);
			if (house != null && !BaseHouse.CheckLockedDownOrSecured(this))
				house.AddSecure(m, (Item)this);
			if (Target != null || !Target.Deleted)
			{
				//return base.OnMoveOver(m);
				if (CheckAccess(m))
				{
					if (!m.Hidden || m.AccessLevel == AccessLevel.Player)
						new EffectTimer(Location, Map, 2023, 0x1F0, TimeSpan.FromSeconds(0.4)).Start();

					new DelayTimer(this, m).Start();
				}
				else
				{
					m.SendLocalizedMessage(1061637); // You are not allowed to access this.
				}
			}
			else
			{
				m.SendMessage(32, "The other side of the teleporter has been destroyed, the magic in this teleporter fades as you step on it.");
				if (Target == null)
				{
					if (house != null)
						house.ReleaseSecure(m, (Item)this);
					Delete();
				}
				return true;
			}
			return true;
		}

		#region Code To Prevent it from being moved
		public override bool OnDragLift(Mobile from)
		{
			BaseHouse house = BaseHouse.FindHouseAt(from);
			if (house != null && !BaseHouse.CheckLockedDownOrSecured(this))
				house.AddSecure(from, (Item)this);
			from.SendMessage("You must chop this to remove.");
			return false;
		}
		public override bool Decays { get { return !IsLockedDown || !IsSecure; } }
		#endregion

		public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
		{
			base.GetContextMenuEntries(from, list);
			SetSecureLevelEntry.AddTo(from, this, list);
		}

		#region Serialization
		public AddonHouseTeleporter(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(2); // version

			writer.Write((bool)m_AllowDifferentHouses);
			writer.Write((int)m_Level);
			writer.Write((Item)m_Target);
			writer.Write((int)m_Quality);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 2:
					{
						m_AllowDifferentHouses = reader.ReadBool();
						goto case 1;
					}
				case 1:
					{
						m_Level = (SecureLevel)reader.ReadInt();
						goto case 0;
					}
				case 0: 
					{
						m_Target = reader.ReadItem();
						m_Quality = (TeleporterQuality)reader.ReadInt();

						if (version < 0)
						{
							m_Level = SecureLevel.Owner;
							m_Quality = TeleporterQuality.Normal;
						}
						break; 
					}
			}
		}
		#endregion

		#region Timers
		private class EffectTimer : Timer
		{
			private Point3D m_Location;
			private Map m_Map;
			private int m_EffectID;
			private int m_SoundID;

			public EffectTimer(Point3D p, Map map, int effectID, int soundID, TimeSpan delay) : base(delay)
			{
				m_Location = p;
				m_Map = map;
				m_EffectID = effectID;
				m_SoundID = soundID;
			}

			protected override void OnTick()
			{
				Effects.SendLocationParticles(EffectItem.Create(m_Location, m_Map, EffectItem.DefaultDuration), 0x3728, 10, 10, m_EffectID, 0);

				if (m_SoundID != -1)
					Effects.PlaySound(m_Location, m_Map, m_SoundID);
			}
		}

		private class DelayTimer : Timer
		{
			private AddonHouseTeleporter m_Teleporter;
			private Mobile m_Mobile;

			public DelayTimer(AddonHouseTeleporter tp, Mobile m) : base(TimeSpan.FromSeconds(1.0))
			{
				m_Teleporter = tp;
				m_Mobile = m;
			}

			protected override void OnTick()
			{
				Item target = m_Teleporter.Target;

				if (target != null && !target.Deleted)
				{
					Mobile m = m_Mobile;

					if (m.Location == m_Teleporter.Location && m.Map == m_Teleporter.Map)
					{
						Point3D p = target.GetWorldTop();
						Map map = target.Map;

						Server.Mobiles.BaseCreature.TeleportPets(m, p, map);

						m.MoveToWorld(p, map);

						if (!m.Hidden || m.AccessLevel == AccessLevel.Player)
						{
							Effects.PlaySound(target.Location, target.Map, 0x1FE);

							Effects.SendLocationParticles(EffectItem.Create(m_Teleporter.Location, m_Teleporter.Map, EffectItem.DefaultDuration), 0x3728, 10, 10, 2023, 0);
							Effects.SendLocationParticles(EffectItem.Create(target.Location, target.Map, EffectItem.DefaultDuration), 0x3728, 10, 10, 5023, 0);

							new EffectTimer(target.Location, target.Map, 2023, -1, TimeSpan.FromSeconds(0.4)).Start();
						}
					}
				}
			}
		}
		#endregion

		#region IChopable Members
		public void OnChop(Mobile from)
		{
			BaseHouse house = BaseHouse.FindHouseAt(from);

			if (house != null && house.IsOwner(from))
			{
				HouseTeleporterDeed deed = new HouseTeleporterDeed();
				if (Target != null)
				{
					BaseHouse house2 = BaseHouse.FindHouseAt(Target);
					if (house2 != null)
					{
						//house2.Addons.Remove(Target);
						house2.ReleaseSecure(from, (Item)Target);
					}
					Target.Delete();
				}
				house.ReleaseSecure(from, (Item)this);
				//house.Addons.Remove(this);
				deed.Quality = Quality;
				deed.AllowDifferentHouses = AllowDifferentHouses;
				from.AddToBackpack(deed);
				Delete();

				from.SendMessage("You destroy the teleporter! A deed has been placed in your pack.");
			}
		}
		#endregion
	}
}