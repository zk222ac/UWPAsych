using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPAsych.Model
{
   public class Room
    {
        public int RoomNo { get; set; }
        public override string ToString()
        {
            return $"{RoomNo}";
        }
    }
}
