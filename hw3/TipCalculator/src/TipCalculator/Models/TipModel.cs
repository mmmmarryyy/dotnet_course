public class TipModel
{
    public decimal BillAmount { get; set; }
    public decimal TipPercentage { get; set; }

    public decimal CalculateTotal()
    {
        if (BillAmount <= 0)
            throw new ArgumentException("Bill amount must be positive");
        if (TipPercentage < 0 || TipPercentage > 100)
            throw new ArgumentException("Tip percentage must be between 0 and 100");

        return BillAmount * (1 + TipPercentage / 100);
    }
}