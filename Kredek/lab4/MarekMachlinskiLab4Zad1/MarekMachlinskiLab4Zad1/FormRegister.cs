using System;
using System.Windows.Forms;

namespace MarekMachlinskiLab4Zad1
{
    /// <summary>
    /// Umożliwia dodanie użytkownika
    /// </summary>
    public partial class FormRegister : Form
    {
        /// <summary>
        /// Obiekt okna nadrzędnego
        /// </summary>
        readonly FormMainWindow parent = null;
        
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="newParent">Okno nadrzędne</param>
        public FormRegister(FormMainWindow newParent)
        {
            InitializeComponent();
            parent = newParent;
        }

        /// <summary>
        /// Zamyka okno i nic nie robi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Dodaje nowego użytkownika i zamyka okno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAccept_Click(object sender, EventArgs e)
        {
            if (textBoxLogin.TextLength > 0 && textBoxPassword.TextLength > 0 && textBoxPasswordRepeat.TextLength > 0)
            {
                if (textBoxPassword.Text == textBoxPasswordRepeat.Text)
                {
                    if (parent != null)
                        parent.Users.Create(new Tables.User(textBoxLogin.Text, textBoxPassword.Text, parent));
                    else
                        MessageBox.Show("Cannot add new user right now.");
                    Close();
                }
                else
                    MessageBox.Show("Repeated password don't match with field password.");
            }
            else
                MessageBox.Show("Fill login and password fields.");
        }
    }
}
