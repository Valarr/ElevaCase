using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Net.Http;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using ApiEscola.Controllers.Specs;


namespace ApiEscola.ControllersFixture
{
    [TestFixture]
    public class EscolaControllerFixture : CommonBase
    {
        [Test]
        public async Task GetEscolaId_Must_Return_True()
        {
            //var controler = new ApiEscola.Controllers.EscolaController();
        }
    }
    
}

