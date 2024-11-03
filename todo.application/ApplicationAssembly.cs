using System.Reflection;

namespace todo.application;

public static class ApplicationAssembly
{
    public static Assembly Assembly => typeof(ApplicationAssembly).Assembly;
}
