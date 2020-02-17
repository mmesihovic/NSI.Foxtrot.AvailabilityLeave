using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace NSI.DAL.Migrations
{
    public partial class Changed_Availibaility_rules_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResourceAvailabilityRule",
                schema: "availabilityleave");

            migrationBuilder.AddColumn<long>(
                name: "ResourceID",
                schema: "availabilityleave",
                table: "AvailabilityRule",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResourceID",
                schema: "availabilityleave",
                table: "AvailabilityRule");

            migrationBuilder.CreateTable(
                name: "ResourceAvailabilityRule",
                schema: "availabilityleave",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AvailabilityRuleID = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ResourceID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceAvailabilityRule", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ResourceAvailabilityRule_AvailabilityRule_AvailabilityRuleID",
                        column: x => x.AvailabilityRuleID,
                        principalSchema: "availabilityleave",
                        principalTable: "AvailabilityRule",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceAvailabilityRule_AvailabilityRuleID",
                schema: "availabilityleave",
                table: "ResourceAvailabilityRule",
                column: "AvailabilityRuleID");
        }
    }
}
