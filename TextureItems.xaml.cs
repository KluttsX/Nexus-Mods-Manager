using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Nexus
{
    /// <summary>
    /// Lógica de interacción para TextureItems.xaml
    /// </summary>
    public partial class TextureItems : UserControl
    {
        public Texture this__texture;

        string id;

        string version;

        bool active;

        public TextureItems(Texture this_texture)
        {
            InitializeComponent();
            string imagePath = this_texture.imgURL;
            BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath));
            imgShow.ImageSource = bitmapImage;
            title.Text = this_texture.title;
            link.Text = this_texture.linkURL;
            description.Text = this_texture.description;
            id = this_texture.id;
            version = this_texture.version;
            titledetails.Text = this_texture.title;
            txtdescription.Text = this_texture.description;

            int indicePunto = txtdescription.Text.IndexOf(':');
            if (indicePunto != -1)
            {
                // Insertar salto de línea después del primer punto
                txtdescription.Text = txtdescription.Text.Insert(indicePunto + 1, Environment.NewLine + Environment.NewLine);
            }
        }

        private void template_Loaded(object sender, RoutedEventArgs e)
        {
            string targetFolderPath = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft\resourcepacks\" + id;
            if (!File.Exists(targetFolderPath))
            {
                status.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#F96383");
                status.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#F96383");
                deletebtn.Visibility = Visibility.Hidden;
                installbtn.Visibility = Visibility.Visible;
            }
            else
            {
                status.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#7DDF94");
                status.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#7DDF94");
                deletebtn.Visibility = Visibility.Visible;
                installbtn.Visibility = Visibility.Hidden;
            }
        }

        private void DeleteResource(object sender, RoutedEventArgs e)
        {
            var folderPath = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft\resourcepacks\" + id;
            if (!File.Exists(folderPath))
            {
                var name = id;
                var message = Message.ShowBox($"Ha ocurrido un error al trata de desintalar el Resources Packs {id}.");
            }
            else
            {
                File.Delete(folderPath);
                var name = title.Text;
                var message = Message.ShowBox($"Se ha desinstalado el Resources Packs {name} correctamente.");
            }
        }

        private void template_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (popDeitals.Visibility == Visibility.Hidden)
            {
                popDeitals.Visibility = Visibility.Visible;
            }
            else
            {

            }
        }

        private void installbtn_Click(object sender, RoutedEventArgs e)
        {
            string pastebinUrl = link.Text;
            string targetFolderPath = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft\resourcepacks";
            string tempZipPath = Path.Combine(targetFolderPath, id);
            string name = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft\resourcepacks\" + id;

            using (WebClient webClient = new WebClient())
            {

                // Descargar el archivo ZIP desde Pastebin
                webClient.DownloadFileTaskAsync(new Uri(pastebinUrl), tempZipPath);
                webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
                webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;
            }
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            // Actualiza la barra de progreso o realiza cualquier otra acción con los datos de progreso
            int progress = e.ProgressPercentage;
            installbtn.Content = $"Progress: {progress}%";
        }


        private void WebClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            installbtn.Content = "Install";
            var name = title.Text;
            var message = Message.ShowBox($"Se Ha instalado el Resource Packs {name} correctamente.");
            popDeitals.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            popDeitals.Visibility = Visibility.Hidden;
        }
    }
}
