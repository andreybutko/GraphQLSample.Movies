using GraphQLSample.Movies.Common;
using GraphQL.Resolvers;
using GraphQL.Types;
using GraphQLSample.Movies.Schemas.DataAccess;
using GraphQLSample.Movies.Schemas.Models;
using GraphQLSample.Movies.Schemas.Types;

namespace GraphQLSample.Movies.Schemas
{
  [InjectableGraphQLType]
  public class Subscription : ObjectGraphType<object>
  {
    public Subscription(IMovieRepository movieRepository)
    {
      this.Name = nameof(Subscription);

      this.AddField(new EventStreamFieldType
      {
        Name = "onMovieCreate",
        Description = "Subscribe on movie creation",
        Type = typeof(MovieCreatedEvent),
        Resolver = new FuncFieldResolver<Movie>(context => context.Source as Movie),
        Subscriber = new EventStreamResolver<Movie>(context => movieRepository.WhenMovieCreated)
      });
    }
  }
}
