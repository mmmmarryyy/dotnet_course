[TestClass]
public class TipModelTests
{
    [TestMethod]
    public void CalculateTotal_ValidInput_ReturnsCorrectTotal()
    {
        var model = new TipModel { BillAmount = 100, TipPercentage = 15 };
        Assert.AreEqual(115, model.CalculateTotal());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CalculateTotal_NegativeBill_ThrowsException()
    {
        var model = new TipModel { BillAmount = -100, TipPercentage = 10 };
        model.CalculateTotal();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CalculateTotal_PercentageOver100_ThrowsException()
    {
        var model = new TipModel { BillAmount = 100, TipPercentage = 150 };
        model.CalculateTotal();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CalculateTotal_NegativePercentage_ThrowsException()
    {
        var model = new TipModel { BillAmount = 100, TipPercentage = -10 };
        model.CalculateTotal();
    }
}