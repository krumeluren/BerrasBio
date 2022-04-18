﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerrasBio.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountID);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    MovieID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Length = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.MovieID);
                });

            migrationBuilder.CreateTable(
                name: "Saloon",
                columns: table => new
                {
                    SaloonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Saloon", x => x.SaloonID);
                });

            migrationBuilder.CreateTable(
                name: "Row",
                columns: table => new
                {
                    RowID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaloonID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Row", x => x.RowID);
                    table.ForeignKey(
                        name: "FK_Row_Saloon_SaloonID",
                        column: x => x.SaloonID,
                        principalTable: "Saloon",
                        principalColumn: "SaloonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Show",
                columns: table => new
                {
                    ShowID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MovieID = table.Column<int>(type: "int", nullable: false),
                    SaloonID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Show", x => x.ShowID);
                    table.ForeignKey(
                        name: "FK_Show_Movie_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movie",
                        principalColumn: "MovieID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Show_Saloon_SaloonID",
                        column: x => x.SaloonID,
                        principalTable: "Saloon",
                        principalColumn: "SaloonID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Seat",
                columns: table => new
                {
                    SeatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seat", x => x.SeatID);
                    table.ForeignKey(
                        name: "FK_Seat_Row_RowID",
                        column: x => x.RowID,
                        principalTable: "Row",
                        principalColumn: "RowID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    ShowID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.BookingID);
                    table.ForeignKey(
                        name: "FK_Booking_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Booking_Show_ShowID",
                        column: x => x.ShowID,
                        principalTable: "Show",
                        principalColumn: "ShowID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookable_Seats",
                columns: table => new
                {
                    Bookable_SeatsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticket_Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShowID = table.Column<int>(type: "int", nullable: false),
                    SeatID = table.Column<int>(type: "int", nullable: false),
                    BookingID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookable_Seats", x => x.Bookable_SeatsID);
                    table.ForeignKey(
                        name: "FK_Bookable_Seats_Booking_BookingID",
                        column: x => x.BookingID,
                        principalTable: "Booking",
                        principalColumn: "BookingID");
                    table.ForeignKey(
                        name: "FK_Bookable_Seats_Seat_SeatID",
                        column: x => x.SeatID,
                        principalTable: "Seat",
                        principalColumn: "SeatID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookable_Seats_Show_ShowID",
                        column: x => x.ShowID,
                        principalTable: "Show",
                        principalColumn: "ShowID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookable_Seats_BookingID",
                table: "Bookable_Seats",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookable_Seats_SeatID",
                table: "Bookable_Seats",
                column: "SeatID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookable_Seats_ShowID",
                table: "Bookable_Seats",
                column: "ShowID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_AccountID",
                table: "Booking",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ShowID",
                table: "Booking",
                column: "ShowID");

            migrationBuilder.CreateIndex(
                name: "IX_Row_SaloonID",
                table: "Row",
                column: "SaloonID");

            migrationBuilder.CreateIndex(
                name: "IX_Seat_RowID",
                table: "Seat",
                column: "RowID");

            migrationBuilder.CreateIndex(
                name: "IX_Show_MovieID",
                table: "Show",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_Show_SaloonID",
                table: "Show",
                column: "SaloonID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookable_Seats");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "Seat");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Show");

            migrationBuilder.DropTable(
                name: "Row");

            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "Saloon");
        }
    }
}
