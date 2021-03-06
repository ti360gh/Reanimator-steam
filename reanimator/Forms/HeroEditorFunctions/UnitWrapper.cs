using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.IO;
using Hellgate;

namespace Reanimator.Forms.HeroEditorFunctions
{
    //public abstract class CharacterProperty
    //{
    //    protected UnitObject _hero;

    //    // todo: rewrite protected TableDataSet _dataSet;
    //    protected DataTable _statsTable;


    //    //// todo: rewrite public CharacterProperty(Unit heroUnit, TableDataSet dataSet)
    //    //{
    //    //    _hero = heroUnit;
    //    //    _dataSet = dataSet;

    //    //    _statsTable = _dataSet.GetExcelTableFromStringId("STATS");
    //    //}

    //    public UnitObject BaseUnit
    //    {
    //        get { return _hero; }
    //        set { _hero = value; }
    //    }

    //    public int GetBitCount(string value)
    //    {
    //        try
    //        {
    //            DataRow[] row = _statsTable.Select("stat = " + value);
    //            int bitCount = (int)row[0]["valbits"];
    //            return bitCount;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw (ex);
    //        }
    //    }

    //    public int GetBitCount(int code)
    //    {
    //        DataRow[] row = _statsTable.Select("code = " + code);
    //        int bitCount = (int)row[0]["valbits"];
    //        return bitCount;
    //    }
    //}

    public class UnitWrapper
    {
        UnitObject _hero;
        string _savePath;
        // todo: rewrite TableDataSet _dataSet;
        int _unitType;
        bool _isMale;
        CharacterClass _class;
        CharacterGameMode _mode;
        CharacterValues _values;
        CharacterSkills _skills;
        CharacterEquippment _equippment;
        CharacterAppearance _appearance;
        CharacterInventory _inventory;
        //CharacterWaypoint _waypoints;
        EngineerDrone _drone;
        DataTable _itemsTable;

        //// todo: rewrite public UnitWrapper(TableDataSet dataSet, Unit heroUnit)
        //{
        //    _hero = heroUnit;
        //    _dataSet = dataSet;

        //    _itemsTable = _dataSet.GetExcelTableFromStringId("ITEMS");

        //    DataTable players = _dataSet.GetExcelTableFromStringId("PLAYERS");
        //    DataRow[] playerRow = players.Select("code = " + _hero.unitCode);

        //    if (playerRow.Length > 0)
        //    {
        //        int playerType = (int)playerRow[0]["unitType"];

        //        List<int> skillTabs = new List<int>();

        //        for (int counter = 1; counter < 8; counter++)
        //        {
        //            int skillTab = (int)playerRow[0]["SkillTab" + counter];
        //            if (skillTab >= 0)
        //            {
        //                skillTabs.Add(skillTab);
        //            }
        //        }

        //        _unitType = playerType - 3;
        //        //the following code also retrieves the unitType from the tables, but is VERY prone to modifications
        //        //DataTable unitTypes = _dataSet.GetExcelTableFromStringId("UNITTYPES");
        //        //DataRow[] unitTypeRow = unitTypes.Select("code = " + playerType);
        //        //_unitType = (int)unitTypeRow[0]["isA0"];


        //        _class = GetCharacterClass(_hero);
        //        _isMale = _class.ToString().EndsWith("_Male");
        //        _mode = new CharacterGameMode(this.HeroUnit, _dataSet);
        //        _values = new CharacterValues(this.HeroUnit, _dataSet);
        //        _skills = new CharacterSkills(this.HeroUnit, _dataSet, skillTabs.ToArray());
        //        _equippment = new CharacterEquippment(this.HeroUnit, _dataSet);
        //        //_waypoints = new CharacterWaypoint(this.HeroUnit, _dataSet);
        //        _appearance = new CharacterAppearance(this.HeroUnit, _dataSet);
        //        _inventory = new CharacterInventory(this.HeroUnit, dataSet);

        //    }
        //    if (_class == CharacterClass.Engineer_Male || _class == CharacterClass.Engineer_Female)
        //    {
        //        _drone = new EngineerDrone(this.HeroUnit, _dataSet);
        //    }

        //    //int level = _values.Level;
        //    //int skills = _values.SkillPoints;
        //    //int attribute = _values.AttributePoints;
        //}

        public string SavePath
        {
            get { return _savePath; }
            set { _savePath = value; }
        }

        public bool IsMale
        {
            get { return _isMale; }
            set { _isMale = value; }
        }

        public CharacterClass Class
        {
            get { return _class; }
            set { _class = value; }
        }

        public string ClassName
        {
            get
            {
                string charClass = _class.ToString();
                string[] split = charClass.Split(new string[]{"_"}, StringSplitOptions.RemoveEmptyEntries);
                return split[0];
            }
        }

        public CharacterGameMode Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }

        public CharacterValues Values
        {
            get { return _values; }
            set { _values = value; }
        }

        public CharacterSkills Skills
        {
            get { return _skills; }
            set { _skills = value; }
        }

        public CharacterInventory Inventory
        {
            get { return _inventory; }
        }

        public CharacterEquippment Equippment
        {
            get { return _equippment; }
            set { _equippment = value; }
        }

        public CharacterAppearance Appearance
        {
            get { return _appearance; }
            set { _appearance = value; }
        }

        //public CharacterWaypoint Waypoints
        //{
        //    get { return _waypoints; }
        //    set { _waypoints = value; }
        //}

        public EngineerDrone Drone
        {
            get { return _drone; }
            set { _drone = value; }
        }

        public int UnitType
        {
            get
            {
                return _unitType;
            }
        }

        private CharacterClass GetCharacterClass(UnitObject _hero)
        {
            return (CharacterClass)Enum.Parse(typeof(CharacterClass), _hero.UnitCode.ToString());
        }

        public UnitObject HeroUnit
        {
            get { return _hero; }
        }

        public string Name
        {
            get { return _hero.Name; }
            set { _hero.Name = value; }
        }
    }






    public class CharacterWaypoint : CharacterProperty
    {
        UnitObjectStats.Stat _waypoints;
        string[] _waypointNames;

        //// todo: rewrite public CharacterWaypoint(Unit heroUnit, TableDataSet dataSet)
        //    : base(heroUnit, dataSet)
        //{
        //    _waypoints = UnitHelpFunctions.GetComplexValue(BaseUnit, ItemValueNames.waypoint_flags);
        //    //if (_waypoints == null)
        //    {
        //        Unit.StatBlock.Stat _waypoint = new Unit.StatBlock.Stat();
        //        _waypoint.repeatFlag = 1;
        //        _waypoint.skipResource = 1;
        //        _waypoint.bitCount = 32;
        //        _waypoint.id = 20532;
        //        _waypoint.Name = "waypoint_flags";

        //        Unit.StatBlock.Stat.Attribute att1 = new Unit.StatBlock.Stat.Attribute();
        //        att1.BitCount = 8;
        //        att1.exists = 1;
        //        att1.skipTableId = 1;

        //        Unit.StatBlock.Stat.Attribute att2 = new Unit.StatBlock.Stat.Attribute();
        //        att2.BitCount = 16;
        //        att2.exists = 1;
        //        att2.TableId = 17715;

        //        _waypoint.attributes.Add(att1);
        //        _waypoint.attributes.Add(att2);

        //        _waypoint.values = new List<Unit.StatBlock.Stat.Values>();
        //        _waypoint.values.Add(GenerateNormal());
        //        _waypoint.values.Add(GenerateNightmare());

        //        _waypoints.values[0].Stat = 1535;
        //        //_waypoints.values.Add(GenerateNightmare());// = _waypoint;
        //    }
        //    //UnitHelpFunctions.SetComplexValue(_hero, ItemValueNames.waypoint_flags.ToString(), _waypoints);
        //}

        private UnitObjectStats.Stat.StatValue GenerateNormal()
        {
            UnitObjectStats.Stat.StatValue normal = new UnitObjectStats.Stat.StatValue();
            normal.Param2 = 16705;
            normal.Value = 1535;
            return normal;
        }

        private UnitObjectStats.Stat.StatValue GenerateNightmare()
        {
            UnitObjectStats.Stat.StatValue nightmare = new UnitObjectStats.Stat.StatValue();
            nightmare.Param2 = 16961;
            nightmare.Value = 1535;
            return nightmare;
        }

        public int NormalWaypoints
        {
            get { return _waypoints.Values[0].Value; }
            set
            {
                _waypoints.Values[0].Value = value;
            }
        }

        public int NightmareWaypoints
        {
            get { return _waypoints.Values[1].Value; }
            set
            {
                _waypoints.Values[1].Value = value;
            }
        }

        public string[] WaypointLocations
        {
            get { return _waypointNames; }
        }
    }

    //public class CharacterGameMode : CharacterProperty
    //{
    //    //public CharacterGameMode(Unit heroUnit, TableDataSet dataSet) : base(heroUnit, dataSet)
    //    //{
    //    //}

    //    public bool IsElite
    //    {
    //        get
    //        {
    //            return BaseUnit.PlayerFlags1.Contains((int)GameMode.Elite) || BaseUnit.PlayerFlags2.Contains((int)GameMode.Elite);
    //        }
    //        set
    //        {
    //            if (value)
    //            {
    //                if (!BaseUnit.PlayerFlags1.Contains((int)GameMode.Elite))
    //                {
    //                    BaseUnit.PlayerFlags1.Add((int)GameMode.Elite);
    //                }
    //                if (!BaseUnit.PlayerFlags2.Contains((int)GameMode.Elite))
    //                {
    //                    BaseUnit.PlayerFlags2.Add((int)GameMode.Elite);
    //                }
    //            }
    //            else
    //            {
    //                BaseUnit.PlayerFlags1.Remove((int)GameMode.Elite);
    //                BaseUnit.PlayerFlags2.Remove((int)GameMode.Elite);
    //            }
    //        }
    //    }

    //    public bool IsHardcore
    //    {
    //        get
    //        {
    //            return BaseUnit.PlayerFlags1.Contains((int)GameMode.Hardcore) || BaseUnit.PlayerFlags2.Contains((int)GameMode.Hardcore);
    //        }
    //        set
    //        {
    //            if (value)
    //            {
    //                if (!BaseUnit.PlayerFlags1.Contains((int)GameMode.Hardcore))
    //                {
    //                    BaseUnit.PlayerFlags1.Add((int)GameMode.Hardcore);
    //                }
    //                if (!BaseUnit.PlayerFlags2.Contains((int)GameMode.Hardcore))
    //                {
    //                    BaseUnit.PlayerFlags2.Add((int)GameMode.Hardcore);
    //                }
    //            }
    //            else
    //            {
    //                BaseUnit.PlayerFlags1.Remove((int)GameMode.Hardcore);
    //                BaseUnit.PlayerFlags2.Remove((int)GameMode.Hardcore);
    //            }
    //        }
    //    }

    //    public bool IsHardcoreDead
    //    {
    //        get
    //        {
    //            return BaseUnit.PlayerFlags1.Contains((int)GameMode.HardcoreDead) || BaseUnit.PlayerFlags2.Contains((int)GameMode.HardcoreDead);
    //        }
    //        set
    //        {
    //            if (value)
    //            {
    //                if (!BaseUnit.PlayerFlags1.Contains((int)GameMode.HardcoreDead))
    //                {
    //                    BaseUnit.PlayerFlags1.Add((int)GameMode.HardcoreDead);
    //                }
    //                if (!BaseUnit.PlayerFlags2.Contains((int)GameMode.HardcoreDead))
    //                {
    //                    BaseUnit.PlayerFlags2.Add((int)GameMode.HardcoreDead);
    //                }
    //            }
    //            else
    //            {
    //                BaseUnit.PlayerFlags1.Remove((int)GameMode.HardcoreDead);
    //                BaseUnit.PlayerFlags2.Remove((int)GameMode.HardcoreDead);
    //            }
    //        }
    //    }
    //}

    //public class CharacterValues : CharacterProperty
    //{
    //    int _maxPalladium;
    //    int _maxLevel;

    //    //public CharacterValues(Unit heroUnit, TableDataSet dataSet)
    //    //    : base(heroUnit, dataSet)
    //    //{
    //    //    DataTable statsTable = dataSet.GetExcelTableFromStringId("STATS");
    //    //    //could also use "stat" column and "gold" entry
    //    //    DataRow[] goldRow = statsTable.Select("code = " + (int)ItemValueNames.gold);
    //    //    int maxPalladium = (int)goldRow[0]["maxSet"];

    //    //    _maxPalladium = maxPalladium;

    //    //    DataTable playersTable = _dataSet.GetExcelTableFromStringId("PLAYERS");
    //    //    DataRow[] playerRows = playersTable.Select("code = " + BaseUnit.unitCode);
    //    //    int maxLevel = (int)playerRows[0]["maxLevel"];

    //    //    _maxLevel = maxLevel;
    //    //}

    //    public int Level
    //    {
    //        get
    //        {
    //            return UnitHelpFunctions.GetSimpleValue(BaseUnit, (int)ItemValueNames.level) - GetBitCount((int)ItemValueNames.level);
    //            //return UnitHelpFunctions.GetSimpleValue(_hero, (int)ItemValueNames.level) - GetBitCount(12336);
    //        }
    //        set
    //        {
    //            if (value > MaxLevel)
    //            {
    //                value = MaxLevel;
    //            }
    //            if(value < 0)
    //            {
    //                value = 0;
    //            }

    //            UnitHelpFunctions.SetSimpleValue(BaseUnit, (int)ItemValueNames.level, value + GetBitCount((int)ItemValueNames.level));
    //        }
    //    }

    //    public int MaxLevel
    //    {
    //        get
    //        {
    //            return _maxLevel;
    //        }
    //    }

    //    public int Palladium
    //    {
    //        get
    //        {
    //            int palladium = UnitHelpFunctions.GetSimpleValue(BaseUnit, (int)ItemValueNames.gold);

    //            //if the character doesn't have palladium on him, there's also no palladium entry, so let's add it
    //            if(palladium == 0)
    //            {
    //                //maximum Palladium value is 9.999.999 which is 100110001001011001111111 = 24 bit = bitCount
    //                UnitHelpFunctions.AddSimpleValue(BaseUnit, ItemValueNames.gold, 0, GetBitCount((int)ItemValueNames.gold));
    //            }

    //            return palladium;
    //        }
    //        set
    //        {
    //            if (value > MaxPalladium)
    //            {
    //                value = MaxPalladium;
    //            }
    //            if (value < 0)
    //            {
    //                value = 0;
    //            }

    //            UnitHelpFunctions.SetSimpleValue(BaseUnit, (int)ItemValueNames.gold, value);
    //        }
    //    }

    //    public int MaxPalladium
    //    {
    //        get
    //        {
    //            return _maxPalladium;
    //        }
    //    }

    //    public int AttributePoints
    //    {
    //        get
    //        {
    //            int attributePoints = UnitHelpFunctions.GetSimpleValue(BaseUnit, (int)ItemValueNames.stat_points);

    //            //if the character doesn't have palladium on him, there's also no palladium entry, so let's add it
    //            if (attributePoints == 0)
    //            {
    //                //bitCount = 10 taken from other saves => max value = 1023
    //                UnitHelpFunctions.AddSimpleValue(BaseUnit, ItemValueNames.stat_points, 0, GetBitCount((int)ItemValueNames.stat_points));
    //            }

    //            return attributePoints;
    //        }
    //        set
    //        {
    //            if (value > MaxAttributePoints)
    //            {
    //                value = MaxAttributePoints;
    //            }
    //            if (value < 0)
    //            {
    //                value = 0;
    //            }

    //            UnitHelpFunctions.SetSimpleValue(BaseUnit, (int)ItemValueNames.stat_points, value);
    //        }
    //    }

    //    public int MaxAttributePoints
    //    {
    //        get
    //        {
    //            return 1000;
    //        }
    //    }

    //    public int SkillPoints
    //    {
    //        get
    //        {
    //            int skillPoints = UnitHelpFunctions.GetSimpleValue(BaseUnit, (int)ItemValueNames.skill_points);

    //            //if the character doesn't have palladium on him, there's also no palladium entry, so let's add it
    //            if (skillPoints == 0)
    //            {
    //                //bitCount = 12 taken from other saves => max value = 4095
    //                UnitHelpFunctions.AddSimpleValue(BaseUnit, ItemValueNames.skill_points, 0, GetBitCount((int)ItemValueNames.skill_points));
    //            }

    //            return skillPoints;
    //        }
    //        set
    //        {
    //            if (value > MaxSkillPoints)
    //            {
    //                value = MaxSkillPoints;
    //            }
    //            if (value < 0)
    //            {
    //                value = 0;
    //            }

    //            UnitHelpFunctions.SetSimpleValue(BaseUnit, (int)ItemValueNames.skill_points, value);
    //        }
    //    }

    //    public int MaxSkillPoints
    //    {
    //        get
    //        {
    //            return 4000;
    //        }
    //    }

    //    public int Accuracy
    //    {
    //        get
    //        {
    //            return UnitHelpFunctions.GetSimpleValue(BaseUnit, (int)ItemValueNames.accuracy);
    //        }
    //        set
    //        {
    //            UnitHelpFunctions.SetSimpleValue(BaseUnit, (int)ItemValueNames.accuracy, value);
    //        }
    //    }

    //    public int Stamina
    //    {
    //        get
    //        {
    //            return UnitHelpFunctions.GetSimpleValue(BaseUnit, (int)ItemValueNames.stamina);
    //        }
    //        set
    //        {
    //            UnitHelpFunctions.SetSimpleValue(BaseUnit, (int)ItemValueNames.stamina, value);
    //        }
    //    }

    //    public int Strength
    //    {
    //        get
    //        {
    //            return UnitHelpFunctions.GetSimpleValue(BaseUnit, (int)ItemValueNames.strength);
    //        }
    //        set
    //        {
    //            UnitHelpFunctions.SetSimpleValue(BaseUnit, (int)ItemValueNames.strength, value);
    //        }
    //    }

    //    public int Willpower
    //    {
    //        get
    //        {
    //            return UnitHelpFunctions.GetSimpleValue(BaseUnit, (int)ItemValueNames.willpower);
    //        }
    //        set
    //        {
    //            UnitHelpFunctions.SetSimpleValue(BaseUnit, (int)ItemValueNames.willpower, value);
    //        }
    //    }

    //    public int AchievementPointsCur
    //    {
    //        get
    //        {
    //            int achievementPointsCur = UnitHelpFunctions.GetSimpleValue(BaseUnit, (int)ItemValueNames.achievement_points_cur);

    //            if (achievementPointsCur == 0)
    //            {
    //                //bitCount = 12 taken from other saves => max value = 4095
    //                UnitHelpFunctions.AddSimpleValue(BaseUnit, ItemValueNames.skill_points, 0, GetBitCount((int)ItemValueNames.achievement_points_cur));
    //            }

    //            return achievementPointsCur;
    //        }
    //        set
    //        {
    //            UnitHelpFunctions.SetSimpleValue(BaseUnit, (int)ItemValueNames.achievement_points_cur, value);
    //        }
    //    }

    //    public int AchievementPointsTotal
    //    {
    //        get
    //        {
    //            int achievementPointsTotal = UnitHelpFunctions.GetSimpleValue(BaseUnit, (int)ItemValueNames.achievement_points_total);

    //            if (achievementPointsTotal == 0)
    //            {
    //                //bitCount = 12 taken from other saves => max value = 4095
    //                UnitHelpFunctions.AddSimpleValue(BaseUnit, ItemValueNames.skill_points, 0, GetBitCount((int)ItemValueNames.achievement_points_total));
    //            }

    //            return achievementPointsTotal;
    //        }
    //        set
    //        {
    //            UnitHelpFunctions.SetSimpleValue(BaseUnit, (int)ItemValueNames.achievement_points_cur, value);
    //        }
    //    }

    //    public int Experience
    //    {
    //        get
    //        {
    //            return UnitHelpFunctions.GetSimpleValue(BaseUnit, (int)ItemValueNames.experience);
    //        }
    //        set
    //        {
    //            UnitHelpFunctions.SetSimpleValue(BaseUnit, (int)ItemValueNames.experience, value);
    //        }
    //    }

    //    public int Experience_Prev
    //    {
    //        get
    //        {
    //            return UnitHelpFunctions.GetSimpleValue(BaseUnit, (int)ItemValueNames.experience_prev);
    //        }
    //        set
    //        {
    //            UnitHelpFunctions.SetSimpleValue(BaseUnit, (int)ItemValueNames.experience_prev, value);
    //        }
    //    }

    //    public int Experience_Next
    //    {
    //        get
    //        {
    //            return UnitHelpFunctions.GetSimpleValue(BaseUnit, (int)ItemValueNames.experience_next);
    //        }
    //        set
    //        {
    //            UnitHelpFunctions.SetSimpleValue(BaseUnit, (int)ItemValueNames.experience_next, value);
    //        }
    //    }

    //    public int PlayTime
    //    {
    //        get
    //        {
    //            return UnitHelpFunctions.GetSimpleValue(BaseUnit, (int)ItemValueNames.played_time_in_seconds);
    //        }
    //        //set
    //        //{
    //        //    UnitHelpFunctions.SetSimpleValue(_hero, (int)ItemValueNames.played_time_in_seconds, value);
    //        //}
    //    }
    //}

    //public class CharacterSkills : CharacterProperty
    //{
    //    List<SkillTab> _skillTabs;
    //    SkillTab _generalSkills;

    //    public SkillTab GeneralSkills
    //    {
    //        get { return _generalSkills; }
    //    }

    //    public List<SkillTab> SkillTabs
    //    {
    //        get { return _skillTabs; }
    //    }

    //    //public CharacterSkills(Unit heroUnit, TableDataSet dataSet, int[] skillTabs)
    //    //    : base(heroUnit, dataSet)
    //    //{
    //    //    _skillTabs = new List<SkillTab>();

    //    //    //to make things easier, let's add all available character skills to the list
    //    //    List<Unit.StatBlock.Stat.Values> availableSkills = new List<Unit.StatBlock.Stat.Values>();
    //    //    ////get the skills the character already knows
    //    //    Unit.StatBlock.Stat skills = UnitHelpFunctions.GetComplexValue(BaseUnit, ItemValueNames.skill_level);
    //    //    ////add them to the complete skill list
    //    //    availableSkills.AddRange(skills.values);

    //    //    DataTable skillTable = dataSet.GetExcelTableFromStringId("SKILLS");

    //    //    //let's add all the skills the character doesn't know yet
    //    //    foreach (int skillTab in skillTabs)
    //    //    {
    //    //        DataRow[] skillRows = skillTable.Select("skillTab = " + skillTab);

    //    //        SkillTab skillsInSkillTab = CreateSkillsFromRow(availableSkills, skillTable, skillRows);

    //    //        if (skillsInSkillTab.Skills.Count > 0)
    //    //        {
    //    //            _skillTabs.Add(skillsInSkillTab);
    //    //        }
    //    //    }


    //    //    // select the general skill tab
    //    //    DataRow[] generalSkillRows = skillTable.Select("skillTab = " + 0);
    //    //    _generalSkills = CreateSkillsFromRow(availableSkills, skillTable, generalSkillRows);

    //    //    //add all skills back to the savegame
    //    //    availableSkills.Clear();

    //    //    foreach (Skill skill in _generalSkills.Skills)
    //    //    {
    //    //        availableSkills.Add(skill.SkillBlock);
    //    //    }

    //    //    foreach (SkillTab skillTab in _skillTabs)
    //    //    {
    //    //        foreach (Skill skill in skillTab.Skills)
    //    //        {
    //    //            availableSkills.Add(skill.SkillBlock);
    //    //        }
    //    //    }

    //    //    //skills.repeatCount = availableSkills.Count;
    //    //    skills.values = availableSkills;
    //    //}

    //    private SkillTab CreateSkillsFromRow(List<UnitObjectStats.Stat.Values> availableSkills, DataTable skillTable, DataRow[] skillRows)
    //    {
    //        List<UnitObjectStats.Stat.Values> values = new List<UnitObjectStats.Stat.Values>();
    //        SkillTab skillInSkillTab = new SkillTab();

    //        //iterate through all available skills
    //        foreach (DataRow row in skillRows)
    //        {
    //            //get the skill id
    //            int skillId = (int)row["code"];
    //            //if the skill is already present, use that one
    //            UnitObjectStats.Stat.Values tmpSkill = availableSkills.Find(tmp => tmp.Attribute1 == skillId);
    //            if (tmpSkill != null)
    //            {
    //                values.Add(tmpSkill);
    //            }
    //            //if not, add a new one
    //            else
    //            {
    //                UnitObjectStats.Stat.Values skillEntry = new UnitObjectStats.Stat.Values();
    //                skillEntry.Attribute1 = skillId;

    //                values.Add(skillEntry);
    //            }
    //        }
    //        //_hero.Stats.statCount

    //        //and finally... initialize all skills :)
    //        foreach (UnitObjectStats.Stat.Values skillBlock in values)
    //        {
    //            Skill skill = InitializeSkill(skillTable, skillBlock);
    //            skillInSkillTab.Skills.Add(skill);
    //        }
    //        return skillInSkillTab;
    //    }

    //    private Skill InitializeSkill(DataTable table, UnitObjectStats.Stat.Values skillBlock)
    //    {
    //        DataRow[] availableSkillRows = table.Select("code = " + skillBlock.Attribute1);

    //        string name = (string)availableSkillRows[0]["displayName_string"];
    //        string description = (string)availableSkillRows[0]["descriptionString_string"];
    //        string iconName = (string)availableSkillRows[0]["smallIcon"];
    //        int maxLevel = (int)availableSkillRows[0]["maxLevel"];
    //        int row = (int)availableSkillRows[0]["skillPageRow"];
    //        int column = (int)availableSkillRows[0]["skillPageColumn"];

    //        List<int> requiredSkills = new List<int>();
    //        List<int> levelsOfRequiredSkills = new List<int>();

    //        for(int counter = 1; counter < 5; counter++)
    //        {
    //            int requiredSkill = (int)availableSkillRows[0]["requiredSkills" + counter];
    //            if (requiredSkill >= 0)
    //            {
    //                requiredSkills.Add(requiredSkill);
    //            }
    //            int requiredLevel = (int)availableSkillRows[0]["levelsOfrequiredSkills" + counter];
    //            if (requiredLevel >= 0)
    //            {
    //                levelsOfRequiredSkills.Add(requiredLevel);
    //            }
    //        }
    //        return new Skill(name, description, iconName, maxLevel, new Point(column, row), requiredSkills.ToArray(), levelsOfRequiredSkills.ToArray(), skillBlock);
    //    }
    //}

    //public class CharacterInventory : CharacterProperty
    //{
    //    List<CharacterInventoryType> _inventoryList;

    //    //// todo: rewrite public CharacterInventory(Unit heroUnit, TableDataSet dataSet)
    //    //    : base(heroUnit, dataSet)
    //    //{
    //    //    _inventoryList = new List<CharacterInventoryType>();

    //    //    foreach (Unit unit in heroUnit.Items)
    //    //    {
    //    //        CharacterItems item = new CharacterItems(unit, dataSet);

    //    //        // get the matching inventory entry
    //    //        CharacterInventoryType inv = _inventoryList.Find(tmp => tmp.InventoryType == (int)item.InventoryType);

    //    //        if (inv == null)
    //    //        {
    //    //            inv = new CharacterInventoryType((int)item.InventoryType);
    //    //            _inventoryList.Add(inv);
    //    //        }

    //    //        inv.Items.Add(item);
    //    //    }
    //    //}

    //    public CharacterInventoryType GetInventoryById(int inventoryId)
    //    {
    //        CharacterInventoryType inv = _inventoryList.Find(tmp => tmp.InventoryType == inventoryId);

    //        if (inv == null)
    //        {
    //            inv = new CharacterInventoryType(inventoryId);
    //            _inventoryList.Add(inv);
    //        }

    //        return inv;
    //    }

    //    public bool CheckIfInventoryIsPopulated(int inventory)
    //    {
    //        CharacterInventoryType inventorySlot = GetInventoryById(inventory);
    //        if (inventorySlot != null && inventorySlot.Items.Count > 0)
    //        {
    //            return true;
    //        }
    //        return false;
    //    }

    //    public List<CharacterInventoryType> InventoryType
    //    {
    //        get { return _inventoryList; }
    //    }

    //    public void Set(CharacterInventoryType inventory)
    //    {
    //        int index = _inventoryList.FindIndex(tmp => tmp.InventoryType == inventory.InventoryType);
    //        _inventoryList[index] = inventory;
    //    }

    //    public void Apply()
    //    {
    //        UnitObject.Items.Clear();

    //        foreach (CharacterInventoryType type in _inventoryList)
    //        {
    //            foreach(CharacterItems item in type.Items)
    //            {
    //                UnitObject.Items.Add(item.UnitObject);
    //            }
    //        }
    //    }
    //}

    //public class CharacterInventoryType
    //{
    //    int _inventoryType;
    //    List<CharacterItems> _items;

    //    public CharacterInventoryType(int inventoryType)
    //    {
    //        _inventoryType = inventoryType;
    //        _items = new List<CharacterItems>();
    //    }

    //    public List<CharacterItems> Items
    //    {
    //        get { return _items; }
    //        set { _items = value; }
    //    }

    //    public int InventoryType
    //    {
    //        get { return _inventoryType; }
    //        set { _inventoryType = value; }
    //    }

    //    public override string ToString()
    //    {
    //        return _inventoryType.ToString();
    //    }
    //}

    //public class CharacterItems : CharacterProperty
    //{
    //    DataTable _itemTable;
    //    List<CharacterItems> _items;
    //    bool _isQuestItem;
    //    Color _qualityColor;
    //    Bitmap _itemImage;
    //    string _itemImagePath;
    //    bool _isItem;
    //    bool _isConsumable;
    //    int _numberOfAugmentations;
    //    int _numberOfAugmentationsLeft;
    //    int _maxNumberOfAugmentations;
    //    int _maxNumberOfAffixes;
    //    int _numberOfAffixes;
    //    int _numberOfUpgrades;
    //    int _maxNumberOfUpgrades;
    //    int _stackSize;
    //    int _maxStackSize;

    //    //// todo: rewrite public CharacterItems(Unit heroUnit, TableDataSet dataSet)
    //    //    : base(heroUnit, dataSet)
    //    //{
    //    //    _itemTable = _dataSet.GetExcelTableFromStringId("ITEMS");
    //    //    DataRow[] itemRow = _itemTable.Select("code = " + BaseUnit.unitCode);

    //    //    //DataTable colorTable = _dataSet.GetExcelTableFromStringId("ITEMQUALITY");
    //    //    //DataRow[] colorRow = colorTable.Select("code = " + _hero.unitCode);

    //    //    if (itemRow.Length > 0)
    //    //    {
    //    //        _isItem = true;

    //    //        uint bitMask = (uint)itemRow[0]["bitmask02"];
    //    //        _isQuestItem = (bitMask >> 13 & 1) == 1;

    //    //        string maxStackSize = (string)itemRow[0]["stackSize"];
    //    //        string[] splitResult = maxStackSize.Split(new char[] { ',' });
    //    //        if(splitResult.Length == 3)
    //    //        {
    //    //            _maxStackSize = int.Parse(splitResult[1]);
    //    //        }
    //    //        if (_maxStackSize <= 0)
    //    //        {
    //    //            _maxStackSize = 1;
    //    //        }

    //    //        _stackSize = UnitHelpFunctions.GetSimpleValue(heroUnit, ItemValueNames.item_quantity.ToString());
    //    //        if (_stackSize <= 0)
    //    //        {
    //    //            _stackSize = 1;
    //    //        }

    //    //        _itemImagePath = CreateImagePath();

    //    //        _numberOfAugmentations = UnitHelpFunctions.GetSimpleValue(BaseUnit, ItemValueNames.item_augmented_count.ToString());
    //    //        _numberOfUpgrades = UnitHelpFunctions.GetSimpleValue(BaseUnit, ItemValueNames.item_upgraded_count.ToString());

    //    //        DataTable gameGlobals = _dataSet.GetExcelTableFromStringId("GAME_GLOBALS");
    //    //        //DataRow[] globalsRow = gameGlobals.Select("name = " + "max_item_upgrades");
    //    //        DataRow[] globalsRow = gameGlobals.Select("Index = " + 16);
    //    //        _maxNumberOfUpgrades = (int)globalsRow[0]["intValue"];

    //    //        //globalsRow = gameGlobals.Select("name = " + "max_item_augmentations");
    //    //        globalsRow = gameGlobals.Select("Index = " + 17);
    //    //        _maxNumberOfAffixes = (int)globalsRow[0]["intValue"];
    //    //        Unit.StatBlock.Stat affixes = UnitHelpFunctions.GetComplexValue(_hero, ItemValueNames.applied_affix.ToString());
    //    //        if (affixes != null)
    //    //        {
    //    //            _numberOfAffixes = affixes.values.Count;
    //    //        }

    //    //        int numberOfInherentAffixes = _numberOfAffixes - _numberOfAugmentations;
    //    //        _numberOfAugmentationsLeft = _maxNumberOfAffixes - numberOfInherentAffixes;

    //    //        if (_numberOfAugmentationsLeft < 0)
    //    //        {
    //    //            _numberOfAugmentationsLeft = 0;
    //    //        }

    //    //        _maxNumberOfAugmentations = _numberOfAugmentations + _numberOfAugmentationsLeft;
    //    //        if (_maxNumberOfAugmentations > _maxNumberOfAffixes)
    //    //        {
    //    //            _maxNumberOfAugmentations = _maxNumberOfAffixes;
    //    //        }
    //    //    }

    //    //    _items = new List<CharacterItems>();

    //    //    foreach (Unit item in BaseUnit.Items)
    //    //    {
    //    //        CharacterItems wrapper = new CharacterItems(item, dataSet);
    //    //        _items.Add(wrapper);
    //    //    }
    //    //}

    //    private string CreateImagePath()
    //    {
    //        DataRow[] itemsRows = _itemTable.Select(String.Format("code = '{0}'", UnitObject.UnitCode));
    //        if (itemsRows.Length == 0)
    //        {
    //            return null;
    //        }
    //        int unitType = (int)itemsRows[0]["unitType"];
    //        string folder = (string)itemsRows[0]["folder"] + @"\icons";
    //        string name = (string)itemsRows[0]["name"];
    //        //string unitType = (string)itemsRows[0]["unitType_string"];

    //        string itemPath = Path.Combine(folder, name);

    //        return itemPath;
    //    }

    //    public override string ToString()
    //    {
    //        return Name;
    //    }

    //    public string Name
    //    {
    //        get
    //        {
    //            return UnitObject.Name;
    //        }
    //    }

    //    public string GetItemImagePath(bool male)
    //    {
    //        string path = _itemImagePath;

    //        if (_itemImagePath.StartsWith("armor"))
    //        {
    //            if (male)
    //            {
    //                path += "_m";
    //            }
    //            else
    //            {
    //                path += "_f";
    //            }
    //        }

    //        //if (path == null)
    //        //{
    //        //    return path; 
    //        //}
    //        //we don't know if we want to load dds or png yet
    //        return path;// += ".dds";
    //    }

    //    public bool IsItem
    //    {
    //        get { return _isItem; }
    //        set { _isItem = value; }
    //    }

    //    /// <summary>
    //    /// Do NOT use this entry for item adding/removing!
    //    /// </summary>
    //    public List<CharacterItems> WrappedItems
    //    {
    //        get
    //        {
    //            return _items;
    //        }
    //    }

    //    public List<UnitObject> Items
    //    {
    //        get
    //        {
    //            return UnitObject.Items;
    //        }

    //        set
    //        {
    //            UnitObject.Items = value;

    //        }
    //    }

    //    public int StackSize
    //    {
    //        get
    //        {
    //            return _stackSize;
    //        }
    //        set
    //        {
    //            if (value < 1)
    //            {
    //                value = 1;
    //            }
    //            if (value > MaxStackSize)
    //            {
    //                value = MaxStackSize;
    //            }

    //            _stackSize = value;
    //            UnitHelpFunctions.SetSimpleValue(UnitObject, (int)ItemValueNames.item_quantity, value);
    //        }
    //    }

    //    public int MaxStackSize
    //    {
    //        get
    //        {
    //            return _maxStackSize;
    //        }
    //    }

    //    public bool IsConsumable
    //    {
    //        get
    //        {
    //            return _isConsumable;
    //        }
    //    }

    //    /// <summary>
    //    /// Number of uses of the Nanoforge up till now
    //    /// </summary>
    //    public int NumberOfUpgrades
    //    {
    //        get { return _numberOfUpgrades; }
    //    }

    //    /// <summary>
    //    /// Number of uses of the Nanoforge left
    //    /// </summary>
    //    public int MaxNumberOfUpgrades
    //    {
    //        get { return _maxNumberOfUpgrades; }
    //    }

    //    /// <summary>
    //    /// Maximum number of Augmentrix usages given the inherent affixes
    //    /// </summary>
    //    public int MaxNumberOfAugmentations
    //    {
    //        get { return _maxNumberOfAugmentations; }
    //    }

    //    /// <summary>
    //    /// Number of Augmentrix usages up till now
    //    /// </summary>
    //    public int NumberOfAugmentations
    //    {
    //        get
    //        {
    //            return _numberOfAugmentations;
    //        }
    //    }

    //    /// <summary>
    //    /// Number of Augmentrix usages left
    //    /// </summary>
    //    public int NumberOfAugmentationsLeft
    //    {
    //        get { return _numberOfAugmentationsLeft; }
    //    }

    //    /// <summary>
    //    /// Number of already present affixes 
    //    /// </summary>
    //    public int NumberOfAffixes
    //    {
    //        get
    //        {
    //            return _numberOfAffixes;
    //        }
    //    }

    //    /// <summary>
    //    /// Maximum number of affixes
    //    /// </summary>
    //    public int MaxNumberOfAffixes
    //    {
    //        get { return _maxNumberOfAffixes; }
    //        set { _maxNumberOfAffixes = value; }
    //    }

    //    public InventoryTypes InventoryType
    //    {
    //        get
    //        {
    //            return (InventoryTypes)Enum.Parse(typeof(InventoryTypes), UnitObject.InventoryType.ToString());
    //        }
    //        set
    //        {
    //            UnitObject.InventoryType = (int)value;
    //        }
    //    }

    //    public Point InventoryPosition
    //    {
    //        get
    //        {
    //            return new Point(UnitObject.InventoryPositionX, UnitObject.InventoryPositionY);
    //        }
    //        set
    //        {
    //            UnitObject.InventoryPositionX = value.X;
    //            UnitObject.InventoryPositionY = value.Y;
    //        }
    //    }

    //    public Size InventorySize
    //    {
    //        get
    //        {
    //            int width = UnitHelpFunctions.GetSimpleValue(UnitObject, (int)ItemValueNames.inventory_width);
    //            int height = UnitHelpFunctions.GetSimpleValue(UnitObject, (int)ItemValueNames.inventory_height);

    //            if (width < 1)
    //            {
    //                width = 1;
    //            }
    //            if (height < 1)
    //            {
    //                height = 1;
    //            }

    //            return new Size(width, height);
    //        }
    //        set
    //        {
    //            UnitHelpFunctions.SetSimpleValue(UnitObject, (int)ItemValueNames.inventory_width, value.Width);
    //            UnitHelpFunctions.SetSimpleValue(UnitObject, (int)ItemValueNames.inventory_height, value.Height);
    //        }
    //    }

    //    public ItemQuality Quality
    //    {
    //        get
    //        {
    //            return (ItemQuality)UnitHelpFunctions.GetSimpleValue(UnitObject, (int)ItemValueNames.item_quality);
    //        }
    //    }

    //    public Color QualityColor
    //    {
    //        get
    //        {
    //            return _qualityColor;
    //        }
    //    }

    //    //public Image ItemImage
    //    //{
    //    //    get
    //    //    {
    //    //        return _itemImage;
    //    //    }
    //    //}

    //    public bool IsQuestItem
    //    {
    //        get
    //        {
    //            return _isQuestItem;
    //        }
    //    }

    //    public List<UnitObject> GetItemsOfQuality(ItemQuality quality)
    //    {
    //        List<UnitObject> tmp = new List<UnitObject>();

    //        foreach (UnitObject item in UnitObject.Items)
    //        {
    //            if (UnitHelpFunctions.GetSimpleValue(UnitObject, (int)ItemValueNames.item_quality) == (int)quality)
    //            {
    //                tmp.Add(item);
    //            }
    //        }
    //        return tmp;
    //    }

    //    public List<UnitObject> GetItemsOfInventoryType(InventoryTypes type)
    //    {
    //        List<UnitObject> tmp = new List<UnitObject>();

    //        foreach (UnitObject item in UnitObject.Items)
    //        {
    //            if (item.InventoryType == (int)type)
    //            {
    //                tmp.Add(item);
    //            }
    //        }

    //        return tmp;
    //    }

    //    public void AddItem(UnitObject item)
    //    {
    //        UnitObject.Items.Add(item);
    //        // todo: rewrite _items.Add(new CharacterItems(item, _dataSet));
    //    }

    //    public void RemoveItem(UnitObject item)
    //    {
    //        UnitObject.Items.Remove(item);
    //        CharacterItems tmpItem = _items.Find(tmp => tmp.UnitObject == item);
    //        _items.Remove(tmpItem);
    //    }

    //    public void AddItem(CharacterItems item)
    //    {
    //        _items.Add(item);
    //        UnitObject.Items.Add(item.UnitObject);
    //    }

    //    public void RemoveItem(CharacterItems item)
    //    {
    //        UnitObject.Items.Remove(item.UnitObject);
    //        _items.Remove(item);
    //    }

    //    public int PlayTime
    //    {
    //        get
    //        {
    //            return UnitHelpFunctions.GetSimpleValue(UnitObject, (int)ItemValueNames.played_time_in_seconds);
    //        }
    //        //set
    //        //{
    //        //    UnitHelpFunctions.SetSimpleValue(_hero, (int)ItemValueNames.played_time_in_seconds, value);
    //        //}
    //    }
    //}

    //public class CharacterItemList : CharacterProperty
    //{
    //    public CharacterItemList(UnitWrapper heroUnit, TableDataSet dataSet)
    //        : base(heroUnit, dataSet)
    //    {
    //    }

    //    public List<Unit> Items
    //    {
    //        get
    //        {
    //            return _hero.Items;
    //        }
    //    }

    //    public List<Unit> GetItemsOfInventoryType(InventoryTypes type)
    //    {
    //        List<Unit> tmp = new List<Unit>();

    //        foreach (Unit item in _hero.Items)
    //        {
    //            if (item.inventoryType == (int)type)
    //            {
    //                tmp.Add(item);
    //            }
    //        }

    //        return tmp;
    //    }

    //    public void RemoveItem(Unit item)
    //    {
    //        _hero.Items.Remove(item);
    //    }

    //    public void AddItem(Unit item)
    //    {
    //        _hero.Items.Add(item);
    //    }
    //}

    //public class CharacterItem : CharacterProperty
    //{
    //    public CharacterItem(UnitWrapper heroUnit, TableDataSet dataSet)
    //        : base(heroUnit, dataSet)
    //    {
    //    }

    //    public List<Unit> Items
    //    {
    //        get
    //        {
    //            return _hero.Items;
    //        }
    //        set
    //        {
    //            _hero.Items = value;
    //        }
    //    }

    //    public InventoryTypes InventoryType
    //    {
    //        get
    //        {
    //            return (InventoryTypes)Enum.Parse(typeof(InventoryTypes), _hero.unitCode.ToString());
    //        }
    //        set
    //        {
    //            _hero.inventoryType = (int)value;
    //        }
    //    }

    //    public Point InventoryPosition
    //    {
    //        get
    //        {
    //            return new Point(_hero.inventoryPositionX, _hero.inventoryPositionY);
    //        }
    //        set
    //        {
    //            _hero.inventoryPositionX = value.X;
    //            _hero.inventoryPositionY = value.Y;
    //        }
    //    }

    //    public Size InventorySize
    //    {
    //        get
    //        {
    //            int width = UnitHelpFunctions.GetSimpleValue(_hero, (int)ItemValueNames.inventory_width);
    //            int height =  UnitHelpFunctions.GetSimpleValue(_hero, (int)ItemValueNames.inventory_height);

    //            if (width < 1)
    //            {
    //                width = 1;
    //            }
    //            if (height < 1)
    //            {
    //                height = 1;
    //            }

    //            return new Size(width, height);
    //        }
    //        set
    //        {
    //            UnitHelpFunctions.SetSimpleValue(_hero, (int)ItemValueNames.inventory_width, value.Width);
    //            UnitHelpFunctions.SetSimpleValue(_hero, (int)ItemValueNames.inventory_height, value.Height);
    //        }
    //    }
    //}

    public class CharacterAppearance : CharacterProperty
    {
        //// todo: rewrite public CharacterAppearance(Unit heroUnit, TableDataSet dataSet)
        //    : base(heroUnit, dataSet)
        //{
        //}

        public Size CharacterSize
        {
            get
            {
                return new Size(UnitObject.CharacterBulk, UnitObject.CharacterHeight);
            }
            set
            {
                UnitObject.CharacterBulk = value.Width;
                UnitObject.CharacterHeight = value.Height;
            }
        }
    }

    public class CharacterEquippment : CharacterProperty
    {
        //// todo: rewrite public CharacterEquippment(Unit heroUnit, TableDataSet dataSet)
        //    : base(heroUnit, dataSet)
        //{
        //}
    }

    public class EngineerDrone : CharacterProperty
    {
        UnitObject _drone;

        //// todo: rewrite public EngineerDrone(Unit heroUnit, TableDataSet dataSet)
        //    : base(heroUnit, dataSet)
        //{
        //    _drone = BaseUnit.Items.Find(item => item.unitCode == (int)CharacterClass.Drone);
        //}

        public UnitObject Drone
        {
            get
            {
                return _drone;
            }
            set
            {
                UnitObject.Items.Remove(_drone);
                UnitObject.Items.Add(value);
                _drone = value;
            }
        }
    }

    //public class WeaponSlots : CharacterProperty
    //{
    //    //// todo: rewrite public WeaponSlots(Unit heroUnit, TableDataSet dataSet)
    //    //    : base(heroUnit, dataSet)
    //    //{
    //    //}

    //    public UnitObject[] WeaponSlot1
    //    {
    //        get
    //        {
    //            List<UnitObject> _weapons = new List<UnitObject>();

    //            foreach (UnitObject item in UnitObject.Items)
    //            {
    //                if (item.InventoryType == (int)InventoryTypes.CurrentWeaponSet)
    //                {
    //                    _weapons.Add(item);
    //                }
    //            }

    //            return _weapons.ToArray();
    //        }
    //    }

    //    public UnitObject[] WeaponSlot2
    //    {
    //        get
    //        {
    //            List<UnitObject> _weapons = new List<UnitObject>();

    //            foreach (UnitObject item in UnitObject.Items)
    //            {
    //                if (item.InventoryType == (int)InventoryTypes.CurrentWeaponSet)
    //                {
    //                    _weapons.Add(item);
    //                }
    //            }

    //            return _weapons.ToArray();
    //        }
    //    }

    //    public UnitObject[] WeaponSlot3
    //    {
    //        get
    //        {
    //            List<UnitObject> _weapons = new List<UnitObject>();

    //            foreach (UnitObject item in UnitObject.Items)
    //            {
    //                if (item.InventoryType == (int)InventoryTypes.CurrentWeaponSet)
    //                {
    //                    _weapons.Add(item);
    //                }
    //            }

    //            return _weapons.ToArray();
    //        }
    //    }
    //}
}
