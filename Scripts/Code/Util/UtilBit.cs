using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EX
// public enum ActRangeCard : uint
// {
//     All = 1 << 0,
//     Random = 1 << 1,
//     Select = 1 << 2,
//     AttackCard = 1 << 3,
//     SkillCard = 1 << 4,
//     PassiveCard = 1 << 5,
// }
public static partial class Util
{
    public static bool BitExist(this uint origin, uint compare)
    {
        return 0 != (origin & compare);
    }
    public static bool BitEqual(this uint origin, uint compare)
    {
        return 0 == (origin ^ compare);
    }
    public static uint BitAdd(this uint origin, uint add)
    {
        return origin | add;
    }
    public static uint BitSub(this uint origin, uint sub)
    {
        if(BitExist(origin,sub))
            return origin ^ sub;
        return origin;
    }


}
