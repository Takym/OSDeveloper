using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OSDeveloper.IO.ItemManagement;

namespace OSDeveloper.GUIs.Explorer
{
	public partial class FileTreeBox : UserControl
	{
		public FolderMetadata Directory { get; set; }

		public FileTreeBox(FormMain mwnd)
		{
			InitializeComponent();
		}
	}
}
