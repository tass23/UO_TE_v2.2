using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class PaintBallGunBase : BaseRanged
	{
		public override int EffectID{ get{ return 0x1BFE; } }
		public override Type AmmoType{ get{ return typeof( PaintBallPellets ); } }
		public override Item Ammo{ get{ return new PaintBallPellets(); } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.Block; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ForceOfNature; } }

		public override int AosStrengthReq{ get{ return 0; } }
		public override int AosMinDamage{ get{ return 1; } }
		public override int AosMaxDamage{ get{ return 1; } }
		public override int AosSpeed{ get{ return 25; } }
		public override float MlSpeed { get { return 4.5f; } }//For ML Change Speed Here


		public override int OldStrengthReq{ get{ return 0; } }
		public override int OldMinDamage{ get{ return 1; } }
		public override int OldMaxDamage{ get{ return 1; } }
		public override int OldSpeed{ get{ return 25; } }

		public override int DefMaxRange{ get{ return 10; } }

		public override int InitMinHits{ get{ return 0; } }
		public override int InitMaxHits{ get{ return 0; } }

		[Constructable]
		public PaintBallGunBase() : base( 3920 )
		{
	 	 	WeaponAttributes.SelfRepair = 20;
			Attributes.NightSight = 1;
			Weight = 1.0;
			Hue = Utility.RandomMetalHue();
		}

		public override void OnHit( Mobile attacker, Mobile defender, double damageBonus )
		{
			if (defender.Player && defender.Region.Name == "Paintball Arena" )
			{
				Item paintrobe = defender.FindItemOnLayer(Layer.OuterTorso );
				defender.SendMessage( "You feel wet paint on you" );
				if ( paintrobe == null)
				{
					defender.SendMessage( "You have NO Robe ON!!!!!!" );
					defender.SendMessage( "You are Eliminated" );
					defender.X = 1141;
					defender.Y = 440;
					defender.Z = -45;
					defender.Map = Map.Malas;
				}
				else if ( paintrobe.Hue == 6) paintrobe.Hue = 11;
				else if ( paintrobe.Hue == 11) paintrobe.Hue = 21;
				else if ( paintrobe.Hue == 21) paintrobe.Hue = 31;
				else if ( paintrobe.Hue == 31) paintrobe.Hue = 38;
				else if ( paintrobe.Hue == 38)
				{
					paintrobe.Hue = 6;
					defender.X = 1141;
					defender.Y = 440;
					defender.Z = -45;
					defender.Map = Map.Malas;
					defender.SendMessage( "You were Eliminated" );
					this.Name = this.Name + "/";
				}
				else
				{
					paintrobe.Hue = 6;
					defender.X = 1141;
					defender.Y = 440;
					defender.Z = -45;
					defender.Map = Map.Malas;
					defender.SendMessage( "You were Eliminated for an improper robe" );
				}

			}
			else if ( defender.Region.Name == "Paintball Arena" )
			{
				attacker.SendMessage( "You stunned the bot for a moment" );
				defender.Freeze( TimeSpan.FromSeconds( 10.0 ) );
			}

			damageBonus = -10;

			base.OnHit( attacker, defender, damageBonus );
		}

		public PaintBallGunBase( Serial serial ) : base( serial )
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