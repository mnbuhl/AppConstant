namespace AppConstant.Benchmarks.Fixtures;

public class Role : AppConstant<Role, string>
{
    public static Role Admin => Set("Admin");
    public static Role User => Set("User");
    public static Role Guest => Set("Guest");
}