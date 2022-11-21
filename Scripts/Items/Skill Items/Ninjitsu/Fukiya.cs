using System;
using Server;
using Server.Targeting;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Mobiles;

namespace Server.Items
{
	[FlipableAttribute( 0x27AA, 0x27F5 )]
	public class Fukiya : Item, INinjaWeapon
	{
		public virtual int WrongAmmoMessage { get { return 1063329; } } //You can only load fukiya darts
		public virtual int NoFreeHandMessage { get { return 1063327; } } //You must have a free hand to use a fukiya.
		public virtual int EmptyWeaponMessage { get { return 1063325; } } //You have no fukiya darts!
		public virtual int RecentlyUsedMessage { get { return 1063326; } } //You are already using that fukiya.
		public virtual int FullWeaponMessage { get { return 1063330; } } //You can only load fukiya darts

		public virtual int WeaponMinRange { get { return 0; } }
		public virtual int WeaponMaxRange { get { return 6; } }

		public virtual int WeaponDamage { get { return Utility.RandomMinMax(4, 6); } }

		public  Type AmmoType{ get { return typeof(FukiyaDarts); } }

		private int m_UsesRemaining;
		private Poison m_Poison;
		private int m_PoisonCharges;

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Poison Poison
		{
			get{ return m_Poison; }
			set{ m_Poison = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int PoisonCharges
		{
			get { return m_PoisonCharges; }
			set { m_PoisonCharges = value; InvalidateProperties(); }
		}

		public bool ShowUsesRemaining{ get{ return true; } set{} }

		[Constructable]
		public Fukiya() : base( 0x27AA )
		{
			Weight = 4.0;
			Layer = Layer.OneHanded;
		}

		public Fukiya( Serial serial ) : base( serial )
		{
		}

		public void AttackAnimation(Mobile from, Mobile to)
		{
			if (from.Body.IsHuman && !from.Mounted)
			{
				from.Animate(33, 2, 1, true, true, 0);
			}

			from.PlaySound(0x223);
			from.MovingEffect(to, 0x2804, 5, 0, false, false);
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060584, m_UsesRemaining.ToString() ); // uses remaining: ~1_val~

			#region Mondain's Legacy mod
			if ( m_Poison != null && m_PoisonCharges > 0 )
				list.Add( m_Poison.LabelNumber, m_PoisonCharges.ToString() );
			#endregion
		}

		public override void OnDoubleClick( Mobile from )
		{
			NinjaWeapon.AttemptShoot((PlayerMobile)from, this);
		}

/*		public void Shoot( Mobile from, Mobile target )
		{
			if ( from == target )
				return;

			if ( m_UsesRemaining < 1 )
			{
				// You have no fukiya darts!
				from.SendLocalizedMessage( 1063325 );
			}
			else if (((PlayerMobile)from).NinjaWepCooldown)
			{
				// You are already using that fukiya.
				from.SendLocalizedMessage( 1063326 );
			}
			else if ( !BasePotion.HasFreeHand( from ) )
			{
				// You must have a free hand to use a fukiya.
				from.SendLocalizedMessage( 1063327 );
			}
			else if ( from.CanBeHarmful( target ) )
			{
				((PlayerMobile)from).NinjaWepCooldown = true;

				from.Direction = from.GetDirectionTo( target );

				from.RevealingAction();

				if ( from.Body.IsHuman && !from.Mounted )
					from.Animate( 33, 2, 1, true, true, 0 );

				from.PlaySound( 0x223 );
				from.MovingEffect( target, 0x2804, 5, 0, false, false );

				if ( from.CheckSkill( SkillName.Ninjitsu, -10.0, 50.0 ) )
					Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( OnDartHit ), new object[]{ from, target } );
				else
					ConsumeUse();

				Timer.DelayCall( TimeSpan.FromSeconds( 2.5 ), new TimerStateCallback( ResetUsing ), from );
			}
		}

		private void OnDartHit( object state )
		{
			object[] states = (object[])state;
			Mobile from = (Mobile)states[0];
			Mobile target = (Mobile)states[1];

			if ( !from.CanBeHarmful( target ) )
				return;

			from.DoHarmful( target );

			AOS.Damage( target, from, Utility.RandomMinMax( 4, 6 ), 100, 0, 0, 0, 0 );

			if ( m_Poison != null && m_PoisonCharges > 0 )
				target.ApplyPoison( from, m_Poison );

			ConsumeUse();
		}

		public void ConsumeUse()
		{
			if ( m_UsesRemaining < 1 )
				return;

			--UsesRemaining;

			if ( m_PoisonCharges > 0 )
			{
				--PoisonCharges;

				if ( m_PoisonCharges == 0 )
					Poison = null;
			}
		}

		public void ResetUsing(object state)
		{
			PlayerMobile from = (PlayerMobile)state;
			from.NinjaWepCooldown = false;
		}

		private const int MaxUses = 10;

		public void Unload( Mobile from )
		{
			if ( m_UsesRemaining < 1 )
				return;

			FukiyaDarts darts = new FukiyaDarts( m_UsesRemaining );

			darts.Poison = m_Poison;
			darts.PoisonCharges = m_PoisonCharges;

			from.AddToBackpack( darts );

			UsesRemaining = 0;
			PoisonCharges = 0;
			Poison = null;
		}

		public void Reload( Mobile from, FukiyaDarts darts )
		{
			int need = ( MaxUses - m_UsesRemaining );

			if ( need <= 0 )
			{
				// You cannot add anymore fukiya darts
				from.SendLocalizedMessage( 1063330 );
			}
			else if ( darts.UsesRemaining > 0 )
			{
				bool canload = false;
				bool poison = false;

				if ( need > darts.UsesRemaining )
					need = darts.UsesRemaining;

				if ( darts.Poison != null && darts.PoisonCharges > 0 )
				{
					poison = true;
					#region Mondain's Legacy mod
					if ( m_Poison == null || ( m_Poison.RealLevel < darts.Poison.RealLevel ))
					{
							Unload( from );
						canload = true;
					}
					#endregion
					else if( m_Poison != null && ( m_Poison.RealLevel == darts.Poison.RealLevel ))
					{
						canload = true;
					}
				}
				else if( darts.Poison == null || darts.PoisonCharges <= 0 )
				{
					if( m_Poison == null || m_PoisonCharges <= 0 )
					{
						canload = true;
					}
				}

				if( !canload )
				{
					from.SendLocalizedMessage( 1070767 ); // Loaded projectile is stronger, unload it first
				}
				else
				{
					if( poison )
					{
						if ( need > darts.PoisonCharges )
						{
							need = darts.PoisonCharges;
						}

						if ( m_Poison == null || m_PoisonCharges <= 0 )
						{
							PoisonCharges = need;
						}
						else
						{
							PoisonCharges += need;
						}

						Poison = darts.Poison;

						darts.PoisonCharges -= need;

						if ( darts.PoisonCharges <= 0 )
						{
							darts.Poison = null;
					}
					}

					UsesRemaining += need;
					darts.UsesRemaining -= need;
				}

				if ( darts.UsesRemaining <= 0 )
					darts.Delete();
			}
		}

		public void OnTarget( Mobile from, object obj )
		{
			if ( Deleted || !IsChildOf( from ) )
				return;

			if ( obj is Mobile )
				Shoot( from, (Mobile) obj );
			else if ( obj is FukiyaDarts )
				Reload( from, (FukiyaDarts) obj );
			else
				from.SendLocalizedMessage( 1063329 ); // You can only load fukiya darts
		}*/

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );

			if ( IsChildOf( from ) )
			{
				list.Add(new NinjaWeapon.LoadEntry(this));
				list.Add(new NinjaWeapon.UnloadEntry(this));
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );

			writer.Write( (int) m_UsesRemaining );

			Poison.Serialize( m_Poison, writer );
			writer.Write( (int) m_PoisonCharges );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_UsesRemaining = reader.ReadInt();

					m_Poison = Poison.Deserialize( reader );
					m_PoisonCharges = reader.ReadInt();

					break;
				}
			}
		}
	}
}