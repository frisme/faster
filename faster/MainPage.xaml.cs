namespace faster;

public partial class MainPage : ContentPage
{
	Player player;
	Inimigos inimigos;

	bool Morto = false;
	bool Pulando = false;
	const int TempoEntreFrames = 29;
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
	const int MaxTempoPulando = 10;
	const int MaxTempoAr = 4;
	const int ForcaPulo = 12;

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
			TempoPulando++;
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
			     GerenciaCenarios();
				 if(inimigos!=null)
				 inimigos.Desenha(Velocidade);
			if (!EstaPulando && !EstaNoAr)
			{
				AplicaGravidade();
				player.Desenha();
			}
			else
				AplicaPulo();
			await Task.Delay(TempoEntreFrames);
		}
	}

	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		CorrigeTamanhoCenario(w, h);
		CalculaVelocidade(w);

		{
			inimigos = new Inimigos(-w);
			inimigos.Add(new Inimigo(quinto));
			inimigos.Add(new Inimigo(quinto));
			inimigos.Add(new Inimigo(quinto));
			inimigos.Add(new Inimigo(quinto));

		}
	}

	void CalculaVelocidade(double w)
	{
		Velocidade01 = (int)(w * 0.001);
		Velocidade02 = (int)(w * 0.004);
		Velocidade03 = (int)(w * 0.007);
		Velocidade = (int)(w * 0.01);
	}

	void CorrigeTamanhoCenario(double width, double height)
	{
		foreach (var background in primeiro.Children)
			(background as Image).WidthRequest = width;

		foreach (var background2 in segundo.Children)
			(background2 as Image).WidthRequest = width;
		
		foreach (var background3 in terceiro.Children)
			(background3 as Image).WidthRequest = width;
		
		foreach (var floor in quarto.Children)
			(floor as Image).WidthRequest = width;

		primeiro.WidthRequest = width * 1.5;
		segundo.WidthRequest = width * 5.0;
		terceiro.WidthRequest = width * 3.5;
		quarto.WidthRequest = width * 1.5;
	}

	void GerenciaCenarios()
	{
		MoveCenario();
		GerenciaCenarios(primeiro);
		GerenciaCenarios(segundo);
		GerenciaCenarios(terceiro);
		GerenciaCenarios(quarto);		
	}

	void MoveCenario()
	{
		primeiro.TranslationX -= Velocidade01;
		segundo.TranslationX -= Velocidade02;
		terceiro.TranslationX -= Velocidade03;
		quarto.TranslationX -= Velocidade;
	}

	void GerenciaCenarios(HorizontalStackLayout HSL)
	{
		var view = (HSL.Children.First() as Image);

		if(view.WidthRequest + HSL.TranslationX < 0)
		{
			HSL.Children.Remove(view);
			HSL.Children.Add(view);
			HSL.TranslationX = view.TranslationX;
		}
	}
	void OnGridTapped (object a, TappedEventArgs e)
    {
        if (EstaNoChao)
        {
            EstaPulando = true;
        }
    }
}