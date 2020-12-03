using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComedyEvents.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "comedians",
                columns: table => new
                {
                    ComedianID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comedians", x => x.ComedianID);
                });

            migrationBuilder.CreateTable(
                name: "venues",
                columns: table => new
                {
                    VenueID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VenueName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seating = table.Column<int>(type: "int", nullable: false),
                    ServesAlcohol = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_venues", x => x.VenueID);
                });

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VenueID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.EventID);
                    table.ForeignKey(
                        name: "FK_events_venues_VenueID",
                        column: x => x.VenueID,
                        principalTable: "venues",
                        principalColumn: "VenueID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "gigs",
                columns: table => new
                {
                    GigID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GigHeadline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GigLengthInMinutes = table.Column<int>(type: "int", nullable: false),
                    EventID = table.Column<int>(type: "int", nullable: true),
                    ComedianID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gigs", x => x.GigID);
                    table.ForeignKey(
                        name: "FK_gigs_comedians_ComedianID",
                        column: x => x.ComedianID,
                        principalTable: "comedians",
                        principalColumn: "ComedianID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_gigs_events_EventID",
                        column: x => x.EventID,
                        principalTable: "events",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "comedians",
                columns: new[] { "ComedianID", "ContactPhone", "FirstName", "LastName" },
                values: new object[] { 1, "112345332", "Pavol", "Almasi" });

            migrationBuilder.InsertData(
                table: "comedians",
                columns: new[] { "ComedianID", "ContactPhone", "FirstName", "LastName" },
                values: new object[] { 2, "442512748", "Robin", "William" });

            migrationBuilder.InsertData(
                table: "venues",
                columns: new[] { "VenueID", "City", "Seating", "ServesAlcohol", "State", "Street", "VenueName", "ZipCode" },
                values: new object[] { 1, "Wikes pron", 125, true, "PA", "123 Doa Street", "Mohegun Sun", "12382" });

            migrationBuilder.InsertData(
                table: "events",
                columns: new[] { "EventID", "EventDate", "EventName", "VenueID" },
                values: new object[] { 1, new DateTime(2019, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comedy Night", 1 });

            migrationBuilder.InsertData(
                table: "gigs",
                columns: new[] { "GigID", "ComedianID", "EventID", "GigHeadline", "GigLengthInMinutes" },
                values: new object[] { 2, 2, null, "Robin show", 45 });

            migrationBuilder.InsertData(
                table: "gigs",
                columns: new[] { "GigID", "ComedianID", "EventID", "GigHeadline", "GigLengthInMinutes" },
                values: new object[] { 1, 1, null, "Pavols show", 60 });

            migrationBuilder.CreateIndex(
                name: "IX_events_VenueID",
                table: "events",
                column: "VenueID");

            migrationBuilder.CreateIndex(
                name: "IX_gigs_ComedianID",
                table: "gigs",
                column: "ComedianID");

            migrationBuilder.CreateIndex(
                name: "IX_gigs_EventID",
                table: "gigs",
                column: "EventID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gigs");

            migrationBuilder.DropTable(
                name: "comedians");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "venues");
        }
    }
}
