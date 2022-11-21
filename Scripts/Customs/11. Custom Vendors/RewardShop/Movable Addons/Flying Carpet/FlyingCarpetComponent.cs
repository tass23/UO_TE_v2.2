using System;
using Server;
using Server.Items;

namespace Solaris.Addons
{
	public enum FlyingCarpetType
	{
		PatternBlue = 0,
		BlankBlue = 1,
		FancyBlue = 2,
		PatternRed = 3,
		BlankRed = 4,
		SwirlBlueBrown = 5,
		SwirlGoldBrown = 6,
		StarBrown = 7, 
		StarBlueBrownA = 8,
		StarBlueBrownB = 9
	}
	
	public class FlyingCarpetComponent : MovableAddonComponent
	{
		
		//carpet types
		
		public static int[][][] CarpetIDs = new int[][][]
		{
		
			//type: 0
			//description:	patterned blue
			new int[][]
			{ 
				//center ID's:	2749, 2751, 2752, 2753
				new int[]{ 2749, 2751, 2752, 2753 },
				
				//edge ID's:		2806(W), 2807(N), 2808(E), 2809(S)
				new int[]{ 2806, 2807, 2808, 2809 },
				//corner ID's:	2755(NW), 2757(NE), 2754(SE), 2756(SW)
				new int[]{ 2755, 2757, 2754, 2756 }
			},
				
			//type: 1
			//description:	blank blue
			new int[][]
			{
				//center ID's:	2750
				new int[]{ 2750, 2750, 2750, 2750 },
				//edge ID's:		2806(W), 2807(N), 2808(E), 2809(S)
				new int[]{ 2806, 2807, 2808, 2809 },
				//corner ID's:	2755(NW), 2757(NE), 2754(SE), 2756(SW)
				new int[]{ 2755, 2757, 2754, 2756 }
			},
				
			
			//type: 2
			//description:	fancy blue
			new int[][]
			{
				//center ID's:	2810
				new int[]{ 2810, 2810, 2810, 2810 },
				//edge ID's:		2806(W), 2807(N), 2808(E), 2809(S)
				new int[]{ 2806, 2807, 2808, 2809 },
				//corner ID's:	2754(SE), 2755(NW), 2756(SW), 2757(NE)
				new int[]{ 2755, 2757, 2754, 2756 }
			},
			
			//type: 3
			//description:	patterned red
			new int[][]
			{
				//center ID's:	2758, 2759
				new int[]{ 2759, 2758, 2759, 2758 },
				//edge ID's:		2765(W), 2766(N), 2767(E), 2768(S)
				new int[]{ 2765, 2766, 2767, 2768 },
				//corner ID's:	2761(SE), 2762(NW), 2763(SW), 2764(NE)
				new int[]{ 2762, 2764, 2761, 2763 }
			},
				
			//type: 4
			//description:	blank red
			new int[][]
			{
				//center ID's:	2760
				new int[]{ 2760, 2760, 2760, 2760 },
				//edge ID's:		2765(W), 2766(N), 2767(E), 2768(S)
				new int[]{ 2765, 2766, 2767, 2768 },
				//corner ID's:	2761(SE), 2762(NW), 2763(SW), 2764(NE)
				new int[]{ 2762, 2764, 2761, 2763 }
			},
			
			//type: 5
			//description:	swirly blue/brown
			//corner ID's:	2770(SE), 2771(NW), 2772(SW), 2773(NE)
			new int[][]
			{
				//center ID's:	2769
				new int[]{ 2769, 2769, 2769, 2769 },
				//edge ID's:		2774(W), 2775(N), 2776(E), 2777(S)
				new int[]{ 2774, 2775, 2776, 2777 },
				//corner ID's:	2770(SE), 2771(NW), 2772(SW), 2773(NE)
				new int[]{ 2771, 2773, 2770, 2772 }
			},
			
			//type: 6
			//description:	swirly gold/brown
			new int[][]
			{
				//center ID's:	2778
				new int[]{ 2778, 2778, 2778, 2778 },
				//edge ID's:		2783(W), 2784(N), 2785(E), 2786(S)
				new int[]{ 2783, 2784, 2785, 2786 },
				//corner ID's:	2779(SE), 2780(NW), 2781(SW), 2782(NE)
				new int[]{ 2780, 2782, 2779, 2781 }
			},
			
			//type: 7
			//description:	star brown
			new int[][]
			{
				//center ID's:	2795
				new int[]{ 2795, 2795, 2795, 2795 },
				//edge ID's:		2791(W), 2792(N), 2793(E), 2794(S)
				new int[]{ 2791, 2792, 2793, 2794 },
				//corner ID's:	2787(SE), 2788(NW), 2789(SW), 2790(NE)
				new int[]{ 2788, 2790, 2787, 2789 }
			},
			
			//type: 8
			//description:	star blue/brown 1
			new int[][]
			{
				//center ID's:	2796
				new int[]{ 2796, 2796, 2796, 2796 },
				//edge ID's:		2802(W), 2803(N), 2804(E), 2805(S)
				new int[]{ 2802, 2803, 2804, 2805 },
				//corner ID's:	2798(SE), 2799(NW), 2800(SW), 2801(NE)
				new int[]{ 2799, 2801, 2798, 2800 }
			},
			
			//type: 9
			//description:	star blue/brown 2
			new int[][]
			{
				//center ID's:	2797
				new int[]{ 2797, 2797, 2797, 2797 },
				//edge ID's:		2802(W), 2803(N), 2804(E), 2805(S)
				new int[]{ 2802, 2803, 2804, 2805 },
				//corner ID's:	2798(SE), 2799(NW), 2800(SW), 2801(NE)
				new int[]{ 2799, 2801, 2798, 2800 }
			}
						
		};

		public FlyingCarpetComponent( FlyingCarpetType type, int part, int direction ) : base( CarpetIDs[(int)type][part], direction )
		{
		}
		
		
		public FlyingCarpetComponent( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( 0 );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
		
		
	}
		
	
}