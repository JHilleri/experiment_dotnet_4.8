using System.Web.Http;

namespace todo.rest_4_8;

public static class WebApiConfig
{
    public static void Register(HttpConfiguration config)
    {
        // Configuration et services de l'API Web
        // Configuration et services de l'API Web

        // Itinéraires de l'API Web
        config.MapHttpAttributeRoutes();

        config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
        );

        DependencyResolverWithServiceProvider dependencyResolver = new(DependenciesConfig.GetResolver());

        config.DependencyResolver = dependencyResolver;
    }
}
