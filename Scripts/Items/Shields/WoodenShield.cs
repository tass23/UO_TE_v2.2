using System;
using Server;

namespace Server.Items
{
	public class WoodenShield : BaseShield
	{
        #region Mondain's Legacy
        public override int PhysicalResistance { get { return BasePhysicalResistance + GetProtOffset() + GetResourceAttrs().ShieldPhysicalResist + PhysicalBonus + (SetEquipped ? SetPhysicalBonus : 0); } }
        public override int FireResistance { get { return BaseFireResistance + GetProtOffset() + GetResourceAttrs().ShieldFireResist + FireBonus + (SetEquipped ? SetFireBonus : 0); } }
        public override int ColdResistance { get { return BaseColdResistance + GetProtOffset() + GetResourceAttrs().ShieldColdResist + ColdBonus + (SetEquipped ? SetColdBonus : 0); } }
        public override int PoisonResistance { get { return BasePoisonResistance + GetProtOffset() + GetResourceAttrs().ShieldPoisonResist + PoisonBonus + (SetEquipped ? SetPoisonBonus : 0); } }
        public override int EnergyResistance { get { return BaseEnergyResistance + GetProtOffset() + GetResourceAttrs().ShieldEnergyResist + EnergyBonus + (SetEquipped ? SetEnergyBonus : 0); } }
        #endregion

		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 0; } }
		public override int BaseColdResistance{ get{ return 0; } }
		public override int BasePoisonResistance{ get{ return 0; } }
		public override int BaseEnergyResistance{ get{ return 1; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 25; } }

		public override int AosStrReq{ get{ return 20; } }

		public override int ArmorBase{ get{ return 8; } }

		[Constructable]
		public WoodenShield() : base( 0x1B7A )
		{
			Weight = 5.0;
			#region UO-The Expanse
			//Colored Item Name Mod Start
			Resource = CraftResource.RegularWood;
			//Colored Item Name Mod Start
			#endregion
		}

		public WoodenShield( Serial serial ) : base(serial)
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );//version
		}
	}
}
