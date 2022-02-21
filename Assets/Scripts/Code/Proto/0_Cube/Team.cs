using UnityEngine;

public enum Team
{
    Gray,
    Blue,
    Red,
    Green,
    Orange,
    Pink,
    Max

}
public static class TeamExtension
{
    public static Color ToColor(this Team team)
    {
        var colorTeam = team switch
        {
            Team.Gray => Color.gray,
            Team.Blue => ColorSet.Grade_Rare,
            Team.Red => ColorSet.Damage,
            Team.Green => ColorSet.HP,
            Team.Orange => ColorSet.Grade_Legend,
            Team.Pink => ColorSet.Pink,
            _ => ColorSet.TranslucentBlack
        };
        return colorTeam;
    }
}
