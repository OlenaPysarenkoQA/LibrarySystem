using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystemModel.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReaderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SecondName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Author__3214EC278299F94B", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DocumentType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Document__3214EC27A0625AC7", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Librarian",
                columns: table => new
                {
                    LibrarianID = table.Column<int>(type: "int", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Libraria__E4D86D9D32825BB8", x => x.LibrarianID);
                });

            migrationBuilder.CreateTable(
                name: "PublishingType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Publishi__3214EC270C571612", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Reader",
                columns: table => new
                {
                    ReaderID = table.Column<int>(type: "int", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DocumentTypeID = table.Column<int>(type: "int", nullable: true),
                    NumDoc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reader__8E67A581B7993CEE", x => x.ReaderID);
                    table.ForeignKey(
                        name: "FK__Reader__Document__3D5E1FD2",
                        column: x => x.DocumentTypeID,
                        principalTable: "DocumentType",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PublisherCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PublishingTypeID = table.Column<int>(type: "int", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Book__3214EC27CE55E00A", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Book__Publishing__440B1D61",
                        column: x => x.PublishingTypeID,
                        principalTable: "PublishingType",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "LibrarianReaderRelationship",
                columns: table => new
                {
                    LibrarianID = table.Column<int>(type: "int", nullable: false),
                    ReaderID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Libraria__EC3E17C532CE269D", x => new { x.LibrarianID, x.ReaderID });
                    table.ForeignKey(
                        name: "FK__Librarian__Libra__403A8C7D",
                        column: x => x.LibrarianID,
                        principalTable: "Librarian",
                        principalColumn: "LibrarianID");
                    table.ForeignKey(
                        name: "FK__Librarian__Reade__412EB0B6",
                        column: x => x.ReaderID,
                        principalTable: "Reader",
                        principalColumn: "ReaderID");
                });

            migrationBuilder.CreateTable(
                name: "BookAuthor",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false),
                    AuthorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookAuth__6AED6DE60403FAC3", x => new { x.BookID, x.AuthorID });
                    table.ForeignKey(
                        name: "FK__BookAutho__Autho__49C3F6B7",
                        column: x => x.AuthorID,
                        principalTable: "Author",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__BookAutho__BookI__48CFD27E",
                        column: x => x.BookID,
                        principalTable: "Book",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_PublishingTypeID",
                table: "Book",
                column: "PublishingTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthor_AuthorID",
                table: "BookAuthor",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_LibrarianReaderRelationship_ReaderID",
                table: "LibrarianReaderRelationship",
                column: "ReaderID");

            migrationBuilder.CreateIndex(
                name: "IX_Reader_DocumentTypeID",
                table: "Reader",
                column: "DocumentTypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthor");

            migrationBuilder.DropTable(
                name: "LibrarianReaderRelationship");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Librarian");

            migrationBuilder.DropTable(
                name: "Reader");

            migrationBuilder.DropTable(
                name: "PublishingType");

            migrationBuilder.DropTable(
                name: "DocumentType");
        }
    }
}
