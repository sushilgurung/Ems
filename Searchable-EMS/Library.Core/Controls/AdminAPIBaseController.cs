﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Library.Core.Controls
{
    [Authorize]
    public class AdminAPIBaseController : UserAPIControl
    {
    }
}
