﻿using System;
using System.Collections.Generic;
using ISE_TEST_V1.Models;
namespace ISE_TEST_V1.ViewModels
{
    public class MarksView
    {

        public Question Q { get; set; }
        public List<Answer> A { get; set; }
        public Answers_Log AL { get; set; }
        public Mark db_mark { get; set; }
        
    }
}