using System;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Engines.Craft;
using Server.Mobiles;

namespace Server.Items
{
	public class SilverOre : Item
	{

		[Constructable]
		public SilverOre() : base( 0x19B8 )
		{
			Weight = 5.0;
			Hue = 2955;
			Name = "silver ore";
			Stackable = true;			
		}
		
		public SilverOre( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;
			
			if ( RootParent is BaseCreature )
			{
				from.SendLocalizedMessage( 500447 ); // That is not accessible
				return;
			}
			else if ( from.InRange( this.GetWorldLocation(), 2 ) )
			{
				from.SendLocalizedMessage( 501971 ); // Select the forge on which to smelt the ore, or another pile of ore with which to combine it.
				from.Target = new InternalTarget( this );
			}
			else
			{
				from.SendLocalizedMessage( 501976 ); // The ore is too far away.
			}
		}
		
		private class InternalTarget : Target
		{
			private SilverOre m_silverore;

			public InternalTarget( SilverOre silverore ) :  base ( 2, false, TargetFlags.None )
			{
				m_silverore = silverore;
			}

			private bool IsForge( object obj )
			{
				if ( Core.ML && obj is Mobile && ((Mobile)obj).IsDeadBondedPet )
					return false;

				if ( obj.GetType().IsDefined( typeof( ForgeAttribute ), false ) )
					return true;

				int itemID = 0;

				if ( obj is Item )
					itemID = ((Item)obj).ItemID;
				else if ( obj is StaticTarget )
					itemID = ((StaticTarget)obj).ItemID;

				return ( itemID == 4017 || (itemID >= 6522 && itemID <= 6569) );
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_silverore.Deleted )
					return;

				if ( !from.InRange( m_silverore.GetWorldLocation(), 2 ) )
				{
					from.SendLocalizedMessage( 501976 ); // The ore is too far away.
					return;
				}

				if ( IsForge( targeted ) )
				{
					double difficulty;

					difficulty = 50.0;

					double minSkill = difficulty - 25.0;
					double maxSkill = difficulty + 25.0;
					
					if ( difficulty > 50.0 && difficulty > from.Skills[SkillName.Mining].Value )
					{
						from.SendLocalizedMessage( 501986 ); // You have no idea how to smelt this strange ore!
						return;
					}

					if ( from.CheckTargetSkill( SkillName.Mining, targeted, minSkill, maxSkill ) )
					{
						if ( m_silverore.Amount <= 0 )
						{
							from.SendLocalizedMessage( 501987 ); // There is not enough metal-bearing ore in this pile to make an ingot.
						}
						else
						{
							int amount = m_silverore.Amount;
							if ( m_silverore.Amount > 30000 )
								amount = 30000;

							SilverIngot silveringot = m_silverore.GetSilverIngot();

							if ( m_silverore.ItemID == 0x19B9 )
							{
								amount *= 2;
								m_silverore.Delete();
							}

							else
							{
								amount /= 1;
								m_silverore.Delete();
							}

							silveringot.Amount = amount;
							from.AddToBackpack( silveringot );
							//from.PlaySound( 0x57 );

							from.SendLocalizedMessage( 501988 ); // You smelt the ore removing the impurities and put the metal in your backpack.
						}
					}
					else if ( m_silverore.Amount < 2 && m_silverore.ItemID == 0x19B9 )
					{
						from.SendLocalizedMessage( 501990 ); // You burn away the impurities but are left with less useable metal.
						m_silverore.ItemID = 0x19B8;
					}
					else
					{
						from.SendLocalizedMessage( 501990 ); // You burn away the impurities but are left with less useable metal.
						m_silverore.Amount /= 2;
					}
				}
			}
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
		
		public SilverIngot GetSilverIngot()
		{
			return new SilverIngot();
		}
		
	}
}