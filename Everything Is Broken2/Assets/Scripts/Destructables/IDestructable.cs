public interface IDestructable
{
    DestructableData DestructableData { get; }

    void ReceiveDamage(int damage);

    void Destroy();
}
