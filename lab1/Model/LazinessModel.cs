using LazyCalculator.Interface;

namespace LazyCalculator.Model;

public class LazinessModel : IObservable
{
    private readonly List<IObserver> _observers = [];

    private static readonly string[] ProductiveQuotes =
    [
        "Ты вообще человек? Иди отдохни, трудоголик.",
        "Подозрительно чисто... Вы случайно не нейросеть?",
        "Все задачи закрыты. Вселенная в шоке от вашей продуктивности.",
        "План перевыполнен. Срочно выпейте кофе и ничего не делайте."
    ];

    private static readonly string[] LightQuotes =
    [
        "Неплохо, но пара задач всё же улетела в чёрную дыру.",
        "Лёгкое прикосновение лени лишь придаёт продуктивности шарм.",
        "Почти идеально! Главное — вовремя остановиться.",
        "80% успеха — это просто появиться. Ты перевыполнил норму."
    ];

    private static readonly string[] ModerateQuotes =
    [
        "Диван сегодня победил по очкам, но ты хотя бы пытался.",
        "Завтра — самый загруженный день недели, судя по твоим планам.",
        "Прокрастинация — это искусство делать всё что угодно, кроме нужного.",
        "Ты сделал ровно столько, чтобы совесть просто ворчала, а не орала."
    ];

    private static readonly string[] CriticalQuotes =
    [
        "Великий магистр прокрастинации. Планы существуют, чтобы на них смотреть.",
        "Если бы лень сжигала калории, ты бы уже растворился в воздухе.",
        "Твоё тотемное животное сегодня — ленивец в глубокой коме.",
        "Дела подождут: Вселенная бесконечна, а сериал сам себя не досмотрит.",
        "План был надёжен как швейцарские часы, но вмешалась гравитация кровати."
    ];

    public int PlannedTasks { get; private set; }
    public int CompletedTasks { get; private set; }
    public double LazinessCoefficient { get; private set; }
    public string ProcrastinationLevel { get; private set; } = "Нет данных";
    public string SarcasticPhrase { get; private set; } = "Данные ещё не введены.";

    public void Attach(IObserver observer) => _observers.Add(observer);
    public void Detach(IObserver observer) => _observers.Remove(observer);

    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.Update();
        }
    }

    public void Calculate(int planned, int completed)
    {
        PlannedTasks = planned;
        CompletedTasks = completed;

        int uncompleted = Math.Max(0, planned - completed);
        LazinessCoefficient = Math.Round((double)uncompleted / planned, 2);

        (ProcrastinationLevel, SarcasticPhrase) = LazinessCoefficient switch
        {
            <= 0.0 => ("Продуктивный робот", GetRandomQuote(ProductiveQuotes)),
            <= 0.3 => ("Лёгкая лень", GetRandomQuote(LightQuotes)),
            <= 0.7 => ("Умеренная прокрастинация", GetRandomQuote(ModerateQuotes)),
            _ => ("Критический уровень", GetRandomQuote(CriticalQuotes))
        };

        Notify();
    }

    private static string GetRandomQuote(string[] quotes) =>
        quotes[Random.Shared.Next(quotes.Length)];
}