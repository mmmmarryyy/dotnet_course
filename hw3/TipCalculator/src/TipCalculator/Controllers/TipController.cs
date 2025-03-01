public class TipController
{
    private readonly TipModel _model;
    private readonly TipView _view;

    public TipController(TipModel model, TipView view)
    {
        _model = model;
        _view = view;
    }

    public void CalculateTip()
    {
        try
        {
            _model.BillAmount = _view.GetBillAmount();
            _model.TipPercentage = _view.GetTipPercentage();
            decimal total = _model.CalculateTotal();
            _view.DisplayResult(total);
        }
        catch (FormatException)
        {
            _view.DisplayError("Invalid input. Please enter a valid number");
        }
        catch (ArgumentException ex)
        {
            _view.DisplayError(ex.Message);
        }
        catch (Exception)
        {
            _view.DisplayError("An unexpected error occurred");
        }
    }
}