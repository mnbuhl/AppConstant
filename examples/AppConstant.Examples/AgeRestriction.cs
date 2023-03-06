namespace AppConstant.Examples;

public class AgeRestriction : AppConstant<AgeRestriction, int>
{
    public static AgeRestriction None => Set(0);
    public static AgeRestriction Eighteen => Set(18);
    public static AgeRestriction TwentyOne => Set(21);
}