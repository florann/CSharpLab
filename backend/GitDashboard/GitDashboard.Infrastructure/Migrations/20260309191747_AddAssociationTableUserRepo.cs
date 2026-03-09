using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeEditor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAssociationTableUserRepo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_git_repo",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    git_repo_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_git_repo", x => new { x.user_id, x.git_repo_id });
                    table.ForeignKey(
                        name: "FK_user_git_repo_git_repos_git_repo_id",
                        column: x => x.git_repo_id,
                        principalTable: "git_repos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_git_repo_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_git_repo_git_repo_id",
                table: "user_git_repo",
                column: "git_repo_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_git_repo");
        }
    }
}
