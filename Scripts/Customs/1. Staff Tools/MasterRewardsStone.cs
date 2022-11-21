using System;
using Server.Gumps;

namespace Server.Items
{
    public class MasterRewardsStone : Item
    {
        [Constructable]
        public MasterRewardsStone() : base( 0xED4 )
        {
            Movable = false;
            Name = "a Reward Systems Stone";
        }

        public MasterRewardsStone( Serial serial ) : base( serial )
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
        
        public override void OnDoubleClick( Mobile from )
        {
            if ( from.InRange( GetWorldLocation(), 2 ) )
            {
				from.CloseGump( typeof( MasterRewardsGump ));
                from.SendGump( new MasterRewardsGump( from ) );
            }
            else
            {
                from.SendLocalizedMessage( 500446 ); // That is too far away.
            }
        }
    }
} 
