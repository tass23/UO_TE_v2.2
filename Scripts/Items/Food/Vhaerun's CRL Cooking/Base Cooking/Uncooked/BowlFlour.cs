using System;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class BowlFlour : Item, IUsesRemaining
	{
		private int m_Uses;

		public bool ShowUsesRemaining{ get{ return true; } set{} }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Uses
		{
			get { return m_Uses; }
			set { m_Uses = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_Uses; }
			set { m_Uses = value; InvalidateProperties(); }
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060584, m_Uses.ToString() );
		}

		public virtual void DisplayDurabilityTo( Mobile m )
		{
			LabelToAffix( m, 1017323, AffixType.Append, ": " + m_Uses.ToString() );
		}

		public override void OnSingleClick( Mobile from )
		{
			DisplayDurabilityTo( from );

			base.OnSingleClick( from );
		}

		[Constructable]
		public BowlFlour() : this( 10 )
		{
		}

		[Constructable]
		public BowlFlour( int StartingUses ) : base( 0xa1e )
		{
			Weight = 2.0;
			m_Uses = StartingUses;
		}

		public BowlFlour( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 );

			writer.Write( (int) m_Uses );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					m_Uses = reader.ReadInt();
					break;
				}
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;

			from.Target = new InternalTarget( this );
		}

		public void Use( Mobile from )
		{
			m_Uses--;
			InvalidateProperties();

			if( m_Uses <= 0 )
			{
				if ( Parent == null )
					new WoodenBowl().MoveToWorld( this.Location, this.Map );
				else
					from.AddToBackpack( new WoodenBowl() );
				Consume();
			}
		}

		private class InternalTarget : Target
		{
			private BowlFlour m_Item;

			public InternalTarget( BowlFlour item ) : base( 1, false, TargetFlags.None )
			{
				m_Item = item;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Item.Deleted ) return;

				if ( targeted is Pitcher )
				{
					if(!((Item)targeted).Movable) return;

					if ( BaseBeverage.ConsumeTotal( from.Backpack, typeof( Pitcher ), BeverageType.Water, 1 ) )
					{
						Effects.PlaySound( from.Location, from.Map, 0x240 );
						from.AddToBackpack( new Dough() );
						from.SendMessage("You made some dough and put it them in your backpack");
						m_Item.Use( from );
					}
				}

				if ( targeted is SweetDough )
				{
					if(!((Item)targeted).Movable) return;
					from.SendMessage("You made a cake mix");
					if( ((SweetDough)targeted).Parent == null )
						new CakeMix().MoveToWorld( ((SweetDough)targeted).Location, ((SweetDough)targeted).Map );
					else
						from.AddToBackpack( new CakeMix() );
					((SweetDough)targeted).Consume();
					m_Item.Use( from );
				}

				if ( targeted is TribalBerry )
				{
					if(!((Item)targeted).Movable) return;

					if ( from.Skills[SkillName.Cooking].Base >= 80.0 )
					{
						m_Item.Use( from );
						((TribalBerry)targeted).Delete();

						from.AddToBackpack( new TribalPaint() );

						from.SendLocalizedMessage( 1042002 );
					}
					else
					{
						from.SendLocalizedMessage( 1042003 );
					}
				}
			}
		}
	}
}