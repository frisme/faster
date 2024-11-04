namespace faster;

public partial class MainPage : ContentPage
{
	int count = 0;
	bool EstaMorto = false;
	bool EstaPulando = false;
	const int TempoEntreFrames = 25;
	int velocidade = 0;
	int velocidade2 = 0;
	int velocidade3 = 0;
	int velocidade4 = 0;
	int larguraJanela = 0;
	int alturaJanela = 0;


	public MainPage()
	{
		InitializeComponent();
	}

	
	protected override void OnSizeAllocated (double width, double height)
	{
		base.OnSizeAllocated(width, height);
			CorrigeTamanhoCenario(width, height);
			CalculaVelocidade(width);
	}

	void CalculaVelocidade(double w)
	{
		velocidade = (int)(w * 0.001);
		velocidade2 = (int)(w * 0.004);
		velocidade3 = (int)(w * 0.008);
		velocidade4 = (int)(w * 0.01);
	}

	void CorrigeTamanhoCenario(double w, double h)
	{
		foreach (var a in primeiro.Children)
			(a as Image).WidthRequest = w;
		foreach (var b in terceiro.Children)
			(b as Image).WidthRequest = w;

		primeiro.WidthRequest = w * 1.5;
		terceiro.WidthRequest = w * 1.5;
	}
}