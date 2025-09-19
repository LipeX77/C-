using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp2.control;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IdeiaInovacaoControle objIIControle = new();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            objIIControle.ControleCadastrarII(textBoxArea.Text,textBoxIdeia.Text , float.Parse(textBoxCusto.Text));

            if (!string.IsNullOrEmpty(textBoxArea.Text) && !string.IsNullOrEmpty(textBoxIdeia.Text)) 
            {
                if(objIIControle.ControleCadastrarII(textBoxArea.Text, textBoxIdeia.Text, float.Parse(textBoxCusto.Text)))
                {
                    MessageBox.Show("Cadastro realizado com sucesso");
                }
            }
            else
            {
                MessageBox.Show("Campos obrigatórios vazios");
            }

            BD.BD.RetornarBD().ForEach(x => Debug.WriteLine(x));
        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}