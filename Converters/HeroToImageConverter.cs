using System.Globalization;
using System.Resources;
using System.Windows.Data;

namespace ExosHeroesManager.Converters;

internal class HeroToImageConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length != 2)
        {
            return Array.Empty<byte>();
        }

        if (values[0] is not Hero hero)
        {
            throw new ArgumentException($"{values[0].GetType()} is not supported.");
        }

        if (hero == null)
        {
            return Array.Empty<byte>();
        }

        string imageName = hero.Name;
        if (hero.Fatecore != null)
        {
            imageName += $" {hero.Fatecore.Name}";
        }
        imageName = imageName.Replace(' ', '_');

        ResourceManager resourceManager = new("ExosHeroesManager.HeroImages", typeof(HeroImages).Assembly);
        byte[] imageSource = (byte[])resourceManager.GetObject(imageName);
        ImageSourceConverter c = new();

        return imageSource == null ? Array.Empty<byte>() : (ImageSource)c.ConvertFrom(imageSource);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
