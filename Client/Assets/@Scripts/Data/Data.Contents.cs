using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class CreatureData
    {
        public int TemplateId;
        public string Name;

        public virtual bool Validate()
        {
            return true;
        }
    }

    #region HeroData

    [Serializable]
    public class HeroData : CreatureData
    {
        public override bool Validate()
        {
            bool validate = base.Validate();

            return validate;
        }
    }

    [Serializable]
    public class HeroDataLoader : ILoader<int, HeroData>
    {
        public List<HeroData> Heroes = new List<HeroData>();

        public Dictionary<int, HeroData> MakeDict()
        {
            Dictionary<int, HeroData> dict = new Dictionary<int, HeroData>();
            foreach (HeroData heroData in Heroes)
                dict.Add(heroData.TemplateId, heroData);

            return dict;
        }

        public bool Validate()
        {
            bool validate = true;

            foreach (var hero in Heroes)
            {
                if (hero.Validate() == false)
                    validate = false;
            }

            return validate;
        }
    }

    #endregion
}
