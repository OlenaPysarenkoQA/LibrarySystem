using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystemModel;

public partial class LibraryDatabaseContext : DbContext
{
    public LibraryDatabaseContext()
    {
    }

    public LibraryDatabaseContext(DbContextOptions<LibraryDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<DocumentType> DocumentTypes { get; set; }

    public virtual DbSet<Librarian> Librarians { get; set; }

    public virtual DbSet<PublishingType> PublishingTypes { get; set; }

    public virtual DbSet<Reader> Readers { get; set; }

    public virtual DbSet<BorrowedBook> BorrowedBooks { get; set; } 

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-M2NQCIO\\SQLEXPRESS;Initial Catalog=LibraryDatabase;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Author__3214EC278299F94B");

            entity.ToTable("Author");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.SecondName).HasMaxLength(50);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Book__3214EC27CE55E00A");

            entity.ToTable("Book");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PublisherCode).HasMaxLength(50);
            entity.Property(e => e.PublishingTypeId).HasColumnName("PublishingTypeID");

            entity.HasOne(d => d.PublishingType).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublishingTypeId)
                .HasConstraintName("FK__Book__Publishing__440B1D61");

            entity.HasMany(d => d.Authors).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookAuthor",
                    r => r.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__BookAutho__Autho__49C3F6B7"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__BookAutho__BookI__48CFD27E"),
                    j =>
                    {
                        j.HasKey("BookId", "AuthorId").HasName("PK__BookAuth__6AED6DE60403FAC3");
                        j.ToTable("BookAuthor");
                        j.IndexerProperty<int>("BookId").HasColumnName("BookID");
                        j.IndexerProperty<int>("AuthorId").HasColumnName("AuthorID");
                    });
        });

        modelBuilder.Entity<DocumentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Document__3214EC27A0625AC7");

            entity.ToTable("DocumentType");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Librarian>(entity =>
        {
            entity.HasKey(e => e.LibrarianId).HasName("PK__Libraria__E4D86D9D32825BB8");

            entity.ToTable("Librarian");

            entity.Property(e => e.LibrarianId)
                  .HasColumnName("LibrarianID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);

            entity.HasMany(d => d.Readers).WithMany(p => p.Librarians)
                .UsingEntity<Dictionary<string, object>>(
                    "LibrarianReaderRelationship",
                    r => r.HasOne<Reader>().WithMany()
                        .HasForeignKey("ReaderId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Librarian__Reade__412EB0B6"),
                    l => l.HasOne<Librarian>().WithMany()
                        .HasForeignKey("LibrarianId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Librarian__Libra__403A8C7D"),
                    j =>
                    {
                        j.HasKey("LibrarianId", "ReaderId").HasName("PK__Libraria__EC3E17C532CE269D");
                        j.ToTable("LibrarianReaderRelationship");
                        j.IndexerProperty<int>("LibrarianId").HasColumnName("LibrarianID");
                        j.IndexerProperty<int>("ReaderId").HasColumnName("ReaderID");
                    });
        });

        modelBuilder.Entity<PublishingType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Publishi__3214EC270C571612");

            entity.ToTable("PublishingType");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Reader>(entity =>
        {
            entity.HasKey(e => e.ReaderId).HasName("PK__Reader__8E67A581B7993CEE");

            entity.ToTable("Reader");

            entity.Property(e => e.ReaderId)
                  .HasColumnName("ReaderID");
            entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.NumDoc).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);

            entity.HasOne(d => d.DocumentType).WithMany(p => p.Readers)
                .HasForeignKey(d => d.DocumentTypeId)
                .HasConstraintName("FK__Reader__Document__3D5E1FD2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public bool Login(string login, string password)
    {
        var librarian = Librarians.FirstOrDefault(l => l.Login == login && l.Password == password);

        return librarian != null;
    }

    public bool RegisterLibrarian(string login, string password, string email)
    {
        var existingLibrarian = Librarians.FirstOrDefault(l => l.Login == login);

        if (existingLibrarian != null)
            return false;

        var newLibrarian = new Librarian
        {
            Login = login,
            Password = password,
            Email = email
        };
        Librarians.Add(newLibrarian);
        SaveChanges();

        return true;
    }

    public bool RegisterReader(string login, string password, string email, string firstName, string lastName, int documentTypeId, string numDoc)
    {
        using (var context = new LibraryDatabaseContext())
        {
            var newReader = new Reader
            {
                Login = login,
                Password = password,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                DocumentTypeId = documentTypeId,
                NumDoc = numDoc
            };

            context.Readers.Add(newReader);
            context.SaveChanges();

            return true;
        }
    }
}
