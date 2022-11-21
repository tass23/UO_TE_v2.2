using System;
using Server;

namespace Server.Engines.XmlSpawner2
{
	public class CustomData : XmlAttachment
	{
		private string m_DataValue = null; 

		[CommandProperty( AccessLevel.GameMaster )]
		public string Data { get{ return m_DataValue; } set { m_DataValue = value; } }

		public CustomData(ASerial serial) : base(serial)
		{
		}

		[Attachable]
		public CustomData()
		{
		}

		[Attachable]
		public CustomData(string data)
		{
			Data = data;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize(writer);

			writer.Write( (int) 0 );
			// version 0
			writer.Write((string)m_DataValue);

		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
			// version 0
			m_DataValue = reader.ReadString();
		}
	}
}