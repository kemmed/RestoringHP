using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoringHP
{
    public class Food
    {
        public string name { get; set; }
        public int HPRestoration { get; set; }
        public int hungerRestoration { get; set; }

        public Food(string name, int HPRestoration, int hungerRestoration)
        {
            this.name = name;
            this.HPRestoration = HPRestoration;
            this.hungerRestoration = hungerRestoration;
        }
    }
}
