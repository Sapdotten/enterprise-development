using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class ChangePostgresFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanRecords_Books_BookId",
                table: "LoanRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanRecords_Readers_ReaderId",
                table: "LoanRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Readers",
                table: "Readers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoanRecords",
                table: "LoanRecords");

            migrationBuilder.RenameTable(
                name: "Readers",
                newName: "readers");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "books");

            migrationBuilder.RenameTable(
                name: "LoanRecords",
                newName: "loan_records");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "readers",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "readers",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "RegistrationDate",
                table: "readers",
                newName: "registration_date");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "readers",
                newName: "phone_number");

            migrationBuilder.RenameColumn(
                name: "PatronymicName",
                table: "readers",
                newName: "patronymic_name");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "readers",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "readers",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "books",
                newName: "year");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "books",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Publisher",
                table: "books",
                newName: "publisher");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "books",
                newName: "code");

            migrationBuilder.RenameColumn(
                name: "Authors",
                table: "books",
                newName: "authors");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "books",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PublisherType",
                table: "books",
                newName: "publisher_type");

            migrationBuilder.RenameColumn(
                name: "InventoryNumber",
                table: "books",
                newName: "inventory_number");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "loan_records",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ReaderId",
                table: "loan_records",
                newName: "reader_id");

            migrationBuilder.RenameColumn(
                name: "LoanTerm",
                table: "loan_records",
                newName: "loan_term");

            migrationBuilder.RenameColumn(
                name: "IssueDate",
                table: "loan_records",
                newName: "issue_date");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "loan_records",
                newName: "book_id");

            migrationBuilder.RenameIndex(
                name: "IX_LoanRecords_ReaderId",
                table: "loan_records",
                newName: "IX_loan_records_reader_id");

            migrationBuilder.RenameIndex(
                name: "IX_LoanRecords_BookId",
                table: "loan_records",
                newName: "IX_loan_records_book_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_readers",
                table: "readers",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_books",
                table: "books",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_loan_records",
                table: "loan_records",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_loan_records_books_book_id",
                table: "loan_records",
                column: "book_id",
                principalTable: "books",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_loan_records_readers_reader_id",
                table: "loan_records",
                column: "reader_id",
                principalTable: "readers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_loan_records_books_book_id",
                table: "loan_records");

            migrationBuilder.DropForeignKey(
                name: "FK_loan_records_readers_reader_id",
                table: "loan_records");

            migrationBuilder.DropPrimaryKey(
                name: "PK_readers",
                table: "readers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_books",
                table: "books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_loan_records",
                table: "loan_records");

            migrationBuilder.RenameTable(
                name: "readers",
                newName: "Readers");

            migrationBuilder.RenameTable(
                name: "books",
                newName: "Books");

            migrationBuilder.RenameTable(
                name: "loan_records",
                newName: "LoanRecords");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Readers",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Readers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "registration_date",
                table: "Readers",
                newName: "RegistrationDate");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "Readers",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "patronymic_name",
                table: "Readers",
                newName: "PatronymicName");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "Readers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "Readers",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "year",
                table: "Books",
                newName: "Year");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Books",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "publisher",
                table: "Books",
                newName: "Publisher");

            migrationBuilder.RenameColumn(
                name: "code",
                table: "Books",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "authors",
                table: "Books",
                newName: "Authors");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Books",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "publisher_type",
                table: "Books",
                newName: "PublisherType");

            migrationBuilder.RenameColumn(
                name: "inventory_number",
                table: "Books",
                newName: "InventoryNumber");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "LoanRecords",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "reader_id",
                table: "LoanRecords",
                newName: "ReaderId");

            migrationBuilder.RenameColumn(
                name: "loan_term",
                table: "LoanRecords",
                newName: "LoanTerm");

            migrationBuilder.RenameColumn(
                name: "issue_date",
                table: "LoanRecords",
                newName: "IssueDate");

            migrationBuilder.RenameColumn(
                name: "book_id",
                table: "LoanRecords",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_loan_records_reader_id",
                table: "LoanRecords",
                newName: "IX_LoanRecords_ReaderId");

            migrationBuilder.RenameIndex(
                name: "IX_loan_records_book_id",
                table: "LoanRecords",
                newName: "IX_LoanRecords_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Readers",
                table: "Readers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoanRecords",
                table: "LoanRecords",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanRecords_Books_BookId",
                table: "LoanRecords",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanRecords_Readers_ReaderId",
                table: "LoanRecords",
                column: "ReaderId",
                principalTable: "Readers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
