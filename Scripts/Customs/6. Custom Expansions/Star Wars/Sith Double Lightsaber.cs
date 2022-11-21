using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x26BF, 0x26C9 )]
	public class SithDoubleLightsaber : BaseSword
	{
		private LightSource light;
        private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.DoubleStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.InfectiousStrike; } }

		public override int AosIntelligenceReq{ get{ return 100; } }
		public override int AosMinDamage{ get{ return 30; } }
		public override int AosMaxDamage{ get{ return 55; } }
		public override int AosSpeed{ get{ return 50; } }
		public override float MlSpeed{ get{ return 2.75f; } }

		public override int OldStrengthReq{ get{ return 50; } }
		public override int OldMinDamage{ get{ return 12; } }
		public override int OldMaxDamage{ get{ return 13; } }
		public override int OldSpeed{ get{ return 49; } }

		public override int InitMinHits{ get{ return 0; } }
		public override int InitMaxHits{ get{ return 0; } }

		[Constructable]
		public SithDoubleLightsaber() : base( 0x26BF )
		{
			Name = "A Sith Double Lightsaber";
			Weight = 12.0;
			Hue = 1465;

			AccuracyLevel = WeaponAccuracyLevel.Supremely;
			DurabilityLevel = WeaponDurabilityLevel.Indestructible;
			WeaponAttributes.HitLeechHits = 25;
			WeaponAttributes.HitLeechMana = 25;
			WeaponAttributes.HitHarm = 40;
			WeaponAttributes.HitLightning = 15;
			Attributes.WeaponDamage = 35;
			Attributes.WeaponSpeed = 30;
			Attributes.AttackChance = 50;
			Attributes.DefendChance = 35;
			Attributes.LowerManaCost = 50;
			Attributes.CastRecovery = 5;
			Attributes.CastSpeed = 5;
			Attributes.SpellChanneling = 1;
			Attributes.Luck = 250;
			Attributes.BonusStr = 20;
			Attributes.BonusDex = 20;
			Attributes.BonusInt = 20;
			LootType = LootType.Blessed;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			fire = cold = pois = chaos = direct =0;
			phys = 50;
			nrgy = 50;
		}
		
        public override void OnDoubleClick(Mobile from)
        {
            // set owner if not already set -- this is only done the first time.
            if (m_Owner == null)
            {
                m_Owner = from;
                this.Name = "The Sith Master " + m_Owner.Name.ToString() + "'s Lightsaber";
                from.SendMessage("This Lightsaber is now yours alone.");
                from.FixedEffect(0x375A, 10, 15);
                from.PlaySound(0x1E7);
            }
            else
            {
                if (m_Owner != from)
                {
                    from.SendMessage("This is not yours to use.");
                    return;
                }
            }
        }

        public override bool OnEquip(Mobile from)
        {
            if (m_Owner == null)
            {
                from.SendMessage("you bind the Lightsaber to you...");
                m_Owner = from;
                this.Name = "The Sith Master " + m_Owner.Name.ToString() + "'s Lightsaber";
                from.FixedEffect(0x375A, 10, 15);
                from.PlaySound(0x1E7);
                InvalidateProperties();
            }
            else if (m_Owner != from)
            {
                from.SendMessage(String.Format("This is not your weapon"));
                return false;
            }

            return base.OnEquip(from);
        }

		public override bool Decays
		{
			get{ return false; }
		}

		public SithDoubleLightsaber( Serial serial ) : base( serial )
		{
		}
		
		public override void OnAdded( object parent )
		{
			light = new LightSource();
			light.Layer = Layer.Waist;
			light.Light = LightType.Circle300;
			if( parent is Mobile )

			{
				Mobile from = (Mobile)parent;
				from.AddItem( light );
			}

			base.OnAdded( parent );
		}

		public override void OnRemoved( object parent )
		{
			if( light != null && parent is Mobile )
			{
				light.Delete();
			}

			base.OnRemoved( parent );
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