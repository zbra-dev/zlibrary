using System;

namespace ZLibrary.Model {

    public class Date
    {

        public DateTime Value { get; private set; }

        public Date(DateTime date)
        {

            Value = date.Date;

        }

    }

}