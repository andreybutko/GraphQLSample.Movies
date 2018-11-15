using System.Linq;
using System.Reflection;
using GraphQLSample.Movies.Common;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.GraphiQL;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using GraphQLSample.Movies.Schemas;
using GraphQLSample.Movies.Middleware;
using GraphQLSample.Movies.Schemas.DataAccess;

namespace GraphQLSample.Movies
{
  public class Startup
  {
    private readonly string AllowAnyPolicy = "Any";

    public void ConfigureServices(IServiceCollection services)
    {
      RegisterServices(services);

      //add graphql services and configure options
      services.AddGraphQL(options =>
      {
        options.EnableMetrics = true;
        options.ExposeExceptions = true;
      })
          .AddWebSockets() // Add required services for web socket support
          .AddDataLoader(); // Add required services for DataLoader support
      services.AddCors(o => o.AddPolicy(AllowAnyPolicy, builder =>
      {
        builder.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader();
      }));
    }

    private static void RegisterServices(IServiceCollection services)
    {
      services.AddSingleton<IMovieRepository, MovieRepository>();

      services.AddSingleton<Query>();
      services.AddSingleton<Mutations>();
      services.AddSingleton<Subscription>();

      services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));

      var typesToInject = Assembly.GetExecutingAssembly()
          .GetTypes()
          .Where(x => x.GetCustomAttribute<InjectableGraphQLType>() != null)
          .ToList();

      services.AddSingleton<MainSchema>();

      foreach (var type in typesToInject)
      {
        services.AddSingleton(type);
      }
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseCors(AllowAnyPolicy);
      app.UseMiddleware<FileUploadMiddleware>();
      app.UseWebSockets();
      app.UseGraphQLWebSockets<MainSchema>();
      app.UseGraphQL<MainSchema>("/graphql");
      app.UseGraphQLPlayground(new GraphQLPlaygroundOptions()
      {
        Path = "/ui/playground"
      });
      app.UseGraphiQLServer(new GraphiQLOptions
      {
        GraphiQLPath = "/ui/graphiql",
        GraphQLEndPoint = "/graphql"
      });
    }
  }
}
