using FFImageLoading.Maui;

namespace faster;

public delegate void CallBack();
public class Player : Animacao
{
    public Player (CachedImageView poggers): base (poggers)
    {
        for (int i = 1; i <= 12; i++)
        {
            Animacao1.Add($"bixim{i.ToString("D2")}.png");
        }
        for (int i2 = 1; i2 <= 2; i2++)
        {
            Animacao2.Add($"minimente{i2.ToString("D2")}.png");
            SetAnimacaoAtiva(1);
        }
    }
    public void Die()
    {
        Loop = false;
        SetAnimacaoAtiva(2);
    }
    public void Run()
    {
        Loop = true;
        SetAnimacaoAtiva(1);
        Play();
    }
     public void MoveY(int s)
    {
        ImageView.TranslationY += s;
    }

    public double GetY()
    {
        return ImageView.TranslationY;
    }

    public void SetY(double a)
    {
        ImageView.TranslationY = a;
    }
}