using GraphQL;
using GraphQL.Types;

namespace GraphQLSample.Movies.Schemas
{
  public class MainSchema : Schema
  {
    public MainSchema(IDependencyResolver resolver)
    {
      this.Query = resolver.Resolve<Query>();
      this.Mutation = resolver.Resolve<Mutations>();
      this.Subscription = resolver.Resolve<Subscription>();
    }
  }
}
