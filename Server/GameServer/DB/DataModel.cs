using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class HeroDb
    {
        public int Id { get; set; }
        public long AccountId { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class ItemDb
    {
        public long Id { get; set; }
        public int TemplateId { get; set; }
        public int EquipSlot { get; set; }
        public long AccountId { get; set; }
        public int Count { get; set; }
        public int OwnerId { get; set; }
        public int EnchantCount { get; set; }
    }
}
