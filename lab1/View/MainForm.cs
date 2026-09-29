using LazyCalculator.Controller;
using LazyCalculator.Interface;
using LazyCalculator.Model;

namespace LazyCalculator.View;

public sealed class MainForm : Form, IObserver
{
    private readonly LazinessModel _model;
    private readonly LazinessController _controller;

    private int? _lastPlanned;
    private int? _lastCompleted;

    private readonly Button _btnInputData = new()
    {
        Text = "Ввести данные",
        AutoSize = true,
        Padding = new Padding(16, 6, 16, 6),
        Cursor = Cursors.Hand
    };

    private readonly Label _lblCoefficient = new()
    {
        AutoSize = true,
        Font = new Font("Segoe UI", 13f, FontStyle.Bold),
        ForeColor = Color.FromArgb(25, 35, 45),
        Margin = new Padding(0, 0, 0, 6)
    };

    private readonly Label _lblLevel = new()
    {
        AutoSize = true,
        Font = new Font("Segoe UI Semibold", 10.5f, FontStyle.Bold),
        ForeColor = Color.FromArgb(70, 80, 95),
        Margin = new Padding(0, 0, 0, 8)
    };

    private readonly Label _lblPhrase = new()
    {
        AutoSize = true,
        ForeColor = Color.FromArgb(90, 100, 110),
        Font = new Font("Segoe UI", 9.5f, FontStyle.Italic),
        MaximumSize = new Size(460, 0)
    };

    public MainForm(LazinessModel model, LazinessController controller)
    {
        _model = model;
        _controller = controller;

        _model.Attach(this);

        InitializeWindow();
        SetupLayout();
        Update();
    }

    private void InitializeWindow()
    {
        Text = "Калькулятор лени";
        ClientSize = new Size(520, 400);
        MinimumSize = new Size(520, 400);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.FromArgb(245, 246, 248);
    }

    private void SetupLayout()
    {
        var mainLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            Padding = new Padding(16)
        };

        mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

        _btnInputData.Click += OnInputDataClicked;

        var cardPanel = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle,
            Padding = new Padding(16),
            Margin = new Padding(0, 12, 0, 0)
        };

        var contentLayout = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoScroll = true
        };

        contentLayout.Controls.AddRange([_lblCoefficient, _lblLevel, _lblPhrase]);
        cardPanel.Controls.Add(contentLayout);

        mainLayout.Controls.Add(_btnInputData, 0, 0);
        mainLayout.Controls.Add(cardPanel, 0, 1);

        Controls.Add(mainLayout);
    }

    private void OnInputDataClicked(object? sender, EventArgs e)
    {
        using var inputForm = new InputDataForm(_lastPlanned, _lastCompleted);
        if (inputForm.ShowDialog(this) == DialogResult.OK)
        {
            if (_controller.TryProcessInput(
                    inputForm.PlannedInput,
                    inputForm.CompletedInput,
                    out string error
                ))
            {
                _lastPlanned = int.Parse(inputForm.PlannedInput);
                _lastCompleted = int.Parse(inputForm.CompletedInput);
            }
            else
            {
                MessageBox.Show(
                    this,
                    error,
                    "Ошибка ввода",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
            }
        }
    }

    public new void Update()
    {
        _lblCoefficient.Text = $"Коэффициент лени: {_model.LazinessCoefficient:P0}";
        _lblLevel.Text = $"Уровень прокрастинации: {_model.ProcrastinationLevel}";
        _lblPhrase.Text = $"Вердикт: «{_model.SarcasticPhrase}»";
    }
}