﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace SavannaFrame.Classes
{
    public class KnowLedgeBase
    {
        // тут пока хз че (ну а так тут короче объединение переменных доменов и правил и действия с ними))
        public static List<Frame> Frames = new List<Frame>();
        public static int MaxFrameId = 0;
        public static int MaxSlotId = 0;

        public KnowLedgeBase()
        {
            Frames = new List<Frame>();
            MaxFrameId = 0;
            MaxSlotId = 0;
        }

        public void Save(string FStream)
        {
            if (Frames.Count > 0)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Frame>));
                FileStream fs = new FileStream(FStream, FileMode.Create);
                xmlSerializer.Serialize(fs, Frames);
                fs.Close();
                MessageBox.Show("Правила успешно сохранены!", "Информация");
            }
            else
            {
                MessageBox.Show("В базе знаний нет правил!", "Информация");
            }
        }

        public void Load(string fStream)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Frame>));
                TextReader textReader = new StreamReader(fStream);
                Frames = (List<Frame>)xmlSerializer.Deserialize(textReader);
                textReader.Close();
                MessageBox.Show("База знаний успешно загружена!!!!", "Информация");
            }
            catch
            {
                MessageBox.Show("Невозможно загрузить из файла!!!", "Ошибка");
            }
            finally
            {
                //if (FStream != null) FStream.Close();

            }
        }

        public int GetMaxNodeId(int i)
        {
            return MaxFrameId++;
        }

        public int GetMaxNodeId()
        {
            return MaxFrameId;
        }

        public int GetMaxSlotId(int i)
        {
            return MaxSlotId++;
        }

        public int GetMaxSlotId()
        {
            return MaxSlotId;
        }

        public bool AddFrame(Frame frame)
        {
            if (frame != null)
            {
                Frames.Add(frame);
                return true;
            }
            return false;
        }

        public bool DeleteFrame(string name)
        {
            if (name != String.Empty)
            {
                int index = Frames.FindIndex(f => f.FrameName == name);
                Frames.RemoveAt(index);
                return true;
            }
            return false;
        }

        public bool AddIsA(Frame frame)
        {
            // выбираем фрейм, у слота IsA присваиваем 
            Frames.Find(f => f.FrameId == frame.FrameId).IsA = frame.IsA;
            return true;
        }

        //public List<Slot> EditGridSlots(string framename)
        //{
        //    Frame frm = Frames.Find(f => f.FrameName == framename);
        //    List<Slot> lst = new List<Slot>();
        //    lst.Add(frm.IsA);
        //    lst.Add(frm.Error);
        //}

        public List<Frame> FrameList ()
        {
            return Frames;
        }

        public List<Slot> SlotList(int frameId)
        {
            List<Slot> listSlot = new List<Slot>();
            Frame frm = Frames.Find(f => f.FrameId == frameId);
            listSlot.Add(frm.IsA);
            listSlot.Add(frm.Error);
            listSlot.AddRange(frm.FrameSlots);
            return listSlot;
        }


    }
}
