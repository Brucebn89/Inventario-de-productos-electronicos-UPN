using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G11_SistemaInventario.Models
{
    public class G11_Category
    {
        public int G5_CategoryId { get; set; }
        public string G5_Name { get; set; }
        public string G11_Name { get; private set; }

        public G11_Category(int categoryId, string name)
        {
            G5_CategoryId = categoryId;
            G5_Name = name;
        }

        public override string ToString()
        {
            return G11_Name;
        }
    }
}
