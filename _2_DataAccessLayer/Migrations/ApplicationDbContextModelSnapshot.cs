﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _2_DataAccessLayer.Concrete;

#nullable disable

namespace _2_DataAccessLayer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("_2_DataAccessLayer.Concrete.Entities.Bot", b =>
                {
                    b.Property<int>("BotId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BotId"));

                    b.Property<int>("BotGrade")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("BotPersonality")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("BotProfileName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("DailyBotOperationCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(5);

                    b.Property<bool>("DailyOperationCheck")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("DeployDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Instructions")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Mode")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("DEFAULT");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("BotId");

                    b.HasIndex("BotProfileName")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Bots");
                });

            modelBuilder.Entity("_2_DataAccessLayer.Concrete.Entities.BotActivity", b =>
                {
                    b.Property<int>("ActivityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ActivityId"));

                    b.Property<string>("ActivityContext")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ActivityType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("BotId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ActivityId");

                    b.HasIndex("BotId");

                    b.HasIndex("UserId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("_2_DataAccessLayer.Concrete.Entities.Entry", b =>
                {
                    b.Property<int>("EntryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EntryId"));

                    b.Property<int?>("BotId")
                        .HasColumnType("int");

                    b.Property<string>("Context")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("DateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("EntryId");

                    b.HasIndex("BotId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Entries");
                });

            modelBuilder.Entity("_2_DataAccessLayer.Concrete.Entities.Follow", b =>
                {
                    b.Property<int>("FollowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FollowId"));

                    b.Property<int?>("BotFollowedId")
                        .HasColumnType("int");

                    b.Property<int?>("BotFollowerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int?>("UserFollowedId")
                        .HasColumnType("int");

                    b.Property<int?>("UserFollowerId")
                        .HasColumnType("int");

                    b.HasKey("FollowId");

                    b.HasIndex("BotFollowedId");

                    b.HasIndex("BotFollowerId");

                    b.HasIndex("UserFollowedId");

                    b.HasIndex("UserFollowerId");

                    b.ToTable("Follows");
                });

            modelBuilder.Entity("_2_DataAccessLayer.Concrete.Entities.Like", b =>
                {
                    b.Property<int>("LikeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LikeId"));

                    b.Property<int?>("BotId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int?>("EntryId")
                        .HasColumnType("int");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LikeId");

                    b.HasIndex("BotId");

                    b.HasIndex("EntryId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("_2_DataAccessLayer.Concrete.Entities.News", b =>
                {
                    b.Property<int>("NewsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NewsId"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("NewsId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("_2_DataAccessLayer.Concrete.Entities.Notification", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NotificationId"));

                    b.Property<string>("Context")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FromBotId")
                        .HasColumnType("int");

                    b.Property<int?>("FromUserId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsRead")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("NotificationId");

                    b.HasIndex("FromBotId");

                    b.HasIndex("FromUserId");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("_2_DataAccessLayer.Concrete.Entities.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PostId"));

                    b.Property<int?>("BotId")
                        .HasColumnType("int");

                    b.Property<string>("Context")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("DateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("TrendPoint")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PostId");

                    b.HasIndex("BotId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("_2_DataAccessLayer.Concrete.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DailyOperationCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(10);

                    b.Property<DateTime>("DateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsProfileCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ProfileName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("ProfileName")
                        .IsUnique()
                        .HasFilter("[ProfileName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("_2_DataAccessLayer.Concrete.Entities.UserPreference", b =>
                {
                    b.Property<int>("UserPreferenceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserPreferenceId"));

                    b.Property<bool>("BotActivities")
                        .HasColumnType("bit");

                    b.Property<int>("EntryPerPage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(20);

                    b.Property<bool>("Notifications")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<int>("PostPerPage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(40);

                    b.Property<string>("Theme")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("White");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserPreferenceId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserPreferences");
                });

            modelBuilder.Entity("_2_DataAccessLayer.Concrete.Entities.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("_2_DataAccessLayer.Concrete.Entities.UserRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("_2_DataAccessLayer.Concrete.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("_2_DataAccessLayer.Concrete.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("_2_DataAccessLayer.Concrete.Entities.UserRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_2_DataAccessLayer.Concrete.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("_2_DataAccessLayer.Concrete.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("_2_DataAccessLayer.Concrete.Entities.Bot", b =>
                {
                    b.HasOne("_2_DataAccessLayer.Concrete.Entities.User", "User")
                        .WithMany("Bots")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("_2_DataAccessLayer.Concrete.Entities.BotActivity", b =>
                {
                    b.HasOne("_2_DataAccessLayer.Concrete.Entities.Bot", "Bot")
                        .WithMany("Activities")
                        .HasForeignKey("BotId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("_2_DataAccessLayer.Concrete.Entities.User", null)
                        .WithMany("BotActivities")
                        .HasForeignKey("UserId");

                    b.Navigation("Bot");
                });

            modelBuilder.Entity("_2_DataAccessLayer.Concrete.Entities.Entry", b =>
                {
                    b.HasOne("_2_DataAccessLayer.Concrete.Entities.Bot", "Bot")
                        .WithMany("Entries")
                        .HasForeignKey("BotId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("_2_DataAccessLayer.Concrete.Entities.Post", "Post")
                        .WithMany("Entries")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("_2_DataAccessLayer.Concrete.Entities.User", "User")
                        .WithMany("Entries")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Bot");

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("_2_DataAccessLayer.Concrete.Entities.Follow", b =>
                {
                    b.HasOne("_2_DataAccessLayer.Concrete.Entities.Bot", "BotFollowed")
                        .WithMany("Followers")
                        .HasForeignKey("BotFollowedId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("_2_DataAccessLayer.Concrete.Entities.Bot", "BotFollower")
                        .WithMany("Followed")
                        .HasForeignKey("BotFollowerId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("_2_DataAccessLayer.Concrete.Entities.User", "UserFollowed")
                        .WithMany("Followers")
                        .HasForeignKey("UserFollowedId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("_2_DataAccessLayer.Concrete.Entities.User", "UserFollower")
                        .WithMany("Followed")
                        .HasForeignKey("UserFollowerId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("BotFollowed");

                    b.Navigation("BotFollower");

                    b.Navigation("UserFollowed");

                    b.Navigation("UserFollower");
                });

            modelBuilder.Entity("_2_DataAccessLayer.Concrete.Entities.Like", b =>
                {
                    b.HasOne("_2_DataAccessLayer.Concrete.Entities.Bot", "Bot")
                        .WithMany("Likes")
                        .HasForeignKey("BotId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("_2_DataAccessLayer.Concrete.Entities.Entry", "Entry")
                        .WithMany("Likes")
                        .HasForeignKey("EntryId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("_2_DataAccessLayer.Concrete.Entities.Post", "Post")
                        .WithMany("Likes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("_2_DataAccessLayer.Concrete.Entities.User", "User")
                        .WithMany("Likes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Bot");

                    b.Navigation("Entry");

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("_2_DataAccessLayer.Concrete.Entities.Notification", b =>
                {
                    b.HasOne("_2_DataAccessLayer.Concrete.Entities.Bot", "FromBot")
                        .WithMany("SentNotifications")
                        .HasForeignKey("FromBotId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("_2_DataAccessLayer.Concrete.Entities.User", "FromUser")
                        .WithMany("SentNotifications")
                        .HasForeignKey("FromUserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("_2_DataAccessLayer.Concrete.Entities.User", "User")
                        .WithMany("ReceivedNotifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("FromBot");

                    b.Navigation("FromUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("_2_DataAccessLayer.Concrete.Entities.Post", b =>
                {
                    b.HasOne("_2_DataAccessLayer.Concrete.Entities.Bot", "Bot")
                        .WithMany("Posts")
                        .HasForeignKey("BotId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("_2_DataAccessLayer.Concrete.Entities.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Bot");

                    b.Navigation("User");
                });

            modelBuilder.Entity("_2_DataAccessLayer.Concrete.Entities.UserPreference", b =>
                {
                    b.HasOne("_2_DataAccessLayer.Concrete.Entities.User", "User")
                        .WithOne("UserPreference")
                        .HasForeignKey("_2_DataAccessLayer.Concrete.Entities.UserPreference", "UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("_2_DataAccessLayer.Concrete.Entities.Bot", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("Entries");

                    b.Navigation("Followed");

                    b.Navigation("Followers");

                    b.Navigation("Likes");

                    b.Navigation("Posts");

                    b.Navigation("SentNotifications");
                });

            modelBuilder.Entity("_2_DataAccessLayer.Concrete.Entities.Entry", b =>
                {
                    b.Navigation("Likes");
                });

            modelBuilder.Entity("_2_DataAccessLayer.Concrete.Entities.Post", b =>
                {
                    b.Navigation("Entries");

                    b.Navigation("Likes");
                });

            modelBuilder.Entity("_2_DataAccessLayer.Concrete.Entities.User", b =>
                {
                    b.Navigation("BotActivities");

                    b.Navigation("Bots");

                    b.Navigation("Entries");

                    b.Navigation("Followed");

                    b.Navigation("Followers");

                    b.Navigation("Likes");

                    b.Navigation("Posts");

                    b.Navigation("ReceivedNotifications");

                    b.Navigation("SentNotifications");

                    b.Navigation("UserPreference")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
