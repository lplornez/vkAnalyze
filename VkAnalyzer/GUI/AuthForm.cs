using System;
using System.Windows.Forms;
using Presentation.Authorization;

namespace GUI
{
    public partial class AuthForm : Form, IAuthView
    {
        public AuthForm()
        {
            InitializeComponent();
            MdiParent = ActiveForm?.Owner;
        }

        public object DataSource {
            get
            {
              return authViewModelBindingSource.DataSource;
            }
            set
            {
                if (authViewModelBindingSource.DataSource != value)
                    authViewModelBindingSource.DataSource = value;
            } }
        public void FormShow()
        {
            ShowDialog(Owner);
        }

        public void FormClose()
        {
            Close();
        }
        
        private void AuthForm_Load(object sender, EventArgs e)
        {
        }
    }
}
