using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyeCT4RailzMVC.Models.Exceptions
{
    public class DataException : Exception
    {
        public DataException(string message)
        {
            ///MessageBox.Show(message);

        }
    }
}