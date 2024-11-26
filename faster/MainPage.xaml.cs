namespace faster;

public partial class MainPage : ContentPage
{
	Player player;
	Inimigos inimigos;

	bool Morto = false;
	bool Pulando = false;
	const int TempoEntreFrames = 25;
	int Velocidade = 0;
	int Velocidade01 = 0;
	int Velocidade02 = 0;
	int Velocidade03 = 0;
	int LarguraJanela = 0;
	int AlturaJanela = 0;
	const int ForcaGravidade = 6;
	bool EstaNoChao = true;
	bool EstaNoAr = false;
	bool EstaPulando = false;
	int TempoPulando = 0;
	int TempoNoAr = 0;
	const int MaxTempoPulando = 6;
	const int MaxTempoAr = 4;
	const int ForcaPulo = 8;

	public MainPage()
	{
		InitializeComponent();
		player = new Player(ImgbRoberto);
		player.Run();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		Desenha();
	}
	void AplicaGravidade()
	{

		if (player.GetY() < 0)
		{
			player.MoveY(ForcaGravidade);
		}
		else if (player.GetY() >= 0)
		{
			player.SetY(0);
			EstaNoChao = true;
		}
	}

	void ClicaNaTela(object i, TappedEventArgs a)
	{
		EstaPulando = true;
	}

	void AplicaPulo()
	{
		EstaNoChao = false;
		if (EstaPulando && TempoPulando >= MaxTempoPulando)
		{
			EstaPulando = false;
			EstaNoAr = true;
			TempoNoAr = 0;
		}
		else if (EstaNoAr && TempoNoAr >= MaxTempoAr)
		{
			EstaPulando = false;
			EstaNoAr = false;
			TempoPulando = 0;
			TempoNoAr = 0;
		}
		else if (EstaPulando && TempoPulando < MaxTempoPulando)
		{
			player.MoveY(-ForcaPulo);
			TempoNoAr++;
		}
		else if (EstaNoAr)
		{
			TempoNoAr++;
		}
	}

	async Task Desenha()
	{
		while (!Morto)
		{
			if (inimigos != null)
			{
				inimigos.Desenha(Velocidade);
			}
			
			if (!EstaPulando && !EstaNoAr)
			{
				AplicaGravidade();
				player.Desenha();
			}
			else
			{
				AplicaPulo();
				
			}
			await Task.Delay(TempoEntreFrames);
		}
	}

	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		CorrigeTamanhoCenario(w, h);
		CalculaVelocidade(w);
	}

	void CalculaVelocidade(double w)
	{
		Velocidade01 = (int)(w * 0.001);
		Velocidade02 = (int)(w * 0.004);
		Velocidade03 = (int)(w * 0.007);
		Velocidade = (int)(w * 0.01);
	}

	void CorrigeTamanhoCenario(double w, double h)
	{
		foreach (var a in primeiro.Children)
			(a as Image).WidthRequest = w;

		foreach (var a in segundo.Children)
			(a as Image).WidthRequest = w;
		
		foreach (var a in terceiro.Children)
			(a as Image).WidthRequest = w;
		
		foreach (var a in quarto.Children)
			(a as Image).WidthRequest = w;

		primeiro.WidthRequest = w * 1.5;
		segundo.WidthRequest = w * 1.5;
		terceiro.WidthRequest = w * 1.5;
		quarto.WidthRequest = w * 1.5;
	}

	void GerenciaCenarios()
	{
		MoveCenario();
		GerenciaCenario(primeiro);
		GerenciaCenario(segundo);
		GerenciaCenario(terceiro);
		GerenciaCenario(quarto);		
	}

	void MoveCenario()
	{
		primeiro.TranslationX -= Velocidade01;
		segundo.TranslationX -= Velocidade02;
		terceiro.TranslationX -= Velocidade03;
	}

	void GerenciaCenario(HorizontalStackLayout hsl)
	{
		var view = (hsl.Children.First() as Image);

		if(view.WidthRequest + hsl.TranslationX < 0)
		{
			hsl.Children.Remove(view);
			hsl.Children.Add(view);
			hsl.TranslationX = view.TranslationX;
		}
	}
	void OnGridTorred (object a, TappedEventArgs e)
    {
        if (EstaNoChao)
        {
            EstaPulando = true;
        }
    }
}