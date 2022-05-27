using UnityEngine;

public enum Grade
{
    Normal,
    Rare,
    Epic,
    Legendary
}
public static class GradeExtension
{
    public static Color ToColor(this Grade grade)
    {
        switch (grade)
        {
            case Grade.Normal:
                return ColorSet.Grade_Normal;
            case Grade.Rare:
                return ColorSet.Grade_Rare;
            case Grade.Epic:
                return ColorSet.Grade_Epic;
            case Grade.Legendary:
                return ColorSet.Grade_Legend;
            default:
                return ColorSet.Grade_Normal;
        }
    }
    public static Color ToUpgrade(this Grade grade)
    {
        switch (grade)
        {
            case Grade.Normal:
                return ColorSet.Grade_Normal_Upgrade;
            case Grade.Rare:
                return ColorSet.Grade_Rare_Upgrade;
            case Grade.Epic:
                return ColorSet.Grade_Epic_Upgrade;
            case Grade.Legendary:
                return ColorSet.Grade_Legend_Upgrade;
            default:
                return ColorSet.Grade_Normal_Upgrade;
        }
    }
}