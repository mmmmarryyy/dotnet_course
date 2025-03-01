public class MockTipView : TipView
{
    public decimal BillAmountInput { get; set; }
    public decimal TipPercentageInput { get; set; }
    public string ErrorMessage { get; set; }
    public decimal TotalResult { get; set; }

    public override decimal GetBillAmount() => BillAmountInput;
    public override decimal GetTipPercentage() => TipPercentageInput;
    public override void DisplayResult(decimal total) => TotalResult = total;
    public override void DisplayError(string message) => ErrorMessage = message;
}