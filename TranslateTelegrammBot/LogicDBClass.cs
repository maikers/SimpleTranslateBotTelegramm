using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslateTelegrammBot
{
    public class LogicDB
    {
        public static void saveData(string ENG, string RU)
        {
            if(ENG==null || RU==null)
            {
                return;
            }
            if (findData(RU).En!=""||RU.Equals(" ")||ENG.Equals(" "))
            {
                return;
            }
            using (var db = new Lite())
            {
                var dic = new Dictionary
                {
                    En = ENG.ToLower(),
                    Ru = RU.ToLower()
                };

                db.Dictionary.Add(dic);
                db.SaveChanges();

            }
        }

        public static Dictionary findData(string Ru)
        {
            Dictionary dic = new Dictionary();
            
           // try { 
            using (var db = new Lite())
            {
                if (db.Dictionary.ToList().Exists(x => x.Ru.Equals(Ru.ToLower()) && x.En != null))
                dic = db.Dictionary.ToList().Find(x => x.Ru.Equals(Ru.ToLower()) && x.En != null);

                
            }

                return dic;
          //  }
          //  catch (Exception) { return new Dictionary(); }


        }

        public static List<Dictionary> GetAll()
        {
            List<Dictionary> tmpArray;
            using (var db = new Lite())
            {
                tmpArray = db.Dictionary.ToList();
            }
            return tmpArray;
        }
    }
}
