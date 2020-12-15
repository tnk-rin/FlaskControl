using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;

namespace FlaskControl {
    static class Program {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        private static readonly HttpClient client = new HttpClient();
        private static Color colorCache = Color.White;

        [STAThread]
        static void Main() {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        async public static void Submit(Color c, String ip) {
            colorCache = c;
            String url = "http://" + ip + "/execute/?r=" + c.R + "&g=" + c.G + "&b=" + c.B + "&bright=100";
            //var values = new Dictionary<string, string>
            //{
            //    { "r", c.R.ToString() },
            //    { "g", c.G.ToString() },
            //    { "b", c.B.ToString() },
            //    { "bright", c.A.ToString() }
            //};
            try
            {
                //doesnt work or something idfk c#. maybe it does and i should leave the server running :) nvm it actually doesnt work :)
                //var content = new FormUrlEncodedContent(values);
                //var response = await client.PostAsync(url, content);
                //var responseString = await response.Content.ReadAsStringAsync();
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
