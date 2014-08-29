using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todoList.Model
{
    public abstract class Entity
    {
        public abstract int Id { get; set; }
        public abstract string Name { get; set; }
    }
}
