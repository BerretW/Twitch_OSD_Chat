/*
 * Created by SharpDevelop.
 * User: adolf
 * Date: 24.11.2015
 * Time: 13:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Drawing;
using OSD_Lib;



namespace OSD_Twitch
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{

        
		public string globalAdresar = new FileInfo(Application.ExecutablePath).Directory.FullName + "\\";
		bool loged_in = false;
        public string irc_Server = "irc.rizon.net";
        public int irc_port = 6667;
        public string irc_desc = "TiRoRiN Twitch OSD Client";
        public string irc_channel = "";
        public string irc_Jmeno = "";
        public string oauth = "";
        public string debug = "N";
        public int Pos_V=0;
        public int Pos_H=0;
        public string alpha = "";
        public bool autoconnect = false;
        public string show_datetime = "";
        public string res_V = SystemInformation.VirtualScreen.Width.ToString();
        public string res_H = SystemInformation.VirtualScreen.Height.ToString();

        
        public int max_radku = 5;
        public int velikost_pisma = 10;
        
        Color barva = new Color();
        
        OSD_Lib.OSD OSD = new OSD_Lib.OSD();
        
        
        private TcpClient irc;
        NetworkStream stream = null;
        StreamReader sr = null;
        StreamWriter sw = null;

        
        
        public void load_file_config()
        {
            if (File.Exists(globalAdresar + @"\config.cfg"))
            {
                using (StreamReader nacteni = new StreamReader(globalAdresar + "config.cfg", Encoding.Default, true))
                {
                    string config = nacteni.ReadToEnd();
                    string[] split1 = new string[] { "\"" };
                    string[] split2 = config.Split(split1, StringSplitOptions.None);
                    irc_Server = split2[1];
                    irc_Jmeno = split2[5];
                    oauth = split2[7];
                    irc_channel = split2[9];
              		int.TryParse(split2[11], out max_radku);
              		int.TryParse(split2[13], out velikost_pisma);
              		int.TryParse(split2[15],out Pos_H);
              		int.TryParse(split2[17],out Pos_V);
              		bool.TryParse(split2[19],out autoconnect);
              		radioButton1.Checked = autoconnect;
              		debug = split2[21];
                    
                }
            //MessageBox.Show("config loaded " + irc_Server + irc_Jmeno +oauth + irc_channel + alpha);
//            float dividend = Int32.Parse(alpha);
//            double Alpha =  dividend / 100;
//            this.Opacity = Alpha;
            //MessageBox.Show(Alpha.ToString());
            //this.Opacity= ;
//            this.Width = Int32.Parse(V_size);
//            this.Height = Int32.Parse(H_size);
           

           
                      
                
            }
            else  MessageBox.Show("Stáhněte si konfigurační soubor znovu.");
        }

        public void write_file_config()
        {
        	
        	string config;
        	StreamReader empty_config = new StreamReader(globalAdresar + "config.blnk", Encoding.UTF8, true);
                
        	config = empty_config.ReadToEnd();
        	config = config.Replace("@@server@@",textBox1.Text);
        	config = config.Replace("@@name@@",textBox3.Text);
        	config = config.Replace("@@oauth@@",textBox4.Text);
        	config = config.Replace("@@channel@@",textBox2.Text);
        	config = config.Replace("@@lines@@",textBox5.Text);
        	config = config.Replace("@@fontsize@@",velikost_pisma.ToString());
        	config = config.Replace("@@posH@@",textBox6.Text);
        	config = config.Replace("@@posV@@",textBox7.Text);
        	config = config.Replace("@@a@@", radioButton1.Checked.ToString());
        	
        	
            System.IO.StreamWriter file = new System.IO.StreamWriter(globalAdresar + "config.cfg");
            file.WriteLine(config);
            file.Close();
        }
        
        
        private void IRC_connect()
        {


            irc = new TcpClient(irc_Server, irc_port);
            stream = irc.GetStream();
            sr = new StreamReader(stream);
            sw = new StreamWriter(stream);
            

        }

        private void irc_client()
        {
        	
        	//rich_write(richTextBox1, "OSD", "Připojuji se k IRC");
            //irc_Jmeno = textBox3.Text;
            //richTextBox1.Text = "pripojuji se";
            sw.WriteLine("PASS " + oauth);                   //autoorizace nicku
            sw.Flush();
            sw.WriteLine("NICK " + irc_Jmeno);                   //autoorizace nicku
            sw.Flush();
            sw.WriteLine("USER " + irc_Jmeno + " 0 * :" + irc_desc); //autorizace uzivatele
            sw.Flush();
            sw.WriteLine("JOIN " + irc_channel);                //pripojeni do kanalu
            sw.Flush();
            string getString = null;
            
            try
            {

                while ((getString = sr.ReadLine()) != null)     //vypisovani vsech hodnot ze streamu do textboxu
                {
					
                    //if (!getString.Contains("PING")) ;
                    if (getString.Contains("PING"))
                    {
                        sw.WriteLine("PONG irc.broke-it.com");
                        sw.Flush();
                        //sw.WriteLine("PRIVMSG " + channel + " :PONG");
                        //sw.Flush();

                    }

                    richTextBox1.Invoke(new Action(() =>
                    {
                        
                        string split_jmeno = null;
                        string split_msg = null;

                        string getstring2 = getString;
                        string getstring3 = getString;


                        //zachytavani
                        
                        
                        try
                        {
                        	if (debug=="A") rich_write(richTextBox1, "OSD", getString);
                            // parsovani normalni zpravy
                            string[] split1 = new string[] { irc_channel.ToLower() + " :" };
							
							string[] split2 = getString.Split(split1, StringSplitOptions.None);
							split_msg = split2[1];

                            split_jmeno = split2[0];


                            if (split_msg.Length > 0) loged_in = true;
                            split1 = new string[] { "!" };
                            split2 = split_jmeno.Split(split1, StringSplitOptions.None);
                            split_jmeno = split2[0];

                            split1 = new string[] { ":" };
                            split2 = split_jmeno.Split(split1, StringSplitOptions.None);
                            split_jmeno = split2[1];
                        }
                        catch
              			{
                    		
                		}



                        
						// Funkce
						if (split_msg==@"\clear") {
							mazani_textu();
							split_jmeno = null;
						}
						
                        if (split_jmeno != null) // text do global chatu
                        {
                            if (!getstring2.Contains(" 353 " + irc_Jmeno + " = " + irc_channel + " :") & !getstring2.Contains(" 366 " + irc_Jmeno)) 
                            {
                            	OSD.chat_window_new_line(Pos_V,Pos_H, split_jmeno + ": " + split_msg,max_radku,velikost_pisma,barva);
                                rich_write(richTextBox1, split_jmeno, split_msg);
                            }
                            
                        }
                        

                    }
                    ));
                }
            }
            catch
            {
                MessageBox.Show("Připojení se nezdařilo. Prosím restartuje aplikaci.");
                ActiveForm.Close();

            }

        }
        
       
        void rich_write(RichTextBox box, string from, string message)
        {
            DateTime datumCas = DateTime.Now;
            
            box.Text = from + ": " + message + Environment.NewLine + box.Text;
        }
        
        void mazani_textu(){
        richTextBox1.Text = null;
		OSD.OSD_init_text(max_radku);
        }
       
        
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			load_file_config();
			OSD.OSD_init(max_radku);
			
			label6.Text = label6.Text + "(0-"+ res_V + ")";
			label7.Text = label7.Text + "(0-" + res_H + ")";
			textBox6.Text = Pos_H.ToString();
			textBox7.Text = Pos_V.ToString();
			textBox1.Text = irc_Server;
			textBox2.Text = irc_channel;
			textBox3.Text = irc_Jmeno;
			textBox4.Text = oauth;
			textBox5.Text = max_radku.ToString();
			
			if (autoconnect) 
			{
				IRC_connect();
				backgroundWorker1.RunWorkerAsync();
			}
			else MessageBox.Show("Máte zvoleno, nepřipojovat automaticky");
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void Button1Click(object sender, EventArgs e)
		{
			if (!loged_in){
			IRC_connect();
			backgroundWorker1.RunWorkerAsync();
			}
		
		}
		void BackgroundWorker1DoWork(object sender, DoWorkEventArgs e)
		{
			irc_client();
		}
		
		
		void Button2Click(object sender, EventArgs e)
		{
			int V;
			int H;
			
			
			if (int.TryParse(textBox6.Text,out V)& int.TryParse(textBox7.Text,out H))
			{
			OSD.chat_window_new_line(V,H, "OSD" + ": " + "Testovací zpráva",max_radku,velikost_pisma,barva);
            rich_write(richTextBox1, "OSD", "Testovací zpráva");
			}
			else MessageBox.Show("Zadejte právnou hodnotu prop pozici chatu");
			
			
		}
		void Button3Click(object sender, EventArgs e)
		{
			mazani_textu();
			
		}
		void Button4Click(object sender, EventArgs e)
		{
			write_file_config();
		}
	}
}
