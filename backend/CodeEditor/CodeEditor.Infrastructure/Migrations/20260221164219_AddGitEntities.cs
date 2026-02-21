using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CodeEditor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGitEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "documents");

            migrationBuilder.CreateTable(
                name: "git_repos",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_git_feed = table.Column<long>(type: "bigint", nullable: false),
                    owner_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    last_update_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_git_repos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "git_feeds",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_git_repo = table.Column<long>(type: "bigint", nullable: false),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    last_update_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_git_feeds", x => x.id);
                    table.ForeignKey(
                        name: "FK_git_feeds_git_repos_id_git_repo",
                        column: x => x.id_git_repo,
                        principalTable: "git_repos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "git_feed_entries",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    git_feed_id = table.Column<long>(type: "bigint", nullable: false),
                    id_tag = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    last_update_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    link = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    author_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_git_feed_entries", x => x.id);
                    table.ForeignKey(
                        name: "FK_git_feed_entries_git_feeds_git_feed_id",
                        column: x => x.git_feed_id,
                        principalTable: "git_feeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tokens_user_id",
                table: "tokens",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_git_feed_entries_git_feed_id",
                table: "git_feed_entries",
                column: "git_feed_id");

            migrationBuilder.CreateIndex(
                name: "IX_git_feeds_id_git_repo",
                table: "git_feeds",
                column: "id_git_repo",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tokens_users_user_id",
                table: "tokens",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tokens_users_user_id",
                table: "tokens");

            migrationBuilder.DropTable(
                name: "git_feed_entries");

            migrationBuilder.DropTable(
                name: "git_feeds");

            migrationBuilder.DropTable(
                name: "git_repos");

            migrationBuilder.DropIndex(
                name: "IX_tokens_user_id",
                table: "tokens");

            migrationBuilder.CreateTable(
                name: "documents",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    path = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_documents", x => x.id);
                });
        }
    }
}
