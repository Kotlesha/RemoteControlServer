using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteControlClient.Views
{
    public partial class TextChatForm : Form
    {
        public TextChatForm()
        {
            InitializeComponent();
        }

        private void Send_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textInput.Text.Trim()))
            {
                MessageBox.Show("Введена пустая строка!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textInput.Focus();
                return;
            }
        }
    }
}
