using GraphQL.Types;
using GraphQLSample.Movies.Schemas.Models;

namespace GraphQLSample.Movies.Schemas.Types
{
  public class GenreEnumerationType : EnumerationGraphType<Genre>
  {
    public GenreEnumerationType()
    {
      this.Name = nameof(Genre);
    }
  }
}
