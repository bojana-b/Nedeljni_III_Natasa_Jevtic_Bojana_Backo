﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nedeljni_III_Natasa_Jevtic_Bojana_Backo
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RecipesDBEntities : DbContext
    {
        public RecipesDBEntities()
            : base("name=RecipesDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblIngredient> tblIngredients { get; set; }
        public virtual DbSet<tblRecipe> tblRecipes { get; set; }
        public virtual DbSet<tblShoppingList> tblShoppingLists { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }
        public virtual DbSet<vwIngredient> vwIngredients { get; set; }
        public virtual DbSet<vwRecipe> vwRecipes { get; set; }
        public virtual DbSet<vwShoppingList> vwShoppingLists { get; set; }
        public virtual DbSet<vwUser> vwUsers { get; set; }
    }
}
