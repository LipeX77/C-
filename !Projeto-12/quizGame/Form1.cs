using System;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace quizGame
{
    public partial class Form1 : Form
    {
        // ====== estado do quiz original ======
        int correctAnswer;
        int questionNumber = 1;
        int score;
        int totalQuestions;

        // ====== UI auxiliar (menu + feedback) ======
        private Panel pnlMenu;          // menu de categorias
        private Label lblFeedback;      // ✓ / ✗
        private System.Windows.Forms.Timer feedbackTimer;        // delay curto entre uma pergunta e outra
        private readonly int feedbackMs = 600;
        private bool somAtivo = true;

        // ====== categoria selecionada ======
        private string categoriaAtual;  // "Jogos" | "Anime" | "Filmes" | "História"

        public Form1()
        {
            InitializeComponent();
            // nada aqui — usamos o Load para montar a UI auxiliar
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            // cria UI extra
            BuildFeedbackUI();
            BuildMenuUI();

            // timer do feedback ✓/✗
            feedbackTimer = new System.Windows.Forms.Timer();
            feedbackTimer.Interval = feedbackMs;
            feedbackTimer.Tick += FeedbackTimer_Tick;


            // começa no menu
            MostrarMenu();

        }

        // ============================
        // ======= FLUXO DO JOGO ======
        // ============================

        private void ClickAnswerEvent(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            int tag = Convert.ToInt32(btn.Tag);
            bool correto = tag == correctAnswer;

            MostrarFeedback(correto);
            if (somAtivo)
            {
                if (correto) SystemSounds.Asterisk.Play();
                else SystemSounds.Hand.Play();
            }

            if (correto) score++;

            if (questionNumber == totalQuestions)
            {
                int perc = (int)Math.Round(100.0 * score / totalQuestions);
                MessageBox.Show(
                    "Quiz finalizado!" + Environment.NewLine +
                    $"Você acertou {score} de {totalQuestions}." + Environment.NewLine +
                    $"Sua porcentagem total é {perc}%." + Environment.NewLine +
                    "Clique OK para voltar ao menu.");

                MostrarMenu();
                return;
            }

            questionNumber++;
            HabilitarBotoes(false);
            feedbackTimer.Start(); // mostra ✓/✗ por alguns ms antes de trocar
        }

        private void askQuestion(int qnum)
        {
            if (string.IsNullOrEmpty(categoriaAtual)) return;

            switch (categoriaAtual)
            {
                case "Jogos": PerguntasJogos(qnum); break;
                case "Anime": PerguntasAnime(qnum); break;
                case "Filmes": PerguntasFilmes(qnum); break;
                case "História": PerguntasHistoria(qnum); break;
            }

            // mantém padrão de Tags 1..4
            button1.Tag = 1; button2.Tag = 2; button3.Tag = 3; button4.Tag = 4;

            HabilitarBotoes(true);
            lblFeedback.Visible = false;
        }

        // ============================
        // ======== CATEGORIAS ========
        // ============================

        // ---- JOGOS ----
        private void PerguntasJogos(int q)
        {
            switch (q)
            {
                case 1:
                    pictureBox1.Image = Properties.Resources.fortnite;
                    lblQuestion.Text = "Qual empresa desenvolveu o jogo mostrado acima?";
                    button1.Text = "EA"; button2.Text = "Activision"; button3.Text = "Square Enix"; button4.Text = "Epic Games";
                    correctAnswer = 4; break;

                case 2:
                    pictureBox1.Image = Properties.Resources.gears_of_war;
                    lblQuestion.Text = "Qual é o nome deste jogo?";
                    button1.Text = "Gears of War"; button2.Text = "Call of Duty"; button3.Text = "Battlefield"; button4.Text = "Bionic Commando";
                    correctAnswer = 1; break;

                case 3:
                    pictureBox1.Image = Properties.Resources.halo;
                    lblQuestion.Text = "Qual é o nome do protagonista de Halo?";
                    button1.Text = "Altair"; button2.Text = "Lara Croft"; button3.Text = "Master Chief"; button4.Text = "Drake";
                    correctAnswer = 3; break;

                case 4:
                    pictureBox1.Image = Properties.Resources.csgo;
                    lblQuestion.Text = "Qual é o nome deste jogo?";
                    button1.Text = "CS:GO"; button2.Text = "Call of Duty"; button3.Text = "Battlefield"; button4.Text = "Half-Life 3";
                    correctAnswer = 1; break;

                case 5:
                    pictureBox1.Image = Properties.Resources.witcher3;
                    lblQuestion.Text = "Em The Witcher 3, quem Geralt procura?";
                    button1.Text = "Victoria"; button2.Text = "Donuts"; button3.Text = "Ciri"; button4.Text = "Yennefer";
                    correctAnswer = 3; break;
            }
        }

        // ---- ANIME ----
        private void PerguntasAnime(int q)
        {
            pictureBox1.Image = Properties.Resources.questions;

            switch (q)
            {
                case 1:
                    pictureBox1.Image = Properties.Resources.kurama;
                    lblQuestion.Text = "Em Naruto, qual é o nome da raposa de nove caudas selada no protagonista?";
                    button1.Text = "Shukaku"; button2.Text = "Gyūki"; button3.Text = "Kurama"; button4.Text = "Matatabi";
                    correctAnswer = 3; break;

                case 2:
                    pictureBox1.Image = Properties.Resources.thousand_sunny;
                    lblQuestion.Text = "Em One Piece, qual é o nome do navio que substitui o Going Merry?";
                    button1.Text = "Going Sunny"; button2.Text = "Thousand Sunny"; button3.Text = "Red Force"; button4.Text = "Oro Jackson";
                    correctAnswer = 2; break;

                case 3:
                    pictureBox1.Image = Properties.Resources.goku;
                    lblQuestion.Text = "Em Dragon Ball, qual é a técnica mais famosa do Goku?";
                    button1.Text = "Rasengan"; button2.Text = "Kamehameha"; button3.Text = "Final Flash"; button4.Text = "Bankai";
                    correctAnswer = 2; break;

                case 4:
                    pictureBox1.Image = Properties.Resources.killua;
                    lblQuestion.Text = "Em Hunter x Hunter, quem é o melhor amigo de Gon?";
                    button1.Text = "Leorio"; button2.Text = "Kurapika"; button3.Text = "Killua"; button4.Text = "Hisoka";
                    correctAnswer = 3; break;

                case 5:
                    pictureBox1.Image = Properties.Resources.mikey;
                    lblQuestion.Text = "Em Tokyo Revengers, qual é o nome da gangue liderada por Mikey?";
                    button1.Text = "Valhalla"; button2.Text = "Black Dragons"; button3.Text = "Tokyo Manji Gang (Toman)"; button4.Text = "Tenjiku";
                    correctAnswer = 3; break;
            }
        }

        // ---- FILMES ----
        private void PerguntasFilmes(int q)
        {
            pictureBox1.Image = Properties.Resources.questions;

            switch (q)
            {
                case 1:
                    pictureBox1.Image = Properties.Resources.stark;
                    lblQuestion.Text = "No Universo Marvel, qual é o alter ego do Homem de Ferro?";
                    button1.Text = "Bruce Wayne"; button2.Text = "Clark Kent"; button3.Text = "Tony Stark"; button4.Text = "Peter Parker";
                    correctAnswer = 3; break;

                case 2:
                    pictureBox1.Image = Properties.Resources.gotham;
                    lblQuestion.Text = "No Universo DC, qual cidade é protegida pelo Batman?";
                    button1.Text = "Metrópolis"; button2.Text = "Gotham"; button3.Text = "Central City"; button4.Text = "Star City";
                    correctAnswer = 2; break;

                case 3:
                    pictureBox1.Image = Properties.Resources.joiadotempo;
                    lblQuestion.Text = "Qual Joia do Infinito controla o tempo?";
                    button1.Text = "Joia da Realidade"; button2.Text = "Joia do Espaço"; button3.Text = "Joia do Tempo"; button4.Text = "Joia da Alma";
                    correctAnswer = 3; break;

                case 4:
                    pictureBox1.Image = Properties.Resources.superman;
                    lblQuestion.Text = "Qual símbolo aparece no peito do Superman?";
                    button1.Text = "Um morcego"; button2.Text = "A letra \"S\""; button3.Text = "Um raio"; button4.Text = "Uma águia";
                    correctAnswer = 2; break;

                case 5:
                    pictureBox1.Image = Properties.Resources.black;
                    lblQuestion.Text = "Quem é o rei de Wakanda em Pantera Negra?";
                    button1.Text = "M'Baku"; button2.Text = "Erik Killmonger"; button3.Text = "T'Challa"; button4.Text = "Shuri";
                    correctAnswer = 3; break;
            }
        }

        // ---- HISTÓRIA (Brasil) ----
        private void PerguntasHistoria(int q)
        {
            pictureBox1.Image = Properties.Resources.questions;

            switch (q)
            {
                case 1:
                    pictureBox1.Image = Properties.Resources.dom;
                    lblQuestion.Text = "Quem foi o primeiro imperador do Brasil?";
                    button1.Text = "Dom Pedro I"; button2.Text = "Dom Pedro II"; button3.Text = "Tiradentes"; button4.Text = "Deodoro da Fonseca";
                    correctAnswer = 1; break;

                case 2:
                    pictureBox1.Image = Properties.Resources.republica;
                    lblQuestion.Text = "Em que ano foi proclamada a República no Brasil?";
                    button1.Text = "1888"; button2.Text = "1889"; button3.Text = "1822"; button4.Text = "1930";
                    correctAnswer = 2; break;

                case 3:
                    pictureBox1.Image = Properties.Resources.zumbi_dos_palmares;
                    lblQuestion.Text = "Quem foi o líder do Quilombo dos Palmares?";
                    button1.Text = "Tiradentes"; button2.Text = "Zumbi dos Palmares"; button3.Text = "José Bonifácio"; button4.Text = "Duque de Caxias";
                    correctAnswer = 2; break;

                case 4:
                    pictureBox1.Image = Properties.Resources.josé;
                    lblQuestion.Text = "Quem é conhecido como o 'Patriarca da Independência'?";
                    button1.Text = "José Bonifácio"; button2.Text = "Dom Pedro II"; button3.Text = "Getúlio Vargas"; button4.Text = "Rui Barbosa";
                    correctAnswer = 1; break;

                case 5:
                    pictureBox1.Image = Properties.Resources.incofidencia;
                    lblQuestion.Text = "Qual movimento teve Tiradentes como símbolo?";
                    button1.Text = "Diretas Já"; button2.Text = "Revolta da Armada"; button3.Text = "Inconfidência Mineira"; button4.Text = "Revolução Farroupilha";
                    correctAnswer = 3; break;
            }
        }

        // ============================
        // ====== UI AUXILIAR =========
        // ============================

        private void BuildMenuUI()
        {
            pnlMenu = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(245, 245, 245),
                Visible = false
            };

            var lblTitle = new Label
            {
                Text = "Escolha a categoria",
                Dock = DockStyle.Top,
                Height = 80,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 18, FontStyle.Bold)
            };

            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 160,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(20),
                WrapContents = false
            };

            Button mk(string texto, string tag)
            {
                var b = new Button
                {
                    Text = texto,
                    Tag = tag,
                    Width = 180,
                    Height = 50,
                    Font = new Font("Segoe UI", 11)
                };
                b.Click += EscolherCategoriaEvent;
                return b;
            }

            flow.Controls.Add(mk("Jogos", "Jogos"));
            flow.Controls.Add(mk("Anime", "Anime"));
            flow.Controls.Add(mk("Filmes", "Filmes"));
            flow.Controls.Add(mk("História", "História"));

            // (Opcional) botão para ligar/desligar som
            var toggleSom = new CheckBox
            {
                Text = "Som ativo",
                Checked = somAtivo,
                AutoSize = true,
                Margin = new Padding(20, 30, 0, 0)
            };
            toggleSom.CheckedChanged += (s, e) => somAtivo = ((CheckBox)s).Checked;
            flow.Controls.Add(toggleSom);

            pnlMenu.Controls.Add(flow);
            pnlMenu.Controls.Add(lblTitle);
            this.Controls.Add(pnlMenu);
            pnlMenu.BringToFront();
        }

        private void EscolherCategoriaEvent(object sender, EventArgs e)
        {
            categoriaAtual = ((Button)sender).Tag.ToString();

            // 5 perguntas por categoria
            totalQuestions = 5;

            // reset e começa
            score = 0;
            questionNumber = 1;

            pnlMenu.Visible = false;
            HabilitarBotoes(true);
            askQuestion(questionNumber);
        }

        private void MostrarMenu()
        {
            pictureBox1.Image = Properties.Resources.questions;
            lblQuestion.Text = "Selecione uma categoria para começar.";
            lblFeedback?.Hide();

            HabilitarBotoes(false);
            pnlMenu.Visible = true;

            categoriaAtual = null;
            score = 0;
            questionNumber = 1;
        }

        private void BuildFeedbackUI()
        {
            lblFeedback = new Label
            {
                AutoSize = false,
                Text = "",
                Font = new Font("Segoe UI", 48, FontStyle.Bold),
                ForeColor = Color.LimeGreen,
                BackColor = Color.Transparent,
                Width = 100,
                Height = 80,
                Visible = false
            };

            // posiciona próximo da imagem
            lblFeedback.Location = new Point(pictureBox1.Right - 110, pictureBox1.Top + 10);
            this.Controls.Add(lblFeedback);
            lblFeedback.BringToFront();
        }

        private void MostrarFeedback(bool correto)
        {
            lblFeedback.Text = correto ? "✓" : "✗";
            lblFeedback.ForeColor = correto ? Color.LimeGreen : Color.IndianRed;
            lblFeedback.Visible = true;
            lblFeedback.BringToFront();
        }

        private void FeedbackTimer_Tick(object sender, EventArgs e)
        {
            feedbackTimer.Stop();
            lblFeedback.Visible = false;
            askQuestion(questionNumber); // carrega a próxima
        }

        private void HabilitarBotoes(bool ativo)
        {
            button1.Enabled = button2.Enabled = button3.Enabled = button4.Enabled = ativo;
        }
    }
}
