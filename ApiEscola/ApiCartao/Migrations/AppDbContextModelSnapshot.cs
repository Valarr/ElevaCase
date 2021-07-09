﻿// <auto-generated />
using ApiEscola.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApiEscola.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApiEscola.Models.Escola", b =>
                {
                    b.Property<int>("escolaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("enderecoEscola")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("nomeEscola")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("telefoneEscola")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.HasKey("escolaId");

                    b.ToTable("Escola");
                });
#pragma warning restore 612, 618
        }
    }
}
