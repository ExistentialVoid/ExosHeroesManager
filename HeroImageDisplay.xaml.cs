using System.Drawing;

namespace ExosHeroesManager;

/// <summary>
/// Interaction logic for HeroImageDisplay.xaml
/// </summary>
public partial class HeroImageDisplay : Window
{
    public static readonly DependencyProperty HeroProperty = DependencyProperty.Register(
        nameof(Hero), typeof(Hero), typeof(HeroPresenter), new(default(Hero)));

    public Hero Hero
    {
        get => (Hero)GetValue(HeroProperty);
        set => SetValue(HeroProperty, value);
    }

    public HeroImageDisplay(Hero hero)
    {
        InitializeComponent();

        Hero = hero;
        LoadImage();
    }

    private void LoadImage()
    {
        Converters.HeroToImageConverter converter = new();
        object[] values = new object[] { Hero, Hero.Fatecore };
        Image.Source = (ImageSource)converter.Convert(values, typeof(byte[]), null, System.Globalization.CultureInfo.InvariantCulture);
    }
    private void Window_Deactivated(object sender, EventArgs e)
    {
        try
        {
            Close();
        }
        catch { }
    }

    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        Close();
    }
}
