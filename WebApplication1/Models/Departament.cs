using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Models
{
    public class Departament
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Departament()
        {

        }

        public Departament(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public void AddSeller(Seller s)
        {
            Sellers.Add(s);
        }
        public double totalSales(DateTime initial , DateTime Final)
        {
            return Sellers.Sum(seller => seller.totalSales(initial,Final));
        }
    }
}
