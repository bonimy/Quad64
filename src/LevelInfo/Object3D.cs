﻿using Quad64.src.JSON;
using Quad64.src.LevelInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace Quad64
{

    class Object3D
    {
        public enum FLAGS {
            POSITION_X = 0x1,
            POSITION_Y = 0x2,
            POSITION_Z = 0x4,
            ROTATION_X = 0x8,
            ROTATION_Y = 0x10,
            ROTATION_Z = 0x20,
            ACT1 = 0x40,
            ACT2 = 0x80,
            ACT3 = 0x100,
            ACT4 = 0x200,
            ACT5 = 0x400,
            ACT6 = 0x800,
            ALLACTS = 0x1000,
            BPARAM_1 = 0x2000,
            BPARAM_2 = 0x4000,
            BPARAM_3 = 0x8000,
            BPARAM_4 = 0x10000,
            ALLFLAGS = 0x1FFFF
        }

        public enum FROM_LS_CMD {
            CMD_24, CMD_39, CMD_2E_8, CMD_2E_10, CMD_2E_12
        }
        
        private const ushort NUM_OF_CATERGORIES = 7;

        bool isBehaviorReadOnly = false;
        bool isModelIDReadOnly = false;
        bool isTempHidden = false;

        [Browsable(false)]
        public bool canEditModelID { get { return !isModelIDReadOnly; } }
        [Browsable(false)]
        public bool canEditBehavior { get { return !isBehaviorReadOnly; } }

        [Browsable(false)]
        public FROM_LS_CMD createdFromLevelScriptCommand { get; set; }

        [CustomSortedCategory("Info", 1, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [Description("Name of the object combo")]
        [DisplayName("Combo Name")]
        [ReadOnly(true)]
        public string Title { get; set; }

        [CustomSortedCategory("Info", 1, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [Description("Location inside the ROM file")]
        [DisplayName("Address")]
        [ReadOnly(true)]
        public string Address { get; set; }
        
        private byte modelID = 0;
        [CustomSortedCategory("Model", 2, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [Description("Model identifer used by the object")]
        [DisplayName("Model ID")]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        public byte ModelID { get { return modelID; } set { modelID = value; } }

        [CustomSortedCategory("Model", 2, NUM_OF_CATERGORIES)]
        [Browsable(false)]
        [Description("Model identifer used by the object")]
        [DisplayName("Model ID")]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [ReadOnly(true)]
        public byte ModelID_ReadOnly { get { return modelID; } }

        private short _xPos, _yPos, _zPos;
        [CustomSortedCategory("Position", 3, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("X")]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        public short xPos { get { return _xPos; } set { _xPos = value; } }
        [CustomSortedCategory("Position", 3, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("Y")]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        public short yPos { get { return _yPos; } set { _yPos = value; } }
        [CustomSortedCategory("Position", 3, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("Z")]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        public short zPos { get { return _zPos; } set { _zPos = value; } }
        
        private short _xRot, _yRot, _zRot;
        [CustomSortedCategory("Rotation", 4, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("RX")]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        public short xRot { get { return _xRot; } set { _xRot = value; } }
        [CustomSortedCategory("Rotation", 4, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("RY")]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        public short yRot { get { return _yRot; } set { _yRot = value; } }
        [CustomSortedCategory("Rotation", 4, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("RZ")]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        public short zRot { get { return _zRot; } set { _zRot = value; } }

        [CustomSortedCategory("Behavior", 5, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("Behavior")]
        //[ReadOnly(true)]
        public string Behavior { get; set; }

        [CustomSortedCategory("Behavior", 5, NUM_OF_CATERGORIES)]
        [Browsable(false)]
        [DisplayName("Behavior")]
        [ReadOnly(true)]
        public string Behavior_ReadOnly { get; set; }
        
        [CustomSortedCategory("Behavior", 5, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("Beh. Name")]
        [ReadOnly(true)]
        public string Behavior_Name { get; set; }

        // default names
        private const string BP1DNAME = "B.Param 1";
        private const string BP2DNAME = "B.Param 2";
        private const string BP3DNAME = "B.Param 3";
        private const string BP4DNAME = "B.Param 4";

        [CustomSortedCategory("Behavior", 5, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName(BP1DNAME)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [Description("")]
        public byte BehaviorParameter1 { get; set; }

        [CustomSortedCategory("Behavior", 5, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName(BP2DNAME)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [Description("")]
        public byte BehaviorParameter2 { get; set; }

        [CustomSortedCategory("Behavior", 5, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName(BP3DNAME)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [Description("")]
        public byte BehaviorParameter3 { get; set; }

        [CustomSortedCategory("Behavior", 5, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName(BP4DNAME)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [Description("")]
        public byte BehaviorParameter4 { get; set; }

        [CustomSortedCategory("Acts", 6, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("All Acts")]
        public bool AllActs { get; set; }
        [CustomSortedCategory("Acts", 6, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("Act 1")]
        public bool Act1 { get; set; }
        [CustomSortedCategory("Acts", 6, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("Act 2")]
        public bool Act2 { get; set; }
        [CustomSortedCategory("Acts", 6, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("Act 3")]
        public bool Act3 { get; set; }
        [CustomSortedCategory("Acts", 6, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("Act 4")]
        public bool Act4 { get; set; }
        [CustomSortedCategory("Acts", 6, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("Act 5")]
        public bool Act5 { get; set; }
        [CustomSortedCategory("Acts", 6, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("Act 6")]
        public bool Act6 { get; set; }

        private ulong Flags = 0;

        private bool isReadOnly = false;
        [CustomSortedCategory("Misc", NUM_OF_CATERGORIES, NUM_OF_CATERGORIES)]
        [DisplayName("Read-Only")]
        [Browsable(false)]
        public bool IsReadOnly { get { return isReadOnly; } }

        /**************************************************************************************/

        [Browsable(false)]
        public Level level { get; set; }
        private ObjectComboEntry objectComboEntry;
        private ushort presetID;

        public int getROMAddress()
        {
            return int.Parse(Address.Substring(2), NumberStyles.HexNumber);
        }

        public uint getROMUnsignedAddress()
        {
            return uint.Parse(Address.Substring(2), NumberStyles.HexNumber);
        }

        public void setPresetID(ushort ID)
        {
            presetID = ID;
        }

        public byte getActMask()
        {
            byte actMask = 0;
            if (Act1) actMask |= 0x1;
            if (Act2) actMask |= 0x2;
            if (Act3) actMask |= 0x4;
            if (Act4) actMask |= 0x8;
            if (Act5) actMask |= 0x10;
            if (Act6) actMask |= 0x20;
            return actMask;
        }

        public void setBehaviorFromAddress(uint address)
        {
            Behavior = "0x"+address.ToString("X8");
            Behavior_ReadOnly = Behavior;
            Behavior_Name = Globals.getBehaviorNameEntryFromSegAddress(address).Name;
        }

        public uint getBehaviorAddress()
        {
            uint value = 0;
            bool succeded = false;
            if(Behavior.ToUpper().StartsWith("0X"))
                succeded = uint.TryParse(Behavior.Substring(2), NumberStyles.HexNumber, new CultureInfo("en-US"), out value);
            else if (Behavior.ToUpper().StartsWith("$"))
                succeded = uint.TryParse(Behavior.Substring(1), NumberStyles.HexNumber, new CultureInfo("en-US"), out value);
            else
                succeded = uint.TryParse(Behavior, out value);

            if (succeded)
                return value;
            else
                return 0;
        }

        public void updateROMData()
        {
            if (Address.Equals("N/A")) return;
            ROM rom = ROM.Instance;
            uint romAddr = getROMUnsignedAddress();
            if (Globals.list_selected == 0) // Regular Object
            {
                rom.writeByte(romAddr + 2, getActMask());
                rom.writeByte(romAddr + 3, ModelID);
                rom.writeHalfword(romAddr + 4, xPos);
                rom.writeHalfword(romAddr + 6, yPos);
                rom.writeHalfword(romAddr + 8, zPos);
                rom.writeHalfword(romAddr + 10, xRot);
                rom.writeHalfword(romAddr + 12, yRot);
                rom.writeHalfword(romAddr + 14, zRot);
                rom.writeByte(romAddr + 16, BehaviorParameter1);
                rom.writeByte(romAddr + 17, BehaviorParameter2);
                rom.writeByte(romAddr + 18, BehaviorParameter3);
                rom.writeByte(romAddr + 19, BehaviorParameter4);
                rom.writeWord(romAddr + 20, getBehaviorAddress());
            }
            else if (Globals.list_selected == 1) // Macro Object
            {
                //Console.WriteLine("Preset ID = 0x" + presetID.ToString("X"));
                ushort first = (ushort)((((ushort)(yRot / 2.8125f) & 0x7F) << 9) | (presetID & 0x1FF));
                rom.writeHalfword(romAddr, first);
                rom.writeHalfword(romAddr + 2, xPos);
                rom.writeHalfword(romAddr + 4, yPos);
                rom.writeHalfword(romAddr + 6, zPos);
                rom.writeByte(romAddr + 8, BehaviorParameter1);
                rom.writeByte(romAddr + 9, BehaviorParameter2);
            }
            else if (Globals.list_selected == 2) // Special Object
            {
                //Console.WriteLine("Special Preset ID = 0x" + presetID.ToString("X"));
                rom.writeHalfword(romAddr, presetID);
                rom.writeHalfword(romAddr + 2, xPos);
                rom.writeHalfword(romAddr + 4, yPos);
                rom.writeHalfword(romAddr + 6, zPos);
                if (!isHidden(FLAGS.ROTATION_Y))
                {
                    rom.writeHalfword(romAddr + 8, (short)(yRot / 1.40625f));
                    if (!isHidden(FLAGS.BPARAM_1))
                    {
                        rom.writeByte(romAddr + 10, BehaviorParameter1);
                        rom.writeByte(romAddr + 11, BehaviorParameter2);
                    }
                }
            }
        }

        public void MakeBehaviorReadOnly(bool isReadOnly)
        {
            isBehaviorReadOnly = isReadOnly;
        }

        public void MakeModelIDReadOnly(bool isReadOnly)
        {
            isModelIDReadOnly = isReadOnly;
        }

        public void MakeReadOnly()
        {
            TypeDescriptor.AddAttributes(this, new Attribute[] { new ReadOnlyAttribute(true) });
            isReadOnly = true;
        }

        private void HideShowProperty(string property, bool show)
        {
            PropertyDescriptor descriptor =
                TypeDescriptor.GetProperties(this.GetType())[property];
            BrowsableAttribute attrib =
              (BrowsableAttribute)descriptor.Attributes[typeof(BrowsableAttribute)];
            FieldInfo isBrow =
              attrib.GetType().GetField("browsable", BindingFlags.NonPublic | BindingFlags.Instance);

            if (isBrow != null)
                isBrow.SetValue(attrib, show);
        }
        
        private bool isHidden(FLAGS flag)
        {
            return (Flags & (ulong)flag) == (ulong)flag;
        }

        private void updateProperty(string property, FLAGS flag)
        {
            if (isHidden(flag))
                HideShowProperty(property, false);
            else
                HideShowProperty(property, true);
        }

        private void updateReadOnlyProperty(string property, bool isReadOnly)
        {
            if (isReadOnly)
            {
                HideShowProperty(property, false);
                HideShowProperty(property+"_ReadOnly", true);
            }
            else
            {
                HideShowProperty(property, true);
                HideShowProperty(property+"_ReadOnly", false);
            }
        }

        private void ChangePropertyName(string property, string name)
        {
            PropertyDescriptor descriptor =
                TypeDescriptor.GetProperties(this.GetType())[property];
            DisplayNameAttribute attrib =
              (DisplayNameAttribute)descriptor.Attributes[typeof(DisplayNameAttribute)];
            FieldInfo isBrow =
              attrib.GetType().GetField("_displayName", BindingFlags.NonPublic | BindingFlags.Instance);
            
            if (isBrow != null)
                isBrow.SetValue(attrib, name);
        }

        private string GetPropertyDisplayName(string property)
        {
            PropertyDescriptor descriptor =
                TypeDescriptor.GetProperties(this.GetType())[property];
            DisplayNameAttribute attrib =
              (DisplayNameAttribute)descriptor.Attributes[typeof(DisplayNameAttribute)];

            return (string)attrib.GetType().GetField("_displayName", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(attrib);
        }

        private void ChangePropertyDescription(string property, string description)
        {
            PropertyDescriptor descriptor =
                TypeDescriptor.GetProperties(this.GetType())[property];
            DescriptionAttribute attrib =
              (DescriptionAttribute)descriptor.Attributes[typeof(DescriptionAttribute)];
            FieldInfo isBrow =
              attrib.GetType().GetField("description", BindingFlags.NonPublic | BindingFlags.Instance);
            if (isBrow != null)
                isBrow.SetValue(attrib, description);
        }

        private void UpdatePropertyName(string property, string oce_name, string otherName)
        {
            if (oce_name != null && !oce_name.Equals(""))
                ChangePropertyName(property, oce_name);
            else
                ChangePropertyName(property, otherName);
        }

        private void UpdatePropertyDescription(string property, string oce_desc)
        {
            if (oce_desc != null && !oce_desc.Equals(""))
                ChangePropertyDescription(property, oce_desc);
            else
                ChangePropertyDescription(property, "");
        }

        private void UpdateObjectComboNames()
        {
            if (objectComboEntry != null)
            {
                UpdatePropertyName("BehaviorParameter1", objectComboEntry.BP1_NAME, BP1DNAME);
                UpdatePropertyName("BehaviorParameter2", objectComboEntry.BP2_NAME, BP2DNAME);
                UpdatePropertyName("BehaviorParameter3", objectComboEntry.BP3_NAME, BP3DNAME);
                UpdatePropertyName("BehaviorParameter4", objectComboEntry.BP4_NAME, BP4DNAME);
                UpdatePropertyDescription("BehaviorParameter1", objectComboEntry.BP1_DESCRIPTION);
                UpdatePropertyDescription("BehaviorParameter2", objectComboEntry.BP2_DESCRIPTION);
                UpdatePropertyDescription("BehaviorParameter3", objectComboEntry.BP3_DESCRIPTION);
                UpdatePropertyDescription("BehaviorParameter4", objectComboEntry.BP4_DESCRIPTION);
            }
            else
            {
                ChangePropertyName("BehaviorParameter1", BP1DNAME);
                ChangePropertyName("BehaviorParameter2", BP2DNAME);
                ChangePropertyName("BehaviorParameter3", BP3DNAME);
                ChangePropertyName("BehaviorParameter4", BP4DNAME);
                ChangePropertyDescription("BehaviorParameter1", "");
                ChangePropertyDescription("BehaviorParameter2", "");
                ChangePropertyDescription("BehaviorParameter3", "");
                ChangePropertyDescription("BehaviorParameter4", "");
            }
        }

        public void UpdateProperties()
        {
            updateProperty("xPos", FLAGS.POSITION_X);
            updateProperty("yPos", FLAGS.POSITION_Y);
            updateProperty("zPos", FLAGS.POSITION_Z);
            updateProperty("xRot", FLAGS.ROTATION_X);
            updateProperty("yRot", FLAGS.ROTATION_Y);
            updateProperty("zRot", FLAGS.ROTATION_Z);
            updateProperty("Act1", FLAGS.ACT1);
            updateProperty("Act2", FLAGS.ACT2);
            updateProperty("Act3", FLAGS.ACT3);
            updateProperty("Act4", FLAGS.ACT4);
            updateProperty("Act5", FLAGS.ACT5);
            updateProperty("Act6", FLAGS.ACT6);
            updateProperty("AllActs", FLAGS.ALLACTS);
            updateProperty("BehaviorParameter1", FLAGS.BPARAM_1);
            updateProperty("BehaviorParameter2", FLAGS.BPARAM_2);
            updateProperty("BehaviorParameter3", FLAGS.BPARAM_3);
            updateProperty("BehaviorParameter4", FLAGS.BPARAM_4);
            updateReadOnlyProperty("Behavior", isBehaviorReadOnly);
            updateReadOnlyProperty("ModelID", isModelIDReadOnly);
            UpdateObjectComboNames();
        }

        FLAGS tempHideFlags;
        
        bool isBehaviorReadOnly_tempTrigger = false, isModelIDReadOnly_tempTrigger = false;
        public void HideFieldsTemporarly(FLAGS showFlags)
        {
            tempHideFlags = (~showFlags & ~(FLAGS)Flags) & FLAGS.ALLFLAGS;
            //Console.WriteLine(Convert.ToString((int)Flags, 2).PadLeft(32, '0'));
            //Console.WriteLine(Convert.ToString((int)tempHideFlags, 2).PadLeft(32, '0'));

            isTempHidden = true;
            if(!isBehaviorReadOnly)
            {
                isBehaviorReadOnly_tempTrigger = true;
                isBehaviorReadOnly = true;
            }
            if (!isModelIDReadOnly)
            {
                isModelIDReadOnly_tempTrigger = true;
                isModelIDReadOnly = true;
            }
            HideProperty(tempHideFlags);
            UpdateProperties();
        }

        public void RevealTemporaryHiddenFields()
        {
            if (isTempHidden)
            {
                if (isBehaviorReadOnly_tempTrigger)
                {
                    isBehaviorReadOnly_tempTrigger = false;
                    isBehaviorReadOnly = false;
                }
                if (isModelIDReadOnly_tempTrigger)
                {
                    isModelIDReadOnly_tempTrigger = false;
                    isModelIDReadOnly = false;
                }
                UnhideProperty(tempHideFlags);
                UpdateProperties();
                isTempHidden = false;
                tempHideFlags = 0;
            }
        }

        public FLAGS getFlagFromDisplayName(string displayName)
        {
            if (displayName == GetPropertyDisplayName("xPos")) return FLAGS.POSITION_X;
            if (displayName == GetPropertyDisplayName("yPos")) return FLAGS.POSITION_Y;
            if (displayName == GetPropertyDisplayName("zPos")) return FLAGS.POSITION_Z;
            if (displayName == GetPropertyDisplayName("xRot")) return FLAGS.ROTATION_X;
            if (displayName == GetPropertyDisplayName("yRot")) return FLAGS.ROTATION_Y;
            if (displayName == GetPropertyDisplayName("zRot")) return FLAGS.ROTATION_Z;
            if (displayName == GetPropertyDisplayName("Act1")) return FLAGS.ACT1;
            if (displayName == GetPropertyDisplayName("Act2")) return FLAGS.ACT2;
            if (displayName == GetPropertyDisplayName("Act3")) return FLAGS.ACT3;
            if (displayName == GetPropertyDisplayName("Act4")) return FLAGS.ACT4;
            if (displayName == GetPropertyDisplayName("Act5")) return FLAGS.ACT5;
            if (displayName == GetPropertyDisplayName("Act6")) return FLAGS.ACT6;
            if (displayName == GetPropertyDisplayName("AllActs")) return FLAGS.ALLACTS;
            if (displayName == GetPropertyDisplayName("BehaviorParameter1")) return FLAGS.BPARAM_1;
            if (displayName == GetPropertyDisplayName("BehaviorParameter2")) return FLAGS.BPARAM_2;
            if (displayName == GetPropertyDisplayName("BehaviorParameter3")) return FLAGS.BPARAM_3;
            if (displayName == GetPropertyDisplayName("BehaviorParameter4")) return FLAGS.BPARAM_4;
            return 0;
        }

        public void SetBehaviorParametersToZero()
        {
            BehaviorParameter1 = 0;
            BehaviorParameter2 = 0;
            BehaviorParameter3 = 0;
            BehaviorParameter4 = 0;
        }

        public void DontShowActs()
        {
            Flags |= (ulong)(
                FLAGS.ACT1 | FLAGS.ACT2 | FLAGS.ACT3 | 
                FLAGS.ACT4 | FLAGS.ACT5 | FLAGS.ACT6 |
                FLAGS.ALLACTS);
        }

        public void ShowHideActs(bool hide)
        {
            if (hide)
                Flags |= (ulong)(FLAGS.ACT1 | FLAGS.ACT2 | 
                    FLAGS.ACT3 | FLAGS.ACT4 | FLAGS.ACT5 | FLAGS.ACT6);
            else
                Flags &= ~(ulong)(FLAGS.ACT1 | FLAGS.ACT2 |
                    FLAGS.ACT3 | FLAGS.ACT4 | FLAGS.ACT5 | FLAGS.ACT6);
            UpdateProperties();
        }

        public void HideProperty(FLAGS flag)
        {
            Flags |= (ulong)flag;
        }

        public void UnhideProperty(FLAGS flag)
        {
            Flags &= ~(ulong)flag;
        }

        public void renameObjectCombo(string newName)
        {
            string oldComboName = Title;
            Title = newName;
            bool undefinedToDefined = oldComboName.StartsWith("Undefined Combo (") 
                && !newName.StartsWith("Undefined Combo (");

            if (!undefinedToDefined) // simple re-define
            {
                if (objectComboEntry != null)
                    objectComboEntry.Name = newName;
            }
            else
            {
                uint modelAddress = 0;
                if (level.ModelIDs.ContainsKey(ModelID))
                    modelAddress = level.ModelIDs[ModelID].GeoDataSegAddress;
                ObjectComboEntry newOCE = new ObjectComboEntry(newName, ModelID, modelAddress, getBehaviorAddress());
                objectComboEntry = newOCE;
                Globals.objectComboEntries.Add(newOCE);
                level.LevelObjectCombos.Add(newOCE);
            }

            ModelComboFile.writeObjectCombosFile(Globals.getDefaultObjectComboPath());
        }

        public string getObjectComboName()
        {
            uint behaviorAddr = getBehaviorAddress();
            uint modelSegmentAddress = 0;
            for (int i = 0; i < Globals.objectComboEntries.Count; i++)
            {
                ObjectComboEntry entry = Globals.objectComboEntries[i];
                modelSegmentAddress = 0;
                if (level.ModelIDs.ContainsKey(ModelID))
                    modelSegmentAddress = level.ModelIDs[ModelID].GeoDataSegAddress;
                if (entry.ModelID == ModelID && entry.Behavior == behaviorAddr 
                    && entry.ModelSegmentAddress == modelSegmentAddress)
                {
                    objectComboEntry = entry;
                    Title = entry.Name;
                    return entry.Name;
                }
            }

            objectComboEntry = null;
            Title = "Undefined Combo (0x" + modelID.ToString("X2") + ", 0x" + behaviorAddr.ToString("X8") + ")";
            return Title;
        }

        public bool isPropertyShown(FLAGS flag)
        {
            return !isHidden(flag);
        }
    }
}
