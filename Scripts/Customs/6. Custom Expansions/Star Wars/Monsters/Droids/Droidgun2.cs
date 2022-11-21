using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x13FD, 0x13FC )]
	public class Droidgun2 : BaseRanged
	{
		public override int EffectID{ get{ return 0x3E75; } }
		public override Type AmmoType{ get{ return typeof( Bolt ); } }
		public override Item Ammo{ get{ return new Bolt(); } }
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.MovingShot; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Dismount; } }
		public override int AosStrengthReq{ get{ return 10; } }
		public override int AosMinDamage{ get{ return Core.ML ? 15 : 14; } }
		public override int AosMaxDamage{ get{ return Core.ML ? 19 : 15; } }
		public override int AosSpeed{ get{ return 22; } }
		public override float MlSpeed{ get{ return 5.00f; } }
		public override int OldStrengthReq{ get{ return 40; } }
		public override int OldMinDamage{ get{ return 16; } }
		public override int OldMaxDamage{ get{ return 20; } }
		public override int OldSpeed{ get{ return 10; } }
		public override int DefMaxRange{ get{ return 8; } }
		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 100; } }
		
		[Constructable]
		public Droidgun2() : base( 0x13FD )
		{
			LootType = LootType.Blessed;
			Weight = 9.0;
			Layer = Layer.TwoHanded;
		}

		public override bool OnFired( Mobile attacker, Mobile defender )
		{
			BaseQuiver quiver = attacker.FindItemOnLayer( Layer.Cloak ) as BaseQuiver;
			Container pack = attacker.Backpack;

			if ( attacker.Player )
			{
				if ( quiver == null || quiver.LowerAmmoCost == 0 || quiver.LowerAmmoCost > Utility.Random( 100 ) )
				{
					if ( quiver != null && quiver.ConsumeTotal( AmmoType, 1 ) )
						quiver.InvalidateWeight();
					else if ( pack == null || !pack.ConsumeTotal( AmmoType, 1 ) )
						return false;
				}
			}

			attacker.MovingEffect( defender, EffectID, 18, 1, false, false, 1392, 0  );
			attacker.PlaySound( 0x644 );
			return true;
		}
			
		public Droidgun2( Serial serial ) : base( serial )
		{
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
	}
}