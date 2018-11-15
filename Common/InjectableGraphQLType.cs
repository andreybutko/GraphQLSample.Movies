using System;

namespace GraphQLSample.Movies.Common
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
  public class InjectableGraphQLType : Attribute { }
}
