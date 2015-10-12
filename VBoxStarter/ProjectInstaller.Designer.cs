namespace VBoxStarter
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.VBoxStarterServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.VBoxStarterServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // VBoxStarterServiceProcessInstaller
            // 
            this.VBoxStarterServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.NetworkService;
            this.VBoxStarterServiceProcessInstaller.Password = null;
            this.VBoxStarterServiceProcessInstaller.Username = null;
            // 
            // VBoxStarterServiceInstaller
            // 
            this.VBoxStarterServiceInstaller.DelayedAutoStart = true;
            this.VBoxStarterServiceInstaller.ServiceName = "VBoxStarterService";
            this.VBoxStarterServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.VBoxStarterServiceProcessInstaller,
            this.VBoxStarterServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller VBoxStarterServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller VBoxStarterServiceInstaller;
    }
}