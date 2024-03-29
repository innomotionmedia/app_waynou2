﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoAppService.NET6.Db;

#nullable disable

namespace TodoAppService.NET6.Migrations
{
    [DbContext(typeof(MigrationDbContext))]
    partial class MigrationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TodoAppService.NET6.Db.tblCategories", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<bool>("Kategorie_DE")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("TodoAppService.NET6.Db.tblChatMessages", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("chatRoomID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("dateCreatedUnix")
                        .HasColumnType("bigint");

                    b.Property<string>("fromUserID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("toUserID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ChatRoomMessages");
                });

            modelBuilder.Entity("TodoAppService.NET6.Db.tblChatRoomMembers", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("chatRoomID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ChatRoomMembers");
                });

            modelBuilder.Entity("TodoAppService.NET6.Db.tblChatRooms", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("chatRoomType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("lastMessageUnix")
                        .HasColumnType("bigint");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ChatRooms");
                });

            modelBuilder.Entity("TodoAppService.NET6.Db.tblCircleMembers", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("circleID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("dateCreatedUnix")
                        .HasColumnType("bigint");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CircleMembers");
                });

            modelBuilder.Entity("TodoAppService.NET6.Db.tblCircles", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("fullpic")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("hostID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isOpen")
                        .HasColumnType("bit");

                    b.Property<int>("membersCount")
                        .HasColumnType("int");

                    b.Property<byte[]>("smallPic")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Circles");
                });

            modelBuilder.Entity("TodoAppService.NET6.Db.tblConstants", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Constants");
                });

            modelBuilder.Entity("TodoAppService.NET6.Db.tblEventInTags", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("catID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("eventID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EventInTags");
                });

            modelBuilder.Entity("TodoAppService.NET6.Db.tblEvents", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("categoryID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("createdUnixInt")
                        .HasColumnType("bigint");

                    b.Property<string>("daysID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("delayLocationHoursInt")
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("displayEndTime")
                        .HasColumnType("bit");

                    b.Property<bool>("displayStartTime")
                        .HasColumnType("bit");

                    b.Property<long?>("endTimeAndDateUnixTicks")
                        .HasColumnType("bigint");

                    b.Property<long?>("endTimeSalesDateInUnixTix")
                        .HasColumnType("bigint");

                    b.Property<byte[]>("eventPictureFull")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("eventPictureThumb")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool?>("hasTicketing")
                        .HasColumnType("bit");

                    b.Property<float?>("latitude")
                        .HasColumnType("real");

                    b.Property<float?>("longitude")
                        .HasColumnType("real");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("onlineInstructions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("onlineLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("priceInCents")
                        .HasColumnType("int");

                    b.Property<string>("price_id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("recurringTicksUnix")
                        .HasColumnType("bigint");

                    b.Property<string>("repeatCycle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("repeatDaysDelimitted")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("slots")
                        .HasColumnType("int");

                    b.Property<long?>("startTimeAndDateUnixTicks")
                        .HasColumnType("bigint");

                    b.Property<long?>("startTimeSalesDateInUnixTix")
                        .HasColumnType("bigint");

                    b.Property<string>("userID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("vat")
                        .HasColumnType("int");

                    b.Property<string>("venueAdresse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("venueCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("venueName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("visibleOnRadar")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("TodoAppService.NET6.Db.tblGroupMessages", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("fromCircles")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fromUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tblGroupMessageID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("unixCreated")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("GroupMessages");
                });

            modelBuilder.Entity("TodoAppService.NET6.Db.tblInvites", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<int?>("amountOfTotalGuests")
                        .HasColumnType("int");

                    b.Property<string>("extraInformation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("invitedCreatedUnixInt")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.Property<string>("invitedPersonIdentifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("isBouncer")
                        .HasColumnType("bit");

                    b.Property<bool?>("isBoxOffice")
                        .HasColumnType("bit");

                    b.Property<bool?>("isSafeSlot")
                        .HasColumnType("bit");

                    b.Property<int?>("maxNumberOfGuests")
                        .HasColumnType("int");

                    b.Property<int?>("plusOnes")
                        .HasColumnType("int");

                    b.Property<string>("price_id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("reservedUntilRejected")
                        .HasColumnType("bit");

                    b.Property<string>("status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tblEventID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tblUserID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Invites");
                });

            modelBuilder.Entity("TodoAppService.NET6.Db.tblNotifications", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<long?>("dateCreatedUnix")
                        .HasColumnType("bigint");

                    b.Property<string>("forUserID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fromUserID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("idenfitifer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("notificationType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("TodoAppService.NET6.Db.tblStripeActions", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("payment_intent_succeeded")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tblUserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StripeActions");
                });

            modelBuilder.Entity("TodoAppService.NET6.Db.tblTags", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<byte[]>("Images")
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("belongsToCategory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("TodoAppService.NET6.Db.tblUsers", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<DateTime>("birthday")
                        .HasColumnType("datetime2");

                    b.Property<int?>("circlesCut")
                        .HasColumnType("int");

                    b.Property<int?>("creditsInCents")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fullname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("merchant_id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nickname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("profilepicture")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("telephone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("thumbnail")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TodoAppService.NET6.Db.tblWaitingListPictures", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<byte[]>("fullpic")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("thumbnail")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("waitingListId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WaitingListPictures");
                });

            modelBuilder.Entity("TodoAppService.NET6.Db.tblWaitingLists", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<long?>("createdUnixInt")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.Property<string>("eventID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("messageForHost")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("numberOfGuests")
                        .HasColumnType("int");

                    b.Property<string>("userID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WaitingLists");
                });
#pragma warning restore 612, 618
        }
    }
}
