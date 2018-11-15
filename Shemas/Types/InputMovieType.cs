using GraphQLSample.Movies.Common;
using GraphQL.Types;
using GraphQLSample.Movies.Schemas.Models;

namespace GraphQLSample.Movies.Schemas.Types
{
  [InjectableGraphQLType]
  public class InputMovieType : InputObjectGraphType
  {
    public InputMovieType()
    {
      this.Name = "InputMovie";

      this.Field<StringGraphType>("name");
      this.Field<StringGraphType>("description");
      this.Field<EnumerationGraphType<Genre>>("genre");
    }
  }
}
