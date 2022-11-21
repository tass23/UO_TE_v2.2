/*
 *	This program is	the	CONFIDENTIAL and PROPRIETARY property 
 *	of Tomasello Software LLC. Any unauthorized	use, reproduction or
 *	transfer of	this computer program is strictly prohibited.
 *
 *		Copyright (c) 2004 Tomasello Software LLC.
 *	This is	an unpublished work, and is	subject	to limited distribution	and
 *	restricted disclosure only.	ALL	RIGHTS RESERVED.
 *
 *			RESTRICTED RIGHTS LEGEND
 *	Use, duplication, or disclosure	by the Government is subject to
 *	restrictions set forth in subparagraph (c)(1)(ii) of the Rights	in
 *	Technical Data and Computer	Software clause	at DFARS 252.227-7013.
 *
 *	Angel Island UO	Shard	Version	1.0
 *			Release	A
 *			March 25, 2004
 */

/* Scripts/Items/TreasureThemes/BoneContainers.cs
 * CHANGELOG
 *  03/28/06 Taran Kain
 *		Override IDyable.Dye() to disable dying
 *		Why do we inherit Bag instead of BaseContainer? Can't change inherit order without killing serialization.
 *	04/01/05, Kitaras	
 *		Initial	Creation
 */

using System;
using System.Collections;
using Server.Multis;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{

	public class BoneContainer : Bag, IDyable
	{ 
		public override	int	MaxWeight{ get{	return 0; }	}
		public override	int	DefaultDropSound{ get{ return 0x42;	} }

		//3	differnt types 0-2 
		[Constructable]	
		public BoneContainer(int type)
		{ 
			
			Name = "a pile of bones";
			Movable	= true;
			GumpID = 9;
		
			if(type	== 0)ItemID	= 3789;
			if(type	== 1)ItemID	= 3790;
			if(type	== 2)ItemID	= 3792;
		}

		public BoneContainer( Serial serial	) :	base( serial )
		{ 
		} 

		public override	void Serialize(	GenericWriter writer ) 
		{ 
			base.Serialize(	writer ); 

			writer.Write( (int)	0 ); //	version	
		} 

		public override	void Deserialize( GenericReader	reader ) 
		{ 
			base.Deserialize( reader );	

			int	version	= reader.ReadInt();	
		}

		#region	IDyable	Members

		public new bool	Dye(Mobile from, DyeTub	sender)
		{
			from.SendMessage("You cannot dye that.");
			return false;
		}

		#endregion
	}
}