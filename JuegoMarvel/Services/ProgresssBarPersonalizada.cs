namespace JuegoMarvel.Services
{
    /// <summary>
    /// Barra de progreso personalizada que muestra el avance visualmente mediante un <see cref="BoxView"/>.
    /// Se puede enlazar su propiedad <c>Progress</c> para actualizar dinámicamente el progreso mostrado.
    /// </summary>
    public class ProgresssBarPersonalizada : ContentView
    {
        /// <summary>
        /// Propiedad enlazable (BindableProperty) que representa el valor del progreso (de 0.0 a 1.0).
        /// </summary>
        public static readonly BindableProperty ProgressProperty =
            BindableProperty.Create(
                nameof(Progress),
                typeof(double),
                typeof(ProgresssBarPersonalizada),
                0.0,
                propertyChanged: OnProgressChanged);

        /// <summary>
        /// Se ejecuta cuando cambia la propiedad <see cref="Progress"/>.
        /// Actualiza visualmente la barra de progreso.
        /// </summary>
        /// <param name="bindable">Instancia afectada.</param>
        /// <param name="oldValue">Valor anterior.</param>
        /// <param name="newValue">Nuevo valor asignado.</param>
        private static void OnProgressChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((ProgresssBarPersonalizada)bindable).UpdateProgress();
        }

        /// <summary>
        /// Propiedad pública que representa el progreso actual de la barra (entre 0.0 y 1.0).
        /// Se puede enlazar desde XAML o código.
        /// </summary>
        public double Progress
        {
            get => (double)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }

        /// <summary>
        /// Propiedad privada que representa visualmente el progreso mediante un <see cref="BoxView"/>.
        /// </summary>
        private BoxView progressBar;

        /// <summary>
        /// Constructor que inicializa la barra de progreso y su estilo visual.
        /// </summary>
        public ProgresssBarPersonalizada()
        {
            progressBar = new BoxView
            {
                Color = Colors.DarkRed,
                HeightRequest = 50,
                HorizontalOptions = LayoutOptions.Start
            };

            Content = new Grid
            {
                BackgroundColor = Colors.DarkGray,
                Children = { progressBar }
            };

            // Evento para actualizar la barra si cambia el tamaño del componente
            SizeChanged += (s, e) => UpdateProgress();

            // Inicializa la barra de progreso con el valor actual
            UpdateProgress();
        }

        /// <summary>
        /// Método privado que actualiza el ancho del <see cref="BoxView"/> en función del valor de <see cref="Progress"/> y del ancho total.
        /// </summary>
        private void UpdateProgress()
        {
            progressBar.AnchorX = 0; // Alinea la barra al inicio
            progressBar.WidthRequest = Width * Progress; // Ajusta el ancho según el progreso
        }
    }
}
