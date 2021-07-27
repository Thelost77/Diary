using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Models
{
    public class MBoxParams
    {
        public string Message { get; set; }
        public string Title { get; set; }
        public Action Accept { get; set; }
        public Action Rejection { get; set; }
    }
}
