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

	protected override void OnAppearing()
	{
		base.OnAppearing();
		Desenha();
	}

	async Task Desenha()
	{
		while(!EstaMorto)
		{
			GerenciarCenarios();
			await Task.Delay(TempoEntreFrames);
		}
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

	void CorrigeTamanhoCenario(double w, double height)
	{
		foreach (var a in primeiro.Children)
			(a as Image).WidthRequest = w;
		foreach (var b in segundo.Children)
			(b as Image).WidthRequest = w;
		foreach (var c in terceiro.Children)
		    (c as Image).WidthRequest = w;
		foreach (var d in quarto.Children)
		    (d as Image).WidthRequest = w;

		primeiro.WidthRequest = w * 1.5;
		segundo.WidthRequest = w * 1.5;
		terceiro.WidthRequest = w * 1.5;
		quarto.WidthRequest = w * 1.5;
	}

	void GerenciarCenarios()
	{
		MoveCenario();
		GerenciaCenario(primeiro);
		GerenciaCenario(segundo);
		GerenciaCenario(terceiro);
		GerenciaCenario(quarto);	
	}

	void MoveCenario()
	{
		primeiro.TranslationX -= velocidade;
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

	public class Animacao
	{
		protected List<String> Animacao1 = new List<String>();
		protected List<String> Animacao2 = new List<String>();
		protected List<String> Animacao3 = new List<String>();
		protected bool Loop = true;
		protected int AnimacaoAtiva = 1;
		bool parado = true;
		int frameAtual = 1;
		protected Image compImage;
		public Animacao(Image a)
		{
			compImage = a;
		}

	public void Stop()
	{
		parado = true;
	}

	public void Play()
	{
		parado = false;
	}

	public void SetAnimacaoAtiva(int a)
	{
		AnimacaoAtiva = a;
	}

	public void Desenha()
	{
		if (parado)
		   return;
		string nomeArquivo;
		int tamanhoAnimacao;
		if (AnimacaoAtiva == 1)
		{
			nomeArquivo = Animacao1 [frameAtual];
			tamanhoAnimacao = Animacao1.Count;
		}
		else if (AnimacaoAtiva == 2)
		{
			nomeArquivo = Animacao2[frameAtual];
			tamanhoAnimacao = Animacao2.Count;
		}
		else if (AnimacaoAtiva == 3)
		{
			nomeArquivo = Animacao3[frameAtual];
			tamanhoAnimacao = Animacao3.Count;
		}
		compImagem.Source = ImageSource.FromFile(nomeArquivo);
		frameAtual++;
		if (frameAtual >= tamanhoAnimacao)
		{
			if(Loop)
			   frameAtual = 0;
			else
			{
				parado = true;
				QuandoParar();
			}
		}
	}	
}
	public virtual void QuandoParar()
	{
	}
}