using Microsoft.EntityFrameworkCore.Migrations;

namespace NSI.DAL.Migrations
{
    public partial class ApprovedByID_Nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ApprovedByID",
                schema: "availabilityleave",
                table: "ResourceSchedule",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ApprovedByID",
                schema: "availabilityleave",
                table: "ResourceSchedule",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
