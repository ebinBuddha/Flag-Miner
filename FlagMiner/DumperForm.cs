using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlagMiner
{
    public partial class DumperForm : Form
    {

        public FlagMiner myForm1;
        public DumperForm(FlagMiner frm1)
        {
            myForm1 = frm1;
            InitializeComponent();
        }
    }
}
