using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LC.ProjectCompleteWorks.Entitys
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string NickName { get; set; }
        public string Mobile { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public DateTime UpdateTime { get; set; } = DateTime.Now;

        public bool IsEnable { get; set; } = true;

        public bool IsDelete { get; set; } = false;
    }
}
