using Supinfy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Supinfy.DAL
{
    internal static class DataContext
    {
        private static Entities _instance = null;
        internal static Entities Instance
        {
            private set
            {
                _instance = value;
            }
            get
            {
                if (_instance == null)
                {
                    _instance = new Entities();
                }
                return _instance;
            }
        }
    }
}