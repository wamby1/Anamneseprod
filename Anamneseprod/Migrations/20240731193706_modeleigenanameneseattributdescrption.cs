﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anamneseprod.Migrations
{
    /// <inheritdoc />
    public partial class modeleigenanameneseattributdescrption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Eigenanamnesen",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "Eigenanamnesen");
        }
    }
}
