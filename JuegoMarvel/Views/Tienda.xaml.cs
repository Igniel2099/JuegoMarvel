using System.Diagnostics;
using CommunityToolkit.Maui.Views;

namespace JuegoMarvel.Views;

public partial class Tienda : ContentPage
{
	public Tienda()
	{
		InitializeComponent();

        List<string> nombres = [
            "Elektra", 
            "Daredevil",
            "Capitan America",
            "Venom",
            "Deadpool",
            "Iron Fist"
        ];

        List<string> tipos = [
            "Asesina",
            "peleador",
            "Tanque",
            "Tanque",
            "Asesino",
            "pelador"
        ];

        List<string> Grupos = [
            "Mano",
            "Callejero",
            "Vengeador",
            "Villano",
            "Mutante",
            "Vengador"
        ];

        List<string> dineros = [
            "1200",
            "100",
            "1240",
            "1457",
            "2574",
            "9805"
        ];

        List<string> imgsPrins = [
            "elk_img_prin.png",
            "dar_img_prin.png",
            "cap_img_prin.png",
            "ven_img_prin.png",
            "dep_img_prin.png",
            "irf_img_prin.png"
        ];

        List<string> habilidad_1 = [
            "elk_hb_1.png",
            "dar_hb_1.png",
            "cap_hb_1.png",
            "ven_hb_1.png",
            "dep_hb_1.png",
            "irf_hb_1.png"
        ];


        List<string> habilidad_2 = [
            "elk_hb_2.png",
            "dar_hb_2.png",
            "cap_hb_2.png",
            "ven_hb_2.png",
            "dep_hb_2.png",
            "irf_hb_2.png"
        ];


        List<string> habilidad_3 = [
            "elk_hb_3.png",
            "dar_hb_3.png",
            "cap_hb_3.png",
            "ven_hb_3.png",
            "dep_hb_3.png",
            "irf_hb_3.png"
        ];

        int count = 0;

        for (int i = 0; i < 2; i++)
        {
            var miHStack = new HorizontalStackLayout
            {
                Spacing = 10,
                Padding = new Thickness(16),
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center
            };
            for (int j = 1; j <= 3; j++)
            {
                View view = CreateCardView(
                    nombres[count],
                    Grupos[count],
                    tipos[count],
                    dineros[count],
                    imgsPrins[count],
                    habilidad_1[count],
                    habilidad_2[count],
                    habilidad_3[count]);
                miHStack.Add(view);
                
                count++;
            }
            miVStack.Add(miHStack);

        }
    }


    private View CreateCardView(
        string nombre,
        string grupo,
        string tipo,
        string dinero,
        string imgPrin,
        string imgHb1,
        string imgHb2,
        string imgHb3)
    {
        // Grid contenedor principal
        var outerGrid = new Grid();

        // Imagen de fondo tipo “card”
        outerGrid.Children.Add(new Image
        {
            Source = "carta_contenido.png",
            HeightRequest = 154,
            WidthRequest = 250
        });

        // Grid interior con dos filas
        var innerGrid = new Grid();
        innerGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        innerGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

        // --- FILA 0: Contenido de la carta ---
        var contentGrid = new Grid
        {
            Margin = new Thickness(10, 10, 0, 8),
            ColumnSpacing = 5,
        };
        contentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        contentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

        // Imagen de personaje con contenedor
        var imgContainer = new Grid();
        imgContainer.Children.Add(new Image
        {
            Source = "contenedor_personaje.png",
            HeightRequest = 106,
            WidthRequest = 89
        });
        imgContainer.Children.Add(new Image
        {
            Source = imgPrin,
            HeightRequest = 106,
            WidthRequest = 89
        });
        contentGrid.Add(imgContainer, 0, 0);

        // Grid para los datos del personaje
        var charContent = new Grid();
        charContent.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        charContent.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
        charContent.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
        contentGrid.Add(charContent, 1, 0);

        // --- Nombre + icono info (fila 0) ---
        var nameLayout = new HorizontalStackLayout
        {
            HorizontalOptions = LayoutOptions.Start,
            MaximumWidthRequest = 134,
            Spacing = 5
        };
        nameLayout.Children.Add(new Label
        {
            FontFamily = "LuckiestGuy",
            FontSize = 12,
            MaximumWidthRequest = 104,
            Text = nombre, // Nombre del Personaje
            TextColor = Colors.White,
            VerticalTextAlignment = TextAlignment.Center
        });

        var infoGrid = new Grid();
        var tapInfo = new TapGestureRecognizer();
        tapInfo.Tapped += OnInformationTapped;
        infoGrid.GestureRecognizers.Add(tapInfo);
        infoGrid.Children.Add(new Image
        {
            Source = "contenedor_info.png",
            HeightRequest = 25,
            WidthRequest = 25
        });
        infoGrid.Children.Add(new Image
        {
            Source = "icono_info.png",
            HeightRequest = 25,
            WidthRequest = 25
        });
        nameLayout.Children.Add(infoGrid);

        charContent.Add(nameLayout, 0, 0);

        // --- Grupo y Tipo (fila 1) ---
        var tagLayout = new HorizontalStackLayout
        {
            Spacing = 7
        };

        // Grupo
        var grupoBorder = new Border
        {
            Background = Color.FromArgb("#FF3100"),
            Stroke = Colors.Red,
            HeightRequest = 37,
            WidthRequest = 60,
            Content = new VerticalStackLayout
            {
                Children =
                    {
                        new Label
                        {
                            FontFamily = "LuckiestGuy",
                            FontSize = 10,
                            Text = "GRUPO",
                            TextColor = Colors.White
                        },
                        new Border
                        {
                            Background = Colors.DarkRed,
                            Stroke = Colors.DarkRed,
                            HeightRequest = 20,
                            WidthRequest = 57,
                            Content = new Label
                            {
                                FontSize = 10,
                                Text = grupo,
                                TextColor = Colors.White
                            }
                        }
                    }
            }
        };
        tagLayout.Children.Add(grupoBorder);

        // Tipo
        var tipoBorder = new Border
        {
            Background = Color.FromArgb("#FF3100"),
            Stroke = Color.FromArgb("#FF3100"),
            HeightRequest = 37,
            WidthRequest = 60,
            Content = new VerticalStackLayout
            {
                Children =
                    {
                        new Label
                        {
                            FontFamily = "LuckiestGuy",
                            FontSize = 10,
                            Text = "TIPO",
                            TextColor = Colors.White
                        },
                        new Border
                        {
                            Background = Colors.DarkRed,
                            Stroke = Colors.DarkRed,
                            HeightRequest = 24,
                            WidthRequest = 57,
                            Content = new Label
                            {
                                FontSize = 10,
                                Text = tipo,
                                TextColor = Colors.White
                            }
                        }
                    }
            }
        };
        tagLayout.Children.Add(tipoBorder);

        charContent.Add(tagLayout, 0, 1);

        // --- Iconos / boxviews (fila 2) ---
        var iconos = new HorizontalStackLayout
        {
            Spacing = 12
        };


        iconos.Add(new Image
            { AnchorX = 0.5, AnchorY = 0.5, Source = imgHb1, HeightRequest = 34, WidthRequest = 34 }
        );

        iconos.Add(new Image
        { AnchorX = 0.5, AnchorY = 0.5, Source = imgHb2, HeightRequest = 34, WidthRequest = 34 }
        );

        iconos.Add(new Image
        { AnchorX = 0.5, AnchorY = 0.5, Source = imgHb3, HeightRequest = 34, WidthRequest = 34 }
        );

        //for (int i = 0; i < 3; i++)
        //{
        //    iconos.Children.Add(new BoxView
        //    {
        //        BackgroundColor = Colors.Black,
        //        HeightRequest = 34,
        //        WidthRequest = 34
        //    });
        //}

        charContent.Add(iconos, 0, 2);

        innerGrid.Add(contentGrid, 0, 0);

        // --- FILA 1: Botón moneda ---
        var buttonGrid = new Grid();
        buttonGrid.Children.Add(new Image
        {
            Source = "contenedor_boton.png",
            HeightRequest = 27,
            WidthRequest = 244
        });

        var coinLayout = new HorizontalStackLayout
        {
            HorizontalOptions = LayoutOptions.Center,
            Spacing = 5
        };
        coinLayout.Children.Add(new Image
        {
            Source = "icono_moneda.png",
            HeightRequest = 22,
            WidthRequest = 22
        });
        coinLayout.Children.Add(new Label
        {
            FontSize = 15,
            Text = dinero,
            TextColor = Colors.White,
            VerticalOptions = LayoutOptions.Center
        });
        buttonGrid.Children.Add(coinLayout);

        innerGrid.Add(buttonGrid, 0, 1);

        // Añadimos todo al grid padre y al root
        outerGrid.Children.Add(innerGrid);

        return outerGrid;
    }

    private void OnGridTapped(object sender, EventArgs e)
    {
        Grid gridTocado = (Grid)sender;
        List<Grid> GridsBarMenuTop = [GridAll, GridTipo, GridGrupo];

        AnimacionesComunes.CambiarImagenGrid(GridsBarMenuTop, gridTocado,"bar_seleccion.png");

        AnimacionesComunes.BorrarImagenGrid(GridsBarMenuTop);
        
        AnimacionesComunes.AnimacionImagen(gridTocado);

    }

    private async void OnInformationTapped(object sender, EventArgs e)
    {
        Grid gridInformación = (Grid)sender;

        AnimacionesComunes.AnimacionImagen(gridInformación);

        var parentPage = this.GetParentPage();
        if (parentPage != null)
        {
            var popup = new InformacionPopup();
            await parentPage.ShowPopupAsync(popup);
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage
            .Navigation
            .PopModalAsync(false);

    }
}