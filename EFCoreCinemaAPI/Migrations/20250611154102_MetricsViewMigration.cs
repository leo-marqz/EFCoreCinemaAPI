using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreCinemaAPI.Migrations
{
    /// <inheritdoc />
    public partial class MetricsViewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                                 create view [dbo].[Metrics]
                                    as
                                    select Id, Title, 
	                                      (select count(*) from GenreMovie where MoviesId = Movies.Id) as TotalGenres,
	                                      (select count(distinct CineId) from CineRoomMovie 
		                                    inner join CineRooms on CineRooms.Id = CineRoomMovie.CineRoomsId where MoviesId = Movies.Id) as TotalCines,
	                                    (select count(*) from MoviesActors where MovieId = Movies.Id) as TotalActors
	                                    from Movies

                                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW [dbo].[Metrics]");
        }
    }
}
