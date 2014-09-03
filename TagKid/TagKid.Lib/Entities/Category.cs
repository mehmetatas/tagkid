﻿using TagKid.Lib.PetaPoco;

namespace TagKid.Lib.Entities
{
    [TableName("categories")]
    [PrimaryKey("id", autoIncrement = true)]
    [ExplicitColumns]
    public class Category
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("user_id")]
        public long UserId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("access_level")]
        public AccessLevel AccessLevel { get; set; }

        [Column("status")]
        public CategoryStatus Status { get; set; }
    }
}
