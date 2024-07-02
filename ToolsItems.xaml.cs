using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Nexus
{
    /// <summary>
    /// Lógica de interacción para ToolsItems.xaml
    /// </summary>
    public partial class ToolsItems : UserControl
    {
        public Herramienta this__herramienta;

        string id;

        string pastebinUrl;

        public ToolsItems(Herramienta this_herramienta)
        {
            InitializeComponent();
            string imagePath = this_herramienta.imgShow;
            BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath));
            imgShow.ImageSource = bitmapImage;
            title.Text = this_herramienta.title;
            description.Text = this_herramienta.description;
            txtdescription.Text = this_herramienta.description;

            pastebinUrl = this_herramienta.linkURL;
            id = this_herramienta.id;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            showdeitals.Visibility = Visibility.Hidden;
            imgShow.Opacity = 1;
        }

        private void template_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (showdeitals.Visibility == Visibility.Hidden)
            {
                showdeitals.Visibility = Visibility.Visible;
                imgShow.Opacity = 0;
            }
        }

        private void installbtn(object sender, RoutedEventArgs e)
        {
            string targetFolderPath = @"C:\Users\" + Environment.UserName + @"\Downloads\";
            string tempZipPath = Path.Combine(targetFolderPath, id);

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
            install.Content = $"Progress: {progress}%";
        }


        private void WebClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            install.Content = "Install";
            var name = title.Text;
            var message = Message.ShowBox($"Se Ha instalado la herriamenta {name} correctamente.");
            showdeitals.Visibility = Visibility.Hidden;
            imgShow.Opacity = 1;
        }
    }
}
