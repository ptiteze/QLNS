using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using QLNS_BackEnd.Models;

namespace QLNS_BackEnd.Data;

public partial class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Boardnew> Boardnews { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Catalog> Catalogs { get; set; }

    public virtual DbSet<ImportDetail> ImportDetails { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Ordered> Ordereds { get; set; }

    public virtual DbSet<Producer> Producers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Slide> Slides { get; set; }

    public virtual DbSet<SupplyInvoice> SupplyInvoices { get; set; }

    public virtual DbSet<SupplyList> SupplyLists { get; set; }

    public virtual DbSet<Used> Useds { get; set; }

    public virtual DbSet<UsedProduct> UsedProducts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-LL3CDGR;Initial Catalog=QLNS3;User ID=sa;Password=nhitnho;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("account");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasComment("1-Đang hoạt đông; 0-Đã bị khóa")
                .HasColumnName("status");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_account_roles");
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__admin__3213E83F4640B4FB");

            entity.ToTable("admin");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("email");
            entity.Property(e => e.IdAccount).HasColumnName("id_account");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");

            entity.HasOne(d => d.IdAccountNavigation).WithMany(p => p.Admins)
                .HasForeignKey(d => d.IdAccount)
                .HasConstraintName("FK_admin_account");
        });

        modelBuilder.Entity<Boardnew>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__boardnew__3213E83FB87261F6");

            entity.ToTable("boardnew");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Author)
                .HasMaxLength(50)
                .HasColumnName("author");
            entity.Property(e => e.Content)
                .HasMaxLength(4000)
                .HasColumnName("content");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.ImageLink)
                .HasMaxLength(4000)
                .HasColumnName("image_link");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ProductId }).HasName("PK_cart_1");

            entity.ToTable("cart");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Product).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cart_product");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cart_users");
        });

        modelBuilder.Entity<Catalog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__catalog__3213E83FF624631C");

            entity.ToTable("catalog");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
        });

        modelBuilder.Entity<ImportDetail>(entity =>
        {
            entity.HasKey(e => new { e.InvoiceId, e.ProductId });

            entity.ToTable("import_detail");

            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ImportPrice).HasColumnName("import_price");
            entity.Property(e => e.QuantityImport).HasColumnName("quantity_import");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.Stock).HasColumnName("stock");

            entity.HasOne(d => d.Invoice).WithMany(p => p.ImportDetails)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_import_detail_supply_Invoice");

            entity.HasOne(d => d.Product).WithMany(p => p.ImportDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_import_detail_product");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_bill");

            entity.ToTable("order");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Message)
                .HasColumnType("text")
                .HasColumnName("message");
            entity.Property(e => e.Payment)
                .HasMaxLength(30)
                .HasColumnName("payment");
            entity.Property(e => e.Receiveddate).HasColumnName("receiveddate");
            entity.Property(e => e.Sentdate).HasColumnName("sentdate");
            entity.Property(e => e.Status)
                .HasDefaultValue(0)
                .HasComment("0 : chưa hoàn thành, 1 hoàn thành")
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("user_name");
            entity.Property(e => e.UserMail)
                .HasMaxLength(30)
                .HasColumnName("user_mail");
            entity.Property(e => e.UserPhone)
                .HasMaxLength(20)
                .HasColumnName("user_phone");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Status)
                .HasConstraintName("FK_order_order_status");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_order_users");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.ToTable("order_status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Ordered>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId });

            entity.ToTable("ordered");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Qty).HasColumnName("qty");

            entity.HasOne(d => d.Order).WithMany(p => p.Ordereds)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ordered_bill");

            entity.HasOne(d => d.Product).WithMany(p => p.Ordereds)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ordered_product");
        });

        modelBuilder.Entity<Producer>(entity =>
        {
            entity.ToTable("producer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Numphone)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("numphone");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__product__3213E83F6B8B1FD3");

            entity.ToTable("product");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CatalogId).HasColumnName("catalog_id");
            entity.Property(e => e.Content)
                .HasMaxLength(4000)
                .HasColumnName("content");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.Description)
                .HasMaxLength(4000)
                .HasColumnName("description");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.ExpiryDate)
                .HasComment("day")
                .HasColumnName("expiry_date");
            entity.Property(e => e.ImageLink)
                .HasMaxLength(4000)
                .HasColumnName("image_link");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(0)
                .HasColumnName("quantity");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Unit)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("unit");

            entity.HasOne(d => d.Catalog).WithMany(p => p.Products)
                .HasForeignKey(d => d.CatalogId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_product_catalog");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__review__3213E83FCDAFC71B");

            entity.ToTable("review");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContentReview)
                .HasMaxLength(4000)
                .HasColumnName("contentReview");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.IdUser)
                .HasColumnName("id_user");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Score)
                .HasDefaultValue(0)
                .HasColumnName("score");

            entity.HasOne(d => d.Product).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_review_product");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_review_users");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Slide>(entity =>
        {
            entity.ToTable("slides");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContentSlide)
                .HasMaxLength(200)
                .HasColumnName("content_slide");
            entity.Property(e => e.DiscountContent)
                .HasMaxLength(100)
                .HasColumnName("discount_content");
            entity.Property(e => e.ImageLink)
                .HasMaxLength(50)
                .HasColumnName("image_link");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");
        });

        modelBuilder.Entity<SupplyInvoice>(entity =>
        {
            entity.ToTable("supply_Invoice");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdId).HasColumnName("ad_id");
            entity.Property(e => e.ProducerId).HasColumnName("producer_id");
            entity.Property(e => e.SupplyTime).HasColumnName("supply_time");

            entity.HasOne(d => d.Ad).WithMany(p => p.SupplyInvoices)
                .HasForeignKey(d => d.AdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_supply_Invoice_admin");

            entity.HasOne(d => d.Producer).WithMany(p => p.SupplyInvoices)
                .HasForeignKey(d => d.ProducerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_supply_Invoice_producer");
        });

        modelBuilder.Entity<SupplyList>(entity =>
        {
            entity.ToTable("supply_list");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ImportPrice)
                .HasDefaultValue(0)
                .HasColumnName("import_price");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(100)
                .HasColumnName("quantity");

            entity.HasOne(d => d.Product).WithMany(p => p.SupplyLists)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_supply_list_product");
        });

        modelBuilder.Entity<Used>(entity =>
        {
            entity.ToTable("used");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<UsedProduct>(entity =>
        {
            entity.ToTable("used_product");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.UsedId).HasColumnName("used_id");

            entity.HasOne(d => d.Product).WithMany(p => p.UsedProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_used_product_product");

            entity.HasOne(d => d.Used).WithMany(p => p.UsedProducts)
                .HasForeignKey(d => d.UsedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_used_product_used");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_users_1");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.IdAccount).HasColumnName("id_account");
            entity.Property(e => e.Nameuser)
                .HasMaxLength(50)
                .HasColumnName("nameuser");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");

            entity.HasOne(d => d.IdAccountNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdAccount)
                .HasConstraintName("FK_users_account");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

