using System;
using System.Windows.Forms;
using Presentation.ShellPresenter;

namespace GUI
{
    public partial class MainForm : Form, IShellView
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public object DataSource { get; set; }
        public void Run()
        {
            Application.EnableVisualStyles();
            Application.Run(this);
            
        }

        public void Host()
        {
            //*Расположение элементов интерфейса
        }

        public event EventHandler FormLoad;

        private void MainForm_Load(object sender, EventArgs e)
        {
            FormLoad?.Invoke(this,EventArgs.Empty);
        }
    }
}
