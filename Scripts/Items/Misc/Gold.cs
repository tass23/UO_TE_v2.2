using System;
using Server;
using Server.Misc;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Server.Accounting;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Engines.Craft;
using Server.Mobiles;

namespace Server.Items
{
	public class Gold : Item
	{
		#region RAFT Sweeper
		public override bool HandlesOnMovement { get { return Movable && Visible && Map != null && Map != Map.Internal && Parent == null; } }
		#endregion
		
		public override double DefaultWeight
		{
			get { return ( Core.ML ? ( 0.02 / 3 ) : 0.02 ); }
		}

		[Constructable]
		public Gold() : this( 1 )
		{
		}

		[Constructable]
		public Gold( int amountFrom, int amountTo ) : this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		[Constructable]
		public Gold( int amount ) : base( 0xEED )
		{
			Stackable = true;
			Amount = amount;
		}

		public Gold( Serial serial ) : base( serial )
		{
		}

		public override int GetDropSound()
		{
			if ( Amount <= 1 )
				return 0x2E4;
			else if ( Amount <= 5 )
				return 0x2E5;
			else
				return 0x2E6;
		}

		protected override void OnAmountChange( int oldValue )
		{
			int newValue = this.Amount;

			UpdateTotal( this, TotalType.Gold, newValue - oldValue );
		}

		public override int GetTotal( TotalType type )
		{
			int baseTotal = base.GetTotal( type );

			if ( type == TotalType.Gold )
				baseTotal += this.Amount;

			return baseTotal;
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

		#region Gold Smelting
        //Mod to allow players to double click gold and add to ledger
		public override void OnDoubleClick( Mobile m )
		{
            if ( !Movable ) return;
            if ( m.InRange( this.GetWorldLocation(), 2 ) )
            {
                m.SendLocalizedMessage( 501971 );
                m.Target = new InternalTarget( this );
            }
            else m.SendLocalizedMessage( 501976 );
		}

        private class InternalTarget : Target
        {
            private Gold m_gold;
            public InternalTarget( Gold gold ) :  base ( 2, false, TargetFlags.None )
            {
                m_gold = gold;
            }

            private bool IsForge( object obj )
            {
                if ( obj.GetType().IsDefined( typeof( ForgeAttribute ), false ) ) return true;
                int itemID = 0;
                if ( obj is Item ) itemID = ((Item)obj).ItemID;
                else if ( obj is StaticTarget ) itemID = ((StaticTarget)obj).ItemID & 0x3FFF;
                return ( itemID == 4017 || (itemID >= 6522 && itemID <= 6569) );
            }

            protected override void OnTarget( Mobile from, object targeted )
            {
                if ( m_gold.Deleted ) return;
                if ( !from.InRange( m_gold.GetWorldLocation(), 2 ) )
                {
                    from.SendLocalizedMessage( 501976 );
                    return;
                }
                if ( IsForge( targeted ) )
                {
                    if ( from.CheckTargetSkill( SkillName.Mining, targeted, 0, 10 ) )
                    {
                        int toConsume = m_gold.Amount;
                        if ( toConsume < 5000 ) from.SendMessage( "You can only smelt down 5,000 gp worth of coins at a time." );
                        else
                        {
                            toConsume = 5000;
                            m_gold.Consume( toConsume );
                            ArtyOre aore = new ArtyOre(5);
                            from.AddToBackpack( aore );
                            from.SendMessage( "You smelt the gold coins into artifact ore." );
                        }
                    }
                    else from.SendMessage( "You need just a little more mining skill to smelt gold coins." );
                }
            }
        }
		#endregion
	}
}