using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
	public class IceFragments : Item
	{
		private bool m_Decays;
		private DateTime m_DecayTime;
		private Timer m_Timer;

		private Mobile m_PetOwner;
		private Mobile m_Pet;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile PetOwner
		{ 
			get{ return m_PetOwner; } 
			set{ m_PetOwner = value; } 
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Pet
		{ 
			get{ return m_Pet; } 
			set{ m_Pet = value; } 
		}

		[Constructable]
		public IceFragments() : this( 1 )
		{
		}

		[Constructable]
		public IceFragments( int amount ) : base( 2272 )
		{
			Weight = 10.0;
			Name = "ice spike";
			Hue = 1152;
			Movable = false;

			m_Decays = true;
			m_DecayTime = DateTime.Now + TimeSpan.FromMinutes( 2.0 );
			m_Timer = new InternalTimer( this, m_DecayTime );
			m_Timer.Start();

		}


		public override bool HandlesOnMovement{ get{  return true; } }

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( m_Pet != null && m_PetOwner != null )
			{
				if ( m.Location != oldLocation )
				{
					if ( Utility.Random( 100 ) < 35 )
					{
						if ( m != m_PetOwner && m is PlayerMobile )
						{
							PlayerMobile pm = (PlayerMobile)m;
							if ( pm.Criminal == true || pm.Kills >= 5 || pm == m_PetOwner.Combatant || pm == m_Pet.Combatant || pm.Combatant == m_Pet || pm.Combatant == m_PetOwner )
							{
								Spells.SpellHelper.Damage( TimeSpan.FromSeconds( 0.5 ), pm, pm, Utility.RandomMinMax( 10, 40 ), 0, 100, 0, 0, 0 );
								pm.SendMessage( "You have taken damage from the extreme cold." );
							}
						}
						else if ( m != m_Pet && m is BaseCreature )
						{
							BaseCreature bc = (BaseCreature)m;
							Mobile cm = bc.ControlMaster;
							if ( bc.Controlled != true )
							{
								Spells.SpellHelper.Damage( TimeSpan.FromSeconds( 0.5 ), bc, bc, Utility.RandomMinMax( 10, 40 ), 0, 100, 0, 0, 0 );
							}
							else if ( bc == m_Pet.Combatant || bc == m_PetOwner.Combatant || bc.Combatant == m_Pet || bc.Combatant == m_PetOwner )
							{
								Spells.SpellHelper.Damage( TimeSpan.FromSeconds( 0.5 ), bc, bc, Utility.RandomMinMax( 10, 40 ), 0, 100, 0, 0, 0 );
					
								if ( cm != null )
									cm.SendMessage( "Your pet has taken damage from the extreme cold." );
							}
						}
					}
				}
			}
		}

		public override void OnAfterDelete()
		{
			if ( m_Timer != null )
				m_Timer.Stop();

			base.OnAfterDelete();
		}

		public IceFragments( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( m_PetOwner );

			writer.Write( m_Pet );

			writer.Write( m_Decays );

			if ( m_Decays )
				writer.WriteDeltaTime( m_DecayTime );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					m_PetOwner = reader.ReadMobile();
					m_Pet = reader.ReadMobile();
					goto case 0;
				}
				case 0:
				{
					m_Decays = reader.ReadBool();

					if ( m_Decays )
					{
						m_DecayTime = reader.ReadDeltaTime();

						m_Timer = new InternalTimer( this, m_DecayTime );
						m_Timer.Start();
					}

					break;
				}
			}
		}

		private class InternalTimer : Timer
		{
			private Item m_Item;

			public InternalTimer( Item item, DateTime end ) : base( end - DateTime.Now )
			{
				m_Item = item;
			}

			protected override void OnTick()
			{
				m_Item.Delete();
			}
		}
	}
}