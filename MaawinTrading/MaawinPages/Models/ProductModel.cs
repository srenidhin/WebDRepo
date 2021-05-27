using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaawinPages.Models
{
    public class ProductModel
    {
        public String pName { get; set; }
        public String pCode { get; set; }
        public String pDesc { get; set; }
        public String pQuantity { get; set; }
        public String pType { get; set; }
        public String pCategory { get; set; }
        public String pImgLocation { get; set; }
    }
    public enum AgriProducts
    {
        SpicesHerbs,
        VeggiesFruits,
        NutsSeeds
    }
    public enum HealthCareEquipments
    {
        Stretcher,
        Mask,
        Oxygen
    }
    public enum ChemicalsEnggProducts
    {
        HCL,
        KMnO4,
        H2SO4
    }
    public enum Category
    {
        AgriProducts,
        HealthCareEquipments,
        ChemicalsEnggProducts
    }
}