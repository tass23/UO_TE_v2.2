using System;
using Server;
using Server.Mobiles;
using Server.Spells;

namespace Server.Items
{
	public class   deceptiveCrystal : Item
	{
		public override string DefaultName
		{
			get { return "Deceptive Crystal"; }
		}

		[Constructable]
		public  deceptiveCrystal() : base( 0x1F19 )
		{
			Weight = 1.0;
			Hue = 0x7F8;
		}

		public  deceptiveCrystal( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
				return;
			}

			double NecroSkill = from.Skills[SkillName.Necromancy].Value;

			if ( NecroSkill < 30.0 )
			{
				from.SendMessage( "You must have at least 30.0 skill in Necromancy to construct a skeleton." );
				return;
			}

			double scalar;

			if ( NecroSkill >= 100.0 )
				scalar = 3.0;
			else if ( NecroSkill >= 90.0 )
				scalar = 2.5;
			else if ( NecroSkill >= 80.0 )
				scalar = 2.0;
			else if ( NecroSkill >= 70.0 )
				scalar = 1.5;
			else
				scalar = 1.0;

			Container pack = from.Backpack;

			if ( pack == null )
				return;

			int res = pack.ConsumeTotal(
				new Type[]
				{
					typeof( SkelBod ),
					typeof( SkelLegs )	
				},
				new int[]
				{
					1,
					1	
				} );

			switch ( res )
			{
				case 0:
				{
					from.SendMessage( "You must have a skeleton torso to construct the skeleton." );
					break;
				}
				case 1:
				{
					from.SendMessage( "You must have a pair of skeleton legs to construct the skeleton." );
					break;
				}
				default:
				{
					SkeletalWorrior g = new SkeletalWorrior( true, scalar );

					if ( g.SetControlMaster( from ) )
					{
						Delete();

						g.MoveToWorld( from.Location, from.Map );
						from.PlaySound( 0x241 );
					}

					break;
				}
			}
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