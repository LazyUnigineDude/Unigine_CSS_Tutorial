using Unigine;

[Component(PropertyGuid = "d1dca4d9e782c864abaef873ee8a63362d43811d")]
public class HUDMaker : Component
{
	public Node MainChar;
	public AssetLink _image, HealthImage;
	Gui GUI;
	WidgetCanvas Canvas;
	WidgetSprite Sprite;
	WidgetLabel CurrentAmount, MaxAmount;
	WidgetGridBox HealthGrid;
	int Width = App.GetWidth(), Height = App.GetHeight(), CurrentHealth;
	HealthBar MainCharHealth;

	private void Init()
	{
		// write here code to be called on component initialization
		GUI = Gui.Get();

		MainCharHealth = GetComponent<HealthBar>(MainChar);
		CurrentHealth = MainCharHealth.ShowHealth();

		Canvas = new();

		//int x = Canvas.AddText(1);
		//Canvas.SetTextText(x, "SAMPLE_TEXT");
		//Canvas.SetTextColor(x, new vec4(1, 1, 1, 0.5));
		//Canvas.SetTextSize(x, 30);
		//Canvas.SetTextPosition(x, new vec2((Width / 2) - (Canvas.GetTextWidth(x) / 2), Height / 2 - (Canvas.GetTextHeight(x) / 2)));

		int y = Canvas.AddPolygon(0);
		Canvas.SetPolygonColor(y, new vec4(0, 0, 0, 0.5));
		Canvas.AddPolygonPoint(y, new vec3(0, 0, 0));
		Canvas.AddPolygonPoint(y, new vec3(450, 0, 0));
		Canvas.AddPolygonPoint(y, new vec3(450, 100, 0));
		Canvas.AddPolygonPoint(y, new vec3(0, 100, 0));

		Sprite = new();

		int z = Sprite.AddLayer();
		Image _i = new(); _i.Load(_image.AbsolutePath);
		Sprite.SetImage(_i);
		Sprite.Width = 50;
		Sprite.Height = 50;
		Sprite.SetPosition((Width / 2) - 25, (Height / 2) - 25);

		//GRID for health

		HealthGrid = new WidgetGridBox(20,1,1);
		GainHealth(CurrentHealth);
		HealthGrid.SetPosition(0, 0);

		CurrentAmount = new(); MaxAmount = new();

		GUI.AddChild(Canvas, Gui.ALIGN_EXPAND);
		GUI.AddChild(Sprite, Gui.ALIGN_EXPAND | Gui.ALIGN_OVERLAP);

		GUI.AddChild(CurrentAmount, Gui.ALIGN_EXPAND | Gui.ALIGN_OVERLAP);
		GUI.AddChild(MaxAmount, Gui.ALIGN_EXPAND | Gui.ALIGN_OVERLAP);
		GUI.AddChild(HealthGrid, Gui.ALIGN_EXPAND | Gui.ALIGN_OVERLAP);
	}

	private void Update()
	{
		// write here code to be called before updating each render frame

		GUI = Gui.Get();

		if (CurrentHealth > MainCharHealth.ShowHealth()) 
        {
			LoseHealth(CurrentHealth - MainCharHealth.ShowHealth());
			CurrentHealth = MainCharHealth.ShowHealth();
		}
	}

	public void UpdateGun(int CAmount, int MAmount) {

		CurrentAmount.Text = CAmount.ToString();
		CurrentAmount.FontSize = 48;
		CurrentAmount.SetPosition(Width - 160, 20);


		MaxAmount.Text = MAmount.ToString();
		MaxAmount.FontSize = 36;
		MaxAmount.SetPosition(Width - 80, 20);
	}

	void LoseHealth(int Amount) {

        for (int i = 0; i < Amount; i++)
        {
			HealthGrid.RemoveChild(HealthGrid.GetChild(0));
        }
	}

	void GainHealth(int Amount) {

		for (int i = 0; i < Amount; i++)
		{
			WidgetSprite _image = new();
			Image _i2 = new(); _i2.Load(HealthImage.AbsolutePath);
			_image.SetImage(_i2);
			_image.Width = 20;
			_image.Height = 20;
			HealthGrid.AddChild(_image);
		}
	}
}