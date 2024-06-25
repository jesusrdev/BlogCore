﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<Articulo> ListaArticulos { get; set; }


        // Para paginacion del inicio
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

    }
}
