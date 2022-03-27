using System.Numerics;

namespace Guap.Rendering;

public struct PropOptions
{
    public readonly string Texture;
    public readonly Vector2 Position;
    public readonly Vector2 Scale;
    public readonly float Rotation;

    public PropOptions(string texture, Vector2 position, Vector2 scale, float rotation)
    {
        Texture = texture;
        Position = position;
        Scale = scale;
        Rotation = rotation;
    }

    public PropOptions WithTexture(string texture) => this = new(texture, Position, Scale, Rotation);
    public PropOptions AtPosition(Vector2 position) => this = new(Texture, position, Scale, Rotation);
    public PropOptions WithScale(Vector2 scale) => this = new(Texture, Position, scale, Rotation);
    public PropOptions WithRotation(float rotation) => this = new(Texture, Position, Scale, rotation);
}