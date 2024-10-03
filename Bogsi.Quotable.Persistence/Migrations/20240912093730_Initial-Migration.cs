using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bogsi.Quotable.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Quotable");

            migrationBuilder.CreateTable(
                name: "Quotes",
                schema: "Quotable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "character varying(1255)", maxLength: 1255, nullable: false),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", rowVersion: true, nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.Id);
                });

            //migrationBuilder.InsertData(
            //    schema: "Quotable",
            //    table: "Quotes",
            //    columns: new[] { "Id", "PublicId", "Value" },
            //    values: new object[,]
            //    {
            //        { 1, new Guid("3414eee0-355b-4cda-acb6-567212236bce"), "Ph'nglui mglw'nafh Cthulhu R'lyeh wgah'nagl fhtagn." },
            //        { 2, new Guid("64a140bd-bdfa-46e8-8cbf-169703626004"), "That is not dead which can eternal lie, And with strange aeons even death may die." }
            //    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quotes",
                schema: "Quotable");
        }
    }
}
