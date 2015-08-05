﻿


using System.Data.Entity.ModelConfiguration;
using Eagle.Model;

namespace Carving.Domain.Repository.EF.Map
{
    public class LetterMap : EntityTypeConfiguration<Letter>
    {
        /// <summary>
        /// Initializes a new instance of EntityTypeConfiguration
        /// </summary>
        public LetterMap()
        {
            this.HasKey(x => x.ID);
        }
    }
}