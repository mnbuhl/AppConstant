namespace AppConstant.Examples;

public class Anniversary : AppConstant<Anniversary, DateTime>
{
    public static Anniversary StartDate => Set(new DateTime(2023, 1, 1));
    public static Anniversary FiveYears => Set(new DateTime(2028, 1, 1));
    public static Anniversary TenYears => Set(new DateTime(2033, 1, 1));
    public static Anniversary FifteenYears => Set(new DateTime(2038, 1, 1));
    public static Anniversary TwentyYears => Set(new DateTime(2043, 1, 1));
}