using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreCinemaAPI.Migrations
{
    /// <inheritdoc />
    public partial class TableValueFunctionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE FUNCTION MetricsByMovieId(@movieId int)
                RETURNS TABLE
                AS
                RETURN (
                    SELECT Id, Title, 
                        (SELECT COUNT(*) FROM GenreMovie WHERE MoviesId = Movies.Id) AS TotalGenres,
                        (SELECT COUNT(DISTINCT CineId) FROM CineRoomMovie 
                         INNER JOIN CineRooms ON CineRooms.Id = CineRoomMovie.CineRoomsId WHERE MoviesId = Movies.Id) AS TotalCines,
                        (SELECT COUNT(*) FROM MoviesActors WHERE MovieId = Movies.Id) AS TotalActors
                    FROM Movies
                    WHERE Id = @movieId
                )
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION MetricsByMovieId");
        }
    }
}
