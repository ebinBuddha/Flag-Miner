using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
namespace FlagMiner
{
	public partial class WatDo
	{

		private void WatDo_KeyDown(object sender, KeyEventArgs e)
		{
			if ((e.KeyCode == Keys.Escape))
				this.Close();
		}
		public WatDo()
		{
			KeyDown += WatDo_KeyDown;
			InitializeComponent();
		}
	}
}
