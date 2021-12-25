namespace ExosHeroesManager.CustomControls;

/// <summary>
/// Interaction logic for EquipmentPanel.xaml
/// </summary>
public partial class EquipmentPanel : UserControl
{
    public static readonly DependencyProperty ClearCommandProperty = DependencyProperty.Register(
        nameof(ClearCommand), typeof(Commands.ClearEquipmentComand), typeof(EquipmentPanel), new(default(Commands.ClearEquipmentComand)));
    public static readonly DependencyProperty EquipmentPieceProperty = DependencyProperty.Register(
        nameof(EquipmentPiece), typeof(EquipmentPieceBase), typeof(EquipmentPanel), new(default(EquipmentPieceBase)));
    public static readonly DependencyProperty IsSelectingSetEffectProperty = DependencyProperty.Register(
        nameof(IsSelectingSetEffect), typeof(bool), typeof(EquipmentPanel), new(true));
    public static readonly DependencyProperty SwitchCommandProperty = DependencyProperty.Register(
        nameof(SwitchCommand), typeof(Commands.SwitchEquipmentCommand), typeof(EquipmentPanel), new(default(Commands.SwitchEquipmentCommand)));
    public static readonly DependencyProperty UploadCommandProperty = DependencyProperty.Register(
        nameof(UploadCommand), typeof(Commands.AddEquipmentCommand), typeof(EquipmentPanel), new(default(Commands.AddEquipmentCommand)));

    internal Commands.ClearEquipmentComand ClearCommand
    {
        get => (Commands.ClearEquipmentComand)GetValue(ClearCommandProperty);
        set => SetValue(ClearCommandProperty, value);
    }
    public EquipmentPieceBase EquipmentPiece
    {
        get => (EquipmentPieceBase)GetValue(EquipmentPieceProperty);
        set => SetValue(EquipmentPieceProperty, value);
    }
    public bool IsSelectingSetEffect
    {
        get => (bool)GetValue(IsSelectingSetEffectProperty);
        set => SetValue(IsSelectingSetEffectProperty, value);
    }
    internal Commands.SwitchEquipmentCommand SwitchCommand
    {
        get => (Commands.SwitchEquipmentCommand)GetValue(SwitchCommandProperty);
        set => SetValue(SwitchCommandProperty, value);
    }
    internal Commands.AddEquipmentCommand UploadCommand
    {
        get => (Commands.AddEquipmentCommand)GetValue(UploadCommandProperty);
        set => SetValue(UploadCommandProperty, value);
    }


    public EquipmentPanel()
    {
        InitializeComponent();

        ClearCommand = new(this);
        SwitchCommand = new(this);
        UploadCommand = new(this);
    }


    private void StarButton_Click(object sender, RoutedEventArgs e)
    {
        StarButton starbutton = sender as StarButton;
        starbutton.AttachedEquipmentStarCount = starbutton.Count;
    }
    private void SetEffectImage_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (((UIElement)sender).IsEnabled)
        {
            IsSelectingSetEffect = true;
        }
    }
    private void Button_Click(object sender, RoutedEventArgs e)
    {

        EquipmentPiece.Rank = EquipmentPiece.Rank switch
        {
            Ranking.Rare => Ranking.Legendary,
            Ranking.Legendary => Ranking.Fated,
            _ => Ranking.Rare
        };
    }

    private void TextBox_GotFocus(object sender, RoutedEventArgs e)
    {
        TextBox box = sender as TextBox;
        box.SelectAll();
        if (box.Text.Equals("0"))
        {
            box.Clear();
        }
    }
    private void TextBox_LostFocus(object sender, RoutedEventArgs e)
    {
        TextBox box = sender as TextBox;
        if (box.Text.Equals(string.Empty))
        {
            box.Text = "0";
        }
    }
}
