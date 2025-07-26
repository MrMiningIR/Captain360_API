namespace Capitan360.Domain.Constants;

public static class ConstantNames
{


    // Path Struct Name
    public const string PathStructType1Name = "تمبر و بارنامه - بارخروجی";
    public const string PathStructType2Name = "هزینه بسته بندی - بارخروجی";
    public const string PathStructType3Name = "هزینه جمع آوری - بارخروجی";
    public const string PathStructType4Name = "متفرقه مبدا - بارخروجی";
    public const string PathStructType5Name = "هزینه قیمت گذاری - بارخروجی";
    public const string PathStructType6Name = "هزینه متفرقه خروجی 1";
    public const string PathStructType7Name = "هزینه متفرقه خروجی 2";
    public const string PathStructType8Name = "هزینه متفرقه خروجی 3";
    public const string PathStructType9Name = "هزینه توزیع - بارخروجی";
    public const string PathStructType10Name = "هزینه متفرقه مقصد - بار خروجی";

    // weight Names

    public const string WeightTypeNormalName = "نرمال";
    public const string WeightTypeMinName = "وزن کمینه";
    public const string WeightType1Name = "وزن اول";
    public const string WeightType2Name = "وزن دوم";
    public const string WeightType3Name = "وزن سوم";
    public const string WeightType4Name = "وزن چهارم";
    public const string WeightType5Name = "وزن پنجم";
    public const string WeightType6Name = "وزن ششم";
    public const string WeightType7Name = "وزن هفتم";
    public const string WeightType8Name = "وزن هشتم";
    public const string WeightType9Name = "وزن نهم";
    public const string WeightType10Name = "وزن دهم";
    public const string WeightType11Name = "وزن یازدهم";



    // Rate Names

    public const string RateOne = "نرخ اول";
    public const string RateTwo = "نرخ دوم";
    public const string RateThree = "نرخ سوم";
    public const string RateFour = "نرخ چهارم";
    public const string RateFive = "نرخ پنجم";
    public const string RateSix = "نرخ ششم";
    public const string RateSeven = "نرخ هفتم";
    public const string RateEight = "نرخ هشتم";
    public const string RateNine = "نرخ نهم";
    public const string RateTen = "نرخ دهم";


    // Custom Claim Names
    public const string SessionId = "SessionId";
    public const string CompanyId = "CompanyId";
    public const string CompanyType = "CompanyType";
    public const string GroupId = "GroupId";
    public const string Permissions = "Permissions";
    public const string Permission = "Permission";
    public const string IsParentCompany = "IsParentCompany";
    public const string CompanyName = "CompanyName";



    // RoleName

    public const string UserTableName = "Users";
    public const string RoleTableName = "Roles";
    public const string UserRole = "User";
    public const string ManagerRole = "Manager";
    public const string SuperAdminRole = "SuperAdmin";

    // Default Permission

    public const string ViewUsers = "ViewUsers";
    public const string EditUsers = "EditUsers";
    public const string DeleteUsers = "DeleteUsers";
    public const string CreateRoles = "CreateRoles";
    public const string AssignRoles = "AssignRoles";
    public const string ViewProducts = "ViewProducts";
    public const string EditProducts = "EditProducts";
    public const string DeleteProducts = "DeleteProducts";


    // Default Groups

    public const string AdminGroup = "AdminGroup";
    public const string ViewerGroup = "ViewerGroup";
    public const string EditorGroup = "EditorGroup";


    public const string ManagerUser = "ManagerUser";
    public const string AdministratorUser = "AdministratorUser";
    public const string User = "User";



    public const string SamplePhone1 = "09155165067";
    public const string SamplePhone2 = "09157369036";
    public const string SamplePhone3 = "09353762674";


    // Cache Prefix

    public const string CachePrefix = "Cap:";
    public const string NormalUser = "کاربرعادی";
    public const string SpecialUser = "کاربرویژه";


    public const string PermissionsTable = "Permissions";
    public const string Action = "متد";
    public const string Controller = "کنترلر";


    public const string Pvc = "PermissionVersionControl";
    public const string DeactivatedAccountMessage = "حساب شما فعال نیست";
    public const string ChangedAccessMessage = "دسترسی های شما تغییر کرده است.مجددا وارد شوید";
    public const string UserNotValidMessage = "کاربر معتبر نیست";
    public const string UserHasNoAccessMessage = "کاربر دسترسی معتبری ندارد";
    public const string UserAlreadyLoggined = "کاربر دیگری با مشخصات ورود شما وارد شده است. مجدد وارد شوید.";
    public const string IdentifierHeaderName = "IdentifierHeader";
}