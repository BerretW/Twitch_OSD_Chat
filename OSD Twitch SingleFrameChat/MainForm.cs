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
using System.Text;
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
			OSD.text_to_line_advance(100,100,"ahoj" + Environment.NewLine + "vole",20,60000,Color.Red);
			//OSD.text_to_line(100,100,"ahoj" + Environment.NewLine + "vole",0,20,Color.Red);
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		string zalamovani_radku(int pocet_pismen, string text)
		{
		

    		string[] words = text.Split(' ');

			StringBuilder newSentence = new StringBuilder();


			string line = "";
			foreach (string word in words)
			{
   				if ((line + word).Length > pocet_pismen)
  					{
     					newSentence.AppendLine(line);
    					line = "";
  					}

    		line += string.Format("{0} ", word);
			}

			if (line.Length > 0)
   			newSentence.AppendLine(line);
			return newSentence.ToString();
			
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			OSD.OSD_close(0);
			string zkouska = "How can I format a C# string to wrap to fit a particular column width?";
			OSD.text_to_line_advance(1800,100,zkouska ,20,60000,Color.Red);
			
		}
		
		
		
		
	}
}
