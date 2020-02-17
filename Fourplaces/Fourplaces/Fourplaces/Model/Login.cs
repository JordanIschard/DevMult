using System;
using System.Collections.Generic;
using System.Text;

namespace Fourplaces.Model
{
    class Login
    {
        private static Login instance;

        public Dictionary<string, string> login = new Dictionary<string, string>();

        public static Login GetInstance()
        {
            if(instance == null)
            {
                instance = new Login();
            }

            return instance;
        }

        private Login()
        {

        }

        public void Add(string key,string value)
        {
            if (!login.ContainsKey(key))
            {
                login.Add(key, value);
            }
        }

        public string Get(string key)
        {
            // Petit craquage mental
            string NICKBIENTAMERE = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAH";

            if (login.ContainsKey(key))
            {
                return login[key];
            }
            return NICKBIENTAMERE;
        }

        internal void Clear()
        {
            login.Clear();
        }
    }
}
