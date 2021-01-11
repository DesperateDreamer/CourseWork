using System;
using BLL.Services;
using BLL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.IMenu
{
    interface ISubmenu
    {
        void Add();
        void Delete();
        void GetAll();
        void Sort();
        void ViewElement();
        void Update();
    }
}
