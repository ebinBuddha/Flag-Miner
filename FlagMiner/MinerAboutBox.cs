using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlagMiner
{
	partial class MinerAboutBox : Form
	{
        public MinerAboutBox()
        {
            Load += AboutBox1_Load;
            InitializeComponent();
        }

		public void AboutBox1_Load(System.Object sender, System.EventArgs e)
		{
			// Set the title of the form.
			string ApplicationTitle = null;
			if (!string.IsNullOrEmpty(this.AssemblyTitle)) {
				ApplicationTitle = this.AssemblyTitle;
			} else {
				ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().GetName().Name);
			}
			this.Text = string.Format("About {0}", ApplicationTitle);
			// Initialize all of the text displayed on the About Box.
			// TODO: Customize the application's assembly information in the "Application" pane of the project 
			//    properties dialog (under the "Project" menu).
			this.LabelProductName.Text = this.AssemblyProduct;
            this.LabelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            this.LabelCopyright.Text = AssemblyCopyright;
            this.TextBoxDescription.Text = AssemblyDescription;
		}

		private void OKButton_Click(System.Object sender, System.EventArgs e)
		{
			this.Close();
		}

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion
    }
}
