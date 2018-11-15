using GraphQLSample.Movies.Common;
using GraphQL.Types;
using GraphQLSample.Movies.Schemas.Models;

namespace GraphQLSample.Movies.Schemas.Types
{
  [InjectableGraphQLType]
  public class MovieType : ObjectGraphType<Movie>
  {
    public MovieType()
    {
      this.Name = nameof(Movie);

      this.Field(x => x.Name).DeprecationReason("I don't know");
      this.Field(x => x.Description);
      this.Field(x => x.ReleaseDate, nullable: true);
      this.Field<GenreEnumerationType, Genre>(name: "genre").Resolve(x => x.Source.Genre);
    }
  }
}
