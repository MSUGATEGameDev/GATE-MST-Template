using UnityEngine;

public class EntityUtils
{
    public static Headings GetRandomHeading()
    {
        return (Headings)Random.Range(0, 7);
    }

    public static Vector2 HeadingToVec2(Headings heading)
    {
        switch (heading)
        {
            case Headings.North:
                return Vector2.up;

            case Headings.NorthEast:
                return new Vector2(1, 1);

            case Headings.East:
                return Vector2.right;

            case Headings.SouthEast:
                return new Vector2(1, -1);

            case Headings.South:
                return Vector2.down;

            case Headings.SouthWest:
                return new Vector2(-1, -1);

            case Headings.West:
                return Vector2.left;

            case Headings.NorthWest:
                return new Vector2(-1, 1);

            default:
                return Vector2.zero;
        }
    }
}

public enum Headings
{
    North,
    NorthEast,
    East,
    SouthEast,
    South,
    SouthWest,
    West,
    NorthWest
}