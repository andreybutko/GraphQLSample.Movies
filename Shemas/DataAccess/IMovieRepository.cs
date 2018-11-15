using GraphQLSample.Movies.Schemas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace GraphQLSample.Movies.Schemas.DataAccess
{
  public interface IMovieRepository
  {
    List<Movie> GetMovies();
    Movie GetMovie(string name);
    Movie AddMovie(Movie movie);
    List<Actor> GetActors();

    IObservable<Movie> WhenMovieCreated { get; }
  }

  public class MovieRepository : IMovieRepository
  {
    private readonly Subject<Movie> whenMovieCreated = new Subject<Movie>();

    public IObservable<Movie> WhenMovieCreated => 
      whenMovieCreated.AsObservable();

    public Movie GetMovie(string name) => 
      MoviesContainer.Movies.FirstOrDefault(x => x.Name == name);

    public List<Movie> GetMovies() => 
      MoviesContainer.Movies;

    public List<Actor> GetActors() => 
      MoviesContainer.Movies
        .Where(x => x.Actors != null)
        .SelectMany(x => x.Actors)
        .Distinct()
        .ToList();

    public Movie AddMovie(Movie movie)
    {
      movie.Id = Guid.NewGuid();
      MoviesContainer.Movies.Add(movie);
      whenMovieCreated.OnNext(movie);
      return movie;
    }
  }
}