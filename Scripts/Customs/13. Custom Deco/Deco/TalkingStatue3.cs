using System; 
using Server.Network; 
namespace Server.Items 
{ 
[Flipable( 0x14F8, 0x14FA )] 
public class TalkingStatue3 : Item 
{ 
[Constructable] 
public TalkingStatue3() : this( 1 ) 
{ 
} 
//You need to tell an item if it handles OnMovement 
public override bool HandlesOnMovement{ get{ return true;} } 
public bool Spam = true;

[Constructable] 
public TalkingStatue3( int amount ) : base( 6942 ) 
{ 
Stackable = false; 
Weight = 1.0; 
Amount = amount; 
} 
public TalkingStatue3( Serial serial ) : base( serial ) 
{ 
} 

public override void Serialize( GenericWriter writer ) 
{ 
base.Serialize( writer ); 

writer.Write( (int) 0 ); 
} 

public override void Deserialize( GenericReader reader ) 
{ 
base.Deserialize( reader ); 

int version = reader.ReadInt(); 
} 

public override void OnMovement( Mobile m, Point3D oldLocation ) 
{ 
if (Spam) 
{
if (m.InRange(Location,3)) //Triggers speech when player moves within 3 spaces 
{
PublicOverheadMessage( MessageType.Regular, 4, true, "Welcome To Milkmans Destiny Shard. Enjoy your stay" ); 
// Change the 4 to whatever color you want the message to be 
Spam = false; 
new SpamTimer(this).Start(); 
}
}
} 
private class SpamTimer : Timer
{
private TalkingStatue3 m_God;

public SpamTimer( TalkingStatue3 god ) : base( TimeSpan.FromSeconds(10.0) ) //10 seconds between messages
{
m_God = god;
}

protected override void OnTick()
{
m_God.Spam = true;
}
}
} 
} 
