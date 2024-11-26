using FFImageLoading.Maui;

namespace faster;

public class Inimigo
{
    Image ImageView;

    public Inimigo(Image poggers)
    {
        ImageView = poggers;
    }

    public void MoveX(double s)
    {
        ImageView.TranslationX -= s;
    }

    public double GetX()
    {
        return ImageView.TranslationX;
    }

    public void Reset()
    {
        ImageView.TranslationX = 500;
    }
}