namespace MarekMachlinskiLab3Zad1
{
    partial class FormMain
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.dataGridViewTable = new System.Windows.Forms.DataGridView();
            this.toolStripMenuOptions = new System.Windows.Forms.ToolStrip();
            this.toolStripSplitButtonAdd = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItemAddContacts = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAddGoal = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAddTask = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAddMeeting = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAddEvent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAddWork = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonShowGoals = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonShowContacts = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonShowMeetings = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonShowEvents = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonShowTasks = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonUsers = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonShowUsersProcesses = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonShowCurrentWork = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonShowWork = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTable)).BeginInit();
            this.toolStripMenuOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewTable
            // 
            this.dataGridViewTable.AllowUserToAddRows = false;
            this.dataGridViewTable.AllowUserToDeleteRows = false;
            this.dataGridViewTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTable.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridViewTable.BackgroundColor = System.Drawing.Color.AntiqueWhite;
            this.dataGridViewTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewTable.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dataGridViewTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTable.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridViewTable.GridColor = System.Drawing.Color.AntiqueWhite;
            this.dataGridViewTable.Location = new System.Drawing.Point(60, 0);
            this.dataGridViewTable.Name = "dataGridViewTable";
            this.dataGridViewTable.ReadOnly = true;
            this.dataGridViewTable.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.AntiqueWhite;
            this.dataGridViewTable.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dataGridViewTable.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Brown;
            this.dataGridViewTable.RowTemplate.DefaultCellStyle.NullValue = " ";
            this.dataGridViewTable.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
            this.dataGridViewTable.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewTable.Size = new System.Drawing.Size(742, 471);
            this.dataGridViewTable.TabIndex = 0;
            // 
            // toolStripMenuOptions
            // 
            this.toolStripMenuOptions.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.toolStripMenuOptions.CanOverflow = false;
            this.toolStripMenuOptions.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStripMenuOptions.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMenuOptions.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.toolStripMenuOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButtonAdd,
            this.toolStripButtonShowGoals,
            this.toolStripButtonShowContacts,
            this.toolStripButtonShowMeetings,
            this.toolStripButtonShowEvents,
            this.toolStripButtonShowTasks,
            this.toolStripButtonUsers,
            this.toolStripButtonShowUsersProcesses,
            this.toolStripButtonShowCurrentWork,
            this.toolStripButtonShowWork});
            this.toolStripMenuOptions.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
            this.toolStripMenuOptions.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenuOptions.Name = "toolStripMenuOptions";
            this.toolStripMenuOptions.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripMenuOptions.Size = new System.Drawing.Size(57, 471);
            this.toolStripMenuOptions.Stretch = true;
            this.toolStripMenuOptions.TabIndex = 1;
            // 
            // toolStripSplitButtonAdd
            // 
            this.toolStripSplitButtonAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButtonAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAddContacts,
            this.toolStripMenuItemAddGoal,
            this.toolStripMenuItemAddTask,
            this.toolStripMenuItemAddMeeting,
            this.toolStripMenuItemAddEvent,
            this.toolStripMenuItemAddWork});
            this.toolStripSplitButtonAdd.Image = global::MarekMachlinskiLab3Zad1.Properties.Resources.Add;
            this.toolStripSplitButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButtonAdd.Name = "toolStripSplitButtonAdd";
            this.toolStripSplitButtonAdd.Size = new System.Drawing.Size(56, 44);
            this.toolStripSplitButtonAdd.Text = "Add...";
            // 
            // toolStripMenuItemAddContacts
            // 
            this.toolStripMenuItemAddContacts.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItemAddContacts.Name = "toolStripMenuItemAddContacts";
            this.toolStripMenuItemAddContacts.Size = new System.Drawing.Size(180, 26);
            this.toolStripMenuItemAddContacts.Text = "Contacts";
            this.toolStripMenuItemAddContacts.Click += new System.EventHandler(this.toolStripMenuItemAddContacts_Click);
            // 
            // toolStripMenuItemAddGoal
            // 
            this.toolStripMenuItemAddGoal.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.toolStripMenuItemAddGoal.Name = "toolStripMenuItemAddGoal";
            this.toolStripMenuItemAddGoal.Size = new System.Drawing.Size(180, 26);
            this.toolStripMenuItemAddGoal.Text = "Life Goal";
            this.toolStripMenuItemAddGoal.Click += new System.EventHandler(this.toolStripMenuItemAddGoal_Click);
            // 
            // toolStripMenuItemAddTask
            // 
            this.toolStripMenuItemAddTask.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.toolStripMenuItemAddTask.Name = "toolStripMenuItemAddTask";
            this.toolStripMenuItemAddTask.Size = new System.Drawing.Size(180, 26);
            this.toolStripMenuItemAddTask.Text = "Task To Do";
            this.toolStripMenuItemAddTask.Click += new System.EventHandler(this.toolStripMenuItemAddTask_Click);
            // 
            // toolStripMenuItemAddMeeting
            // 
            this.toolStripMenuItemAddMeeting.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.toolStripMenuItemAddMeeting.Name = "toolStripMenuItemAddMeeting";
            this.toolStripMenuItemAddMeeting.Size = new System.Drawing.Size(180, 26);
            this.toolStripMenuItemAddMeeting.Text = "Meeting";
            this.toolStripMenuItemAddMeeting.Click += new System.EventHandler(this.toolStripMenuItemAddMeeting_Click);
            // 
            // toolStripMenuItemAddEvent
            // 
            this.toolStripMenuItemAddEvent.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.toolStripMenuItemAddEvent.Name = "toolStripMenuItemAddEvent";
            this.toolStripMenuItemAddEvent.Size = new System.Drawing.Size(180, 26);
            this.toolStripMenuItemAddEvent.Text = "Event";
            this.toolStripMenuItemAddEvent.Click += new System.EventHandler(this.toolStripMenuItemAddEvent_Click);
            // 
            // toolStripMenuItemAddWork
            // 
            this.toolStripMenuItemAddWork.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.toolStripMenuItemAddWork.Name = "toolStripMenuItemAddWork";
            this.toolStripMenuItemAddWork.Size = new System.Drawing.Size(180, 26);
            this.toolStripMenuItemAddWork.Text = "Work";
            this.toolStripMenuItemAddWork.Click += new System.EventHandler(this.toolStripMenuItemAddWork_Click);
            // 
            // toolStripButtonShowGoals
            // 
            this.toolStripButtonShowGoals.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripButtonShowGoals.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonShowGoals.Image = global::MarekMachlinskiLab3Zad1.Properties.Resources.Goal;
            this.toolStripButtonShowGoals.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShowGoals.Name = "toolStripButtonShowGoals";
            this.toolStripButtonShowGoals.Size = new System.Drawing.Size(44, 44);
            this.toolStripButtonShowGoals.Text = "Show Life Goals";
            this.toolStripButtonShowGoals.Click += new System.EventHandler(this.ToolStripButtonShowGoals_Click);
            // 
            // toolStripButtonShowContacts
            // 
            this.toolStripButtonShowContacts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripButtonShowContacts.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonShowContacts.Image = global::MarekMachlinskiLab3Zad1.Properties.Resources.Contacts;
            this.toolStripButtonShowContacts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShowContacts.Name = "toolStripButtonShowContacts";
            this.toolStripButtonShowContacts.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
            this.toolStripButtonShowContacts.Size = new System.Drawing.Size(44, 44);
            this.toolStripButtonShowContacts.Text = "Show Contacts";
            this.toolStripButtonShowContacts.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.toolStripButtonShowContacts.Click += new System.EventHandler(this.toolStripButtonShowContacts_Click);
            // 
            // toolStripButtonShowMeetings
            // 
            this.toolStripButtonShowMeetings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonShowMeetings.Image = global::MarekMachlinskiLab3Zad1.Properties.Resources.Meeting;
            this.toolStripButtonShowMeetings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShowMeetings.Name = "toolStripButtonShowMeetings";
            this.toolStripButtonShowMeetings.Size = new System.Drawing.Size(44, 44);
            this.toolStripButtonShowMeetings.Text = "Show Planned Meetings";
            this.toolStripButtonShowMeetings.Click += new System.EventHandler(this.toolStripButtonShowMeetings_Click);
            // 
            // toolStripButtonShowEvents
            // 
            this.toolStripButtonShowEvents.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripButtonShowEvents.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonShowEvents.Image = global::MarekMachlinskiLab3Zad1.Properties.Resources.Event;
            this.toolStripButtonShowEvents.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShowEvents.Name = "toolStripButtonShowEvents";
            this.toolStripButtonShowEvents.Size = new System.Drawing.Size(44, 44);
            this.toolStripButtonShowEvents.Text = "Show Events";
            this.toolStripButtonShowEvents.Click += new System.EventHandler(this.toolStripButtonShowEvents_Click);
            // 
            // toolStripButtonShowTasks
            // 
            this.toolStripButtonShowTasks.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripButtonShowTasks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonShowTasks.Image = global::MarekMachlinskiLab3Zad1.Properties.Resources.Task;
            this.toolStripButtonShowTasks.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShowTasks.Name = "toolStripButtonShowTasks";
            this.toolStripButtonShowTasks.Size = new System.Drawing.Size(44, 44);
            this.toolStripButtonShowTasks.Text = "Show Tasks To Do";
            this.toolStripButtonShowTasks.Click += new System.EventHandler(this.toolStripButtonShowTasks_Click);
            // 
            // toolStripButtonUsers
            // 
            this.toolStripButtonUsers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonUsers.Image = global::MarekMachlinskiLab3Zad1.Properties.Resources.User;
            this.toolStripButtonUsers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUsers.Name = "toolStripButtonUsers";
            this.toolStripButtonUsers.Size = new System.Drawing.Size(44, 44);
            this.toolStripButtonUsers.Text = "Show Computer Users";
            this.toolStripButtonUsers.Click += new System.EventHandler(this.toolStripButtonUsers_Click);
            // 
            // toolStripButtonShowUsersProcesses
            // 
            this.toolStripButtonShowUsersProcesses.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonShowUsersProcesses.Image = global::MarekMachlinskiLab3Zad1.Properties.Resources.UserProcesses;
            this.toolStripButtonShowUsersProcesses.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShowUsersProcesses.Name = "toolStripButtonShowUsersProcesses";
            this.toolStripButtonShowUsersProcesses.Size = new System.Drawing.Size(44, 44);
            this.toolStripButtonShowUsersProcesses.Text = "Show Processes Started by Current Users";
            this.toolStripButtonShowUsersProcesses.Click += new System.EventHandler(this.toolStripButtonShowUsersProcesses_Click);
            // 
            // toolStripButtonShowCurrentWork
            // 
            this.toolStripButtonShowCurrentWork.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripButtonShowCurrentWork.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonShowCurrentWork.Image = global::MarekMachlinskiLab3Zad1.Properties.Resources.Process;
            this.toolStripButtonShowCurrentWork.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShowCurrentWork.Name = "toolStripButtonShowCurrentWork";
            this.toolStripButtonShowCurrentWork.Size = new System.Drawing.Size(44, 44);
            this.toolStripButtonShowCurrentWork.Text = "Show Current Work";
            this.toolStripButtonShowCurrentWork.Click += new System.EventHandler(this.toolStripButtonShowCurrentWork_Click);
            // 
            // toolStripButtonShowWork
            // 
            this.toolStripButtonShowWork.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonShowWork.Image = global::MarekMachlinskiLab3Zad1.Properties.Resources.Work;
            this.toolStripButtonShowWork.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShowWork.Name = "toolStripButtonShowWork";
            this.toolStripButtonShowWork.Size = new System.Drawing.Size(44, 44);
            this.toolStripButtonShowWork.Text = "Show Work";
            this.toolStripButtonShowWork.Click += new System.EventHandler(this.toolStripButtonShowWork_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AntiqueWhite;
            this.ClientSize = new System.Drawing.Size(800, 471);
            this.Controls.Add(this.toolStripMenuOptions);
            this.Controls.Add(this.dataGridViewTable);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.Text = "Organiser";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTable)).EndInit();
            this.toolStripMenuOptions.ResumeLayout(false);
            this.toolStripMenuOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewTable;
        private System.Windows.Forms.ToolStrip toolStripMenuOptions;
        private System.Windows.Forms.ToolStripButton toolStripButtonShowContacts;
        private System.Windows.Forms.ToolStripButton toolStripButtonShowGoals;
        private System.Windows.Forms.ToolStripButton toolStripButtonShowTasks;
        private System.Windows.Forms.ToolStripButton toolStripButtonShowEvents;
        private System.Windows.Forms.ToolStripButton toolStripButtonShowMeetings;
        private System.Windows.Forms.ToolStripButton toolStripButtonShowCurrentWork;
        private System.Windows.Forms.ToolStripButton toolStripButtonUsers;
        private System.Windows.Forms.ToolStripButton toolStripButtonShowUsersProcesses;
        private System.Windows.Forms.ToolStripButton toolStripButtonShowWork;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonAdd;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAddContacts;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAddGoal;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAddTask;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAddMeeting;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAddEvent;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAddWork;
    }
}

