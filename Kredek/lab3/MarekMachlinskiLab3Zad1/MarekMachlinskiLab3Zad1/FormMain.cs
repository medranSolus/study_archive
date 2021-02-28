using System;
using System.Media;
using System.Data.SqlClient;
using System.Drawing;
using System.Data;
using System.Management;
using System.Windows.Forms;

namespace MarekMachlinskiLab3Zad1
{
    /// <summary>
    /// Główne okno organizatora
    /// </summary>
    public partial class FormMain : Form
    {
        /// <summary>
        /// Nazwa naszej bazy danych
        /// </summary>
        public static string DataSource { get; } = @"MARKBOOK\MEDRANSQL";
        /// <summary>
        /// Dźwięk kliknięcia na przycisk
        /// </summary>
        SoundPlayer click = new SoundPlayer(Properties.Resources.Click);
        /// <summary>
        /// Czas startu aplikacji
        /// </summary>
        DateTime startTime = DateTime.Now;
        /// <summary>
        /// Ikona na pasku powiadomień
        /// </summary>
        static NotifyIcon trayIcon = new NotifyIcon();
        /// <summary>
        /// Menu konteksowe wyświetlane po kliknięciu na ikonę na pasku powiadomień
        /// </summary>
        static ContextMenu trayMenu = new ContextMenu();
        /// <summary>
        /// Obiekt obsługujący pojawienie się nowego procesu w systemie
        /// </summary>
        ManagementEventWatcher processStartWatcher = new ManagementEventWatcher("SELECT * FROM Win32_ProcessStartTrace");
        /// <summary>
        /// Obiekt obsługujący zamknięcie procesu w systemie
        /// </summary>
        ManagementEventWatcher processStopWatcher = new ManagementEventWatcher("SELECT * FROM Win32_ProcessStopTrace");

        /// <summary>
        /// Tworzy okno i menu kontekstowe znajdujące się w ikonie na pasku powiadomień
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
            trayIcon.Text = "Organiser";
            trayIcon.Icon = new Icon(Icon, 265, 256);
            trayMenu.MenuItems.Add("Show", (object sender, EventArgs e) => ShowInTaskbar = Enabled = Visible = true);
            trayMenu.MenuItems.Add("Exit", (object sender, EventArgs e)=> 
            {
                string query = @"IF EXISTS (SELECT * FROM Users WHERE Name=@Name) UPDATE Users SET LastTimeActive=@LastTimeActive WHERE Name=@Name; ELSE INSERT INTO Users(Name, LastTimeActive) VALUES(@Name, @LastTimeActive);";
                using (SqlConnection connection = new SqlConnection($@"Data Source={DataSource}; database=Organiser; Trusted_Connection=yes;"))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Environment.UserName;
                    command.Parameters.Add("@LastTimeActive", SqlDbType.BigInt).Value = (DateTime.Now - startTime).Ticks;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                ShowInTaskbar = Enabled = Visible = false;
                trayIcon.Dispose();
                Dispose();
                Application.Exit();
            });
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible = true;
            processStartWatcher.EventArrived += new EventArrivedEventHandler(ProcessStartEvent);
            processStartWatcher.Start();
            processStopWatcher.EventArrived += new EventArrivedEventHandler(ProcessStopEvent);
            processStopWatcher.Start();
        }

        /// <summary>
        /// Dodaje nowy proces do tabeli Processes jeśli się pojawi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void ProcessStartEvent(object sender, EventArrivedEventArgs e)
        {
            string processName = e.NewEvent.Properties["ProcessName"].Value.ToString();
            string query = @"IF EXISTS (SELECT * FROM Processes WHERE Name=@Name) UPDATE Processes SET LastOpen=@LastOpen WHERE Name=@Name; ELSE INSERT INTO Processes(Name, LastOpen, AverageTimeUsed) VALUES(@Name, @LastOpen, 0);";
            using (SqlConnection connection = new SqlConnection($@"Data Source={DataSource}; database=Organiser; Trusted_Connection=yes;"))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = processName;
                command.Parameters.Add("@LastOpen", SqlDbType.DateTime).Value = DateTime.Now;
                await connection.OpenAsync();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        /// <summary>
        /// Aktualizuje rekord w tabeli Processes uzupełniająć go o informację o ostatnim zamknięciu i średnim czasi użycia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void ProcessStopEvent(object sender, EventArrivedEventArgs e)
        {
            long timeEnd = DateTime.Now.Ticks;
            string processName = e.NewEvent.Properties["ProcessName"].Value.ToString();
            string queryGet = @"IF EXISTS (SELECT * FROM Processes WHERE Name=@Name) SELECT LastOpen, AverageTimeUsed FROM Processes WHERE Name=@Name;";
            string querySet = @"UPDATE Processes SET LastClosed=@LastClosed, AverageTimeUsed=@AverageTimeUsed WHERE Name=@Name;";
            using (SqlConnection connectionGet = new SqlConnection($@"Data Source={DataSource}; database=Organiser; Trusted_Connection=yes;"))
            using (SqlCommand commandGet = new SqlCommand(queryGet, connectionGet))
            {
                commandGet.Parameters.Add("@Name", SqlDbType.NVarChar).Value = processName;
                await connectionGet.OpenAsync();
                using (SqlDataReader reader = commandGet.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        DateTime openTime = Convert.ToDateTime(reader[0]);
                        long averageTime = Convert.ToInt64(reader[1]);
                        reader.Close();
                        averageTime = (averageTime + timeEnd - openTime.Ticks) / 2;
                        using (SqlConnection connectionSet = new SqlConnection($@"Data Source={DataSource}; database=Organiser; Trusted_Connection=yes;"))
                        using (SqlCommand commandSet = new SqlCommand(querySet, connectionSet))
                        {
                            commandSet.Parameters.Add("@Name", SqlDbType.NVarChar).Value = processName;
                            commandSet.Parameters.Add("@LastClosed", SqlDbType.DateTime).Value = DateTime.FromBinary(timeEnd);
                            commandSet.Parameters.Add("@AverageTimeUsed", SqlDbType.BigInt).Value = averageTime;
                            await connectionSet.OpenAsync();
                            commandSet.ExecuteNonQuery();
                            connectionSet.Close();
                        }
                    }
                    else
                        reader.Close();
                }
                connectionGet.Close();
            }
        }

        /// <summary>
        /// Uniemożliwia zamknięcie programu poprzez zamknięcie okna (wyjątek to zamknięcie systemu)
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.WindowsShutDown)
                return;
            e.Cancel = true;
            ShowInTaskbar = Enabled = Visible = false;            
        }

        /// <summary>
        /// Pobiera i wyświetla zawartość tabeli podanej w kwerendzie
        /// </summary>
        /// <param name="query">Kwerenda precyzująca z której tabeli należy pobrać zawartość</param>
        private async void ShowData(string query)
        {
            using (SqlConnection connection = new SqlConnection($@"Data Source={DataSource}; database=Organiser; Trusted_Connection=yes;"))
            {
                SqlDataAdapter data = new SqlDataAdapter("SELECT * FROM " + query, connection);
                await connection.OpenAsync();
                DataTable table = new DataTable();
                data.Fill(table);
                connection.Close();
                dataGridViewTable.DataSource = table;
            }
        }

        /// <summary>
        /// Wyświetla zawartość tabeli LifeGoals
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonShowGoals_Click(object sender, EventArgs e)
        {
            click.Play();
            ShowData("GoalsView");
        }

        /// <summary>
        /// Wyświetla zawartość tabeli Contacts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonShowContacts_Click(object sender, EventArgs e)
        {
            click.Play();
            ShowData("ContactsView");
        }

        /// <summary>
        /// Wyświetla zawartość tabeli Meetings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonShowMeetings_Click(object sender, EventArgs e)
        {
            click.Play();
            ShowData("MeetingsView");
        }

        /// <summary>
        /// Wyświetla zawartość tabeli Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonShowEvents_Click(object sender, EventArgs e)
        {
            click.Play();
            ShowData("EventsView");
        }

        /// <summary>
        /// Wyświetla zawartość tabeli TasksToDo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonShowTasks_Click(object sender, EventArgs e)
        {
            click.Play();
            ShowData("TasksView");
        }

        /// <summary>
        /// Wyświetal zawartość tabeli Processes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonShowUsersProcesses_Click(object sender, EventArgs e)
        {
            click.Play();
            ShowData("ProcessesView");
        }
        
        /// <summary>
        /// Wyświetla zawartość tabeli Users
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonUsers_Click(object sender, EventArgs e)
        {
            click.Play();
            ShowData("UsersView");
        }

        /// <summary>
        /// Wyświetla zawartość tabeli CurrentWorks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonShowCurrentWork_Click(object sender, EventArgs e)
        {
            click.Play();
            ShowData("CurrentWorksView");
        }

        /// <summary>
        /// Wyświetla zawartość tabeli Works
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonShowWork_Click(object sender, EventArgs e)
        {
            ShowData("WorkView");
        }

        /// <summary>
        /// Otwiera okno umożliwiające dodanie pozycji do tabeli Contacts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemAddContacts_Click(object sender, EventArgs e)
        {
            FormAddContact addForm = new FormAddContact();
            addForm.ShowDialog();
        }

        /// <summary>
        /// Otwiera okno umożliwiające dodanie pozycji do dtabeli LifeGoals
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemAddGoal_Click(object sender, EventArgs e)
        {
            FormAddGoal addForm = new FormAddGoal();
            addForm.ShowDialog();
        }

        /// <summary>
        /// Otwiera okno umożliwiające dodanie pozycji do tabeli TasksToDo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemAddTask_Click(object sender, EventArgs e)
        {
            FormAddTask addForm = new FormAddTask();
            addForm.ShowDialog();
        }

        /// <summary>
        /// Otwiera okno umożliwiające dodanie pozycji do tabeli Meetings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemAddMeeting_Click(object sender, EventArgs e)
        {
            FormAddMeeting addForm = new FormAddMeeting();
            addForm.ShowDialog();
        }

        /// <summary>
        /// Otwiera okno umożliwiające dodanie pozycji do tabeli Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemAddEvent_Click(object sender, EventArgs e)
        {
            FormAddEvent addForm = new FormAddEvent();
            addForm.ShowDialog();
        }

        /// <summary>
        /// Otwiera okno umożliwiające dodanie pozycji do tabeli Works
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemAddWork_Click(object sender, EventArgs e)
        {
            FormAddWork addForm = new FormAddWork();
            addForm.ShowDialog();
        }
    }
}
