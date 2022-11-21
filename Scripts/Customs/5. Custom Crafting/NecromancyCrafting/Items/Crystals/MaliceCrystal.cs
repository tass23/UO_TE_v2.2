using System;
using Server;
using Server.Mobiles;
using Server.Spells;

namespace Server.Items
{
	public class MaliceCrystal : Item
	{
		public override string DefaultName
		{
			get { return "Malice Crystal"; }
		}

		[Constructable]
		public MaliceCrystal() : base( 0x1F19 )
		{
			Weight = 1.0;
			Hue = 0x506;
		}

		public MaliceCrystal( Serial serial ) : base( serial )
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

			if ( NecroSkill < 100.0 )
			{
				from.SendMessage( "You must have at least 100.0 skill in Necromancy to construct a lich." );
				return;
			}

			double scalar;

			if ( NecroSkill >= 100.0 )
				scalar = 1.0;
			else if ( NecroSkill >= 90.0 )
				scalar = 1.0;
			else if ( NecroSkill >= 80.0 )
				scalar = 1.0;
			else if ( NecroSkill >= 70.0 )
				scalar = 1.0;
			else
				scalar = 1.0;

			Container pack = from.Backpack;

			if ( pack == null )
				return;

			int res = pack.ConsumeTotal(
				new Type[]
				{
					typeof( WrappedMageBod ),
					typeof( WrappedLegs ),
					typeof( Phylacery ),
					typeof( Mind ),
					typeof( NecromancerSpellbook )
				},
				new int[]
				{
					1,
					1,
					1,
					1,
					1
				} );

			switch ( res )
			{
				case 0:
				{
					from.SendMessage( "You must have an inscribed mummified torso to construct the lich." );
					break;
				}
				case 1:
				{
					from.SendMessage( "You must have a pair of mummified legs to construct the lich." );
					break;
				}
				case 2:
				{
					from.SendMessage( "You must have a phylacery to bind the soul of the lich." );
					break;
				}
				case 3:
				{
					from.SendMessage( "You must provide a new mind to control such a powerful creature." );
					break;
				}
				case 4:
				{
					from.SendMessage( "A lich requires a spellbook to cast.");
					break;
				}
				default:
				{
					Vecna g = new Vecna( true, scalar );

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