using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TranslateTelegrammBot
{
    public class LogicTranslator
    {
        /*public static string Translate(string words)
        {
            //string tempStr = words.Replace();
        }*/ 
        public static async Task<string> Translate(string words)
        { 
           
           // return str;
            //foreach (var i in TranslatedArray)
            //{
            //     if (LogicDB.findData(i).En == null)
            //    {
            //      // await PostRequest2(i); 
            //    } 
            //}  
             
             
            string tmpString= "";
          //  foreach (var i in  )
            {
                //if (LogicDB.findData(i).En != null)
                //{
                  //  tmpString+=i +".";
              //  }
            }
            return tmpString;
        }
        public static async Task<string> PostRequest(List< string > words/*string rawWord*/)
        {
            /*if(!words.Any())
            {
                return "";
            }*/
            string rawWord="";
            foreach(var i in words )
            {
                rawWord += i.ToLower() + ". "; 
            }
            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("SrcTxt",rawWord),
                new KeyValuePair<string, string>("TranFrom","Rus"),
                new KeyValuePair<string, string>("TranTo","Eng"),
                new KeyValuePair<string, string>("Subject","**")

            };
            HttpContent q = new FormUrlEncodedContent(queries);
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.PostAsync("https://pereklad.online.ua/_ajax/translate/", q))
                {
                    using (HttpContent content = response.Content)
                    {
                        string mycontent = await content.ReadAsStringAsync();
                        if (mycontent == null) { return "Перевод не найден,введите русский язык"; }
                        HttpContentHeaders headers = content.Headers;
                         List <string> translateRawString = mycontent.Split('.').ToList();
                        foreach(var i in translateRawString)
                        {
                            foreach (var j in words)
                            {
                                LogicDB.saveData(i, j);
                            }
                        } 
                        return mycontent;

                    }
                }


            }

        }
        public static async Task<string> PostRequest2(string rawWord)
        {
            if(rawWord == null)
            {
                return "Ничего не введено";
            }
            string[] tmpStr = Regex.Split(rawWord, "(?<=[.;!?])");
            string str = "";
            List<string> tmpStr2 = new List<string>();
            try { 
            foreach (var i in tmpStr)
            {
                if (LogicDB.findData(i).En =="")
                {
                    str+=i+" ";
                    tmpStr2.Add(i);
                }
            }
            }catch(Exception ex) {Form1.logger.Error(ex.StackTrace); }
            if (tmpStr.Length==0) { return rawWord; }
           
            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("SrcTxt",str),
                new KeyValuePair<string, string>("TranFrom","Rus"),
                new KeyValuePair<string, string>("TranTo","Eng"),
                new KeyValuePair<string, string>("Subject","**")

            };
            HttpContent q = new FormUrlEncodedContent(queries);
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.PostAsync("https://pereklad.online.ua/_ajax/translate/", q))
                {
                    using (HttpContent content = response.Content)
                    {
                        string mycontent = await content.ReadAsStringAsync();
                       
                        if (mycontent == null) { return "Перевод не найден,введите русский язык"; }
                        HttpContentHeaders headers = content.Headers;
                        string[] tmpStr3 = Regex.Split(mycontent, "(?<=[.;!?])");
                        for (int i = 0; i < tmpStr2.Count(); i++)
                        {
                            LogicDB.saveData(tmpStr3[i], tmpStr2.ToArray()[i]);
                        }
                        mycontent = "";
                        foreach (var i in tmpStr)
                        {
                            if (LogicDB.findData(i).En != null)
                            {
                                mycontent += LogicDB.findData(i).En + " ";
                            }
                            else
                                mycontent += " *перевод не найден";
                        }
                        return mycontent;

                    }
                }


            }

        }

    }
}
