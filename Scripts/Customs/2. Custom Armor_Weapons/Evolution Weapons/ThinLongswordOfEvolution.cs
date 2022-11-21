/*
 created by:
     /\       
____/_ \____  ### ### ### ### #  ### ### # ##  ##  ###
\  ___\ \  /  #   #   # # # # #  # # # # # # # # # #
 \/ /  \/ /   ### ##  ### # # #  ### # # # # # ##  ##
 / /\__/_/\     # #   # # # # #  # # # # # # # # # #
/__\ \_____\  ### ### # # # ###  # # # ### ##  # # ###
    \  /             http://www.wftpradio.net/
     \/       
*/
using System;
using Server;

namespace Server.Items
{
	public class ThinLongswordOfEvolution : ThinLongsword
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.CrushingBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }
		private int mEvolutionPoints = 0;

		[CommandProperty( AccessLevel.GameMaster )]
		public int EvolutionPoints { get { return mEvolutionPoints; } set { mEvolutionPoints = value; } }

		public override int ArtifactRarity{ get{ return 500; } }
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public ThinLongswordOfEvolution()
		{
			Name = "Thin Longsword Of Evolution";
			Hue = 0x4F2;
			WeaponAttributes.UseBestSkill = 1;
            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;
            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
        }

        public ThinLongswordOfEvolution(Serial serial) : base(serial)
        {
        }

        public override void OnHit(Mobile attacker, Mobile defender, double Damagebonus)
        {
            if (Utility.Random(2) == 1)
            {
                ApplyGain();
            }

            base.OnHit(attacker, defender, Damagebonus);
        }

        public void ApplyGain()
        {
            int expr;
            if (mEvolutionPoints < 10001)
            {
                mEvolutionPoints++;
                this.Name = "Thin Longsword Of Evolution (" + mEvolutionPoints.ToString() + ")";

                if ((mEvolutionPoints / 100) > 0)
                {
                    expr = mEvolutionPoints / 100;
                    this.WeaponAttributes.HitHarm = expr;
                    this.WeaponAttributes.HitMagicArrow = expr;
                }

                if ((mEvolutionPoints / 200) > 0)
                {
                    expr = mEvolutionPoints / 100;
                    this.WeaponAttributes.HitLightning = expr;
                    this.WeaponAttributes.HitFireball = expr;
                    this.Attributes.WeaponDamage = expr;
                }

                if ((25 + (mEvolutionPoints / 200)) > 0) this.Attributes.WeaponSpeed = (25 + (mEvolutionPoints / 200));

                if ((mEvolutionPoints / 2000) > 0)
                {
                    expr = mEvolutionPoints / 2000;
                    this.Attributes.CastRecovery = expr;
                    this.Attributes.CastSpeed = expr;
                }

                InvalidateProperties();
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
            writer.Write(mEvolutionPoints);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            mEvolutionPoints = reader.ReadInt();
        }
    }
}