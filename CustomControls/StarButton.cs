namespace ExosHeroesManager.CustomControls;

public class StarButton : Button
{
    public static readonly DependencyProperty AttachedEquipmentStarCountProperty = DependencyProperty.Register(
        nameof(AttachedEquipmentStarCount), typeof(int), typeof(StarButton), new(default(int)));
    public static readonly DependencyProperty CountProperty = DependencyProperty.Register(
        nameof(Count), typeof(int), typeof(StarButton), new(default(int)));
    public static readonly DependencyProperty StarGeometryProperty = DependencyProperty.Register(
        nameof(StarGeometry), typeof(PathGeometry), typeof(StarButton), new(default(PathGeometry)));

    public int AttachedEquipmentStarCount
    {
        get => (int)GetValue(AttachedEquipmentStarCountProperty);
        set => SetValue(AttachedEquipmentStarCountProperty, value);
    }
    public int Count
    {
        get => (int)GetValue(CountProperty);
        set => SetValue(CountProperty, value);
    }
    public PathGeometry StarGeometry
    {
        get => (PathGeometry)GetValue(StarGeometryProperty);
        set => SetValue(StarGeometryProperty, value);
    }

    public StarButton()
    {
        SizeChanged += (s, e) => StarGeometry = GetShapeGeometry(Width, Height);
    }


    public static PathGeometry GetShapeGeometry(double width, double height)
    {
        PathSegmentCollection pathSegments = new()
        {
            new LineSegment(new(0.382 * width, 0.382 * height), true),
            new LineSegment(new(0.5 * width, 0), true),
            new LineSegment(new(0.618 * width, 0.382 * height), true),
            new LineSegment(new(width, 0.382 * height), true),
            new LineSegment(new(0.691 * width, 0.618 * height), true),
            new LineSegment(new(0.809 * width, height), true),
            new LineSegment(new(0.5 * width, 0.764 * height), true),
            new LineSegment(new(0.191 * width, height), true),
            new LineSegment(new(0.309 * width, 0.618 * height), true),
        };
        PathFigureCollection pathFigures = new()
        {
            new PathFigure(new(0, 0.382 * height), pathSegments, true)
        };

        return new(pathFigures);
    }
}
