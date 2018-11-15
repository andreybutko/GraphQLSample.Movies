using System;
using System.Collections.Generic;

namespace GraphQLSample.Movies.Schemas.Models
{
  public class Movie
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public Genre Genre { get; set; }
    public List<Actor> Actors { get; set; } = new List<Actor>();
  }

  public enum Genre
  {
    HORROR,
    FANTASY,
    ACTION,
    SCIENCE_FICTION
  }

  public class Actor
  {
    public string Name { get; set; }
    public List<Movie> Movies { get; set; } = new List<Movie>();
  }
}