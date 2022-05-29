namespace MAUIInAction;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"点击了 {count} 次";
		else if(count <= 10)
			CounterBtn.Text = $"点击了 {count} 次";
		else
            CounterBtn.Text = $"点击太多了！";

        SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

