using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Accounting;

using Server.Voting;

namespace Server.Items
{

	public class LinkStone : VoteItem
	{
		private string _LabelMessage = "Use: Launches your browser to " + ServerList.ServerName;

		[CommandProperty(AccessLevel.GameMaster)]
		public string LabelMessage
		{
			get { return _LabelMessage; }
			set
			{
				_LabelMessage = value;
				InvalidateProperties();
			}
		}

		[Constructable]
		public LinkStone()
			: this("Link Stone")
		{ }

		[Constructable]
		public LinkStone(string name)
			: this(name, 0)
		{ }

		[Constructable]
		public LinkStone(string name, int hue)
			: base(0xED4)
		{
			Name = name;
			Hue = hue;

			Movable = true;
		}
			
		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);

			if (VoteSite.Valid)
			{
				list.Add(1070722,
					String.Format(
						"<BASEFONT COLOR=#00FF00>{0}<BASEFONT COLOR=#FFFFFF>" +
						"\n[{1}] " +
						"{2}",
						_LabelMessage,
						VoteSite.Name,
						VoteSite.URL.ToUpper()
					)
				);
			}
			else
			{
				list.Add(1070722, "<BIG><BASEFONT COLOR=#FF0000>This link is currently unavailable.</BIG><BASEFONT COLOR=#FFFFFF>");
			}
		}

		public LinkStone(Serial serial)
			: base(serial)
		{ }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version 

			writer.Write((string)_LabelMessage);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
					{
						_LabelMessage = (string)reader.ReadString();
					} break;
			}
		}
	}
}