﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmadeusG3_Neo_Tech_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class SeagregacampoavataramodeloUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Users");
        }
    }
}
