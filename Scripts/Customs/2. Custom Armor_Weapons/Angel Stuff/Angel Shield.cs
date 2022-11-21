using System;
using Server;
using Server.Guilds;

namespace Server.Items
{
	public class AngelShield : BaseShield
	{
        public override int ArtifactRarity{ get{ return 100; } }
		public override int BasePhysicalResistance{ get{ return 15; } }
		public override int BaseFireResistance{ get{ return 30; } }
		public override int BaseColdResistance{ get{ return 10; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 15; } }
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }
		public override int AosStrReq{ get{ return 50; } }
		public override int ArmorBase{ get{ return 60; } }

		[Constructable]
		public AngelShield() : base( 0x1BC4 )
		{
			if ( !Core.AOS )
			LootType = LootType.Blessed;

			Weight = 10.0;
			Name = "Angel's Shield";
			Hue = 1153;
            Attributes.SpellChanneling = 1;
            ArmorAttributes.SelfRepair = 4;
            Attributes.DefendChance = 20;
            Attributes.Luck = 150;
		}

		public AngelShield( Serial serial ) : base(serial)
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( Weight == 6.0 )
				Weight = 0.0;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );//version
		}

		public override bool OnEquip( Mobile from )
		{
			return Validate( from ) && base.OnEquip( from );
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( Validate( Parent as Mobile ) )
				base.OnSingleClick( from );
		}

		public virtual bool Validate( Mobile m )
		{
			if ( Core.AOS || m == null || !m.Player || m.AccessLevel != AccessLevel.Player )
				return true;

			Guild g = m.Guild as Guild;

			if ( g == null || g.Type != GuildType.Order )
			{
				m.FixedEffect( 0x3728, 10, 13 );
				Delete();

				return false;
			}

			return true;
		}
	}
}