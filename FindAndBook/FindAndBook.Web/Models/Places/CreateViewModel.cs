using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindAndBook.Web.Models.Places
{
    public class CreateViewModel
    {
        public CreateViewModel(bool isManager)
        {
            this.IsManager = isManager;
        }

        public bool IsManager { get; set; }
    }
}