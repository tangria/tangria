using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkStudyUI
{
    public class UCSFRecord
    {
        public string Year { get; set; }
        public string SocialSecurity { get; set; }
        public string AltID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string FedWorkStudy { get; set; }
        public string StateWorkStudy { get; set; }
        public string InstWorkStudy { get; set; }
        public string OtherWorkStudy { get; set; }
        public string Filler { get; set; }
        public string CarriageReturn { get; set; }
        public string LineFeed { get; set; }
    } // end class UCSF Record
}
