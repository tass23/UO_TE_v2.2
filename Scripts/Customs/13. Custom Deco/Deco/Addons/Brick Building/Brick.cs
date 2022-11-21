using Server;
using Server.Items;

namespace Gacoperz.Bricks
{
	public class Brick : Item, IDyable
	{
		public Mobile m_Owner;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get{ return m_Owner; }
			set{ m_Owner = value; }
		}
		public Brick(int itemID, Mobile owner) : base(itemID)
		{
			Name = "Brick";
			m_Owner = owner;
		}
		public Brick(Serial serial): base(serial)
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (Mobile) m_Owner );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_Owner = reader.ReadMobile();
		}
		public override void OnDoubleClick( Mobile from)
		{
			if( from == m_Owner)
			{
				if(Movable == true)
					Movable = false;
				else 
					Movable = true;
			}
		}
		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;
			else if ( Movable == true || (from == m_Owner && Movable == false))
			{
				Hue = sender.DyedHue;
				return true;
			}
			return false;
		}
	};
}