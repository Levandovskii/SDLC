using LazyCalculator.Model;

namespace LazyCalculator.Controller;

public class LazinessController(LazinessModel model)
{
    private readonly LazinessModel _model = model;

    public bool TryProcessInput(string plannedRaw, string completedRaw, out string errorMessage)
    {
        if (!int.TryParse(plannedRaw, out int planned) || planned <= 0)
        {
            errorMessage = "Запланированное количество дел должно быть целым положительным числом (> 0).";
            return false;
        }

        if (!int.TryParse(completedRaw, out int completed) || completed < 0)
        {
            errorMessage = "Количество выполненных дел не может быть отрицательным.";
            return false;
        }

        if (completed > planned)
        {
            errorMessage = "Выполненных дел не может быть больше, чем запланированных.";
            return false;
        }

        errorMessage = string.Empty;
        _model.Calculate(planned, completed);
        return true;
    }
}