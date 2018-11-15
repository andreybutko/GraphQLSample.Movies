using GraphQLSample.Movies.Common;
using GraphQL.Types;
using GraphQLSample.Movies.Schemas.DataAccess;
using GraphQLSample.Movies.Schemas.Types;

namespace GraphQLSample.Movies.Schemas
{
  [InjectableGraphQLType]
  public class Query : ObjectGraphType
  {
    public Query(IMovieRepository movieRepository)
    {
      this.Name = nameof(Query);

      this.Field<MovieType>(name: "movie",
          description: "Get details by movie name",
          arguments: new QueryArguments(
            new QueryArgument<StringGraphType> { Name = "name" }),
          resolve: ctx => movieRepository.GetMovie(ctx.GetArgument<string>("name")));

      this.Field<ListGraphType<MovieType>>(name: "movies",
          description: "Get list movies",
          resolve: ctx => movieRepository.GetMovies());

      this.Field<ListGraphType<ActorType>>(name: "actors",
          description: "Get list actors",
          resolve: ctx => movieRepository.GetActors());
    }
  }
}
