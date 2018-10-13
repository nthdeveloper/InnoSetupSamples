using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;

namespace ExtensibleApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Form1(IEnumerable<IFeature> featureList) : this()
        {
            BindingList<IFeature> _installedFeatures = new BindingList<IFeature>();
            foreach (var feature in featureList)
                _installedFeatures.Add(feature);

            lbxInstalledFeatures.DisplayMember = "Name";
            lbxInstalledFeatures.DataSource = _installedFeatures;
        }
    }
}
