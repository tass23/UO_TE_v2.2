using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Multis;
using Server.ContextMenus;
/*todo Make items non-repairable add Ankh Pendant Map of the known World 10th Anniversary Sculpture and Virtue Armor Set*/
namespace Server.Items
{
    public class KatrinasCrook : ShepherdsCrook
    {
        public override int LabelNumber { get { return 1079789; } } // Katrina's Crook

        public override int ArtifactRarity { get { return 11; } }
        public override int InitMinHits { get { return 120; } }
        public override int InitMaxHits { get { return 120; } }

        [Constructable]
        public KatrinasCrook()
            : base()
        {
            WeaponAttributes.HitLeechStam = 40;
            WeaponAttributes.HitLeechMana = 55;
            WeaponAttributes.HitLeechHits = 55;

            Attributes.WeaponDamage = 60;
            Attributes.DefendChance = 15;
        }

        public KatrinasCrook(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)2);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class JaanasStaff : GnarledStaff
    {
        public override int LabelNumber { get { return 1079790; } } // Jaana's Staff

        public override int ArtifactRarity { get { return 11; } }
        public override int InitMinHits { get { return 120; } }
        public override int InitMaxHits { get { return 120; } }

        [Constructable]
        public JaanasStaff()
            : base()
        {
            Hue = 0x58C;

            WeaponAttributes.MageWeapon = 20;

            Attributes.SpellChanneling = 1;
            Attributes.Luck = 220;
            Attributes.DefendChance = 15;
        }

        public JaanasStaff(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DragonsEnd : Longsword
    {
        public override int LabelNumber { get { return 1079791; } } // Dragon's End

        public override int ArtifactRarity { get { return 11; } }

        public override int InitMinHits { get { return 120; } }
        public override int InitMaxHits { get { return 120; } }

        [Constructable]
        public DragonsEnd()
            : base()
        {
            Hue = 0x554;

            Slayer = SlayerName.DragonSlaying;

            Attributes.AttackChance = 10;
            Attributes.WeaponDamage = 60;

            WeaponAttributes.ResistFireBonus = 20;
        }

        public override void GetDamageTypes(Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct)
        {
            phys = fire = nrgy = pois = direct = chaos = 0;
            cold = 100;
        }

        public DragonsEnd(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class SentinelsGuard : OrderShield
    {
        public override int LabelNumber { get { return 1079792; } } // Sentinel's Guard

        public override int ArtifactRarity { get { return 11; } }

        public override int BasePhysicalResistance { get { return 16; } }
        public override int BaseFireResistance { get { return 10; } }
        public override int BaseColdResistance { get { return 10; } }
        public override int BasePoisonResistance { get { return 5; } }
        public override int BaseEnergyResistance { get { return 10; } }

        public override int InitMinHits { get { return 150; } }
        public override int InitMaxHits { get { return 150; } }

        [Constructable]
        public SentinelsGuard()
            : base()
        {
            Hue = 0x21;
        }

        public SentinelsGuard(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class LordBlackthornsExemplar : ChaosShield
    {
        public override int LabelNumber { get { return 1079793; } } // Lord Blackthorn's Exemplar

        public override int ArtifactRarity { get { return 11; } }

        public override int BasePhysicalResistance { get { return 6; } }
        public override int BaseFireResistance { get { return 10; } }
        public override int BaseColdResistance { get { return 10; } }
        public override int BasePoisonResistance { get { return 15; } }
        public override int BaseEnergyResistance { get { return 10; } }

        public override int InitMinHits { get { return 150; } }
        public override int InitMaxHits { get { return 150; } }

        [Constructable]
        public LordBlackthornsExemplar()
            : base()
        {
            Hue = 0x501;
        }

        public LordBlackthornsExemplar(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [FlipableAttribute(0x3BB6, 0x3BB7)]
    public class MapofKnownWorld : Item, ISecurable
    {
        private SecureLevel m_Level;

        public override int LabelNumber { get { return 1075571; } } // A map of the known world

        [CommandProperty(AccessLevel.GameMaster)]
        public SecureLevel Level
        {
            get { return m_Level; }
            set { m_Level = value; }
        }

        [Constructable]
        public MapofKnownWorld()
            : base(0x3BB6)
        {
            Weight = 1.0;
            LootType = LootType.Blessed;
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);

            SetSecureLevelEntry.AddTo(from, this, list);
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.InRange(GetWorldLocation(), 2))
            {
                from.CloseGump(typeof(InternalGump));
                from.SendGump(new InternalGump());
            }
            else
            {
                from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1019045); // I can't reach that.
            }
        }

        private class InternalGump : Gump
        {
            public InternalGump()
                : base(50, 50)
            {
                AddImage(0, 0, 0x12B);
            }
        }

        public MapofKnownWorld(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt((int)0); // version

            writer.WriteEncodedInt((int)m_Level);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();

            m_Level = (SecureLevel)reader.ReadEncodedInt();
        }
    }

    [FlipableAttribute(0x3BB3, 0x3BB4)]
    public class AnniversarySculpture : Item
    {
        public override int LabelNumber { get { return 1079532; } } // 10th Anniversary Sculpture

        [Constructable]
        public AnniversarySculpture()
            : base(0x3BB3)
        {
        }

        public AnniversarySculpture(Serial serial)
            : base(serial)
        {
        }

        // To do. Make the luck addition for this sculpture.

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)2);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}