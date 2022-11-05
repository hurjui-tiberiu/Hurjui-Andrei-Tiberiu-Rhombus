

using System.Collections.Generic;
using System.Linq;

namespace AdministratorFisierNS
{
    //clasa utilitara pentru a citi coordonatele din fisier
    public class Admin
    {
        private readonly string numeFisier;

        public Admin()
        {
            //nemele fisierului citit din App.config
            numeFisier = System.Configuration.ConfigurationManager.AppSettings["NumeFisier"];
        }

        //Obtinerea listei de coordonate
        public List<List<float>> GetCoordonate()
        {
            //O lista de float-uri reprezinta coordonatele unui vertex
            //Coordonatele sunt cate 3 pe un rand, delimitate prin ","
           
            List<List<float>> list = new List<List<float>>();
            foreach (string line in System.IO.File.ReadLines(numeFisier))
            {
                list.Add(line.Split(',')?.Select(float.Parse)?.ToList());
            }

            for (int i = 0; i < list.Count; i++)
            {
                list[i][0] *= (float)5;
                list[i][1] *= (float)5;
                list[i][2] *= (float)5;
            }

            return list;
        }
    }
}
