﻿// <auto-generated />
using DataBaseCore.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SqlServer.Migrations
{
    [DbContext(typeof(EmployeeDBContext))]
    partial class EmployeeDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BasicPay")
                        .HasColumnType("int");

                    b.Property<int>("Bonus")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmpType")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HRA")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Employees");

                    b.HasDiscriminator<int>("EmpType");
                });

            modelBuilder.Entity("Core.Models.FullTimeEmployee", b =>
                {
                    b.HasBaseType("Core.Models.Employee");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Core.Models.PartTimeEmployee", b =>
                {
                    b.HasBaseType("Core.Models.Employee");

                    b.HasDiscriminator().HasValue(2);
                });
#pragma warning restore 612, 618
        }
    }
}
