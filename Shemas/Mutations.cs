using GraphQL.Types;
using GraphQLSample.Movies.Schemas.DataAccess;
using GraphQLSample.Movies.Schemas.Models;
using GraphQLSample.Movies.Schemas.Types;

namespace GraphQLSample.Movies.Schemas
{
  public class Mutations : ObjectGraphType
  {
    public Mutations(IMovieRepository movieRepository)
    {
      this.Name = nameof(Mutations);

      this.Field<MovieType, Movie>("createMovie")
          .Argument<InputMovieType>("movie", "movie to create")
          .Resolve(x =>
          {
            var movie = x.GetArgument<Movie>("movie");
            return movieRepository.AddMovie(movie);
          });
    }
  }
}
