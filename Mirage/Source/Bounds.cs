namespace Mirage;

/// <summary>
/// A representation of a bounding-box with a position and size.
/// </summary>
public struct Bounds
{
    /// <summary>
    /// The position of the <see cref="Bounds"/>.
    /// </summary>
    public readonly Vector2 Center;
    
    /// <summary>
    /// The size of the <see cref="Bounds"/>.
    /// </summary>
    public readonly Vector2 Size;
    
    /// <summary>
    /// Half of the <see cref="Bounds"/>' <see cref="Size"/>.
    /// </summary>
    public Vector2 Extents => Size / 2f;
    
    /// <summary>
    /// The left-center of the <see cref="Bounds"/>.
    /// </summary>
    public Vector2 Left => new(Center.X - Extents.X, Center.Y);
    
    /// <summary>
    /// The right-center of the <see cref="Bounds"/>.
    /// </summary>
    public Vector2 Right => new(Center.X + Extents.X, Center.Y);
    
    /// <summary>
    /// The bottom-center of the <see cref="Bounds"/>.
    /// </summary>
    public Vector2 Bottom => new(Center.X, Center.Y - Extents.Y);
    
    /// <summary>
    /// The top-center of the <see cref="Bounds"/>.
    /// </summary>
    public Vector2 Top => new(Center.X, Center.Y + Extents.Y);
    
    /// <summary>
    /// Creates a <see cref="Bounds"/> with a position and size.
    /// </summary>
    /// <param name="center">The position of the <see cref="Bounds"/>.</param>
    /// <param name="size">The size of the <see cref="Bounds"/>.</param>
    public Bounds(Vector2 center, Vector2 size)
    {
        Center = center;
        Size = size;
    }

    /// <summary>
    /// Returns true if the point overlaps the <see cref="Bounds"/>.
    /// </summary>
    /// <param name="point">The point to compare to.</param>
    public bool Overlaps(Vector2 point) =>
        point.X >= Left.X &&
        point.X <= Right.X &&
        point.Y >= Bottom.Y &&
        point.Y <= Top.Y;

    /// <summary>
    /// Returns true if the <see cref="Bounds"/> overlaps the other <see cref="Bounds"/>.
    /// </summary>
    /// <param name="other">The other <see cref="Bounds"/> to compare to.</param>
    public bool Overlaps(Bounds other)
    {
        var bottomLeft1 = new Vector2(Left.X, Bottom.Y);
        var topRight1 = new Vector2(Right.X, Top.Y);
        var bottomLeft2 = new Vector2(other.Left.X, other.Bottom.Y);
        var topRight2 = new Vector2(other.Right.X, other.Top.Y);
        
        var oneIsALine = bottomLeft1.X == topRight1.X || bottomLeft1.Y == topRight1.Y || bottomLeft2.X == topRight2.X || bottomLeft2.Y == topRight2.Y;
        if (oneIsALine)
            return false;

        var oneIsToTheLeft = bottomLeft1.X >= topRight2.X || bottomLeft2.X >= topRight1.X;
        if (oneIsToTheLeft)
            return false;

        var oneIsOnTop = topRight1.Y <= bottomLeft2.Y || topRight2.Y <= bottomLeft1.Y;
        return !oneIsOnTop;
    }

    /// <summary>
    /// Returns true if the top edge of the <see cref="Bounds"/> is above the other <see cref="Bounds"/>.
    /// </summary>
    /// <param name="other">The other <see cref="Bounds"/> to compare to.</param>
    public bool IsAbove(Bounds other) => Top.Y >= other.Top.Y;
    
    /// <summary>
    /// Returns true if the bottom edge of the <see cref="Bounds"/> is below the other <see cref="Bounds"/>.
    /// </summary>
    /// <param name="other">The other <see cref="Bounds"/> to compare to.</param>
    public bool IsBelow(Bounds other) => Bottom.Y <= other.Bottom.Y;
    
    /// <summary>
    /// Returns true if the right edge of the <see cref="Bounds"/> is to the right of the other <see cref="Bounds"/>.
    /// </summary>
    /// <param name="other">The other <see cref="Bounds"/> to compare to.</param>
    public bool IsRightOf(Bounds other) => Right.X >= other.Right.X;
    
    /// <summary>
    /// Returns true if the left edge of the <see cref="Bounds"/> is to the left of the other <see cref="Bounds"/>.
    /// </summary>
    /// <param name="other">The other <see cref="Bounds"/> to compare to.</param>
    public bool IsLeftOf(Bounds other) => Left.X <= other.Left.X;
    
    /// <summary>
    /// Returns true if the <see cref="Bounds"/> is completely above and not overlapping the other <see cref="Bounds"/>.
    /// </summary>
    /// <param name="other">The other <see cref="Bounds"/> to compare to.</param>
    public bool IsCompletelyAbove(Bounds other) => Bottom.Y >= other.Top.Y;
    
    /// <summary>
    /// Returns true if the <see cref="Bounds"/> is completely below and not overlapping the other <see cref="Bounds"/>.
    /// </summary>
    /// <param name="other">The other <see cref="Bounds"/> to compare to.</param>
    public bool IsCompletelyBelow(Bounds other) => other.IsCompletelyAbove(this);
    
    /// <summary>
    /// Returns true if the <see cref="Bounds"/> is completely to the right of and not overlapping the other <see cref="Bounds"/>.
    /// </summary>
    /// <param name="other">The other <see cref="Bounds"/> to compare to.</param>
    public bool IsCompletelyRightOf(Bounds other) => Left.X >= other.Right.X;
    
    /// <summary>
    /// Returns true if the <see cref="Bounds"/> is completely to the left of and not overlapping the other <see cref="Bounds"/>.
    /// </summary>
    /// <param name="other">The other <see cref="Bounds"/> to compare to.</param>
    public bool IsCompletelyLeftOf(Bounds other) => other.IsCompletelyRightOf(this);

    /// <summary>
    /// Expands the <see cref="Bounds"/> to fit the specified point.
    /// </summary>
    /// <param name="point">The point to encapsulate.</param>
    public void Encapsulate(Vector2 point)
    {
        if (Overlaps(point)) return;
        var left = Left.X;
        var right = Right.X;
        var bottom = Bottom.Y;
        var top = Top.Y;
        if (point.X < Left.X)
            left = point.X;
        else if (point.X > Right.X)
            right = point.X;
        if (point.Y < Bottom.Y)
            bottom = point.Y;
        else if (point.Y > Top.Y)
            top = point.Y;
        var bottomLeft = new Vector2(left, bottom);
        var topRight = new Vector2(right, top);
        var size = topRight - bottomLeft;
        var center = bottomLeft + size / 2f;
        this = new(center, size);
    }
}