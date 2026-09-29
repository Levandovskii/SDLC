namespace LazyCalculator.View;

public sealed class InputDataForm : Form
{
    private readonly TextBox _txtPlanned = new();
    private readonly TextBox _txtCompleted = new();
    private readonly Button _btnSubmit = new()
    {
        Text = "Рассчитать",
        DialogResult = DialogResult.OK,
        Dock = DockStyle.Fill,
        Cursor = Cursors.Hand
    };

    private readonly Button _btnCancel = new()
    {
        Text = "Отмена",
        DialogResult = DialogResult.Cancel,
        Dock = DockStyle.Fill,
        Cursor = Cursors.Hand
    };
    
    public string PlannedInput => _txtPlanned.Text.Trim();
    public string CompletedInput => _txtCompleted.Text.Trim();
    
    public InputDataForm(int? lastPlanned, int? lastCompleted)
    {
        InitializeWindow();
        SetupLayout();
        RestoreLastData(lastPlanned, lastCompleted);
    }

    private void InitializeWindow()
    {
        Text = "Ввод данных";
        ClientSize = new Size(390, 165);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        BackColor = Color.FromArgb(245, 246, 248);

        AcceptButton = _btnSubmit;
        CancelButton = _btnCancel;
    }
    
    private void SetupLayout()
    {
        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 3,
            Padding = new Padding(16, 16, 16, 12)
        };

        layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));

        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36f));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36f));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

        var lblPlanned = new Label
        {
            Text = "Запланировано дел:",
            AutoSize = true,
            Anchor = AnchorStyles.Left,
            Margin = new Padding(0, 0, 10, 0)
        };

        var lblCompleted = new Label
        {
            Text = "Выполнено дел:",
            AutoSize = true,
            Anchor = AnchorStyles.Left,
            Margin = new Padding(0, 0, 10, 0)
        };

        _txtPlanned.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtCompleted.Anchor = AnchorStyles.Left | AnchorStyles.Right;

        var buttonsLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Bottom,
            Height = 34,
            ColumnCount = 2,
            RowCount = 1,
            Margin = new Padding(0)
        };
        buttonsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
        buttonsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));

        _btnSubmit.Margin = new Padding(0, 0, 4, 0);
        _btnCancel.Margin = new Padding(4, 0, 0, 0);

        buttonsLayout.Controls.Add(_btnSubmit, 0, 0);
        buttonsLayout.Controls.Add(_btnCancel, 1, 0);

        layout.Controls.Add(lblPlanned, 0, 0);
        layout.Controls.Add(_txtPlanned, 1, 0);

        layout.Controls.Add(lblCompleted, 0, 1);
        layout.Controls.Add(_txtCompleted, 1, 1);
        
        layout.Controls.Add(buttonsLayout, 0, 2);
        layout.SetColumnSpan(buttonsLayout, 2);

        Controls.Add(layout);
    }
    
    private void RestoreLastData(int? lastPlanned, int? lastCompleted)
    {
        if (lastPlanned.HasValue)
        {
            _txtPlanned.Text = lastPlanned.Value.ToString();
        }

        if (lastCompleted.HasValue)
        {
            _txtCompleted.Text = lastCompleted.Value.ToString();
        }
    }
}