using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAgendamento.Models
{
    public class BaseModel
    {
        
            public static int _nextId = 1; // contador global

            public int Id { get; set; }

            public BaseModel()
            {
                Id = _nextId++;
            }
        
        

    }
}
