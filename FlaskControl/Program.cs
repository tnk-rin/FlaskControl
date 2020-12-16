using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Http;

namespace FlaskControl {
    static class Program {
        private static readonly HttpClient client = new HttpClient();
        [STAThread]
        static void Main() {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        async public static void Submit(Color c, String ip) {
            String url = "https://" + ip + "/execute/?r=" + c.R + "&g=" + c.G + "&b=" + c.B + "&bright=100";
            try
            {
                //doesnt work or something idfk c#. maybe it does and i should leave the server running :) nvm it actually doesnt work :)
                var responseString = await client.GetStringAsync(url);
            } catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
            }
            
        }

        async public static void PatternSubmit(List<Color> colors, string ip)
        {
            String url = "http://" + ip + "/pattern_color/?p=";
            int size = colors.Count - 1;
            foreach(Color c in colors)
            {
                url += c.R + "*" + c.G + "*" + c.B;
                if (size > 0)
                    url += "_";
                --size;
            }
            System.Diagnostics.Debug.Print(url);
            try
            {
                var responseString = await client.GetStringAsync(url);
            } catch (Exception e) 
            {
                System.Diagnostics.Debug.Print(e.ToString());
            }
        }
    }
}
