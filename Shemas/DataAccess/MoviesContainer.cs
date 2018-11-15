using GraphQLSample.Movies.Schemas.Models;
using System;
using System.Collections.Generic;

namespace GraphQLSample.Movies.Schemas.DataAccess
{
  public static class MoviesContainer
  {
    public static List<Movie> Movies = new List<Movie>();

    static MoviesContainer()
    {
      var martian = new Movie
      {
        Name = "Kartofanin",
        Description = @"
          When astronauts blast off from the planet Mars, 
          they leave behind Mark Watney (Matt Damon), 
          presumed dead after a fierce storm",
        Genre = Genre.SCIENCE_FICTION,
        ReleaseDate = new DateTime(2015, 1, 1),
        Actors = new List<Actor>
        {
          new Actor { Name = "Matt Damon" },
          new Actor { Name = "Jessica Chastain" },
          new Actor { Name = "Kristen Wiig" },
        }
      };
      foreach(var actor in martian.Actors)
      {
        actor.Movies.Add(martian);
      }
      Movies.Add(martian);
    }
  }
}