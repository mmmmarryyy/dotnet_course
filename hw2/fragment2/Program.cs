/* ORIGINAL CODE:
public class gMethods
{
    public string Name;
    private int price; 
    private int amount; 
    private string platform;

    public void PrintPack()
    {
        this.PrintBanner();

        // Print details.
        Console.WriteLine("name: " + this.name); 
        Console.WriteLine("amount: " + this.GetOutstanding()); 
        Console.WriteLine("price: " + this.price); 
        Console.WriteLine("platform: " + platform);
    }

    float GetAmnt()
    {
        if ((platform.ToUpper().IndexOf("PC") > -1) &&
            (Name.ToUpper().IndexOf("XX") > -1) && amount > 0) 
            return amount * 0.956;
        double temp = amount * price; 
        Console.WriteLine(temp);
        temp = 0.8 * amount * price; 
        Console.WriteLine(temp); 
        return -1;
    }
}
*/

public class GameMethods
{
    public string Name { get; set; }
    private int Price { get; set; }
    private int Amount { get; set; }
    private string Platform { get; set; }

    private const double Discount = 0.8;
    private const float AmountDecreaseCoefficient = 0.956f;

    public void PrintPack()
    {
        PrintBanner();
        PrintDetails();
    }

    public void PrintDetails()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Amount: {Amount}");
        Console.WriteLine($"Price: {Price}");
        Console.WriteLine($"Platform: {Platform}");
    }

    private float GetAmount()
    {
        if (Platform.ToUpper().Contains("PC") && Name.ToUpper().Contains("XX") && Amount > 0)
        {
            return AmountDecreaseCoefficient * Amount;
        }

        double temp = Amount * Price;
        Console.WriteLine(temp);
        Console.WriteLine(Discount * temp);

        return -1;
    }
}
