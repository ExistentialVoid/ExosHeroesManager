namespace ExosHeroesManager.CustomControls;

/// <summary>
/// Interaction logic for SetEffectMosaic.xaml
/// </summary>
public partial class SetEffectMosaic : UserControl
{
    public static readonly DependencyProperty ButtonGeometryProperty = DependencyProperty.Register(
        nameof(ButtonGeometry), typeof(EllipseGeometry), typeof(SetEffectMosaic), new(default(EllipseGeometry)));
    public static readonly DependencyProperty ButtonHeightProperty = DependencyProperty.Register(
        nameof(ButtonHeight), typeof(double), typeof(SetEffectMosaic), new(default(double)));
    public static readonly DependencyProperty ButtonWidthProperty = DependencyProperty.Register(
        nameof(ButtonWidth), typeof(double), typeof(SetEffectMosaic), new(default(double)));
    public static readonly DependencyProperty SelectedSetEffectProperty = DependencyProperty.Register(
        nameof(SelectedSetEffect), typeof(SetEffect), typeof(SetEffectMosaic), new(SetEffect.Unassigned));

    public EllipseGeometry ButtonGeometry
    {
        get => (EllipseGeometry)GetValue(ButtonGeometryProperty);
        set => SetValue(ButtonGeometryProperty, value);
    }
    public double ButtonHeight
    {
        get => (double)GetValue(ButtonHeightProperty);
        set => SetValue(ButtonHeightProperty, value);
    }
    public double ButtonWidth
    {
        get => (double)GetValue(ButtonWidthProperty);
        set => SetValue(ButtonWidthProperty, value);
    }
    public SetEffect SelectedSetEffect
    {
        get => (SetEffect)GetValue(SelectedSetEffectProperty);
        set => SetValue(SelectedSetEffectProperty, value);
    }


    public SetEffectMosaic()
    {
        InitializeComponent();

        SizeChanged += OnSizeChanged;
    }


    /// <summary>
    /// Return the origin of the SetEffectButton of a given set effect within the mosaic
    /// </summary>
    /// <param name="setEffect"></param>
    /// <returns></returns>
    public Point GetOrigin(SetEffect setEffect)
    {
        double Rw = Width / 2;
        double Rh = Height / 2;

        double rw = Rw * Math.Sin(Math.PI / 9) / (1 + Math.Sin(Math.PI / 9));
        double rh = Rh * Math.Sin(Math.PI / 9) / (1 + Math.Sin(Math.PI / 9));

        double bw = Rw - rw;
        double bh = Rh - rh;

        int randomWOffset = -16;
        int randomHOffset = -16;

        return setEffect switch
        {
            SetEffect.Breaker => new(Rw - bw * Math.Cos(Math.PI / 18) + randomWOffset, Rh + bh * Math.Sin(Math.PI / 18) + randomHOffset),
            SetEffect.Fortification => new(Rw - bw * Math.Cos(Math.PI / 6) + randomWOffset, Rh - bh * Math.Sin(Math.PI / 6) + randomHOffset),
            SetEffect.Vitality => new(Rw - bw * Math.Cos(7 * Math.PI / 18) + randomWOffset, Rh - bh * Math.Sin(7 * Math.PI / 18) + randomHOffset),
            SetEffect.Critical => new(Rw + bw * Math.Cos(7 * Math.PI / 18) + randomWOffset, Rh - bh * Math.Sin(7 * Math.PI / 18) + randomHOffset),
            SetEffect.Dash => new(Rw + bw * Math.Cos(Math.PI / 6) + randomWOffset, Rh - bh * Math.Sin(Math.PI / 6) + randomHOffset),
            SetEffect.Sniper => new(Rw + bw * Math.Cos(Math.PI / 18) + randomWOffset, Rh + bh * Math.Sin(Math.PI / 18) + randomHOffset),
            SetEffect.Destruction => new(Rw + bw * Math.Cos(5 * Math.PI / 18) + randomWOffset, Rh + bh * Math.Sin(5 * Math.PI / 18) + randomHOffset),
            SetEffect.RedBlood => new(Rw + randomWOffset, 2 * Rh - rh + randomHOffset),
            SetEffect.Resistance => new(Rw - bw * Math.Cos(5 * Math.PI / 18) + randomWOffset, Rh + bh * Math.Sin(5 * Math.PI / 18) + randomHOffset),
            SetEffect.Retaliation => new(Rw + randomWOffset, 2 * Rh - 3 * rh + randomHOffset),
            _ => throw new NotImplementedException()
        };
    }
    public void OnSizeChanged(object sender, EventArgs e)
    {
        double w = Math.Sin(Math.PI / 9) / (1 + Math.Sin(Math.PI / 8)) * Width;
        ButtonWidth = w;
        double h = Math.Sin(Math.PI / 9) / (1 + Math.Sin(Math.PI / 8)) * Height;
        ButtonHeight = h;
        ButtonGeometry = new(new Point(w / 2, h / 2), w / 2, h / 2);

        Point p = GetOrigin(SetEffect.Breaker);
        Canvas.SetLeft(BreakerButton, p.X);
        Canvas.SetTop(BreakerButton, p.Y);
        p = GetOrigin(SetEffect.Fortification);
        Canvas.SetLeft(FortificationButton, p.X);
        Canvas.SetTop(FortificationButton, p.Y);
        p = GetOrigin(SetEffect.Vitality);
        Canvas.SetLeft(VitalityButton, p.X);
        Canvas.SetTop(VitalityButton, p.Y);
        p = GetOrigin(SetEffect.Critical);
        Canvas.SetLeft(CriticalButton, p.X);
        Canvas.SetTop(CriticalButton, p.Y);
        p = GetOrigin(SetEffect.Dash);
        Canvas.SetLeft(DashButton, p.X);
        Canvas.SetTop(DashButton, p.Y);
        p = GetOrigin(SetEffect.Sniper);
        Canvas.SetLeft(SniperButton, p.X);
        Canvas.SetTop(SniperButton, p.Y);
        p = GetOrigin(SetEffect.Destruction);
        Canvas.SetLeft(DestructionButton, p.X);
        Canvas.SetTop(DestructionButton, p.Y);
        p = GetOrigin(SetEffect.RedBlood);
        Canvas.SetLeft(RedBloodButton, p.X);
        Canvas.SetTop(RedBloodButton, p.Y);
        p = GetOrigin(SetEffect.Resistance);
        Canvas.SetLeft(ResistanceButton, p.X);
        Canvas.SetTop(ResistanceButton, p.Y);
        p = GetOrigin(SetEffect.Retaliation);
        Canvas.SetLeft(RetaliationButton, p.X);
        Canvas.SetTop(RetaliationButton, p.Y);
    }

    private void BreakerButton_Click(object sender, RoutedEventArgs e)
    {
        SelectedSetEffect = SetEffect.Breaker;
        Release();
    }
    private void FortificationButton_Click(object sender, RoutedEventArgs e)
    {
        SelectedSetEffect = SetEffect.Fortification;
        Release();
    }
    private void VitalityButton_Click(object sender, RoutedEventArgs e)
    {
        SelectedSetEffect = SetEffect.Vitality;
        Release();
    }
    private void CriticalButton_Click(object sender, RoutedEventArgs e)
    {
        SelectedSetEffect = SetEffect.Critical;
        Release();
    }
    private void DashButton_Click(object sender, RoutedEventArgs e)
    {
        SelectedSetEffect = SetEffect.Dash;
        Release();
    }
    private void SniperButton_Click(object sender, RoutedEventArgs e)
    {
        SelectedSetEffect = SetEffect.Sniper;
        Release();
    }
    private void DestructionButton_Click(object sender, RoutedEventArgs e)
    {
        SelectedSetEffect = SetEffect.Destruction;
        Release();
    }
    private void RedBloodButton_Click(object sender, RoutedEventArgs e)
    {
        SelectedSetEffect = SetEffect.RedBlood;
        Release();
    }
    private void ResistanceButton_Click(object sender, RoutedEventArgs e)
    {
        SelectedSetEffect = SetEffect.Resistance;
        Release();
    }
    private void RetaliationButton_Click(object sender, RoutedEventArgs e)
    {
        SelectedSetEffect = SetEffect.Retaliation;
        Release();
    }

    private void Release()
    {
        ((EquipmentPanel)((Grid)Parent).Parent).IsSelectingSetEffect = false;
    }
}
