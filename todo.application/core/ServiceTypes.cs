namespace todo.application.core;

public enum Lifetime
{
    Singleton,
    Transient,
    Scoped,
}

/// <summary>
/// Attribute to mark a class to be registered in the dependency injection using the interfaces it implements.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class InjectableAttribute(Lifetime lifetime = Lifetime.Scoped) : Attribute
{
    public Lifetime Lifetime { get; } = lifetime;
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public class InjectableOfAttribute<T>() : Attribute
{
    public Type Type { get; } = typeof(T);
}
