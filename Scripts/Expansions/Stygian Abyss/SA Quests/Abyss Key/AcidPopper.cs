using System;
using System.Collections.Generic;
using Server;
using Server.Mobiles;

namespace Server.Items
{
    public class AcidPopper : Item
    {
        public override int LabelNumber { get { return 1095058; } } // Acid Popper

        [Constructable]
        public AcidPopper() : this( 1 )
        {
        }

        [Constructable]
        public AcidPopper( int amount ) : base( 0x172A )
        {
            Hue = 68;
            Stackable = true;
            Amount = amount;
        }

        public override void OnDoubleClick( Mobile from )
        {
            if ( !IsChildOf( from.Backpack ) )
            {
                from.SendLocalizedMessage( 1060640 );   // The item must be in your backpack to use it.
                return;
            }

            List<NavreyParalyzingWeb> list = new List<NavreyParalyzingWeb>();
            foreach ( Item item in Map.GetItemsInRange( GetWorldLocation(), 0 ) )
            {
                if ( item is NavreyParalyzingWeb )
                    list.Add( (NavreyParalyzingWeb)item );
            }

            if ( 0 == list.Count )
                return;

            Consume();
            from.SendLocalizedMessage( 1113240 );   // The acid popper bursts and burns away the webbing.

            foreach (Item item in list)
                    item.Delete();
        }

        public AcidPopper( Serial serial ) : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( (int) 0 );
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );

            int version = reader.ReadInt();
        }
    }
}
