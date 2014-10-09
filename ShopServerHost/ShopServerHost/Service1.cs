using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using ShopServer;


namespace ShopServerHost
{
    public partial class ShopServerHost : ServiceBase
    {
        internal static ServiceHost sh = null;
        public ShopServerHost()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (sh != null)
            {
                sh.Close();
            }
            sh = new ServiceHost(typeof(Shop));
            sh.Open();
        }

        protected override void OnStop()
        {
            if (sh != null)
            {
                sh.Close();
                sh = null;
            }

        }
    }
}
