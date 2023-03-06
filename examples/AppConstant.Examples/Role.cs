namespace AppConstant.Examples;

public class Role : AppConstant<Role, string>
{
    public static Role Admin => Set("admin");
    public static Role User => Set("user");
    public static Role Guest => Set("guest");
}