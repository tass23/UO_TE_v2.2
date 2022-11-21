//scripted by Sterling
using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public class BlessBag : BaseContainer, IDyable, IEngravable
	{	
        public override int DefaultMaxItems { get { return 30; } }

        private Mobile m_Owner;
        
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get{ return m_Owner; }
			set{ m_Owner = value; }
		}

		[Constructable]
		public BlessBag() : base( 0x9b2 )
		{
			LootType = LootType.Blessed;
			Name ="Blessed Bag";
			Hue = 1152;
			Weight = 2.0;
            this.LootType = LootType.Newbied;
		}

        public override void OnItemLifted(Mobile from, Item item)
        {
            base.OnItemLifted(from, item);

            if (this.Owner == null && from is PlayerMobile)
                this.Owner = from;
        }
			
        public override void OnDoubleClick( Mobile from )
        {
            if (Owner != null && from != null)
            {
                if (Owner == from || from.AccessLevel > Owner.AccessLevel)
                {
                    base.OnDoubleClick(from);
                }
                else
                {
                    from.SendMessage(238, "This is not your bag, so now you will die!"); //appears to player
                    Effects.SendBoltEffect(from);
                    //Effects.PlaySound( 0x51E );
                    Effects.PlaySound(from.Location, from.Map, 0x51E);
                    from.Kill(); //remove if you dont want instant death
                }
            }
        }

        public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_Owner != null )
				list.Add( 1072304, m_Owner.Name ); 
		}

		public BlessBag( Serial serial ) : base( serial )
		{
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted ) return false;
			Hue = sender.DyedHue;
			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );

			writer.Write( (Mobile) m_Owner );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
				
			switch ( version )
			{
				case 0:
				{ 
					m_Owner = reader.ReadMobile();
					break;
				}
			}
			if( Owner == null )
				Name = "No OWNER"+ this.Name;
		}
	}

	public class BlessedBagDeed : Item 
	{
		[Constructable]
		public BlessedBagDeed() : this( 1 )
		{
			ItemID = 5360;
			Movable = true;
			Hue = 1152;
			Name = "Blessed Bag Deed";
		}
		
		public override void OnDoubleClick( Mobile from )
      	{
       		from.AddToBackpack( new BlessBag() ); 
       		this.Delete();
        }

		[Constructable]
		public BlessedBagDeed( int amount ) 
        {
		}		

		public BlessedBagDeed( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 0 ); // version 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 
		}
	}
}