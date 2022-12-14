/* Created by Hammerhand*/

using System;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server.Engines.Harvest;

namespace Server.Items
{
    public class FirePick : BaseAxe, IUsesRemaining
	{
        public override HarvestSystem HarvestSystem { get { return FireRockMining.System; } }
        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.DoubleStrike; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.Disarm; } }

        public override int AosStrengthReq { get { return 50; } }
        public override int AosMinDamage { get { return 13; } }
        public override int AosMaxDamage { get { return 15; } }
        public override int AosSpeed { get { return 35; } }

        public override int OldStrengthReq { get { return 25; } }
        public override int OldMinDamage { get { return 1; } }
        public override int OldMaxDamage { get { return 15; } }
        public override int OldSpeed { get { return 35; } }

        public override WeaponAnimation DefAnimation { get { return WeaponAnimation.Slash1H; } }

        		[Constructable]
		public FirePick() : this( 50 )
		{
		}
		[Constructable]
		public FirePick(int uses) : base( 0xE86 )
		{
            Name = "a Fire Pick";
			Weight = 11.0;
            Hue = 1358;
            UsesRemaining = uses;
			ShowUsesRemaining = true;
		}

        public FirePick(Serial serial)
            : base(serial)
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
			ShowUsesRemaining = true;
		}
	}
}