using System;
using Server.Misc;

namespace Server.Items
{
	[FlipableAttribute( 0x1515, 0x1530 )] 
	public class JediCloak : Cloak
	{
		public override int Hue { get { return 1821; } }
        private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}

		private SkillMod m_SkillMod0;
		private SkillMod m_SkillMod1;
		private StatMod m_StatMod0;

		[Constructable]
		public JediCloak() : base( 0x309 )
		{
			Name = "a Jedi Cloak";
			LootType = LootType.Blessed;
			DefineMods();
		}

		private void DefineMods()
		{
			m_SkillMod0 = new DefaultSkillMod( SkillName.Focus, true, 10 );
			m_SkillMod1 = new DefaultSkillMod( SkillName.Meditation, true, 10 );
			m_StatMod0 = new StatMod( StatType.Int, "JediCloak", 15, TimeSpan.Zero );
		}

		private void SetMods( Mobile wearer )
		{
			wearer.AddSkillMod( m_SkillMod0 );
			wearer.AddSkillMod( m_SkillMod1 );
			wearer.AddStatMod( m_StatMod0 );
		}

		public override bool OnEquip( Mobile from )
		{
			SetMods( from );

			if ( from.Karma >= 5000 )
			{
				if ( from != m_Owner )
				{
					if ( m_Owner == null )
					{
						if ( from.Karma >= 5000 )
						{
							from.SendMessage( "The cloak binds to you..." );
							m_Owner = from;
							Name = "The Jedi " + m_Owner.Name.ToString() + "'s Cloak";
							from.FixedEffect( 0x3779, 10, 15 );
							from.PlaySound( 1623 );
							return base.OnEquip( from );
						}
						else
						{
							from.SendMessage( "Only a Jedi, can wear this cloak." );
						}
					}
					else
					{
						from.SendMessage( "This is not your cloak." );
					}
					return false;
				}
				return base.OnEquip( from );
			}
			else
			{
				from.SendMessage( "Only a Jedi, can wear this cloak." );
				return false;
			}
			return base.OnEquip( from );
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendLocalizedMessage( 1042083 ); // You can not dye that.
			return false;
		}

		public override void OnAdded( object parent )
		{
			base.OnAdded( parent );

			if ( parent is Mobile )
				Misc.Titles.AwardKarma( (Mobile)parent, 100, true );
		}

		public override void OnRemoved( object parent )
		{
			if ( parent is Mobile )
			{
				Mobile m = (Mobile)parent;
				m.RemoveStatMod( "JediCloak" );

				if ( m.Hits > m.HitsMax )
					m.Hits = m.HitsMax;

				if ( m_SkillMod0 != null )
					m_SkillMod0.Remove();

				if ( m_SkillMod1 != null )
					m_SkillMod1.Remove();

				if ( parent is Mobile )
					Misc.Titles.AwardKarma( (Mobile)parent, -100, true );
			}
		}

		public override void OnSingleClick( Mobile from )
		{
			this.LabelTo( from, Name );
		}

		public JediCloak( Serial serial ) : base( serial )
		{
			DefineMods();

			if ( Parent != null && this.Parent is Mobile )
				SetMods( (Mobile)Parent );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write((Mobile)m_Owner);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Owner = reader.ReadMobile();
		}
	}
}