public class Program
{
    public static void Main()
    {
        TipModel model = new TipModel();
        TipView view = new TipView();
        TipController controller = new TipController(model, view);

        controller.CalculateTip();
    }
}