﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using jwks_csharp.Models;

#nullable disable

namespace jwks_csharp.Migrations
{
    [DbContext(typeof(PkiContext))]
    partial class PkiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("jwks_csharp.Models.Pki", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PrivateKey")
                        .HasColumnType("text")
                        .HasColumnName("private_key");

                    b.Property<string>("PublicKey")
                        .HasColumnType("text")
                        .HasColumnName("public_key");

                    b.HasKey("Id");

                    b.ToTable("pki");
                });
#pragma warning restore 612, 618
        }
    }
}
