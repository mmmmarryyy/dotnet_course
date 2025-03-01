public class TipView
{
    public virtual decimal GetBillAmount()
    {
        Console.Write("Enter bill amount: ");
        return decimal.Parse(Console.ReadLine());
    }

    public virtual decimal GetTipPercentage()
    {
        Console.Write("Enter tip percentage: ");
        return decimal.Parse(Console.ReadLine());
    }

    public virtual void DisplayResult(decimal total)
    {
        Console.WriteLine($"Total to pay: {total:C}");
    }

    public virtual void DisplayError(string message)
    {
        Console.WriteLine($"Error: {message}");
    }
}