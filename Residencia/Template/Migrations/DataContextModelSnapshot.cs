﻿// <auto-generated />
using Exemplo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Template.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("Template.Servicos.Residencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Morador")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NomeLocal")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TaxaCondominio")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("residencias");
                });
#pragma warning restore 612, 618
        }
    }
}
