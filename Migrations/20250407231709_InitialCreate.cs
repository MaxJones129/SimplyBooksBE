using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SimplyBooksBE1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    Favorite = table.Column<bool>(type: "boolean", nullable: false),
                    Uid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<int>(type: "integer", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Sale = table.Column<bool>(type: "boolean", nullable: false),
                    Uid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Email", "Favorite", "FirstName", "Image", "LastName", "Uid" },
                values: new object[,]
                {
                    { 1, "charlotte.perkins@example.com", true, "Charlotte", "charlotte.jpg", "Perkins Gilman", "UID001" },
                    { 2, "langston.hughes@example.com", false, "Langston", "langston.jpg", "Hughes", "UID002" },
                    { 3, "zora.hurston@example.com", true, "Zora", "zora.jpg", "Neale Hurston", "UID003" },
                    { 4, "james.baldwin@example.com", false, "James", "james.jpg", "Baldwin", "UID004" },
                    { 5, "toni.morrison@example.com", true, "Toni", "toni.jpg", "Morrison", "UID005" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Description", "Image", "Price", "Sale", "Title", "Uid" },
                values: new object[,]
                {
                    { 1, 1, "A pioneering feminist work by Charlotte Perkins Gilman examining the economic independence of women.", "women_economics.jpg", 13.50m, false, "Women and Economics", "BID001" },
                    { 2, 4, "A short story collection by Daphne Du Maurier exploring psychological tension and mystery.", "breaking_point.jpg", 9.25m, true, "The Breaking Point", "BID002" },
                    { 3, 3, "A reflective novel by Jacqueline Harpman about a woman's inner world and the meaning of solitude.", "mistress_silence.jpg", 10.99m, false, "The Mistress of Silence", "BID003" },
                    { 4, 2, "A haunting tale by Ling Ling Huang about a concert pianist whose emotional unraveling blurs the lines between art and grief.", "ghost_music.jpg", 13.75m, true, "Ghost Music", "BID004" },
                    { 5, 5, "A collection of short stories by N.K. Jemisin blending speculative fiction with social commentary.", "black_future_month.jpg", 11.50m, false, "How Long 'Til Black Future Month?", "BID005" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
