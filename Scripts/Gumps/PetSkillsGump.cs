using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;

namespace Server.Gumps
{
	public class PetSkillsGump : Gump
	{
		private Mobile m_Pet;

		public PetSkillsGump( Mobile pet ) : base( 0, 0 )
		{
			m_Pet = pet;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddBackground(30, 25, 289, 336, 5120);
			AddLabel(38, 28, 1160, @"Viewing Skills Of:");
			AddImageTiled(36, 48, 282, 9, 5121);
			AddImageTiled(38, 55, 192, 22, 5154);
			AddImageTiled(233, 55, 50, 22, 5154);
			AddImageTiled(286, 55, 26, 22, 5154);
			AddImageTiled(38, 80, 192, 245, 5154);
			AddImageTiled(233, 80, 50, 245, 5154);
			AddImageTiled(286, 80, 26, 245, 5154);
			AddAlphaRegion(38, 55, 274, 269);
			AddLabel(43, 56, 1160, @"Skill Name");
			AddLabel(235, 56, 1160, @"Value");
			AddImage(293, 59, 2092);
			AddLabel(43, 80, 1149, @"Wrestling");
			AddLabel(43, 97, 1149, @"Tactics");
			AddLabel(43, 114, 1149, @"Anatomy");
			AddLabel(43, 131, 1149, @"Magic Resistance");
			AddLabel(43, 148, 1149, @"Poisoning");
			AddLabel(43, 165, 1149, @"Magery");
			AddLabel(43, 182, 1149, @"Evaluating Inteligence");
			AddLabel(43, 199, 1149, @"Meditation");
			AddLabel(43, 216, 1149, @"Swordsmanship");
			AddLabel(43, 233, 1149, @"Mace Fighting");
			AddLabel(43, 250, 1149, @"Fencing");
			AddLabel(43, 267, 1149, @"Archery");
			AddLabel(43, 284, 1149, @"Parry");

			if ( m_Pet != null )
			{
				AddLabel(237, 80, 1149, m_Pet.Skills[SkillName.Wrestling].Base.ToString() );
				AddLabel(237, 97, 1149, m_Pet.Skills[SkillName.Tactics].Base.ToString() );
				AddLabel(237, 114, 1149, m_Pet.Skills[SkillName.Anatomy].Base.ToString() );
				AddLabel(237, 131, 1149, m_Pet.Skills[SkillName.MagicResist].Base.ToString() );
				AddLabel(237, 148, 1149, m_Pet.Skills[SkillName.Poisoning].Base.ToString() );
				AddLabel(237, 165, 1149, m_Pet.Skills[SkillName.Magery].Base.ToString() );
				AddLabel(237, 182, 1149, m_Pet.Skills[SkillName.EvalInt].Base.ToString() );
				AddLabel(237, 199, 1149, m_Pet.Skills[SkillName.Meditation].Base.ToString() );
				AddLabel(237, 216, 1149, m_Pet.Skills[SkillName.Swords].Base.ToString() );
				AddLabel(237, 233, 1149, m_Pet.Skills[SkillName.Macing].Base.ToString() );
				AddLabel(237, 250, 1149, m_Pet.Skills[SkillName.Fencing].Base.ToString() );
				AddLabel(237, 267, 1149, m_Pet.Skills[SkillName.Archery].Base.ToString() );
				AddLabel(237, 284, 1149, m_Pet.Skills[SkillName.Parry].Base.ToString() );
			}

			if ( m_Pet != null )
			{
				if ( m_Pet.Skills[SkillName.Wrestling].Lock == SkillLock.Up )
				{
					AddButton(293, 82, 2435, 2436, 1, GumpButtonType.Reply, 0);
				}
				else if ( m_Pet.Skills[SkillName.Wrestling].Lock == SkillLock.Down )
				{
					AddButton(293, 82, 2437, 2438, 1, GumpButtonType.Reply, 0);
				}
				else
				{
					AddButton(293, 82, 2092, 2092, 1, GumpButtonType.Reply, 0);
				}

				if ( m_Pet.Skills[SkillName.Tactics].Lock == SkillLock.Up )
				{
					AddButton(293, 99, 2435, 2436, 2, GumpButtonType.Reply, 0);
				}
				else if ( m_Pet.Skills[SkillName.Tactics].Lock == SkillLock.Down )
				{
					AddButton(293, 99, 2437, 2438, 2, GumpButtonType.Reply, 0);
				}
				else
				{
					AddButton(293, 99, 2092, 2092, 2, GumpButtonType.Reply, 0);
				}

				if ( m_Pet.Skills[SkillName.Anatomy].Lock == SkillLock.Up )
				{
					AddButton(293, 116, 2435, 2436, 3, GumpButtonType.Reply, 0);
				}
				else if ( m_Pet.Skills[SkillName.Anatomy].Lock == SkillLock.Down )
				{
					AddButton(293, 116, 2437, 2438, 3, GumpButtonType.Reply, 0);
				}
				else
				{
					AddButton(293, 116, 2092, 2092, 3, GumpButtonType.Reply, 0);
				}

				if ( m_Pet.Skills[SkillName.MagicResist].Lock == SkillLock.Up )
				{
					AddButton(293, 133, 2435, 2436, 4, GumpButtonType.Reply, 0);
				}
				else if ( m_Pet.Skills[SkillName.MagicResist].Lock == SkillLock.Down )
				{
					AddButton(293, 133, 2437, 2438, 4, GumpButtonType.Reply, 0);
				}
				else
				{
					AddButton(293, 133, 2092, 2092, 4, GumpButtonType.Reply, 0);
				}

				if ( m_Pet.Skills[SkillName.Poisoning].Lock == SkillLock.Up )
				{
					AddButton(293, 150, 2435, 2436, 5, GumpButtonType.Reply, 0);
				}
				else if ( m_Pet.Skills[SkillName.Poisoning].Lock == SkillLock.Down )
				{
					AddButton(293, 150, 2437, 2438, 5, GumpButtonType.Reply, 0);
				}
				else
				{
					AddButton(293, 150, 2092, 2092, 5, GumpButtonType.Reply, 0);
				}

				if ( m_Pet.Skills[SkillName.Magery].Lock == SkillLock.Up )
				{
					AddButton(293, 167, 2435, 2436, 6, GumpButtonType.Reply, 0);
				}
				else if ( m_Pet.Skills[SkillName.Magery].Lock == SkillLock.Down )
				{
					AddButton(293, 167, 2437, 2438, 6, GumpButtonType.Reply, 0);
				}
				else
				{
					AddButton(293, 167, 2092, 2092, 6, GumpButtonType.Reply, 0);
				}

				if ( m_Pet.Skills[SkillName.EvalInt].Lock == SkillLock.Up )
				{
					AddButton(293, 184, 2435, 2436, 7, GumpButtonType.Reply, 0);
				}
				else if ( m_Pet.Skills[SkillName.EvalInt].Lock == SkillLock.Down )
				{
					AddButton(293, 184, 2437, 2438, 7, GumpButtonType.Reply, 0);
				}
				else
				{
					AddButton(293, 184, 2092, 2092, 7, GumpButtonType.Reply, 0);
				}

				if ( m_Pet.Skills[SkillName.Meditation].Lock == SkillLock.Up )
				{
					AddButton(293, 201, 2435, 2436, 8, GumpButtonType.Reply, 0);
				}
				else if ( m_Pet.Skills[SkillName.Meditation].Lock == SkillLock.Down )
				{
					AddButton(293, 201, 2437, 2438, 8, GumpButtonType.Reply, 0);
				}
				else
				{
					AddButton(293, 201, 2092, 2092, 8, GumpButtonType.Reply, 0);
				}

				if ( m_Pet.Skills[SkillName.Swords].Lock == SkillLock.Up )
				{
					AddButton(293, 218, 2435, 2436, 9, GumpButtonType.Reply, 0);
				}
				else if ( m_Pet.Skills[SkillName.Swords].Lock == SkillLock.Down )
				{
					AddButton(293, 218, 2437, 2438, 9, GumpButtonType.Reply, 0);
				}
				else
				{
					AddButton(293, 218, 2092, 2092, 9, GumpButtonType.Reply, 0);
				}

				if ( m_Pet.Skills[SkillName.Macing].Lock == SkillLock.Up )
				{
					AddButton(293, 235, 2435, 2436, 10, GumpButtonType.Reply, 0);
				}
				else if ( m_Pet.Skills[SkillName.Macing].Lock == SkillLock.Down )
				{
					AddButton(293, 235, 2437, 2438, 10, GumpButtonType.Reply, 0);
				}
				else
				{
					AddButton(293, 235, 2092, 2092, 10, GumpButtonType.Reply, 0);
				}

				if ( m_Pet.Skills[SkillName.Fencing].Lock == SkillLock.Up )
				{
					AddButton(293, 252, 2435, 2436, 11, GumpButtonType.Reply, 0);
				}
				else if ( m_Pet.Skills[SkillName.Fencing].Lock == SkillLock.Down )
				{
					AddButton(293, 252, 2437, 2438, 11, GumpButtonType.Reply, 0);
				}
				else
				{
					AddButton(293, 252, 2092, 2092, 11, GumpButtonType.Reply, 0);
				}

				if ( m_Pet.Skills[SkillName.Archery].Lock == SkillLock.Up )
				{
					AddButton(293, 269, 2435, 2436, 12, GumpButtonType.Reply, 0);
				}
				else if ( m_Pet.Skills[SkillName.Archery].Lock == SkillLock.Down )
				{
					AddButton(293, 269, 2437, 2438, 12, GumpButtonType.Reply, 0);
				}
				else
				{
					AddButton(293, 269, 2092, 2092, 12, GumpButtonType.Reply, 0);
				}

				if ( m_Pet.Skills[SkillName.Parry].Lock == SkillLock.Up )
				{
					AddButton(293, 286, 2435, 2436, 13, GumpButtonType.Reply, 0);
				}
				else if ( m_Pet.Skills[SkillName.Parry].Lock == SkillLock.Down )
				{
					AddButton(293, 286, 2437, 2438, 13, GumpButtonType.Reply, 0);
				}
				else
				{
					AddButton(293, 286, 2092, 2092, 13, GumpButtonType.Reply, 0);
				}
			}

			AddLabel(38, 329, 1160, @"Total Skills:");
			
			if ( m_Pet != null )
				AddLabel(117, 329, 1149, m_Pet.SkillsTotal.ToString() );

			AddLabel(184, 329, 1160, @"Skill Cap:");

			if ( m_Pet != null )
				AddLabel(246, 329, 1149, m_Pet.SkillsCap.ToString() );

			if ( m_Pet != null )
				AddLabel(150, 28, 1149, m_Pet.Name.ToString() );

		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

			if ( info.ButtonID == 1 )
			{
				if ( m_Pet.Skills[SkillName.Wrestling].Lock == SkillLock.Up )
				{
					m_Pet.Skills[SkillName.Wrestling].SetLockNoRelay( SkillLock.Down );
					m_Pet.Skills[SkillName.Wrestling].Update();
				}
				else if ( m_Pet.Skills[SkillName.Wrestling].Lock == SkillLock.Down )
				{
					m_Pet.Skills[SkillName.Wrestling].SetLockNoRelay( SkillLock.Locked );
					m_Pet.Skills[SkillName.Wrestling].Update();
				}
				else if ( m_Pet.Skills[SkillName.Wrestling].Lock == SkillLock.Locked )
				{
					m_Pet.Skills[SkillName.Wrestling].SetLockNoRelay( SkillLock.Up );
					m_Pet.Skills[SkillName.Wrestling].Update();
				}

				from.SendGump( new PetSkillsGump( m_Pet ) );
			}

			if ( info.ButtonID == 2 )
			{
				if ( m_Pet.Skills[SkillName.Tactics].Lock == SkillLock.Up )
				{
					m_Pet.Skills[SkillName.Tactics].SetLockNoRelay( SkillLock.Down );
					m_Pet.Skills[SkillName.Tactics].Update();
				}
				else if ( m_Pet.Skills[SkillName.Tactics].Lock == SkillLock.Down )
				{
					m_Pet.Skills[SkillName.Tactics].SetLockNoRelay( SkillLock.Locked );
					m_Pet.Skills[SkillName.Tactics].Update();
				}
				else if ( m_Pet.Skills[SkillName.Tactics].Lock == SkillLock.Locked )
				{
					m_Pet.Skills[SkillName.Tactics].SetLockNoRelay( SkillLock.Up );
					m_Pet.Skills[SkillName.Tactics].Update();
				}

				from.SendGump( new PetSkillsGump( m_Pet ) );
			}

			if ( info.ButtonID == 3 )
			{
				if ( m_Pet.Skills[SkillName.Anatomy].Lock == SkillLock.Up )
				{
					m_Pet.Skills[SkillName.Anatomy].SetLockNoRelay( SkillLock.Down );
					m_Pet.Skills[SkillName.Anatomy].Update();
				}
				else if ( m_Pet.Skills[SkillName.Anatomy].Lock == SkillLock.Down )
				{
					m_Pet.Skills[SkillName.Anatomy].SetLockNoRelay( SkillLock.Locked );
					m_Pet.Skills[SkillName.Anatomy].Update();
				}
				else if ( m_Pet.Skills[SkillName.Anatomy].Lock == SkillLock.Locked )
				{
					m_Pet.Skills[SkillName.Anatomy].SetLockNoRelay( SkillLock.Up );
					m_Pet.Skills[SkillName.Anatomy].Update();
				}

				from.SendGump( new PetSkillsGump( m_Pet ) );
			}

			if ( info.ButtonID == 4 )
			{
				if ( m_Pet.Skills[SkillName.MagicResist].Lock == SkillLock.Up )
				{
					m_Pet.Skills[SkillName.MagicResist].SetLockNoRelay( SkillLock.Down );
					m_Pet.Skills[SkillName.MagicResist].Update();
				}
				else if ( m_Pet.Skills[SkillName.MagicResist].Lock == SkillLock.Down )
				{
					m_Pet.Skills[SkillName.MagicResist].SetLockNoRelay( SkillLock.Locked );
					m_Pet.Skills[SkillName.MagicResist].Update();
				}
				else if ( m_Pet.Skills[SkillName.MagicResist].Lock == SkillLock.Locked )
				{
					m_Pet.Skills[SkillName.MagicResist].SetLockNoRelay( SkillLock.Up );
					m_Pet.Skills[SkillName.MagicResist].Update();
				}

				from.SendGump( new PetSkillsGump( m_Pet ) );
			}

			if ( info.ButtonID == 5 )
			{
				if ( m_Pet.Skills[SkillName.Poisoning].Lock == SkillLock.Up )
				{
					m_Pet.Skills[SkillName.Poisoning].SetLockNoRelay( SkillLock.Down );
					m_Pet.Skills[SkillName.Poisoning].Update();
				}
				else if ( m_Pet.Skills[SkillName.Poisoning].Lock == SkillLock.Down )
				{
					m_Pet.Skills[SkillName.Poisoning].SetLockNoRelay( SkillLock.Locked );
					m_Pet.Skills[SkillName.Poisoning].Update();
				}
				else if ( m_Pet.Skills[SkillName.Poisoning].Lock == SkillLock.Locked )
				{
					m_Pet.Skills[SkillName.Poisoning].SetLockNoRelay( SkillLock.Up );
					m_Pet.Skills[SkillName.Poisoning].Update();
				}

				from.SendGump( new PetSkillsGump( m_Pet ) );
			}

			if ( info.ButtonID == 6 )
			{
				if ( m_Pet.Skills[SkillName.Magery].Lock == SkillLock.Up )
				{
					m_Pet.Skills[SkillName.Magery].SetLockNoRelay( SkillLock.Down );
					m_Pet.Skills[SkillName.Magery].Update();
				}
				else if ( m_Pet.Skills[SkillName.Magery].Lock == SkillLock.Down )
				{
					m_Pet.Skills[SkillName.Magery].SetLockNoRelay( SkillLock.Locked );
					m_Pet.Skills[SkillName.Magery].Update();
				}
				else if ( m_Pet.Skills[SkillName.Magery].Lock == SkillLock.Locked )
				{
					m_Pet.Skills[SkillName.Magery].SetLockNoRelay( SkillLock.Up );
					m_Pet.Skills[SkillName.Magery].Update();
				}

				from.SendGump( new PetSkillsGump( m_Pet ) );
			}

			if ( info.ButtonID == 7 )
			{
				if ( m_Pet.Skills[SkillName.EvalInt].Lock == SkillLock.Up )
				{
					m_Pet.Skills[SkillName.EvalInt].SetLockNoRelay( SkillLock.Down );
					m_Pet.Skills[SkillName.EvalInt].Update();
				}
				else if ( m_Pet.Skills[SkillName.EvalInt].Lock == SkillLock.Down )
				{
					m_Pet.Skills[SkillName.EvalInt].SetLockNoRelay( SkillLock.Locked );
					m_Pet.Skills[SkillName.EvalInt].Update();
				}
				else if ( m_Pet.Skills[SkillName.EvalInt].Lock == SkillLock.Locked )
				{
					m_Pet.Skills[SkillName.EvalInt].SetLockNoRelay( SkillLock.Up );
					m_Pet.Skills[SkillName.EvalInt].Update();
				}

				from.SendGump( new PetSkillsGump( m_Pet ) );
			}

			if ( info.ButtonID == 8 )
			{
				if ( m_Pet.Skills[SkillName.Meditation].Lock == SkillLock.Up )
				{
					m_Pet.Skills[SkillName.Meditation].SetLockNoRelay( SkillLock.Down );
					m_Pet.Skills[SkillName.Meditation].Update();
				}
				else if ( m_Pet.Skills[SkillName.Meditation].Lock == SkillLock.Down )
				{
					m_Pet.Skills[SkillName.Meditation].SetLockNoRelay( SkillLock.Locked );
					m_Pet.Skills[SkillName.Meditation].Update();
				}
				else if ( m_Pet.Skills[SkillName.Meditation].Lock == SkillLock.Locked )
				{
					m_Pet.Skills[SkillName.Meditation].SetLockNoRelay( SkillLock.Up );
					m_Pet.Skills[SkillName.Meditation].Update();
				}

				from.SendGump( new PetSkillsGump( m_Pet ) );
			}

			if ( info.ButtonID == 9 )
			{
				if ( m_Pet.Skills[SkillName.Swords].Lock == SkillLock.Up )
				{
					m_Pet.Skills[SkillName.Swords].SetLockNoRelay( SkillLock.Down );
					m_Pet.Skills[SkillName.Swords].Update();
				}
				else if ( m_Pet.Skills[SkillName.Swords].Lock == SkillLock.Down )
				{
					m_Pet.Skills[SkillName.Swords].SetLockNoRelay( SkillLock.Locked );
					m_Pet.Skills[SkillName.Swords].Update();
				}
				else if ( m_Pet.Skills[SkillName.Swords].Lock == SkillLock.Locked )
				{
					m_Pet.Skills[SkillName.Swords].SetLockNoRelay( SkillLock.Up );
					m_Pet.Skills[SkillName.Swords].Update();
				}

				from.SendGump( new PetSkillsGump( m_Pet ) );
			}

			if ( info.ButtonID == 10 )
			{
				if ( m_Pet.Skills[SkillName.Macing].Lock == SkillLock.Up )
				{
					m_Pet.Skills[SkillName.Macing].SetLockNoRelay( SkillLock.Down );
					m_Pet.Skills[SkillName.Macing].Update();
				}
				else if ( m_Pet.Skills[SkillName.Macing].Lock == SkillLock.Down )
				{
					m_Pet.Skills[SkillName.Macing].SetLockNoRelay( SkillLock.Locked );
					m_Pet.Skills[SkillName.Macing].Update();
				}
				else if ( m_Pet.Skills[SkillName.Macing].Lock == SkillLock.Locked )
				{
					m_Pet.Skills[SkillName.Macing].SetLockNoRelay( SkillLock.Up );
					m_Pet.Skills[SkillName.Macing].Update();
				}

				from.SendGump( new PetSkillsGump( m_Pet ) );
			}

			if ( info.ButtonID == 11 )
			{
				if ( m_Pet.Skills[SkillName.Fencing].Lock == SkillLock.Up )
				{
					m_Pet.Skills[SkillName.Fencing].SetLockNoRelay( SkillLock.Down );
					m_Pet.Skills[SkillName.Fencing].Update();
				}
				else if ( m_Pet.Skills[SkillName.Fencing].Lock == SkillLock.Down )
				{
					m_Pet.Skills[SkillName.Fencing].SetLockNoRelay( SkillLock.Locked );
					m_Pet.Skills[SkillName.Fencing].Update();
				}
				else if ( m_Pet.Skills[SkillName.Fencing].Lock == SkillLock.Locked )
				{
					m_Pet.Skills[SkillName.Fencing].SetLockNoRelay( SkillLock.Up );
					m_Pet.Skills[SkillName.Fencing].Update();
				}

				from.SendGump( new PetSkillsGump( m_Pet ) );
			}

			if ( info.ButtonID == 12 )
			{
				if ( m_Pet.Skills[SkillName.Archery].Lock == SkillLock.Up )
				{
					m_Pet.Skills[SkillName.Archery].SetLockNoRelay( SkillLock.Down );
					m_Pet.Skills[SkillName.Archery].Update();
				}
				else if ( m_Pet.Skills[SkillName.Archery].Lock == SkillLock.Down )
				{
					m_Pet.Skills[SkillName.Archery].SetLockNoRelay( SkillLock.Locked );
					m_Pet.Skills[SkillName.Archery].Update();
				}
				else if ( m_Pet.Skills[SkillName.Archery].Lock == SkillLock.Locked )
				{
					m_Pet.Skills[SkillName.Archery].SetLockNoRelay( SkillLock.Up );
					m_Pet.Skills[SkillName.Archery].Update();
				}

				from.SendGump( new PetSkillsGump( m_Pet ) );
			}

			if ( info.ButtonID == 13 )
			{
				if ( m_Pet.Skills[SkillName.Parry].Lock == SkillLock.Up )
				{
					m_Pet.Skills[SkillName.Parry].SetLockNoRelay( SkillLock.Down );
					m_Pet.Skills[SkillName.Parry].Update();
				}
				else if ( m_Pet.Skills[SkillName.Parry].Lock == SkillLock.Down )
				{
					m_Pet.Skills[SkillName.Parry].SetLockNoRelay( SkillLock.Locked );
					m_Pet.Skills[SkillName.Parry].Update();
				}
				else if ( m_Pet.Skills[SkillName.Parry].Lock == SkillLock.Locked )
				{
					m_Pet.Skills[SkillName.Parry].SetLockNoRelay( SkillLock.Up );
					m_Pet.Skills[SkillName.Parry].Update();
				}

				from.SendGump( new PetSkillsGump( m_Pet ) );
			}
		}
	}
}