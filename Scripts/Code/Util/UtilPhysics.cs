using UnityEngine;
public static class UtilPhysics
{
    public static void Stop(this Rigidbody2D rigid)
    {
        rigid.velocity = Vector2.zero;
        rigid.angularVelocity = 0;
    }
}
