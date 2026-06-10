using System.Numerics;

namespace Game.Utils;

public static class GameMath
{
    public static Vector2 Project(Vector2 vector, Vector2 vectorBase)
    {
        return vectorBase * Vector2.Dot(vector, vectorBase) / vectorBase.LengthSquared();
    }

    public static Vector2 GetSegmentNormal(Vector2 start, Vector2 end)
    {
        Vector2 dir = end - start;
        return Vector2.Transform(Vector2.Normalize(dir), Matrix3x2.CreateRotation((float) -Math.PI / 2));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="angle">Angle to rotate (in radians).</param>
    /// <returns></returns>
    public static Vector2 RotatedVector(Vector2 vector, float angle)
    {
        return Vector2.Transform(vector, Matrix3x2.CreateRotation((float) angle));
    }

    /**
    /// <summary>
    /// 
    /// </summary>
    /// <returns>Collision point between two line segments if there is a collision, 
    /// otherwise returns the first segment's end point</returns>
    public static Vector2 GetCollisionPoint((Vector2, Vector2) segmentA, (Vector2, Vector2) segmentB)
    {
        Vector2 aDir = segmentA.Item2 - segmentA.Item1;
        Vector2 bDir = segmentB.Item2 - segmentB.Item1;

        Vector2 bNormal = Vector2.Transform(Vector2.Normalize(bDir), Matrix3x2.CreateRotation((float) -Math.PI / 2));

        // Project a onto the normal of B to check if center of the segmentB is 
        // inside the segment created by the first point of segmentA and the projected vector
        Vector2 aEndProjected = Project(segmentA.Item1 + aDir, bNormal);
        Vector2 aStartProjected = Project(segmentA.Item1, bNormal);


        return segmentA.Item2;
    }
    **/
}