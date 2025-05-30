
namespace JuegoMarvel.Services
{
    public class ProgresssBarPersonalizada : ContentView
    {
        public static readonly BindableProperty ProgressProperty =
            BindableProperty.Create(nameof(Progress), typeof(double), typeof(ProgresssBarPersonalizada), 0.0, propertyChanged: OnProgressChanged);

        private static void OnProgressChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((ProgresssBarPersonalizada)bindable).UpdateProgress();
        }

        public double Progress
        {
            get => (double)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }

        private BoxView progressBar;

        public ProgresssBarPersonalizada()
        {
            progressBar = new BoxView { Color = Colors.DarkRed, HeightRequest = 50, HorizontalOptions = LayoutOptions.Start };
            Content = new Grid
            {
                BackgroundColor = Colors.DarkGray,
                Children = { progressBar }
            };

            // Event hinzufügen, um die Größe zu überwachen
            SizeChanged += (s, e) => UpdateProgress();

            // Initiale Aktualisierung des Fortschrittsbalkens
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            progressBar.AnchorX = 0;
            progressBar.WidthRequest = Width * Progress;
        }
    }
}
