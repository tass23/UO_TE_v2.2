using System;
using Server;
using Server.Items;
using Server.Targets;
using Server.Network;


namespace Server.Items
{
	[FlipableAttribute( 0x13B8, 0x13B7 )]
	public class MadCowDisease : Kryss
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.InfectiousStrike; } }
		public override int ArtifactRarity{ get{ return 67; } }
		public override int AosStrengthReq{ get{ return 35; } }
		public override int AosMinDamage{ get{ return 15; } }
		public override int AosMaxDamage{ get{ return 16; } }
		public override int AosSpeed{ get{ return 50; } }

		public override int OldStrengthReq{ get{ return 25; } }
		public override int OldMinDamage{ get{ return 5; } }
		public override int OldMaxDamage{ get{ return 33; } }
		public override int OldSpeed{ get{ return 35; } }

		public override int DefHitSound{ get{ return 0x7b; } }
		public override int DefMissSound{ get{ return 0x7c; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public MadCowDisease() 
		{
			Weight = 1.0;
            		Hue = 0x4F6;
            		Name = "Mad Cow Disease";
            		WeaponAttributes.HitPoisonArea = 100;
			WeaponAttributes.ResistPoisonBonus = 20;
			Attributes.AttackChance = 15;
			Attributes.WeaponDamage = 50;
		}

        public virtual void GetDamageTypes(Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy)
        {
            fire = cold = nrgy = 0;
            phys = 25;
            pois = 75;
        }


		public MadCowDisease( Serial serial ) : base( serial )
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

        
	//Choose one or the other of the following OnHit Methods... Both Work To Suit Your Needs In Different Ways.
	//--------------------------------------------------------------------------------------------------------
	
	//public virtual void OnHit( Mobile attacker, Mobile defender, double damageBonus )

	  new public void OnHit( Mobile attacker, Mobile defender )

	//--------------------------------------------------------------------------------------------------------

        {
            base.OnHit(attacker, defender);

            if (!Core.AOS && Poison != null && PoisonCharges > 0)
            {
                --PoisonCharges;

                if (Utility.RandomDouble() >= 0.1) // 100% chance to poison
                    defender.ApplyPoison(attacker, Poison);
                    attacker.PlaySound(1086); //Oops!
            }
        }
        public override void OnDoubleClick (Mobile from)
        {
            from.SendMessage ("Forged from the Soul of an insane Bovine, Mad Cow Disease just ain't quite right");
            from.Target = new BladedItemTarget(this);
        }    
	}
}