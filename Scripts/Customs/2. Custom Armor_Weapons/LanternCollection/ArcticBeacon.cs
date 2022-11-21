using System;
using Server;

namespace Server.Items
{
    public class ArcticBeacon : MetalShield, ITokunoDyable
    {
		public override int ArtifactRarity{ get{ return 100; } }
        public override int BaseColdResistance{ get{ return 15; } }
		public override int BaseFireResistance{ get{ return 0; } }
        public override int InitMinHits{ get{ return 255; } }
        public override int InitMaxHits{ get{ return 255; } }

        [Constructable]
        public ArcticBeacon()
        {
            Name = "Arctic Beacon";
			ItemID = 2597;
            Hue = Utility.RandomList( 1150, 1151, 1152, 1153, 1154, 2066 );
            StrRequirement = 45;
            Attributes.SpellChanneling = 1;
            Attributes.NightSight = 1;
			Attributes.ReflectPhysical = 14;
            Attributes.Luck = 100;
            
        }

        public ArcticBeacon(Serial serial) : base( serial )
        {
        }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else
			{
				if ( this.ItemID == 2597 )
				{
					this.ItemID = 2594;
				}
				else if ( this.ItemID == 2594 )
				{
					this.ItemID = 2597;
				}
				else if (this.ItemID != 2597 || this.ItemID != 2594 )
				{
					from.SendMessage("There was a problem lighting your lantern. Please contact a staff member");				
				}
				else
				{
					from.SendMessage( "Your lantern is broken. Please contact a staff member to repair it!" );
				}
			}
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