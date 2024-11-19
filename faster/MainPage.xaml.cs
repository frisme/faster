namespace faster;

public partial class MainPage : ContentPage
{
	Player player;

	bool estaMorto = false;
	bool estaPulando = false;

	const int tempoEntreFrames = 25;

	int velocidade = 0;
	int velocidade1 = 0;
	int velocidade2 = 0;
	int velocidade3 = 0;
	int velocidade4 = 0;
	int larguraJanela = 0;
	int alturaJanela = 0;

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

	async Task Desenha()
	{
		// bool a =false;
		while(!estaMorto)
		{
			GerenciaCenarios();
			player.Desenha();
			//ImgCarro.IsVisible = a;
			//ImgCarroBack.IsVisible = !a;
			//a = !a;
			await Task.Delay(tempoEntreFrames);
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
		velocidade1 = (int)(w * 0.001);
		velocidade2 = (int)(w * 0.004);
		velocidade3 = (int)(w * 0.007);
		velocidade4 = (int)(w * .009);
		velocidade = (int)(w * 0.01);
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
		primeiro.TranslationX -= velocidade1;
		segundo.TranslationX -= velocidade2;
		terceiro.TranslationX -= velocidade3;
		quarto.TranslationX -= velocidade4;
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
}