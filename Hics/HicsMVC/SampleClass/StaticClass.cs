using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HicsMVC.SampleClass
{
    public class StaticClass
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string date;

        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        private string time;

        public string Time
        {
            get { return time; }
            set { time = value; }
        }

        private string group;

        public string Group
        {
            get { return group; }
            set { group = value; }
        }

        private bool lampstatus;

        public bool Lampstatus
        {
            get { return lampstatus; }
            set { lampstatus = value; }
        }


        public StaticClass(int id, string d, string t, string g, bool ls)
        {
            this.ID = id;
            this.Date = d;
            this.Time = t;
            this.Group = g;
            this.Lampstatus = ls;
        }
    }
}