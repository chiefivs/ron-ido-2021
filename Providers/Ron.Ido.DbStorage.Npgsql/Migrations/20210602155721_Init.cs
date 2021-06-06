using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Ron.Ido.DbStorage.Npgsql.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplyAims",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false),
                    NameEng = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyAims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplyAttachmentTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    NameEng = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false),
                    Required = table.Column<bool>(type: "boolean", nullable: false),
                    ForArchive = table.Column<bool>(type: "boolean", nullable: false),
                    ForPortal = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyAttachmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplyDeliveryForms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    NameEng = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyDeliveryForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplyDocFullPackageTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyDocFullPackageTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplyEntryForms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyEntryForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplyLearnForms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false),
                    NameEng = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyLearnForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplyPassportTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyPassportTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplyTemplates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    OrderNum = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CertificateDeliveryForms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    NameEng = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificateDeliveryForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LearnLevels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    FullName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearnLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Legalizations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Legalizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false),
                    OldId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReglamentEtaps",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    MinTerm = table.Column<int>(type: "integer", nullable: false),
                    MaxTerm = table.Column<int>(type: "integer", nullable: false),
                    Required = table.Column<bool>(type: "boolean", nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReglamentEtaps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false),
                    ViewApplyStatusesString = table.Column<string>(type: "text", nullable: true),
                    StepApplyStatusesString = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchoolType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameEng = table.Column<string>(type: "text", nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SurName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    LastName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Login = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Snils = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Remark = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsBlocked = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplyDocTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false),
                    LearnLevelId = table.Column<long>(type: "bigint", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NameEng = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyDocTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplyDocTypes_LearnLevels_LearnLevelId",
                        column: x => x.LearnLevelId,
                        principalTable: "LearnLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    FullName = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    NameEng = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LegalizationNeeded = table.Column<bool>(type: "boolean", nullable: true),
                    LegalizationComment = table.Column<string>(type: "text", nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false),
                    A2code = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    A3code = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    EiisCode = table.Column<int>(type: "integer", nullable: true),
                    IsgaCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    OksmCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    RegionId = table.Column<long>(type: "bigint", nullable: true),
                    LegalizationId = table.Column<int>(type: "integer", nullable: true),
                    CoordX = table.Column<double>(type: "double precision", nullable: true),
                    CoordY = table.Column<double>(type: "double precision", nullable: true),
                    OldId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countries_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplyStatuses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusEnumValue = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    NameForButton = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    NameForApplier = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    NameForApplierEng = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DescriptionForApplier = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    DescriptionForApplierEng = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    VisibleForApplier = table.Column<bool>(type: "boolean", nullable: false),
                    AllowStepToStatuses = table.Column<string>(type: "text", nullable: true),
                    EtapId = table.Column<long>(type: "bigint", nullable: true),
                    OldId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplyStatuses_ReglamentEtaps_EtapId",
                        column: x => x.EtapId,
                        principalTable: "ReglamentEtaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolesPermissions",
                columns: table => new
                {
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesPermissions", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolesPermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileInfos",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(260)", maxLength: 260, nullable: true),
                    Size = table.Column<int>(type: "integer", nullable: false),
                    ContentType = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    OldId = table.Column<int>(type: "integer", nullable: false),
                    CreatorEmail = table.Column<string>(type: "character varying(260)", maxLength: 260, nullable: true),
                    Source = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileInfos", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_FileInfos_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsersRoles",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UsersRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Applies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrimaryBarCode = table.Column<string>(type: "text", nullable: true),
                    BarCode = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    AcceptTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EpguCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    EpguStatus = table.Column<int>(type: "integer", nullable: true),
                    Storage = table.Column<string>(type: "text", nullable: true),
                    IsEnglish = table.Column<bool>(type: "boolean", nullable: false),
                    TransmitOpenChannels = table.Column<bool>(type: "boolean", nullable: false),
                    DocsWillSendByPost = table.Column<bool>(type: "boolean", nullable: false),
                    ForInfoLetter = table.Column<bool>(type: "boolean", nullable: false),
                    ForOferta = table.Column<bool>(type: "boolean", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    StatusId = table.Column<long>(type: "bigint", nullable: false),
                    StatusChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatorFirstName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    IsCreatorFirstNameAbsent = table.Column<bool>(type: "boolean", nullable: false),
                    CreatorLastName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    IsCreatorLastNameAbsent = table.Column<bool>(type: "boolean", nullable: false),
                    CreatorSurname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    IsCreatorSurnameAbsent = table.Column<bool>(type: "boolean", nullable: false),
                    CreatorGender = table.Column<int>(type: "integer", nullable: false),
                    CreatorBirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatorBirthPlace = table.Column<string>(type: "text", nullable: true),
                    CreatorCitizenshipId = table.Column<long>(type: "bigint", nullable: true),
                    CreatorPassportTypeId = table.Column<long>(type: "bigint", nullable: true),
                    CreatorPassportReq = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    ByWarrant = table.Column<bool>(type: "boolean", nullable: false),
                    WarrantReq = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    WarrantDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    WarrantTerm = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatorCountryId = table.Column<long>(type: "bigint", nullable: true),
                    CreatorMailIndex = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    CreatorCityName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatorStreet = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatorCorpus = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    CreatorBuilding = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    CreatorBlock = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    CreatorFlat = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    CreatorPhone = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatorEmail = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DeliveryFormId = table.Column<long>(type: "bigint", nullable: true),
                    ReturnOriginalsFormId = table.Column<long>(type: "bigint", nullable: true),
                    IsReturnOriginalsPostAddressDifferent = table.Column<bool>(type: "boolean", nullable: false),
                    ReturnOriginalsPostAddress = table.Column<string>(type: "text", nullable: true),
                    OwnerFirstName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    IsOwnerFirstNameAbsent = table.Column<bool>(type: "boolean", nullable: false),
                    OwnerLastName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    IsOwnerLastNameAbsent = table.Column<bool>(type: "boolean", nullable: false),
                    OwnerSurname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    IsOwnerSurnameAbsent = table.Column<bool>(type: "boolean", nullable: false),
                    OwnerGender = table.Column<int>(type: "integer", nullable: false),
                    OwnerBirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    OwnerBirthPlace = table.Column<string>(type: "text", nullable: true),
                    OwnerCountryId = table.Column<long>(type: "bigint", nullable: true),
                    OwnerMailIndex = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    OwnerCityName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    OwnerStreet = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    OwnerCorpus = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    OwnerBuilding = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    OwnerBlock = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    OwnerFlat = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    OwnerPhone = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    OwnerEmail = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    OwnerCitizenshipId = table.Column<long>(type: "bigint", nullable: true),
                    OwnerPassportTypeId = table.Column<long>(type: "bigint", nullable: true),
                    OwnerPassportReq = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    DocCountryId = table.Column<long>(type: "bigint", nullable: true),
                    DocTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DocDescription = table.Column<string>(type: "text", nullable: true),
                    DocBlankNum = table.Column<string>(type: "text", nullable: true),
                    DocRegNum = table.Column<string>(type: "text", nullable: true),
                    DocDate = table.Column<string>(type: "text", nullable: true),
                    DocDateYear = table.Column<int>(type: "integer", nullable: true),
                    DocAttachmentsCount = table.Column<int>(type: "integer", nullable: true),
                    DocFullName = table.Column<string>(type: "text", nullable: true),
                    SchoolCountryId = table.Column<long>(type: "bigint", nullable: true),
                    SchoolName = table.Column<string>(type: "text", nullable: true),
                    SchoolTypeId = table.Column<long>(type: "bigint", nullable: true),
                    SchoolPostIndex = table.Column<string>(type: "text", nullable: true),
                    SchoolCityName = table.Column<string>(type: "text", nullable: true),
                    SchoolAddress = table.Column<string>(type: "text", nullable: true),
                    SchoolPhone = table.Column<string>(type: "text", nullable: true),
                    SchoolFax = table.Column<string>(type: "text", nullable: true),
                    SchoolEmail = table.Column<string>(type: "text", nullable: true),
                    BaseLearnDateBegin = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    BaseLearnDateEnd = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SpecialLearnDateBegin = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SpecialLearnDateEnd = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FixedLearnSpecialityName = table.Column<string>(type: "text", nullable: true),
                    SpecialLearnFormId = table.Column<long>(type: "bigint", nullable: true),
                    AimId = table.Column<long>(type: "bigint", nullable: true),
                    OrgCreator = table.Column<string>(type: "text", nullable: true),
                    Other = table.Column<string>(type: "text", nullable: true),
                    EntryFormId = table.Column<long>(type: "bigint", nullable: true),
                    IsNovorossia = table.Column<bool>(type: "boolean", nullable: false),
                    IsRostovFilial = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applies_ApplyAims_AimId",
                        column: x => x.AimId,
                        principalTable: "ApplyAims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Applies_ApplyDeliveryForms_DeliveryFormId",
                        column: x => x.DeliveryFormId,
                        principalTable: "ApplyDeliveryForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Applies_ApplyDeliveryForms_ReturnOriginalsFormId",
                        column: x => x.ReturnOriginalsFormId,
                        principalTable: "ApplyDeliveryForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Applies_ApplyDocTypes_DocTypeId",
                        column: x => x.DocTypeId,
                        principalTable: "ApplyDocTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Applies_ApplyEntryForms_EntryFormId",
                        column: x => x.EntryFormId,
                        principalTable: "ApplyEntryForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Applies_ApplyLearnForms_SpecialLearnFormId",
                        column: x => x.SpecialLearnFormId,
                        principalTable: "ApplyLearnForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Applies_ApplyPassportTypes_CreatorPassportTypeId",
                        column: x => x.CreatorPassportTypeId,
                        principalTable: "ApplyPassportTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Applies_ApplyPassportTypes_OwnerPassportTypeId",
                        column: x => x.OwnerPassportTypeId,
                        principalTable: "ApplyPassportTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Applies_ApplyStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ApplyStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applies_Countries_CreatorCitizenshipId",
                        column: x => x.CreatorCitizenshipId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Applies_Countries_CreatorCountryId",
                        column: x => x.CreatorCountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Applies_Countries_DocCountryId",
                        column: x => x.DocCountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Applies_Countries_OwnerCitizenshipId",
                        column: x => x.OwnerCitizenshipId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Applies_Countries_OwnerCountryId",
                        column: x => x.OwnerCountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Applies_Countries_SchoolCountryId",
                        column: x => x.SchoolCountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Applies_SchoolType_SchoolTypeId",
                        column: x => x.SchoolTypeId,
                        principalTable: "SchoolType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplyAttachments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplyId = table.Column<long>(type: "bigint", nullable: false),
                    Required = table.Column<bool>(type: "boolean", nullable: false),
                    Given = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Error = table.Column<string>(type: "text", nullable: true),
                    AttachmentTypeId = table.Column<long>(type: "bigint", nullable: true),
                    TypeId = table.Column<long>(type: "bigint", nullable: true),
                    FileInfoUid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplyAttachments_Applies_ApplyId",
                        column: x => x.ApplyId,
                        principalTable: "Applies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplyAttachments_ApplyAttachmentTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ApplyAttachmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplyAttachments_FileInfos_FileInfoUid",
                        column: x => x.FileInfoUid,
                        principalTable: "FileInfos",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplyCertificateDeliveryForms",
                columns: table => new
                {
                    ApplyId = table.Column<long>(type: "bigint", nullable: false),
                    DeliveryFormId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyCertificateDeliveryForms", x => new { x.ApplyId, x.DeliveryFormId });
                    table.ForeignKey(
                        name: "FK_ApplyCertificateDeliveryForms_Applies_ApplyId",
                        column: x => x.ApplyId,
                        principalTable: "Applies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplyCertificateDeliveryForms_CertificateDeliveryForms_Deli~",
                        column: x => x.DeliveryFormId,
                        principalTable: "CertificateDeliveryForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplyStatusHistories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplyId = table.Column<long>(type: "bigint", nullable: false),
                    ChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    StatusId = table.Column<long>(type: "bigint", nullable: false),
                    PrevStatusId = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyStatusHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplyStatusHistories_Applies_ApplyId",
                        column: x => x.ApplyId,
                        principalTable: "Applies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplyStatusHistories_ApplyStatuses_PrevStatusId",
                        column: x => x.PrevStatusId,
                        principalTable: "ApplyStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplyStatusHistories_ApplyStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ApplyStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplyStatusHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dossiers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplyId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dossiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dossiers_Applies_ApplyId",
                        column: x => x.ApplyId,
                        principalTable: "Applies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applies_AimId",
                table: "Applies",
                column: "AimId");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_CreatorCitizenshipId",
                table: "Applies",
                column: "CreatorCitizenshipId");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_CreatorCountryId",
                table: "Applies",
                column: "CreatorCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_CreatorPassportTypeId",
                table: "Applies",
                column: "CreatorPassportTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_DeliveryFormId",
                table: "Applies",
                column: "DeliveryFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_DocCountryId",
                table: "Applies",
                column: "DocCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_DocTypeId",
                table: "Applies",
                column: "DocTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_EntryFormId",
                table: "Applies",
                column: "EntryFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_EpguCode",
                table: "Applies",
                column: "EpguCode");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_OwnerCitizenshipId",
                table: "Applies",
                column: "OwnerCitizenshipId");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_OwnerCountryId",
                table: "Applies",
                column: "OwnerCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_OwnerPassportTypeId",
                table: "Applies",
                column: "OwnerPassportTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_ReturnOriginalsFormId",
                table: "Applies",
                column: "ReturnOriginalsFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_SchoolCountryId",
                table: "Applies",
                column: "SchoolCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_SchoolTypeId",
                table: "Applies",
                column: "SchoolTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_SpecialLearnFormId",
                table: "Applies",
                column: "SpecialLearnFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_StatusId",
                table: "Applies",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyAims_Name",
                table: "ApplyAims",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyAims_NameEng",
                table: "ApplyAims",
                column: "NameEng");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyAttachments_ApplyId",
                table: "ApplyAttachments",
                column: "ApplyId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyAttachments_FileInfoUid",
                table: "ApplyAttachments",
                column: "FileInfoUid");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyAttachments_TypeId",
                table: "ApplyAttachments",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyCertificateDeliveryForms_DeliveryFormId",
                table: "ApplyCertificateDeliveryForms",
                column: "DeliveryFormId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyDeliveryForms_Name",
                table: "ApplyDeliveryForms",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyDeliveryForms_NameEng",
                table: "ApplyDeliveryForms",
                column: "NameEng");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyDeliveryForms_OrderNum",
                table: "ApplyDeliveryForms",
                column: "OrderNum");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyDocFullPackageTypes_Name",
                table: "ApplyDocFullPackageTypes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyDocFullPackageTypes_OrderNum",
                table: "ApplyDocFullPackageTypes",
                column: "OrderNum");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyDocTypes_BeginDate",
                table: "ApplyDocTypes",
                column: "BeginDate");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyDocTypes_EndDate",
                table: "ApplyDocTypes",
                column: "EndDate");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyDocTypes_LearnLevelId",
                table: "ApplyDocTypes",
                column: "LearnLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyDocTypes_Name",
                table: "ApplyDocTypes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyDocTypes_NameEng",
                table: "ApplyDocTypes",
                column: "NameEng");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyDocTypes_OrderNum",
                table: "ApplyDocTypes",
                column: "OrderNum");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyEntryForms_Name",
                table: "ApplyEntryForms",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyEntryForms_OrderNum",
                table: "ApplyEntryForms",
                column: "OrderNum");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyLearnForms_Name",
                table: "ApplyLearnForms",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyLearnForms_NameEng",
                table: "ApplyLearnForms",
                column: "NameEng");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyLearnForms_OrderNum",
                table: "ApplyLearnForms",
                column: "OrderNum");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyPassportTypes_Code",
                table: "ApplyPassportTypes",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyPassportTypes_Name",
                table: "ApplyPassportTypes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyPassportTypes_OrderNum",
                table: "ApplyPassportTypes",
                column: "OrderNum");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyStatuses_EtapId",
                table: "ApplyStatuses",
                column: "EtapId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyStatusHistories_ApplyId",
                table: "ApplyStatusHistories",
                column: "ApplyId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyStatusHistories_ChangeTime",
                table: "ApplyStatusHistories",
                column: "ChangeTime");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyStatusHistories_EndTime",
                table: "ApplyStatusHistories",
                column: "EndTime");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyStatusHistories_PrevStatusId",
                table: "ApplyStatusHistories",
                column: "PrevStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyStatusHistories_StatusId",
                table: "ApplyStatusHistories",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyStatusHistories_UserId",
                table: "ApplyStatusHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyTemplates_Name",
                table: "ApplyTemplates",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyTemplates_OrderNum",
                table: "ApplyTemplates",
                column: "OrderNum");

            migrationBuilder.CreateIndex(
                name: "IX_CertificateDeliveryForms_Name",
                table: "CertificateDeliveryForms",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_CertificateDeliveryForms_NameEng",
                table: "CertificateDeliveryForms",
                column: "NameEng");

            migrationBuilder.CreateIndex(
                name: "IX_CertificateDeliveryForms_OrderNum",
                table: "CertificateDeliveryForms",
                column: "OrderNum");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_A2code",
                table: "Countries",
                column: "A2code");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_A3code",
                table: "Countries",
                column: "A3code");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_EiisCode",
                table: "Countries",
                column: "EiisCode");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_IsgaCode",
                table: "Countries",
                column: "IsgaCode");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Name",
                table: "Countries",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_NameEng",
                table: "Countries",
                column: "NameEng");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_OksmCode",
                table: "Countries",
                column: "OksmCode");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_OldId",
                table: "Countries",
                column: "OldId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_OrderNum",
                table: "Countries",
                column: "OrderNum");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_RegionId",
                table: "Countries",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Dossiers_ApplyId",
                table: "Dossiers",
                column: "ApplyId");

            migrationBuilder.CreateIndex(
                name: "IX_FileInfos_CreatedById",
                table: "FileInfos",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FileInfos_OldId",
                table: "FileInfos",
                column: "OldId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnLevels_BeginDate",
                table: "LearnLevels",
                column: "BeginDate");

            migrationBuilder.CreateIndex(
                name: "IX_LearnLevels_EndDate",
                table: "LearnLevels",
                column: "EndDate");

            migrationBuilder.CreateIndex(
                name: "IX_LearnLevels_FullName",
                table: "LearnLevels",
                column: "FullName");

            migrationBuilder.CreateIndex(
                name: "IX_LearnLevels_Name",
                table: "LearnLevels",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_LearnLevels_OrderNum",
                table: "LearnLevels",
                column: "OrderNum");

            migrationBuilder.CreateIndex(
                name: "IX_Legalizations_Name",
                table: "Legalizations",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Legalizations_OrderNum",
                table: "Legalizations",
                column: "OrderNum");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_Name",
                table: "Regions",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_OldId",
                table: "Regions",
                column: "OldId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_OrderNum",
                table: "Regions",
                column: "OrderNum");

            migrationBuilder.CreateIndex(
                name: "IX_ReglamentEtaps_Name",
                table: "ReglamentEtaps",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ReglamentEtaps_OrderNum",
                table: "ReglamentEtaps",
                column: "OrderNum");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolType_Name",
                table: "SchoolType",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolType_NameEng",
                table: "SchoolType",
                column: "NameEng");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolType_OrderNum",
                table: "SchoolType",
                column: "OrderNum");

            migrationBuilder.CreateIndex(
                name: "IX_UsersRoles_RoleId",
                table: "UsersRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplyAttachments");

            migrationBuilder.DropTable(
                name: "ApplyCertificateDeliveryForms");

            migrationBuilder.DropTable(
                name: "ApplyDocFullPackageTypes");

            migrationBuilder.DropTable(
                name: "ApplyStatusHistories");

            migrationBuilder.DropTable(
                name: "ApplyTemplates");

            migrationBuilder.DropTable(
                name: "Dossiers");

            migrationBuilder.DropTable(
                name: "Legalizations");

            migrationBuilder.DropTable(
                name: "RolesPermissions");

            migrationBuilder.DropTable(
                name: "UsersRoles");

            migrationBuilder.DropTable(
                name: "ApplyAttachmentTypes");

            migrationBuilder.DropTable(
                name: "FileInfos");

            migrationBuilder.DropTable(
                name: "CertificateDeliveryForms");

            migrationBuilder.DropTable(
                name: "Applies");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ApplyAims");

            migrationBuilder.DropTable(
                name: "ApplyDeliveryForms");

            migrationBuilder.DropTable(
                name: "ApplyDocTypes");

            migrationBuilder.DropTable(
                name: "ApplyEntryForms");

            migrationBuilder.DropTable(
                name: "ApplyLearnForms");

            migrationBuilder.DropTable(
                name: "ApplyPassportTypes");

            migrationBuilder.DropTable(
                name: "ApplyStatuses");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "SchoolType");

            migrationBuilder.DropTable(
                name: "LearnLevels");

            migrationBuilder.DropTable(
                name: "ReglamentEtaps");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
