using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Multis;
using Server.Targeting;
using Server.Gumps;
using Server.Engines.Craft;

namespace Server.Items
{
	public class HouseTeleporterDeed : Item, ICraftable
	{
		private Point3D m_FirstTarget;
		private Point3D m_SecondTarget;
		private Map m_FirstMap;
		private Map m_SecondMap;
		private BaseHouse m_FirstHouse;
		private BaseHouse m_SecondHouse;
		private bool m_AllowDifferentHouses = false;
		private TeleporterQuality m_Quality;

		[CommandProperty(AccessLevel.GameMaster)]
		public Map FirstMap { get { return m_FirstMap; } set { m_FirstMap = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public Map SecondMap { get { return m_SecondMap; } set { m_SecondMap = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public Point3D FirstTarget { get { return m_FirstTarget; } set { m_FirstTarget = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public Point3D SecondTarget { get { return m_SecondTarget; } set { m_SecondTarget = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public BaseHouse FirstHouse { get { return m_FirstHouse; } set { m_FirstHouse = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public BaseHouse SecondHouse { get { return m_SecondHouse; } set { m_SecondHouse = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public bool AllowDifferentHouses { get { return m_AllowDifferentHouses; } set { m_AllowDifferentHouses = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public TeleporterQuality Quality { get { return m_Quality; } set { m_Quality = value; } }

		public override string DefaultName { get { return "House Teleporter Deed"; } }
		public override bool DisplayWeight { get { return false; } }

		[Constructable]
		public HouseTeleporterDeed() : base(0x14F0)
		{
			Weight = 1.0;
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);
			//if (m_Quality == TeleporterQuality.Exceptional)
				//list.Add(1018303); // Exceptional
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (from != null)
			{
				BaseHouse house = BaseHouse.FindHouseAt(from);
				if (house != null)
				{
					if (house.IsOwner(from))
					{
						if (m_FirstTarget != Point3D.Zero && m_AllowDifferentHouses)
						{
							from.SendMessage("Target the second location for the new teleporter.");
							from.Target = new GetSecondTarget(from, this);
						}
						else
						{
							Reset();
							from.SendMessage("Target the first location for the new teleporter.");
							from.Target = new GetFirstTarget(from, this);
						}
					}
					else
						from.SendMessage(32, "You must be the owner of this house to do this.");
				}
				else
					from.SendMessage(32, "You must be inside your house to use this.");
			}
		}

		public static bool CheckHouse(Mobile from, Point3D p, Map map, int height, ref List<BaseHouse> list)
		{
			if (from == null /*|| from.AccessLevel >= AccessLevel.GameMaster*/)
				return true;

			BaseHouse house = BaseHouse.FindHouseAt(p, map, height);

			if (from == null || house == null || !house.IsOwner(from))
				return false;

			if (!list.Contains(house))
				list.Add(house);

			return true;
		}

		public bool CouldFit(IPoint3D p, Map map)
		{
			List<BaseHouse> houses = null;

			return ( CouldFit( p, map, null, ref houses ) == AddonFitResult.Valid );
		}

		public AddonFitResult CouldFit(IPoint3D p, Map map, Mobile from, ref List<BaseHouse> houses)
		{
			if (Deleted)
				return AddonFitResult.Blocked;

			Point3D p3D = new Point3D(p);
			if (!map.CanFit(p3D.X, p3D.Y, p3D.Z, 0, false, true, true))
				return AddonFitResult.Blocked;
			else if (!CheckHouse(from, p3D, map, 0, ref houses))
				return AddonFitResult.NotInHouse;

			//if (houses != null)
			//{
			//	foreach (BaseHouse house in houses)
			//	{
			//		ArrayList doors = house.Doors;
			//		for (int i = 0; i < doors.Count; ++i)
			//		{
			//			BaseDoor door = (BaseDoor)doors[i];
			//			if (door != null && door.Open)
			//				return AddonFitResult.DoorsNotClosed;

			//			Point3D doorLoc = door.GetWorldLocation();
			//			int doorHeight = door.ItemData.CalcHeight;

			//			if (Utility.InRange(doorLoc, p3D, 1) && (p3D.Z == doorLoc.Z || (p3D.Z > doorLoc.Z && (doorLoc.Z + doorHeight) > p3D.Z)))
			//				return AddonFitResult.DoorTooClose;
			//		}
			//	}
			//}

			return AddonFitResult.Valid;
		}

		#region Targets
		public bool CheckTarget(Mobile from, object targeted, out Point3D point, out BaseHouse house)
		{
			point = Point3D.Zero;
			house = null;
			IPoint3D p = (IPoint3D)targeted;
			Map map = from.Map;

			if (p == null || map == null || Deleted)
				return false;

			if (targeted is HouseTeleporter || targeted is AddonHouseTeleporter)
			{
				from.SendMessage("You can't place a teleporter ontop of another teleporter.");
				return false;
			}

			if (IsChildOf(from.Backpack))
			{
				Server.Spells.SpellHelper.GetSurfaceTop(ref p);

				List<BaseHouse> houses = new List<BaseHouse>();

				AddonFitResult res = CouldFit(p, map, from, ref houses);

				if (res == AddonFitResult.Blocked)
					from.SendLocalizedMessage(500269); // You cannot build that there.
				else if (res == AddonFitResult.NotInHouse)
					from.SendLocalizedMessage(500274); // You can only place this in a house that you own!
				//else if (res == AddonFitResult.DoorsNotClosed)
					//from.SendMessage("You must close all house doors before placing this.");
				else if (res == AddonFitResult.DoorTooClose)
					from.SendLocalizedMessage(500271); // You cannot build near the door.
				else if (res == AddonFitResult.NoWall)
					from.SendLocalizedMessage(500268); // This object needs to be mounted on something.

				if (res == AddonFitResult.Valid)
				{
					if (houses.Count != 1)
					{
						from.SendMessage("House count is invalid.  Placement canceled.");
					}
					else
					{
						house = houses[0];
						point = new Point3D(p);
						return true;
					}
				}
			}
			else
				from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.

			return false;
		}

		private void TeleporterExists(Mobile from)
		{
			from.SendMessage(53, "This teleporter already exists in this house, select another.");
			from.SendGump(new HouseTeleporterSelectGump(from, this));
		}

		public void PlaceTeleporters(Mobile from, int teleporterID)
		{
			ArrayList secures = m_FirstHouse.Secures;
			List<Item> fixtures = null;
			if (m_FirstHouse is HouseFoundation)
				fixtures = ((HouseFoundation)m_FirstHouse).Fixtures;

			foreach (object o in secures)
			{
				if (o is AddonHouseTeleporter && ((AddonHouseTeleporter)o).ItemID == teleporterID)
				{
					TeleporterExists(from);
					return;
				}
			}
			if (fixtures != null)
			{
				foreach (Item item in fixtures)
				{
					if (item is HouseTeleporter && item.ItemID == teleporterID)
					{
						TeleporterExists(from);
						return;
					}
				}
			}

			if (m_FirstHouse != m_SecondHouse)
			{
				secures = m_SecondHouse.Secures;
				if (m_SecondHouse is HouseFoundation)
					fixtures = ((HouseFoundation)m_SecondHouse).Fixtures;
				else
					fixtures = null;

				foreach (object o in secures)
				{
					if (o is AddonHouseTeleporter && ((AddonHouseTeleporter)o).ItemID == teleporterID)
					{
						TeleporterExists(from);
						return;
					}
				}
				if (fixtures != null)
				{
					foreach (Item item in fixtures)
					{
						if (item is HouseTeleporter && item.ItemID == teleporterID)
						{
							TeleporterExists(from);
							return;
						}
					}
				}
			}

			AddonHouseTeleporter tporter1 = new AddonHouseTeleporter(teleporterID);
			tporter1.Quality = this.Quality;
			tporter1.AllowDifferentHouses = this.AllowDifferentHouses;
			tporter1.MoveToWorld(m_FirstTarget, m_FirstMap);
			m_FirstHouse.AddSecure(from, (Item)tporter1);
			//m_FirstHouse.Addons.Add(tporter1);

			AddonHouseTeleporter tporter2 = new AddonHouseTeleporter(teleporterID, tporter1);
			tporter2.Quality = this.Quality;
			tporter2.AllowDifferentHouses = this.AllowDifferentHouses;
			tporter2.MoveToWorld(m_SecondTarget, m_SecondMap);
			m_SecondHouse.AddSecure(from, (Item)tporter2);
			//m_SecondHouse.Addons.Add(tporter2);

			tporter1.Target = tporter2;

			from.SendMessage("The teleporters have been created.");
			// This code should be used for testing only staff can add new deeds.
			//if (from.AccessLevel == AccessLevel.Player)
				Delete();
			//else
				//Reset();
		}

		private class GetFirstTarget : Target
		{
			private HouseTeleporterDeed m_Deed;
			private Mobile m_From;

			public GetFirstTarget(Mobile from, HouseTeleporterDeed deed) : base(-1, true, TargetFlags.None)
			{
				m_From = from;
				m_Deed = deed;
				AllowNonlocal = true;
				CheckLOS = false;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (m_Deed != null)
				{
					Point3D p = Point3D.Zero;
					BaseHouse house = null;
					if (m_Deed.CheckTarget(from, targeted, out p, out house))
					{
						m_Deed.FirstHouse = house;
						m_Deed.FirstTarget = p;
						m_Deed.FirstMap = from.Map;
						if (!m_Deed.AllowDifferentHouses)
						{
							from.SendMessage("Now target the second location for the new teleporter.");
							from.Target = new GetSecondTarget(from, m_Deed);
						}
						else
						{
							from.SendMessage("First location has been set.  Double click the deed to set the second location (which may be in a different house.)");
						}
					}
				}
			}

			protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
			{
				from.SendMessage(53, "Placement canceled.");
			}
		}

		private class GetSecondTarget : Target
		{
			private HouseTeleporterDeed m_Deed;
			private Mobile m_From;

			public GetSecondTarget(Mobile from, HouseTeleporterDeed deed) : base(-1, true, TargetFlags.None)
			{
				m_From = from;
				m_Deed = deed;
				AllowNonlocal = true;
				CheckLOS = false;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (m_Deed != null)
				{
					Point3D p = Point3D.Zero;
					BaseHouse house = null;
					if (m_Deed.CheckTarget(from, targeted, out p, out house))
					{
						if (house != m_Deed.FirstHouse && !m_Deed.AllowDifferentHouses)
						{
							from.SendMessage("Both parts of the teleporter must be in the same house!");
							m_Deed.Reset();
							return;
						}
						m_Deed.SecondTarget = p;
						m_Deed.SecondMap = from.Map;
						m_Deed.SecondHouse = house;
						from.SendMessage("Select a teleporter style.");
						m_From.SendGump(new HouseTeleporterSelectGump(m_From, m_Deed));
					}
				}
			}

			protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
			{
				m_Deed.Reset();
				from.SendMessage(53, "Placement canceled.");
			}
		}
		#endregion

		#region Serialization
		public HouseTeleporterDeed(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write(0); // version
			writer.Write(m_AllowDifferentHouses);
			writer.Write((int)m_Quality);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
					m_AllowDifferentHouses = reader.ReadBool();
					m_Quality = (TeleporterQuality)reader.ReadInt();
					break;
			}
		}
		#endregion

		internal void Reset()
		{
			m_FirstTarget = Point3D.Zero;
			m_SecondTarget = Point3D.Zero;
			m_FirstMap = Map.Internal;
			m_SecondMap = Map.Internal;
			m_FirstHouse = null;
			m_SecondHouse = null;
		}

		#region ICraftable Members
		public int OnCraft(int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue)
		{
			m_Quality = (TeleporterQuality)quality;
			if (m_Quality == TeleporterQuality.Exceptional)
				m_AllowDifferentHouses = true;
			return quality;
		}
		#endregion
	}
}
