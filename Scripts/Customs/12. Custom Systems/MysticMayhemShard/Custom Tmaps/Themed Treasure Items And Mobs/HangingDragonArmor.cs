/***************************************************************************
 *                               CREDITS
 *                         -------------------
 *                         : (C) 2004-2009 Luke Tomasello (AKA Adam Ant)
 *                         :   and the Angel Island Software Team
 *                         :   luke@tomasello.com
 *                         :   Official Documentation:
 *                         :   www.game-master.net, wiki.game-master.net
 *                         :   Official Source Code (SVN Repository):
 *                         :   http://game-master.net:8050/svn/angelisland
 *                         : 
 *                         : (C) May 1, 2002 The RunUO Software Team
 *                         :   info@runuo.com
 *
 *   Give credit where credit is due!
 *   Even though this is 'free software', you are encouraged to give
 *    credit to the individuals that spent many hundreds of hours
 *    developing this software.
 *   Many of the ideas you will find in this Angel Island version of 
 *   Ultima Online are unique and one-of-a-kind in the gaming industry! 
 *
 ***************************************************************************/

/***************************************************************************
 *
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 2 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/

/* Scripts/Items/TreasureThemes/HangingDragonArmor.cs
 * CHANGELOG
 *	04/07/05, Kitaras	
 *		Initial Creation
 */

using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Mobiles;
using Server.Multis;
using Server.Misc;

namespace Server.Items
{
        [Flipable( 5095, 5096 )]  
	public class HangingDragonChest: BaseDecorationArtifact 
	{	
	public override int ArtifactRarity{ get{ return 6; } }
		
       		[Constructable]
		public HangingDragonChest() : base(5095)
		{
			Weight = 20.0;
			Hue = 1645;
			Name = "Hanging Dragonscale Chest";
				
		}

        	public HangingDragonChest(Serial serial) : base( serial )
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

	[Flipable( 5093, 5094 )]  
	public class HangingDragonLegs: BaseDecorationArtifact 
	{	
	public override int ArtifactRarity{ get{ return 6; } }
		
       		[Constructable]
		public HangingDragonLegs() : base(5093)
		{
			Weight = 20.0;
			Hue = 1645;
			Name = "Hanging Dragonscale Legs";
				
		}

        	public HangingDragonLegs(Serial serial) : base( serial )
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

	[Flipable( 5097, 5098 )]  
	public class HangingDragonArms: BaseDecorationArtifact 
	{	
	
		public override int ArtifactRarity{ get{ return 6; } }
       		[Constructable]
		public HangingDragonArms() : base(5097)
		{
			Weight = 20.0;
			Hue = 1645;
			Name = "Hanging Dragonscale Arms";
				
		}

        	public HangingDragonArms(Serial serial) : base( serial )
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
