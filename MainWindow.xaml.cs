using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Nexus
{
    public partial class MainWindow : Window
    {
        public int currentPage = 1;
        private int modsPerPage = 10;
        private int totalPages;

        public int currentPageTexture = 1;
        private int modsPerPageTexture = 9;
        private int totalPagesTexture;

        public int currentPagemodpack = 1;
        private int modsPerPagemodpack = 9;
        private int totalPagesmodpack;

        private Animations anims = new Animations();

        public MainWindow()
        {
            InitializeComponent();
            if (!HasInternetConnection())
            {
                popwifi.Visibility = Visibility.Visible;
            }
            else
            {
                popwifi.Visibility = Visibility.Hidden;
                topmost.IsChecked = Properties.Settings.Default.TopMostValue;
                LoadVersions();
                LoadHerramientas();
                LoadModPack(currentPagemodpack);
            }

        }

        #region ModsPages

        private void LoadVersions()
        {
            WebClient webClient = new WebClient();
            string json = webClient.DownloadString("https://gitlab.com/DTXScripts/nexus/-/raw/main/MODS.json");
            Root root = JsonConvert.DeserializeObject<Root>(json);

            // Obtiene las versiones únicas de los mods
            List<string> versions = root.Versions.Select(mod => mod.id).Distinct().ToList();

            // Configura el ComboBox con las versiones obtenidas
            ChangesVersions.ItemsSource = versions;
            ChangesVersionsTexture.ItemsSource = versions;
        }

        public void LoadMods(int page, string selectedVersion)
        {
            WebClient webClient = new WebClient();
            string json = webClient.DownloadString("https://gitlab.com/DTXScripts/nexus/-/raw/main/MODS.json");
            Root root = JsonConvert.DeserializeObject<Root>(json);

            List<Mod> filteredMods;

            if (selectedVersion == null || selectedVersion == "Todas")
            {
                filteredMods = root.Mods; // Mostrar todos los mods sin filtrar
            }
            else
            {
                // Filtrar los mods por versión seleccionada
                filteredMods = root.Mods.Where(mod => mod.version == selectedVersion).ToList();
            }

            totalPages = (int)Math.Ceiling((double)filteredMods.Count / modsPerPage);

            currentPage = page;

            int startIndex = (currentPage - 1) * modsPerPage;
            int endIndex = Math.Min(startIndex + modsPerPage, filteredMods.Count);

            mod_wrap_panel.Children.Clear();

            for (int i = startIndex; i < endIndex; i++)
            {
                Mod mod = filteredMods[i];
                mod_wrap_panel.Children.Add(new Items(mod));
            }

            if (filteredMods.Count == 0)
            {
                txt1.Visibility = Visibility.Visible;
                txt2.Visibility = Visibility.Visible;
            }
            else
            {
                txt1.Visibility = Visibility.Collapsed;
                txt2.Visibility = Visibility.Collapsed;
            }

            UpdatePageButtons();
        }

        private void UpdatePageButtons()
        {
            // Verificar si hay resultados suficientes para más de una página
            if (totalPages > 1)
            {
                previous_button.IsEnabled = currentPage > 1; // Habilitar botón anterior si no es la primera página
                next_button.IsEnabled = currentPage < totalPages; // Habilitar botón siguiente si no es la última página

                int firstPage;
                int lastPage;

                if (currentPage <= 3)
                {
                    firstPage = 1;
                    lastPage = Math.Min(totalPages, 5);
                }
                else if (currentPage >= totalPages - 2)
                {
                    firstPage = Math.Max(1, totalPages - 4);
                    lastPage = totalPages;
                }
                else
                {
                    firstPage = currentPage - 2;
                    lastPage = currentPage + 2;
                }

                page_buttons.Children.Clear();

                if (firstPage > 1)
                {
                    // Agregar botón "..." para indicar que hay más páginas antes
                    RadioButton dotsButtonBefore = new RadioButton()
                    {
                        Content = "...",
                        Width = 30,
                        Background = Brushes.Transparent,
                        Foreground = Brushes.White,
                        Height = 30,
                        FontSize = 14,
                        Cursor = Cursors.Hand,
                        Margin = new Thickness(0, 0, 5, 0),
                        IsEnabled = false, // Deshabilitar el botón de los puntos suspensivos
                        Style = (TryFindResource("rdbutton") as Style)
                    };
                    page_buttons.Children.Add(dotsButtonBefore);
                }

                for (int i = firstPage; i <= lastPage; i++)
                {
                    RadioButton button = new RadioButton()
                    {
                        Content = i.ToString(),
                        Width = 30,
                        Background = Brushes.Transparent,
                        Foreground = Brushes.White,
                        Height = 30,
                        FontSize = 14,
                        Cursor = Cursors.Hand,
                        Margin = new Thickness(0, 0, 5, 0),
                        Tag = i,
                        Style = (TryFindResource("rdbutton") as Style)
                    };
                    button.Click += PageButton_Click;
                    page_buttons.Children.Add(button);

                    if (i == currentPage)
                    {
                        button.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#3B56E1");// Desactivar botón de la página actual
                    }
                }

                if (lastPage < totalPages)
                {
                    // Agregar botón "..." para indicar que hay más páginas después
                    RadioButton dotsButtonAfter = new RadioButton()
                    {
                        Content = "...",
                        Width = 30,
                        Background = Brushes.Transparent,
                        Foreground = Brushes.White,
                        Height = 30,
                        FontSize = 14,
                        Cursor = Cursors.Hand,
                        Margin = new Thickness(0, 0, 5, 0),
                        IsEnabled = false, // Deshabilitar el botón de los puntos suspensivos
                        Style = (TryFindResource("rdbutton") as Style)
                    };
                    page_buttons.Children.Add(dotsButtonAfter);
                }
                anims.Fade(mod_panel);
            }
            else
            {
                // Si hay resultados para solo una página, no mostrar los botones de navegación
                previous_button.IsEnabled = false;
                next_button.IsEnabled = false;
                page_buttons.Children.Clear();
            }
        }

        private void PageButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedVersion = ChangesVersions.SelectedItem.ToString(); // Obtener la versión seleccionada del ComboBox
            RadioButton button = (RadioButton)sender;
            int page = (int)button.Tag;
            LoadMods(page, selectedVersion);
        }

        private void previous_button_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                string selectedVersion = ChangesVersions.SelectedItem?.ToString(); // Obtener la versión seleccionada del ComboBox
                LoadMods(currentPage - 1, selectedVersion); // Cargar los mods de la página anterior
            }
        }

        private void next_button_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage < totalPages)
            {
                string selectedVersion = ChangesVersions.SelectedItem?.ToString(); // Obtener la versión seleccionada del ComboBox
                LoadMods(currentPage + 1, selectedVersion); // Cargar los mods de la siguiente página
            }
        }

        private void ChangesVersions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedVersion = ChangesVersions.SelectedItem.ToString(); // Obtener la versión seleccionada del ComboBox

            LoadMods(1, selectedVersion);
        }

        private void ClearTextMod(object sender, RoutedEventArgs e)
        {
            txtSearchMod.Text = ""; // Borra el texto del TextBox

            // Obtiene la versión seleccionada del ComboBox
            string selectedVersion = ChangesVersions.SelectedItem?.ToString();

            LoadMods(currentPage, selectedVersion); // Carga la primera página de mods

            bord.Visibility = Visibility.Hidden;
            mod_panel.Margin = new Thickness(0, 20, 0, 60);
        }

        private void SearchMod(int page)
        {
            WebClient webClient = new WebClient();
            string json = webClient.DownloadString("https://gitlab.com/DTXScripts/nexus/-/raw/main/MODS.json");
            Root root = JsonConvert.DeserializeObject<Root>(json);

            // Obtiene el texto de búsqueda del TextBox
            string searchText = txtSearchMod.Text.ToLower();

            string selectedVersion = ChangesVersions.SelectedItem?.ToString();

            List<Mod> filteredMods = root.Mods
                .Where(mod => selectedVersion == null || selectedVersion == "Todas" || mod.version == selectedVersion)
                .ToList();

            filteredMods = filteredMods
                .Where(mod => string.IsNullOrEmpty(searchText) || mod.title.ToLower().Contains(searchText) || mod.title.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            mod_wrap_panel.Children.Clear();

            if (filteredMods.Count > 0)
            {
                foreach (Mod mod in filteredMods)
                {
                    mod_wrap_panel.Children.Add(new Items(mod));
                }
            }
            else
            {
                // Mostrar resultados similares
                List<Mod> similarMods = root.Mods
                    .Where(mod => selectedVersion == null || selectedVersion == "Todas" || mod.version == selectedVersion)
                    .Where(mod => mod.title.ToLower().Contains(searchText.Substring(0, 1)))
                    .ToList();

                foreach (Mod mod in similarMods)
                {
                    mod_wrap_panel.Children.Add(new Items(mod));
                }
            }

            totalPages = (int)Math.Ceiling((double)filteredMods.Count / modsPerPage);

            currentPage = page;
            UpdatePageButtons();
        }

        private void SearchButtonMod(object sender, RoutedEventArgs e)
        {
            SearchMod(currentPage);
            if (txtSearchMod.Text.Length >= 1)
            {
                bord.Visibility = Visibility.Visible;
                mod_panel.Margin = new Thickness(0, 70, 0, 60);
                txtlabel.Text = txtSearchMod.Text;
            }
            else
            {
                bord.Visibility = Visibility.Hidden;
                mod_panel.Margin = new Thickness(0, 20, 0, 60);
            }
        }
        #endregion

        #region TexturePages

        private void LoadTexture(int page, string selectedVersion)
        {
            WebClient webClient = new WebClient();
            string json = webClient.DownloadString("https://gitlab.com/DTXScripts/nexus/-/raw/main/MODS.json");
            Root root = JsonConvert.DeserializeObject<Root>(json);

            List<Texture> filteredMods;

            if (selectedVersion == null || selectedVersion == "Todas")
            {
                filteredMods = root.Texturas; // Mostrar todos los mods sin filtrar
            }
            else
            {
                // Filtrar los mods por versión seleccionada
                filteredMods = root.Texturas.Where(mod => mod.version == selectedVersion).ToList();
            }

            totalPagesTexture = (int)Math.Ceiling((double)filteredMods.Count / modsPerPageTexture);

            currentPageTexture = page;

            int startIndex = (currentPageTexture - 1) * modsPerPageTexture;
            int endIndex = Math.Min(startIndex + modsPerPageTexture, filteredMods.Count);

            texture_wrap_panel.Children.Clear();

            for (int i = startIndex; i < endIndex; i++)
            {
                Texture mod = filteredMods[i];
                texture_wrap_panel.Children.Add(new TextureItems(mod));
            }

            if (filteredMods.Count == 0)
            {
                txt3.Visibility = Visibility.Visible;
                txt4.Visibility = Visibility.Visible;
            }
            else
            {
                txt3.Visibility = Visibility.Hidden;
                txt4.Visibility = Visibility.Hidden;
            }

            UpdatePageButtonsTexture();
        }

        private void UpdatePageButtonsTexture()
        {
            // Verificar si hay resultados suficientes para más de una página
            if (totalPagesTexture > 1)
            {
                previous_button3.IsEnabled = currentPageTexture > 1; // Habilitar botón anterior si no es la primera página
                next_button3.IsEnabled = currentPageTexture < totalPagesTexture; // Habilitar botón siguiente si no es la última página

                int firstPage;
                int lastPage;

                if (currentPageTexture <= 3)
                {
                    firstPage = 1;
                    lastPage = Math.Min(totalPagesTexture, 5);
                }
                else if (currentPageTexture >= totalPagesTexture - 2)
                {
                    firstPage = Math.Max(1, totalPagesTexture - 4);
                    lastPage = totalPagesTexture;
                }
                else
                {
                    firstPage = currentPageTexture - 2;
                    lastPage = currentPageTexture + 2;
                }

                page_buttons3.Children.Clear();

                if (firstPage > 1)
                {
                    // Agregar botón "..." para indicar que hay más páginas antes
                    RadioButton dotsButtonBefore = new RadioButton()
                    {
                        Content = "...",
                        Width = 30,
                        Background = Brushes.Transparent,
                        Foreground = Brushes.White,
                        Height = 30,
                        FontSize = 14,
                        Cursor = Cursors.Hand,
                        Margin = new Thickness(0, 0, 5, 0),
                        IsEnabled = false, // Deshabilitar el botón de los puntos suspensivos
                        Style = (TryFindResource("rdbutton") as Style)
                    };
                    page_buttons3.Children.Add(dotsButtonBefore);
                }

                for (int i = firstPage; i <= lastPage; i++)
                {
                    RadioButton button = new RadioButton()
                    {
                        Content = i.ToString(),
                        Width = 30,
                        Background = Brushes.Transparent,
                        Foreground = Brushes.White,
                        Height = 30,
                        FontSize = 14,
                        Cursor = Cursors.Hand,
                        Margin = new Thickness(0, 0, 5, 0),
                        Tag = i,
                        Style = (TryFindResource("rdbutton") as Style)
                    };
                    button.Click += PageButton_Click3;
                    page_buttons3.Children.Add(button);

                    if (i == currentPageTexture)
                    {
                        button.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#3B56E1");// Desactivar botón de la página actual
                    }
                }

                if (lastPage < totalPagesTexture)
                {
                    // Agregar botón "..." para indicar que hay más páginas después
                    RadioButton dotsButtonAfter = new RadioButton()
                    {
                        Content = "...",
                        Width = 30,
                        Background = Brushes.Transparent,
                        Foreground = Brushes.White,
                        Height = 30,
                        FontSize = 14,
                        Cursor = Cursors.Hand,
                        Margin = new Thickness(0, 0, 5, 0),
                        IsEnabled = false, // Deshabilitar el botón de los puntos suspensivos
                        Style = (TryFindResource("rdbutton") as Style)
                    };
                    page_buttons3.Children.Add(dotsButtonAfter);
                }
                anims.Fade(texturepanel);
            }
            else
            {
                // Si hay resultados para solo una página, no mostrar los botones de navegación
                previous_button3.IsEnabled = false;
                next_button3.IsEnabled = false;
                page_buttons3.Children.Clear();
            }
        }

        private void PageButton_Click3(object sender, RoutedEventArgs e)
        {
            string selectedVersion = ChangesVersionsTexture.SelectedItem.ToString(); // Obtener la versión seleccionada del ComboBox
            RadioButton button = (RadioButton)sender;
            int page = (int)button.Tag;
            LoadTexture(page, selectedVersion);
        }

        private void previous_button_Click3(object sender, RoutedEventArgs e)
        {
            if (currentPageTexture > 1)
            {
                string selectedVersion = ChangesVersionsTexture.SelectedItem?.ToString(); // Obtener la versión seleccionada del ComboBox
                LoadTexture(currentPageTexture - 1, selectedVersion); // Cargar los mods de la página anterior
            }
        }

        private void next_button_Click3(object sender, RoutedEventArgs e)
        {
            if (currentPageTexture < totalPagesTexture)
            {
                string selectedVersion = ChangesVersionsTexture.SelectedItem?.ToString(); // Obtener la versión seleccionada del ComboBox
                LoadTexture(currentPageTexture + 1, selectedVersion); // Cargar los mods de la siguiente página
            }
        }

        private void ChangesVersionsTexture_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedVersion = ChangesVersionsTexture.SelectedItem.ToString(); // Obtener la versión seleccionada del ComboBox

            LoadTexture(1, selectedVersion);
        }

        private void ClearTextTexture(object sender, RoutedEventArgs e)
        {
            txtSearchTexture.Clear();

            string selectedVersion = ChangesVersionsTexture.SelectedItem?.ToString();

            LoadTexture(currentPageTexture, selectedVersion); // Carga la primera página de mods

            bord3.Visibility = Visibility.Hidden;
            texturepanel.Margin = new Thickness(0, 20, 0, 60);
        }

        private void SearchTexture(int page)
        {
            WebClient webClient = new WebClient();
            string json = webClient.DownloadString("https://gitlab.com/DTXScripts/nexus/-/raw/main/MODS.json");
            Root root = JsonConvert.DeserializeObject<Root>(json);

            // Obtiene el texto de búsqueda del TextBox
            string searchText = txtSearchTexture.Text.ToLower();

            string selectedVersion = ChangesVersionsTexture.SelectedItem?.ToString();

            List<Texture> filteredMods = root.Texturas
                .Where(mod => selectedVersion == null || selectedVersion == "Todas" || mod.version == selectedVersion)
                .ToList();

            filteredMods = filteredMods
                .Where(mod => string.IsNullOrEmpty(searchText) || mod.title.ToLower().Contains(searchText) || mod.title.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            texture_wrap_panel.Children.Clear();

            if (filteredMods.Count > 0)
            {
                foreach (Texture mod in filteredMods)
                {
                    texture_wrap_panel.Children.Add(new TextureItems(mod));
                }
            }
            else
            {
                // Mostrar resultados similares
                List<Texture> similarMods = root.Texturas
                    .Where(mod => selectedVersion == null || selectedVersion == "Todas" || mod.version == selectedVersion)
                    .Where(mod => mod.title.ToLower().Contains(searchText.Substring(0, 1)))
                    .ToList();

                foreach (Texture mod in similarMods)
                {
                    texture_wrap_panel.Children.Add(new TextureItems(mod));
                }
            }

            totalPagesTexture = (int)Math.Ceiling((double)filteredMods.Count / modsPerPageTexture);

            currentPageTexture = page;
            UpdatePageButtonsTexture();
        }

        private void SearchButtonTexture(object sender, RoutedEventArgs e)
        {
            SearchTexture(currentPageTexture);
            if (txtSearchTexture.Text.Length >= 1)
            {
                bord3.Visibility = Visibility.Visible;
                texturepanel.Margin = new Thickness(0, 70, 0, 60);
                txtlabel3.Text = txtSearchTexture.Text;
            }
            else
            {
                bord3.Visibility = Visibility.Hidden;
                texturepanel.Margin = new Thickness(0, 20, 0, 60);
            }
        }
        #endregion

        #region HerramientasPage
        private void LoadHerramientas()
        {
            if (tools_wrap_panel.Children.Count != 0)
            {
                return;
            }
            WebClient webClient = new WebClient();
            foreach (Herramienta mod in JsonConvert.DeserializeObject<Root>(webClient.DownloadString("https://gitlab.com/DTXScripts/nexus/-/raw/main/MODS.json")).Herramientas)
            {
                tools_wrap_panel.Children.Add(new ToolsItems(mod));
            }
        }
        #endregion

        #region ModPack
        private void LoadModPack(int page)
        {
            WebClient webClient = new WebClient();
            string json = webClient.DownloadString("https://gitlab.com/DTXScripts/nexus/-/raw/main/MODS.json");
            Root root = JsonConvert.DeserializeObject<Root>(json);

            List<ModPack> allMods = root.ModPacks;

            totalPagesmodpack = (int)Math.Ceiling((double)allMods.Count / modsPerPagemodpack);

            currentPagemodpack = page;

            int startIndex = (currentPagemodpack - 1) * modsPerPagemodpack;
            int endIndex = Math.Min(startIndex + modsPerPagemodpack, allMods.Count);

            List<ModPack> modsToShow = allMods.GetRange(startIndex, endIndex - startIndex);

            modpack_wrap_panel.Children.Clear();

            foreach (ModPack mod in modsToShow)
            {
                modpack_wrap_panel.Children.Add(new ModPackItems(mod));
            }

            UpdatePageButtonsModPack();
        }

        private void UpdatePageButtonsModPack()
        {
            // Verificar si hay resultados suficientes para más de una página
            if (totalPagesmodpack > 1)
            {
                previous_button4.IsEnabled = currentPagemodpack > 1; // Habilitar botón anterior si no es la primera página
                next_button4.IsEnabled = currentPagemodpack < totalPagesmodpack; // Habilitar botón siguiente si no es la última página

                int firstPage;
                int lastPage;

                if (currentPagemodpack <= 3)
                {
                    firstPage = 1;
                    lastPage = Math.Min(totalPagesmodpack, 5);
                }
                else if (currentPagemodpack >= totalPagesmodpack - 2)
                {
                    firstPage = Math.Max(1, totalPagesmodpack - 4);
                    lastPage = totalPagesmodpack;
                }
                else
                {
                    firstPage = currentPagemodpack - 2;
                    lastPage = currentPagemodpack + 2;
                }

                page_buttons4.Children.Clear();

                if (firstPage > 1)
                {
                    // Agregar botón "..." para indicar que hay más páginas antes
                    RadioButton dotsButtonBefore = new RadioButton()
                    {
                        Content = "...",
                        Width = 30,
                        Background = Brushes.Transparent,
                        Foreground = Brushes.White,
                        Height = 30,
                        FontSize = 14,
                        Cursor = Cursors.Hand,
                        Margin = new Thickness(0, 0, 5, 0),
                        IsEnabled = false, // Deshabilitar el botón de los puntos suspensivos
                        Style = (TryFindResource("rdbutton") as Style)
                    };
                    page_buttons4.Children.Add(dotsButtonBefore);
                }

                for (int i = firstPage; i <= lastPage; i++)
                {
                    RadioButton button = new RadioButton()
                    {
                        Content = i.ToString(),
                        Width = 30,
                        Background = Brushes.Transparent,
                        Foreground = Brushes.White,
                        Height = 30,
                        FontSize = 14,
                        Cursor = Cursors.Hand,
                        Margin = new Thickness(0, 0, 5, 0),
                        Tag = i,
                        Style = (TryFindResource("rdbutton") as Style)
                    };
                    button.Click += PageButton4_Click;
                    page_buttons4.Children.Add(button);

                    if (i == currentPagemodpack)
                    {
                        button.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#3B56E1");// Desactivar botón de la página actual
                    }
                }

                if (lastPage < totalPagesmodpack)
                {
                    // Agregar botón "..." para indicar que hay más páginas después
                    RadioButton dotsButtonAfter = new RadioButton()
                    {
                        Content = "...",
                        Width = 30,
                        Background = Brushes.Transparent,
                        Foreground = Brushes.White,
                        Height = 30,
                        FontSize = 14,
                        Cursor = Cursors.Hand,
                        Margin = new Thickness(0, 0, 5, 0),
                        IsEnabled = false, // Deshabilitar el botón de los puntos suspensivos
                        Style = (TryFindResource("rdbutton") as Style)
                    };
                    page_buttons4.Children.Add(dotsButtonAfter);
                }
                anims.Fade(modpackpanel);
            }
            else
            {
                // Si hay resultados para solo una página, no mostrar los botones de navegación
                previous_button4.IsEnabled = false;
                next_button4.IsEnabled = false;
                page_buttons4.Children.Clear();
            }
        }

        private void PageButton4_Click(object sender, RoutedEventArgs e)
        {
            RadioButton button = (RadioButton)sender;
            int page = (int)button.Tag;
            LoadModPack(page);
        }

        private void previous_button4_Click(object sender, RoutedEventArgs e)
        {
            if (currentPagemodpack > 1)
            {
                LoadModPack(currentPagemodpack - 1); // Cargar los mods de la página anterior
            }
        }

        private void next_button4_Click(object sender, RoutedEventArgs e)
        {
            if (currentPagemodpack < totalPagesmodpack)
            {
                LoadModPack(currentPagemodpack + 1); // Cargar los mods de la siguiente página
            }
        }

        private void ClearTextModPack(object sender, RoutedEventArgs e)
        {
            txtSearchModPack.Text = ""; // Borra el texto del TextBox

            LoadModPack(currentPagemodpack); // Carga la primera página de mods

            bord4.Visibility = Visibility.Hidden;
            modpackpanel.Margin = new Thickness(0, 20, 0, 60);
        }

        private void SearchModPack(int page)
        {
            WebClient webClient = new WebClient();
            string json = webClient.DownloadString("https://gitlab.com/DTXScripts/nexus/-/raw/main/MODS.json");
            Root root = JsonConvert.DeserializeObject<Root>(json);

            // Obtiene el texto de búsqueda del TextBox
            string searchText = txtSearchModPack.Text.ToLower();

            List<ModPack> filteredMods = root.ModPacks
                .Where(mod => string.IsNullOrEmpty(searchText) || mod.title.ToLower().Contains(searchText) || mod.title.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            modpack_wrap_panel.Children.Clear();

            if (filteredMods.Count > 0)
            {
                foreach (ModPack mod in filteredMods)
                {
                    modpack_wrap_panel.Children.Add(new ModPackItems(mod));
                }
            }
            else
            {
                // Mostrar resultados similares
                List<ModPack> similarMods = root.ModPacks
                    .Where(mod => mod.title.ToLower().Contains(searchText.Substring(0, 1)))
                    .ToList();

                foreach (ModPack mod in similarMods)
                {
                    modpack_wrap_panel.Children.Add(new ModPackItems(mod));
                }
            }

            totalPagesmodpack = (int)Math.Ceiling((double)filteredMods.Count / modsPerPagemodpack);

            currentPagemodpack = page;
            UpdatePageButtonsModPack();
        }

        private void SearchButtonModPackClick(object sender, RoutedEventArgs e)
        {
            SearchModPack(currentPagemodpack);
            if (txtSearchModPack.Text.Length >= 1)
            {
                bord4.Visibility = Visibility.Visible;
                modpackpanel.Margin = new Thickness(0, 70, 0, 60);
                txtlabel4.Text = txtSearchModPack.Text;
            }
            else
            {
                bord4.Visibility = Visibility.Hidden;
                modpackpanel.Margin = new Thickness(0, 20, 0, 60);
            }
        }
        #endregion

        #region General
        private void ClearConfig(object sender, RoutedEventArgs e)
        {
            string folderPath = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft\config";

            string[] files = Directory.GetFiles(folderPath);
            foreach (string file in files)
            {
                File.Delete(file); // Eliminar cada archivo en la carpeta
            }

            string[] subfolders = Directory.GetDirectories(folderPath);
            foreach (string subfolder in subfolders)
            {
                Directory.Delete(subfolder, recursive: true); // Eliminar cada subcarpeta en la carpeta de forma recursiva
            }

            Message.ShowBox("La configuración se ha restablecido correctamente.");
        }

        public void CreateDesktopShortcut(string targetPath, string shortcutName)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string shortcutPath = Path.Combine(desktopPath, $"{shortcutName}.url");

            using (StreamWriter writer = new StreamWriter(shortcutPath))
            {
                writer.WriteLine("[InternetShortcut]");
                writer.WriteLine("URL=file:///" + targetPath);
                writer.WriteLine("IconIndex=0");
                writer.WriteLine("IconFile=" + targetPath);
            }
        }

        private void CreateShortcutButton_Click(object sender, RoutedEventArgs e)
        {
            string appPath = Assembly.GetExecutingAssembly().Location;  // Ruta de la aplicación
            string appName = "Nexus";  // Nombre de la aplicación (sin extensión)

            CreateDesktopShortcut(appPath, appName);
            Message.ShowBox("Se ha creado el acceso directo en el escritorio.");
        }

        public bool HasInternetConnection()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                // No hay ninguna conexión de red disponible
                return false;
            }

            // Comprueba si se puede alcanzar una dirección IP externa, por ejemplo, google.com
            Ping ping = new Ping();
            try
            {
                PingReply reply = ping.Send("google.com");
                if (reply.Status == IPStatus.Success)
                {
                    // Se puede alcanzar la dirección IP externa, hay conexión a Internet
                    return true;
                }
            }
            catch (PingException)
            {
                // Ocurrió un error al intentar realizar el ping
            }

            // No se puede alcanzar la dirección IP externa, no hay conexión a Internet
            return false;
        }

        private void UIDrag(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (e.ButtonState == MouseButtonState.Pressed)
                DragMove();
        }

        private void GuardarCambios(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.TopMostValue = (bool)topmost.IsChecked;
            Properties.Settings.Default.Save();
            Message.ShowBox("Se han guardado todos los cambios correctamente");
        }


        private void txtSearchModPack_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SearchModPacks.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                Keyboard.ClearFocus();
            }
        }


        private void OpenMod(object sender, RoutedEventArgs e)
        {
            string targetFolderPath = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft\mods\";
            Process.Start(targetFolderPath);
        }

        private void OpenTexture(object sender, RoutedEventArgs e)
        {
            string targetFolderPath = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft\resourcepacks\";
            Process.Start(targetFolderPath);
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Maximize(object sender, RoutedEventArgs e)
        {
            if (base.WindowState == WindowState.Maximized)
            {
                base.WindowState = WindowState.Normal;
                window2.CornerRadius = new CornerRadius(20, 20, 20, 20);
            }
            else
            {
                this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight - 16;
                window2.CornerRadius = new CornerRadius(0, 0, 0, 0);
                base.WindowState = WindowState.Maximized;
            }
        }

        private void Minimize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Modsbtn_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 0;
        }

        private void Resourcesbtn_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 1;
        }

        private void ModPackbtn_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 2;
        }

        private void CloseHelp(object sender, RoutedEventArgs e)
        {
            popHelp.Visibility = Visibility.Hidden;
        }

        private void ShowHelps(object sender, RoutedEventArgs e)
        {
            if (popSettings.Visibility == Visibility.Hidden)
            {
                popHelp.Visibility = Visibility.Visible;
            }
            else
            {
                popHelp.Visibility = Visibility.Hidden;
            }
        }

        private void ShowSettings(object sender, RoutedEventArgs e)
        {
            if (popHelp.Visibility == Visibility.Hidden)
            {
                popSettings.Visibility = Visibility.Visible;
            }
            else
            {
                popSettings.Visibility = Visibility.Hidden;
            }
        }

        private void CloseSettings(object sender, RoutedEventArgs e)
        {
            popSettings.Visibility = Visibility.Hidden;
        }

        private void txtSearchMod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                searchMOD.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                Keyboard.ClearFocus();
            }
        }

        private void txtSearchTexturasMod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SearchTEXTURE.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                Keyboard.ClearFocus();
            }
        }

        private void showtools_Click(object sender, RoutedEventArgs e)
        {
            if (popSettings.Visibility == Visibility.Visible)
            {
                anims.Fade(toolspanel);
                popTools.Visibility = Visibility.Visible;
            }
            else
            {
                popTools.Visibility = Visibility.Hidden;
            }
        }

        private void CloseTools(object sender, RoutedEventArgs e)
        {
            popTools.Visibility = Visibility.Hidden;
        }

        private void topmost_Checked(object sender, RoutedEventArgs e)
        {
            base.Topmost = true;
        }

        private void topmost_Unchecked(object sender, RoutedEventArgs e)
        {
            base.Topmost = false;
        }
        #endregion

    }
}
