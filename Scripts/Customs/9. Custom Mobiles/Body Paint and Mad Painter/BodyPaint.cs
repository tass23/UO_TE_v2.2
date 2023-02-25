

using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
	public class BodyPaint : Item
	{
		public override int LabelNumber{ get{ return 1040000; } } // savage kin paint

		[Constructable]
		public BodyPaint() : base( 0x9EC )
		{
			Name = " Body Paint ";
			Hue = Utility.RandomList( 1, 2, 12, 17, 22, 27, 32, 37, 42, 47, 53, 58, 63, 88, 1891, 123, 90, 34, 1234, 1175, 1153, 1150, 1072, 1166, 1392, 1152, 1164, 1165, 1170, 1172, 1171, 1173, 1194, 1195, 1196, 1197, 1198, 1199, 1289, 1288, 1287, 1174, 1284, 2949, 2967);
			Weight = 2.0;
		}

		public BodyPaint( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				if ( Factions.Sigil.ExistsOn( from ) )
				{
					from.SendLocalizedMessage( 1010465 ); // You cannot disguise yourself while holding a sigil.
				}
				else if ( !from.CanBeginAction( typeof( Spells.Fifth.IncognitoSpell ) ) )
				{
					from.SendLocalizedMessage( 501698 ); // You cannot disguise yourself while incognitoed.
				}
				else if ( !from.CanBeginAction( typeof( Spells.Seventh.PolymorphSpell ) ) )
				{
					from.SendLocalizedMessage( 501699 ); // You cannot disguise yourself while polymorphed.
				}
				/*
				else if ( Spells.Necromancy.TransformationSpell.UnderTransformation( from ) )
				{
					from.SendLocalizedMessage( 501699 ); // You cannot disguise yourself while polymorphed.
				}
				*/
				else if ( Spells.Ninjitsu.AnimalForm.UnderTransformation( from ) )
				{
					from.SendLocalizedMessage( 1061634 ); // You cannot disguise yourself while in that form.
				}
				else if ( from.IsBodyMod || from.FindItemOnLayer( Layer.Helm ) is OrcishKinMask )
				{
					from.SendLocalizedMessage( 501605 ); // You are already disguised.
				}
				else
				{
					from.BodyMod = ( from.Female ? 184 : 183 );
					from.HueMod = this.Hue;
					
					if ( from is PlayerMobile )
						((PlayerMobile)from).SavagePaintExpiration = TimeSpan.FromDays( 7.0 );

					from.SendMessage( "You now paint your body.  Your body paint will last about a week or you can remove it with an oil cloth.");
					Consume();
				}
			}
			else
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
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