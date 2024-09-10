using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class Hero : Creature
    {
        public ClientSession Session { get; set; }
        public VisionCubeComponent Vision { get; set; }

        // 남에게 보낼 때 사용하는 정보
        public HeroInfo HeroInfo { get; private set; } = new HeroInfo();

        // 자기자신에게 보낼 때 사용하는 정보
        public MyHeroInfo MyHeroInfo { get; private set; } = new MyHeroInfo();

        public string Name
        {
            get { return HeroInfo.Name; }
            set { HeroInfo.Name = value; }
        }

        public Hero()
        {
            MyHeroInfo.HeroInfo = HeroInfo;
            HeroInfo.CreatureInfo = CreatureInfo;


        }
    }
}
