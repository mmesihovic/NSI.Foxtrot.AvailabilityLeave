using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace NSI.DAL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "availabilityleave");

            migrationBuilder.CreateTable(
                name: "AvailabilityRule",
                schema: "availabilityleave",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    WeekDay = table.Column<string>(nullable: false),
                    FromTime = table.Column<DateTime>(nullable: false),
                    ToTime = table.Column<DateTime>(nullable: false),
                    Available = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailabilityRule", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EventType",
                schema: "availabilityleave",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventType", x => x.ID);
                    table.UniqueConstraint("AK_EventType_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "LeaveBalance",
                schema: "availabilityleave",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ResourceID = table.Column<long>(nullable: false),
                    AvailableDays = table.Column<long>(nullable: false),
                    MonthlyGain = table.Column<long>(nullable: false),
                    YearlyGain = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveBalance", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ResourceAvailabilityRule",
                schema: "availabilityleave",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    ResourceID = table.Column<long>(nullable: false),
                    AvailabilityRuleID = table.Column<long>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Event",
                schema: "availabilityleave",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    EventType = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.ID);
                    table.UniqueConstraint("AK_Event_Name", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Event_EventType_EventType",
                        column: x => x.EventType,
                        principalSchema: "availabilityleave",
                        principalTable: "EventType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeaveTransaction",
                schema: "availabilityleave",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    Days = table.Column<long>(nullable: false),
                    LeaveBalanceID = table.Column<long>(nullable: true),
                    ApprovedByID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveTransaction", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LeaveTransaction_LeaveBalance_LeaveBalanceID",
                        column: x => x.LeaveBalanceID,
                        principalSchema: "availabilityleave",
                        principalTable: "LeaveBalance",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResourceSchedule",
                schema: "availabilityleave",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    ResourceID = table.Column<long>(nullable: false),
                    Approved = table.Column<bool>(nullable: false),
                    ApprovedByID = table.Column<long>(nullable: false),
                    RequestByID = table.Column<long>(nullable: false),
                    EventID = table.Column<long>(nullable: false),
                    FromDate = table.Column<DateTime>(nullable: false),
                    UntilDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceSchedule", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ResourceSchedule_Event_EventID",
                        column: x => x.EventID,
                        principalSchema: "availabilityleave",
                        principalTable: "Event",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "availabilityleave",
                table: "EventType",
                columns: new[] { "ID", "DateCreated", "DateModified", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "ABSENCE" },
                    { 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "LEAVE" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_EventType",
                schema: "availabilityleave",
                table: "Event",
                column: "EventType");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveTransaction_LeaveBalanceID",
                schema: "availabilityleave",
                table: "LeaveTransaction",
                column: "LeaveBalanceID");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceAvailabilityRule_AvailabilityRuleID",
                schema: "availabilityleave",
                table: "ResourceAvailabilityRule",
                column: "AvailabilityRuleID");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceSchedule_EventID",
                schema: "availabilityleave",
                table: "ResourceSchedule",
                column: "EventID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveTransaction",
                schema: "availabilityleave");

            migrationBuilder.DropTable(
                name: "ResourceAvailabilityRule",
                schema: "availabilityleave");

            migrationBuilder.DropTable(
                name: "ResourceSchedule",
                schema: "availabilityleave");

            migrationBuilder.DropTable(
                name: "LeaveBalance",
                schema: "availabilityleave");

            migrationBuilder.DropTable(
                name: "AvailabilityRule",
                schema: "availabilityleave");

            migrationBuilder.DropTable(
                name: "Event",
                schema: "availabilityleave");

            migrationBuilder.DropTable(
                name: "EventType",
                schema: "availabilityleave");
        }
    }
}
