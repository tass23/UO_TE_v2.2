using System;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class UtilityItem
	{
		static public int RandomChoice( int itemID1, int itemID2 )
		{
			int iRet = 0;
			switch ( Utility.Random( 2 ) )
			{
				default:
				case 0: iRet = itemID1; break;
				case 1: iRet = itemID2; break;
			}
			return iRet;
		}
	}

	public class WheatSheaf : Item
	{
		[Constructable]
		public WheatSheaf() : this( 1 ){}

		[Constructable]
		public WheatSheaf( int amount ) : base( 7869 )
		{
			Stackable = true;
			Amount = amount;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;

			from.BeginTarget( 4, false, TargetFlags.None, new TargetCallback( OnTarget ) );
		}

		public virtual void OnTarget( Mobile from, object obj )
		{
			if ( obj is AddonComponent )
				obj = (obj as AddonComponent).Addon;

			IFlourMill mill = obj as IFlourMill;

			if ( mill != null )
			{
				int needs = mill.MaxFlour - mill.CurFlour;

				if ( needs > this.Amount )
					needs = this.Amount;

				mill.CurFlour += needs;
				Consume( needs );
			}
		}

		public WheatSheaf( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
}