using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace Nexus
{
    /// <summary>
    /// Lógica de interacción para Items.xaml
    /// </summary>
    public partial class Items : UserControl
    {
        public Mod this__mod;

        public Items(Mod this_mod)
        {
            InitializeComponent();
            this__mod = this_mod;
            id.Text = this_mod.id;
            titleitems.Text = this_mod.title;
            description.Text = this_mod.description;
            link.Text = this_mod.linkURL;
            versionstxt.Text = this_mod.version;
        }

        private void btninstall_Click(object sender, RoutedEventArgs e)
        {
            string pastebinUrl = link.Text;
            string targetFolderPath = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft\mods";
            string tempZipPath = Path.Combine(targetFolderPath, id.Text);

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
            txtinstall.Text = $"{progress}%";
        }

        private void WebClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            foreach (Window SWindow in Application.Current.Windows)
            {
                if (SWindow is MainWindow)
                {
                    MainWindow ParentWindow = (MainWindow)SWindow;
                    //ParentWindow.UpdatePageMods();
                }
            }
            var name = titleitems.Text;
            var message = Message.ShowBox($"Se Ha instalado el mod {name} correctamente.");
            txtinstall.Text = "Install";
        }

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            var folderPath = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft\mods\" + id.Text;
            if (!File.Exists(folderPath))
            {
                statusinstalled.Visibility = Visibility.Hidden;
                btninstall.Visibility = Visibility.Visible;
                btndelete.Visibility = Visibility.Hidden;
            }
            else
            {
                statusinstalled.Visibility = Visibility.Visible;
                btndelete.Visibility = Visibility.Visible;
                btninstall.Visibility = Visibility.Hidden;
            }
        }

        private void btndelete_Click(object sender, RoutedEventArgs e)
        {
            var folderPath = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft\mods\" + id.Text;
            if (!File.Exists(folderPath))
            {
                var name = titleitems.Text;
                var message = Message.ShowBox($"Ha ocurrido un error al trata de desintalar el mod {name}.");
            }
            else
            {
                File.Delete(folderPath);
                foreach (Window SWindow in Application.Current.Windows)
                {
                    if (SWindow is MainWindow)
                    {
                        MainWindow ParentWindow = (MainWindow)SWindow;
                        //ParentWindow.UpdatePageMods();
                    }
                }
                var name = titleitems.Text;
                var message = Message.ShowBox($"Se ha desinstalado el mod {name} correctamente.");
            }
        }
    }
}
