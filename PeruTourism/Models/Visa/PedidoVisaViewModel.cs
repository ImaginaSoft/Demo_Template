﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeruTourism.Models.Visa
{
    public class PedidoVisaViewModel
    {

        public List<PedidoVisa> lstPedidoVisa { get; set; }

        public string urlPago { get; set; }

        public string codCliente { get; set; }
    }
}