﻿// <auto-generated />
using System;
using LibrarySystemModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibrarySystemModel.Migrations
{
    [DbContext(typeof(LibraryDatabaseContext))]
    [Migration("20240214105024_BorrowedBook")]
    partial class BorrowedBook
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookAuthor", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("BookID");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int")
                        .HasColumnName("AuthorID");

                    b.HasKey("BookId", "AuthorId")
                        .HasName("PK__BookAuth__6AED6DE60403FAC3");

                    b.HasIndex("AuthorId");

                    b.ToTable("BookAuthor", (string)null);
                });

            modelBuilder.Entity("LibrarianReaderRelationship", b =>
                {
                    b.Property<int>("LibrarianId")
                        .HasColumnType("int")
                        .HasColumnName("LibrarianID");

                    b.Property<int>("ReaderId")
                        .HasColumnType("int")
                        .HasColumnName("ReaderID");

                    b.HasKey("LibrarianId", "ReaderId")
                        .HasName("PK__Libraria__EC3E17C532CE269D");

                    b.HasIndex("ReaderId");

                    b.ToTable("LibrarianReaderRelationship", (string)null);
                });

            modelBuilder.Entity("LibrarySystemModel.Author", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SecondName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id")
                        .HasName("PK__Author__3214EC278299F94B");

                    b.ToTable("Author", (string)null);
                });

            modelBuilder.Entity("LibrarySystemModel.Book", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    b.Property<string>("City")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Country")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PublisherCode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("PublishingTypeId")
                        .HasColumnType("int")
                        .HasColumnName("PublishingTypeID");

                    b.Property<int?>("ReaderId")
                        .HasColumnType("int");

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Book__3214EC27CE55E00A");

                    b.HasIndex("PublishingTypeId");

                    b.HasIndex("ReaderId");

                    b.ToTable("Book", (string)null);
                });

            modelBuilder.Entity("LibrarySystemModel.BorrowedBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<DateTime>("BorrowDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReaderId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("ReaderId");

                    b.ToTable("BorrowedBooks");
                });

            modelBuilder.Entity("LibrarySystemModel.DocumentType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id")
                        .HasName("PK__Document__3214EC27A0625AC7");

                    b.ToTable("DocumentType", (string)null);
                });

            modelBuilder.Entity("LibrarySystemModel.Librarian", b =>
                {
                    b.Property<int>("LibrarianId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("LibrarianID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LibrarianId"));

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Login")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("LibrarianId")
                        .HasName("PK__Libraria__E4D86D9D32825BB8");

                    b.ToTable("Librarian", (string)null);
                });

            modelBuilder.Entity("LibrarySystemModel.PublishingType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id")
                        .HasName("PK__Publishi__3214EC270C571612");

                    b.ToTable("PublishingType", (string)null);
                });

            modelBuilder.Entity("LibrarySystemModel.Reader", b =>
                {
                    b.Property<int>("ReaderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ReaderID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReaderId"));

                    b.Property<int?>("DocumentTypeId")
                        .HasColumnType("int")
                        .HasColumnName("DocumentTypeID");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Login")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NumDoc")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ReaderId")
                        .HasName("PK__Reader__8E67A581B7993CEE");

                    b.HasIndex("DocumentTypeId");

                    b.ToTable("Reader", (string)null);
                });

            modelBuilder.Entity("BookAuthor", b =>
                {
                    b.HasOne("LibrarySystemModel.Author", null)
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .IsRequired()
                        .HasConstraintName("FK__BookAutho__Autho__49C3F6B7");

                    b.HasOne("LibrarySystemModel.Book", null)
                        .WithMany()
                        .HasForeignKey("BookId")
                        .IsRequired()
                        .HasConstraintName("FK__BookAutho__BookI__48CFD27E");
                });

            modelBuilder.Entity("LibrarianReaderRelationship", b =>
                {
                    b.HasOne("LibrarySystemModel.Librarian", null)
                        .WithMany()
                        .HasForeignKey("LibrarianId")
                        .IsRequired()
                        .HasConstraintName("FK__Librarian__Libra__403A8C7D");

                    b.HasOne("LibrarySystemModel.Reader", null)
                        .WithMany()
                        .HasForeignKey("ReaderId")
                        .IsRequired()
                        .HasConstraintName("FK__Librarian__Reade__412EB0B6");
                });

            modelBuilder.Entity("LibrarySystemModel.Book", b =>
                {
                    b.HasOne("LibrarySystemModel.PublishingType", "PublishingType")
                        .WithMany("Books")
                        .HasForeignKey("PublishingTypeId")
                        .HasConstraintName("FK__Book__Publishing__440B1D61");

                    b.HasOne("LibrarySystemModel.Reader", null)
                        .WithMany("Books")
                        .HasForeignKey("ReaderId");

                    b.Navigation("PublishingType");
                });

            modelBuilder.Entity("LibrarySystemModel.BorrowedBook", b =>
                {
                    b.HasOne("LibrarySystemModel.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibrarySystemModel.Reader", "Reader")
                        .WithMany("BorrowedBooks")
                        .HasForeignKey("ReaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Reader");
                });

            modelBuilder.Entity("LibrarySystemModel.Reader", b =>
                {
                    b.HasOne("LibrarySystemModel.DocumentType", "DocumentType")
                        .WithMany("Readers")
                        .HasForeignKey("DocumentTypeId")
                        .HasConstraintName("FK__Reader__Document__3D5E1FD2");

                    b.Navigation("DocumentType");
                });

            modelBuilder.Entity("LibrarySystemModel.DocumentType", b =>
                {
                    b.Navigation("Readers");
                });

            modelBuilder.Entity("LibrarySystemModel.PublishingType", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("LibrarySystemModel.Reader", b =>
                {
                    b.Navigation("Books");

                    b.Navigation("BorrowedBooks");
                });
#pragma warning restore 612, 618
        }
    }
}