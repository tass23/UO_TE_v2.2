using System;
using Server.Items;
using Reward = Server.Engines.Quests.BaseReward;

namespace Server.Items
{
	public class CrownPieces : Item
	{
		[Constructable]
		public CrownPieces( ) : base( Utility.RandomList ( 12126 ) )
		{
			Weight = 2.0;
			Name = "an ancient crown fragment";
			Hue = 1266;
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
				return;
			}
			
			Container pack = from.Backpack;

			if ( pack == null )
				return;
				
			int tele = pack.ConsumeTotal(
				new Type[]
				{
					typeof( CrownPieces )
				},
				new int[]
				{
					8
				} );


			switch ( tele )
			{
				case 0:
				{
					from.SendMessage("You need 8 ancient crown fragments.");
					break;
				}
				default:
				{
					from.SendMessage("You have reassembled the ancient crown!");
					AncientCrown r = new AncientCrown();	//This is the new item that is created and drop on the ground next to the player.

					r.MoveToWorld( from.Location, from.Map );
					from.PlaySound( 0x241 );

					break;
				}

			}
		}
		
		public CrownPieces( Serial serial ) : base( serial )
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