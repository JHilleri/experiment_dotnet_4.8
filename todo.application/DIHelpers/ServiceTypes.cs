namespace todo.application.DIHelpers;

public enum Lifetime
{
    Singleton,
    Transient,
    Scoped,
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class InjectableAttribute(Lifetime Lifetime = Lifetime.Transient)
    : Attribute
{
    public Lifetime Lifetime { get; } = Lifetime;
}
