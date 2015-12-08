/*
 * Created by SharpDevelop.
 * User: adolf
 * Date: 26.11.2015
 * Time: 20:56
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Text;

namespace OSD_Lib
{
	/// <summary>
	/// Description of OSD.
	/// </summary>
	public class OSD
	{
		private FloatingOSDWindow[] _osd = new FloatingOSDWindow[200];
    	private string[] line_text = new string[200];
    
		public void OSD_init(int num_lines)
		{
			for(int i = 0; i < num_lines+1 ; i++)
			{
				this._osd[i] = new FloatingOSDWindow();
				line_text[i] = " ";
			}
		}
		public void text_to_line(int pos_V , int pos_H ,string new_text, int line, int font_size, Color _textColor = new Color())
		{
			_osd[line].Show(new Point(pos_V, pos_H+(line*(font_size+2))),
			250, _textColor ,new Font("Verdana", font_size,
			FontStyle.Bold | FontStyle.Italic),20000,
			FloatingWindow.AnimateMode.Blend, 370, new_text);
		}
		
		public void text_to_line_advance(int pos_V , int pos_H ,string new_text, int font_size, int time_to_show, Color _textColor = new Color())
		{
			int max_chars = (SystemInformation.VirtualScreen.Width - pos_V)/font_size;
			_osd[0].Show(new Point(pos_V, pos_H),250, _textColor ,new Font("Verdana", font_size,
			FontStyle.Bold | FontStyle.Italic),time_to_show,
			FloatingWindow.AnimateMode.Blend, 370, line_wrap(max_chars, new_text) + max_chars.ToString());
		}
		
		public void OSD_close(int line){
			_osd[line].Close();
		}
		
		
		string line_wrap(int max_chars, string text)
		{
		

    		string[] words = text.Split(' ');

			StringBuilder wraped_text = new StringBuilder();


			string line = "";
			foreach (string word in words)
			{
   				if ((line + word).Length > max_chars)
  					{
     					wraped_text.AppendLine(line);
    					line = "";
  					}

    		line += string.Format("{0} ", word);
			}

			if (line.Length > 0)
   			wraped_text.AppendLine(line);
			return wraped_text.ToString();
			
		}
		
		
		public void chat_window_new_line(int pos_V, int pos_H,string new_text, int num_lines, int font_size, Color _textColor = new Color())
		{
			
			line_text[0] = line_wrap(20,new_text);
			
			for(int i = num_lines; i <= num_lines & i >0;  i--)
			{
				if (i == num_lines) line_text[i] = null;
				
				line_text[i] = line_text[i-1];
				
				text_to_line(pos_V , pos_H,line_text[i],i,font_size,_textColor);

			}
		}
		public void OSD_init_text(int num_lines)
		{
			for(int i = 0; i < num_lines+1 ; i++)
			{
				line_text[i] = " ";
			}
		}
	}
}
