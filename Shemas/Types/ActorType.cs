using GraphQLSample.Movies.Common;
using GraphQL.Types;
using System.Collections.Generic;
using GraphQLSample.Movies.Schemas.Models;

namespace GraphQLSample.Movies.Schemas.Types
{
  [InjectableGraphQLType]
  public class ActorType : ObjectGraphType<Actor>
  {
    public ActorType()
    {
      this.Name = nameof(Actor);

      this.Field(x => x.Name);
      this.Field<ListGraphType<MovieType>, List<Movie>>("appearsIn")
          .Resolve(x => x.Source.Movies);
    }
  }
}
