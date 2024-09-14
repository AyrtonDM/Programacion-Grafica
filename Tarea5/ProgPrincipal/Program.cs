using System;
using OpenTK;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using ProgPrincipal;
using Newtonsoft.Json;
using System.Drawing;
using System.Xml.Linq;

class Program
{
    static void Main(String[] args)
    {
        Graficador game = new Graficador();
        game.Run();
    }
}
