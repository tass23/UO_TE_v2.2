using System;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	[Flipable( 0x12CA, 0x12CB )]
	public class RawWaxBust : Item
	{
		private bool m_Pictured = false;

		[Constructable]
		public RawWaxBust() : base( 0x12CA )
		{
			Name = "Raw Wax Bust";
			Weight = 0.5;
			Hue = 1150;
		}


		public RawWaxBust( Serial serial ) : base( serial )
		{
		}

		[CommandProperty( AccessLevel.GameMaster)]
		public bool Pictured
		{
			get
			{
				return m_Pictured;
			}
			set
			{
				m_Pictured = value;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				if (m_Pictured == false )
				{
					if ( from.Skills[SkillName.Anatomy].Base < 100.0 )
					from.SendMessage( "You do not possess enough knowledge of the human body to carve a face into the raw wax bust." );
										
					else
					{
						from.Target = new InternalTarget( this );
					}
				}
				
				else
					from.SendMessage( "You cannot change the bust without destroying it." );
			}
			else
				from.SendMessage( "This must be in your backpack." );
		}

		private class InternalTarget : Target
		{
			private RawWaxBust it_Bust;
			
			public InternalTarget( RawWaxBust bust ) : base( 1, false, TargetFlags.None )
			{
				it_Bust = bust;
			}
			
			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted != null && targeted is PlayerMobile )
				{
					if ( it_Bust != null )
					{
						from.SendMessage( "You carefully apply the face of your model to the bust." );

						it_Bust.m_Pictured = true;
						it_Bust.Name = "Wax Bust Of " + ((Mobile)targeted).Name.ToString();
					}
					else
						return;
				}
				else
					from.SendMessage( "Invalid target" );
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( m_Pictured );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			switch ( version )
			{
				case 1:
				{
					m_Pictured = reader.ReadBool();
					break;
				}
			}

		}
	}
}