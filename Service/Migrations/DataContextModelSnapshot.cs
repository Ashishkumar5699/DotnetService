﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sonaar.Domain.DataContext;

#nullable disable

namespace Sonaar.Service.APi.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.8");

            modelBuilder.Entity("Sonaar.Domain.Entities.Contacts.ContactDetails", b =>
                {
                    b.Property<int>("ContactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AdharNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactAddress1")
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactAddress2")
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactCity")
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactFirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactLandMark")
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactLastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactPhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactPinCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactPrifix")
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactState")
                        .HasColumnType("TEXT");

                    b.Property<int>("ContactType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CustmorCountry")
                        .HasColumnType("TEXT");

                    b.Property<string>("CustmorGSTNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("CustmorZipCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("PanNumber")
                        .HasColumnType("TEXT");

                    b.HasKey("ContactId");

                    b.ToTable("ContactDetails");
                });

            modelBuilder.Entity("Sonaar.Domain.Entities.Product.ProductEntity", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("HSN_Code")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Making_Charge")
                        .HasColumnType("TEXT");

                    b.Property<string>("Purity")
                        .HasColumnType("TEXT");

                    b.Property<int>("QuotationId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Rate")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Weight")
                        .HasColumnType("TEXT");

                    b.HasKey("ProductId");

                    b.HasIndex("QuotationId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Sonaar.Domain.Entities.Quotations.GstAmountEntity", b =>
                {
                    b.Property<int>("GstAmountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("CGSt")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Discount")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("GrandTotal")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("IGST")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("SGST")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TotalAfterDiscount")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TotalBeforeDiscount")
                        .HasColumnType("TEXT");

                    b.HasKey("GstAmountId");

                    b.ToTable("GstAmountEntity");
                });

            modelBuilder.Entity("Sonaar.Domain.Entities.Quotations.Quotation", b =>
                {
                    b.Property<int>("QuotationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BillType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Billid")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ContactId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateofBill")
                        .HasColumnType("TEXT");

                    b.Property<int?>("GstAmountId")
                        .HasColumnType("INTEGER");

                    b.HasKey("QuotationId");

                    b.HasIndex("ContactId");

                    b.HasIndex("GstAmountId");

                    b.ToTable("Quotations");
                });

            modelBuilder.Entity("Sonaar.Entities.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("BLOB");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Sonaar.Entities.Purchase.PurchaseRequest", b =>
                {
                    b.Property<int>("PurchaseRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Brand")
                        .HasColumnType("TEXT");

                    b.Property<double>("GrossWeight")
                        .HasColumnType("REAL");

                    b.Property<int?>("IsApproved")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ItemType")
                        .HasColumnType("TEXT");

                    b.Property<double>("Labour")
                        .HasColumnType("REAL");

                    b.Property<double>("LessWeight")
                        .HasColumnType("REAL");

                    b.Property<string>("ManufactureId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ManufactureName")
                        .HasColumnType("TEXT");

                    b.Property<string>("MatelType")
                        .HasColumnType("TEXT");

                    b.Property<double>("NetWeight")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Rate")
                        .HasColumnType("REAL");

                    b.Property<double>("Wastage")
                        .HasColumnType("REAL");

                    b.HasKey("PurchaseRequestId");

                    b.ToTable("PurchaseRequests");
                });

            modelBuilder.Entity("Sonaar.Entities.Stock.Gold", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Brand")
                        .HasColumnType("TEXT");

                    b.Property<string>("Carrot")
                        .HasColumnType("TEXT");

                    b.Property<string>("Code")
                        .HasColumnType("TEXT");

                    b.Property<double>("GrossWeight")
                        .HasColumnType("REAL");

                    b.Property<string>("Image")
                        .HasColumnType("TEXT");

                    b.Property<double>("LessWeight")
                        .HasColumnType("REAL");

                    b.Property<double>("NetWeight")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("GoldStock");
                });

            modelBuilder.Entity("Sonaar.Domain.Entities.Product.ProductEntity", b =>
                {
                    b.HasOne("Sonaar.Domain.Entities.Quotations.Quotation", "Quotation")
                        .WithMany("ProductList")
                        .HasForeignKey("QuotationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Quotation");
                });

            modelBuilder.Entity("Sonaar.Domain.Entities.Quotations.Quotation", b =>
                {
                    b.HasOne("Sonaar.Domain.Entities.Contacts.ContactDetails", "ContactDetails")
                        .WithMany()
                        .HasForeignKey("ContactId");

                    b.HasOne("Sonaar.Domain.Entities.Quotations.GstAmountEntity", "GSTAmount")
                        .WithMany()
                        .HasForeignKey("GstAmountId");

                    b.Navigation("ContactDetails");

                    b.Navigation("GSTAmount");
                });

            modelBuilder.Entity("Sonaar.Domain.Entities.Quotations.Quotation", b =>
                {
                    b.Navigation("ProductList");
                });
#pragma warning restore 612, 618
        }
    }
}
