﻿using System;
using System.Runtime.InteropServices;
using ExcelOutput = Reanimator.ExcelFile.ExcelOutputAttribute;

namespace Reanimator.ExcelDefinitions
{
    // size = 0x00000000000000F8 (not including header)
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class AchievementsRow
    {
        ExcelFile.TableHeader header;

        Int32 undefined;                                                                        // ??       0x0000000000000000      ?? (Never Read?)
        [ExcelOutput(SortId = 1)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public String name;                                                                     // 1        0x0000000000000004      XLS_ReadString
        [ExcelOutput(SortId = 2)]
        public Int32 code;                                                                      // 2        0x0000000000000044      XLS_ReadCode
        [ExcelOutput(IsStringId = true, TableStringId = "Strings_Strings")]
        public Int32 nameString;                                                                // 3        0x0000000000000048      XLS_ReadStringIndex
        [ExcelOutput(IsStringId = true, TableStringId = "Strings_Strings")]
        public Int32 descripFormatString;                                                       // 4        0x000000000000004C      XLS_ReadStringIndex
        [ExcelOutput(IsStringId = true, TableStringId = "Strings_Strings")]
        public Int32 detailsString;                                                             // 5        0x0000000000000050      XLS_ReadStringIndex
        [ExcelOutput(IsStringId = true, TableStringId = "Strings_Strings")]
        public Int32 rewardTypeString;                                                          // 6        0x0000000000000054      XLS_ReadStringIndex
        public Int32 revealCondition;                                                           // 7        0x0000000000000058      XLS_ReadInternalIndex             .text:00000001402F0AAF mov     dword ptr [rsp+58h+var_30], 5        (Default = Always)
        public Int32 revealValue;                                                               // 8        0x000000000000005C      XLS_ReadInt32
        [ExcelOutput(IsTableIndex = true, TableIndex = 0xB4)]
        public Int32 revealParentAchievement;                                                   // 9        0x0000000000000060      XLS_ReadIndex,0xB4
        [ExcelOutput(IsTableIndex = true, TableIndex = 0x17)]
        public Int32 playerClass0;                                                              // 11       0x0000000000000064      XLS_ReadUNITTYPEIndexArray,0x0A
        [ExcelOutput(IsTableIndex = true, TableIndex = 0x17)]
        public Int32 playerClass1;                                                              // 11       0x0000000000000068
        [ExcelOutput(IsTableIndex = true, TableIndex = 0x17)]
        Int32 playerClass2;                                                                     // 11       0x000000000000006C
        [ExcelOutput(IsTableIndex = true, TableIndex = 0x17)]
        Int32 playerClass3;                                                                     // 11       0x0000000000000070
        [ExcelOutput(IsTableIndex = true, TableIndex = 0x17)]
        Int32 playerClass4;                                                                     // 11       0x0000000000000074
        [ExcelOutput(IsTableIndex = true, TableIndex = 0x17)]
        Int32 playerClass5;                                                                     // 11       0x0000000000000078
        [ExcelOutput(IsTableIndex = true, TableIndex = 0x17)]
        Int32 playerClass6;                                                                     // 11       0x000000000000007C
        [ExcelOutput(IsTableIndex = true, TableIndex = 0x17)]
        Int32 playerClass7;                                                                     // 11       0x0000000000000080
        [ExcelOutput(IsTableIndex = true, TableIndex = 0x17)]
        Int32 playerClass8;                                                                     // 11       0x0000000000000084
        [ExcelOutput(IsTableIndex = true, TableIndex = 0x17)]
        Int32 playerClass9;                                                                     // 11       0x0000000000000088
        public Int32 type;                                                                      // 12       0x000000000000008C      XLS_ReadInternalIndex             .text:00000001402F0B74 mov     dword ptr [rsp+58h+var_30], 17h      (Default = Kill)
        [ExcelOutput(IsTableIndex = true, TableIndex = 0xB4)]
        public Int32 notActiveTillParentComplete;                                               // 10       0x0000000000000090      XLS_ReadIndex,0xB4
        public Int32 completeNumber;                                                            // 13       0x0000000000000094      XLS_ReadInt32
        public Int32 param1;                                                                    // 14       0x0000000000000098      XLS_ReadInt32
        [ExcelOutput(IsTableIndex = true, TableIndex = 0x17)]
        public Int32 unitType0;                                                                 // 15       0x000000000000009C      XLS_ReadUNITTYPEIndexArray,0x0A
        [ExcelOutput(IsTableIndex = true, TableIndex = 0x17)]
        public Int32 unitType1;                                                                 // 15       0x00000000000000A0
        [ExcelOutput(IsTableIndex = true, TableIndex = 0x17)]
        Int32 unitType2;                                                                        // 15       0x00000000000000A4
        [ExcelOutput(IsTableIndex = true, TableIndex = 0x17)]
        Int32 unitType3;                                                                        // 15       0x00000000000000A8
        [ExcelOutput(IsTableIndex = true, TableIndex = 0x17)]
        Int32 unitType4;                                                                        // 15       0x00000000000000AC
        [ExcelOutput(IsTableIndex = true, TableIndex = 0x17)]
        Int32 unitType5;                                                                        // 15       0x00000000000000B0
        [ExcelOutput(IsTableIndex = true, TableIndex = 0x17)]
        Int32 unitType6;                                                                        // 15       0x00000000000000B4
        [ExcelOutput(IsTableIndex = true, TableIndex = 0x17)]
        Int32 unitType7;                                                                        // 15       0x00000000000000B8
        [ExcelOutput(IsTableIndex = true, TableIndex = 0x17)]
        Int32 unitType8;                                                                        // 15       0x00000000000000BC
        [ExcelOutput(IsTableIndex = true, TableIndex = 0x17)]
        Int32 unitType9;                                                                        // 15       0x00000000000000C0
        [ExcelOutput(IsTableIndex = true, TableStringId = "QUEST")]
        public Int32 questTaskComplete;                                                         // 23       0x00000000000000C4      XLS_ReadIndex,0xA5
        public Int32 randomQuests;                                                              // 24       0x00000000000000C8      XLS_ReadInt32
        [ExcelOutput(IsTableIndex = true, TableStringId = "MONSTERS")]
        public Int32 monster;                                                                   // 16       0x00000000000000CC      XLS_ReadIndex,0x73
        [ExcelOutput(IsTableIndex = true, TableStringId = "OBJECTS")]
        public Int32 Object;                                                                    // 17       0x00000000000000D0      XLS_ReadIndex,0x77
        [ExcelOutput(IsTableIndex = true, TableStringId = "ITEMS")]
        public Int32 item;                                                                      // 18       0x00000000000000D4      XLS_ReadIndex,0x63
        [ExcelOutput(IsTableIndex = true, TableStringId = "ITEM_QUALITY")]
        public Int32 quality;                                                                   // 19       0x00000000000000D8      XLS_ReadIndex,0x43
        [ExcelOutput(IsTableIndex = true, TableStringId = "SKILLS")]
        public Int32 skill;                                                                     // 20       0x00000000000000DC      XLS_ReadIndex,0x29
        [ExcelOutput(IsTableIndex = true, TableStringId = "LEVEL")]
        public Int32 level;                                                                     // 21       0x00000000000000E0      XLS_ReadIndex,0x69
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATS")]
        public Int32 stat;                                                                      // 22       0x00000000000000E4      XLS_ReadIndex,0x1B
        public Int32 rewardAchievementPoints;                                                   // 25       0x00000000000000E8      XLS_ReadInt32
        [ExcelOutput(IsTableIndex = true, TableIndex = 0x65)]
        Int32 rewardTreasureClass;//idx                                                         // 26       0x00000000000000EC      XLS_ReadIndex,0x65
        public Int32 rewardXP;                                                                  // 27       0x00000000000000F0      XLS_ReadInt32
        [ExcelOutput(IsTableIndex = true, TableIndex = 0x29)]
        Int32 rewardSkill;//idx                                                                 // 28       0x00000000000000F4      XLS_ReadIndex,0x29
        [ExcelOutput(IsIntOffset = true)]
        Int32 rewardScript;                                                                     // 29       0x00000000000000F8      XLS_ReadIntPtr
    }
}