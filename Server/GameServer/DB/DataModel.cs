using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameServer
{
    public class AccountDb
    {
        [Key]
        public long AccountDbId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        public virtual List<HeroDb> Heroes { get; set; } = new List<HeroDb>();
    }

    public class HeroDb
    {
        [Key]
        public long HeroDbId { get; set; }

        [Required]
        public string NickName { get; set; }

        [Required]
        public EHeroClass ClassType { get; set; }

        [Required]
        public int Level { get; set; } = 1;

        [Required]
        public int Exp { get; set; } = 0;

        [Required]
        public int MapId { get; set; } = 0;

        [Required]
        public int PosX { get; set; } = 0;

        [Required]
        public int PosY { get; set; } = 0;

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        [Required]
        public long AccountDbId { get; set; }

        [ForeignKey("AccountDbId")]
        public virtual AccountDb Account { get; set; }
    }
}