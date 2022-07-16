using System;
using System.ComponentModel.DataAnnotations;

namespace Swallow.Models.Requests;    
    
    public class UpdateProjectReq
    {


        private string _name;
        private string _type;
        private string _expectedEnd;

        private string _description;



        public string? Name 
        { 
            get => _name; 
            set => _name = replaceEmptyWithNull(value); 
        }

        
        public string? Type 
        { 
            get => _type; 
            set => _type = replaceEmptyWithNull(value); 
        }

        
        public string? Description 
        { 
            get => _description; 
            set => _description = replaceEmptyWithNull(value); 
        }
        
        // public int CreatorId { get; set; }
        
        // public DateTime ExpectedEnd { get => new DateTime(int.Parse(_expectedEnd.Split("/")[0]), int.Parse(_expectedEnd.Split("/")[1]), int.Parse(_expectedEnd.Split("/")[2])); set => _expectedEnd = replaceEmptyWithNull(value); }

        
        public string? ExpectedEnd { get => _expectedEnd; set => _expectedEnd = replaceEmptyWithNull(value); }

        private string replaceEmptyWithNull(string value)
        {
            // replace empty string with null to make field optional
            return string.IsNullOrEmpty(value) ? null : value;
        }

        private DateTime stringToDate(string dateString) 
        {

            if (string.IsNullOrEmpty(dateString)) return DateTime.UnixEpoch;

            DateTime date = new DateTime(int.Parse(dateString.Split("/")[0]), int.Parse(dateString.Split("/")[1]), int.Parse(dateString.Split("/")[2]));

            return date;
        }

        private string dateTimeToString(DateTime date)
        {

            if (date == DateTime.UnixEpoch) return null;

            string stringDate = date.Year.ToString()+ "/" +date.Month.ToString()+ "/" +date.Day.ToString();

            return stringDate;
        }
    }
