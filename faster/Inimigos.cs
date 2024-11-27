using FFImageLoading.Maui;

namespace faster;

public class Inimigos
{
    List<Inimigo> inimigos = new List<Inimigo>();
    Inimigo atual = null;
    double minX = 0;

    public Inimigos(double noggers)
    {
        minX = noggers;
    }

    public void Add(Inimigo noggers)
    {
        inimigos.Add(noggers);

        if (atual == null)
        {
            atual = noggers;
            Iniciar();
        }
    }

    public void Iniciar()
    {
        foreach (var e in inimigos)
        {
            e.Reset();
        }
    }

    void Gerencia()
    {
        if (atual.GetX() < minX)
        {
            Iniciar();
            var r = Random.Shared.Next(0, inimigos.Count);
            atual = inimigos[r];
        }
    }

    public void Desenha(int veloc)
    {
        atual.MoveX(veloc);
        Gerencia();
    }
}