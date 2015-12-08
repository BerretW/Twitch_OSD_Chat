/*
 * Created by SharpDevelop.
 * User: adolf
 * Date: 08.12.2015
 * Time: 16:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OSD_Lib;

namespace OSD_Twitch_SingleFrameChat
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		OSD_Lib.OSD OSD = new OSD_Lib.OSD();
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			OSD.OSD_init(1);
			OSD.text_to_line_advance(100,100,"ahoj" + Environment.NewLine + "vole",0,20,60000,Color.Red);
			//OSD.text_to_line(100,100,"ahoj" + Environment.NewLine + "vole",0,20,Color.Red);
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void Button1Click(object sender, EventArgs e)
		{
			OSD.OSD_close(0);
			OSD.text_to_line_advance(100,100,"Neklikej laskavě" + Environment.NewLine + "namě",0,20,60000,Color.Red);
		}
	}
}
