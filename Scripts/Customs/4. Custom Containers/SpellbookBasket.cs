using System;
using System.Data;
using System.Xml;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using Server.Targeting;
using Server.Engines.PartySystem;
using Server.Engines.XmlSpawner2;
using Server.ACC.CSS;
using Server.ACC.CSS.Systems.Spellweaving;
using Server.ACC.CSS.Systems.Mysticism;
using Server.ACC.CSS.Systems.Chivalry;
using Server.ACC.CSS.Systems.Mage;
using Server.ACC.CSS.Systems.Necromancy;
using Server.ACC.CSS.Systems.LightForce;
using Server.ACC.CSS.Systems.DarkForce;

namespace Server.Items
{
	public class SpellbookBasket : BaseContainer
	{
		private PlayerMobile m_Owner;

        [CommandProperty(AccessLevel.Administrator)]
        public PlayerMobile M_Owner { get { return m_Owner; } set { m_Owner = value; InvalidateProperties(); } }

		private double m_Redux;
        [CommandProperty(AccessLevel.GameMaster)]

        public int ReduxPercent
        {
            get { return (int)(m_Redux * 100); }
            set
            {
                value = 100;
                m_Redux = ((double)value) / 100;
                    UpdateTotals();
            }
        }

		public override int DefaultGumpID{ get{ return 0x108; } }
		public override int DefaultDropSound{ get{ return 0x4F; } }

		public override Rectangle2D Bounds
		{
			get{ return new Rectangle2D( 19, 47, 163, 76 ); }
		}

		[Constructable]
		public SpellbookBasket() : base( 0x24D9 )
		{
			ReduxPercent = 100;
			Movable = true;
			Hue = 1259;
			Name = "Spellbook Basket";
			LootType = LootType.Blessed;
		}

		public SpellbookBasket( Serial serial ) : base( serial )
		{
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( dropped is Spellbook )
			{  
				Spellbook spellb = (Spellbook)dropped;			
				DropItem ( spellb );
				return true;
			}
			else if ( dropped is NecroSpellbook )
			{
				NecroSpellbook spellb = (NecroSpellbook)dropped;
				DropItem ( spellb );
				return true;
			}
			else if ( dropped is MageSpellbook )
			{
				MageSpellbook spellb = (MageSpellbook)dropped;
				DropItem ( spellb );
				return true;
			}
			else if ( dropped is ChivalrySpellbook )
			{
				ChivalrySpellbook spellb = (ChivalrySpellbook)dropped;
				DropItem ( spellb );
				return true;
			}
			else if ( dropped is SpellweavingSpellbook )
			{
				SpellweavingSpellbook spellb = (SpellweavingSpellbook)dropped;
				DropItem ( spellb );
				return true;
			}
			else if ( dropped is MysticismSpellbook )
			{
				MysticismSpellbook spellb = (MysticismSpellbook)dropped;
				DropItem ( spellb );
				return true;
			}
			else if ( dropped is JediHolocron )
			{
				JediHolocron spellb = (JediHolocron)dropped;
				DropItem ( spellb );
				return true;
			}
			else if ( dropped is SithHolocron )
			{
				SithHolocron spellb = (SithHolocron)dropped;
				DropItem ( spellb );
				return true;
			}
			else if ( dropped is CSpellbook )
			{
				CSpellbook spellb = (CSpellbook)dropped;
				DropItem ( spellb );
				return true;
			}
			/*	Remove these comment marks to allow Treasure Map Books and Recall Runes back into the Spellbook Basket. Uncomment section further down too.
			else if ( dropped is TMapBook )
			{  
				TMapBook spellb = (TMapBook)dropped;			
				DropItem ( spellb );
				return true;
			}
			else if ( dropped is RecallRune )
			{  
				RecallRune spellb = (RecallRune)dropped;			
				DropItem ( spellb );
				return true;
			}
			*/
			else
				return false;
		}

		public override bool OnDragDropInto( Mobile from, Item dropped, Point3D p )
		{
			if ( dropped is Spellbook )
			{
				Spellbook spellb = (Spellbook)dropped;
				spellb.Location = new Point3D( p.X, p.Y, 0 );
				AddItem ( spellb );
				return true;
			}
			else if ( dropped is NecroSpellbook )
			{
				NecroSpellbook spellb = (NecroSpellbook)dropped;
				spellb.Location = new Point3D( p.X, p.Y, 0 );
				AddItem ( spellb );
				return true;
			}
			else if ( dropped is MageSpellbook )
			{
				MageSpellbook spellb = (MageSpellbook)dropped;
				spellb.Location = new Point3D( p.X, p.Y, 0 );
				AddItem ( spellb );
				return true;
			}
			else if ( dropped is ChivalrySpellbook )
			{
				ChivalrySpellbook spellb = (ChivalrySpellbook)dropped;
				spellb.Location = new Point3D( p.X, p.Y, 0 );
				AddItem ( spellb );
				return true;
			}
			else if ( dropped is SpellweavingSpellbook )
			{
				SpellweavingSpellbook spellb = (SpellweavingSpellbook)dropped;
				spellb.Location = new Point3D( p.X, p.Y, 0 );
				AddItem ( spellb );
				return true;
			}
			else if ( dropped is MysticismSpellbook )
			{
				MysticismSpellbook spellb = (MysticismSpellbook)dropped;
				spellb.Location = new Point3D( p.X, p.Y, 0 );
				AddItem ( spellb );
				return true;
			}
			else if ( dropped is JediHolocron )
			{
				JediHolocron spellb = (JediHolocron)dropped;
				spellb.Location = new Point3D( p.X, p.Y, 0 );
				AddItem ( spellb );
				return true;
			}
			else if ( dropped is SithHolocron )
			{
				SithHolocron spellb = (SithHolocron)dropped;
				spellb.Location = new Point3D( p.X, p.Y, 0 );
				AddItem ( spellb );
				return true;
			}
			else if ( dropped is CSpellbook )
			{
				CSpellbook spellb = (CSpellbook)dropped;
				spellb.Location = new Point3D( p.X, p.Y, 0 );
				AddItem ( spellb );
				return true;
			}
			/* Remove these comment marks to allow Treasure Map Books and Recall Runes back into the Spellbook Basket.
			else if ( dropped is TMapBook )
			{
				TMapBook spellb = (TMapBook)dropped;
				spellb.Location = new Point3D( p.X, p.Y, 0 );
				AddItem ( spellb );
				return true;
			}
			else if ( dropped is RecallRune )
			{
				RecallRune spellb = (RecallRune)dropped;
				spellb.Location = new Point3D( p.X, p.Y, 0 );
				AddItem ( spellb );
				return true;
			}
			*/
			else

			return false;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_Owner != null )
				list.Add( 1072304, m_Owner.Name ); 
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			
            writer.Write((double)m_Redux);
            writer.Write(M_Owner);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_Redux = reader.ReadDouble();
            this.M_Owner = reader.ReadMobile() as PlayerMobile;
		}

		public override void OnItemLifted(Mobile from, Item item)
        {
            base.OnItemLifted(from, item);

            if (from is PlayerMobile)
            {
                if (from is PlayerMobile && M_Owner == null)
                {
                    M_Owner = from as PlayerMobile;
                    LootType = LootType.Blessed;
                }
                 else if (from.AccessLevel >= AccessLevel.GameMaster)
                {
                    base.OnItemLifted(from, item);
                }
                else if (M_Owner != from)
                {
                    from.SendMessage(1173, "This is not your Spellbook Basket!");
                    M_Owner.AddToBackpack(this);
                }
            }
        }

		public override void OnAdded(object target)
        {
            base.OnAdded(target);

            if ((target != null) && target is Container)
            {
                object parentOfTarget = ((Container)target).Parent;

				if ((parentOfTarget != null) && (parentOfTarget is PlayerMobile) && M_Owner != null)
				{
					if (RootParentEntity == null)
					{

					}                            
					else
						if ((parentOfTarget != M_Owner) || (target is Mobile) || (target is BankBox))//
						{
							M_Owner.AddToBackpack(this);
							this.ReduxPercent = 100;
						}
				}
				else
				{
					if ((RootParentEntity != null) && (M_Owner != null))
					{
						M_Owner.AddToBackpack(this);
						this.ReduxPercent = 100;
					}
				}
            }
        }

		public override void UpdateTotal(Item sender, TotalType type, int delta)
        {
            base.UpdateTotal(sender, type, delta);
            if (type == TotalType.Weight)
            {
                if (Parent is Item)
                    (Parent as Item).UpdateTotal(sender, type, (int)(delta * m_Redux) * -1);
                else if (Parent is Mobile)
                    (Parent as Mobile).UpdateTotal(sender, type, (int)(delta * m_Redux) * -1);
            }
        }

        public override int GetTotal(TotalType type)
        {
            if (type == TotalType.Weight)
                return (int)(base.GetTotal(type) * (1.0 - m_Redux));
            return base.GetTotal(type);
        }
	}
}