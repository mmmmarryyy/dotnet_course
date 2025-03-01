[TestClass]
public class TipControllerTests
{
    [TestMethod]
    public void CalculateTip_ValidInput_DisplaysCorrectTotal()
    {
        var model = new TipModel();
        var mockView = new MockTipView
        {
            BillAmountInput = 200,
            TipPercentageInput = 10
        };
        var controller = new TipController(model, mockView);

        controller.CalculateTip();

        Assert.AreEqual(220, mockView.TotalResult);
        Assert.IsNull(mockView.ErrorMessage);
    }

    [TestMethod]
    public void CalculateTip_NegativeBill_DisplaysError()
    {
        var model = new TipModel();
        var mockView = new MockTipView
        {
            BillAmountInput = -100,
            TipPercentageInput = 10
        };
        var controller = new TipController(model, mockView);

        controller.CalculateTip();

        Assert.AreEqual("Bill amount must be positive", mockView.ErrorMessage);
    }

    [TestMethod]
    public void CalculateTip_PercentageOver100_DisplaysError()
    {
        var model = new TipModel();
        var mockView = new MockTipView
        {
            BillAmountInput = 100,
            TipPercentageInput = 101
        };
        var controller = new TipController(model, mockView);

        controller.CalculateTip();

        Assert.AreEqual("Tip percentage must be between 0 and 100", mockView.ErrorMessage);
    }

    [TestMethod]
    public void CalculateTip_NegativePercentage_DisplaysError()
    {
        var model = new TipModel();
        var mockView = new MockTipView
        {
            BillAmountInput = 100,
            TipPercentageInput = -1
        };
        var controller = new TipController(model, mockView);

        controller.CalculateTip();

        Assert.AreEqual("Tip percentage must be between 0 and 100", mockView.ErrorMessage);
    }

    [TestMethod]
    public void CalculateTip_InvalidNumberFormat_DisplaysError()
    {
        var model = new TipModel();
        var mockView = new MockTipViewThrowsFormatException();
        var controller = new TipController(model, mockView);

        controller.CalculateTip();

        Assert.AreEqual("Invalid input. Please enter a valid number", mockView.ErrorMessage);
    }
}

public class MockTipViewThrowsFormatException : MockTipView
{
    public override decimal GetBillAmount() => throw new FormatException();
}