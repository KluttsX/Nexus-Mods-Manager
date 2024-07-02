using System.Windows;
using System.Windows.Input;

namespace Nexus
{
    /// <summary>
    /// Lógica de interacción para Message.xaml
    /// </summary>
    public partial class Message : Window
    {
        static Message message;

        public Message()
        {
            InitializeComponent();
        }

        public static string ShowBox(string txtMessage)
        {
            message = new Message();
            message.Messagetxt.Text = txtMessage;
            message.ShowDialog();
            return txtMessage;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (e.ButtonState == MouseButtonState.Pressed)
                DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
