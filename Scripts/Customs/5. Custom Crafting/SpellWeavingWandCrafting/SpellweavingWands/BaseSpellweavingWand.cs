using System;
using System.Text;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Spells;
using Server.Spells.Spellweaving;

namespace Server.Items
{
	public enum SpellWeavingWandEffect
	{
		DryadAllure,
		WordOfDeath,
		Thunderstorm,
		SummonFiend,
		SummonFey,
		ReaperForm,
		NatureFury,
		GiftOfRenewal,
		GiftOfLife,
		EtherealVoyage,
        EssenceOfWind,
        Wildfire,
        ArcaneEmpowerment,
		ArcaneCircle
	}

	public abstract class BaseSpellWeavingWand : BaseBashing
	{
		private SpellWeavingWandEffect m_SpellWeavingWandEffect;
		private int m_Charges;

		public virtual TimeSpan GetUseDelay{ get{ return TimeSpan.FromSeconds( 4.0 ); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public SpellWeavingWandEffect Effect
		{
			get{ return m_SpellWeavingWandEffect; }
			set{ m_SpellWeavingWandEffect = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges
		{
			get{ return m_Charges; }
			set{ m_Charges = value; InvalidateProperties(); }
		}

		public BaseSpellWeavingWand( SpellWeavingWandEffect effect, int minCharges, int maxCharges ) : base( Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ) )
		{
			Weight = 3.0;
			Effect = effect;
			Charges = Utility.RandomMinMax( minCharges, maxCharges );
		}

		public BaseSpellWeavingWand( Serial serial ) : base( serial )
		{
		}

		public void ConsumeCharge( Mobile from )
		{
			--Charges;

			ApplyDelayTo( from );
		}

		public virtual void ApplyDelayTo( Mobile from )
		{
			from.BeginAction( typeof( BaseSpellWeavingWand ) );
			Timer.DelayCall( GetUseDelay, new TimerStateCallback( ReleaseSpellWeavingWandLock_Callback ), from );
		}

		public virtual void ReleaseSpellWeavingWandLock_Callback( object state )
		{
			((Mobile)state).EndAction( typeof( BaseSpellWeavingWand ) );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (int) m_SpellWeavingWandEffect );
			writer.Write( (int) m_Charges );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_SpellWeavingWandEffect = (SpellWeavingWandEffect)reader.ReadInt();
					m_Charges = (int)reader.ReadInt();

					break;
				}
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			switch ( m_SpellWeavingWandEffect )
			{
				case SpellWeavingWandEffect.DryadAllure:		list.Add( "dryadallure charges: " + m_Charges, m_Charges.ToString() ); break;
				case SpellWeavingWandEffect.WordOfDeath:		list.Add( "wordofdeath charges: " + m_Charges, m_Charges.ToString() ); break;
				case SpellWeavingWandEffect.Thunderstorm:		list.Add( "thunderstorm charges: " + m_Charges, m_Charges.ToString() ); break;
				case SpellWeavingWandEffect.SummonFiend:	        list.Add( "summonfiend charges: " + m_Charges, m_Charges.ToString() ); break;
				case SpellWeavingWandEffect.SummonFey:			list.Add( "summonfey charges: " + m_Charges, m_Charges.ToString() ); break;
				case SpellWeavingWandEffect.ReaperForm:			list.Add( "reaperform charges: " + m_Charges, m_Charges.ToString() ); break;
				case SpellWeavingWandEffect.NatureFury:			list.Add( "naturefury charges: " + m_Charges, m_Charges.ToString() ); break;
				case SpellWeavingWandEffect.GiftOfRenewal:		list.Add( "giftofrenewal charges: " + m_Charges, m_Charges.ToString() ); break;
				case SpellWeavingWandEffect.GiftOfLife:		        list.Add( "giftoflife charges: " + m_Charges, m_Charges.ToString() ); break;
				case SpellWeavingWandEffect.EtherealVoyage:		list.Add( "etherealvoyage charges: " + m_Charges, m_Charges.ToString() ); break;
				case SpellWeavingWandEffect.EssenceOfWind:		list.Add( "essenceofwind charges: " + m_Charges, m_Charges.ToString() ); break;
				case SpellWeavingWandEffect.Wildfire:		        list.Add( "wildfire charges: " + m_Charges, m_Charges.ToString() ); break;
				case SpellWeavingWandEffect.ArcaneEmpowerment:		list.Add( "arcaneempowerment charges: " + m_Charges, m_Charges.ToString() ); break;
				case SpellWeavingWandEffect.ArcaneCircle:		list.Add( "arcanecircle charges: " + m_Charges, m_Charges.ToString() ); break;
			}
		}

		public override void OnSingleClick( Mobile from )
		{
			ArrayList attrs = new ArrayList();

			if ( DisplayLootType )
			{
				if ( LootType == LootType.Blessed )
					attrs.Add( new EquipInfoAttribute( 1038021 ) ); // blessed
				else if ( LootType == LootType.Cursed )
					attrs.Add( new EquipInfoAttribute( 1049643 ) ); // cursed
			}

			if ( !Identified )
			{
				attrs.Add( new EquipInfoAttribute( 1038000 ) ); // Unidentified
			}
			else
			{
				int num = 0;

				switch ( m_SpellWeavingWandEffect )
				{
					case SpellWeavingWandEffect.DryadAllure:		num = 3002622; break;
					case SpellWeavingWandEffect.WordOfDeath:		num = 3002624; break;
					case SpellWeavingWandEffect.Thunderstorm:		num = 3002615; break;
					case SpellWeavingWandEffect.SummonFiend:		num = 3002618; break;
					case SpellWeavingWandEffect.SummonFey:			num = 3002617; break;
					case SpellWeavingWandEffect.ReaperForm:			num = 3002619; break;
					case SpellWeavingWandEffect.NatureFury:			num = 3002616; break;
					case SpellWeavingWandEffect.GiftOfRenewal:		num = 3002612; break;
					case SpellWeavingWandEffect.GiftOfLife:			num = 3002625; break;
					case SpellWeavingWandEffect.EtherealVoyage:		num = 3002623; break;
					case SpellWeavingWandEffect.EssenceOfWind:		num = 3002621; break;
					case SpellWeavingWandEffect.Wildfire:		        num = 3002620; break;
					case SpellWeavingWandEffect.ArcaneEmpowerment:		num = 3002626; break;
					case SpellWeavingWandEffect.ArcaneCircle:		num = 3002611; break;
				}

				if ( num > 0 )
					attrs.Add( new EquipInfoAttribute( num, m_Charges ) );
			}

			int number;

			if ( Name == null )
			{
				number = 1017085;
			}
			else
			{
				this.LabelTo( from, Name );
				number = 1041000;
			}

			if ( attrs.Count == 0 && Crafter == null && Name != null )
				return;

			EquipmentInfo eqInfo = new EquipmentInfo( number, Crafter, false, (EquipInfoAttribute[])attrs.ToArray( typeof( EquipInfoAttribute ) ) );

			from.Send( new DisplayEquipmentInfo( this, eqInfo ) );
		}

		public void Cast( Spell spell )
		{
			bool m = Movable;

			Movable = false;
			spell.Cast();
			Movable = m;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.CanBeginAction( typeof( BaseSpellWeavingWand ) ) )
				return;

			if ( Parent == from )

			{
				if ( Charges > 0 )
					OnSpellWeavingWandUse( from );
				else
					this.Delete();

			}

                        if (this is BaseSpellWeavingWand)

                        {
                                this.Charges -= 1;

                        }
 
			else
			{
				from.SendLocalizedMessage( 502641 ); // You must equip this item to use it.
			}
		}

		public virtual void OnSpellWeavingWandUse( Mobile from )
		{
			from.Target = new SpellWeavingWandTarget( this );

		}

		public virtual void DoSpellWeavingWandTarget( Mobile from, object o )
		{
			 if ( Deleted || Parent != from )
				return;
		}

		public virtual bool OnSpellWeavingWandTarget( Mobile from, object o )
		{
			return true;
		}


	}
}