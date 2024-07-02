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
    /// Lógica de interacción para ModPackItems.xaml
    /// </summary>
    public partial class ModPackItems : UserControl
    {
        public ModPack this__modpack;

        string id;

        string linkURL;

        public ModPackItems(ModPack this_modpack)
        {
            InitializeComponent();
            this__modpack = this_modpack;
            string imagePath = this_modpack.imgURL;
            BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath));

            imgShow.ImageSource = bitmapImage;
            id = this_modpack.id;
            title.Text = this_modpack.title;
            linkURL = this_modpack.linkURL;
            versionstxt.Text = this_modpack.version;

            string modList = this_modpack.ListMods;
            modList = modList.Replace(",", Environment.NewLine);
            txtmods.Text = modList;
        }

        private void template_Loaded(object sender, RoutedEventArgs e)
        {


        }

        private void template_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (showdeitals.Visibility == Visibility.Hidden)
            {
                showdeitals.Visibility = Visibility.Visible;
                imgShow.Opacity = 0;
                showversion.Opacity = 0;
            }
            else
            {


            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            showdeitals.Visibility = Visibility.Hidden;
            showversion.Opacity = 1;
            imgShow.Opacity = 1;
        }

        private void installbtn_Click(object sender, RoutedEventArgs e)
        {
            string targetFolderPath = @"C:\Users\" + Environment.UserName + @"\Downloads\";
            string tempZipPath = Path.Combine(targetFolderPath, id);

            using (WebClient webClient = new WebClient())
            {

                // Descargar el archivo ZIP desde Pastebin
                webClient.DownloadFileTaskAsync(new Uri(linkURL), tempZipPath);
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
            var message = Message.ShowBox($"Se Ha instalado el Resource Packs {name} correctamente.");
            showdeitals.Visibility = Visibility.Hidden;
            imgShow.Opacity = 1;
        }
    }
}
