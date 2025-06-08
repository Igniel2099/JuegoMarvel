using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls.Shapes;

namespace JuegoMarvel.Services
{
    public class ProgresssBarPersonalizada : ContentView
    {
        // 1) Progress (0.0 – 1.0)
        public static readonly BindableProperty ProgressProperty =
            BindableProperty.Create(
                nameof(Progress),
                typeof(double),
                typeof(ProgresssBarPersonalizada),
                0.0,
                propertyChanged: OnProgressChanged);

        public double Progress
        {
            get => (double)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }

        static void OnProgressChanged(BindableObject bindable, object oldValue, object newValue)
            => ((ProgresssBarPersonalizada)bindable).UpdateProgress();

        // 2) Color dinámico
        public static readonly BindableProperty ProgressColorProperty =
            BindableProperty.Create(
                nameof(ProgressColor),
                typeof(Color),
                typeof(ProgresssBarPersonalizada),
                Colors.DarkRed,
                propertyChanged: OnProgressColorChanged);

        public Color ProgressColor
        {
            get => (Color)GetValue(ProgressColorProperty);
            set => SetValue(ProgressColorProperty, value);
        }

        static void OnProgressColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = (ProgresssBarPersonalizada)bindable;
            ctrl.progressBorder.Background = new SolidColorBrush((Color)newValue);
        }

        // 3) CornerRadius dinámico
        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(
                nameof(CornerRadius),
                typeof(float),
                typeof(ProgresssBarPersonalizada),
                0f,
                propertyChanged: OnCornerRadiusChanged);

        public float CornerRadius
        {
            get => (float)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        static void OnCornerRadiusChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = (ProgresssBarPersonalizada)bindable;
            ctrl.ApplyCornerRadius((float)newValue);
        }

        // 4) Dirección del progreso (izq->der o der->izq)
        public static readonly BindableProperty IsReversedProperty =
            BindableProperty.Create(
                nameof(IsReversed),
                typeof(bool),
                typeof(ProgresssBarPersonalizada),
                false,
                propertyChanged: OnIsReversedChanged);

        public bool IsReversed
        {
            get => (bool)GetValue(IsReversedProperty);
            set => SetValue(IsReversedProperty, value);
        }

        static void OnIsReversedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = (ProgresssBarPersonalizada)bindable;
            ctrl.UpdateProgress();
        }

        // Elementos visuales
        readonly Border trackBorder;
        readonly Border progressBorder;

        public ProgresssBarPersonalizada()
        {
            // Track de fondo
            trackBorder = new Border
            {
                Background = new SolidColorBrush(Colors.DarkGray),
                StrokeThickness = 0,
                Padding = 0
            };

            // Barra de progreso
            progressBorder = new Border
            {
                Background = new SolidColorBrush(ProgressColor),
                StrokeThickness = 0,
                Padding = 0,
            };

            // Superponer en un Grid
            var grid = new Grid();
            grid.Children.Add(trackBorder);
            grid.Children.Add(progressBorder);
            Content = grid;

            SizeChanged += (s, e) => UpdateProgress();
            ApplyCornerRadius(CornerRadius);
            UpdateProgress();
        }

        void ApplyCornerRadius(float radius)
        {
            trackBorder.StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(radius) };
            progressBorder.StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(radius) };
        }

        void UpdateProgress()
        {
            // Calcula ancho de la barra
            var width = Width * Progress;
            progressBorder.WidthRequest = width;

            // Ajusta alineación según la dirección
            progressBorder.HorizontalOptions = IsReversed
                ? LayoutOptions.End
                : LayoutOptions.Start;
        }
    }
}
