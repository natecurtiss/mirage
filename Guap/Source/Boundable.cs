namespace Guap;

public interface Boundable
{
    Bounds Bounds();

    public bool IsAbove(Boundable other) => Bounds().IsAbove(other.Bounds());
    public bool IsBelow(Boundable other) => Bounds().IsBelow(other.Bounds());
    public bool IsRightOf(Boundable other) => Bounds().IsRightOf(other.Bounds());
    public bool IsLeftOf(Boundable other) => Bounds().IsLeftOf(other.Bounds());
    public bool IsCompletelyAbove(Boundable other) => Bounds().IsCompletelyAbove(other.Bounds());
    public bool IsCompletelyBelow(Boundable other) => Bounds().IsCompletelyBelow(other.Bounds());
    public bool IsCompletelyRightOf(Boundable other) => Bounds().IsCompletelyRightOf(other.Bounds());
    public bool IsCompletelyLeftOf(Boundable other) => Bounds().IsCompletelyLeftOf(other.Bounds());
}