using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Yoda
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public bool IsAlphanumeric(String strIn)
        {
            Regex pattern = new Regex("[^0-9a-zA-Z]");
            return !pattern.IsMatch(strIn);
        }
        
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void buttonGo_Click(object sender, RoutedEventArgs e)
        {
            // go solve the problem

            String strIn = textBox.Text;
            if (strIn.Length == 0)
            {
                textOut.Text = "You didn't enter a string";
                return;
            }

            if (strIn.Length == 1)
            {
                textOut.Text = textBox.Text;
                return;
            }

            // the input is valid and at least two characters
            int i = 0;
            int iw = 0;
            int ip = 0;
            bool fWordFirst = true;
            List<String> words = new List<string>();
            List<String> punct = new List<string>();

            // likely a hack, but makes the while loops below work better
            // less testing for being at the end of the string
            int length = strIn.Length;
            strIn += " ";

            // build a string of the actual correct length
            StringBuilder sb = new StringBuilder(strIn.Length);

            // detect how the string starts
            if (!IsAlphanumeric(strIn[0].ToString()))
            {
                fWordFirst = false;
            }

            //parse
            while (i < length)
            {
                sb.Clear();

                if (IsAlphanumeric(strIn[i].ToString()))
                {
                    // it's a word made of letters and numbers
                    while (IsAlphanumeric(strIn[i].ToString()) && i < length)
                    {
                        sb.Append(strIn[i]);
                        i++;
                    }
                    words.Add(sb.ToString());
                }
                else
                {
                    // it's a word made of punctuation
                    while (!IsAlphanumeric(strIn[i].ToString()) && i < length)
                    {
                        sb.Append(strIn[i]);
                        i++;
                    }
                    punct.Add(sb.ToString());
                }
            }// while parse

            //build
            sb.Clear();

            iw = words.Count - 1;
            if (!fWordFirst)
            {
                sb.Append(punct[ip]);
                ip++;
            }

            while ( (iw >= 0) && (ip < punct.Count))
            {
                sb.Append(words[iw]);
                iw--;

                sb.Append(punct[ip]);
                ip++;
            }

            textOut.Text = sb.ToString();
            sb.Clear();
        }
    }
}
